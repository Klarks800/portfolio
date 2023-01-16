using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerDelayBar : MonoBehaviour
{
    public GameObject bar;
    public Image timerWheel;
    public RectTransform rt;
    private RectTransform canvas;
    private Camera camera;

    
    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        camera = Camera.main;
        bar.SetActive(false);
    }
    
    public void ActivateTimer()
    {
        bar.SetActive(true);
    }
    
    public void DisableTimer()
    {
        bar.SetActive(false);
    }

    public void SetTimer(float t)
    {
        timerWheel.fillAmount = 1 - (t / GameManager.manager.serviceSpeed);
    }

  
    public void MoveToClickPoint(Vector3 objectTransformPosition)
    {
        Vector2 uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);

        Vector2 ViewportPosition = camera.WorldToViewportPoint(objectTransformPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvas.sizeDelta.x, ViewportPosition.y * canvas.sizeDelta.y);

        rt.localPosition = proportionalPosition - uiOffset;
    }
}
