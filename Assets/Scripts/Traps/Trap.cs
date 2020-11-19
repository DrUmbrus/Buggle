using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    /// <summary>
    /// Ssystème pour des objets de type piège. WIP
    /// </summary>
    public GameObject part2;
    public bool dieOnTime=false;  
    public float time=0;
    public float maxTime=0;
    public float lifeTime = 0;
    public float uses = 0;

    public bool permanent = false;

    public bool activated = false;

    SpriteRenderer sr;

    public int creator=-1;
    bool lifesteal = false;


    //On récupère les infos sur le tireur.
    public void SetTrap(int cre, bool life)
    {
        creator = cre;
        lifesteal = life;
    }
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Si quelqu'un qui n'est pas le tireur rentre dans la zone de collision, le piège s'active.
        if (!activated)
        {
            bool test = false;
            if(collision.tag == "player")
            {
                if (collision.GetComponent<Character>().playerNum != creator)
                {
                    test = true;
                }
            }
            else if(collision.tag == "damageable")
            {
                if (collision.GetComponent<Damageable>().id != creator)
                {
                    test = true;
                }
            }

            //Le piège invoque l'objet "actif", qui permet d'infliger les dégâts ou l'effet du piège.
            if(test)
            {
                GameObject temp = Instantiate(part2, transform.position, Quaternion.identity);
                temp.GetComponent<Projectile>().Create(creator, lifesteal);
                time = maxTime;
                activated = true;

                //Permanent = piège d'environnement. Sinon on détruit le piège quand il a atteint son nombre de triggers (généralement un seul, mais mieux vaut prévoir)
                if (!permanent)
                {
                    uses--;
                    if (uses <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            
        }
    }

    public void Update()
    {

        //Si c'est un piège temporaire, il est détruit à la fin de son temps d'existence.
        if (dieOnTime)
        {
            lifeTime -= Time.deltaTime;
        }

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        //S'il a été activé et est réutilisable, il le sera à la fin de ce temps.
        if(activated)
        {
            time -= Time.deltaTime;
        }

        if(time<=0)
        {
            activated = false;
        }
    }

}
