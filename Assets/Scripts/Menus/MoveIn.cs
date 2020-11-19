using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIn : MonoBehaviour
{

    /// <summary>
    /// Un simple script pour déplacer un objet entre des positions verticales et horizontales. Sert dans de l'interface.
    /// </summary>
    [SerializeField]
    float speed=0;
    [SerializeField]
    Vector3 start=Vector3.zero, end = Vector3.zero, left = Vector3.zero, right = Vector3.zero, pos = Vector3.zero;
    float timer = 0, timerMove=0;

    bool moving = false;
    bool side = true;

    private void Update()
    {
        if (moving)
        {
            timer += Time.deltaTime * speed;
        }
        else
        {
            timer -= Time.deltaTime * speed;
        }

        if (side)
        {
            timerMove+= Time.deltaTime * speed;
        }
        else
        {
            timerMove -= Time.deltaTime * speed;
        }

        pos.x = Mathf.Lerp(left.x, right.x, timerMove);
        pos.y = Mathf.Lerp(start.y, end.y, timer);
        transform.localPosition = pos;
        if (timer <=0 )
        {
            SideMeBack();
        }
    }

    public void MoveMe()
    {
        if (moving)
        {
            moving = false;
            if (timer > 1)
            {
                timer = 1;
            }
        }
        else
        {
            moving = true;
            if (timer <0)
            {
                timer = 0;
            }
        }
    }

    public void SideMeBack()
    {
        side = true;
        if (timerMove <= 0)
        {
            timerMove = 0;
        }
    }

    public void SideMe()
    {
            side = false;
            if(timerMove>=1)
            {
                timerMove = 1;
            }

            
    }
}
