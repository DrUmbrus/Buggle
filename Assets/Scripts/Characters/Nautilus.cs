using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nautilus : Character
{

    /// <summary>
    /// Le perso à grab. WIP
    /// </summary>

    //Son projectile en prefab et en version invoquée.
    public GameObject anchor;
    GameObject summonedAnchor;

    //Si son grab est en cours, il ne peut pas bouger.
    public override void Pattern()
    {
        if (summonedAnchor != null)
        {
            stopped = true;
        }
    }

    //Son spell invoque un projectile tiré en ligne droite, qui grab.
    public override void Competence()
    {
        summonedAnchor = Instantiate(anchor, transform.position, Quaternion.identity);
        summonedAnchor.transform.rotation = Quaternion.Euler(0, 0, angle);
        summonedAnchor.transform.Rotate(new Vector3(0, 0, -90));
        summonedAnchor.GetComponent<Projectile>().Create(playerNum, false);
    }

    //Fin du spell, détruit le projectile et permet de reprendre le jeu.
    public void EndAnchor()
    {
        summonedAnchor = null;
        stopped = false;
    }
}
