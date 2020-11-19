using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cococinelle : Character
{
    /// <summary>
    /// Les aspects uniques de la coccinelle 
    /// </summary>
    
    //Son ulti
    public GameObject omegaTide;
    //Les sons pour le spell et l'ult
    public AudioSource endSpell;
    public AudioSource jump, crash;


    //Si on est en train de bloquer
    bool block = false;

    //Timer de block et durée max du block
    public float timeBlock, maxTimeBlock;




    public override void Pattern()
    {
        //Si on est en train de bloquer
        if (block)
        {
            //On vérifie le timer
            if (timeBlock > 0)
            {
                timeBlock -= Time.deltaTime;
                //Le cd du spell ne se fait pas tant qu'on est actif
                timerComp = maxTimerComp;
            }
            else
            {
                endSpell.Play();
                anim.SetTrigger("endshield");
                //Voir OnEnd.
                block = false;
                //anim.SetTrigger("release");
                speed = baseSpeed;
                blocking = false;
            }
        }
    }


    //Quand on lance le spell
    public override void Competence()
    {
        
        anim.SetTrigger("shield");
        //On  met le temps de block au max
        timeBlock = maxTimeBlock;
        //On bloque
        block = true;
        //Le perso est ralentit
        speed = baseSpeed / 2;
        //Et on active le blocage de dégâts
        blocking = true;
    }

    //Lancer la coroutine de l'ulti
    public override void Ultimate()
    {
        StartCoroutine(Omega());
    }

    IEnumerator Omega()
    {
        //On active l'invincibilité, le mode ulti, et l'animation.
        anim.SetTrigger("ultimate");
        gm.ult = true;
        stopped = true;
        box.enabled = false;
        yield return new WaitForSeconds(0.3f);
        //On a besoin de timer bien les sons : Jump, 0.6s de pause, crash, 0.3s de pause, recommencer
        jump.Play();
        yield return new WaitForSeconds(0.6f);
        crash.Play();

        //Là ça devient compliqué.
        //On créé le premier coup de l'ulti, puis les deux cercles extérieurs, avec 0.3s de décallage.
        GameObject first = Instantiate(omegaTide, transform.position, Quaternion.identity);
            first.GetComponent<Warning>().Summoning(playerNum);
            yield return new WaitForSeconds(0.3f);


            for (int i = 0; i < 5; i++)
            {
                GameObject temp = Instantiate(omegaTide, transform.position, Quaternion.identity);

                temp.transform.rotation = Quaternion.Euler(0, 0, angle + 90 + 72 * i);
                temp.transform.Translate((Vector3.up * 8), Space.Self);
                temp.transform.rotation = Quaternion.identity;
                temp.GetComponent<Warning>().Summoning(playerNum);
            }
        jump.Play();
        yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < 10; i++)
            {
                GameObject temp = Instantiate(omegaTide, transform.position, Quaternion.identity);

                temp.transform.rotation = Quaternion.Euler(0, 0, angle + 45 + 36 * i);
                temp.transform.Translate((Vector3.up * 16), Space.Self);
                temp.transform.rotation = Quaternion.identity;
                temp.GetComponent<Warning>().Summoning(playerNum);
            }
            yield return new WaitForSeconds(0.3f);

        //Le processus est repris 2 fois, en changeant les valeurs d'angle et de distance pour donner un aspect plus chaotique à l'ensemble.
        first = Instantiate(omegaTide, transform.position, Quaternion.identity);
        first.GetComponent<Warning>().Summoning(playerNum);
        crash.Play();
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(omegaTide, transform.position, Quaternion.identity);

            temp.transform.rotation = Quaternion.Euler(0, 0, angle + 90 + 72 * i+36);
            temp.transform.Translate((Vector3.up * 6), Space.Self);
            temp.transform.rotation = Quaternion.identity;
            temp.GetComponent<Warning>().Summoning(playerNum);
        }
        jump.Play();
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(omegaTide, transform.position, Quaternion.identity);

            temp.transform.rotation = Quaternion.Euler(0, 0, angle + 45 + 36 * i+18);
            temp.transform.Translate((Vector3.up * 18), Space.Self);
            temp.transform.rotation = Quaternion.identity;
            temp.GetComponent<Warning>().Summoning(playerNum);
        }
        yield return new WaitForSeconds(0.3f);

        first = Instantiate(omegaTide, transform.position, Quaternion.identity);
        first.GetComponent<Warning>().Summoning(playerNum);
        crash.Play();
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(omegaTide, transform.position, Quaternion.identity);

            temp.transform.rotation = Quaternion.Euler(0, 0, angle + 90 + 72 * i+18);
            temp.transform.Translate((Vector3.up * 9), Space.Self);
            temp.transform.rotation = Quaternion.identity;
            temp.GetComponent<Warning>().Summoning(playerNum);
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(omegaTide, transform.position, Quaternion.identity);

            temp.transform.rotation = Quaternion.Euler(0, 0, angle + 45 + 36 * i+54);
            temp.transform.Translate((Vector3.up * 16), Space.Self);
            temp.transform.rotation = Quaternion.identity;
            temp.GetComponent<Warning>().Summoning(playerNum);
        }
        yield return new WaitForSeconds(0.3f);
        //Fin de l'ulti, retour à la normale.
        stopped = false;
        box.enabled = true;
        killStreak = 0;
        gm.ult = false;
    }
}
