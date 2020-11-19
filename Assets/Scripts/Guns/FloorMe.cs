using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMe : MonoBehaviour
{
    //Arme posée au sol

    //Quelle arme c'est
    public GameObject gun;
    //Son canvas de "press a to grab"
    public GameObject canvas;
    //Qui l'a fait spawn
    public WeaponSpawner spawn;
    //le temps avant destruction
    public float dyingTime = 8;
    bool ending = false;
    AudioSource aud;
    bool taken = false;

    List<GameObject> top = new List<GameObject>();

    //Mise en place des stats
    private void Awake()
    {
        aud = GetComponent<AudioSource>();

        if (aud != null)
        {
            aud.volume = 1;
            aud.spatialBlend = 1;
            aud.dopplerLevel = 0;
            aud.minDistance = 8;
            aud.maxDistance = 20;
        }
        
        canvas.SetActive(false);
        StartCoroutine(Dying());
    }

    //La fonction quand on récupère l'objet au sol
    public void GrabGun(Character chara)
    {
        //On peut ramasser une arme que si personne y a touché avant.
        if (!taken)
        {
            taken = true;
            //Invoque l'arme, et active sa fonction grabMe, puis détruit le prefab au sol
            GameObject temp = Instantiate(gun, transform.position, Quaternion.identity);
            temp.GetComponent<Gun>().GrabMe(chara);
            //Si l'arme vient d'un spawner, on lui annonce qu'elle a été grab pour lancer le cooldown
            if (spawn != null)
            {
                spawn.Grabbing();
            }
            GetComponent<SpriteRenderer>().color = Color.clear;
            StartCoroutine(EndSound());
        }
        
    }

    //On attend le temps de vie, puis l'arme se détruit, si elle est pas dans son autre coroutine.
    IEnumerator Dying()
    {
        yield return new WaitForSeconds(dyingTime);
        if (spawn != null)
        {
            spawn.Grabbing();
        }
        yield return 0;
        if (!ending)
        {
            Destroy(gameObject);
        }
        
    }

    //Afficher et enlever le canvas quand quelqu'un s'approche.
    public void ShowCan(GameObject who)
    {
        if (!ending)
        {
            canvas.SetActive(true);
            top.Add(who);
        }
        
    }

    public void HideCan(GameObject who)
    {
        top.Remove(who);
        if (top.Count == 0)
        {
            canvas.SetActive(false);
        }
    }

    //Autre coroutine de mort, l'arme est invisible, et existe juste pour finir son bruit.
    IEnumerator EndSound()
    {
        ending = true;
        canvas.SetActive(false);
        GetComponent<SpriteRenderer>().color = Color.clear;
        if (aud != null)
        {
            aud.Play();
            while (aud.isPlaying)
            {
                yield return 0;
            }
        }
        else
        {
            yield return 0;
        }
        Destroy(gameObject);
    }
}
