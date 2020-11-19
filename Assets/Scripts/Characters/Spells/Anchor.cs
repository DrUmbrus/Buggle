using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sort pour un personnage à grab, dans le futur. wip.
public class Anchor : Projectile
{
    //Le temps de stun si
    public float stunTime = 0;
    //Où on en est du mouvement
    public float lerpTime = 0;
    //Qui a créé et reçu l'ancre.
    Transform creator = null;
    Transform victime = null;
    //Est-ce qu'on a touché quelque chose, et est-ce que c'est un joueur ?
    bool grabbed = false, grabChar = false;
    //Le point de grab.
    Vector2 grabPos = Vector2.zero;
    //La position du perso et de la victime.
    Vector2 creatorStart = Vector2.zero, victimeStart = Vector2.zero;

    public override void Pattern()
    {
        //C'est un projectile tant que rien n'a été touché/
        if (!grabbed)
        {
            transform.Translate((Vector3.up * speed * Time.deltaTime), Space.Self);
        }
        //Sinon, un lerp déplace le joueur, et le joueur touché, le cas échéant.
        else
        {
            lerpTime += Time.deltaTime * 2;

            creator.position = Vector2.Lerp(creatorStart, grabPos, lerpTime);

            if (grabChar)
            {
                victime.position = Vector2.Lerp(victimeStart, grabPos, lerpTime);
            }

            if (lerpTime >= 1)
            {
                KillMe();
            }
        }
    }

    //Mettre fin au spell.
    void KillMe()
    {
        creator.GetComponent<Nautilus>().EndAnchor();
        Destroy(gameObject);
    }

    //Si on touche quelque chose qui n'est pas un joueur (ex : un mur), on prend le point d'accroche et on bougera que le joueur.
    public override void Touch(Collider2D collision)
    {
        grabbed = true;
        creatorStart = creator.position;
        grabPos = transform.position;
    }

    //Si on touche un autre joueur, les deux seront déplacés pour se rejoindre sur la position centrale.
    public override void HitPlayer(Collider2D collision)
    {
        grabbed = true;
        creatorStart = creator.position;
        
        grabChar = true;
        victime = collision.transform;
        victime.GetComponent<Character>().Stunned(stunTime);
        victimeStart = victime.transform.position;

        grabPos = new Vector2((creatorStart.x+victimeStart.x)/2, (creatorStart.y + victimeStart.y) / 2);
    }
}
