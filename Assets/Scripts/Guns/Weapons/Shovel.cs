using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : Gun
{
    /// <summary>
    /// La pelle. Créé une onde de choc au cac et envoie des cailloux dans des angles aléatoires.
    /// </summary>
    
    //Les extrêmes de vitesse pour les cailloux.
    [SerializeField]
    float minSpeed=0, maxSpeed=0;
    [SerializeField]
    GameObject rock=null;
   // [SerializeField]
    //float angle1=-90, angle2=-90;

        //Le tir "secondaire"
    public override void ShootingPattern()
    {

        //Des projectiles normaux, mais on leur donne une vitesse aléatoire, et on a un gros angle de déviation.
        for (int i = 0; i < 3; i++)
        {
            GameObject temp = Instantiate(rock, muzzle.position, Quaternion.identity);

            //On réutilise le même système que pour le laser
            Vector2 targeting = ownerChar.aim;
            mousePos = Camera.main.WorldToScreenPoint(socket.transform.position) + new Vector3(targeting.x, targeting.y, 0) * 1000;

            socketPos = Camera.main.WorldToScreenPoint(socket.transform.position);
            mousePos.x = mousePos.x - socketPos.x;
            mousePos.y = mousePos.y - socketPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            //On adapte ensuite le tir pour qu'il soit dans l'angle visé
            temp.transform.rotation = Quaternion.Euler(0, 0, angle);
            float targetAngle = Random.Range(-maxAngle,maxAngle+1);

            //On rajoute un -90 parce que le truc marche pas sinon. Et on y ajoute un random qui permet de donner de la dispersion.
            temp.transform.Rotate(new Vector3(0, 0, -90 +targetAngle));
            temp.GetComponent<Projectile>().Create(owner, ownerChar.lifesteal);
            temp.GetComponent<Projectile>().speed=Random.Range(minSpeed, maxSpeed+1);

        }

    }
}
