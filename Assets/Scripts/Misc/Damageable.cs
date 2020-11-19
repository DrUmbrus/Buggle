using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    /// <summary>
    /// Ancestor de tous les objets pouvant prendre des dégâts
    /// </summary>
    /// 

    public int life = 0;
    
    public AudioSource aud;
    public AudioClip ded;
    public bool dying=false;
    public bool fixe=false;
    public Rigidbody2D rb;
    public bool speDead = false;
    public GameManager gm = null;
    public int id = -1;

    public void Awake()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = 0.8f;
        aud.spatialBlend = 1;
        aud.minDistance = 8;
        aud.maxDistance = 20;
        aud.dopplerLevel = 0;
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        Awakening();
    }

    //Fonction pour les aspects uniques à faire dans le awake.
    public virtual void Awakening()
    {

    }

    //Quand l'objet reçoit des dégâts. On regarde la quantité, la provenance, la puissance d'éjection, si les dégâts traversent une protection, et si le tireur est soigné.
    public void Damage(int dam, int source, Vector2 force, bool go, bool lifeSteal)
    {
        //S'il y a du lifesteal, le tireur reçoit la moitié des dégâts en hp.
        if (lifeSteal)
        {
            gm.team[source].HealMe(Mathf.CeilToInt(dam / 2));
        }

        //Si l'objet n'est pas fixé au sol, il est projeté.
        if (!fixe)
        {
            Vector2 targetSens = force;
            targetSens.Normalize();
            rb.velocity = targetSens * rb.velocity.magnitude;
            rb.velocity += force;
        }

        //On retire de la vie à l'objet.
        life -= dam;

        //Si on tombe en dessous de 0, on  on devient intouchable, en activant les effets à la mort.
        if (life <= 0 && !dying)
        {
            dying = true;
            Death(source);
            StartCoroutine(Dying());
        }
        //Si on est toujours en vie au moment de prendre des dégâts, les effets à l'impact s'appliquent.
        if (!dying)
        {
            Dam(source);
        }   
    }

    //Effets à l'impact.
    public virtual void Dam(int source)
    {

    }

    //Effets à la mort. (exemple du cas des balles smash)
    public virtual void Death(int source)
    {

    }

    //La mort concrète, l'objet devient invisible et intouchable, on joue l'audio de mort, puis on détruit l'objet quand le son est fini.
    public IEnumerator Dying()
    {
       
        GetComponent<SpriteRenderer>().color = Color.clear;
        GetComponent<Collider2D>().enabled = false;
        aud.clip = ded;
        aud.Play();
        while (aud.isPlaying)
        {
            yield return 0;
        }
        yield return 0;
        if (!speDead)
        {
            Destroy(gameObject);
        }
        
    }

}
