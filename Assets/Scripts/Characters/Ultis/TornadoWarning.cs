using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoWarning : MonoBehaviour
{
    /// <summary>
    /// La préparation des tirs de l'ulti moth
    /// </summary>

    //Le visuel de prévention
    LineRenderer lr;
    //Le joueur source
    int playerNum;
    //La direction
    Vector2 dir=Vector2.zero;
    //Le lerp de distance, pour donner cette impression de progression.
    float timer = 0;
    float dist=0;
    //Le tir en lui même
    public GameObject slash;
    //Les couleurs de la ligne. (note : faudra peut être faire des matériaux un jour)
    public Gradient[] gradCol;
    //L'angle
    float angled;
    AudioSource aud;

    //Variables d'état
    bool started = false;
    bool ending = false;
    float endTime = 1;
    bool endTornato = false;

    //Mise en place
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        aud = GetComponent<AudioSource>();
        aud.volume = 0.8f;
        aud.spatialBlend = 1;
        aud.dopplerLevel = 0;
    }

    //La fonction appelée par le character pour lui donner ses infos d'angle et de créateur
    public void Starting(int player, float angle)
    {
        lr.SetPosition(0, transform.position);
        angled = angle;
        playerNum = player;
        dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        //On lance l'action que quand on a déjà les stats (décalle d'une frame)
        started = true;
    }

    private void Update()
    {
        //Le line renderer progresse via le vector2 définit, un lerp, et une multiplication de distance pour son second point.
        if (started)
        {
            timer += Time.deltaTime*0.6f;
            dist = Mathf.Lerp(0, 70, timer);
            lr.SetPosition(1, (Vector2)transform.position + dir * dist);

            //Une fois le lerp complété, on met fin à ce passage, et on lance la deuxième séquence, avec un changement de couleur pour correspondre au perso
            if (timer >= 1)
            {
                started = false;
                ending = true;
                lr.colorGradient = gradCol[playerNum];
            }
        }

        //Si la préaparation est finie, on fait disparaitre progressivement la ligne. Quand celle-ci est complètement disparue, on lance la tornade, et on passe à la fin
        if (ending && !endTornato)
        {
            endTime -= Time.deltaTime * 2;
            lr.colorGradient.alphaKeys[0].alpha = Mathf.Lerp(0, 1, endTime);

            if (endTime <= 0)
            {
                GameObject temp =Instantiate(slash, transform.position, Quaternion.identity);
                temp.GetComponent<Projectile>().Create(playerNum, false);
                dir.Normalize();
                temp.GetComponent<TornadoMoth>().moving = dir;
                StartCoroutine(Dying());
            }
        }
    }

    //Coroutine de mort, en laissant le son de tornade se jouer;
    IEnumerator Dying()
    {
        endTornato = true;
        aud.Play();
        while(aud.isPlaying){
            yield return 0;
        }
        Destroy(gameObject);
    }
}
