using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : Projectile
{
    /// <summary>
    /// Un projectile qui va en ligne droite et qui gagne en puissance en traversant les murs.
    /// </summary>
    public Sprite activeSprite;

    public override void Pattern()
    {
        //Il va en ligne droite
        transform.Translate((Vector3.up * speed * Time.deltaTime), Space.Self);
    }

    public override void Touch(Collider2D collision)
    {
        damage += 5;
        GetComponent<SpriteRenderer>().sprite = activeSprite;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public override void HitPlayer(Collider2D collision)
    {
        StartCoroutine(Dying());
    }
}
