using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingLightning : MonoBehaviour
{
    /// <summary>
    /// Tentative de script pour un éclair rebondissant entre les ennemis. WIP, non testé.
    /// </summary>
    
    List<GameObject> targets= new List<GameObject>();
    public float distance=0;
    LineRenderer lr = null;
    public float damage = 0;
    public float timeBetweenShot = 0;
    float timeLerp = 0;

    public GameObject Overlap(float dist, Vector2 pos)
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(pos, dist);
        List<GameObject> closeEnough = new List<GameObject>();
        

        foreach(Collider2D co in cols)
        {
            if (co.gameObject.tag=="player" || co.gameObject.tag == "damageable")
            {
                closeEnough.Add(co.gameObject);
            }
        }

        if (closeEnough.Count <= 0)
        {
            return null;
        }

        List<int> begone = new List<int>();

        if(targets.Count>0 && closeEnough.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                for (int j = 0; j < closeEnough.Count; j++)
                {
                    if (closeEnough[j] == targets[i])
                    {
                        begone.Add(j);
                    }
                }
            }
        }

        if (begone.Count > 0)
        {
            for (int i = begone.Count-1; i>=0; i--)
            {
                closeEnough.RemoveAt(begone[i]);
            }
        }

        float closest = 100;
        GameObject newTarget = null;

        if (closeEnough.Count > 0)
        {
            for (int i = 0; i < closeEnough.Count; i++)
            {
                if(Vector2.Distance(closeEnough[i].transform.position, pos) < closest)
                {
                    closest = Vector2.Distance(closeEnough[i].transform.position, pos);
                    newTarget = closeEnough[i];
                }
            }
        }

        targets.Add(newTarget);
        return newTarget;
    }

    IEnumerator Discharge()
    {
        yield return new WaitForSeconds(timeBetweenShot);
        if(Overlap(distance, targets[targets.Count - 1].transform.position)!=null)
        {
            timeLerp = 0;
            Discharge();
        }
        else
        {
            StartCoroutine(Ded());
        }
    }

    IEnumerator Ded()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (timeBetweenShot<=0)
        {
            //Version instant
            if (targets.Count > 0)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    lr.SetPosition(i, targets[i].transform.position);
                }
            }
        }
        else
        {
            if (timeLerp < 1)
            {
                timeLerp += Time.deltaTime/timeBetweenShot;
            }

            if (targets.Count > 1)
            {

                lr.SetPosition(0, Vector2.Lerp(targets[targets.Count-2].transform.position, targets[targets.Count-1].transform.position, timeLerp));
                lr.SetPosition(1, targets[targets.Count - 1].transform.position);
            }
            else if (targets.Count == 1)
            {
                lr.SetPosition(0, Vector2.Lerp(transform.position, targets[0].transform.position, timeLerp));
                lr.SetPosition(1, targets[targets.Count - 1].transform.position);
            }
            
        }
        


        
    }

    private void Awake()
    {
        if (timeBetweenShot > 0)
        {
            if(Overlap(distance, transform.position) != null)
            {
                StartCoroutine(Discharge());
            }
        }
        else
        {
            bool test = false;
            if (Overlap(distance, transform.position)== null)
            {
                test = true;
            }

            while (!test)
            {
                
               if(Overlap(distance, targets[targets.Count - 1].transform.position) == null)
                {
                    test = true;
                }
            }

            StartCoroutine(Ded());
            
        }
    }
}
