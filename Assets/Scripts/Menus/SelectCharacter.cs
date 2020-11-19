using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    /// <summary>
    /// Le script des curseurs de sélection de perso
    /// </summary>
    [SerializeField]
    AudioSource movement = null, valide = null, refuse = null, active = null, desactive = null;


    [Header("UI")]
    //Le GO du texte pour l'aide à la visée.
    public GameObject text;
    //Le curseur lui même. Sert à gérer sa couleur.
    Image image;
    //La couleur de base, et la couleur grisée.
    public Color startColor, endColor;
    //Le fond sur lequel on affiche le perso sélectioné. Permet de le mettre en gris puis de le colorier.
    Image backGround;
    //Le texte en lui même de l'aide à la visée.
    public Text activeHelp;

    [Header("Technique")]
    //La position (= perso choisi)
    public int pos = 0;
    GameManager gm;
    //L'ancienne position, pour savoir si on a changé de perso.
    int oldpos;
    //Les 4 positions pour le curseur.
    public Transform[] positions;
    //Empêche de bouger à chaque frame
    public bool move = false;
    //Si on a pris son perso
    public bool selected=true;
    //Quel n° on est
    public int id;
    //L'objet qui créé le curseur.
    public SummonCursors father;

    public GameObject j;
    //Obsolète
    bool activatedInfo = false;
    

    //La mise en place des stats, par l'autre script
    public void Stats(Transform[] tran, GameObject the, SummonCursors curs, Image bg, Text hlep)
    {

        activeHelp = hlep;
        text = the;
        for (int i = 0; i < tran.Length; i++)
        {
            positions[i] = tran[i];
        }
        image = GetComponent<Image>();
        gm = FindObjectOfType<GameManager>();
        
        pos = id;
        transform.position = positions[pos].position;
        father = curs;
        //On désactive tout, tant que le joueur a pas activé son perso.
        selected = false;
        image.color = Color.clear;
            backGround = bg;
            backGround.color = Color.grey;
            father.ChangeCharac(-1, id);
            j.SetActive(false);
    }


    public void OnMove(InputValue value)
    {
        //Si on est actif, et qu'on a pas choisi
        if (!selected && image.color != Color.clear)
        {
            oldpos = pos;
            Vector2 potato = value.Get<Vector2>();
            //On change de position et ça nous met sur le perso voulu
            if (!move)
            {
                if (potato.y > 0.5f || potato.y < -0.5f)
                {
                    if (pos == 0)
                    {
                        pos = 2;
                    }
                    else if (pos == 2)
                    {
                        pos = 0;
                    }
                    else if (pos == 1)
                    {
                        pos = 3;
                    }
                    else if (pos == 3)
                    {
                        pos = 1;
                    }
                    move = true;
                    movement.Play();
                }
                if (potato.x > 0.5f)
                {
                    if (pos < 3)
                    {
                        pos++;
                    }
                    else
                    {
                        pos = 0;
                    }
                    move = true;
                    movement.Play();
                }
                else if (potato.x < -0.5f)
                {
                    if (pos > 0)
                    {
                        pos--;
                    }
                    else
                    {
                        pos = 3;
                    }
                    move = true;
                    movement.Play();
                }
            }
            //Pour pouvoir bouger de nouveau, il faut lacher le stick. A voir si ça va rester
            else if (potato.x > -0.2f && potato.x < 0.2f && potato.y < 0.2f && potato.y > -0.2f)
            {
                move = false;

            }
            //Si on a changé de position, on update le perso en fond et l'animation.
            if (pos != oldpos)
            {
                father.ChangeCharac(pos, id);
                father.UpdateCEM(pos, id);
            }
        }
        

        transform.position = positions[pos].position;
    }

    //Si on est actif et qu'on appuie sur la gachette, on active ou non l'aide à la visée.
    public void OnFire()
    {

        if (image.color != Color.clear)
        {
            if (!gm.stick[id])
            {
                gm.stick[id] = true;
                activeHelp.text = "Aiming help enabled";
            }
            else
            {
                gm.stick[id] = false;
                activeHelp.text = "Aiming help disabled";
            }
        }
    }

    //Pareil
    public void OnAltFire()
    {

        if (image.color != Color.clear)
        {
            if (!gm.stick[id])
            {
                gm.stick[id] = true;
                activeHelp.text = "Aiming help enabled";
            }
            else
            {
                gm.stick[id] = false;
                activeHelp.text = "Aiming help disabled";
            }
        }
    }

    //Obsolète
    public void OnSpell()
    {
        if (image.color != Color.clear)
        {
            if (!activatedInfo)
            {
                activatedInfo = true;
                active.Play();

            }
            else
            {
                activatedInfo = false;
                desactive.Play();
            }
            father.ActivateCEM(id);
        }
    }

    //Obsolète
    public void OnAltSpell()
    {
        if (image.color != Color.clear)
        {
            if (!activatedInfo)
            {
                activatedInfo = true;
                active.Play();

            }
            else
            {
                activatedInfo = false;
                desactive.Play();
            }
            father.ActivateCEM(id);
        }
    }

    //Appuyer sur A
    public void OnGrab()
    {
        Debug.Log("potato");
        //Si on est activé
        if(image.color!=Color.clear)
        {
            //Si on a pas sélectionné, on joue le son
            if (image.color != endColor)
            {
                valide.Play();
            }
            //On sélectionne le perso en cours, on change la couleur du curseur.
        selected = true;
        image.color = endColor;
        gm.chosenCharacters[id] = pos;

            //Ensuite, on vérifie que tout le monde a sélectionné, et qu'on a bien toutes les manettes.
        bool test = true;
            int count = 0;
        for (int i = 0; i < 4; i++)
        {
                if (father.active[i])
                {
                    count++;
                }
            if (gm.chosenCharacters[i] < 0 && father.active[i])
            {
                test = false;
            }
        }

            if (count < id + 1 || count==1)
            {
                test = false;
            }

            //Si c'est ok, on active le texte qui permet de passer à la suite.
        if (test)
        {
            text.gameObject.SetActive(true);
        }
        }

        //Si le curseur n'était pas activé, on l'active, avec tout ce qui va avec, et on le place à sa position de départ.
        else
        {
            father.VisualCEM(id);
            father.active[id] = true;
            image.color = startColor;
            backGround.color = startColor;
            activeHelp.text = "Aiming help enabled";
            pos = id;
            transform.position = positions[pos].position;
            j.SetActive(true);
            text.gameObject.SetActive(false);
            father.ChangeCharac(pos, id);
            father.press[id].SetActive(false);
            father.UpdateCEM(pos, id);
        }
    }


    public void OnBack()
    {
        //Si on est pas déjà en train de passer à la scène suivante
        if (!gm.loading && image.color!=Color.clear)
        {
            //On désélectionne son perso.
            selected = false;
            image.color = startColor;
            bool test = true;

            for (int i = 0; i < 4; i++)
            {
                if (gm.chosenCharacters[i] >= 0)
                {
                    test = false;
                }
            }
            //Si tout le monde n'a aucun perso sélectionné, on retourne à l'écran titre.
            if (test)
            {
                if (!gm.backSelection.isPlaying)
                {
                    gm.backSelection.Play();
                }
                gm.BackMenu();
            }
            else
            {
                refuse.Play();
            }

            if (gm.chosenCharacters[id] >= 0)
            {
                gm.chosenCharacters[id] = -1;
            }

            //On s'assure de désactiver le texte dans tous les cas.
            text.gameObject.SetActive(false);
        }
        

    }

    //Si on appuie sur start et que le texte est là (que tout le monde a sélectionné), on valide.
    public void OnPause()
    {
        if (text.activeSelf)
        {
            if (!gm.nextSelection.isPlaying)
            {
                gm.nextSelection.Play();
            }

            gm.CharacterSelected();

        }
    }
}
