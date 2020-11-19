using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : Gun
{
    //Une arme qui tire trois projectiles en cone. Techniquement identique au Coutal, mais avait un 4ème tir sur le barycentre du joueur et des 3 tirs avant.

    GameManager gm;
    List<Vector3> targets= new List<Vector3>();
    public GameObject explosion;
    public LineRenderer[] side;
    public Transform[] sidePos;


    public override void Grabbed2()
    {
        gm = FindObjectOfType<GameManager>();
        foreach (LineRenderer line in side)
        {
            line.startColor = lines[owner];
            line.endColor = lines[owner];
        }
    }


    public override void ShootingPattern()
    {

        targets.Clear();
        //Le système de visée en se basant sur le joueur
        Vector2 targeting = ownerChar.aim;
        mousePos = Camera.main.WorldToScreenPoint(socket.transform.position) + new Vector3(targeting.x, targeting.y, 0) * 1000;

        socketPos = Camera.main.WorldToScreenPoint(socket.transform.position);
        mousePos.x = mousePos.x - socketPos.x;
        mousePos.y = mousePos.y - socketPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        GameObject[] temps = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            //On créé l'objet
            temps[i] = Instantiate(projec, muzzle.position, Quaternion.identity);
            
            //On adapte ensuite le tir pour qu'il soit dans l'angle visé
            temps[i].transform.rotation = Quaternion.Euler(0, 0, angle);

            float anguleux = -45 + 45 * i+Random.Range(-maxAngle, maxAngle+1);
            //On rajoute un -90 parce que le truc marche pas sinon. Et on y ajoute un random qui permet de donner de la dispersion.
            temps[i].transform.Rotate(new Vector3(0, 0, anguleux));
            temps[i].GetComponent<TridentRock>().SetTrident(anguleux + angle);
            temps[i].GetComponent<Projectile>().Create(owner, ownerChar.lifesteal);
            temps[i].GetComponent<Projectile>().character = ownerChar;


        }

        
    }


    


}
