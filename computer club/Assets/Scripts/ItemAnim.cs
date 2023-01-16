using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnim : MonoBehaviour
{
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private Vector3 pointAB;
    private Vector3 pointBC;

    private float interpolateAmount;
    private bool active;
    private float speed = 2;
    public void SetPos( Vector3 from)
    {
        pointA = from;
        pointB = GetMiddlePoit(from,transform.position, 0.5f);
        pointC = transform.position;
        transform.position = from;
        active = true;
        interpolateAmount = 0;
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
    float dist = 9999;
    void FixedUpdate()
    {
        if (active)
        {
            if (Vector3.Distance(transform.position, pointC) > 0.1f)
            {


                interpolateAmount = (interpolateAmount + Time.deltaTime * speed) % 1f;
                pointAB = Vector3.Lerp(pointA, pointB, interpolateAmount);
                pointBC = Vector3.Lerp(pointB, pointC, interpolateAmount);
                transform.position = Vector3.Lerp(pointAB, pointBC, interpolateAmount);
               
                if(Vector3.Distance(transform.position, pointC) > dist)
                {
                    transform.position = pointC;
                    active = false;
                    dist = 9999;
                }
                else
                dist = Vector3.Distance(transform.position, pointC);
            }
        }
        
   
    }
}
