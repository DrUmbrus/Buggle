using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SelectSetup : MonoBehaviour
{
    /// <summary>
    /// Script permettant de choisir son niveau
    /// </summary>

    //Les audios
    [SerializeField]
    AudioSource movement = null, valide = null, back = null, activate = null, deactivate = null, changeValue = null;

    /// <summary>
    /// Les aspects technique
    /// </summary>
    [Header("Technique")]
    //Empêche de bouger à chaque frame
    public bool move = false;
    //Le mouvement du stick
    Vector2 potato = Vector2.zero;
    GameManager gm = null;
    //A quelle étape de la sélection on est
    public int pos = 0;

    //Les noms des modes
    [SerializeField]
    string[] names = new string[0];
    //Les deux flèches quand on change de niveau, avec une petite animation.
    [SerializeField]
    Animator flecheG = null, flecheD = null;

    /// <summary>
    /// Les aspects visibles
    /// </summary>
    [Header("UI")]
    public Text nameMode = null;
    //Le nombre de kills à faire, version texte.
    public Text target = null;
    //Le temps de jeu, en texte
    public Text timing = null;
    public Text itemsText = null;
    //Le curseur autour de la sélection de niveau
    [SerializeField]
    Image curseurSeelction = null;
    //La composante image du curseur de sélection de mode
    public Image CurseurColored = null;

    [Header("Valeurs")]
    
    //Sur quelle map
    int levelSelected = 0;



    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();

        //Si c'est la première partie depuis le lancement du jeu, on met ces valeurs par défaut. Sinon on garde les dernières.
        if (gm.firstgame)
        {
            gm.targetScore = 5;
            gm.startTime = 90;
        }

        //On est par défaut en deathmatch, de manière arbitraire.
        gm.mode = 0;
        target.text = "Target kills : " + gm.targetScore.ToString();
        target.color = Color.white;
        timing.color = Color.white;
        timing.text = "Play Time : " + gm.startTime.ToString() + "s";


    }

    public void OnMove(InputValue value)
    {
        potato = value.Get<Vector2>();
        if (potato.x > -0.2f && potato.x < 0.2f)
        {
            move = false;

        }
    }

    //Là ça devient spaghetti.
    private void Update()
    {
        //Si on est pas en train de choisir le mode, le curseur de mode est vert. Sinon il est rouge.
        if (pos != 0)
        {
            CurseurColored.color = Color.green;
        }
        else
        {
            CurseurColored.color = Color.red;
        }
        
        //Si on peut bouger
        if (!move)
        {
            //Vers la droite
            if (potato.x > 0.5f)
            {
                //Sur la sélection des modes, ça passe de deathmatch à tresor, et inversement. Note : à modifier pour ajouter d'autres modes.
                if (pos == 0)
                {
                    movement.Play();
                    flecheD.SetTrigger("d");
                    if (gm.mode == gm.maxMode)
                    {
                        gm.mode = 0;
                    }
                    else
                    {
                        gm.mode++;
                    }

                    
                }
                //Changer le score
                else if (pos == 1)
                {
                    if (gm.mode == 0)
                    {
                        
                    }
                    else if (gm.mode == 1)
                    {
                        gm.targetScore++;
                        changeValue.Play();
                    }
                    else if (gm.mode == 2 || gm.mode == 3)
                    {
                        gm.targetLast++;
                        changeValue.Play();
                    }
                    
                }
                //Changer le temps
                else if (pos == 2)
                {
                    gm.startTime += 10;
                    
                    changeValue.Play();
                }

                //Si on est pas en train de modifier les valeurs, on utilise la coroutine MoveAgain (0.4s entre deux mouvements). Sinon c'est MoveAgainInTime (0.1s entre deux mouvements)
                if (pos < 2)
                {
                    StartCoroutine(MoveAgain());
                }
                else
                {
                    StartCoroutine(MoveAgainInTime());

                }
                //On doit attendre la fin de la coroutine pour bouger de nouveau.
                move = true;
            }

            //Mêmes principes vers la gauche
            else if (potato.x < -0.5f)
            {
                //Sur la sélection des modes, ça passe de deathmatch à tresor, et inversement. Note : à modifier pour ajouter d'autres modes.
                if (pos == 0)
                {
                    movement.Play();
                    flecheG.SetTrigger("g");
                    if (gm.mode == 0)
                    {
                        gm.mode = gm.maxMode;
                    }
                    else
                    {
                        gm.mode--;
                    }


                }
                //Changer le score
                else if (pos == 1)
                {
                    if (gm.mode == 0)
                    {

                    }
                    else if (gm.mode == 1)
                    {
                        if (gm.targetScore > 0)
                        {
                            gm.targetScore--;
                        }
                        
                        changeValue.Play();
                    }
                    else if (gm.mode == 2 || gm.mode == 3)
                    {
                        if (gm.targetLast > 1)
                        {
                            gm.targetLast--;
                        }
                        
                        changeValue.Play();
                    }

                }
                //Changer le temps
                else if (pos == 2)
                {
                    gm.startTime -= 10;

                    changeValue.Play();
                }

                //Si on est pas en train de modifier les valeurs, on utilise la coroutine MoveAgain (0.4s entre deux mouvements). Sinon c'est MoveAgainInTime (0.1s entre deux mouvements)
                if (pos < 2)
                {
                    StartCoroutine(MoveAgain());
                }
                else
                {
                    StartCoroutine(MoveAgainInTime());

                }
                //On doit attendre la fin de la coroutine pour bouger de nouveau.
                move = true;
            }
        }


        //Les textes d'explication
        if (gm.mode == 0)
        {
            target.text = "No target score";
            timing.text = "Play Time : " + gm.startTime.ToString() + " s";
        }
        else if (gm.mode == 1)
        {
            if (gm.targetScore > 0)
            {
                target.text = "Target kills : " + gm.targetScore.ToString();
            }
            else
            {
                target.text = "Target kills : no limit";
            }
            
            timing.text = "Play Time : " + gm.startTime.ToString() + " s";
        }
        else if(gm.mode==2||gm.mode==3)
        {
            target.text = "Target points : " + gm.targetLast.ToString();
            timing.text = "Play Time : " + gm.startTime.ToString() + " s";
        }

        nameMode.text = names[gm.mode];
    }



    
    //Quand on fait start
    public void OnPause()
    {
        //On dit au gm quel niveau on veut, et avec quel mode.
        gm.nextLevel = levelSelected;
        
        if (!gm.nextSelection.isPlaying)
        {
            gm.nextSelection.Play();
        }
        //Et on lance la scène.
        gm.OptionSelected();
    }

    //Quand on valide
    public void OnGrab()
    {
        //Si on valide le nombre de kills ou le mode, on passe simplement à l'étape suivante.
        if (pos < 4)
        {
            valide.Play();
            pos++;
        }
        //Mais si on valide le niveau
        else
        {
            //gm.ChangeItems();
        }

        //Si on passe sur la gestion des kills ou du temps, ce texte passe en vert. Le(s) texte(s) inutilisé(s) est en blanc.
        if (pos == 1)
        {

            target.color = Color.green;
            timing.color = Color.white;
        }
        else if (pos == 2)
        {
            target.color = Color.white;
            timing.color = Color.green;
        }
        else
        {
            target.color = Color.white;
            timing.color = Color.white;
        }

        //La sélection de niveau est en blanc au début, puis rouge quand on est dessus, puis vert quand confirmé.
        if (pos ==0)
        {
            curseurSeelction.color = Color.red;
        }
        else
        {
            curseurSeelction.color = Color.green;
        }

        if (pos == 3)
        {
            itemsText.color = Color.green;
        }
        else
        {
            itemsText.color = Color.white;
        }
    }

    //Pareil dans l'autre sens
    public void OnBack()
    {
        if (pos == 0)
        {
            gm.ButtonBattle(0);
        }
        else
        {

                back.Play();
                pos--;
        }

        if (pos == 1)
        {
            target.color = Color.green;
            timing.color = Color.white;
        }
        else if (pos == 2)
        {
            target.color = Color.white;
            timing.color = Color.green;
        }
        else
        {
            target.color = Color.white;
            timing.color = Color.white;
        }

        if (pos ==0)
        {
            curseurSeelction.color = Color.red;
        }
        else
        {
            curseurSeelction.color = Color.green;
        }

        if (pos == 3)
        {
            itemsText.color = Color.green;
        }
        else
        {
            itemsText.color = Color.white;
        }
    }

    //Le temps entre deux mouvements.
    IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(0.4f);
        move = false;
    }

    IEnumerator MoveAgainInTime()
    {
        yield return new WaitForSeconds(0.1f);
        move = false;
    }

    
}
