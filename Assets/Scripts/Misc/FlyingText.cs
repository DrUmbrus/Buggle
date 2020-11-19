using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingText : MonoBehaviour
{
    /// <summary>
    /// Fait défiler un texte sur l'UI. Sert pour les annonces de temps restant.
    /// </summary>

    float timer = 1;
    Text text;
    Canvas father;
    public float speed=30;

    private void Awake()
    {
        father = GetComponentInParent<Canvas>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed*Time.deltaTime);
        timer -= Time.deltaTime/2;
        text.color = Color.Lerp(Color.clear, Color.white, timer);

        if (timer <= 0)
        {
            Destroy(father.gameObject);
        }
    }
}
