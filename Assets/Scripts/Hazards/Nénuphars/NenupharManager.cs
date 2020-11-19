using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NenupharManager : MonoBehaviour
{
    //Gère les nénuphars du niveau. WIP.

    //Les nénuphars.
    public NenupharMove[] nenuphars;
    //Le temps entre deux nénuphars qui passent sous l'eau. 
    public float timer, maxTimer;

    //Simple système de timer et on active un nénuphar au hasard quand on arrive à 0.
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = maxTimer;
            int rand = Random.Range(0, nenuphars.Length);
            nenuphars[rand].MoveIn();
        }
    }

    private void Awake()
    {
        nenuphars = FindObjectsOfType<NenupharMove>();
    }
}
