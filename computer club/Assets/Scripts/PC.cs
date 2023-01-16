using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PC : MonoBehaviour
{
    public string pc_name;
    public bool isPCBuilded;
    public bool isPCTaken;
    public GameObject pc;
    public GameObject pcPlane;
    public Transform customerPoint;
    public Transform sittingPoint;
    public Transform moneyBarPoint;
    public Customer cur_customer;
    //private pcBar PcBar;
    private int moneyNeed = 10;
    private int curMoneyAmount;
    public TextMeshPro text;
    public upgradeCylinder uc;
    public ParticleSystem ps;
   
    public int GetLvL()
    {
        for(int i = 0; i < 3;i++)
        {
            if (uc.pcc[i].activeSelf)
                return i;
        }
      

        return 0;
    }

    
    void Start()
    {
        moneyNeed =  Properties.properties.PC_Prices[GameManager.manager.PC.IndexOf(this)];
        //PcBar = GameManager.manager.GetPCBar(this);
        //PcBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
        text.text = (moneyNeed - curMoneyAmount) + "";
        if (PlayerPrefs.HasKey(pc_name))//isBuild*lvl
        {
            string s = PlayerPrefs.GetString(pc_name);
            string[] sArray;
            sArray = s.Split('*');

            if(bool.Parse(sArray[0]))
            {
                isPCBuilded = true;
                GameManager.manager.max += 1;
                GameManager.manager.RecalculatePC();
                pc.SetActive(true);
                pcPlane.SetActive(false);

                uc.pcc[0].SetActive(false);
                uc.pcc[1].SetActive(false);
                uc.pcc[2].SetActive(false);

                uc.pcc[int.Parse(sArray[1])].SetActive(true);

                //Debug.Log(int.Parse(sArray[1]));
                switch(int.Parse(sArray[1]))
                {
                    case 0:
                        uc.currentGrade = 0;
                        uc.moneyNeed = Properties.properties.PCUpgrade1_Price;
                        uc.Updt();
                        break;
                    case 1:
                        //Debug.Log("Hello");
                        uc.currentGrade = 1;
                        //Debug.Log(Properties.properties.PCUpgrade2_Price+" grade2" );
                        uc.moneyNeed = Properties.properties.PCUpgrade2_Price;
                        uc.Updt();
                        break;
                    case 2:
                        Destroy(uc.gameObject);
                        break;

                }

               
                  

                GameManager.manager.RecalculatePC();

            }
          
            
        }
        else
        {
            PlayerPrefs.SetString(pc_name, isPCBuilded+"*"+ (uc.pcc[0].activeSelf?"0":(uc.pcc[1].activeSelf?"1":"2")));
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(pc_name, isPCBuilded + "*" + (uc.pcc[0].activeSelf ? "0" : (uc.pcc[1].activeSelf ? "1" : "2")));
    }
    private void Update()
    {
        if (!isPCBuilded)
        {
            //PcBar.MoveToClickPoint(moneyBarPoint.position);
           
        }
        
    }
    public void SetCustomer(Customer customer)
    {
        cur_customer = customer;
        isPCTaken = true;
    }

    public void LeavePC(Customer customer)
    {
        cur_customer = null;
        isPCTaken = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cashier" && cur_customer != null)
        {
            for(int i = 0; i < GameManager.manager.player.carryingItems.Count; i++)
            {
                if(GameManager.manager.player.carryingItems[i].type == cur_customer.orderedFood)
                {
                    cur_customer.DeliveredFood();
                    GameManager.manager.player.GiveAwayItem(i);
                    break;
                }
            }
        }
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
        if(other.tag == "Cashier" && !isPCBuilded && GameManager.manager.player.isStanding() && ((GameManager.manager.tut.oreder == 4 && GameManager.manager.tut.active) || !GameManager.manager.tut.active))
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
                    if (GameManager.manager.mon > 0)
                    {
                        if ((moneyNeed - curMoneyAmount) <= GameManager.manager.mon)
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

            if (!monn)
            {
                mm = moneyNeed;
                monn = true;
            }


            mon += 0.001f * moneyNeed;
        }
    }

    



    public IEnumerator PlusOneMoney(int m)
    {
        curMoneyAmount += m;
        GameManager.manager.MinusMoney(m);
        if (curMoneyAmount == moneyNeed)
        {
            isPCBuilded = true;
            GameManager.manager.max += 1;
        }
        yield return new WaitForSeconds(0.25f);
        //PcBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
        text.text = (moneyNeed - curMoneyAmount) + "";

   
        if (curMoneyAmount == moneyNeed)
        {
            //PcBar.DisableBar();
            if(GameManager.manager.ind > 1)
            GameManager.manager.RecalculatePC();
            pc.SetActive(true);
            pcPlane.SetActive(false);
            Save();
        }
    }
}
