using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentRock : Projectile
{

    /// <summary>
    /// Les projectiles de base du trident. Invisible et sans hitbox.
    /// </summary>
    /// 
    public float distanceExplo;
    public LayerMask masks;
    public Vector3 targetPoint;
    public Vector3 startPos;
    float timer = 0;
    public GameObject explosion;
    bool started = false;
    public float angle;

    
    //Au moment où on les lance, ils déterminent un point d'impact dans leur direction de tir. Soit une distance, soit le premier obstacle venu.
    public void SetTrident(float ang)
    {
        angle = ang;
        startPos = transform.position;
        Vector2 targeting = Vector2.zero;
        float rad = angle * Mathf.Deg2Rad;
        targeting.x = Mathf.Cos(rad);
        targeting.y = Mathf.Sin(rad);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targeting, distanceExplo, masks);
        if (hit)
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = transform.position + (new Vector3(targeting.x, targeting.y, -5)* distanceExplo);
        }
        started = true;
    }

    //Ensuite ils se déplacent jusqu'au point d'arrivée, avant de se détruire pour créer les TridentExplo.
    private void Update()
    {
        if (started)
        {

            timer += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, targetPoint, timer);
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);

            if (timer >= 1)
            {
                GameObject temp = Instantiate(explosion, transform.position, Quaternion.identity);
                temp.GetComponent<Trap>().SetTrap(player, lifesteal);
                Destroy(gameObject);
            }
        }
    }
}
