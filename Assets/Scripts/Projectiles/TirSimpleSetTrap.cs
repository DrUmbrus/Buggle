using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirSimpleSetTrap : Projectile
{
    [SerializeField]
    GameObject endSpawn = null;

    //Tir simple qui peut invoquer un piège en mourrant

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
        GameObject temp =Instantiate(endSpawn, transform.position, Quaternion.identity);
        temp.GetComponent<Trap>().SetTrap(player, lifesteal);

        StartCoroutine(Dying());
    }
}
