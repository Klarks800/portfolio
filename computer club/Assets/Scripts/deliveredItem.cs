using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deliveredItem : MonoBehaviour
{
    public PlayerController.OnTimerEndBehavior typeOfItem;
    public List<upgrateableItem> items;
    public int lvl;
    //private loadingBoard loadingBoard;
    //private 
    private void Start()
    {
        //loadingBoard = GetComponentInParent<loadingBoard>();
    }

    public void Set(PlayerController.OnTimerEndBehavior type,int n)
    {
        typeOfItem = type;

        for (int i = 0; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(false);
        }

        lvl = n;

        switch (typeOfItem)
        {
            case PlayerController.OnTimerEndBehavior.PCGetUp:
                items[0].gameObject.SetActive(true);
                items[0].Set(n);
                break;

            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                items[1].gameObject.SetActive(true);
                items[1].Set(n);
                break;

            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                items[2].gameObject.SetActive(true);
                items[2].Set(n);
                break;

            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                items[3].gameObject.SetActive(true);
                items[3].Set(n);
                break;
        }

    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cashier")
        {
            GameManager.manager.player.ActivateTimer();
            loadingBoard.curItem = this;
            GameManager.manager.player.curLoadingBoard = loadingBoard;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cashier")
        {
            GameManager.manager.player.timer(typeOfItem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cashier")
        {
            GameManager.manager.player.DisableTimer();
            loadingBoard.curItem = null;
            GameManager.manager.player.curLoadingBoard = null;
        }

    }
    */
}
