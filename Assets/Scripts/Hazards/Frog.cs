using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Damageable
{
    /// <summary>
    /// Wip qui ne marche pas, à retravailler 
    /// </summary>
    public GameObject waterPart;
    float timer =0;
    SummonFrog sf = null;
    Vector3 destination=Vector3.zero;
    Vector3 startPos=Vector3.zero;
    AudioClip silence;
    Collider2D col = null;

    public override void Awakening()
    {
        startPos = transform.position;
        Instantiate(waterPart, transform.position, Quaternion.identity);
    }

    public void SetStats(Vector3 des, SummonFrog s)
    {
        sf = s;
        destination = des;
    }

    private void Update()
    {
        if (destination != Vector3.zero && !dying)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, destination, timer);

            if(timer>1 && !col.enabled)
            {
                col.enabled = true;
            }
        }

        if (dying)
        {
            timer -= Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, destination, timer);

            if (timer <= 0)
            {
                Instantiate(waterPart, transform.position, Quaternion.identity);
                sf.NoFrog();
                Destroy(gameObject);
            }
        }
    }

    public override void Death(int source)
    {
        GetComponent<Animator>().SetTrigger("end");

    }
}
