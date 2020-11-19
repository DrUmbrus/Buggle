using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuMove : MonoBehaviour
{
    /// <summary>
    /// Déplacement du curseur sur le menu principal, et choix de la scène.
    /// </summary>
    
    GameManager gm;
    int pos = 0;
    public int maxPos;
    public Transform[] positions;
    bool move=false;
    AudioSource movement;

    private void Awake()
    {
        movement = GetComponent<AudioSource>();

        gm = FindObjectOfType<GameManager>();
        transform.position = positions[pos].position;
        transform.position = positions[pos].position;
        transform.Translate(new Vector3(-300, 0, 0));
        positions[0].GetComponent<Outline>().enabled = true;
    }

    public void OnMove(InputValue value)
    {
        positions[pos].GetComponent<Outline>().enabled = false;
        Vector2 potato = value.Get<Vector2>();
        if (!move)
        {
            if (potato.y > 0.5f)
            {
                if (pos > 0)
                {
                    pos--;
                }
                else
                {
                    pos = maxPos;
                }
                move = true;
                movement.Play();
            }
            else if (potato.y < -0.5f)
            {
                if (pos <maxPos)
                {
                    pos++;
                }
                else
                {
                    pos = 0;
                }
                move = true;
                movement.Play();
            }
        }
        else if (potato.y >-0.1f && potato.y < 0.1f)
        {
            move = false;
        }
        positions[pos].GetComponent<Outline>().enabled = true;
        transform.position = positions[pos].position;
        transform.Translate(new Vector3(-300, 0, 0));
    }

    public void OnGrab()
    {
        gm.ButtonBattle(pos);
        if (!gm.nextSelection.isPlaying)
        {
            gm.nextSelection.Play();
        }
    }


}
