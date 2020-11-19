using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    /// <summary>
    /// L'écran des résultats. Probablement à refaire.
    /// </summary>


    GameManager gm;

    //Les scores en texte
    public Text[] scoresText;
    //Les 4 personnages à placer
    public Image[] characters;
    //Les trois récompenses + la pierre tombale
    public GameObject[] trophees;

    //Les sprites vivant et morts
    public Sprite[] living;
    //Les 4 couleurs de fonts
    public Color[] fonts;
    //Le classement, sous forme d'ints
    public int[] classement= new int[4];

    public int[] scoresInt = new int[4];
    //Les flèches et le texte associés aux persos
    public Image[] fleches;
    public Text[] number;
    //Le bool pour éviter de revenir au menu trop vite.
    bool canBack = false;
    //Les scores
    public List<int> scores;

    private void Awake()
    {
        gm=FindObjectOfType<GameManager>();
        //On lance l'attente pour le retour au menu.
        StartCoroutine(WaitBack());

        //On récupère les scores du game manager.
        for (int i = 0; i < 4; i++)
        {
            scoresInt[i] = gm.scores[i];
        }

        //On lance l'organisation des scores
        OrderResults();

        //Ensuite on met en place le visuel
        for(int i=0;i<4; i++)
        {
            if (scoresInt[i] != -1)
            {
                //On active autant de personnages et récompenses qu'il y a de joueurs
                characters[i].gameObject.SetActive(true);
                trophees[i].SetActive(true);
                scoresText[i].gameObject.SetActive(true);

                characters[i].sprite = living[gm.chosenCharacters[classement[i]]];
                fleches[i].gameObject.SetActive(true);
                

                //On met les scores dans la couleur du joueur associé et on update le texte
                scoresText[i].color = fonts[classement[i]];
                scoresText[i].text = scoresInt[classement[i]].ToString() + " POINTS";
                number[i].color = fonts[classement[i]];
                number[i].text = "J" + (classement[i]+1).ToString();
                fleches[i].color = fonts[classement[i]];
            }
            
        }

        
    }

    private void Update()
    {
        //Retour au menu
        
        if(Input.anyKeyDown)
        {  if (canBack)
            {
                gm.BackMenu();
            }
        }
    }

    //Mettre les scores dans l'ordre pour classer les joueurs
    //Note : Ne gère pas les égalités actuellement. Pas d'idée sur comment régler ça
    public void OrderResults()
    {
        //Un tableau où ranger ça pour l'instant
            int[] scoresTemp = new int[4];

            for (int i = 0; i < 4; i++)
            {
                scoresTemp[i] = -1;
                classement[i] = -1;
            }


            //Pour le nombre de joueurs => taille du classement
            for (int i = 0; i < 4; i++)
            {
                //Vérifier chaque joueur
                for (int j = 0; j < 4; j++)
                {
                if (gm.chosenCharacters[j] != -1 && gm.chosenCharacters[i]!=-1)
                {

                    //Si le score du joueur est plus haut que le score actuel
                    if (scoresInt[j] > scoresTemp[i])
                    {
                        bool test = true;
                        //On vérifie le classement
                        for (int k = 0; k < classement.Length; k++)
                        {
                            //Si aucun point du classement n'est déjà pris par ce joueur
                            if (classement[k] == j)
                            {
                                test = false;
                            }
                        }
                        //On ajoute le score du joueur.
                        if (test)
                        {
                            scoresTemp[i] = scoresInt[j];
                            classement[i] = j;
                        }

                    }
                }
                else if(gm.chosenCharacters[j]==-1)
                {
                    bool last = false;
                    for (int o = 3; o >0; o--)
                    {
                        if(!last && classement[o] == -1)
                        {
                            classement[o] = j;
                            last = true;
                        }
                    }
                }
                
                }
                
            }

            


    }

    

    //5 secondes de pause avant de pouvoir revenir au menu.
    IEnumerator WaitBack()
    {
        yield return new WaitForSeconds(5);
        canBack = true;
    }
}
