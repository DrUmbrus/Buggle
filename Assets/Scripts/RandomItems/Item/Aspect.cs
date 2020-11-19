using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspect : MonoBehaviour
{
    
    /// <summary>
/// Classe générale pour les objets au sol. Toutes les stats ne sont pas forcément utile.
/// </summary>

    //Le personnage concerné
    public Character cha;
    //La disparition de l'objet (notamment utile pour un son de destruction)
    public bool dying = false;
    //Le temps utilisé pour le lerp de disparition en fin d'effet. A voir pour faire mieux.
    public float timer = 1;
    //La couleur claire et opaque de l'objet.
    public Color cle, op;
    //Le numéro du joueur, pour quand on fait un kill.
    public int num;
    //Le sr de l'aspect
    public SpriteRenderer sr;
    //Si oui, l'objet suit son propriétaire. Mettre non pour les objets à effet instantané.
    public bool sticky = true;

    //Mise en place des stats, autour du personnage qui ramasse l'objet.
    public void SetStats(Character cara)
    {
        cha = cara;
        num = cha.playerNum;
        sr = GetComponent<SpriteRenderer>();
        cle = cha.playerCol[num];
        op = new Color(cle.r, cle.g, cle.b, 0);
        Stat2();
    }

    //Si des infos supplémentaires sont nécessaires
    public virtual void Stat2()
    {

    }

    public void Update()
    {
        //Si on est sticky, on suit le joueur.
        if (sticky)
        {
            transform.position = new Vector3(cha.transform.position.x, cha.transform.position.y, cha.transform.position.z - 0.1f);
        }
        
        Pattern();
    }

    //Update spécifique.
    public virtual void Pattern()
    {

    }
}
