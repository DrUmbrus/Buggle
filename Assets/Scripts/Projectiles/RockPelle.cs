using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPelle : Projectile
{
    /// <summary>
    /// Un tir simple, avec un sprite aléatoire. Potentiellement plus pertinent à refaire comme tir normal en changeant le sprite depuis la pelle.
    /// </summary>
    [SerializeField]
    Sprite[] sprites= new Sprite[0];

    public override void Starting()
    {
        int rand = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[rand];
    }

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
