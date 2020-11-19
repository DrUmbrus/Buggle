using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coutal : Gun
{
    /// <summary>
    /// Le couteau de la sauterelle
    /// </summary>
    /// 

    public int numberShot = 3;
    public float angleBetweenShots = 5;
    public float back = 0;


    //On créé simplement deux tirs supplémentaires, avec 5° d'écart par rapport au tir principal.
    public override void ShootingPattern()
    {


        angle += Random.Range(-maxAngle, maxAngle + 1);
        
        if (throwable)
        {
            sr.color = Color.clear;
        }
        float potato = -1;

        if (numberShot % 2 == 0)
        {
            potato = numberShot / 2-0.5f;
        }
        else
        {
            potato = (numberShot - 1) / 2;
        }

        potato *= angleBetweenShots;
        

        for (int i = 0; i < numberShot; i++)
        {
            //On créé l'objet
            GameObject temp = Instantiate(projec, muzzle.position, Quaternion.identity);



            //On adapte ensuite le tir pour qu'il soit dans l'angle visé
            temp.transform.rotation = Quaternion.Euler(0, 0, angle);

            //On rajoute un -90 parce que le truc marche pas sinon. Et on y ajoute un random qui permet de donner de la dispersion.
            temp.transform.Rotate(new Vector3(0, 0, -90-potato+(angleBetweenShots*i)));
            temp.GetComponent<Projectile>().Create(owner, ownerChar.lifesteal);
            temp.GetComponent<Projectile>().character = ownerChar;
        }

        Vector2 knockBack = ownerChar.transform.position - transform.position;
        knockBack.Normalize();
        ownerChar.GetComponent<Rigidbody2D>().AddForce(knockBack * back * 350);
    }
}
