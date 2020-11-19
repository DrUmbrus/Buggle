using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    /// <summary>
    /// Classe générale pour les guns
    /// </summary>

    //La couleur de base de l'arme (outil de test, pour faire des swap colors en attendant les sprites)
    public Color baseCol = Color.white;

    //La distance du laser de visée (utile pour les armes à courte portée)
    public float laserRange=100;
    //L'objet qu'on a tiré. Sert pour le cas du boomerang.
    public GameObject shootyObj;
    public float offset = 0.5f;
    
    public bool standing = false;
    //Détermine ce qui n'arrête pas le raycast
    public LayerMask Mask;
    //Le renderer pour faire le laser de visée
    public LineRenderer lr;
    //Le point d'ancrage du laser
    public Transform hitPos;

    //L'animator
    public Animator anim;

    //Le projectile à invoquer
    public GameObject projec;

    //Le joueur qui tient l'arme
    public Character ownerChar;

    //L'id du joueur
    public int owner;
    
    //La socket où est placée l'arme
    public GameObject socket;

    //L'angle de spread sur les tirs
    public int maxAngle;

    //Un prefab pour quand on repose l'arme par terre
    public GameObject floorMe;
    //Le point d'où part le projectile
    public Transform muzzle;
    
    //Le temps minimal entre deux tirs
    public float cooldown;
    //Le sprite renderer
    public SpriteRenderer sr;

    //Le point utilisée pour la visée
    public Vector3 mousePos;
    //L'angle de visée
    public float angle;

    //Les sprites qui déterminent quand est-ce qu'on tire (shotSprite), et à quelle frame on lance l'animation de recharge (reloadSprite)
    public Sprite shotSprite;
    //Le sprite de base
    public Sprite baseSprite;
    //Est-ce uq'on est en train de tirer
    public bool shooting = false;
    //Est-ce qu'on est en train d'attaquer 
    public bool attacking = false;


    //L'audioSource
    public AudioSource audSo;

    //Clip audio de quand on tire et de quand on recharge
    public AudioClip shoot;

    //Les 4 couleurs de joueurs, pour le laser
    public Color[] lines;

    //Permet de passer l'arme à gauche
    public bool flipped=false;

    public Vector3 socketPos;

    //Nombre de tirs par activation
    public int multishot=1;
    //Temps entre deux tirs pour les armes à rafale.
    public float timebetweenshot = 0.1f;
    //Les armes de jet (boomerang et couteau) => Fonctionnement un peu différent
    public bool throwable;


    //Bloquer la visée (cas de l'ultralaser)
    public bool lockOn;

    //Nombre de tirs par activation, pour le pattern secondaire;
    public int multiPattern=1;

    public int munitions = 9999;

    private void Awake()
    {
       
        //Récupérer les composants
        sr = GetComponent<SpriteRenderer>();
        baseCol = sr.color;
        anim = GetComponent<Animator>();
        baseSprite = sr.sprite;
        audSo = GetComponent<AudioSource>();
        lr = GetComponent<LineRenderer>();

        if(throwable && ownerChar != null)
        {
            Visual();
        }
        var potato = GetComponents<AudioSource>();
        for (int i = 0; i < potato.Length; i++)
        {
            potato[i].spatialBlend = 1;
            potato[i].minDistance = 8;
            potato[i].maxDistance = 20;
            potato[i].volume = 0.7f;
            potato[i].dopplerLevel = 0;
        }
    }



    private void LateUpdate()
    {
        if (!standing) { 
        //Flip l'arme si on vise à gauche, pour éviter qu'elle soit à l'envers. On adapte aussi la position du point de tir
        if (transform.position.x < socket.transform.position.x)
        {
            if (!flipped)
            {
                sr.flipY = true;
                flipped = true;
                muzzle.transform.localPosition = new Vector3(muzzle.transform.localPosition.x, -muzzle.transform.localPosition.y, muzzle.transform.localPosition.z);
            }

        }
        else
        {
            if (flipped)
            {
                sr.flipY = false;
                flipped = false;
                muzzle.transform.localPosition = new Vector3(muzzle.transform.localPosition.x, -muzzle.transform.localPosition.y, muzzle.transform.localPosition.z);
            }
        }
        }


        //Si on est en train de tirer, qu'on a atteint le sprite de tir, et que la balle est pas encore partie
        if(sr.sprite==shotSprite && shooting && !attacking)
        {
            //On active attacking, pour noter qu'on a pas fini l'animation
            attacking = true;
            //On désactive shooting, comme deuxième protection pour éviter que ça tire à chaque frame
            shooting = false;
            //On lance la fonction de shoot
            Shooting();
            //On joue l'audio s'il y en a un
            if (audSo != null)
            {
                audSo.clip = shoot;
                audSo.Play();
            }
        }

        //Si on était en train de tirer mais qu'on est revenu au sprite de base (fin de l'animation donc), on désactiver les sécurités.
        if (sr.sprite == baseSprite && attacking && !throwable)
        {
            attacking = false;
            shooting = false;
           // munitions--;
        }

        if(ownerChar.cooldown<=0 && (attacking || shooting))
        {
            attacking = false;
            shooting = false;
        }
        //On lance le laser
        Laser();
        Pattern();

        //Quand on a plus de munitions, le joueur récupère son arme de base.
        if (munitions <= 0 && ownerChar.cooldown<=0)
        {
            GameObject temp = Instantiate(ownerChar.startingGun, transform.position, Quaternion.identity);
            temp.GetComponent<Gun>().GrabMe(ownerChar);
            Destroy(gameObject);
        }

        //On fait réaparaitre l'arme quand on cd est fini (cas des armes de lancer)
        if (ownerChar.cooldown <= 0)
        {
            sr.color = baseCol;
        }

    }

    //Ce qui se passe d'unique à l'arme dans le update (utile pour les lasers par exemple)
    public virtual void Pattern()
    {

    }

    public void Laser()
    {
        if (!standing)
        {


            //On récupère le aim du propriétaire ==> le Vector2 du joystick droit
            Vector2 targeting = ownerChar.aim;

            //On prend un point exagérément loin dans ce vecteur, pour avoir une meilleure précision
            mousePos = ownerChar.mousePos;
            mousePos.Normalize();
            //On considère l'endroit où se trouve actuellement la socket
            socketPos = Camera.main.WorldToScreenPoint(socket.transform.position);

            //Puis on calcule l'angle en radian qui en résulte, avant de le repasser en degrés pour avoir une valeur utile entre 180 et -180;
            angle = ownerChar.angle;

            //Avec tout ça, on peut donc simuler l'angle et la direction du tir


            //Ensuite, on vérifie si le joueur est bien en train de viser, en regardant qu'on est au dessus de 0.25 dans au moins une direction. Le test est en valeur absolue pour prendre en compte la gauche et le bas
            if (Mathf.Abs(ownerChar.aim.x) > 0.25f || Mathf.Abs(ownerChar.aim.y) > 0.25f)
            {
                //On active le laser
                lr.enabled = true;
                RaycastHit2D hit = Physics2D.Raycast(muzzle.position, new Vector2(mousePos.x, mousePos.y), laserRange, Mask);
                if (hit)
                {
                    //Le point d'arrivée est placé là où le rayon s'arrête
                    hitPos.position = hit.point;
                }
                else
                {
                    
                    hitPos.position = muzzle.position + mousePos*laserRange;
                }

                //On place entre le point de départ du renderer sur le point de tir, et son point d'arrivée sur la position du rayon, pour créer le laser visible
                lr.SetPosition(0, muzzle.position);
                lr.SetPosition(1, hitPos.position);
            }
            //On désactive le laser si le joueur n'est pas activement en train de viser, pour éviter certains pb.
            else
            {
                if (!lockOn)
                {
                    lr.enabled = false;

                }
            }
        }
        else
        {
            lr.enabled = false;
            transform.rotation = Quaternion.identity;
        }
        
    }

    //Le moment où la balle part
    public void Shooting()
    {
        
        //munitions--;
        StartCoroutine(Rafale());
        StartCoroutine(DoubleShot());
    }

    //Le pattern des tirs secondaires. (par exemple pour le couteau qui tire en cone, ou la pelle qui a deux types de projectiles.
    public virtual void ShootingPattern()
    {

    }

    IEnumerator DoubleShot()
    {
        for (int i = 0; i < multiPattern; i++)
        {
            ShootingPattern();
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    IEnumerator Rafale()
    {
        for (int i=0; i<multishot; i++)
        {
            //On créé l'objet
            GameObject temp = Instantiate(projec, muzzle.position, Quaternion.identity);

            //On adapte ensuite le tir pour qu'il soit dans l'angle visé
            temp.transform.rotation = Quaternion.Euler(0, 0, angle);

            //On rajoute un -90 parce que le truc marche pas sinon. Et on y ajoute un random qui permet de donner de la dispersion.
            temp.transform.Rotate(new Vector3(0, 0, -90 + Random.Range(-maxAngle, maxAngle + 1)));
            //Enfin, on dit à la balle qui est son créateur.
            temp.GetComponent<Projectile>().Create(owner, ownerChar.lifesteal);
            temp.GetComponent<Projectile>().character=ownerChar;
            
            shootyObj = temp;
            yield return new WaitForSeconds(timebetweenshot);
        }
        munitions--;
    }

    //Invoqué par le joueur pour lancer l'animation et le mode shoot
    public void Gunning()
    {
        anim.SetTrigger("Shot");
        shooting = true;
    }

    //Le moment où on attrape l'arme (voir les script Floor)
    public void GrabMe(Character chara)
    {
        //On remet à zéro la socket
        chara.socket.transform.rotation = Quaternion.identity;
        //On dit à l'arme qui est son propriétaire
        ownerChar = chara;
        owner = chara.playerNum;
        ownerChar.cooldown = 0;
        //On place l'arme dans la socket, et on récupère l'objet.
        socket = chara.socket;
        transform.parent = socket.transform;
        
        //On s'assure que l'arme est bien en position zéro, puis on la translate pour qu'elle ne cache pas le joueur.
        transform.localPosition=Vector3.zero;
        transform.Translate(new Vector3(offset, 0, 0));

        //Si le joueur avait déjà une arme en main, il la pose (créé un FloorMe)
        if (chara.gun != null)
        {
            chara.gun.Discard();
        }
        //On dit au joueur qu'il tient bien cette arme
        chara.gun = this;
        //Et on récupère sa couleur pour le laser
        lr.startColor = lines[owner];
        lr.endColor = lines[owner];
        Grabbed2();
        lr.startColor = new Color(lines[owner].r, lines[owner].g, lines[owner].b, 1);
        lr.endColor = new Color(lines[owner].r, lines[owner].g, lines[owner].b, 1);
    }

    public virtual void Grabbed2()
    {

    }

    //Détruire l'arme quand on la pose.
    public void Discard()
    {
       
        Destroy(gameObject);
    }


    public void Visual()
    {
        sr.color = baseCol;
        ownerChar.cooldown = 0;
        attacking = false;
        munitions--;
    }

    //Sécurité avec le ptn de boomerang
    public void NoCD()
    {
        ownerChar.cooldown = 0;
        attacking = false;
    }
}
