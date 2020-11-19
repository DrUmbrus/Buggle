using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetSol : MonoBehaviour
{
    public GameObject aspect;
    bool ramasse = false;
    public AudioSource sounding;
    public RandomSpawner rs=null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ramasse)
        {
            if (collision.gameObject.tag == "player")
            {
                GameObject temp = Instantiate(aspect, transform.position, Quaternion.identity);
                temp.GetComponent<Aspect>().SetStats(collision.GetComponent<Character>());
                ramasse = true;
                StartCoroutine(End());
                if (rs != null)
                {
                    rs.Grabbing();
                }
                
            }
        }

    }

    private void Awake()
    {
        sounding.minDistance = 8;
        sounding.maxDistance = 20;
    }

    IEnumerator End()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
        sounding.Play();
        while (sounding.isPlaying)
        {
            yield return 0;
        }
        Destroy(gameObject);
    }
}
