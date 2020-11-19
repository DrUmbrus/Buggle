using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    /// <summary>
    /// Les téléporteurs de la map téléporteur
    /// </summary>
    
    //Le téléporteur associé.
    public Teleporter partner = null;
    //La liste des objets qui ont traversé le tp récemment.
    public List<GameObject> traverse= new List<GameObject>();
    //L'animation
    public Animator anim = null;
    //Le canvas pour les instruction.
    [SerializeField]
    GameObject canvas = null;

    //Des particle system pour un effet sur la téléportation. Actuellement désactivés le temps de les améliorer.
    public ParticleSystem inside1 = null, inside2 = null, outside1 = null, outside2 = null;

    //Les joueurs qui sont sur le tp.
    List<GameObject> top = new List<GameObject>();

    private void Awake()
    {
        anim = GetComponent<Animator>();
        inside1.Stop();
        outside1.Stop();
        inside2.Stop();
        outside2.Stop();
    }

    //Faire apparaître le canvas d'instruction quand un joueur arrive.
    public void ShowCan(GameObject who)
    {
        canvas.SetActive(true);
        top.Add(who);
    }

    //Quand un joueur sort de la hitbox, s'il n'y a plus personne, on désactive le canvas.
    public void HideCan(GameObject who)
    {
        top.Remove(who);
        if (top.Count == 0)
        {
            canvas.SetActive(false);
        }
    }


    //Quand on active le tp, on s'ajoute à la liste, on change de position, et on active l'animation des deux téléporteurs.
    public void TPMe(GameObject col)
    {
        bool test = false;

        if (traverse.Count > 0)
        {

            for (int i = 0; i < traverse.Count; i++)
            {
                if (traverse[i] == col)
                {
                    test = true;
                }
            }
        }
        if (!test)
        {
            partner.traverse.Add(col);
            traverse.Add(col);
            StartCoroutine(WorkAgain(col));
            col.transform.position = new Vector3(partner.transform.position.x, partner.transform.position.y, col.transform.position.z);
            anim.SetTrigger("tp");
            partner.anim.SetTrigger("tp");
        }
    }

    //Coroutine de cooldown individuel du tp, pour éviter de pouvoir les utiliser en boucle.
    IEnumerator WorkAgain(GameObject target)
    {
        yield return new WaitForSeconds(1f);
        traverse.Remove(target);
        partner.traverse.Remove(target);
    }
}
