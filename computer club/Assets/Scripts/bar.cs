using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bar : MonoBehaviour
{
    public Sprite[] sprites;
    public Image img;
    public Image color;
    public RectTransform rt;
    private RectTransform canvas;
    private Camera camera;
    private int index;
    public List<Color> colors = new List<Color>();
    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        camera = Camera.main;
    }

    public void Set(int i)
    {
        img.sprite = sprites[i];

        switch(i)
        {
            case 1:
                color.color = colors[0];
                break;

            case 2:
                color.color = colors[1];
                break;

            case 3:
                color.color = colors[2];
                break;

            case 4:
                color.color = colors[3];
                break;
        }

        index = i;
    }

    public void MoveToClickPoint(Vector3 objectTransformPosition)
    {
        Vector2 uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);

        Vector2 ViewportPosition = camera.WorldToViewportPoint(objectTransformPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvas.sizeDelta.x, ViewportPosition.y * canvas.sizeDelta.y);

        rt.localPosition = proportionalPosition - uiOffset;
        
    }
}
