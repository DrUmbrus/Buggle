using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikWall : MonoBehaviour
{

    void Update()
    {
       if(transform.localPosition!=Vector3.zero)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
