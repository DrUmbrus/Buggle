using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonFrog : MonoBehaviour
{
    /// <summary>
    /// Wip qui ne marche pas, à retravailler 
    /// </summary>
    public float maxTimer, timer;
    public GameObject frog;
    public Vector3[] startFrog;
    public Vector3[] posFrog;
    GameObject froggy = null;

    private void Update()
    {
        if(timer>0 && froggy == null)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = maxTimer;
            SummonFroggy();
        }
    }

    void SummonFroggy()
    {
        int rand = Random.Range(0, startFrog.Length);

        froggy = Instantiate(frog, startFrog[rand], Quaternion.identity);
        Vector3 destination = posFrog[rand];
        froggy.GetComponent<Frog>().SetStats(destination, this);
    }

    public void NoFrog()
    {
        froggy = null;
    }
}
