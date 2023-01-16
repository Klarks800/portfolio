using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class indicator : MonoBehaviour
{
    public RectTransform rt;
    public List<GameObject> behaviors;
    public GameObject indic;
    public Image timer;
    private RectTransform canvas;
    private Camera camera;
    private int behavior;
    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        camera = Camera.main;
        DisableIndicator();
    }

    public void SetBehavior(int b)
    {
        indic.SetActive(true);

        for (int i = 0; i < behaviors.Count; i++)
        {
            behaviors[i].SetActive(false);
        }

        behaviors[b].SetActive(true);
        behavior = b;
    }

    public void DisableIndicator()
    {
        indic.SetActive(false);
    }

    public void SetGreenColor()
    {
        timer.color = new Color(0.32f, 0.81f, 0.33f);
    }

    public void SetOrangeColor()
    {
        timer.color = new Color(0.81f, 0.58f, 0.32f);
    }

    public void MoveToClickPoint(Vector3 objectTransformPosition)
    {
        Vector2 uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);

        Vector2 ViewportPosition = camera.WorldToViewportPoint(objectTransformPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvas.sizeDelta.x, ViewportPosition.y * canvas.sizeDelta.y);

        rt.localPosition = proportionalPosition - uiOffset;
    }
}
