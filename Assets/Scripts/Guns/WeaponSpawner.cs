using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    /// <summary>
    /// Spawner d'armes.
    /// </summary>

    //La liste d'armes qui peut spawn. Note : mettre des armes plusieurs fois si elles ont un taux de spawn plus élevé. Note 2 : faire un système de taux de spawn mieux foutu.
    public List<GameObject> guns;

    //Cooldown
    float timer=0;
    public float baseTime=0;
    public bool grabbed = true;

    //Quelles armes sont high tiers.
    public List<bool> highTiers;
    //Combien de spawn on a eu sans high tiers.
    int noHigh=0;

    SpriteRenderer sr = null;
    bool spawning = false;

    public Sprite spawnSprite = null;

    Animator anim = null;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        timer = baseTime + Random.Range(-15, 36) / 10;
    }

    //On spawn quand le timer est fini
    private void Update()
    {
        if (grabbed && !spawning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0 && !spawning)
            {
                spawning = true;
                anim.SetTrigger("spawning");
                StartCoroutine(Spawn());
            }
        }

    }

    public void Grabbing()
    {
        grabbed = true;
    }


    IEnumerator Spawn()
    {
        //Cooldown pour que l'animation se joue en entier
        yield return new WaitForSeconds(4);
        spawning = false;
        timer = baseTime + Random.Range(-15, 36) / 10;
        int gun = Random.Range(0, guns.Count);

        //Si on spawn une arme low tiers, on le note, sinon on remet la variable à 0.
        if (!highTiers[gun])
        {
            noHigh++;
        }
        else
        {
            noHigh = 0;
        }
       
        //Au bout de  4 spawns low tiers, on force un reroll tant que ça tombe pas sur du high tiers.
        if (noHigh >= 4)
        {
            while (!highTiers[gun])
            {
                gun = Random.Range(0, guns.Count);
            }
        }
        //Le spawn
        GameObject temp = Instantiate(guns[gun], new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), Quaternion.identity);
        temp.GetComponent<FloorMe>().spawn = this;
        grabbed = false;
    }

}
