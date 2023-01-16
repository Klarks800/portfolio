using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodMachine : MonoBehaviour
{
    public int typeOfFood;
    public Transform bar_point;
    public bar food_bar;
    public Transform point;
    void Start()
    {
        food_bar = GameManager.manager.GetBar(this);
        food_bar.Set(typeOfFood);
    }

    void Update()
    {
        food_bar.MoveToClickPoint(bar_point.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cashier" && GameManager.manager.player.isAvalibleSpace())
        {
            GameManager.manager.player.ActivateTimer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cashier" && GameManager.manager.player.isAvalibleSpace())
        {
           if(!GameManager.manager.player.active)
                GameManager.manager.player.ActivateTimer();


            switch (typeOfFood)
            {
                case 1:
                    GameManager.manager.player.timer(PlayerController.OnTimerEndBehavior.BurgerCarrying);
                    break;

                case 2:
                    GameManager.manager.player.timer(PlayerController.OnTimerEndBehavior.PizzaCarrying);
                    break;

                case 3:
                    GameManager.manager.player.timer(PlayerController.OnTimerEndBehavior.CackeCarrying);
                    break;

                case 4:
                    GameManager.manager.player.timer(PlayerController.OnTimerEndBehavior.SodaCarrying);
                    break;
            }          
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if(other.tag == "Cashier")
        {
            GameManager.manager.player.DisableTimer();
        }
        
    }
}
