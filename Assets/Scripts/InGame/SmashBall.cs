using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SmashBall : Damageable
{
    /// <summary>
    /// Un objet à détruire pour gagner plein de trésors.
    /// </summary>
    /// 
    public bool bouncy = false;
    public int speed;
    public Vector2 direction;
    public GameObject treasure, bigTreasure, endPart;
    public int damageReceived = 0;
    public int threshold, thresholdValue;
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
        threshold = life - thresholdValue + Random.Range(-5, 6);
    }

    private void Update()
    {
        //Si personne ne l'a touchée, elle ne bouge pas.
        if (!bouncy)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (bouncy)
        {
            //Quand on l'a touchée, tant qu'elle n'a pas trop ralentit, elle bouge. Si elle devient trop lente, elle s'arrête et reprend son déplacement random.
            if (rb.velocity.magnitude < 0.2f)
            {
                
                rb.velocity = Vector2.zero;
                bouncy = false;
                direction = Random.insideUnitCircle;
                direction.Normalize();
            }
        }
        //Une petite fonction de temps pour changer de couleur.
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

    //Si elle se cogne contre un mur, elle change de direction de manière aléatoire.    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!bouncy)
        {
            direction = Random.insideUnitCircle;
            direction.Normalize();
        }
    }

    //Quand la balle prend des dégâts
    public override void Dam(int source)
    {
        //Si elle n'est pas en dessous de 0hp
        if (!dying)
        {
            //Tous les x hp, elle fait tomber des trésors.
            if (life<=threshold)
            {
                threshold = life - thresholdValue + Random.Range(-5, 6);
                Vector2 pos = new Vector2(transform.position.x, transform.position.y);
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(treasure, pos + Random.insideUnitCircle * 2, Quaternion.identity);
                    treasure.transform.Translate(new Vector3(0, 0, -0.15f));
                }

            }

            //Le son de dégâts et le fait qu'elle est projetée.
            bouncy = true;
            aud.Play();
        }
        //Si elle est en train d'être détruite
        else
        {
            //On créé les trésors de récompense, des paillettes, on prévient le stats manager, puis on détruit la balle.
            for (int i = 0; i < 3; i++)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y);
                GameObject temp = Instantiate(bigTreasure, pos + Random.insideUnitCircle * 2, Quaternion.identity);
                temp.GetComponent<BigTreasure>().SetTarget(source);
                bigTreasure.transform.Translate(new Vector3(0, 0, -0.15f));

            }
            Destroy(Instantiate(endPart, transform.position, Quaternion.identity), 2);
            sm.EndSmash();
        }
    }
}

