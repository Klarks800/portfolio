using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputMove : MonoBehaviour
{
    public Joystick jst;
    public RectTransform joystick;
    public RectTransform canvas;
    public Image[] img = new Image[2];

    bool touch = false;


    void Update()
    {


        if (Input.GetMouseButton(0))
        {
            img[0].enabled = true;
            img[1].enabled = true;

            Vector2 v2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (!touch)
            {
                touch = true;
                Vector2 lp;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.gameObject.GetComponent<RectTransform>(), v2, null, out lp);
                joystick.anchoredPosition = lp;
            }
            else
            {
                PointerEventData pData = new PointerEventData(EventSystem.current);
                pData.position = v2;
                jst.OnPointerDown(pData);
            }
        }
        else
        {
            PointerEventData pData = new PointerEventData(EventSystem.current);
            pData.position = Vector2.zero;
            jst.OnPointerUp(pData);
            touch = false;
            img[0].enabled = false;
            img[1].enabled = false;
        }




    }
}
