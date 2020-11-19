using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    /// <summary>
    /// Préparation de l'ulti coccinelle
    /// </summary>
    
    [SerializeField]
    GameObject summon = null;
    float timer=0.75f;

    
    public void Summoning(int owner)
    {
        StartCoroutine(Summon(owner));
    }

    //C'est simplement un timer avec la cible, puis on lance les dégâts.
    IEnumerator Summon(int owner)
    {
        yield return new WaitForSeconds(timer);
        GameObject temp = Instantiate(summon, transform.position, transform.rotation);
        temp.GetComponent<Projectile>().Create(owner, false);
        Destroy(gameObject);
    }

}
