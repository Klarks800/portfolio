using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class loadingBoard : MonoBehaviour
{
    public PlayerController.OnTimerEndBehavior typeOfItem;
    public deliveredItem curItem;
    public List<deliveredItem> Items;
    public List<bool> isItemAvalible;
    public Transform money_point;
    //private pcBar moneyBar;
    public bool isEmpty;
    public int moneyNeed = 10;
    public int curMoneyAmount;
    private float timer = 0.05f;
    public bool dil;
    public SpriteRenderer s;
    public Transform point;
    public TextMeshPro text;
    public GameObject g;
    public int lvl;
    public Transform from;
    public upgradeCylinder uc;
    public ParticleSystem ps;
    public void RemoveItem(deliveredItem deliItem)
    {
      
        isItemAvalible[Items.IndexOf(deliItem)] = false;
        deliItem.gameObject.SetActive(false);
        isEmpty = isStashEmpty();
        dil = isStashEmpty();
        if (isEmpty)
        {

            StartCoroutine(Refresh());

            // Debug.Log("AQADAOIOIDJP");
          
            // g.SetActive(true);
            //moneyBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
            //text.text = (moneyNeed - curMoneyAmount) + "";
        }
    }

    public int GetFirstAvalible()
    {
        for(int i = 0; i < Items.Count; i++)
        {
            if (Items[i].gameObject.activeSelf)
            {
                curItem = Items[i];
                return Items[i].lvl;
            
            }
        }

       
        /*
        dil = false;
            //moneyBar.DisableBar();
            g.SetActive(false);
            timer = 0.15f;
            curMoneyAmount = 0;
           // Debug.Log("AQADAOIOIDJP");
            TruckDilivery();
            StartCoroutine(Dilivery());
         */

        return 0;
    }

    public bool isAvalible()
    {
       

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
    private void Start()
    {
       if(lvl == 0)
        {
            switch(typeOfItem)
            {
                case PlayerController.OnTimerEndBehavior.PCGetUp:
                    moneyNeed = Properties.properties.Loading1Grade_PC_Price;
                    break;

                case PlayerController.OnTimerEndBehavior.MouseGetUp:
                    moneyNeed = Properties.properties.Loading1Grade_Mouse_Price;
                    break;

                case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                    moneyNeed = Properties.properties.Loading1Grade_Keyboard_Price;
                    break;

                case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                    moneyNeed = Properties.properties.Loading1Grade_Monitor_Price;
                    break;
            }
        }
       
        //moneyBar = GameManager.manager.GetMoneyBar(this);
        //moneyBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
        text.text = (moneyNeed - curMoneyAmount) + "";
        s.sprite = GameManager.manager.getImage(typeOfItem);
        isEmpty = isStashEmpty();
        dil = isStashEmpty();
       
        if(dil)
        {
            dil = false;
            //moneyBar.DisableBar();
            g.SetActive(false);
            timer = 0.15f;
            curMoneyAmount = 0;
            // Debug.Log("AQADAOIOIDJP");
            TruckDilivery();
            StartCoroutine(Dilivery());
        }
        
        //for (int i = 0; i < Items.Count; i++)
            //Items[i].Set(typeOfItem, lvl);
    }

    private void Update()
    {
        if (isEmpty)
        {
            //moneyBar.MoveToClickPoint(money_point.position);
            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cashier" && GameManager.manager.player.isAvalibleSpace() && !isEmpty)
        {
            GameManager.manager.player.ActivateTimer();
        }
    }

   
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cashier"  )//
        {
            if (isEmpty)
            {
             /*   
                if(dil && GameManager.manager.player.isStanding() && ((GameManager.manager.tut.oreder == 1 && GameManager.manager.tut.active) || !GameManager.manager.tut.active))
                {
                    
                    
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else
                    {
                        timer = 0.15f;
                        if (GameManager.manager.mon > 0)
                        {
                            GameManager.manager.SpawnMoney(money_point.position);
                            StartCoroutine(PlusOneMoney());
                        }
                    }
                }
               */
            }
            else
            {
               
                
                if (GameManager.manager.player.isAvalibleSpace())
                {
                    if (!GameManager.manager.player.active)
                        GameManager.manager.player.ActivateTimer();

                    GameManager.manager.player.lvl = GetFirstAvalible();
                    GameManager.manager.player.curLoadingBoard = this;
              
                    GameManager.manager.player.timer(typeOfItem);
                }
            }
          


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cashier")
        {
            GameManager.manager.player.curLoadingBoard = null;
            GameManager.manager.player.DisableTimer();
        }

    }




    public void Remove()
    {


        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].gameObject.SetActive(false);
            
        }

       
    }

    public void Ref()
    {
        StartCoroutine(Refresh());
    }

    public IEnumerator Refresh()
    {
Debug.Log("qq");
        dil = false;

        yield return new WaitForSeconds(0.25f);

        Debug.Log("ss");

        //moneyBar.DisableBar();
        g.SetActive(false);
        timer = 0.05f;
        curMoneyAmount = 0;
        // Debug.Log("AQADAOIOIDJP");
        TruckDilivery();
        StartCoroutine(Dilivery());




    }
    public IEnumerator PlusOneMoney()
    {
        //Debug.Log(curMoneyAmount+"  "+moneyNeed);
        curMoneyAmount++;
        if (curMoneyAmount == moneyNeed)
        {
            dil = false;
        }
        yield return new WaitForSeconds(0.25f);
        GameManager.manager.MinusMoney(1);
        if (curMoneyAmount == moneyNeed )
        {
          
            //moneyBar.DisableBar();
            g.SetActive(false);
            timer = 0.15f;
            curMoneyAmount = 0;
           // Debug.Log("AQADAOIOIDJP");
            TruckDilivery();
            StartCoroutine(Dilivery());
        }

        if(dil)
        {
            //moneyBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
            text.text = (moneyNeed - curMoneyAmount) + "";
        }
           
    }


    public bool isStashEmpty()
    {
        bool b = true;


        for(int i = 0; i < Items.Count; i++)
        {
            if (Items[i].gameObject.activeSelf)
            {
                b = false;
                break;
            }
        }

        return b;
    }
    public Animator truck;
    void TruckDilivery()
    {
     
        truck.Play("truck_anim");
    }
    float tt = 0;
    public IEnumerator Dilivery()
    {
        
         yield return new WaitForSeconds(2.15f);
       
        switch(lvl)
        {
            case 0:

                switch (typeOfItem)
                {
                    case PlayerController.OnTimerEndBehavior.PCGetUp:
                        moneyNeed = Properties.properties.Loading1Grade_PC_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.MouseGetUp:
                        moneyNeed = Properties.properties.Loading1Grade_Mouse_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                        moneyNeed = Properties.properties.Loading1Grade_Keyboard_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                        moneyNeed = Properties.properties.Loading1Grade_Monitor_Price;
                        break;
                }
             
                break;

            case 1:
                switch (typeOfItem)
                {
                    case PlayerController.OnTimerEndBehavior.PCGetUp:
                        moneyNeed = Properties.properties.Loading2Grade_PC_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.MouseGetUp:
                        moneyNeed = Properties.properties.Loading2Grade_Mouse_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                        moneyNeed = Properties.properties.Loading2Grade_Keyboard_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                        moneyNeed = Properties.properties.Loading2Grade_Monitor_Price;
                        break;
                }
                
                break;

            case 2:
                switch (typeOfItem)
                {
                    case PlayerController.OnTimerEndBehavior.PCGetUp:
                        moneyNeed = Properties.properties.Loading3Grade_PC_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.MouseGetUp:
                        moneyNeed = Properties.properties.Loading3Grade_Mouse_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                        moneyNeed = Properties.properties.Loading3Grade_Keyboard_Price;
                        break;

                    case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                        moneyNeed = Properties.properties.Loading3Grade_Monitor_Price;
                        break;
                }
               
                break;
        }
        tt = 0;
        for (int i = 0; i < Items.Count; i++)
            Items[i].Set(typeOfItem, lvl);
        isEmpty = false;

        for (int i = 0; i < Items.Count; i++)
        {
            
            tt += 0.15f;

            StartCoroutine( Anim(Items[i].gameObject, tt));
        }

       

    }

    public IEnumerator Anim(GameObject iaa, float d)
    {
        yield return new WaitForSeconds(d);
        if (iaa.gameObject.GetComponent<ItemAnim>() == null)
        {
            ItemAnim ia = iaa.gameObject.AddComponent<ItemAnim>();
            ia.SetPos(from.position);
            iaa.SetActive(true);
        }
        else
        {
            ItemAnim ia = iaa.gameObject.GetComponent<ItemAnim>();
            ia.SetPos(from.position);
            iaa.SetActive(true);
        }
    }
}
