using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonCursors : MonoBehaviour
{
    /// <summary>
    /// Le script chargé de créer les 4 curseurs de sélection, et de la gestion à un niveau supérieur.
    /// </summary>
   
    //Respectivement les positions pour le J1/2/3/4. Note : Faudrait peut être mettre des parents aux curseurs et avoir une position central, c'est un peu weird comme strat et ça compliqué pour rien.
    public Transform[] positions0;
    public Transform[] positions1;
    public Transform[] positions2;
    public Transform[] positions3;
    //Les 4 GO des curseurs
    public GameObject[] prefabs;
    //Le texte de validation de la scène
    public GameObject the;
    //Le canvas, pour pouvoir le mettre en parent des curseurs. (note : c'est un autre canvas que celui de l'UI, pour éviter l'overlap avec le texte.
    public Canvas can;
    //Les scripts qui gèrent l'image du perso en fond
    public CharacterMenu[] select;
    //Les sprites des personnages.
    public Sprite[] characters;
    //Le fond coloré
    public Image[] backgrounds;
    //"Press A to start"
    public GameObject[] press;
    //Le script qui gère les infos sur le perso.
    public CharacterEquipmentMenu[] cem;
    //Le nom du perso
    public Text[] nameText;
    //La liste des noms
    public string[] names;
    //Le texte pour savoir si on a mis l'aide à la visée
    public Text[] activateHelp;
    //Est-ce que ce joueur est activé.
    public bool[] active= new bool[4];

    private void Awake()
    {
        StartCoroutine(Create());
    }


    IEnumerator Create()
    {
        //On attend une frame, pour être sûr que tout soit en place.
        yield return 0;

        //On créé les 4 curseurs, avant de désactiver leur écran d'aide.
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject temp = Instantiate(prefabs[i], transform.position, Quaternion.identity, can.transform);
            if (i == 0)
            {
                //Cette fonction donne leur liste de positions, le texte, le script SummonCursors, le background, et le texte d'aide à la visée.)
                temp.GetComponent<SelectCharacter>().Stats(positions0, the, this, backgrounds[i], activateHelp[i]);
            }
            else if (i == 1)
            {
                temp.GetComponent<SelectCharacter>().Stats(positions1, the, this, backgrounds[i], activateHelp[i]);
            }
            else if (i == 2)
            {
                temp.GetComponent<SelectCharacter>().Stats(positions2, the, this, backgrounds[i], activateHelp[i]);
            }
            else if (i == 3)
            {
                temp.GetComponent<SelectCharacter>().Stats(positions3, the, this, backgrounds[i], activateHelp[i]);
            }
            cem[i].gameObject.SetActive(false);
            yield return 0;
        }
    }

    //Appelé par SelectCharacter. Indique quel est le nouveau perso de ce joueur, et son id.
    public void ChangeCharac(int sprite, int which)
    {
        //On met à jour le sprite et le nom du perso.
        if (sprite>= 0)
        {
            select[which].ChangeSprite(characters[sprite]);
            nameText[which].text = names[sprite];
        }
        //Une option pour si on veut pas de perso (ex avant d'avoir activé le joueur)
        else
        {
            select[which].ChangeSprite(null);
            nameText[which].text = "";
        }
        
    }

    //obsolète
    public void KillVisual(int id)
    {
        select[id].gameObject.SetActive(false);
    }

    //Mettre à jour les infos sur le perso
    public void UpdateCEM(int cha, int id)
    {
        cem[id].ChangeInfos(cha);
    }

    //Obsolète
    public void ActivateCEM(int id)
    {
        //cem[id].Activate();
    }

    //Activer les infos (quand on active le joueur)
    public void VisualCEM(int id)
    {
        cem[id].gameObject.SetActive(true);
    }
}
