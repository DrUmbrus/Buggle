using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirSimple : Projectile
{
    

    //Le tir le plus basique

    public override void Pattern()
    {
        //Il va en ligne droite
        transform.Translate((Vector3.up * speed * Time.deltaTime), Space.Self);
    }

    public override void Touch(Collider2D collision)
    {
        //Il meurt contre les murs
        StartCoroutine(Dying());

    }

    public override void HitPlayer(Collider2D collision)
    {
        StartCoroutine(Dying());
    }

}
