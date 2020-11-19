using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    /// <summary>
    /// Script général pour des objets de type tourelle. WIP.
    /// </summary>
    public List<GameObject> targets;
    public float maxTimer;
    public float timer;
    public float shootingTime;
    public Animator anim;
    public GameObject targetAcquired=null;
    public SpriteRenderer sr;
    public bool aiming = false;
    public float angle = 0;


    //Quand un objet qui n'est pas le tireur entre dans la zone de détection, l'objet devient une cible potentielle.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var potato = collision.gameObject;
        if(potato.tag=="player" || potato.tag == "damageable")
        {
            bool test = true;
            if (targets.Count > 0)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i] == potato)
                    {
                        test = false;
                    }
                    
                }                
            }
            if (test)
            {
                targets.Add(potato);
            }
            
        }
    }

    //En sortant de la zone de détection, on n'est plus une cible.
    public void OnTriggerExit2D(Collider2D collision)
    {
        var potato = collision.gameObject;
        int removed = -1;
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == potato)
            {
                removed = i;
            }

        }

        if (removed != -1)
        {
            targets.RemoveAt(removed);
        }
    }

    public void LateUpdate()
    {
        if (targets.Count <= 0)
        {
            timer = maxTimer;
        }
        //Si un temps x se passe avec au moins une cible, on prépare le tire.
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = maxTimer + shootingTime;
                anim.SetTrigger("shoot");
                StartCoroutine(WaitShot());
            }
        }

        //Si on n'a pas de cible, la tourelle prend pour cible l'ennemi le plus proche.
        if (!aiming)
        {
            targetAcquired = CurrentTarget();
        }
        //Si on a une cible, on la vise.
        if (targetAcquired != null)
        {
            Aiming();
        }
        
    }

    public void Aiming()
    {
        Vector2 targetVec = targetAcquired.transform.position - transform.position;
        angle = Mathf.Atan2(targetVec.x, targetVec.y) * Mathf.Rad2Deg;
    }

    public GameObject CurrentTarget()
    {
        if (targets.Count <= 0)
        {
            return null;
        }
        else
        {
            float dist = 100;
            var targetting = -1;

            for (int i = 0; i < targets.Count; i++)
            {
                if(Vector2.Distance(transform.position, targets[i].transform.position) < dist)
                {
                    dist = Vector2.Distance(transform.position, targets[i].transform.position);
                    targetting = i;
                }
            }

            if (targetting < 0)
            {
                return null;
            }
            else
            {
                return targets[targetting];
            }
        }
    }

    //Le tir (à définir selon la tourelle, comme pour les guns.
    public virtual void Shooting()
    {

    }

    //Prépare le tir.
    public IEnumerator WaitShot()
    {
        yield return new WaitForSeconds(shootingTime);
        Shooting();
    }
}
