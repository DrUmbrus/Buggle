using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] guns;
    float timer;
    public float baseTime;
    public bool grabbed = true;

    SpriteRenderer sr;
    bool spawning = false;

    public Sprite spawnSprite;

    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        timer = baseTime + Random.Range(-15, 36) / 10;
    }
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
        yield return new WaitForSeconds(4f);
        spawning = false;
        timer = baseTime + Random.Range(-15, 36) / 10;
        var gun = Random.Range(0, guns.Length);
        GameObject temp = Instantiate(guns[gun], new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), Quaternion.identity);
        temp.GetComponent<ObjetSol>().rs = this;
        grabbed = false;
    }
}
