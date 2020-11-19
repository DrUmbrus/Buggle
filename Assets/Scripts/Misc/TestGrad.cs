using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrad : MonoBehaviour
{
    /// <summary>
    /// Test de material défilant.
    /// </summary>
    float timer = 0.3f;
    public Material mat;
    public float speed=1.5f;

    void Update()
    {
        timer += Time.deltaTime*speed;
        if (timer > 1)
        {
            timer = 0;
        }

        mat.SetFloat("_Timing", timer);
    }
}
