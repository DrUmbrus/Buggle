using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisapearingText : MonoBehaviour
{
    /// <summary>
    /// Un texte qui apparait, et fade.
    /// </summary>
    
    public float speedDiv=0;
    Text text = null;
    float timer = 1;
    public Color cle = new Color(0,0,0,0), op = new Color(0,0,0,0);


    private void Awake()
    {
        timer = 1;
    }
    private void Update()
    {
        timer -= Time.deltaTime / speedDiv;

        text.color = Color.Lerp(cle, op, timer);
        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
