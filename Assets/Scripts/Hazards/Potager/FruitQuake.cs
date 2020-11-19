using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitQuake : MonoBehaviour
{
    /// <summary>
    /// Copie du système EarthQuake, pour la map potager.
    /// </summary>
    /// 

    GameManager gm=null;
    //Les ombres pour annoncer les chutes.
    public GameObject shadow= null;
    //les power ups qui peuvent se trouver dans les fruits.
    public GameObject[] powerUp = new GameObject[0];
    //Le temps entre deux chutes, et la variance dans ce temps.
    public float timer=0;
    public float maxTimer=0;
    public int variante=0;

    //Le temps entre deux apparitions de vers.
    [SerializeField]
    float wormTime = 0, maxWormTime = 0;

    //Le prefab des vers.
    [SerializeField]
    GameObject wormPart = null;


    private void Awake()
    {
        timer = maxTimer;
        gm = FindObjectOfType<GameManager>();
    }

    //Les timers descendent et se reset à 0 après avoir fait leur action.
    private void Update()
    {
        timer -= Time.deltaTime;
        wormTime -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 999;
            StartCoroutine(Quaking());
        }
        if (wormTime <= 0)
        {
            wormTime = maxWormTime + Random.Range(-5, 6);
            int rand = Random.Range(0, 4);
            Debug.Log(rand*90);
            
            StartCoroutine(Worming(rand*90));
        }
    }

    //On invoque 25 morceaux de vers, avec un timer pour en faire un gros objet. Ils peuvent avoir une des quatre directions cardinales.
    IEnumerator Worming(int angle)
    {
        for (int i = 0; i < 25; i++)
        {
            Instantiate(wormPart, transform.position, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(0.1f);
        }
    }

    //La coroutine pour faire tomber les fruits.
    IEnumerator Quaking()
    {
        //Nombre aléatoire de chutes.
        int rocks = Random.Range(7,13);
        //Mettre le screenshake
        gm.Shake(5, 35, 1.05f);

        //On les invoque à des positions aléatoires, en leur mettant un powerup aléatoire dedans.
        for (int i = 0; i < rocks; i++)
        {
            Vector3 pos = Vector3.zero;
            pos.z = -2;
            pos.x = Random.Range(-gm.width, gm.width +0.1f);
            pos.y = Random.Range(-gm.height, gm.height -0.5f);
            int rand = Random.Range(0, powerUp.Length);
            GameObject temp = Instantiate(shadow,pos, Quaternion.identity);
            temp.GetComponent<Fruitfall>().powerup = powerUp[rand];
            yield return new WaitForSeconds(Random.Range(0.1f,0.35f));
        }
        //On reset ensuite le timer avant la prochaine chute. Pas avant, parce que la durée dépend du nombre de roches.
        timer = maxTimer + Random.Range(-variante, variante + 1) / 10;
    }
}
