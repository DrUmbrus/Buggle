using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Classe générale pour les projectiles
    public float trail = 1;
    public float force = 0;
        //Temps avant destruction (mettre 1000 si le tir s'arrête seulement sur un mur
    public float lifetime;
    //Vitesse de déplacement
    public float speed;
    //Nombre de dégâts sur le joueur
    public int damage;
    //Qui l'a créé
    public int player;
    public Character character;
    public AudioSource noise;

    public bool ignore = false;

    public bool staticShot=false;
    public bool souffle = false;
    public bool lifesteal = false;
    public bool goThroughDefense = false;
    public List<GameObject> victimes = new List<GameObject>();

    //Au spawn, on récupère l'id du joueur qui l'a créé, et on met un timer pour mourir.
    public void Create(int j, bool life)
    {
        lifesteal = life;
        player = j;
        StartCoroutine(WaitForDead());

        if (player != -1)
        {
            var persos = FindObjectsOfType<Character>();
            for (int i = 0; i < persos.Length; i++)
            {
                if (persos[i].playerNum == player)
                {
                    character = persos[i];
                }
            }

            if (character.hunted)
            {
                damage = Mathf.FloorToInt(damage * 1.5f);
            }
            if (!souffle)
            {
                Vector2 knockBack = character.transform.position - transform.position;
                knockBack.Normalize();
                character.GetComponent<Rigidbody2D>().AddForce(knockBack * force * 350);
            }
        }
        
        
    }

    //Lance la fonction Starting au lancement
    public void Awake()
    {
        noise = GetComponent<AudioSource>();
        if (GetComponent<TrailRenderer>() != null)
        {
            GetComponent<TrailRenderer>().widthMultiplier = trail;
        }
        var potato = GetComponents<AudioSource>();
        for (int i = 0; i < potato.Length; i++)
        {
            potato[i].spatialBlend = 1;
            potato[i].minDistance = 8;
            potato[i].maxDistance = 20;
            potato[i].volume = 0.8f;
            potato[i].dopplerLevel = 0;
        }
        Starting();
    }

    //Foncton virtuelle pour tout ce qui se passe au lancement
    public virtual void Starting()
    {

    }

    //Lancer le Pattern
    public void FixedUpdate()
    {
        Pattern();
    }

    //Fonction virtuelle pour l'action à chauqe frame.
    public virtual void Pattern()
    {

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Collided(collision);

        //Si le projectile entre en contact avec un joueur qui n'est pas son créateur, on lance HitPlayer
        if (collision.gameObject.tag == "player")
        {
            Character temp = collision.gameObject.GetComponent<Character>();
            if (temp.playerNum != player)
            {
                bool test = true;
                if (victimes.Count > 0)
                {
                    for (int i = 0; i < victimes.Count; i++)
                    {
                        if (collision.gameObject == victimes[i])
                        {
                            test = false;
                        }
                    }
                }
                if (test)
                {
                    if (staticShot)
                    {
                        if (temp.GetComponent<Rigidbody2D>().velocity.magnitude < 0.5f)
                        {
                            Vector2 knockBack = temp.transform.position - transform.position;
                            knockBack.Normalize();
                            temp.GetComponent<Rigidbody2D>().AddForce(knockBack * force * 750);
                        }

                    }
                    else
                    {
                        if (temp.GetComponent<Rigidbody2D>().velocity.magnitude < 0.5f)
                        {
                            Vector2 knockBack = Quaternion.AngleAxis(transform.rotation.z, Vector3.forward) * Vector3.right;
                            knockBack.Normalize();
                            temp.GetComponent<Rigidbody2D>().AddForce(knockBack * force * 750);
                        }
                    }

                    collision.GetComponent<Character>().Damage(damage, player, goThroughDefense, lifesteal);

                    victimes.Add(collision.gameObject);
                    {
                        HitPlayer(collision);
                    }

                   
                }
                

                
            }
            
        }
        //Les damageable fonctionnent comme des joueurs
        else if (collision.gameObject.tag == "damageable")
        {
            if (collision.gameObject.GetComponent<Damageable>().id != player)
            {
                bool test = true;
                if (victimes.Count > 0)
                {
                    for (int i = 0; i < victimes.Count; i++)
                    {
                        if (collision.gameObject == victimes[i])
                        {
                            test = false;
                        }
                    }
                }
                if (test)
                {
                    Vector2 send = collision.transform.position - transform.position;
                    send.Normalize();
                    send *= force;
                    collision.GetComponent<Damageable>().Damage(damage, player, send, goThroughDefense, lifesteal);

                    victimes.Add(collision.gameObject);
                    HitDam(collision);

                        
                    
                }
                    
            }
           
        }
        else
        {
            if (collision.gameObject.tag == "solid")
            {
                Touch(collision);
            }
        }

    }

    public virtual void Collided(Collider2D collision)
    {

    }

    
   
    //Fonction virtuelle en touchant un mur
    public virtual void Touch(Collider2D collision)
    {

    }

    //Fonction virtuelle en touchant un joueur
    public virtual void HitPlayer(Collider2D collision)
    {

    }
    public virtual void HitDam(Collider2D collision)
    {

    }

    IEnumerator WaitForDead()
    {
        yield return new WaitForSeconds(lifetime);
        OnDeath();
    }

    public virtual void OnDeath()
    {
        StartCoroutine(Dying());
    }

    //A la mort, on désactive le collider et le visuel puis on fait disparaitre le projectile quand son son est terminé.
    public IEnumerator Dying()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (GetComponent<CircleCollider2D>() != null)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else if (GetComponent<CapsuleCollider2D>() != null)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else if (GetComponent<PolygonCollider2D>() != null)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
        }


        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = Color.clear;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.clear;
        }

        if (GetComponent<TrailRenderer>() != null)
        {
            GetComponent<TrailRenderer>().enabled=false;
        }

        if (noise != null)
        {
            while (noise.isPlaying)
            {
                yield return 0;
            }
        }
        

        Destroy(gameObject);
    }
}
