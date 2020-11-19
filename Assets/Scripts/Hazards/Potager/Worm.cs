using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    /// <summary>
    /// Les vers de la map potager. WIP
    /// Des petits objets en groupe, qui servent de mur, et qui bougent.
    /// </summary>
    
    //La vitesse et la durée de vie.
    [SerializeField]
    float speed=0;
    [SerializeField]
    float lifetime = 0;

    //Ils ont une durée de vie
    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    //Et ils avancent.
    private void Update()
    {
        transform.Translate(Vector3.right * speed*Time.deltaTime, Space.Self);    
    }
}
