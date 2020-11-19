using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Ancienne compétence de la mite. Actuellement inutile.
public class MothBurst : MonoBehaviour
{
    /*
    /// <summary>
    /// Le spell de la moth
    /// </summary>
    
    //
    public int damage;
    public GameObject creatorOb;
    public Moth creator;
    CircleCollider2D col;

    //Le collider est désactivé au début pour des raisons techniques
    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }

    //On lui dit qui a créé le spell, sa durée de vie, et on réactive le collider.
    public void SetStats(GameObject obj, Moth moth)
    {
        creator = moth;
        creatorOb = obj;
        Destroy(gameObject, 0.75f);
        col.enabled = true;
    }

    //Si on touche un joueur qui n'est pas soi même, ça fait des dégâts
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="player" && collision.gameObject != creatorOb)
        {
            collision.GetComponent<Character>().Damage(damage, creator.playerNum);
        }
    }

    //Quand il se détruit, on met fin au spell.
    private void OnDestroy()
    {
        creator.EndComp();
    }
    */
}
