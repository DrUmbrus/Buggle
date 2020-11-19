using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauterelleSlash : MonoBehaviour
{
    /// <summary>
    /// Marouflage pour le spell de la sauterelle
    /// Basiquement, c'est un GO invisible qu'on active pendant le jump, pour infliger des dégâts sur la zone souhaitée, sans changer la hitbox de la sauterelle.
    /// </summary>

    //Quel perso et les dégâts qui vont avec
    public Character creator;
    public int damage;
    List<Character> c = new List<Character>();
    List<Damageable> d = new List<Damageable>();

    public void SetStats(Vector2 target)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg-90);
        c.Clear();
        d.Clear();

    }

    //Quand on est en collision avec quelqu'un autre que soit, ça fait des dégâts.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject potato = collision.gameObject;

        if (potato.tag == "player")
        {
            Character ch = potato.GetComponent<Character>();
            if (ch.playerNum != creator.playerNum)
            {
                c.Add(ch);
            }

        }
        else if (potato.tag == "damageable" && potato.GetComponent<Damageable>().id != creator.playerNum)
        {
            d.Add(potato.GetComponent<Damageable>());
        }
    }

    //L'effet s'active à la fin du mouvement, sur tout objet touché. En mode trésor, ça vole des trésors aux joueurs. En mode deathmatch, ou si ça touche un objet non joueur, ça inflige des dégâts.
    public void Detonation()
    {
        foreach(Character ch in c)
        {
            if (creator.gm.mode==1)
            {
                ch.Damage(damage, creator.playerNum, false, creator.lifesteal);
            }
            else if(creator.gm.mode == 0)
            {
                if (ch.tresors.Count > 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        creator.tresors.Add(ch.tresors[0]);
                        ch.tresors.RemoveAt(0);
                    }
                }
                else if (ch.tresors.Count > 0)
                {
                    for (int i = 0; i < ch.tresors.Count; i++)
                    {
                        creator.tresors.Add(ch.tresors[0]);
                        ch.tresors.RemoveAt(0);
                    }
                }

            }
        }

        foreach(Damageable da in d)
        {
            da.Damage(damage, creator.playerNum, Vector2.zero, false, creator.lifesteal);
        }
    }
}

