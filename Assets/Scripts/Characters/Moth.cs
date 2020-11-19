using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Character
{
    /// <summary>
    /// Aspects uniques de la mite.
    /// </summary>
    
    [Header("Character")]
    //Le spell
    public GameObject burst;
    //La préparation des tirs de l'ulti.
    public GameObject slash;

    //La tornade autour de la moth, et ses  variables pour la changer de taille.
    [SerializeField]
    GameObject windUlt = null;
    float timeTornado = 0;
    bool tornadoCreate = false;
    Vector3 minTor = new Vector3(3, 0, 1);
    Vector3 maxTor = new Vector3(3, 3, 1);
    [SerializeField]
    AudioSource ultAudio = null, tornadodio = null;

    public GameObject[] mothShots = new GameObject[0];
    public MothShot[] shots = new MothShot[4];

    public float timeBall, maxTimeBall;

    public override void Starting()
    {
        for (int i = 0; i < mothShots.Length; i++)
        {
            shots[i] = mothShots[i].GetComponentInChildren<MothShot>();
            shots[i].id = playerNum;
            mothShots[i].SetActive(false);
        }
    }

    //La compétence. Invoque 4 boules qui tournent (voir MothShot). Contient encore le code de l'ancien spell, au cas où.
    public override void Competence()
    {
        //Quand on lance la compétence, le perso s'arrête, et on lance l'animation.
        anim.SetTrigger("action");      
        /*sstopped = true;
        blocking = true;*/
        for (int i = 0; i < mothShots.Length; i++)
        {
            mothShots[i].SetActive(true);
        }
        timeBall = maxTimeBall;
        //GameObject temp=Instantiate(burst, transform.position, Quaternion.identity);
        //temp.GetComponent<MothBurst>().SetStats(gameObject, this);
    }

    public override void Pattern()
    {
        //Si on est en ulti, la tornade autour du perso s'active, sinon elle s'en va. L'audio de l'ulti est réglé sur la même valeur.
            if (tornadoCreate)
            {
                timeTornado += Time.deltaTime*2f;
            }
            else
            {
                timeTornado -= Time.deltaTime*2;
            }

        windUlt.transform.localScale = Vector3.Lerp(minTor, maxTor, timeTornado);
        ultAudio.volume = Mathf.Lerp(0, 1, timeTornado);

        if (timeBall > 0)
        {
            timeBall -= Time.deltaTime;

            if (timeBall <= 0)
            {
                for (int i = 0; i < mothShots.Length; i++)
                {
                    //shots[i].Shot(angle);
                    mothShots[i].SetActive(false);
                }
            }
        }

        if (life <= 0)
        {
            for (int i = 0; i < mothShots.Length; i++)
            {
                //shots[i].Shot(angle);
                mothShots[i].SetActive(false);
            }
        }
    }

    //Activer la fin du spell (passe par l'objet créé)
    public void EndComp()
    {
        stopped = false;
        blocking = false;
    }

    //Lancer l'ulti
    public override void Ultimate()
    {
        StartCoroutine(UltiTor());
    }


    IEnumerator UltiTor()
    {
        //Mise en place de l'invincibilité, de l'animation, et de la tornade.
        anim.SetTrigger("ultimate");
        windUlt.gameObject.SetActive(true);
        gm.ult = true;
        stopped = true;
        box.enabled = false;
        yield return new WaitForSeconds(0.25f);
        timeTornado = 0;
        tornadoCreate = true;
        yield return new WaitForSeconds(0.5f);

        //Pour les deux directions possibles
        for (int i = 0; i < 2; i++)
        {
            //On a 16 tirs, avec tous 22.5° d'écart entre eux.
            for (int j = 0; j < 16; j++)
            {
                GameObject temp = Instantiate(slash, transform.position, Quaternion.identity);
                temp.GetComponent<TornadoWarning>().Starting(playerNum, i*11.25f+ 90+ j*22.5f);
            }
            //On attend avant l'audio parce qu'il y a 1.5s de préparation.
            yield return new WaitForSeconds(1.5f);
            tornadodio.Play();
        }
        //Fin de l'ulti, on désactive la tornade et tout le reste
        timeTornado = 1;
        tornadoCreate = false;
        anim.SetTrigger("endult");
        yield return new WaitForSeconds(0.25f);
        stopped = false;
        box.enabled = true;
        gm.ult = false;
        killStreak = 0;
        windUlt.gameObject.SetActive(false);
    }
}
