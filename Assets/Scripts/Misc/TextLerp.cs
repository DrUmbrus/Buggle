using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLerp : MonoBehaviour
{
    /// <summary>
    /// Sert à déplacer un texte entre deux positions.
    /// </summary>
    [SerializeField]
    float speed=0;
    [SerializeField]
    Vector3 start=Vector3.zero, end=Vector3.zero;
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime * speed;
        transform.localPosition = Vector3.Lerp(start, end, timer);
        if (timer >= 1)
        {
            Destroy(gameObject);
        }
    }
}
