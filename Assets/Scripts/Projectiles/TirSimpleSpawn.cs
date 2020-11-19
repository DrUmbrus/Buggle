using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirSimpleSpawn : Projectile
{
    [SerializeField]
    GameObject endSpawn=null;

    public float angle = 0;
    public float numberEnd = 0;
    public bool fan = false;

    //Tir simple qui peut invoquer un truc en mourrant

    public override void Pattern()
    {
        //Il va en ligne droite
        transform.Translate((Vector3.up * speed * Time.deltaTime), Space.Self);
    }

    public override void Touch(Collider2D collision)
    {
        OnDeath();


    }

    public override void HitDam(Collider2D collision)
    {
        OnDeath();

    }

    public override void HitPlayer(Collider2D collision)
    {
        OnDeath();

    }

    

    public override void OnDeath()
    {
        float rot = transform.rotation.z;
        if (!fan)
        {
            for (int i = 0; i < numberEnd; i++)
            {
                GameObject temp = Instantiate(endSpawn, transform.position, Quaternion.identity);
                temp.GetComponent<Projectile>().Create(player, lifesteal);
                temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i * angle + rot));
            }
        }
        else
        {
            float potato = -1;

            if (numberEnd % 2 == 0)
            {
                potato = numberEnd / 2 - 0.5f;
            }
            else
            {
                potato = (numberEnd - 1) / 2;
            }

            potato *= angle;


            for (int i = 0; i < numberEnd; i++)
            {
                //On créé l'objet
                GameObject temp = Instantiate(endSpawn, transform.position, Quaternion.identity);


                temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -potato+i * angle + rot));

                /*
                //On adapte ensuite le tir pour qu'il soit dans l'angle visé
                temp.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);

                //On rajoute un -90 parce que le truc marche pas sinon. Et on y ajoute un random qui permet de donner de la dispersion.
                temp.transform.Rotate(new Vector3(0, 0, -90 - potato + (angle * i)));*/
                temp.GetComponent<Projectile>().Create(player, lifesteal);

            }
        }

        StartCoroutine(Dying());
    }

}
