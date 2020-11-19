using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniSlash : MonoBehaviour
{
    /// <summary>
    /// Ulti de la sauterelle
    /// </summary>
    
    //Le créateur (pour ne pas se target soi même)
    public int avoidN;
    //La cible
    Character targetChar;
    //Le line renderer. C'est ça qui donne le visuel du slash
    LineRenderer lr;
    //Le vecteur de visée
    Vector2 targetV=Vector2.zero;
    //Les stats
    public int damage;
    public int force;
    //La couleur
    public Gradient grad;
    GameManager gm;
    BoxCollider2D box;


    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        lr = GetComponent<LineRenderer>();
    }

    //Mise en place des stats
    public void SetStats(int avoid, Vector3 pos, GameManager gam)
    {
        //On lui dit qui est le gm, on le met sur sa position de départ, et on lui dit qui ne pas viser
        gm = gam;
        transform.position = pos;
        avoidN = avoid;


        //Ensuite, on fait une liste de int, de 0 à 3
        int test = 0;
        List<int> inting = new List<int>();
        
        for (int i = 0; i < 4; i++)
        {
            inting.Add(i);
        }

        //On retire de la liste le lanceur de l'ulti, et tous les joueurs non présents.
        for (int i = 0; i < 4; i++)
        {
            if(i==avoid || gm.chosenCharacters[i] == -1)
            {
                inting.Remove(i);
            }
        }

        //Ensuite on choisit un chiffre au hasard parmi ceux qui restent, et on choppe sur le game manager le perso qui correspond à ce chiffre
        test = Random.Range(0, inting.Count);
        targetChar = gm.team[inting[test]];

        
        //Et on lance la coroutine de visée
        StartCoroutine(Targeting());


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var potato = collision.gameObject;
        if (potato.tag == "player")
        {
            if (potato.GetComponent<Character>().playerNum != avoidN)
            {
                potato.GetComponent<Character>().Damage(damage, avoidN, true, false);
            }

        }
        else if (potato.tag == "damageable")
        {
            if (potato.GetComponent<Damageable>().id != avoidN)
            {
                Vector2 send = potato.transform.position - transform.position;
                send.Normalize();
                send *= force;
                potato.GetComponent<Damageable>().Damage(damage, avoidN, send, true, false);
            }

        }
    }

    private void Update()
    {
        //On met en place le line renderer entre sa position de départ, et un rayon de taille 50 vers la cible. Note perso : Voir pour adapter la taille à celle de la map. Note 2 : peut être mettre des points intermédiaire si on veut faire des changements de couleur
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + targetV * 50);


    }

    

    //Coroutine de visée
    IEnumerator Targeting()
    {
        //On met en place les stats du line renderer et on fait un vecteur entre le slash et la cible
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        targetV = targetChar.transform.position - transform.position;
        targetV.Normalize();

        //Même principe que le script de gun, on détermine un angle selon ce vecteur, de manière à tourner les points de départ des raycast dans le bon sens
        var mousePos = (Vector2)Camera.main.WorldToScreenPoint(transform.position) + targetV * 1000;

        var socketPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - socketPos.x;
        mousePos.y = mousePos.y - socketPos.y;
        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);

        //Après une seconde, le rayon devient de taille normale, change de couleur et active son mode slash
        yield return new WaitForSeconds(1f);
        lr.startWidth = 1f;
        lr.endWidth = 1f;
        lr.colorGradient = grad;
        box.enabled = true;
        GetComponent<AudioSource>().Play();
        //Le slash existe pendant 0.55s puis se détruit
        yield return new WaitForSeconds(0.55f);
        Destroy(gameObject);
    }
}
