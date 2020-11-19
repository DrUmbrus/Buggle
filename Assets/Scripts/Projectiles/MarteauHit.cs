using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarteauHit : Projectile
{
    /// <summary>
    /// Une zone de dégâts en cercle, qui grandit en partant du point de tir.
    /// </summary>
    CircleCollider2D cc;
    public float maxSize;
    float timer = 0;


    public override void Starting()
    {
        cc = GetComponent<CircleCollider2D>();

            GetComponent<AudioSource>().volume = 0.5f;

    }

    public override void Pattern()
    {
        timer += Time.deltaTime * speed;
        cc.radius = Mathf.Lerp(0, maxSize, timer);

        if (timer >= 1)
        {
            cc.enabled = false;
            StartCoroutine(Dying());
        }
    }

}
