using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTreasure : MonoBehaviour
{
    Character targetPlayer;
    public int speed;
    //public float speedTarget;
    Vector3 pos;
    float timer = 0;
    bool moving = false;
    public Gradient[] colors;
    bool dying = false;


    public void SetTarget(int tar)
    {
        
        Character[] players = FindObjectsOfType<Character>();
        foreach (var plaer in players)
        {
            if (plaer.playerNum == tar)
            {
                targetPlayer = plaer;
            }
        }
        StartCoroutine(Floating());
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var col = ps.colorOverLifetime;
        col.color = colors[tar];
    }

    private void Update()
    {
        if (!dying)
        {
            if (!moving)
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }

            if (moving)
            {
                timer += Time.deltaTime;

                transform.position = Vector3.Lerp(pos, targetPlayer.transform.position, timer);
            }

            if (timer > 0.9f)
            {
                targetPlayer.AddLoot(10);
                dying = true;
                StartCoroutine(Dying());
            }
        }
        
    }

    IEnumerator Dying()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<AudioSource>().Play();
        while (GetComponent<AudioSource>().isPlaying) {
            yield return 0;
        }
        Destroy(gameObject);
    }
    IEnumerator Floating()
    {

        yield return new WaitForSeconds(1.5f);

        pos = transform.position;
        moving = true;

    }
}
