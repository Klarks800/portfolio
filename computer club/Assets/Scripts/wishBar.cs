using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wishBar : MonoBehaviour
{
    public RectTransform rt;
    public List<GameObject> behaviors;
    public GameObject img;

    private RectTransform canvas;
    private Camera camera;
    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        camera = Camera.main;

    }

    public void SetBehavior(int b)
    {


        for (int i = 0; i < behaviors.Count; i++)
        {
            behaviors[i].SetActive(false);
        }

        behaviors[b].SetActive(true);

    }

    public void DisableBar()
    {
        img.SetActive(false);
    }
    public void MoveToClickPoint(Vector3 objectTransformPosition)
    {
        if(canvas == null)
            canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();

        if (camera == null)
            camera = Camera.main;

        Vector2 uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);

        Vector2 ViewportPosition = camera.WorldToViewportPoint(objectTransformPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvas.sizeDelta.x, ViewportPosition.y * canvas.sizeDelta.y);

        rt.localPosition = proportionalPosition - uiOffset;
    }
}
