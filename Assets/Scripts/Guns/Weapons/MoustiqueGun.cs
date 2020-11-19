using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoustiqueGun : Gun
{
    /// <summary>
    /// Template pour les armes de type "laser", qui fonctionnent en continu.
    /// </summary>
    
    //Est-ce qu'on est en train de tirer ?
    public bool shooty = false;
    //Le ou les tirs (si un jour il y a un multi laser)
    public List<GameObject> tir = new List<GameObject>();
    

    public override void Pattern()
    {


        //si on appuie sur la gachette sans être stun, ça lance le tir.
        if (ownerChar.shooting && !shooty && !ownerChar.stopped)
        {
            shooty = true;
            for (int i = 0; i < multiPattern; i++)
            {
                tir.Add(Instantiate(projec, muzzle.position, Quaternion.identity));
                tir[tir.Count-1].GetComponent<Projectile>().Create(owner, ownerChar.lifesteal);
            }
            
        }
        //Si on lache la gachette, ou qu'on se fait stop (spell, ulti, ou mort), ou qu'on meurt, le tir s'arrête
        else if (!ownerChar.shooting || ownerChar.stopped || ownerChar.life <= 0)
        {
            shooty = false;
            if (tir.Count>0)
            {
                for (int i = 0; i <tir.Count; i++)
                {
                    tir[i].GetComponent<RayonMoustique>().EndThis();
                }
                tir.Clear();
            }
        }
    }

}
