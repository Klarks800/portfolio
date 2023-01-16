using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pcBar : MonoBehaviour
{
    public Text moneyAmount;
    public Image img;
    public RectTransform rt;
    private RectTransform canvas;
    private Camera camera;
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        camera = Camera.main;
    }

    public void SetMoneyAmount(int money)
    {
        img.gameObject.SetActive(true);
        moneyAmount.text = money.ToString();
       // Debug.Log(money);
    }

    public void DisableBar()
    {
        img.gameObject.SetActive(false);
    }


    public void MoveToClickPoint(Vector3 objectTransformPosition)
    {
        Vector2 uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);

        Vector2 ViewportPosition = camera.WorldToViewportPoint(objectTransformPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvas.sizeDelta.x, ViewportPosition.y * canvas.sizeDelta.y);

        rt.localPosition = proportionalPosition - uiOffset;
    }
   
}
