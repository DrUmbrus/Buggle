using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamaitachi : Projectile
{
    /// <summary>
    /// Un tir en ligne droite, qui traverse les murs, et qui accélère
    /// </summary>

    public float startSpeed;
    public float timer=0;



    public override void Pattern()
    {

            //Il va en ligne droite
            transform.Translate((Vector3.up * speed * Time.deltaTime), Space.Self);
        
        timer += Time.deltaTime*2;
        speed = startSpeed + Mathf.Pow(timer, 4);

    }



}
