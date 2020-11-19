using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguetteShot : Projectile
{
    /// <summary>
    /// Le tir de base de la libellule. Ne fait pas de dégâts mais créé une explosion au contact.
    /// </summary>
    public GameObject explosion;

    public override void Pattern()
    {
        //Il va en ligne droite
        transform.Translate((Vector3.up * speed * Time.deltaTime), Space.Self);
    }

    public override void Touch(Collider2D collision)
    {
        OnDeath();
    }

    public override void HitPlayer(Collider2D collision)
    {
        OnDeath();

    }

    public override void HitDam(Collider2D collision)
    {
        OnDeath();
    }

    public override void OnDeath()
    {
            GameObject temp = Instantiate(explosion, transform.position, transform.rotation);
            temp.GetComponent<Projectile>().Create(player, lifesteal);
            

        StartCoroutine(Dying());
    }
}
