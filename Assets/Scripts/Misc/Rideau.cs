using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rideau : MonoBehaviour
{
    /// <summary>
    /// Gère l'écran de chargement, en faisant apparaître ou disparaître l'écran noir
    /// </summary>
    GameManager gm;
    Image sr;
    Color op = new Color(0, 0, 0, 1);
    Color tran = new Color(0, 0, 0, 0);
    float time=0;
    bool sens=false;
    public bool sent=false;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        sr = GetComponent<Image>();
        time = 1;
    }

    private void Update()
    {
        if (sens)
        {
            if (time < 1)
            {
                time += Time.deltaTime *2f;
            }
            else
            {
                if (!sent)
                {
                    sent = true;
                }
            }
        }
        else
        {
            if (time > 0)
            {
                time -= Time.deltaTime * 2f;
            }
            else
            {
                if (!sent)
                {
                    sent = true;
                    transform.position = new Vector3(10000, -10000, 0);
                }
            }
        }

        sr.color = Color.Lerp(tran, op, time);
    }


    public void Open()
    {
        sens = false;
        sent = false;
    }

    public void Close()
    {
        transform.localPosition = new Vector3(0, 0, -3.2f);
        sens = true;
        sent = false;
    }
}
