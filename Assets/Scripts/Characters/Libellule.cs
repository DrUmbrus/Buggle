using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libellule : Character
{
    /// <summary>
    /// Les aspects uniques de la libellule
    /// </summary>
    
    //L'impact et la durée de son spell
    public int plusAngle;
    public float timer, maxTimer;
    //Bool de vérification pour le décompte.
    bool started;
    //Vérifie si on change d'arme pendant le spell.
    Gun oldGun;

    //Le portail magique, ses positions, et la distance minimale entre deux.
    public GameObject shadow;
    public List<Vector2> fallPos;
    public float minDis=2.5f;

    [SerializeField]
    AudioSource ultiShake;

    public override void Pattern()
    {
       
        //Si le spell est actif, on diminue le temps restant, et on remet les valeurs à 0 à la fin du spell.
        if (started)
        {
            //Si on a changé d'arme, on update et on applique l'effet dessus.
            if (gun != oldGun)
            {
                oldGun = gun;
                gun.multiPattern *= 2;
                gun.maxAngle += plusAngle;
                gun.multishot *= 2;
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                dragons.Remove(gameObject);
                gun.multiPattern /= 2;
                anim.SetTrigger("endaction");
                gun.multishot /= 2;
                started = false;
                gun.maxAngle -= plusAngle;
            }
        }

    }

    //Mise en place du spell, on applique l'effet, on note l'arme en cours, et on active le bool + le timer.
    //Ce sort permet d'avoir un double tir temporairement, avec une perte de précision. 
    public override void Competence()
    {
        dragons.Add(gameObject);
        oldGun = gun;
        anim.SetTrigger("action");
        gun.multiPattern *= 2;
        gun.maxAngle += plusAngle;
        gun.multishot *= 2;
        started = true;
        timer = maxTimer;
    }

    //Activer l'ulti
    public override void Ultimate()
    {
        StartCoroutine(UltiLib());
    }


    IEnumerator UltiLib()
    {
        //Mise en place de l'invincibilité, et on vide le tableau.
        anim.SetTrigger("ultimate");
        gm.ult = true;
        box.enabled = false;
        stopped = true;
        fallPos.Clear();

        yield return new WaitForSeconds(0.5f);
        gm.Shake(5, 35, 1.05f);


        for (int i = 0; i < 10; i++)
        {
            //Pour chaque portail, on définit une position
            Vector3 pos = Vector3.zero;
            bool test = false;
            pos.z = -8;
            while (!test)
            {
                //Cette position est aléatoire, dans les limites de l'écran.
                test = true;
                pos.x = Random.Range(-gm.width, gm.width+0.1f);
                pos.y = Random.Range(-gm.height-2, gm.height -2);

                //On s'assure également que tous les portails respectent la distance de sécurité
                if (fallPos.Count > 0)
                {
                    for (int j = 0; j < fallPos.Count; j++)
                    {
                        if(Vector2.Distance(pos, fallPos[j]) < minDis)
                        {
                            test = false;
                        }
                    }
                }
            }

            fallPos.Add(pos);

            //Ensuite on invoque le portail, en indiquant qui en est à l'origine.
           GameObject temp= Instantiate(shadow, pos, Quaternion.identity);
            temp.GetComponent<RayonMagique>().source = playerNum;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        //Fin du spell, retour à la normale.
        anim.SetTrigger("ultend");
        killStreak = 0;
        yield return new WaitForSeconds(0.5f);
        box.enabled = true;
        stopped = false;

        gm.ult = false;
    }

    
}
