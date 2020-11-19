using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;



public class StatsManager : MonoBehaviour
{
    [Header("Misc")]
    //le game manager
    GameManager gm = null;
    //Les joueurs
    public Character[] players= new Character[0];
    //Les images des persos
    public Sprite[] portrait = new Sprite[0];
    //Quand est-ce qu'on a activé l'UI et qu'on peut tout lancer
    public bool started = false;
    //Les sprites pour indiquer le type de scoring
    public Sprite coin = null, skull = null;
    Transform can = null;
    public int nPlayers = 0;
    [SerializeField]
    Sprite[] speellSprites= new Sprite[0];
    public Color[] playersCol= new Color[0];



    [Header("UI")]
    //Le texte avant de lancer
    public Text go = null;
    //Les canvas parents. Permet d'activer seulement l'ui des joueurs qu'on a
    public GameObject[] parentsCan= new GameObject[0];
    //Le temps restant, texte (note : En faire un vrai timer ?
    public Text timer = null;
    
    //Le texte des scores
    public Text[] scores= new Text[0];
    //Les images où mettre les portraits
    public Image[] portraits= new Image[0];
    //L'image des killstreaks en mode DM
    public GameObject[] killPre= new GameObject[0];
    //Les valeurs de killstreak en DM
    public Text[] killStreak= new Text[0];
    //L'image pour le type de scoring
    public Image[] symbol= new Image[0];
    //La cooldown des spells
    public Image[] mana= new Image[0];
    public Image[] manaBG= new Image[0];
    [SerializeField]
    GameObject textTime = null;
    [SerializeField]
    GameObject[] couronnes= new GameObject[0];
    [SerializeField]
    Light2D[] classementLight= new Light2D[0];

    [Header("Life")]
    //Les barres de vie, texte
    public Text[] lifes= new Text[0];
    //Les sliders de vie
    public Image[] lifeSlide= new Image[0];
    public Image[] lifeSlideRed = new Image[0];


    [Header("Values")]
    //Le temps restant
    public float time=0;
    //Taille de la map
    public float width=0, height=0;
    //Les scores
    public int[] scoresNum=new int[0];
    //Les scores à la frame d'avant (pour voir l'évolution)
    public int[] oldScores= new int[0];
    int nextTresh = 90;


    [Header("Visuals")]
    public Color orange= new Color(0,0,0,0);
    //Le post process
    Volume vol = null;
    
    //Sert à vérifier la taille des scores
    bool[] big = new bool[4];
    

    public Light2D globalLight = null;
    //Le fait de baisser la lumière globale
    //bool dimLight = false;
    //La vitesse à laquelle la lumière baisse
    float lightTime = 1;
    float targetLight=0;
    [SerializeField]
    float maxLight=1.5f, minLight=0.8f;
    //Le bloom
    UnityEngine.Rendering.Universal.Bloom blo;
    public Light2D[] sideLights= new Light2D[0];


    [Header("Smash")]
    //Invocation de la balle smash
    public float timeSummon = 99;
    public float maxTimesummon=0;
    GameObject summoned = null;
    //La balle smash
    public GameObject smash = null, smashDM = null;

    //Système de classement des joueurs in game.
    [Header("Classement")]
    public int[] classement= new int[4];
    public int[] oldClass = new int[4];
    public float[] timefirst = new float[4];
    public float[] timerTargetLightPlayers = new float[4];
    public float[] timeLast = new float[4];
    public Vector2[] classementPos = new Vector2[4];
    public Vector2[] oldClassementPos= new Vector2[0];
    public Vector2[] targetClassementPos = new Vector2[0];
    public GameObject[] parentClassementPos = new GameObject[0];
    public Image[] portraitClassementPos = new Image[0];
    public Text[] scoreClassementPos = new Text[0];




    //Mise en place des composents et variables
    private void Awake()
    {
        timer.text = "";
        can = GameObject.Find("UI").GetComponent<Transform>();
        
        timeSummon = maxTimesummon;
        gm = FindObjectOfType<GameManager>();
        //On dit au gm la taille de la map
        gm.width = width;
        gm.height = height;
        vol = GameObject.Find("PP").GetComponent<Volume>();
        vol.profile.TryGet(out blo);

        //On détruit les spawners inutiles
        SpawnPoint[] spa = FindObjectsOfType<SpawnPoint>();

        foreach(SpawnPoint sp in spa)
        {
            if (gm.chosenCharacters[sp.player] == -1)
            {
                Destroy(sp.gameObject);
            }
        }

        for (int i = 0; i < scoresNum.Length; i++)
        {
            scoresNum[i] = 0;
            timerTargetLightPlayers[i] = 0;
            classement[i] = -1;
            
        }

        //Si on est pas en mode trésor, on désactive les trésors.
        if (gm.mode !=0)
        {
            TresorSpawn[] potato = FindObjectsOfType<TresorSpawn>();

            foreach(TresorSpawn treSpa in potato)
            {
                treSpa.gameObject.SetActive(false);
            }
        }
        //Si on est en mode sumo ou golden, on désactive les spawners.
        if(gm.mode==2 || gm.mode == 3)
        {
            RandomSpawner[] potato = FindObjectsOfType<RandomSpawner>();

            foreach (RandomSpawner treSpa in potato)
            {
                treSpa.gameObject.SetActive(false);
            }

            WeaponSpawner[] potata = FindObjectsOfType<WeaponSpawner>();

            foreach (WeaponSpawner treSpad in potata)
            {
                treSpad.gameObject.SetActive(false);
            }
        }
    }

    //Appelé par le gm
    public void SelectPlayers(Character[] team)
    {
        
        if (gm.startTime == 60)
        {
            nextTresh = 30;
        }
        else if (gm.startTime>60)
        {
            nextTresh = 60;
        }
        else{
            nextTresh = -10;
        }
        
        for (int i = 0; i < 4; i++)
        {
            big[i] = false;
        }
        //On récupère les joueurs
        for(int i=0; i<team.Length; i++)
        {
            players[i] = team[i];
        }

        time = gm.startTime;
        timer.text = time.ToString();

        for (int i = 0; i < 4; i++)
        {
            //Pour chaque joueur existant
            if (players[i] != null)
            {
                parentClassementPos[i].SetActive(true);
                nPlayers++;
                //On active le parent
                parentsCan[i].SetActive(true);
                //On met en place la vie
                lifes[i].text = players[i].life.ToString() + " / " + players[i].maxLife.ToString();
                lifeSlide[i].fillAmount = players[i].life/players[i].maxLife;
                //On met en place les portraits
                portraits[i].sprite = portrait[gm.chosenCharacters[i]];
                scores[i].text = "0";
                mana[i].sprite = speellSprites[gm.chosenCharacters[i]];
                manaBG[i].sprite = speellSprites[gm.chosenCharacters[i]];
                portraitClassementPos[i].sprite = portrait[gm.chosenCharacters[i]];
                scoreClassementPos[i].color = playersCol[i];
                scoreClassementPos[i].text = "0";
                parentClassementPos[i].GetComponent<Image>().color = playersCol[i];

                if (gm.mode > 1)
                {
                    mana[i].color = Color.clear;
                    manaBG[i].color = Color.clear;
                }
                //On ajuste l'UI au mode de jeu
                if (gm.mode==0)
                {
                    //killPre[i].SetActive(false);
                    symbol[i].sprite = coin;
                }
                else
                {
                    symbol[i].sprite = skull;
                }

            } 
        }

        
        StartCoroutine(Waiting());
    }

    public void NewStart()
    {
        StartCoroutine(StopCharacter());

    }

    IEnumerator StopCharacter()
    {
        yield return 0;
        UpdateStats();
        for (int i = 0; i < gm.team.Length; i++)
        {
            if (gm.chosenCharacters[i] > 0)
            {
                gm.team[i].stopped = true;
            }
            
        }
        go.text = "3";
        yield return new WaitForSeconds(1);
        go.text = "2";
        yield return new WaitForSeconds(1);
        go.text = "1";
        yield return new WaitForSeconds(1);
        go.text = "GO";
        started = true;
        for (int i = 0; i < gm.team.Length; i++)
        {
            if (gm.chosenCharacters[i] > 0)
            {
                gm.team[i].stopped = false;
            }
        }
        yield return new WaitForSeconds(0.5f);
        go.text = "";
    }

    //Le temps au lancement
    IEnumerator Waiting()
    {
        go.text = "3";
        yield return new WaitForSeconds(1);
        go.text = "2";
        yield return new WaitForSeconds(1);
        go.text = "1";
        yield return new WaitForSeconds(1);
        go.text = "GO";
        started = true;
        yield return new WaitForSeconds(0.5f);
        go.text = "";
    }

    private void FixedUpdate()
    {
        if (started)
        {

                //On sonne la fin de partie si le timer tombe à 0
                if (time > 0)
                {
                    time -= Time.deltaTime;
                }
                else
                {
                    gm.EndGame();
                }

                //Le temps est mis à jour, sous forme d'int
                timer.text = Mathf.CeilToInt(time).ToString();

            //Le timer devient plus gros en fin de partie
            if (nextTresh <= 0)
            {
                timer.fontSize = 120;
                timer.color = Color.Lerp(Color.red, Color.yellow, time / 30);
            }

            if (time < nextTresh)
            {
                GameObject temp=Instantiate(textTime, transform.position, Quaternion.identity, can);
                temp.GetComponent<Text>().text = nextTresh + " seconds left";
                nextTresh -= 30;

                if (nextTresh == 0)
                {
                    nextTresh = -10;
                }
            }


            //Mise à jour de l'UI
            UpdateStats();

            //A la fin de ce timer, on invoque une balle smash
            timeSummon -= Time.deltaTime;

            
                if (timeSummon <= 0 && summoned == null)
                {
                //Différence entre les deux balles
                if (gm.mode==0)
                {
                    Vector2 potato = new Vector2(transform.position.x, transform.position.y);
                    summoned = Instantiate(smash, potato + Random.insideUnitCircle * 3, Quaternion.identity);
                    summoned.transform.Translate(new Vector3(0, 0, -0.15f));
                    summoned.GetComponent<SmashBall>().sm = this;
                }
                else if(gm.mode==1)
                {
                    Vector2 potato = new Vector2(transform.position.x, transform.position.y);
                    summoned = Instantiate(smashDM, potato + Random.insideUnitCircle * 3, Quaternion.identity);
                    summoned.transform.Translate(new Vector3(0, 0, -0.15f));
                    summoned.GetComponent<DMSmashBall>().sm = this;

                }
                else
                {
                    timeSummon = 9999;
                }
            }


                //Evolution progressive de la lumière, quand elle diminue ou se remet.
            if (lightTime < targetLight)
            {
                lightTime += Time.deltaTime;
            }
            else if (lightTime > targetLight)
            {
                lightTime -= Time.deltaTime;
            }

            //Une approximation pour pouvoir fixer la valeur au lieu de danser autour.
            if (Mathf.Abs(lightTime - targetLight) < 0.05f)
            {
                lightTime = targetLight;
            }

            
            //Mettre en place la luminosité des lumières selon les valeurs précédentes.
            globalLight.intensity = Mathf.Lerp(minLight, maxLight, lightTime);
            for (int i = 0; i < sideLights.Length; i++)
            {

                sideLights[i].intensity = Mathf.Lerp(0, maxLight/2, lightTime);
            }

            blo.intensity.Override(Mathf.Lerp(0.2f, 0.4f, 1 - lightTime));
        }

        
    }

    public void UpdateStats()
    {
        bool testLight = false;
        //Reprend basiquement la création, on update la vie, le slider, les munitions, et les armes
        for(int i=0; i<players.Length; i++)
        {
            if (players[i] != null)
            {
                lifes[i].text = players[i].life.ToString() + " / " + players[i].maxLife.ToString();
                lifeSlide[i].fillAmount = (float)players[i].life / (float)players[i].maxLife;

                if(lifeSlideRed[i].fillAmount> lifeSlide[i].fillAmount)
                {
                    lifeSlideRed[i].fillAmount -= Time.deltaTime / 3;
                }
                else
                {
                    lifeSlideRed[i].fillAmount = lifeSlide[i].fillAmount;
                }
                scoresNum[i] = players[i].AskScore();
                if(scoresNum[i]> oldScores[i] && !big[i])
                {
                    StartCoroutine(ScoreUp(i));
                }
                scores[i].text = scoresNum[i].ToString();
                scoreClassementPos[i].text= scoresNum[i].ToString();
                oldScores[i] = scoresNum[i];

                if (gm.mode==1)
                {
                    killStreak[i].text = players[i].killStreak.ToString();
                    if (players[i].killStreak >= 3)
                    {
                        testLight = true;
                    }
                }
                else if(gm.mode==0)
                {
                    if (timefirst[i] >= 30)
                    {
                        killStreak[i].text ="0";
                    }
                    else
                    {
                        killStreak[i].text = Mathf.CeilToInt(30 - timefirst[i]).ToString();
                    }
                    
                }
                mana[i].fillAmount = 1-players[i].timerComp/players[i].maxTimerComp;
                
            }
            
        }

        //Baisse la lumière si quelqu'un a son ulti.
        if (testLight)
        {
            targetLight = 0;

        }
        //Lumière tamisée s'il y a la balle smash.
        else if (summoned != null)
        {
            targetLight = 0.75f;
        }
        else
        {
            targetLight = 1;
        }
        //Compromis pour le mode bughunt.
        if (gm.manHunt)
        {
            targetLight = 0.3f;
        }
        

        OrderScores();

        for (int i = 0; i < 4; i++)
        {
            timerTargetLightPlayers[i] += Time.deltaTime;
            //classementLight[i].intensity = Mathf.Lerp(oldTargetLightPlayers[i], targetLightPlayers[i], timerTargetLightPlayers[i]);
            parentClassementPos[i].transform.localPosition = Vector2.Lerp(oldClassementPos[i], targetClassementPos[i], timerTargetLightPlayers[i]);
        }
    }

    public void OrderScores()
    {
        //Un tableau où ranger ça pour l'instant
        int[] scores = new int[4];

        for (int i = 0; i < 4; i++)
        {
            oldClass[i] = classement[i];
        }

        for (int i = 0; i < 4; i++)
        {
            scores[i] = -1;
            classement[i] = -1;
        }


        //Pour le nombre de joueurs => taille du classement
        for (int i = 0; i < 4; i++)
        {
            
            //Vérifier chaque joueur
            for (int j = 0; j < 4; j++)
            {
                if (gm.chosenCharacters[j] != -1 && gm.chosenCharacters[i] != -1)
                {

                    //Si le score du joueur est plus haut que le score actuel
                    if (scoresNum[j] > scores[i])
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
                            scores[i] = scoresNum[j];
                            classement[i] = j;
                        }

                    }
                }

            }

        }

        //Pour activer le bughunt, il faut rester premier pendant 30s consécutives.
        if (classement[0] == oldClass[0])
        {
            timefirst[classement[0]] += Time.deltaTime;
            if (timefirst[classement[0]] >= 30 && !gm.manHunt && !players[classement[0]].hunted && gm.mode==0 && nPlayers!=2)
            {
                gm.manHunt = true;
                players[classement[0]].HuntMe();
            }
        }
        //Si le premier a changé, on remet les stats à 0.
        else
        {
            for (int i = 0; i < 4; i++)
            {
                timefirst[i] = 0;
                if (gm.mode==0)
                {
                    killStreak[i].text= Mathf.CeilToInt(30 - timefirst[i]).ToString();
                }

                couronnes[i].SetActive(false);
            }
            if (scoresNum[classement[0]] > 0)
            {
                couronnes[classement[0]].SetActive(true);
            }
            
        }

        
        //Long système qui permet de faire le déplacement des portraits en fonction de leur place précédente et updatée, sans toucher aux autres, en fonction du nombre de joueurs.
        if (oldClass[0] != classement[0])
        {
            targetClassementPos[classement[0]] = classementPos[0];
            oldClassementPos[classement[0]] = parentClassementPos[classement[0]].transform.localPosition;
            timerTargetLightPlayers[classement[0]] = 0;
        }
        if (nPlayers == 4)
        {
            if (oldClass[1] != classement[1])
            {
                targetClassementPos[classement[1]] = classementPos[1];
                oldClassementPos[classement[1]] = parentClassementPos[classement[1]].transform.localPosition;
                timerTargetLightPlayers[classement[1]] = 0; 
            }
            if (oldClass[2] != classement[2])
            {
                targetClassementPos[classement[2]] = classementPos[2];
                oldClassementPos[classement[2]] = parentClassementPos[classement[2]].transform.localPosition;
                timerTargetLightPlayers[classement[2]] = 0;
            }
            if (oldClass[3] != classement[3])
            {
                targetClassementPos[classement[3]] = classementPos[3];
                oldClassementPos[classement[3]] = parentClassementPos[classement[3]].transform.localPosition;
                timerTargetLightPlayers[classement[3]] = 0;
            }

            //Si quelqu'un reste dernier trop longtemps, il reçoit un bouclier automatique quand il respawn, pour l'aider.  
            if (classement[3] == oldClass[3])
            {
                timeLast[classement[3]] += Time.deltaTime;
                if (timeLast[classement[3]] >= 20 )
                {
                    players[classement[3]].shieldMe = true ;
                    players[classement[3]].maxShieldLife = 3;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    timeLast[i] = 0;
                }
            }
        }
        else if (nPlayers == 3)
        {
            if (oldClass[1] != classement[1])
            {
                targetClassementPos[classement[1]] = classementPos[1];
                oldClassementPos[classement[1]] = parentClassementPos[classement[1]].transform.localPosition;
                timerTargetLightPlayers[classement[1]] = 0;
            }
            if (oldClass[2] != classement[2])
            {
                targetClassementPos[classement[2]] = classementPos[2];
                oldClassementPos[classement[2]] = parentClassementPos[classement[2]].transform.localPosition;
                timerTargetLightPlayers[classement[2]] = 0;
            }

            if (classement[2] == oldClass[2])
            {
                timeLast[classement[2]] += Time.deltaTime;
                if (timeLast[classement[2]] >= 20)
                {
                    players[classement[2]].shieldMe = true;
                    players[classement[2]].maxShieldLife = 3;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    timeLast[i] = 0;
                }
            }
        }
        else
        {
            if (oldClass[1] != classement[1])
            {
                targetClassementPos[classement[1]] = classementPos[1];
                oldClassementPos[classement[1]] = parentClassementPos[classement[1]].transform.localPosition;
                timerTargetLightPlayers[classement[1]] = 0;
            }
        }

        //Le temps passé en dernier n'est compté que si on est en mode trésor ou deathmatch.
        if (gm.mode == 0 || gm.mode == 1) {
            for (int i = 0; i < 4; i++)
            {
                timeLast[i] = 0;
            }
        }
    }

    //Quand un joueur a gagné des points
    IEnumerator ScoreUp(int charac)
    {
        //En gros on change la taille et la couleur du texte, et ça fait mieux quand il passe premier.
        big[charac] = true;
        int taille = scores[charac].fontSize;
        scores[charac].color= Color.yellow;
        for(int i= 0; i< scores[charac].fontSize /4; i++)
        {
            scores[charac].fontSize+=2;
            yield return 0;
        }

        bool size = true;

        for (int i = 0; i < 4; i++)
        {
            if (scoresNum[charac] < scoresNum[i])
            {
                size = false;
            }
        }

        if (size)
        {
            scores[charac].fontSize = Mathf.CeilToInt(scores[charac].fontSize*1.5f);
            scores[charac].color = Color.red;
            yield return new WaitForSeconds(0.2f);
            int removeSize = Mathf.CeilToInt((scores[charac].fontSize - taille) / 3);
            scores[charac].fontSize = scores[charac].fontSize - removeSize;
            scores[charac].color = orange;
            yield return new WaitForSeconds(0.15f);
            scores[charac].fontSize = scores[charac].fontSize - removeSize;
            scores[charac].color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            scores[charac].fontSize = scores[charac].fontSize - removeSize;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            scores[charac].fontSize = taille;
        }

        scores[charac].color = Color.black;

        big[charac] = false;
    }

    //Quand la balle a été détruite
    public void EndSmash()
    {
        timeSummon = maxTimesummon;
        summoned = null;
        
    }

    
}
