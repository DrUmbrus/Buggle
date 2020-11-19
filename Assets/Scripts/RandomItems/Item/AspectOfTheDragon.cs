using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectOfTheDragon : Aspect
{
    //L'arme du joueur
    public Gun gun;
    //L'impact sur la précision
    public int angle;
    bool started = false;


    public override void Stat2()
    {
        //On régupère l'arme du joueur, on double son tir, et on augmente l'angle de visée.
        gun = cha.gun;
        cha.gun.multiPattern *= 2;
        cha.gun.maxAngle += angle;
        cha.gun.multishot *= 2;
        StartCoroutine(Aspect());
        started = true;
        cha.dragons.Add(gameObject);
    }

    public override void Pattern()
    {
        if (started)
        {
            if (cha.life <= 0)
            {
                Destroy(gameObject);
            }

            //Si le joueur change d'arme, on met aussi le bonus sur la nouvelle. Sinon ça causait des bugs.
            if (cha.gun != gun)
            {
                gun = cha.gun;
                cha.gun.multiPattern *= 2;
                cha.gun.maxAngle += angle;
                cha.gun.multishot *= 2;
            }

            if (dying)
            {
                timer -= Time.deltaTime * 1.5f;
                sr.color = Color.Lerp(cle, op, timer);

                //On remet les stats aux valeurs de base.
                if (timer <= 0)
                {
                    cha.gun.multiPattern /= 2;
                    cha.gun.maxAngle -= angle;
                    cha.gun.multishot /= 2;
                    cha.dragons.Remove(gameObject);
                    Destroy(gameObject);
                }

            }

        }
        
    }


    IEnumerator Aspect()
    {

        yield return new WaitForSeconds(6);
        dying = true;
        
    }
}
