using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectBoss : MonoBehaviour
{
    /// <summary>
    /// Script permettant de choisir son niveau
    /// </summary>

    //Les audios
    [SerializeField]
    AudioSource movement=null, activate = null, deactivate = null;

    /// <summary>
    /// Les aspects technique
    /// </summary>
    [Header("Technique")]
    //Empêche de bouger à chaque frame
    public bool move = false;
    //Le mouvement du stick
    Vector2 potato = Vector2.zero;
    GameManager gm = null;
    
    //Les curseurs et l'objet pour les infos
    [SerializeField]
    GameObject  masterCard = null;
    //Les images utilisées pour indiquer chaque map
    [SerializeField]
    Sprite[] levels= new Sprite[0];
    [SerializeField]
    Sprite[] sumoLevels = new Sprite[0];
    //L'image qui sert à afficher le niveau sélectionné.
    [SerializeField]
    Image showLevel = null;
    //Les noms des niveaux
    [SerializeField]
    string[] names= new string[0];
    [SerializeField]
    string[] sumoNames = new string[0];
    //Les deux flèches quand on change de niveau, avec une petite animation.
    [SerializeField]
    Animator flecheG = null, flecheD = null;
    
    /// <summary>
    /// Les aspects visibles
    /// </summary>
    [Header("UI")]    
    //Le nom du niveau choisi.
    public Text levelName = null;
   

    [Header("Valeurs")]
    //Sur quelle map
    int levelSelected = 0;
    
    [Header("Infos")]
    //Les GO qui contiennent les infos sur le mode et la map.
    [SerializeField]
    GameObject[] cards= new GameObject[0];

    [SerializeField]
    GameObject[] sumoCards= new GameObject[0];
    //Les deux positions pour lerp les cartes d'information.
    [SerializeField]
    Vector3 outPos= Vector3.zero, inPos=Vector3.zero;
    //Savoir si on est en train de montrer l'info ou pas.
    bool showing = false;
    //Le temps pour le lerp
    float timer = 0;
    //Quelle carte d'info on veut.
    int activeCard=0;


    private void Awake()
    {        
        gm = FindObjectOfType<GameManager>();
        if (gm.mode == 2)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < sumoCards.Length; i++)
            {
                sumoCards[i].SetActive(false);
            }
        }
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
        

        //Si on active la carte d'info, elle va sur sa position active, sinon elle s'éloigne.
        if (showing)
        {
            timer += Time.deltaTime*3;
        }
        else
        {
            timer -= Time.deltaTime*3;
        }

        if (gm.mode == 2)
        {
            activeCard = levelSelected;

            //On active celle qui est sélectionnée et pas les autres. (note : c'est une solution très sale, faudrait plutôt en faire une fonction à activer, mais on verra à la refonte.
            for (int i = 0; i < sumoCards.Length; i++)
            {
                if (i != activeCard)
                {
                    sumoCards[i].SetActive(false);
                }
                else
                {
                    sumoCards[i].SetActive(true);
                }

            }

            //On met les infos sur le niveau choisi.
            showLevel.sprite = sumoLevels[levelSelected];
            levelName.text = sumoNames[levelSelected];
        }
        else
        {
            //Basiquement, la liste des cartes est divisée en trois, pour les modes trésor, deathmatch, et golden
            if (gm.mode == 1)
            {
                activeCard = levelSelected * 2;
            }
            else if (gm.mode == 3)
            {
                activeCard = levelSelected * 3;
            }
            else
            {
                activeCard = levelSelected;
            }

            //On active celle qui est sélectionnée et pas les autres. (note : c'est une solution très sale, faudrait plutôt en faire une fonction à activer, mais on verra à la refonte.
            for (int i = 0; i < cards.Length; i++)
            {
                if (i != activeCard)
                {
                    cards[i].SetActive(false);
                }
                else
                {
                    cards[i].SetActive(true);
                }

            }

            //On met les infos sur le niveau choisi.
            showLevel.sprite = levels[levelSelected];
            levelName.text = names[levelSelected];
        }
        //On utilise un lerp, donc le mouvement prend 0.33s.
        masterCard.transform.localPosition = Vector3.Lerp(outPos, inPos, timer);

        

        


        //Si on peut bouger
        if (!move)
        {
            //Vers la droite
            if (potato.x > 0.5f)
            {
               
                    movement.Play();
                    StartCoroutine(ChangeLevel(true));
                    flecheD.SetTrigger("d");
               
                    StartCoroutine(MoveAgain());

                //On doit attendre la fin de la coroutine pour bouger de nouveau.
                move = true;
            }

            //Mêmes principes vers la gauche
            else if (potato.x < -0.5f)
            {
                
                    flecheG.SetTrigger("g");
                    StartCoroutine(ChangeLevel(false));
                    movement.Play();
                
                    StartCoroutine(MoveAgain());
               
                move = true;
            }
        }
        
    }


    
    public void OnSpell()
    {
        //Active ou désactive la carte d'informations.
        if (showing)
        {
            activate.Play();
            showing = false;
            if (timer >= 1)
            {
                timer = 1;
            }
            
        }
        else
        {
            deactivate.Play();
            showing = true;
            if (timer <= 0)
            {
                timer = 0;
            }
            
        }
    }

    //Deuxième gachette
    public void OnAltSpell()
    {
        if (showing)
        {
            activate.Play();
            showing = false;
            if (timer >= 1)
            {
                timer = 1;
            }

        }
        else
        {
            deactivate.Play();
            showing = true;
            if (timer <= 0)
            {
                timer = 0;
            }

        }
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
        gm.StartBattle();
    }

    

    //Pareil dans l'autre sens
    public void OnBack()
    {

        gm.CharacterSelected();
        
    }

    //Le temps entre deux mouvements.
    IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(0.4f);
        move = false;
    }

   

    //Le changement de niveau. Avec une coroutine pour que ça soit sync avec la flèche.
    IEnumerator ChangeLevel(bool plus)
    {
        yield return new WaitForSeconds(0.1f);

        if (gm.mode == 2)
        {
            if (!plus)
            {
                if (levelSelected > 0)
                {
                    levelSelected--;
                }
                else
                {
                    levelSelected = sumoLevels.Length - 1;
                }

            }
            else
            {
                if (levelSelected < sumoLevels.Length - 1)
                {
                    levelSelected++;
                }
                else
                {
                    levelSelected = 0;
                }
            }
        }
        else
        {
            if (!plus)
            {
                if (levelSelected > 0)
                {
                    levelSelected--;
                }
                else
                {
                    levelSelected = levels.Length - 1;
                }

            }
            else
            {
                if (levelSelected < levels.Length - 1)
                {
                    levelSelected++;
                }
                else
                {
                    levelSelected = 0;
                }
            }
        }
        
    }
}
