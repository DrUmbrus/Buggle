using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaqueCac : Projectile
{
    /// <summary>
    /// Inflige des dégâts à courte portée. Une zone de dégâts qui suit le joueur, depuis son point de création et en suivant l'angle de tir.
    /// </summary>
    bool sounded = false;

    public override void Starting()
    {
        StartCoroutine(SetParent());
    }

    IEnumerator SetParent()
    {
        while (character == null)
        {
            yield return 0;
        }
        transform.parent = character.transform;
    }

    public override void HitPlayer(Collider2D collision)
    {
        Vector2 projection = collision.transform.position - transform.position;
        projection.Normalize();
        collision.GetComponent<Rigidbody2D>().AddForce(projection * force);

        if (!sounded)
        {
            sounded = true;
            noise.Play();
        }
    }
}
