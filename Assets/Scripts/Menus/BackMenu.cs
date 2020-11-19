using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackMenu : MonoBehaviour
{
    //Retour au menu en appuyant sur B.

    public void OnBack()
    {
        FindObjectOfType<GameManager>().BackMenu();
        FindObjectOfType<GameManager>().backSelection.Play();
    }
}
