using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derringer : Gun
{
    /// <summary>
    /// Le dezingeur de la mite. Tire plus vite que son animation, donc on fonctionne différemment.
    /// </summary>
    /// 


    public bool shooty = false;
    GameManager gm;

    public override void Pattern()
    {
        if (gm == null)
        {
            gm = FindObjectOfType<GameManager>();
        }

        //si on appuie sur la gachette sans être stun, ça lance le tir.
        if(ownerChar.shooting && !shooty && !ownerChar.stopped)
        {
            StartCoroutine(Shooty());
        }
        //Si on lache la gachette, ou qu'on se fait stop (spell, ulti, ou mort), ou qu'on meurt, le tir s'arrête
        else if(!ownerChar.shooting || ownerChar.stopped || ownerChar.life<=0)
        {
            shooty = false;
            StopAllCoroutines();
        }
    }

    //La coroutine de tir.
    IEnumerator Shooty()
    {
        shooty = true;
        //On tourne en boucle
        for (int i = 0; i < 999; i++)
        {
            //Et ça fait des tirs
            for (int j = 0; j < multiPattern; j++)
            {
                
                GameObject temp = Instantiate(projec, muzzle.position, Quaternion.identity);

                //On réutilise le même système que pour le laser
                Vector2 targeting = ownerChar.aim;
                mousePos = Camera.main.WorldToScreenPoint(socket.transform.position) + new Vector3(targeting.x, targeting.y, 0) * 1000;

                socketPos = Camera.main.WorldToScreenPoint(socket.transform.position);
                mousePos.x = mousePos.x - socketPos.x;
                mousePos.y = mousePos.y - socketPos.y;
                angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

                //On adapte ensuite le tir pour qu'il soit dans l'angle visé
                temp.transform.rotation = Quaternion.Euler(0, 0, angle);

                //On rajoute un -90 parce que le truc marche pas sinon. Et on y ajoute un random qui permet de donner de la dispersion.
                temp.transform.Rotate(new Vector3(0, 0, -90 + Random.Range(-maxAngle, maxAngle + 1)));
                //Enfin, on dit à la balle qui est son créateur.
                temp.GetComponent<Projectile>().Create(owner, ownerChar.lifesteal);
                temp.GetComponent<Projectile>().character = ownerChar;

                
            }
            //Avec un cooldown
            yield return new WaitForSeconds(timebetweenshot);

        }
    }
}
