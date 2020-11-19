using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentExplo : Projectile
{
    /// <summary>
    /// Les cristaux du trident
    /// </summary>
    
    PolygonCollider2D box;
    SpriteRenderer sr;
    public Sprite wall, ded;
    bool end = false;

    public AudioClip explo;

    public override void Starting()
    {
        box = GetComponent<PolygonCollider2D>();
        box.enabled = false;
        sr = GetComponent<SpriteRenderer>();
    }

    public override void Pattern()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        //Une fois l'animation assez avancée (les cristaux assez gros), la  hitbox s'active, et ils deviennent des murs qui font des dégâts.
        if (sr.sprite==wall && !end)
        {
            box.enabled = true;
            box.isTrigger = false;
            end = true;
            StartCoroutine(Ending());
        }
        //Arrivé à ce sprite, on détruit l'objet.
        if (sr.sprite == ded)
        {
            Destroy(gameObject);
        }
    }


    IEnumerator Ending()
    {
        //On attend 2.5s avant de déclencher l'animation de mort, avec le son de destruction, et en coupant la hitbox.
        yield return new WaitForSeconds(2.5f);
        GetComponent<Animator>().SetTrigger("death");
        GetComponent<AudioSource>().clip = explo;
        GetComponent<AudioSource>().volume /= 3;
        GetComponent<AudioSource>().Play();
        box.enabled = false;
    }

    //Fait des dégâts au contact.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Character temp = collision.gameObject.GetComponent<Character>();
            if (temp.playerNum != player)
            {

                temp.Damage(damage, player, false, lifesteal);

            }

        }
        else if (collision.gameObject.tag == "smash")
        {
            Vector2 send = collision.transform.position - transform.position;
            send.Normalize();
            send *= force;
                collision.gameObject.GetComponent<SmashBall>().Damage(damage, player, send, false, lifesteal);

        }
        
    }
}
