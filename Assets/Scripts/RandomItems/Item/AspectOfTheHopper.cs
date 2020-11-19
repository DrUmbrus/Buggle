using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectOfTheHopper : Aspect
{
    /// <summary>
    /// Aspect de la sauterelle. Court plus vite et fait des dégâts en zone.
    /// </summary>
    
        //Le collider
    CircleCollider2D cc;
    //Les dégâts
    public int damage;
    //Et une variable pour la vitesse
    public float speed;

    public override void Stat2()
    {
        //On active la collision
        cc = GetComponent<CircleCollider2D>();
        cc.enabled=true;
        
        //La vitesse est 50% plus élevée que celle de départ.
        speed = cha.baseSpeed*1.5f;

        StartCoroutine(Aspect());
    }

    public override void Pattern()
    {
        if (cha.life <= 0)
        {
            Destroy(gameObject);
        }
        if (dying)
        {
            timer -= Time.deltaTime * 1.5f;
            sr.color = Color.Lerp(cle, op, timer);

            if (timer <= 0)
            {
                Destroy(gameObject);
            }

        }

        //On accélère le joueur s'il va moins vite que la vitesse calculée. (notamment important pour le dash de la sauterelle)
        if (cha.speed < speed)
        {
            cha.speed = speed;
        }
    }

    //On fait des dégâts en touchant un joueur, sauf le propriétaire, ofc.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player" && collision.gameObject!=cha.gameObject)
        {
            collision.GetComponent<Character>().Damage(damage, num, false, false);
        }
    }

    IEnumerator Aspect()
    {

        yield return new WaitForSeconds(12);
        cc.enabled = false;
        dying = true;
    }

    //On oublie pas de remettre la bonne vitesse au joueur.
    private void OnDestroy()
    {
        cha.speed = cha.baseSpeed;
    }
}
