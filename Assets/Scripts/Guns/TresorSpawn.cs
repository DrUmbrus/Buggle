using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresorSpawn : MonoBehaviour
{
    /// <summary>
    /// Les spawners de trésors
    /// </summary>
    
        //Le trésor
    public GameObject treasure;

    //Les valeurs pour le temps de spawn
    float timer;
    public float baseTime;
    public Sprite spawnSprite;

    bool spawning = false;
    SpriteRenderer sr;
    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        timer = 0;
    }

    private void Update()
    {
        //On attend le timer, puis on lance l'animation. Quand on atteint le sprite souhaité, on invoque les trésors.
        if (!spawning)
        {
            timer -= Time.deltaTime;
        }
            if (timer <= 0 && !spawning)
            {
            spawning = true;
            anim.SetTrigger("spawning");
            }

            if(sr.sprite==spawnSprite && spawning)
        {
            SummonTreasure();
        }
    }

    //On remet à jour le timer, et on invoque X trésors, choisis au hasard, à des positions random. 
    public void SummonTreasure()
    {
        timer = baseTime + Random.Range(-15, 36) / 10;
        spawning = false;
        for (int i = 0; i < Random.Range(5,9); i++)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            GameObject temp = Instantiate(treasure, pos + Random.insideUnitCircle * 5f, Quaternion.identity);
            treasure.transform.Translate(new Vector3(0, 0, -0.15f));
        }
        
    }
}
