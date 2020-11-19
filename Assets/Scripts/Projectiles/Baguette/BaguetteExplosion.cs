using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguetteExplosion : Projectile
{
    /// <summary>
    /// L'explosion de la baguette de la libellule.
    /// Inflige des dégâts au contact, ne bouge pas, et fade out.
    /// </summary>
    SpriteRenderer sr;
    float timer=0;
    CircleCollider2D col;
    bool dying = false;

    public override void Starting()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        StartCoroutine(WaitCol());
    }

    public override void Pattern()
    {
        timer += Time.deltaTime*1.25f;
        if (timer > 0.8f)
        {
            col.enabled = false;
        }
        if (timer >= 1 && !dying)
        {
            StartCoroutine(WaitDeath());
        }
    }





    IEnumerator WaitCol()
    {
        yield return 0;
        col.enabled = true;
    }

    IEnumerator WaitDeath()
    {
        dying=true;
        sr.color = Color.clear;
        while (noise.isPlaying)
        {
            yield return 0;
        }
        Destroy(gameObject);
    }
}
