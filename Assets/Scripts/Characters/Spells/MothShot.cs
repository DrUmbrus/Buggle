using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nouveau sort de la mite. 4 boules qui tournent autour d'elle.
public class MothShot : MonoBehaviour
{
    //La vitesse de mouvement
    public float speed=0;
    //L'objet qui tourne
    public GameObject ball = null;
    //L'id du joueur.
    public int id = -1;
    //Les dégâts infligés.
    public int damage = 2;
    //Le parent.
    public Transform par = null;

    //Création des 4 boules.
    public void Shot(float angle)
    {
        GameObject temp=Instantiate(ball, transform.position, Quaternion.identity);
        temp.transform.rotation = Quaternion.Euler(0,0,angle - 90);
        temp.GetComponent<Projectile>().Create(id, false);
    }

    //Le parent est placé sur le joueur, et tourne pour déplacer les boules qui sont en enfant.
    private void Update()
    {
        par.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    //Si elles touchent un joueur ou un bojet qui reçoit des dégâts, on inflige lesdits dégâts.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Character c = collision.GetComponent<Character>();
            if (c.playerNum != id)
            {
                c.Damage(damage, id, false, false);
            }
        }
        else if (collision.tag == "damageable")
        {
            Damageable d = collision.GetComponent<Damageable>();
            if (d.id != id)
            {
                d.Damage(damage, id, Vector2.zero, false, false);
            }
        }
    }
}
