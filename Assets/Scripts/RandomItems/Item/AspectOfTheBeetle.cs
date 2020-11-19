using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectOfTheBeetle : Aspect
{
    /// <summary>
    /// Aspect de protection
    /// </summary>

    public override void Stat2()
    {
        cha.reduc = true;   
        StartCoroutine(Aspect());
    }

    public override void Pattern()
    {
        //Si le joueur meurt, il disparait aussi.
        if (cha.life <= 0)
        {
            cha.reduc = false;
            Destroy(gameObject);
        }
        //Il disparait en 0.66s, et se désactive à la fin.
        if (dying)
        {
            timer -= Time.deltaTime * 1.5f;
            sr.color = Color.Lerp(cle, op, timer);

            if (timer <= 0)
            {
                cha.reduc = false;
                Destroy(gameObject);
            }

        }
    }

    
    //Son temps d'efficacité.
    IEnumerator Aspect()
    {

        yield return new WaitForSeconds(10);
        dying = true;
        
    }
}
