using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangShot : Projectile
{
    /// <summary>
    /// Le boomerang
    /// </summary>

    Character owner = null;
    GameObject creator;
    public Vector3 target, spawn;
    float timer = 0;
    bool retour=false;
    public bool started = false;
    public float dist;


    public override void Starting()
    {
        StartCoroutine(TargetAcquired());
    }

    //Quand on le lance, il définit son point d'arrivée en fonction de l'angle de tir et de sa portée.
    IEnumerator TargetAcquired()
    {
        yield return 0;
        
        
        Character[] potato = FindObjectsOfType<Character>();
        foreach (Character dude in potato)
        {
            if (dude.playerNum == player)
            {
                creator = dude.gameObject;
                owner = dude;
            }
        }

        spawn = creator.transform.position;

        Vector3 aim = new Vector3(transform.position.x - spawn.x, transform.position.y - spawn.y, 0);
        aim.Normalize();
        aim *= dist;
        target = (transform.position + aim);

        spawn = transform.position;
        started = true;
    }


    public override void Pattern()
    {
        
        if (started)
        {
            //Si le joueur meurt, le boomerang disparait.
            if (owner.life <= 0)
            {
                Destroy(gameObject);
            }

            //Il tourne et se déplace
            transform.Rotate(new Vector3(0, 0, 20));
            transform.position = Vector3.Lerp(spawn, target, timer);
            timer += Time.deltaTime*speed;

            //Lors du retour, il vise le joueur au lieu de sa cible de départ.
            if (retour)
            {
                target = creator.transform.position;
            }

            //Une fois la cible atteinte, on lance la fonction de retour. Et si on finit le retour, on détruit l'objet.
            if (timer >= 1)
            {
                if (!retour)
                {
                    Retour();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Retour()
    {
        spawn = transform.position;
        retour = true;
        timer = 0;
    }



    

}
