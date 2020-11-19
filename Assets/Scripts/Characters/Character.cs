using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;


public class Character : MonoBehaviour
{
    /// <summary>
    /// Les statistiques du personnage, en dehors de la vie.
    /// </summary>
    [Header("Stats")]
    //Nb de kills depuis la dernière mort
    public int killStreak;
    //Les trésors récoltés
    public List<int> tresors;
    //Le nombre de kills
    public int kills;
    //Le nombre de victoires
    public int lastStand = 0;
    //Le temps pour le spell
    public float timerComp, maxTimerComp;
    //Un id pour savoir qui est qui
    public int playerNum;
    //La vitesse de mouvement
    public float speed;
    //Vitesse de mouvement de base (utilisée pour le retour à la normale avec le jump et la menthe)
    public float baseSpeed;
    public bool lifesteal = false;
    public List<GameObject> dragons = new List<GameObject>();
    
    
    
    /// <summary>
    /// Les varibles pour le fonctionnement interne.
    /// </summary>
    [Header("Technique")]
    //Le personnage ne peut temporairement pas faire d'action. 
    bool stunned = false;

    //Fin de partie --> Bloque tous les persos
    public bool end;
    
    //Collider
    public BoxCollider2D box;
    //Quand le perso est arrêté et ne peux pas agir (note : à différencier du stun)
    public bool stopped;
    //La direction de mouvement et de visée
    public Vector2 movement;
    public Vector2 aim;
    //Le game manager
    public GameManager gm;
    //Permet de savoir quand on peut ramasser des trucs
    public FloorMe sol;
    Teleporter tpSol;
    //Le point de spawn, pour le début et le respawn
    public Vector3 spawnpoint;
    //Variable pour déterminer si on est en train de tirer. Sert pour pouvoir garder la touche enfoncée.
    public bool shooting = false;
    
    //Est-ce que l'aide à la visée est activée.
    bool sticky = false;
    //Permet de repérer les autres joueurs et de définir le plus proche, pour la visée assistée.
    List<float> targetAngles = new List<float>();

    //Le sprite renderer
    public SpriteRenderer sr;
    //L'animator
    public Animator anim;
    //Le rigidbody, pour le recul.
    Rigidbody2D rb;


    /// <summary>
    /// Tout ce qui concerne le shield d'aide
    /// </summary>
    [Header("HelpShield")]
    //Si le bouclier du dernier est activé.
    public bool shieldMe = false;
    //Le nombre de dégâts qu'il peut recevoir.
    int shieldLife = 0;
    //Sa vie max (pour pouvoir l'augmenter à chaque mort)
    public int maxShieldLife = 0;
    //Et l'objet en lui même, pour pouvoir activer le visuel.
    public GameObject shieldObject;



    /// <summary>
    /// La vie, son visuel, et la résurection.
    /// </summary>
    [Header("Life")]
    //La vie au dessus du perso
    public Slider lifebar;
    //Reduction des dégâts
    public bool reduc = false;
    //quand le perso stoppe les attaques
    public bool blocking;
    //Les pv et pv max
    public int life;
    //La vie maximum du joueur. Est notamment modifiée en cas de bug hunt.
    public int maxLife;
    //Le temps où on peut pas se faire toucher
    public float timeinvincible;
    //Combien de temps est invincible au repop
    public float respawnTime = 2.5f;

    /// <summary>
    /// Les feedbacks visuels et l'aide visuelle sur le perso
    /// </summary>
    [Header("FeedBack")]
    //Le truc pour voir son perso
    public GameObject pointeur, pointeurCercle;
    //Les paillettes quand on prend un coup
    public GameObject paillettes;
    
    /// <summary>
    /// Les autres aspects visuels du personnage.
    /// </summary>
    [Header("Visuals")]
    //Le sprite de mort (sert pour le respawn)
    public Sprite ded;
    //Les couleurs "de base". Utilisées pour colorer certains aspects.
    public Color[] playerCol;
    //Le canvas avec les infos
    public GameObject can;
    //La position du canvas
    Vector3 canPos;
    //La lumière qui s'active quand le joueur peut ulti
    public Light2D glowLight;
    //La petite poussière sur les déplacements
    public ParticleSystem dust;
    //Les 4 matériaux pour colorer les persos.
    [SerializeField]
    Material[] colorMat= new Material[0];

    
    /// <summary>
    /// La gestion de l'arme
    /// </summary>
    [Header("Stuff")]
    //L'arme du joueur
    public Gun gun;
    //L'endroit où on range l'arme
    public GameObject socket;
    //L'arme de départ 
    public GameObject startingGun;
    //Temps entre deux tirs
    public float cooldown = 1;
    //Variables pour déterminer où on vise (voir plus tard)
    public Vector3 mousePos;
    //L'angle de visée
    public float angle;
    //Le point d'ancrage des armes
    public Vector3 socketPos;
    //Quand le perso est bloqué sur son tir (exemple de l'ultralaser)
    public bool lockOn;
    //Le transform des joueurs, utilisés pour l'aide à la visée.
    List<Transform> otherPlayers = new List<Transform>();

    /// <summary>
    /// L'aspect audio
    /// </summary>
    [Header("Sound")]
    //Les audiosources
    public AudioSource cast = null;
    public AudioSource damaged = null;
    public AudioSource dedSon = null;
    public AudioSource blok = null;
    [SerializeField]
    AudioSource ultLancement = null;

    /// <summary>
    /// La gestion du bughunt
    /// </summary>
    [Header("Manhunt")]
    //L'activation
    public bool hunted = false;
    //Les pv qu'on a en étant hunted
    int huntedLife=0;
    //La taille du perso agrandi 
    Vector3 huntedSize= new Vector3(-3,3,1);
    //Et la taille de base, pour le retour à la normale.
    Vector3 baseSize = new Vector3(-1.7f, 1.7f, 1);

    /// <summary>
    /// Tout ce qui est cassé ou pas utilisé mais qui pourrait servir plus tard
    /// </summary>
    [Header("Obsolete")]
    //Le nb du joueur, au dessus de sa tête
    public Text numberP;
    //La flèche qui indique le joueur
    public Image fleche;
    //Sensé gérer les moteurs de la manette. Actuellement hors service.
    public float left = 0, right = 0;

    //Pour les stats uniques à un perso
    public virtual void Starting()
    {

    }

    //Temps d'attente au début de la partie
    IEnumerator WaitSpawn()
    {
        
            
        //La mise en place des valeurs audio
        var potato = GetComponents<AudioSource>();
        for (int i = 0; i < potato.Length; i++)
        {
            potato[i].spatialBlend = 1;
            potato[i].minDistance = 8;
            potato[i].maxDistance = 20;
            potato[i].volume = 1;
            potato[i].dopplerLevel = 0;
        }

        //On setup la vie pour le mode bugHunt
        huntedLife = maxLife + 10;

        //Le perso est inactif pendant quelques secondes, et on affiche la vie.
        stopped = true;
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowLife());
        if (gm.mode == 3)
        {
            cooldown = 3;
        }
        stopped = false;
        //La protection en early.
        StartCoroutine(StartShield());
    }

    //Appelé par le GM Permet de tout mettre en place
    public void SetStats(int number, bool stick)
    {
        sticky = stick;
        playerNum = number;
        //On récupère les components
        rb = GetComponent<Rigidbody2D>();
        canPos = can.transform.localPosition ;
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        sr.material = colorMat[playerNum];
        //Les stats supplémentaires
        Starting();

        //Le gm donne au joueur son identité, et on prend le material qui va avec
        
       

        //On cherche les spawns et on trouve celui qui a la bonne id
        SpawnPoint[] spawns = FindObjectsOfType<SpawnPoint>();
        foreach (SpawnPoint spa in spawns)
        {
            if (spa.player == playerNum)
            {
                spawnpoint = spa.transform.position;
            }
        }
        spawnpoint = new Vector3(spawnpoint.x, spawnpoint.y, -5);
        //Puis on se tp dessus
        transform.position = spawnpoint;

        //On créé l'arme de départ et on la grab
        GameObject temp = Instantiate(startingGun, transform.position, Quaternion.identity);
        temp.GetComponent<Gun>().GrabMe(this);

        //On met à jour les éléments colorés et on désactive ce qui est pas nécessaire actuellement
        numberP.text = "P" + (playerNum + 1).ToString();
        numberP.color = playerCol[playerNum];
        fleche.color = playerCol[playerNum];
        pointeur.GetComponent<SpriteRenderer>().color = playerCol[playerNum];
        pointeurCercle.GetComponent<SpriteRenderer>().color = new Color(playerCol[playerNum].r, playerCol[playerNum].g, playerCol[playerNum].b, 0.5f);
        shieldObject.GetComponent<SpriteRenderer>().color = playerCol[playerNum];
        shieldObject.SetActive(false);
        pointeurCercle.SetActive(false);
        pointeur.SetActive(false);
        //La lumière du perso est de sa couleur, sauf le bleu, qui est turquoise.
        glowLight.color = playerCol[playerNum];
        if (playerNum == 1)
        {
            glowLight.color = new Color(0, 1, 1, 1);
        }

        //On met en place les stats de la vie
        lifebar.maxValue = maxLife;
        lifebar.value = maxLife;

        //L'attente est lancée
        StartCoroutine(WaitSpawn());

        //On fixe la vitesse par défaut du perso
        baseSpeed = speed;


            glowLight.intensity = 3.5f;
        //On la désactive ensuite
        glowLight.enabled = false;

        
        StartCoroutine(UneSeconde());
        

    }

    //Simplement bloquer pendant 2 secondes au lancement du jeu
    IEnumerator StartShield()
    {
        blocking = true;
        yield return new WaitForSeconds(2);
        blocking = false;
    }

    //Une pause d'une frame à la création, pour s'assurer que tous les persos sont bien en jeu, et donc pouvoir les ajouter à la liste, pour l'aide à la visée. 
    IEnumerator UneSeconde()
    {
        yield return 0;
        for (int i = 0; i < 4; i++)
        {
            if (gm.chosenCharacters[i] != -1 && i != playerNum)
            {
                otherPlayers.Add(gm.team[i].transform);
                targetAngles.Add(0);
            }
        }
    }



    //Activer la vision sur le perso
    public void OnShowChar()
    {
        StartCoroutine(ShowCharacter());
    }


    //Récupère la valeur du stick gauche, avec une deadzone de 40%
    public void OnMove(InputValue value)
    {
        Vector2 potato = value.Get<Vector2>();
        if (potato.x < 0.4 && potato.x > -0.4)
        {
            potato.x = 0;
        }
        if (potato.y < 0.4 && potato.y > -0.4)
        {
            potato.y = 0;
        }
        movement = potato;
    }


    //Récupère la valeur du stick droit, avec une deadzone de 10%
    public void OnLook(InputValue value)
    {
        if (!lockOn)
        {
            Vector2 potato = value.Get<Vector2>();
            if (potato.x > 0.1 || potato.x < -0.1 || potato.y > 0.1 || potato.y < -0.1)
            {
                aim = value.Get<Vector2>();
            }
        }
        
        
    }

    //S'active quand on appuie sur le bouton pour ramasser
    public void OnGrab()
    {
        //On ramasse une arme, s'il y en a une
        if (sol != null)
        {
            sol.GrabGun(this);
        }
        //Sinon ça vérifie les tp. Note perso : Si on met d'autres objets intéractifs, ça serait pas déconnant de faire une fonction commune
        else if (tpSol != null)
        {
            tpSol.TPMe(gameObject);
        }
    }

    //Quand on active sa compétence
    public void OnSpell()
    {
        if(gm.mode!=2 && gm.mode != 3)
        {
            //Si le cooldown est fini, et que le joueur peut agir;
            if (timerComp <= 0 && !stunned && !stopped)
            {
                //S'il n'a pas assez de kills, ou qu'un ulti est déjà en cours, la compétence normale s'active.
                if (killStreak < 3 || gm.ult)
                {
                    cast.Play();
                    timerComp = maxTimerComp;
                    Competence();
                }
                //Sinon, il peut utiliser son ulti.
                else
                {
                    ultLancement.Play();
                    Ultimate();
                }
            }
        }
        
    }

    //Deuxième touche de spell, pour éviter le bug chelou d'unity
    public void OnAltSpell()
    {
        if (gm.mode != 2 && gm.mode != 3)
        {
            //Si le cooldown est fini, et que le joueur peut agir;
            if (timerComp <= 0 && !stunned && !stopped)
            {
                //S'il n'a pas assez de kills, ou qu'un ulti est déjà en cours, la compétence normale s'active.
                if (killStreak < 3 || gm.ult)
                {
                    cast.Play();
                    timerComp = maxTimerComp;
                    Competence();
                }
                //Sinon, il peut utiliser son ulti.
                else
                {
                    ultLancement.Play();
                    Ultimate();
                }
            }
        }
    }

    //Quand on appuie sur le bouton pour tirer
    public void OnFire()
    {
        shooting = true;
    }

    //Quand on relache le bouton pour tirer
    public void OnFire1()
    {
        shooting = false;
    }

    //Deuxième touche pour tirer, bug unity.
    public void OnAltFire()
    {
        shooting = true;
    }

    //Deuxième touche à relacher, bug unity.
    public void OnAltFire1()
    {
        shooting = false;
    }

    //Permet de se débarasser de son arme et reprendre celle de départ.
    public void OnBack()
    {
        if (!stopped)
        {
            gun.Discard();
            GameObject temp = Instantiate(startingGun, transform.position, Quaternion.identity);
            temp.GetComponent<Gun>().GrabMe(this);
        }
        
        
    }

    //La visée
    public void Aiming()
    {
        //On casse le vecteur de visée en variables
        float aH = aim.x;
        float aV = aim.y;
        //Une variable pour l'angle d'aide à la visée.
        float newAngle = 360;

        //On prend un point exagérément loin. Note : Je sais pas si c'est utile, tho
        mousePos = Camera.main.WorldToScreenPoint(socket.transform.position) + new Vector3(aH, aV, 0) * 1000;
        socketPos = Camera.main.WorldToScreenPoint(socket.transform.position);
        //On créé de nouvelles infos avec le point visé par rapport au point d'ancrage de l'arme
        mousePos.x = mousePos.x - socketPos.x;
        mousePos.y = mousePos.y - socketPos.y;
        //Puis on en tire l'angle, pour pouvoir orienter la socket dans la bonne direction, et donc viser où on veut
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        

        //Si on utilise l'aide à la visée
        if (sticky)
        {
            //Vérifier si on a quelqu'un dans le cone autour de notre angle.
            bool verifiedAngle = false;
            //Et l'angle du personnage le plus proche de ce que l'on vise
            float minAngle = 360;

            for (int i = 0; i < otherPlayers.Count; i++)
            {
                //On vérifie la position relative des autres persos par rapport à nous
                Vector2 targetPos = (otherPlayers[i].position - transform.position);
                //On en tire un angle
                targetAngles[i] = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                //Cet angle est comparé avec celui de notre visée. En valeur absolue, c'est important.
                float diff = Mathf.Abs(Vector2.Angle(mousePos, targetPos));

                //Ensuite, si on est en dessous des 5° de précision, et du dernier angle le plus bas
                if (diff < 5f && diff < minAngle)
                {
                    //On récupère cette différence comme étant l'angle le plus petit, on confirme qu'un angle a été trouvé, et on adapte la position de visée
                    minAngle = diff;
                    newAngle = targetAngles[i];
                    verifiedAngle = true;
                    mousePos = targetPos;
                }
            }

            //Si un angle assez bas a été trouvé, on donne cette valeur à la visée
            if (verifiedAngle)
            {
                
                angle = newAngle;
            }
        }

        //Une fois le processus de visée terminé, on applique l'angle à la rotation de la socket dans laquelle se trouve l'arme.
        socket.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //Le déplacement
    public void Movement()
    {
        
        //On casse le vecteur de mouvement
        float h = movement.x;
        float v = movement.y;

        //Si le joueur va à gauche on le flip, sinon on le déflip
        if (h < -0.4)
        {
            if (sr.flipX == false)
            {
                sr.flipX = true;
            }
        }
        else if (h > 0.4)
        {
            if (sr.flipX == true)
            {
                sr.flipX = false;
            }
        }

        //Déplacements, avec un /10 parce que le nouveau système faisait des sanics
        transform.Translate(new Vector2(h, v) * speed/10 * Time.deltaTime);

        //On récupère une variable avec le mouvement, pour définir si le perso bouge, et lancer l'anim de marche
        float move = (Mathf.Abs(h) + Mathf.Abs(v)) * speed;
        anim.SetFloat("movement", move);

        //En même temps que l'anim de marche, on lance le particle system qui fait de la poussière
        if (move > 0.4f)
        {
            if(!dust.isPlaying)
            dust.Play();
        }
        else
        {
            dust.Stop();
        }
    }

    //Actions unique au perso
    public virtual void Pattern()
    {

    }


    public void FixedUpdate()
    {
        if (dragons.Count <= 0)
        {
            if (gun.multiPattern > 1)
            {
                gun.multiPattern = 1;
            }
            if(gun.multishot > 1)
            {
                gun.multishot = 1;
            }
        }
        //On active la lumière si le joueur a assez de killstreak ou qu'il est en bughunt
        if (killStreak >= 3 || hunted)
        {
            glowLight.enabled = true;
        }
        else
        {
            glowLight.enabled = false;
        }

        //Si le joueur est en train d'appuyer sur la touche de tir
        if (shooting)
        {
            //S'il peut agir
            if (!end && !stopped && !stunned)
            {
                //On vérifie de bien avoir une arme (même si normalement c'est impossible de pas en avoir)
                if (gun != null)
                {
                    //On vérifie bien que le cd est fini, qu'on a rechargé, et qu'on est pas en train de tirer
                    if (cooldown <= 0 && !gun.shooting && !gun.attacking)
                    {
                        //Puis on lance le gunning (voir Gun) et le cooldown
                        gun.Gunning();
                        cooldown = gun.cooldown;
                    }
                }
            }
        }

        //Le canvas est fixé pour éviter qu'il essaye de partir quand on touche un mur
        can.transform.localPosition = canPos;

        //Si on est pas en fin de partie
        if (!end)
        {
            //Ni bloqué
            if (!stopped && !stunned)
            {
                //Movement et Aiming se lancent à chaque frame
                Movement();
               
            }

            //On peut viser même quand on est stun, je trouve ça plus pratique
            Aiming();

            //Si on a une arme en main, et qu'on est pas en train de tirer/recharger, on descend le cooldown
            if (gun != null)
            {
                if (cooldown <= 0 && !gun.shooting && !gun.attacking)
                {
                }
                else
                {
                    if (cooldown > 0)
                    {
                        cooldown -= Time.deltaTime;
                    }

                }

            }

            //On baisse le temps du spell
            if (timerComp > 0)
            {
                timerComp -= Time.deltaTime;
            }

            //On suit les actions du perso
            Pattern();
        }
        
    }

    //Quand on respawn
    public void Respawn()
    {
        box.isTrigger = false;
        //L'arme est réactivée
        gun.gameObject.SetActive(true);
        //Trigger pour passer du sprite ded au idle
        anim.SetTrigger("alive");
        //On se retrouve au point de spawn
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //Le perso est souligné au spawn
        StartCoroutine(ShowCharacter());
        //Son killstreak est remis à 0
        killStreak = 0;
        //Le cooldown ded son arme est remis à 0, au cas où.
        cooldown = 0;

        //Si le shield du dernier est activé, on augmente sa résistance et on l'active.
        if (shieldMe)
        {
            maxShieldLife+=2;
            shieldLife = maxShieldLife;
            shieldObject.SetActive(true);
        }
        //On revient full vie, au point de spawn, avec X temps invincible
            life = maxLife;
            transform.position = spawnpoint;


        stopped = false;
        box.enabled = true;
        //La barre de vie apparaît aussi
        StartCoroutine(ShowLife());
        StartCoroutine(StartShield());

    }

    //Quand on entre dans le trigger d'une arme au sol, on peut la prendre. S'applique aussi aux autres intéractions.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "floorG")
        {
            sol = collision.gameObject.GetComponent<FloorMe>();
            collision.gameObject.GetComponent<FloorMe>().ShowCan(gameObject);
        }
        else if (collision.tag == "tp")
        {
            tpSol = collision.GetComponent<Teleporter>();
            tpSol.ShowCan(gameObject);
        }

    }

    //Quand on sort du trigger d'une arme au sol, on peut plus la prendre. Pareil pour les intéractions.
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (sol != null)
        {
            if (collision.gameObject == sol.gameObject)
            {
                sol = null;
                collision.gameObject.GetComponent<FloorMe>().HideCan(gameObject);
            }
        }
        if (tpSol != null)
        {
            if (collision.gameObject == tpSol.gameObject)
            {
                tpSol = null;
                collision.gameObject.GetComponent<Teleporter>().HideCan(gameObject);
            }
        } 
    }

    

    //Quand on prend des dégâts
    public void Damage(int dam, int source, bool go, bool lifeSteal)
    {
        //Si on bloque, il ne se passe rien
        if (!blocking || go)
        {
            
            float damage = dam;
            //Si les dégats sont réduits, on les divise par deux.
            if (reduc)
            {
                //Noter qu'on arrondit au supérieur
                damage = Mathf.CeilToInt(damage / 2);
                dam = (int)damage;
            }
            if (lifeSteal)
            {
                gm.team[source].HealMe(dam);
            }
            //Un léger screenshake est lancé
            gm.Shake(0.5f, 10, 1.05f);

            //On devrait avoir des vibrations mais ça marche pas.
            /*
            StopCoroutine(Shaking());
            StartCoroutine(Shaking());
            */

            //Si le shield existe, c'est lui qui prend les dégâts. Il disparait s'il tombe en dessous de 0hp.
            if (shieldLife > 0)
            {
                shieldLife-=dam;
                if (dam > 0)
                {
                    blok.Play();
                }
                
                if (shieldLife <= 0)
                {
                    shieldObject.SetActive(false);
                }
            }
            else
            {
                //On peut seulement blesser un joueur encore en vie
                if (life > 0)
                {
                    //Les paillettes apparaissent, pour rendre ça plus visible
                    Destroy(Instantiate(paillettes, transform.position, Quaternion.identity), 1);
                    //Il perd de la vie en fonction des damages
                    life -= dam;

                    //S'il meurt, on active l'animation, la coroutine de repop, et on dit au gm qui c'est qui a fait le kill
                    if (life <= 0)
                    {
                        //On joue l'audio de mort, et on met la vie à 0 pour éviter les trucs weird avec la barre de vie.
                        dedSon.Play();
                        life = 0;
                        StartCoroutine(RespawnTime());
                        gm.Kill(source, playerNum);

                        //On fait tomber les trésors
                        if (tresors.Count >= 1)
                        {
                            //On fait tomber précisément la moitié du butin arrondit au supérieur, en partant des plus anciens.
                            int potato = Mathf.CeilToInt(tresors.Count / 2);
                            for (int i = 0; i < potato; i++)
                            {
                                //On invoque les trésors, en mettant un gros si nécessaire.
                                if (tresors[0] == 1)
                                {
                                    gm.SummonTreasure(transform.position);
                                }
                                else
                                {
                                    gm.SummonBigTreasure(transform.position, source);
                                }
                                //Les trésors sont ensuite retirés.
                                tresors.RemoveAt(0);
                            }
                        }

                        if (hunted)
                        {
                            //Si le joueur était en bughunt, on remet ses stats comme avant.
                            FindObjectOfType<StatsManager>().timefirst[playerNum] = 0;
                            hunted = false;
                            transform.localScale = baseSize;
                            maxLife = huntedLife - 10;
                            gm.manHunt = false;
                            //Et on invoque autant de trésors qu'il lui reste
                            if (tresors.Count >= 1)
                            {
                                for (int i = 0; i < tresors.Count; i++)
                                {
                                        gm.SummonTreasure(transform.position);
                                }
                            }
                        }
                    }
                    //Sinon on lance l'anim de damages
                    else
                    {
                        //Le son de dégâts se joue.
                        if (dam > 0)
                        {
                            damaged.Play();
                            anim.SetTrigger("damage");
                        }
                        
                    }
                }
            }
        }
        //Si on bloque, le son de bloquer se joue
        else
        {
            if (dam > 0)
            {
                blok.Play();
            }
            
        }

        if (dam > 0)
        {
            //Si la vie était déjà montrée, on coupe la coroutine, puis on la relance, pour éviter les conflits
            StopCoroutine(ShowLife());
            StartCoroutine(ShowLife());
        }
        
    }
    
    //L'utilisation de la compétence du perso
    public virtual void Competence()
    {

    }

    

    //Pour respawn
    public IEnumerator RespawnTime()
    {
        //On désactive la hitbox et l'arme du persos, et on peut plus bouger, jusqu'à la fin de la coroutine.
        box.enabled = false;
        gun.gameObject.SetActive(false);
        stopped = true;
        anim.SetTrigger("dead");

        if(gm.mode==1 || gm.mode == 0)
        {
            //On reste mort ce temps
            yield return new WaitForSeconds(4);
            
            //Puis on respawn
            Respawn();

        }
        else
        {
            yield return null;
        }
    }

    //Gagner des trésors
    public void AddLoot(int value)
    {
        tresors.Add(value);        
    }

    //Récupérer le score d'un joueur
    public int AskScore()
    {
        //Variable pour voir le score, en fonction du mode
        int potato = 0;

        if (gm.mode==1)
        {
            potato = kills;
        }

        else if(gm.mode==0)
        {
            if (tresors.Count > 0)
            {
                for (int i = 0; i < tresors.Count; i++)
                {
                    potato += tresors[i];
                }
            }

            
        }
        else
        {
            potato = lastStand;
        }
        return potato;
    }

    //Activation du bughunt
    public void HuntMe()
    {
        //On change les stats du joueur, et on active sa barre de vie pour montrer qu'il est passé full life.
        maxLife = huntedLife;
        life = maxLife;
        hunted = true;
        transform.localScale = huntedSize;
        lifebar.gameObject.SetActive(false);
        StartCoroutine(ShowLife());
    }

    public void HealMe(int heal)
    {
        life += heal;
        if (life > maxLife)
        {
            life = maxLife;
        }
        StopCoroutine(ShowLife());
        StartCoroutine(ShowLife());
    }

    //Permet de montrer la vie du joueur
    IEnumerator ShowLife()
    {
        lifebar.gameObject.SetActive(true);
        lifebar.value = life;
        yield return new WaitForSeconds(1.5f);
        if (life > 5 || life<=0)
        {
            lifebar.gameObject.SetActive(false);
        }
    }

    //Active le pointeur autour du joueur
    IEnumerator ShowCharacter()
    {
        pointeur.SetActive(true);
        pointeurCercle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        pointeurCercle.SetActive(false);
        pointeur.SetActive(false);
    }

    //Activation de l'ulti
    public virtual void Ultimate()
    {

    }

    //Les vibrations. Ne marche pas.
    IEnumerator Shaking()
    {
        /*left += 0.25f;
        right += 0.75f;
        Gamepad.current.SetMotorSpeeds(left, right);
        yield return new WaitForSeconds(1.5f);

        Gamepad.current.SetMotorSpeeds(0, 0);
        left = 0;
        right = 0;*/

        yield return 0;
    }

    public void Stunned(float dure)
    {
        StartCoroutine(StunnedOut(dure));
    }


    IEnumerator StunnedOut(float dure)
    {
        stopped = true;
        yield return new WaitForSeconds(dure);
        stopped = false;
    }
}
