using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private Vector3 pointAB;
    private Vector3 pointBC;
    private Vector3 pointAB_BC;
    private float interpolateAmount;
    private bool active;
    private float speed = 2;
    public void SetPos(Vector3 from, Vector3 to)
    {
        pointA = from;
        pointB = GetMiddlePoit(from, to, 0.5f);
        pointC = to;
        active = true;
    }

    bool toPlayer = false;
    public void SetPos(Vector3 from)
    {
        
        pointA = from;
        //pointB = GetMiddlePoit(from, , 0.5f);
       // pointC = to;
        active = true;
        toPlayer = true;
    }
    Vector3 GetMiddlePoit(Vector3 v1, Vector3 v2, float y)
    {
        Vector3 ret = Vector3.zero;

        ret.x = v1.x + (v2.x - v1.x) / 2;
        ret.y = v1.y + (v2.y - v1.y) / 2;
        ret.z = v1.z + (v2.z - v1.z) / 2;
        ret.y += y;

        return ret;
    }

    void Update()
    {
        if (toPlayer)
        {
            if (Vector3.Distance(transform.position, GameManager.manager.player.moneyPoint.position) > 0.2f)
            {
                if (active)
                {
                    interpolateAmount = (interpolateAmount + Time.deltaTime * speed) % 1f;
                    Vector3 mid = GetMiddlePoit(pointA, GameManager.manager.player.moneyPoint.position, 0.5f);
                    pointAB = Vector3.Lerp(pointA, mid, interpolateAmount);
                    pointBC = Vector3.Lerp(mid, GameManager.manager.player.moneyPoint.position, interpolateAmount);
                    transform.position = Vector3.Lerp(pointAB, pointBC, interpolateAmount);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, pointC) > 0.1f)
            {
                if (active)
                {
                    interpolateAmount = (interpolateAmount + Time.deltaTime * speed) % 1f;
                    pointAB = Vector3.Lerp(pointA, pointB, interpolateAmount);
                    pointBC = Vector3.Lerp(pointB, pointC, interpolateAmount);
                    transform.position = Vector3.Lerp(pointAB, pointBC, interpolateAmount);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
          
           
    }
}
