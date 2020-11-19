using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    /// <summary>
    /// Changer l'affichage du perso
    /// </summary>

        //L'image d'UI
    Image perso;
    //Le nouveau sprite
    public Sprite nextCharacter=null;
    //Les deux positions
    public Vector3 posTop, postBot;
    //Le timer de lerp
    public float timer=0;

    //On met en place les variables.
    private void Awake()
    {
        perso = GetComponent<Image>();
        posTop = transform.position;
        postBot = new Vector3(transform.position.x, transform.position.y - 850, transform.position.z);
        transform.position = postBot;
        nextCharacter = perso.sprite;
        perso.color = Color.clear;
    }

    
    private void FixedUpdate()
    {

        //On fait bouger le lerp vers le bas si on veut changer de sprite, et vers le haut sinon.
        if (perso.sprite != nextCharacter  || perso.sprite==null)
        {
            timer -= Time.deltaTime * 2.5f;
        }
        else
        {
            timer += Time.deltaTime * 2.5f;
        }

        //Le timer est clampé
        if (timer > 1)
        {
            timer = 1;
        }
        if (timer < 0)
        {
            timer = 0;
        }


        transform.position = Vector3.Lerp(postBot, posTop, timer);

        //Quand on arrive tout en bas, on change de sprite, ce qui fait repartir en haut.
        if (timer <= 0)
        {
            perso.sprite = nextCharacter;
            //S'il n'y a pas de sprite, on met une couleur transparente, pour ne pas avoir un carré blanc moche.
            if (nextCharacter == null)
            {
                perso.color = Color.clear;

            }
            else
            {
                perso.color = Color.white;
            }
        }
    }

    //La fonction pour annoncer le nouveau sprite.
    public void ChangeSprite(Sprite next)
    {
        nextCharacter = next;

    }
}
