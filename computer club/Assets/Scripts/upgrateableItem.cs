using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrateableItem : MonoBehaviour
{
    public List<GameObject> items;
    public void Set(int n)
    {
 

        for (int i = 0; i < items.Count; i++)
            items[i].SetActive(false);


        items[n].SetActive(true);

    }
}
