using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaqueLaser : Projectile
{
    /// <summary>
    /// Le laser de la baguette à prisme.
    /// Un tir qui se charge puis qui bloque le joueur en place pour tout détruire sur son passage.
    /// </summary>

    public AnimationCurve ac;
    GameManager gm;
    public Gradient grad;
    public LayerMask Mask;
    public Character creator;
    public Transform muzzle;
    public Transform hitPos;
    public LineRenderer laser;
    float timer = 0;
    public float duration;
    bool lockedOn = false;
    public Vector2 mousePos, socketPos;
    float angle = 0;
    bool ending = false;
    [SerializeField]
    ParticleSystem paillettes = null;

    Gun creGun = null;

    public AudioClip laz2;

    public BoxCollider2D box;

    bool started = false;

    List<GameObject> touche = new List<GameObject>();


    public override void Starting()
    {
        box = GetComponent<BoxCollider2D>();
        StartCoroutine(Waiting());
        gm = FindObjectOfType<GameManager>();
        box.enabled = false;
    }

    //Une frame d'attente pour être sûr que tout soit bon, avant de récupérer les informations sur le joueur.
    IEnumerator Waiting()
    {
        yield return 0;
        creator = character;
        creator.stopped = true;
        muzzle = creator.gun.muzzle;
        yield return new WaitForSeconds(0.25f);
        started = true;
        creGun = creator.gun;
    }

    public override void Pattern()
    {

        if (started)
        {

            //Si le joueur meurt ou change d'arme, le laser est détruit.
            if (creator.life <= 0 || character.gun != creGun)
            {
                ending = true;
                timer = 0.75f;
                creator.lockOn = false;
                creator.gun.lockOn = false;
                Destroy(gameObject);
            }
            if (muzzle != null)
            {


                //On récupère le aim du propriétaire ==> le Vector2 du joystick droit
                Vector2 targeting = creator.aim;

                //On prend un point exagérément loin dans ce vecteur, pour avoir une meilleure précision
                mousePos = creator.mousePos;
                mousePos.Normalize();
                //On considère l'endroit où se trouve actuellement la socket
                socketPos = Camera.main.WorldToScreenPoint(creator.socket.transform.position);

                //Puis on calcule l'angle en radian qui en résulte, avant de le repasser en degrés pour avoir une valeur utile entre 180 et -180;
                angle = creator.angle;



                //Avec tout ça, on peut donc simuler l'angle et la direction du tir

                //Ensuite, on vérifie si le joueur est bien en train de viser, en regardant qu'on est au dessus de 0.25 dans au moins une direction. Le test est en valeur absolue pour prendre en compte la gauche et le bas
                if (Mathf.Abs(mousePos.x) > 0.25 || Mathf.Abs(mousePos.y) > 0.25)
                {
                    mousePos.Normalize();
                    hitPos.position = new Vector2(muzzle.position.x, muzzle.position.y) + mousePos * 40;



                }
                Vector3 hitVec = hitPos.position - muzzle.position;
                laser.SetPosition(0, muzzle.position);
                laser.SetPosition(1, muzzle.position + hitVec / 7);
                laser.SetPosition(2, muzzle.position + hitVec / 7 * 2);
                laser.SetPosition(3, muzzle.position + hitVec / 7 * 3);
                laser.SetPosition(4, muzzle.position + hitVec / 7 * 4);
                laser.SetPosition(5, muzzle.position + hitVec / 7 * 5);
                laser.SetPosition(6, muzzle.position + hitVec / 7 * 6);
                laser.SetPosition(7, hitPos.position);


            }





            //La phase de préparation
            if (!lockedOn)
            {


                timer += Time.deltaTime*2;

                laser.startWidth = Mathf.Lerp(0.5f, 1, 1 - timer);
                laser.endWidth = 0.5f;

                //La mise en place du tir
                if (timer >= 1)
                {
                    gm.Shake(5, 60, 1.05f);
                    laser.startWidth = 3;
                    laser.endWidth = 3;
                    lockedOn = true;
                    creator.lockOn = true;
                    creator.gun.lockOn = true;
                    GetComponent<AudioSource>().clip = laz2;
                    GetComponent<AudioSource>().Play();
                    ac.keys[1].value = 1;
                    ac.keys[0].value = 0f;
                    ac.keys[2].value = 0.33f;

                    laser.widthCurve = ac;
                    laser.widthMultiplier = 3;
                    box.enabled = true;
                }

            }
            //La phase de tir
            else if (!ending)
            {
                mousePos = hitPos.position - muzzle.position;



                duration -= Time.deltaTime;

                //La mise en place de la fin
                if (duration <= 0)
                {
                    box.enabled = false;
                    ending = true;
                    timer = 1;
                    creator.lockOn = false;
                    creator.gun.lockOn = false;
                    creator.stopped = false;

                }
                if (duration <= 0.5f)
                {
                    paillettes.Stop();
                }
            }

            //La fin du tir
            else
            {


                timer -= Time.deltaTime * 3;
                laser.startWidth = Mathf.Lerp(0, 3, timer);
                laser.endWidth = Mathf.Lerp(0, 3, timer);

                if (timer <= 0)
                {
                    Destroy(gameObject);
                }
            }


            if (muzzle != null)
            {
                transform.rotation = creator.gun.transform.rotation;
                transform.Rotate(new Vector3(0, 0, -90));
            }


        }
    }

}
