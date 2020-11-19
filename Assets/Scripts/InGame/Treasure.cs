using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    /// <summary>
    /// Les trésors du mode trésor.
    /// </summary>
    
    //La valeur (si jamais on veut faire plusieurs types de trésors un jour)
    public int value;
    //S'ils sont en train d'être ramassés.
    bool ramasse=false;
    //Le son
    public AudioSource sounding;
    //Les particules
    public GameObject paillettes;


    private void Awake()
    {
        transform.Translate(new Vector3(0, 0, -0.5f));

    }

    //On active une coroutine quand on les ramasse. Avec le bool pour s'assurer qu'une seule personne puisse récupérer.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ramasse)
        {
            if (collision.gameObject.tag == "player")
            {
                collision.GetComponent<Character>().AddLoot(value);
                ramasse = true;
                StartCoroutine(End());
            }
        }
        
    }

    //Petite coroutine pour avoir le temps d'entendre le son et mettre les particules. Pas très bon, mais ça marche.
    IEnumerator End()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
        Instantiate(paillettes, transform.position, Quaternion.identity);
        sounding.Play();
        while (sounding.isPlaying)
        {
            yield return 0;
        }
        Destroy(GetComponentInParent<Transform>().gameObject);
    }

}
