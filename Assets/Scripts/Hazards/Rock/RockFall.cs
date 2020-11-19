using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour
{
    /// <summary>
    /// Les cailloux qui tombent sur la map earthquake.
    /// </summary>
    
    CircleCollider2D box;
    public int damage;
    public Sprite activation, destruction;
    SpriteRenderer sr;
    public int ignore = -1;
    public float timeFall=1.5f;


    private void Awake()
    {
        box = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitFall());
        box.enabled = false;

        GameManager gm = FindObjectOfType<GameManager>();
        //La map est utilisée en mode goldengun, mais les rocks sont désactivés.
        if (gm.mode == 3)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //Si on atteint le sprite d'activation, la hitbox d'active. Et sur le sprite destruction, on détruit l'objet.
        if (sr.sprite == activation)
        {
            box.enabled = true;
        }
        else if (sr.sprite == destruction)
        {
            Destroy(gameObject);
        }
    }

    //Quand un rocher entre en contact avec quelque chose qui prend des dégâts, on inflige les dégâts.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if(collision.GetComponent<Character>().playerNum!=ignore)
            collision.GetComponent<Character>().Damage(damage, ignore, false, false);
        }
        else if (collision.gameObject.tag == "damageable")
        {
            if (collision.GetComponent<Damageable>().id != ignore)
            {
                collision.GetComponent<Damageable>().Damage(damage, ignore, Vector2.zero, false, false);
            }
        }
    }

    //Le temps où on a l'ombre, avant la chute.
    IEnumerator WaitFall()
    {
        yield return new WaitForSeconds(timeFall);
        GetComponent<Animator>().SetTrigger("fall");
    }
}
