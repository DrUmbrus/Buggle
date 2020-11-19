using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DMSmashBall : Damageable
{
    /// <summary>
    /// Variante de la smash ball, pour le mode deathmatch.
    /// Fonctionne de manière identique, mais elle donne l'ultime au lieu de faire tomber des trésors.
    /// </summary>
    
    public bool bouncy = false;
    public int speed;
    public Vector2 direction;
    public GameObject endPart;
    public int damageReceived = 0;
    public StatsManager sm;
    public ParticleSystem ps;

    public Gradient grad;

    public float colorTime = 0;

    public Light2D lumiere;

    public override void Awakening()
    {
        ps = GetComponent<ParticleSystem>();
        direction = Random.insideUnitCircle;
        direction.Normalize();
        Destroy(gameObject, 20);
        lumiere = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        if (!bouncy)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (bouncy)
        {

            if (rb.velocity.magnitude < 0.2f)
            {

                rb.velocity = Vector2.zero;
                bouncy = false;
                direction = Random.insideUnitCircle;
                direction.Normalize();
            }
        }

        if (colorTime < 1)
        {
            colorTime += Time.deltaTime / 3;
        }
        else
        {
            colorTime = 0;
        }
        lumiere.color = grad.Evaluate(colorTime);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!bouncy)
        {
            direction = Random.insideUnitCircle;
            direction.Normalize();
        }
    }


    public override void Dam(int source)
    {
        if (!dying)
        {

            bouncy = true;
            aud.Play();
        }
    }

    public override void Death(int source)
    {
        Destroy(Instantiate(endPart, transform.position, Quaternion.identity),2);
        sm.EndSmash();
        gm.team[source].killStreak += 3;
    }
}
