using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSumo : MonoBehaviour
{
    //Si on touche le mur en mode sumo, on meurt.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            collision.GetComponent<Character>().Damage(100, -1, true, false);
        }
    }
}
