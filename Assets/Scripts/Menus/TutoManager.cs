using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{

    /// <summary>
    /// La gestion de la scène compendium
    /// </summary>
    /// 

    GameManager gm = null;
    [SerializeField]
    MoveIn info = null;

    [SerializeField]
    GameObject flecheH = null, flecheB = null;

    [SerializeField]
    GameObject[] realinfo= new GameObject[0];

    Vector2 potato = Vector2.zero;
    public bool move=false;

    int currentInfo=0;
    int oldInfo=0;
    bool watching = false;


    [SerializeField]
    GameObject cursor = null;

    [SerializeField]
    Text[] texts= new Text[0];

    [SerializeField]
    string[] names= new string[0];

    [SerializeField]
    int maxPage=0;
    int currentPage = 0;

    [SerializeField]
    AudioSource movement = null, activate = null, deactivate = null;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        ChangePage(0);
        for (int i = 0; i < realinfo.Length; i++)
        {
            realinfo[i].SetActive(false);
        }
        realinfo[0].SetActive(true);
    }

    private void Update()
    {
        oldInfo = currentInfo;

        //Quand on bouge vers le haut, on monte, quand on bouge vers le bas, on descent. Avec une boucle quad on arrive sur la limite.
        //Si on dépasse la limite d'entrées de la page, on passe à la page suivante.
        if (!move)
        {
            if (potato.y > 0.5f)
            {
                if (currentInfo > 0)
                {
                    currentInfo--;
                }
                else
                {
                    if (currentPage > 0)
                    {
                        ChangePage(currentPage - 1);
                    }
                    else
                    {
                        ChangePage(maxPage);
                    }
                }
                StartCoroutine(MoveAgain());
                move = true;
            }
            else if (potato.y < -0.5f)
            {
                if(currentInfo>=1 && currentPage == maxPage)
                {
                    if (currentPage < maxPage)
                    {
                        ChangePage(currentPage + 1);
                    }
                    else
                    {
                        ChangePage(0);
                    }
                }
                else if (currentInfo < 4)
                {
                    currentInfo++;
                }
                else
                {
                    if (currentPage < maxPage)
                    {
                        ChangePage(currentPage + 1);
                    }
                    else
                    {
                        ChangePage(0);
                    }
                }
                
                StartCoroutine(MoveAgain());
                move = true;
            }


            //On on est dans le mode lecture d'une entrée, on peut déplacer vers la gauche et la droite pour alterner entre le texte et l'image de présentation.
            if (watching)
            {
                if (potato.x > 0.5f)
                {
                    info.SideMe();
                    StartCoroutine(MoveAgain());
                    move = true;
                }
                else if (potato.x < -0.5f)
                {
                    info.SideMeBack();
                    StartCoroutine(MoveAgain());
                    move = true;
                }
            }
        }

        if (oldInfo != currentInfo)
        {
            for (int i = 0; i < realinfo.Length; i++)
            {
                realinfo[i].SetActive(false);
            }
            realinfo[currentInfo+5*currentPage].SetActive(true);
        }
        cursor.transform.position = texts[currentInfo].transform.position;

        //La flèche pour passer à la page suivante ou précédente n'est présente que s'il y a une page dans cette direction.
        if (currentPage == maxPage)
        {
            flecheB.SetActive(false);
        }
        else
        {
            flecheB.SetActive(true);
        }

        if (currentPage == 0)
        {
            flecheH.SetActive(false);
        }
        else
        {
            flecheH.SetActive(true);
        }
    }

    public void OnMove(InputValue value)
    {
        potato = value.Get<Vector2>();

    }

    //Quand on change de page, on met à jour la position du curseur pour qu'il soit au bon endroit sur la page suivante, et on update les entrées de la page.
    public void ChangePage(int newPage)
    {
        if (newPage == maxPage && currentPage == 0)
        {
            currentInfo = 1;
        }
        else if (newPage >= currentPage)
        {
            currentInfo = 0;
        }
        else
        {
            if (newPage == 0 && currentPage==maxPage)
            {
                currentInfo = 0;
            }
            else
            {
                currentInfo = 4;
            }
            
        }
        currentPage = newPage;
        for (int i = 0; i < texts.Length; i++)
        {
            if (5 * currentPage + i < names.Length)
            {
                texts[i].text = names[5 * currentPage + i];
            }
            else
            {
                texts[i].text = "";
            }
        }

    }

    //Quand on appuie sur A sur une entrée, on peut voir les détails.
    public void OnGrab()
    {
        if (!watching)
        {
            activate.Play();
            watching = true;
            info.MoveMe();
        }
    }

    //Et B permet de revenir au menu.
    public void OnBack()
    {
        if (!watching)
        {
            if (!gm.backSelection.isPlaying)
            {
                gm.backSelection.Play();
            }
            
            gm.BackMenu();
        }
        else
        {
            deactivate.Play();
            watching = false;
            info.MoveMe();
        }
    }

    //Timer de 0.3s entre chaque mouvement pour que ça reste gérable.
    IEnumerator MoveAgain()
    {
        movement.Play();
        yield return new WaitForSeconds(0.3f);
        move = false;
    }

}
