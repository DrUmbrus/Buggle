using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;




public class GameManager : MonoBehaviour
{
    [Header("Technical")]
    //Des armes uniques pour les modes sumo et golden. (Actuellement en développement)
    public GameObject sumoWeapon, goldenWeapon;
    //Noter le nombre de modes (-1, puisqu'on part de 0).
    public int maxMode = 1;

    //Un gameObject qui permet "d'imiter" un personnage, pour si on a par exemple le Joueur 1 et 3 en partie, mais pas de J2.
    [SerializeField]
    GameObject fake = null;
    //La scène actuelle
    string currentScene="";
    //Si on est en jeu ou pas
    public bool inGame=false;
    //La caméra à shaker
    public ScreenShake shaked = null;
    //Le script qui sert à faire l'écran noir de transition.
    public Rideau rid = null;
    //Les prefabs des trésors.
    public GameObject treasuresPrefab = null, bigTres = null;
    //Les particules des 4 couleurs, pour pouvoir les appliquer sur les joueurs.
    public GameObject[] particules=new GameObject[0];
    //La map à charger
    public int nextLevel = 0;
    //Est-ce qu'on est en train de préparer le combat.
    bool loadingBattle = false;
    //Est-ce qu'un joueur a son ultime de prêt.
    public bool ult = false;
    //Est-ce qu'on est en train de charger.
    public bool loading = false;
    //Est-ce que le mode bughunt est activé.
    public bool manHunt=false;
    //Sert pour lerp le son de la musique et donc avoir une transition smooth entre menu et combat.
    float musicTime = 0;
    //Les audiosources pour la musique et certains bruitages de menu.
    public AudioSource nextSelection = null, backSelection = null, bgm = null, fightM = null;
    //Est-ce que le joueur a activé l'aide à la visée
    public bool[] stick = new bool[4];
    //Compter le nombre de joueurs morts, pour les modes sumo et golden. (permet de savoir quand il ne reste que le vainqueur)
    int killed = 0;
    //Savoir aussi qui est mort.
    public bool[] killedBool = new bool[4];



    //Singleton
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    //L'objet qui gère les stats et l'UI en jeu.
    StatsManager stat = null;

    [Header("GameSettings")]
    //Le type de mode. 0=trésor, 1=deathmatch, 2=sumo, 3=golden
    public int mode = 0;
    //Le score des joueurs
    public int[] scores;
    //Le temps règlementaire
    public float startTime = 60;
    //Le score à atteindre
    public int targetScore=5;

    public int targetLast = 0;
    //La liste des persos du jeu
    public GameObject[] characters;
    //La liste des id des personnages choisis (pr rapport à la liste précédente)
    public int[] chosenCharacters;
    //Les personnages une fois créés
    public Character[] team;
    //Taille du niveau
    public float width, height;
    //La puissance du screenshake.
    public float shakePower=1;


    //Est-ce que c'est la première partie depuis le lancement du jeu ?
    public bool firstgame = true;
    //Permet de changer la vitesse du jeu. Pour l'instant inutile mais peut servir de modifier.
    public float speedo = 1;

    //Le nombre de joueurs.
    public int nPlayer;

    //Gérer les armes qui peuvent apparaître en jeu. Permettra plus tard de faire un système d'unlock, ou de la personnalisation des objets.
    [Header("ChosenItems")]
    //La liste des armes
    public List<GameObject> weapons;
    //Lesquelles sont rares (change les probabilités)
    public List<bool> rare;
    //Lesquelles sont en jeu.
    public List<bool> selectedWeapon;


    //Singleton
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);

        

    }

    //Raccourcis de dev
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            speedo = 0.25f;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            speedo = 1f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            speedo = 4f;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            CharacterSelected();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            BackMenu();
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            EndGame();
        }

        //Le lerp sur les musiques
        if (inGame)
        {
            musicTime += Time.deltaTime;
        }
        else
        {
            musicTime -= Time.deltaTime;
        }

        bgm.volume = Mathf.Lerp(0, 0.03f, 1 - musicTime);
        fightM.volume = Mathf.Lerp(0, 0.1f, musicTime);
    }


    //Invocation des joueurs
    public void Summon()
    {
        for(int i=0; i<4; i++)
        {
            if (chosenCharacters[i] >= 0)
            {
                //On créé les joueurs, on leur donne leurs stats, et on les ajoute à team
               GameObject temp= Instantiate(characters[chosenCharacters[i]]);
                team[i] = temp.GetComponent<Character>();
                if (mode == 2)
                {
                    team[i].startingGun = sumoWeapon;
                    team[i].speed = 70;
                }
                else if (mode == 3)
                {
                    team[i].startingGun = goldenWeapon;
                    team[i].speed = 90;
                }
                team[i].SetStats(i, stick[i]);

                team[i].paillettes = particules[i];
            }
            else
            {
                Instantiate(fake, transform.position, Quaternion.identity);
            }
            
        }

        //Puis on active l'UI
        stat.SelectPlayers(team);
    }


    //Quand quelqu'un fait un kill
    public void Kill(int id, int ded)
    {
        //Si c'est bien un joueur qui a fait le kill
        if (id >= 0)
        {
            
            if (mode==1)
            {
                //On ajoute à son killstreak
                team[id].killStreak++;
                //Et on met à jour le score
                team[id].kills++;
                scores[id]++;

                //Si le score cible est atteint, on met fin à la partie
                if (scores[id] >= targetScore)
                {
                    EndGame();
                }
            }
        }

        //Pour les modes sumo et golden
        if(mode==2 || mode == 3)
        {
            //On note que le joueur est mort, puis on vérfie s'il reste un seul joueur en vie.
            killedBool[ded] = true;
            killed++;
            if (killed >= nPlayer - 1)
            {
                //On cherche qui est le gagnant.
                int lastStanding = -1;
                for (int i = 0; i < 4; i++)
                {
                    if(lastStanding==-1 && !killedBool[i])
                    {
                        lastStanding = i;
                    }
                }

                //On remet ensuite les stats à 0, en préparation de la fin de partie.
                for (int i = 0; i < 4; i++)
                {
                    killedBool[i] = false;
                }
                killed = 0;
                //On compte le nombre de victoires
                team[lastStanding].lastStand++;

                //Si assez de victoires, on passe à la fin de partie, sinon on lance la suivante.
                if (team[lastStanding].lastStand >= targetLast)
                {
                    EndGame();
                }
                else
                {
                    
                    team[lastStanding].stopped = true;
                    StartCoroutine(RideauInGame());
                    stat.started = false;
                }
            }
        }
    }

    //Remettre les stats à 0.
    public void PurgeStats()
    {
        for(int i=0; i<4; i++)
        {
            scores[i] = 0;
            team[i] = null;
            //chosenCharacters[i] = -1;
            targetScore = 10;
            startTime = 120;
        }
        
    }

    //Retour au menu
    public void BackMenu()
    {

        StartCoroutine(ChangeScene("ChoixMode"));
        PurgeStats();
    }

    //Passer au choix du niveau
    public void CharacterSelected()
    {
        int counter = 0;
        for (int i = 0; i < chosenCharacters.Length; i++)
        {
            if (chosenCharacters[i] >= 0)
            {
                counter++;
            }
        }

        nPlayer = counter;
        StartCoroutine(ChangeScene("ChoixMode"));
    }

    //Passer sur la fenêtre de choix du stage.
    public void OptionSelected()
    {
        StartCoroutine(ChangeScene("ChoixNiveau"));
    }

    //Passer à la personnalisation des objets (wip)
    public void ChangeItems()
    {
        StartCoroutine(ChangeScene("ChoixItems"));

    }

    //Les commandes du menu principal
    public void ButtonBattle(int nb)
    {

        if (nb == 0)
        {
            //On met bien tout à 0
            for (int i = 0; i < team.Length; i++)
            {
                team[i] = null;
                chosenCharacters[i] = -1;
            }
            //Et on lance la scène
            StartCoroutine(ChangeScene("CharacterScene"));
        }
        else if (nb == 3)
        {
            //Les crédits
            StartCoroutine(ChangeScene("CreditsScene"));
        }
        else if (nb == 2)
        {
            //Les commandes
            StartCoroutine(ChangeScene("ControlScene"));
        }
        else if (nb == 1)
        {
            //Les commandes
            StartCoroutine(ChangeScene("CommandScene"));
        }
        else
        {
            //Quitter
            Application.Quit();
        }
        
    }

    //Fin de la partie, on se débarasse de l'UI et on lance la séquence de fin
    public void EndGame()
    {
        //On récupère les scores et on stoppe les joueurs
        for (int i = 0; i < 4; i++)
        {
            if (chosenCharacters[i] != -1)
            { 
                scores[i] = team[i].AskScore();
            team[i].end = true;
            }
            else
            {
                scores[i] = -1;
            }
        }
        
        //Puis on lance l'écran de scores
        StartCoroutine(ChangeScene("ResultScreen"));
        if (targetScore == 9999)
        {
            targetScore = 0;
        }
    }

    

    //Préparation du combat
    public void StartBattle()
    {
        loadingBattle = true;
        if (mode==1 || mode==0 || mode==3)
        {
            StartCoroutine(ChangeScene("Scene Tresor" + nextLevel));
            if (targetScore <= 0)
            {
                targetScore = 9999;
            }
        }
        else if(mode==2)
        {
            StartCoroutine(ChangeScene("Scene Sumo" + nextLevel));
        }
        
    }


    //Fonction à appeler si quelqu'un veut lancer un screenshake
    public void Shake(float dura, float str, float dank)
    {
        shaked.StartShaking(dura, str*shakePower, dank);
    }

    //Invocation de trésors autour d'un joueur mort
    public void SummonTreasure(Vector3 posi)
    {
        Vector2 pose= Random.insideUnitCircle*4;
        Instantiate(treasuresPrefab, (Vector2)posi+pose, Quaternion.identity);
        treasuresPrefab.transform.Translate(new Vector3(0, 0, -0.15f));
    }

    //Création des gros trésors à la mort (note : peut être à retirer ?)
    public void SummonBigTreasure(Vector3 posi, int target)
    {
        if (target >= 0)
        {
            GameObject temp = Instantiate(bigTres, posi, Quaternion.identity);
            temp.GetComponent<BigTreasure>().SetTarget(target);
            bigTres.transform.Translate(new Vector3(0, 0, -0.15f));
        }

        else
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 pose = Random.insideUnitCircle * 4;
                Instantiate(treasuresPrefab, (Vector2)posi + pose, Quaternion.identity);
                treasuresPrefab.transform.Translate(new Vector3(0, 0, -0.15f));
            }
        }
       
    }



    //Mise en place du jeu
    IEnumerator PrepareThings()
    {
        //bgm.Stop();
        //Une frame de pause pour qu'on ait bien changé de scène
        yield return 0;
        //On récupère les trucs importants
        stat = FindObjectOfType<StatsManager>();
        shaked = FindObjectOfType<ScreenShake>();
        inGame = true;
        if (musicTime < 0)
        {
            musicTime = 0;
        }
        fightM.Play();

        WeaponSpawner[] ws = FindObjectsOfType<WeaponSpawner>();
        foreach(WeaponSpawner w in ws)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (selectedWeapon[i])
                {
                    w.guns.Add(weapons[i]);
                    w.highTiers.Add(rare[i]);

                    if (!rare[i])
                    {
                        w.guns.Add(weapons[i]);
                        w.highTiers.Add(rare[i]);
                    }
                }
                
            }
        }

        //On pose les joueurs
        Summon();

        
        //On attend d'avoir bien récupéré le stat, donc on attend tant que c'est pas bon
        while (stat == null)
        {
            yield return 0;
        }
      
    }

    //Changement de scène
    IEnumerator ChangeScene(string futureScene)
    {

        currentScene = futureScene;
        //Le fondu au noir
        loading = true;
        rid.Close();
        while (!rid.sent)
        {
            yield return 0;
        }
        //On lance la scène quand le fondu est complet
        SceneManager.LoadScene(futureScene);
        //On attend une frame, pour que le chargement soit complet
        yield return 0;
        //Fin du fondu
        rid.Open();
        loading = false;

        if(loadingBattle)
        {
            if (firstgame)
            {
                firstgame = false;
            }
            StartCoroutine(PrepareThings());
            Time.timeScale = speedo;
        }
        else
        {
            Time.timeScale = 1;
            inGame = false;
            if (musicTime > 1)
            {
                musicTime = 1;
            }
            stat = null;
        }
        loadingBattle = false;
    }

    IEnumerator RideauInGame()
    {
        rid.Close();
        while (!rid.sent)
        {
            yield return 0;
        }
        //On attend une frame, pour que le chargement soit complet
        yield return 0;
        //Fin du fondu
        rid.Open();
        for (int i = 0; i < team.Length; i++)
        {
            if (chosenCharacters[i] >= 0)
            {
                team[i].Respawn();
            }
            
        }
        stat.NewStart();
        yield return new WaitForSeconds(0.5f);
        
        
    }
}
