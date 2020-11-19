using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruitfall : MonoBehaviour
{
    /// <summary>
    /// Les fruits qui tombent sur la map potager. WIP
    /// </summary>
    
    //Le power up contenu dedans.
    public GameObject powerup;
    CircleCollider2D box;
    public int damage;
    //Le sprite sur lequel la hitbox devient active et sur lequel on détruit l'objet.
    public Sprite activation, destruction;
    SpriteRenderer sr;
    //Si un joueur doit être ignoré par le fruit.
    public int ignore = -1;
    public float timeFall = 1.5f;
    //Si on a touché quelque chose
    bool hit = false;


    //La hitbox est désactivée par défaut
    private void Awake()
    {
        box = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitFall());
        box.enabled = false;
    }


    private void Update()
    {
        //Si on atteint le sprite d'activation, la hitbox devient active. (pour qu'on ait l'impression d'être touché que quand l'objet est assez bas).
        if (sr.sprite == activation)
        {
            box.enabled = true;
        }
        //Sur le sprite de destruction, on détruit l'objet. S'il n'a pas touché de joueur, le powerup apparait aussi.
        else if (sr.sprite == destruction)
        {
            if (!hit)
            {
                Instantiate(powerup, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    //Si on touche un joueur ou un damageable, on fait des dégâts et on aura pas le powerup.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if (collision.GetComponent<Character>().playerNum != ignore)
            {
                hit = true;
                collision.GetComponent<Character>().Damage(damage, ignore, false, false);
            }
                
        }
        else if (collision.gameObject.tag == "damageable")
        {
            if (collision.GetComponent<Damageable>().id != ignore)
            {
                hit = true;
                collision.GetComponent<Damageable>().Damage(damage, ignore, Vector2.zero, false, false);
            }
        }
    }

    //Le temps entre l'ombre et le début de la chute.  
    IEnumerator WaitFall()
    {
        yield return new WaitForSeconds(timeFall);
        GetComponent<Animator>().SetTrigger("fall");
    }

        
}
