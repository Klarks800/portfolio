using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashBucket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cashier" && GameManager.manager.player.carryingItems.Count > 0)
        {
            GameManager.manager.player.ActivateTimer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cashier" && GameManager.manager.player.carryingItems.Count > 0)
        {
            if (!GameManager.manager.player.active)
                GameManager.manager.player.ActivateTimer();

            GameManager.manager.player.timer(PlayerController.OnTimerEndBehavior.TrashBucket);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cashier")
        {

            GameManager.manager.player.DisableTimer();
        }

    }
}
