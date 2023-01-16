using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class upgradeCylinder : MonoBehaviour
{
    public Transform pc;
    public PC pc_;
    public TextMeshPro text;
    public float curMoneyAmount;
    public float moneyNeed = 10;
    public int maxGrade = 2;
    public int currentGrade = 0;

    void Start()
    {
        
        switch (t)
        {
            case type.pc:
                if (pcc[0].activeSelf)
                    moneyNeed = Properties.properties.PCUpgrade1_Price;
                break;

            case type.load:
                if (lb.lvl == 0)
                {
                    switch(lb.typeOfItem)
                    {
                        case PlayerController.OnTimerEndBehavior.PCGetUp:
                            moneyNeed = Properties.properties.LoadingUpgrade1_PC_Price;
                            break;

                        case PlayerController.OnTimerEndBehavior.MouseGetUp:
                            moneyNeed = Properties.properties.LoadingUpgrade1_Mouse_Price;
                            break;

                        case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                            moneyNeed = Properties.properties.LoadingUpgrade1_Keyboard_Price;
                            break;

                        case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                            moneyNeed = Properties.properties.LoadingUpgrade1_Monitor_Price;
                            break;
                    }
                }
                   
                break;
        }

        text.text = (moneyNeed - curMoneyAmount) + "";
        StartCoroutine(start());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    float timer = 0.05f;
    public float mon = 1;
    bool monn = false;
    float mm;
    private void OnTriggerExit(Collider other)
    {
        mon = 1;
        monn = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cashier" && GameManager.manager.player.isStanding() && can && !GameManager.manager.tut.active)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0.07f;

                if (GameManager.manager.mon >= mon)
                {
                    if ((curMoneyAmount + mon) > moneyNeed)
                    {
                        Debug.Log("need money");
                        GameManager.manager.SpawnMoney(transform.position);
                        StartCoroutine(PlusOneMoney((int)(moneyNeed - curMoneyAmount)));

                    }
                    else
                    {
                        Debug.Log("def money");
                        GameManager.manager.SpawnMoney(transform.position);
                        StartCoroutine(PlusOneMoney((int)mon));
                    }
                }
                else
                {
                    if (GameManager.manager.mon > 0)
                    {
                        if ((moneyNeed - curMoneyAmount) <= GameManager.manager.mon)
                        {
                            Debug.Log("need money");
                            GameManager.manager.SpawnMoney(transform.position);
                            StartCoroutine(PlusOneMoney((int)(moneyNeed - curMoneyAmount)));
                        }
                        else
                        {
                            Debug.Log("last money");
                            GameManager.manager.SpawnMoney(transform.position);
                            StartCoroutine(PlusOneMoney(GameManager.manager.mon));
                        }

                    }
                }
            }

            if (!monn)
            {
                mm = moneyNeed;
                monn = true;
            }


            mon += 0.001f * moneyNeed;
        }
    }


    public enum type
    {
        pc,
        load
    }
    public type t;
    public void Updt()
    {
        text.text = (moneyNeed - curMoneyAmount) + "";
    }

    public List<GameObject> pcc;
    public loadingBoard lb;
    public IEnumerator PlusOneMoney(int m)
    {
        curMoneyAmount += m;
        GameManager.manager.MinusMoney(m);
        if (curMoneyAmount == moneyNeed)
        {
            can = false;
            currentGrade++;
            switch (t)
            {
                case type.pc:
                    pcc[0].gameObject.SetActive(false);
                    pcc[1].gameObject.SetActive(false);
                    pcc[2].gameObject.SetActive(false);

                    pcc[currentGrade].gameObject.SetActive(true);
                    curMoneyAmount = 0;
                    moneyNeed = Properties.properties.PCUpgrade2_Price ;
                    pc_.Save();
                    pc_.ps.Play();
                    break;

                case type.load:
                    lb.lvl++;
                    
                    switch(lb.lvl)
                    {
                        case 1:

                            switch(lb.typeOfItem)
                            {
                                case PlayerController.OnTimerEndBehavior.PCGetUp:
                                    lb.moneyNeed = Properties.properties.Loading2Grade_PC_Price;
                                    break;

                                case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                    lb.moneyNeed = Properties.properties.Loading2Grade_Mouse_Price;
                                    break;

                                case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                    lb.moneyNeed = Properties.properties.Loading2Grade_Keyboard_Price;
                                    break;

                                case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                    lb.moneyNeed = Properties.properties.Loading2Grade_Monitor_Price;
                                    break;
                            }
                          
                            lb.text.text = (lb.moneyNeed - lb.curMoneyAmount) + "";
                            break;

                        case 2:

                            switch (lb.typeOfItem)
                            {
                                case PlayerController.OnTimerEndBehavior.PCGetUp:
                                    lb.moneyNeed = Properties.properties.Loading3Grade_PC_Price;
                                    break;

                                case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                    lb.moneyNeed = Properties.properties.Loading3Grade_Mouse_Price;
                                    break;

                                case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                    lb.moneyNeed = Properties.properties.Loading3Grade_Keyboard_Price;
                                    break;

                                case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                    lb.moneyNeed = Properties.properties.Loading3Grade_Monitor_Price;
                                    break;
                            }
                           
                            lb.text.text = (lb.moneyNeed - lb.curMoneyAmount) + "";
                            break;
                    }
                    
                    curMoneyAmount = 0;

                    lb.ps.Play();
                    switch (lb.typeOfItem)
                    {
                        case PlayerController.OnTimerEndBehavior.PCGetUp:
                            moneyNeed = Properties.properties.LoadingUpgrade2_PC_Price;
                            break;

                        case PlayerController.OnTimerEndBehavior.MouseGetUp:
                            moneyNeed = Properties.properties.LoadingUpgrade2_Mouse_Price;
                            break;

                        case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                            moneyNeed = Properties.properties.LoadingUpgrade2_Keyboard_Price;
                            break;

                        case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                            moneyNeed = Properties.properties.LoadingUpgrade2_Monitor_Price;
                            break;
                    }
                    lb.Remove();
                    lb.Ref();



                    GameManager.manager.PCshelf[GameManager.manager.loadingBoards.IndexOf(lb)].Save();
                  
                    
                    break;
            }
            
            text.text = (moneyNeed - curMoneyAmount) + "";
            StartCoroutine(secondUpgrade());
        
        
        }
        
        
        if(currentGrade == maxGrade)
        {
            Destroy(gameObject);
        }
        
        
        yield return new WaitForSeconds(0.25f);

        text.text = (moneyNeed - curMoneyAmount) + "";
     

       // Debug.Log(currentGrade+"  grade "+ curMoneyAmount + "  money");
        if (curMoneyAmount == moneyNeed)
        {

            if (maxGrade > currentGrade)
            {
              
            }
           
        }
    }
    public bool can = false;

    public IEnumerator start()
    {
        yield return new WaitForSeconds(1f);
      
            can = true;
    



    }
    public IEnumerator secondUpgrade()
    {
        yield return new WaitForSeconds(1f);
        if (maxGrade > currentGrade)
        {
            can = true;
            //curMoneyAmount = 0;
            //moneyNeed += 10;
           

        }



    }
}
