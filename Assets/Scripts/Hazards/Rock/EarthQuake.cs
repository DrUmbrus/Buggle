using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour
{
    /// <summary>
    /// Le manager pour la map earthquake.
    /// </summary>
    /// 

    GameManager gm;
    //Les ombres des rocks.
    public GameObject shadow;
    //Temps entre deux chutes, et la variance.
    public float timer;
    public float maxTimer;
    public int variante;


    private void Awake()
    {
        timer = maxTimer;
        gm = FindObjectOfType<GameManager>();
    }

    //Le timer diminue et lance la coroutine à 0.
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 999;
            StartCoroutine(Quaking());
        }
    }

    IEnumerator Quaking()
    {
        //On fait tomber un nombre aléatoire de cailloux à des placements aléatoires, puis on remet le timer à son maximum, avec une petite variance.
        int rocks = Random.Range(7,13);

        gm.Shake(5, 35, 1.05f);
        for (int i = 0; i < rocks; i++)
        {
            Vector3 pos = Vector3.zero;
            pos.z = -2;
            pos.x = Random.Range(-gm.width, gm.width +0.1f);
            pos.y = Random.Range(-gm.height, gm.height -0.5f);
            Instantiate(shadow,pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.1f,0.35f));
        }

        timer = maxTimer + Random.Range(-variante, variante + 1) / 10;
    }
}
