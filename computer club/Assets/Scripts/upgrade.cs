using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class upgrade : MonoBehaviour
{
    public string up_name;
    public Transform moneyPoint;
    private int moneyNeed = 10;
    private int curMoneyAmount;
   // private pcBar upgradeBar;
    public int currentGrade;
    private int maxGrade = 2;
    private bool can = true;
    public string text;
    public TextMeshPro meshText;
    public TextMeshPro textt;
    public enum UpgradeType
    {
        carryingAmount,
        movementSpeed,
        serviceSpeed,
        personal,
        All
    }
    public UpgradeType type;
    void Start()
    {
        //PlayerPrefs.DeleteKey(up_name);
        
        if (type == UpgradeType.personal)
        {
            maxGrade = 3;
            moneyNeed = Properties.properties.Storage_Worcker_Price;
        }

        if (type == UpgradeType.All)
        {
            moneyNeed = Properties.properties.Upgrade_Prices[0];
            maxGrade = 9;
            text = "Carrying Amount";
            meshText.text = text;
        }


        // upgradeBar = GameManager.manager.GetUpdrageBar(this);
        // upgradeBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
        textt.text = (moneyNeed - curMoneyAmount) + "";
        meshText.text = text;



        if(PlayerPrefs.HasKey(up_name))
        {
            switch(type)
            {
                case UpgradeType.All:
                   for(int i = 1; i < PlayerPrefs.GetInt(up_name)+1;i++)
                    {
                        switch (i)
                        {
                            case 1:
                                GameManager.manager.player.maxCapasity += 2;
                                GameManager.manager.up = true;
                                text = "Movement Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[1];
                                break;

                            case 2:
                                GameManager.manager.player.movementSpeed += 0.5f;
                                GameManager.manager.up = true;
                                text = "Service Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[2];
                                break;

                            case 3:
                                GameManager.manager.ChangeServiceSpeed(1f);
                                GameManager.manager.up = true;
                                text = "Carrying Amount";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[3]; 
                                break;

                            case 4:
                                GameManager.manager.player.maxCapasity += 2;
                                GameManager.manager.up = true;
                                text = "Movement Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[4];
                                break;

                            case 5:
                                GameManager.manager.player.movementSpeed += 0.5f;
                                GameManager.manager.up = true;
                                text = "Service Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[5];
                                break;

                            case 6:
                                GameManager.manager.ChangeServiceSpeed(0.5f);
                                GameManager.manager.up = true;
                                text = "Carrying Amount";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[6];
                                break;

                            case 7:
                                GameManager.manager.player.maxCapasity += 2;
                                GameManager.manager.up = true;
                                text = "Movement Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[7];
                                break;

                            case 8:
                                GameManager.manager.player.movementSpeed += 0.5f;
                                GameManager.manager.up = true;
                                text = "Service Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[8];
                                Destroy(gameObject);
                                break;

                            case 9:
                                GameManager.manager.player.movementSpeed += 0.5f;
                                GameManager.manager.up = true;
                                text = "Service Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                resetBar();
                                Destroy(gameObject);
                                break;
                        }
                        
                    }

                    currentGrade = PlayerPrefs.GetInt(up_name);
                    break;

                case UpgradeType.personal:
                    Debug.Log("sdgfsdgh");
                    for (int i = 1; i < PlayerPrefs.GetInt(up_name) + 1; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                GameManager.manager.personal[1].gameObject.SetActive(true);
                                text = "PC Cashier";
                                meshText.text = text;
                                GameManager.manager.up = true;
                                curMoneyAmount = 0;
                                moneyNeed = Properties.properties.Cashier_PC_Price;
                                break;

                            case 2:
                                GameManager.manager.personal[2].gameObject.SetActive(true);
                                text = "Accessories Cashier";
                                meshText.text = text;
                                GameManager.manager.up = true;
                                curMoneyAmount = 0;
                                moneyNeed = Properties.properties.Cashier_Items_Price;
                                break;

                            case 3:
                                GameManager.manager.personal[3].gameObject.SetActive(true);
                                text = "Accessories Cashier";
                                meshText.text = text;
                                GameManager.manager.up = true;
                                curMoneyAmount = 0;
                                //moneyNeed += 10;
                                Destroy(gameObject);
                                break;


                        }
                    }
                    currentGrade = PlayerPrefs.GetInt(up_name);
                    break;
            }
        }
        else
        {
            PlayerPrefs.SetInt(up_name,0);
        }
        textt.text = (moneyNeed - curMoneyAmount) + "";
    }

    void Save()
    {

    }
    void resetBar()
    {
        //upgradeBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
        textt.text = (moneyNeed - curMoneyAmount) + "";
    }

    // Update is called once per frame
    void Update()
    {
        //upgradeBar.MoveToClickPoint(moneyPoint.position);
    }
    private float timer = 0.07f;

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
        
        
        if (other.tag == "Cashier" && GameManager.manager.player.isStanding() && maxGrade > currentGrade && can && ((GameManager.manager.tut.oreder == 6 && GameManager.manager.tut.active ) || !GameManager.manager.tut.active))
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
                        StartCoroutine(PlusOneMoney((moneyNeed - curMoneyAmount)));
                      
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
                   if(GameManager.manager.mon > 0)
                    {
                        if((moneyNeed - curMoneyAmount) <= GameManager.manager.mon)
                        {
                            Debug.Log("need money");
                            GameManager.manager.SpawnMoney(transform.position);
                            StartCoroutine(PlusOneMoney((moneyNeed - curMoneyAmount)));
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

           if(!monn)
            {
                mm = moneyNeed;
                monn = true;
            }


            mon += 0.001f * moneyNeed;
        }
    }

    public IEnumerator PlusOneMoney(int m)
    {
        curMoneyAmount+=m;
        GameManager.manager.MinusMoney(m);
       
        if (curMoneyAmount == moneyNeed)
        {
            can = false;



            if (maxGrade > currentGrade)
            {
                currentGrade++;
                GameManager.manager.player.Effect();
                switch (type)
                {

                    case UpgradeType.All:
                        switch (currentGrade)
                        {
                            case 1:
                                GameManager.manager.player.maxCapasity += 1;
                                GameManager.manager.up = true;
                                text = "Movement Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[1];
                                break;

                            case 2:
                                GameManager.manager.player.movementSpeed += 0.3f;
                                GameManager.manager.up = true;
                                text = "Service Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[2];
                                break;

                            case 3:
                                GameManager.manager.ChangeServiceSpeed(1.2f);
                                GameManager.manager.up = true;
                                text = "Carrying Amount";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[3];
                                break;

                            case 4:
                                GameManager.manager.player.maxCapasity += 1;
                                GameManager.manager.up = true;
                                text = "Movement Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[4];
                                break;

                            case 5:
                                GameManager.manager.player.movementSpeed += 0.3f;
                                GameManager.manager.up = true;
                                text = "Service Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[5];
                                break;

                            case 6:
                                GameManager.manager.ChangeServiceSpeed(0.8f);
                                GameManager.manager.up = true;
                                text = "Carrying Amount";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[6];
                                break;

                            case 7:
                                GameManager.manager.player.maxCapasity += 1;
                                GameManager.manager.up = true;
                                text = "Movement Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[7];
                                break;

                            case 8:
                                GameManager.manager.player.movementSpeed += 0.4f;
                                GameManager.manager.up = true;
                                text = "Service Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                moneyNeed = moneyNeed = Properties.properties.Upgrade_Prices[8];
                                break;

                            case 9:
                                GameManager.manager.player.movementSpeed += 0.5f;
                                GameManager.manager.up = true;
                                text = "Service Speed";
                                meshText.text = text;
                                curMoneyAmount = 0;
                                Destroy(gameObject);
                                break;
                        }
                        PlayerPrefs.SetInt(up_name, currentGrade);
                        break;

                    case UpgradeType.carryingAmount:
                        switch (currentGrade)
                        {
                            case 1:
                                GameManager.manager.player.maxCapasity += 1;
                                GameManager.manager.up = true;
                                break;

                            case 2:
                                GameManager.manager.player.maxCapasity += 1;
                                GameManager.manager.up = true;
                                break;
                        }


                        break;

                    case UpgradeType.movementSpeed:
                        switch (currentGrade)
                        {
                            case 1:
                                GameManager.manager.player.movementSpeed += 0.5f;
                                GameManager.manager.up = true;
                                break;

                            case 2:
                                GameManager.manager.player.movementSpeed += 0.5f;
                                GameManager.manager.up = true;
                                break;
                        }


                        break;

                    case UpgradeType.serviceSpeed:
                        switch (currentGrade)
                        {
                            case 1:
                                GameManager.manager.ChangeServiceSpeed(1f);
                                GameManager.manager.up = true;
                                break;

                            case 2:
                                GameManager.manager.ChangeServiceSpeed(0.5f);
                                GameManager.manager.up = true;
                                break;
                        }


                        break;

                    case UpgradeType.personal:
                        switch (currentGrade)
                        {
                            case 1:
                                GameManager.manager.personal[1].gameObject.SetActive(true);
                                text = "PC Cashier";
                                meshText.text = text;
                                GameManager.manager.up = true;
                                curMoneyAmount = 0;
                                moneyNeed = Properties.properties.Cashier_PC_Price;
                                break;

                            case 2:
                                GameManager.manager.personal[2].gameObject.SetActive(true);
                                text = "Accessories Cashier";
                                meshText.text = text;
                                GameManager.manager.up = true;
                                curMoneyAmount = 0;
                                moneyNeed = Properties.properties.Cashier_Items_Price;
                                break;
                           
                            case 3:
                                GameManager.manager.personal[3].gameObject.SetActive(true);
                                text = "Accessories Cashier";
                                meshText.text = text;
                                GameManager.manager.up = true;
                                curMoneyAmount = 0;
                                //moneyNeed += 10;
                                Destroy(gameObject);
                                break;

                         
                        }
                        PlayerPrefs.SetInt(up_name, currentGrade);
                        break;
                        
                }

                StartCoroutine(secondUpgrade());
            }

        }
        yield return new WaitForSeconds(0.25f);
       // upgradeBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
        textt.text = (moneyNeed - curMoneyAmount) + "";
       
        if (curMoneyAmount == moneyNeed)
        {

            //upgradeBar.DisableBar();


        }
    }

    public IEnumerator secondUpgrade()
    {
        yield return new WaitForSeconds(1f);
        if (maxGrade > currentGrade)
        {
            can = true;
            
            resetBar();
        }



    }
}
