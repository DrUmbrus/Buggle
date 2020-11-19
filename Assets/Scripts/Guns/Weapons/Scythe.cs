using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : Gun
{
    /// <summary>
    /// Le "parapluie" électrique. Se charge quelques secondes, puis fait un tir en ligne droite, suivi de 4 tirs en étoile.
    /// </summary>


    bool co = false;
    /*public LineRenderer[] side;
    public Transform[] sidePos;*/
    bool flippedB = false;
    public GameObject trail;


    public override void Pattern()
    {
        

        //On active la trainée que quand on tire
        if (sr.sprite != baseSprite) 
        {
            trail.SetActive(true);
        }

        if (sr.sprite == baseSprite)
        {
            trail.SetActive(false);
        }

        //On pense à faire que la trainée suit la tête de l'objet.
        if (sr.flipY && !flippedB)
        {
            trail.transform.localPosition = new Vector3(trail.transform.localPosition.x, -trail.transform.localPosition.y, trail.transform.localPosition.z);
            flippedB = true;
        }
        if(flippedB && !sr.flipY)
        {
            trail.transform.localPosition = new Vector3(trail.transform.localPosition.x, -trail.transform.localPosition.y, trail.transform.localPosition.z);
            flippedB = false ;
        }
    }



    public override void ShootingPattern()
    {
        if(!co)
        StartCoroutine(ShootyMode());
    }

    //4 tirs dans les angles, après les 0.35s d'attente.
    IEnumerator ShootyMode()
    {
        co = true;
        yield return new WaitForSeconds(0.35f);
        audSo.Play();
        for (int i = 0; i < 4; i++)
        {
            //On créé l'objet
            GameObject temp = Instantiate(projec, muzzle.position, Quaternion.identity);

            //On réutilise le même système que pour le laser
            Vector2 targeting = ownerChar.aim;
            mousePos = Camera.main.WorldToScreenPoint(socket.transform.position) + new Vector3(targeting.x, targeting.y, 0) * 1000;

            socketPos = Camera.main.WorldToScreenPoint(socket.transform.position);
            mousePos.x = mousePos.x - socketPos.x;
            mousePos.y = mousePos.y - socketPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            //On adapte ensuite le tir pour qu'il soit dans l'angle visé
            temp.transform.rotation = Quaternion.Euler(0, 0, angle);

            //On rajoute un -90 parce que le truc marche pas sinon. Et on y ajoute un random qui permet de donner de la dispersion.
            temp.transform.Rotate(new Vector3(0, 0, -90+72*(i+1) + Random.Range(-maxAngle, maxAngle + 1)));
            //Enfin, on dit à la balle qui est son créateur.
            temp.GetComponent<Projectile>().Create(owner, ownerChar.lifesteal);
            temp.GetComponent<Projectile>().character = ownerChar;
            temp.GetComponent<Projectile>().character = ownerChar;

        }
        co = false;
        anim.SetTrigger("end");
    }
}
