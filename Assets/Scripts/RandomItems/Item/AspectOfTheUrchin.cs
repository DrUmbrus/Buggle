using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectOfTheUrchin : Aspect
{
    /// <summary>
    /// La réincarnation du bomboursin
    /// </summary>
    
        //Qui est le dernier propriétaire.
    public int oldNum=-1;
    //Le collider
    CircleCollider2D col = null;
    //L'animator
    Animator anim = null;
    //Un prefab à invoquer pour le temps restant.
    public GameObject timerExplo = null;
    AudioSource aud = null;
    //Est-ce qu'on est en mode dégâts ou pas.
    bool explosif = false;
    [SerializeField]
    Sprite dedSprite = null;

    public override void Stat2()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        col = GetComponent<CircleCollider2D>();
        StartCoroutine(Timing());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            
            Character vic = collision.GetComponent<Character>();

            //Si on touche un joueur avec, il se colle sur le nouveau joueur, et note qui était son propriétaire.
            if (vic.playerNum != num)
            {
                if (!explosif)
                {
                    cha = vic;
                    oldNum = num;
                    num = cha.playerNum;
                    StartCoroutine(Backsies());
                }
                
                    
            }

            //Le dernier propriétaire est immunisé à l'explosion.
            if(vic.playerNum!= oldNum && explosif)
            {
                vic.Damage(30, oldNum, true, false);
            }
        }
    }

    //L'explosion marche aussi en stay, pour éviter une mauvaise intéraction.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {

            Character vic = collision.GetComponent<Character>();

            if (vic.playerNum != oldNum && explosif)
            {
                vic.Damage(30, oldNum, true, false);
            }
        }
    }

    public override void Pattern()
    {
        if (sr.sprite == dedSprite)
        {
            StartCoroutine(Dying());
        }
    }

    IEnumerator Timing()
    {
        //On attend et on met régulièrement une update sur le temps + changer l'animation.
        yield return new WaitForSeconds(1);
        anim.SetTrigger("1");
        GameObject temp = Instantiate(timerExplo, transform.position, Quaternion.identity);
        temp.GetComponentInChildren<Text>().text = "3";
        
        yield return new WaitForSeconds(1);
        anim.SetTrigger("2");
        temp = Instantiate(timerExplo, transform.position, Quaternion.identity);
        temp.GetComponentInChildren<Text>().text = "2";
        yield return new WaitForSeconds(1);
        anim.SetTrigger("3");
        temp = Instantiate(timerExplo, transform.position, Quaternion.identity);
        temp.GetComponentInChildren<Text>().text = "1";
        yield return new WaitForSeconds(1);
        anim.SetTrigger("4");
        //A la fin du timer, on augmente la taille du collider et on passe en mode explosion. La stickiness est retirée.
        explosif = true;
        col.radius *= 2;
        sticky = false;
        aud.Play();
    }

    //Finir de jouer le son avant de disparaitre.
    IEnumerator Dying()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
        while (aud.isPlaying)
        {
            yield return 0;
        }
        Destroy(gameObject);
    }

    //On peut pas redonner l'oursin pendant 0.75s après l'avoir reçu.
    IEnumerator Backsies()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.75f);
        col.enabled = true;
    }
}
