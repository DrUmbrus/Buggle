using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEquipmentMenu : MonoBehaviour
{
    /// <summary>
    /// La petite vidéo de présentation des personnages sur le menu. A modifier pour avoir quelque chose de plus pratique à utiliser
    /// </summary>
    /// 

    public Vector3 outPos, inPos;
    float timer = 1;
    bool coming = true;

    public GameObject[] anims;

    /*public string[] names;
    public string[] descriptions;
    public string[] damage;
    public GameObject[] weapons;
    public string[] spells;

    //public Image weaponVis;
    public Text weaponDes;
    public Text spellDes;
    public Text weaponStat;
    public Text weaponName;*/

    void Update()
    {
        if (!coming)
        {
            timer -= Time.deltaTime*2;
            if (timer < 0)
            {
                timer = 0;
            }
        }
        else
        {
            timer += Time.deltaTime*2;
            if (timer > 1)
            {
                timer = 1;
            }
        }
        

        transform.localPosition = Vector3.Lerp(outPos, inPos, timer);
    }

    public void ChangeInfos(int character)
    {
        for (int i = 0; i < 4; i++)
        {
            anims[i].SetActive(false);
        }
        anims[character].SetActive(true);
        /*for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[character].SetActive(true);
       // weaponVis.sprite = weapons[character];
        weaponDes.text = descriptions[character];
        weaponStat.text = damage[character];
        spellDes.text = spells[character];
        weaponName.text = names[character];*/
    }

    public void Activate()
    {

        if (coming)
        {
            coming = false;
        }
        else
        {
            coming = true;
        }
    }
}
