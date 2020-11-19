using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    //SCREENSHAKE


    //La durée, la force, et la baisse de force dans le temps
    public float duration;
    public float strength;
    public float damping;
    
    //La position de la caméra
    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Si c'est en train de shake, on bouge au hasard dans un cercle, on diminue le temps, et on s'assure de bien rester à la bonne distance pour voir le jeu
        if (duration > 0)
        {
       //     Vector2 cam = new Vector2(initPos.x, initPos.y);
            Vector2 pos = Random.insideUnitCircle * strength*Time.deltaTime;
            transform.position = new Vector3(pos.x, pos.y, -10);
            
            duration -= Time.deltaTime;
            strength /= damping;
        }
        //Sinon on revient à la position de départ. Note : à modifier pour prendre en compte les déplacements de la caméra
        else
        {
            transform.position = initPos;
            strength = 0;
            damping = 20;
        }
    }

    //Lancer le shake
    public void StartShaking(float dura, float str, float dank)
    {
        //On met en place la durée si c'est plus long qu'un shake en cours/que 0
        if (dura > duration)
        {
            duration = dura;
        }

        //Les shakes se cumulent (peut être pas pertinent)  
        if (str > strength)
        {
            strength = str;
        }
        

        //Et on prend le plus bas damping
        if (dank < damping)
        {
            damping = dank;
        }
    }
}
