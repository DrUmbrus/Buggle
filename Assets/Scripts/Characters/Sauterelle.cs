using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauterelle : Character
{
    /// <summary>
    /// Aspects uniques de la sauterelle
    /// </summary>
    public LayerMask masks = 0, touchMask = 0;
    //Le GO de marouflage pour son saut
    public GameObject slash = null;
    //Le GO de l'ulti
    public GameObject omniSlash = null;
    //Les positions d'attaque pour l'ulti
    public List<Vector3> slashPos;
    Vector2 targetPos = Vector2.zero;
    Vector2 startPos = Vector2.zero;
    //Sert pour l'aléatoire de l'ulti
    List<int> intPos= new List<int>();
    //La direction de saut
    Vector2 target=Vector2.zero;
    //La vitese du saut
    public float speedDash=0;
    public float disDash = 7;

    //Si on est en train de sauter
    bool jumping=false;
    bool stopJump = false;

    //Un timer
    float move = 0;
    float stoppedTime = 0;

    //Qui a déjà été touché par le dash.
    public List<Character> victimes=new List<Character>();

    [SerializeField]
    AudioSource omnislash = null;

    //On récupère tous les slash de la map quand on commence
    public override void Starting()
    {
        GameObject[] slashes = GameObject.FindGameObjectsWithTag("slash");
        foreach(GameObject sla in slashes)
        {
            slashPos.Add(sla.transform.position);
        }

    }

    public override void Competence()
    {
        startPos = transform.position;
        victimes.Clear();
        //On prend la direction dans laquelle le joueur était en train de se déplacer
        target = movement;
        //Le vecteur est normalisé
        target.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target, disDash, masks);
        if (hit)
        {
            targetPos = hit.point;
        }
        else
        {
            targetPos = (Vector2)transform.position + target* disDash;
        }
        //Le joueur ne peut pas se déplacer pendant le saut
        stopped = true;

        stopJump = true;
        stoppedTime = 0;
        //Le timer est reset
        move = 0;
        slash.SetActive(true);
        slash.transform.localScale = new Vector3(slash.transform.localScale.x, Mathf.Clamp(Vector2.Distance(transform.position, targetPos)*(3f/5f),0, 100), slash.transform.localScale.z);
        slash.GetComponent<SauterelleSlash>().SetStats(target);
        anim.SetTrigger("action");
    }

    public override void Pattern()
    {
        if (stopJump)
        {
            stoppedTime += Time.deltaTime * 2;

            if (stoppedTime >= 1)
            {
                stopJump = false;
                blocking = true;
                jumping = true;
                sr.color = Color.clear;
                

            }
        }
        

        //Pendant le saut
        if (jumping)
        {
            //Le timer est en route
            move += Time.deltaTime*speedDash;
            transform.position = Vector2.Lerp(startPos, targetPos, move);
            slash.transform.localScale = new Vector3(slash.transform.localScale.x, 0, slash.transform.localScale.z);

            //Au bout d'une seconde, le jump s'arrête
            if (move >= 1)
            {
                sr.color = Color.white;
                anim.SetTrigger("endaction");
                jumping = false;
                blocking = false;
                if (life > 0)
                {
                    stopped = false;
                }
                slash.GetComponent<SauterelleSlash>().Detonation();
                // slash.SetActive(false);
            }
        }

        if (life <= 0)
        {
            stopJump = false;
            jumping = false;
            sr.color = Color.white;
            slash.SetActive(false);
        }

    }

    //Si on se cogne contre un mur, le saut est stoppé.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (jumping)
        {
                anim.SetTrigger("endaction");
                jumping = false;
                if (life > 0)
                {
                    stopped = false;
                }

                slash.SetActive(false);
        }
    }

    public override void Ultimate()
    {
        StartCoroutine(UltiSlash());   
    }

    IEnumerator UltiSlash()
    {
        //Mise en place des stats. On remplie intPos avec autant de valeurs que y a de slashs.
        omnislash.Play();
        gm.ult = true;
        for (int i = 0; i < slashPos.Count; i++)
        {
            intPos.Add(i);
        }
        anim.SetTrigger("ultimate");
        stopped = true;
        box.enabled = false;
        yield return new WaitForSeconds(0.5f);
        
        sr.color = Color.clear;
        
        //Ensuite on créé un slash pour chaque position, à 0.15s d'intervale.
        for (int i = 0; i < slashPos.Count; i++)
        {
            int posi = Random.Range(0, intPos.Count);
            

            GameObject temp=Instantiate(omniSlash, transform.position, Quaternion.identity);
            temp.GetComponent<OmniSlash>().SetStats( playerNum, slashPos[intPos[posi]], gm);
            intPos.RemoveAt(posi);

            yield return new WaitForSeconds(0.15f);
        }

        //Fin de l'ult
        box.enabled = true;
        sr.color = Color.white;
        anim.SetTrigger("endult");
        yield return new WaitForSeconds(0.5f);
        stopped = false;
        killStreak = 0;
        gm.ult = false;
    }

}
