using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspiration : MonoBehaviour
{
    /// <summary>
    /// Attirer les trésors en mode bugHunt
    /// </summary>

    //Un effet de particules à rajouter un jour.
    [SerializeField]
    GameObject visual;
    //Le perso associé
    Character owner;
    //Les trésors à portée.
    List<Transform> treasures= new List<Transform>();
    //La vitesse d'aspiration.
    int speed=2;

    CircleCollider2D col;

    private void Awake()
    {
        owner = GetComponentInParent<Character>();
        col = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
       
        //Si le propriétaire est en bugHunt, tous les trésors dans sa portée sont translatés vers le joueur.
            if (owner != null)
        {
            if (owner.hunted)
            {
                col.enabled = true;
                foreach(Transform treasure in treasures)
                {
                    Vector3 movement = (transform.position - treasure.position);
                    movement.Normalize();
                    treasure.Translate( movement* Time.deltaTime * speed);
                }
            }
            else
            {
                col.enabled = false;
            }
        }

        
    }

    //On récupère les trésors dans la portée en se basant sur le layer.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            treasures.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            treasures.Remove(collision.transform);
        }
    }
}
