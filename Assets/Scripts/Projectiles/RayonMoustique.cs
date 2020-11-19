using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayonMoustique : Projectile
{
    /// <summary>
    /// Les armes de type laser continu 
    /// </summary>
    LineRenderer lr = null;
    float loadingTime = 0;
    public bool ending = false;
    public Transform muzzle;
    public Transform hitPos;
    public LayerMask mask;
    public List<float> hitTime = new List<float>();
    List<bool> isChar = new List<bool>();
    public int newDam = 0;
    bool started = false;
    Gun creGun = null;
    bool hitted = false;
    public float maxLength = -1;
    public float speedOn = 20;
    public float timeBetweenHit = 0.3f;
    public float laserSize;


    public override void Starting()
    {
        StartCoroutine(Wait());
    }
    //Récupérer les données sur le joueur.
    IEnumerator Wait()
    {
        yield return 0;
        muzzle = character.gun.muzzle;
        lr = GetComponent<LineRenderer>();
        creGun = character.gun;
        started = true;
        lr.endWidth = laserSize;
        
    }

    public override void Pattern()
    {
        if (started)
        {
            //Si le joueur change d'arme ou meurt,on détruit le laser.
            if (character.life <= 0 || character.gun!=creGun)
            {
                if (character.life <= 0)
                {
                    character.gun.GetComponent<MoustiqueGun>().tir.Remove(gameObject);
                }
                ending = true;
                loadingTime = 0.75f;
                character.lockOn = false;
                character.gun.lockOn = false;
                Destroy(gameObject);
            }

            /*Le principe est que le rayon s'allonge avec le temps, jusqu'à sa limite de portée, afin de ne pas en faire une arme "hitscan".
            Le rayon est également arrêté par les murs, et reprend son allongement une fois qu'il n'y a plus d'obstacle.            */
            
            if (!ending)
            {
                if (!hitted)
                {
                    loadingTime += Time.deltaTime * speedOn;
                }

                if(loadingTime>=maxLength && maxLength > 0)
                {
                    loadingTime = maxLength;
                }

                if (muzzle != null)
                {
                    transform.rotation = character.gun.transform.rotation;
                    transform.Rotate(new Vector3(0, 0, -90));
                }
            }
            //Lorsque l'on relache le bouton, le laser diminue en longueur jusqu'à disparaître. Cela s'effectue bien plus vite que l'allongement, pour éviter d'avoir trop de temps mort.
            else
            {
                if (loadingTime > 0)
                {
                    loadingTime -= Time.deltaTime * speedOn * 2 ;
                }
                else
                {
                    StartCoroutine(Dying());
                    lr.enabled = false;
                }
            }
            Vector2 target = character.mousePos;
            target.Normalize();
            Vector2 lrPos = Vector2.zero;
            RaycastHit2D hit = Physics2D.Raycast(muzzle.position, target, loadingTime, mask);
            if (hit)
            {
                lrPos = hit.point;
                hitted = true;
                loadingTime = Vector2.Distance(muzzle.position, lrPos);
            }
            else
            {
                hitted = false;
                lrPos = (Vector2)muzzle.position+target*loadingTime;
            }

            lr.SetPosition(0, muzzle.position);
            lr.SetPosition(1, lrPos);
            transform.localScale = new Vector3(laserSize, Vector2.Distance(muzzle.position, lrPos) /* (3f / 5f)*/, 1);
            transform.position = muzzle.position;
        }
    }

    //Lorsqu'on touche un autre joueur ou un damageable avec le laser, il est ajouté à la liste.
    public override void HitPlayer(Collider2D collision)
    {
        hitTime.Add(timeBetweenHit);
        isChar.Add(true);
        int a = -1;
        for (int i = 0; i < victimes.Count; i++)
        {
            if (collision.gameObject == victimes[i])
            {
                a = i;
            }
        }

        if (a != -1)
        {
            if (isChar[a])
            {
                victimes[a].GetComponent<Character>().Damage(newDam, player, false, lifesteal);
            }
            else
            {
                victimes[a].GetComponent<Damageable>().Damage(newDam, player, Vector2.zero, false, lifesteal);
            }
        }
    }

    public override void HitDam(Collider2D collision)
    {
        hitTime.Add(timeBetweenHit);
        isChar.Add(false);
        int a = -1;
        for (int i = 0; i < victimes.Count; i++)
        {
            if (collision.gameObject == victimes[i])
            {
                a = i;
            }
        }

        if (a != -1)
        {
                if (isChar[a])
                {
                    victimes[a].GetComponent<Character>().Damage(newDam, player, false, lifesteal);
                }
                else
                {
                    victimes[a].GetComponent<Damageable>().Damage(newDam, player, Vector2.zero, false, lifesteal);
                }
        }
    }

    //Les dégâts sont effectués régulièrement, si la victime reste un temps x dans le rayon.
    private void OnTriggerStay2D(Collider2D collision)
    {
        int a = -1;
        for (int i = 0; i < victimes.Count; i++)
        {
            if (collision.gameObject == victimes[i])
            {
                a = i;
            }
        }

        if (a != -1)
        {

            
            hitTime[a] -= Time.deltaTime;

            if (hitTime[a] <= 0)
            {
                hitTime[a] = timeBetweenHit;
                if (isChar[a])
                {
                    victimes[a].GetComponent<Character>().Damage(newDam, player, false, lifesteal);
                }
                else
                {
                    victimes[a].GetComponent<Damageable>().Damage(newDam, player, Vector2.zero, false, lifesteal);
                }
            }
        }
    }

    public void EndThis()
    {
        ending = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
