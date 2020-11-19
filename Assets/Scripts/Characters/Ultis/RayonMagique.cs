using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayonMagique : MonoBehaviour
{
    /// <summary>
    /// L'ulti de la libellule
    /// </summary>
    
    //Le sprite sur lequel le symbole est complet et le portail peut s'ouvrir
    [SerializeField]
    Sprite activation = null;
    //La taille du collider du portail
    CircleCollider2D col = null;
    //Les couleurs que prend le symbole
    [SerializeField]
    Gradient rainbow = null;

    //Stats du spell
    [SerializeField]
    int damage=0;
    [SerializeField]
    int force=0;
    public int source=-1;
    //Le temps du gradient et sa vitesse d'évolution
    public float timer = 0;
    public float speed;
    SpriteRenderer sr;
    //Le prefab et l'objet du portail
    [SerializeField]
    GameObject portalpre = null;
    GameObject portal=null;
    //Le lerp du collider
    float colSize = 0;
    //Si on est en train de finir ou d'avoir une coroutine
    bool end = false;
    bool corou = false;

    [SerializeField]
    AudioSource sound = null;

    //Mise en place des stats de base
    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Starting());
        sound.volume = 1;
        sound.spatialBlend = 1;
        sound.maxDistance = 20;
        sound.minDistance = 8;
    }

    //Récupérer le joueur qui a lancé le spell
    public void SetStats(int sourcing)
    {
        source = sourcing;
    }

    private void Update()
    {
        //Evolution du gradient
        timer += Time.deltaTime * speed;
        if (timer > 1)
        {
            timer = 0;
        }
        sr.material.SetColor("_ColorGrad", rainbow.Evaluate(timer));

        //Une fois le symbole complété, on créé un portail, qu'on garde comme variable, et on active le collider
        if (sr.sprite == activation && portal==null && !corou)
        {
            portal = Instantiate(portalpre, transform.position, Quaternion.identity);
            portal.transform.Translate(new Vector3(0,0,-0.2f));
            col.enabled = true;
            sound.Play();
        }

        //si la collision est active, on augemente colSize, sauf si on est en phase de fin
        if (col.enabled && !end)
        {
            colSize += Time.deltaTime*1.5f;
        }
        if (end)
        {
            colSize -= Time.deltaTime * 1.5f;
        }
        //Le son du spell dépend de la taille du collider.
        sound.volume = Mathf.Lerp(0, 1, colSize);
        //On utilise un lerp pour la taille du collider et la taille du sprite de portail.
        col.radius = Mathf.Lerp(0, 0.32f, colSize);
        float portSize = Mathf.Lerp(0, 6.2f, colSize);
        if (portal != null)
        {
            portal.transform.localScale = new Vector3(portSize, portSize, 1);
        }
        

        //En fin de vie, on détruit l'objet quand le portail est arrivé à une taille de 0.
        if(colSize<=0 && end)
        {
            Destroy(portal);
            Destroy(gameObject);
        }

        //On lance la coroutine de fin une fois que le portail a atteint sa taille maximale.
        if(colSize>=1 && !corou)
        {
            StartCoroutine(Dying());
        }
    }

    //Si on touche un joueur qui n'est pas le lanceur, on fait des dégâts
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var potato = collision.gameObject;
        if (collision.gameObject.tag == "player")
        {
            Character temp = collision.gameObject.GetComponent<Character>();
            if (temp.playerNum != source)
            {
                if (temp.GetComponent<Rigidbody2D>().velocity.magnitude < 0.5f)
                {
                    Vector2 knockBack = temp.transform.position - transform.position;
                    knockBack.Normalize();
                    temp.GetComponent<Rigidbody2D>().AddForce(knockBack * force * 750);
                }
                temp.Damage(damage, source, true, false);
            }
        }
        else if (potato.tag == "damageable")
        {
            if (potato.GetComponent<Damageable>().id != source)
            {
                Vector2 send = potato.transform.position - transform.position;
                send.Normalize();
                send *= force;
                potato.GetComponent<Damageable>().Damage(damage, source, send, true, false);
            }
            
        }
    }

    //Coroutine de destruction
    IEnumerator Dying()
    {
        corou = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetTrigger("end");
        end = true;
        colSize = 1;
    }

    //Une pause de 0.1s au début pour éviter que ça s'autodétruise ou autres problèmes
    IEnumerator Starting()
    {
        corou = true;
        yield return new WaitForSeconds(0.1f);
        corou = false;
    }
}
