using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NenupharMove : MonoBehaviour
{
    /// <summary>
    /// Les nénuphars de la map éponyme. Ils peuvent couler et on prend des dégâts dans l'eau. WIP
    /// </summary>
    /// 

    //La position en dehors et sous l'eau.
    Vector3 startPos, endPos;

    float timerL = 0, timeSink = 0;
    //Est-ce qu'on est en train de monter ou de descendre ?
    bool moveIn = false;
    //Des particules pour faire plouf dans l'eau. (pas encore présent)
    public GameObject waterPart = null;
    //Un collider sur le nénuphar, qui sert à faire les dégâts sous l'eau.
    CircleCollider2D col;
    //Les persos qui sont sous l'eau avec le nénuphar.
    List<Character> underwater = new List<Character>();
    //Le temps passé sous l'eau, pour timer les dégâts.
    List<float> underwatime = new List<float>();


    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        startPos = transform.position;
        endPos = startPos;
        endPos.z += 1;
    }

    private void Update()
    {
        //Le changement de valeurs dans le temps, pour le Lerp.
        if (!moveIn)
        {
            timerL -= Time.deltaTime;
        }
        else
        {
            timerL += Time.deltaTime;
        }

        transform.position = Vector3.Lerp(startPos, endPos, timerL);


        //Si le temps sous l'eau est au dessus de 0, on le diminue.
        if (timeSink > 0)
        {
            timeSink -= Time.deltaTime;

            if (timeSink <= 0)
            {
                timerL = 1;
                moveIn = false;

            }
        }

        //Tant qu'on est à plus de la moitié du lerp, le nénuphar est sous l'eau et donc fait des dégâts
        if (timerL >= 0.5f)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }

    public void MoveIn()
    {
        //On met le timer pour que le nénuphar soit dans l'eau pendant 10 secondes, et on lance le fait qu'il descend. Les particules seront rajoutées quand il y en aura.
        timeSink = 10;
        moveIn = true;
        timerL = 0;
        //Instantiate(waterPart, transform.position, Quaternion.identity);
    }

    //Si on entre en collision avec le nénuphar (donc qu'il est sous l'eau), on récupère le joueur pour le mettre dans la liste, et lancer le décompte avant de prendre des dégâts.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            bool test = true;
            if (underwater.Count > 0)
            {
                for (int i = 0; i < underwater.Count; i++)
                {
                    if (underwater[i] = collision.GetComponent<Character>())
                    {
                        test = false;
                    }
                }
            }
            if (test)
            {
                underwater.Add(collision.GetComponent<Character>());
                underwatime.Add(1f);
            }

        }
    }

    //Si on reste dans la collision, le timer descend, et on prend régulièrement des dégâts.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            Character potato = collision.GetComponent<Character>();
            int test = -1;
            for (int i = 0; i < underwater.Count; i++)
            {
                if (potato == underwater[i])
                {
                    test = i;
                    underwatime[i] -= Time.deltaTime;
                    if (underwatime[i] <= 0)
                    {
                        underwatime[i] = 0.2f;
                        potato.Damage(1, -1, true, false);
                    }
                }
            }

            if (test < 0)
            {
                underwater.Add(collision.GetComponent<Character>());
                underwatime.Add(0.2f);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            Character potato = collision.GetComponent<Character>();
            int test = -1;
            for (int i = 0; i < underwater.Count; i++)
            {
                if (potato == underwater[i])
                {
                    test = i;

                }
            }

            if (test >= 0)
            {
                underwater.RemoveAt(test);
                underwatime.RemoveAt(test);
            }
        }
    }

}
