using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMoth : Projectile
{
    /// <summary>
    /// Similaire au kamaitachi.
    /// </summary>

    public Vector2 moving=Vector2.zero;
    public float startSpeed=0;
    public float timer = 0;

    [SerializeField]
    GameObject particleWind = null;

    [SerializeField]
    Vector3 maxSize=Vector3.zero;


    public override void Pattern()
    {

        //Il va en ligne droite
        transform.Translate(moving*speed*Time.deltaTime);

        timer += Time.deltaTime * 2;
        speed = startSpeed + Mathf.Pow(timer, 4);

        transform.localScale = Vector3.Lerp(Vector3.zero, maxSize, timer);

    }



    public override void HitPlayer(Collider2D collision)
    {

        Instantiate(particleWind, collision.transform.position, Quaternion.identity);
    }

    public override void Touch(Collider2D collision)
    {
        //Il meurt contre les murs
        //StartCoroutine(Dying());

    }

}
