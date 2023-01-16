using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PCShelf : MonoBehaviour
{
    public string shelf_name;
    public List<isAvalibe> customerPoint;
    public GameObject shelfBuy;
    public GameObject shelf_plane;
    public bool isShelfBulded;
    public List<deliveredItem> shelf;
    public List<bool> avalible_item;
    public PlayerController.OnTimerEndBehavior shelfType;
   // private pcBar ShelfBar;
    public Transform money_point;
    private int moneyNeed = 10;
    private int curMoneyAmount;
    public SpriteRenderer s1;
    public SpriteRenderer s2;
    public TextMeshPro text;

    public bool isAnyone()
    {
        for (int i = 0; i < customerPoint.Count; i++)
            if (customerPoint[i].isAvalible)
                return true;

        return false;
    }

    private void Start()
    {
       switch(shelfType)
        {
            case PlayerController.OnTimerEndBehavior.PCGetUp:
                moneyNeed = Properties.properties.Shelf_Build_PC_price;
                break;

            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                moneyNeed = Properties.properties.Shelf_Build_Mouse_price;
                break;

            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                moneyNeed = Properties.properties.Shelf_Build_Keyboard_price;
                break;

            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                moneyNeed = Properties.properties.Shelf_Build_Monitor_price;
                break;
        }
        
       
        //ShelfBar = GameManager.manager.GetShelfBar(this);
        //ShelfBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
        text.text = (moneyNeed - curMoneyAmount)+"";
        s1.sprite = GameManager.manager.getImage(shelfType);
        s2.sprite = GameManager.manager.getImage(shelfType);

        //PlayerPrefs.DeleteKey(shelf_name);
        if (PlayerPrefs.HasKey(shelf_name))//isBuild*loadingLvl
        {
            string s = PlayerPrefs.GetString(shelf_name);
            string[] sArray;
            sArray = s.Split('*');

            if (bool.Parse(sArray[0]))
            {
                isShelfBulded = true;
                GameManager.manager.max += 1;
               
                shelfBuy.SetActive(true);
                shelf_plane.SetActive(false);
              

                GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].lvl = int.Parse(sArray[1]);

                GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].gameObject.SetActive(true);
                switch (int.Parse(sArray[1]))
                {
                    case 0:
                        GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].uc.currentGrade = 0;
                        switch (shelfType)
                        {
                            case PlayerController.OnTimerEndBehavior.PCGetUp:
                                GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].uc.moneyNeed = Properties.properties.LoadingUpgrade1_PC_Price;
                                break;

                            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].uc.moneyNeed = Properties.properties.LoadingUpgrade1_Mouse_Price;
                                break;

                            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].uc.moneyNeed = Properties.properties.LoadingUpgrade1_Keyboard_Price;
                                break;

                            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].uc.moneyNeed = Properties.properties.LoadingUpgrade1_Monitor_Price;
                                break;
                        }
                       
                        GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].uc.Updt();
                     
                        break;

               
                        
                        break;
                    case 1:
                        loadingBoard lb = GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)];
                        switch (shelfType)
                        {
                            case PlayerController.OnTimerEndBehavior.PCGetUp:
                                lb.uc.currentGrade = 1;
                                lb.uc.moneyNeed = Properties.properties.LoadingUpgrade2_PC_Price;
                                lb.uc.Updt();
                                lb.moneyNeed = Properties.properties.Loading2Grade_PC_Price;
                                lb.text.text = (lb.moneyNeed - lb.curMoneyAmount) + "";
                                break;

                            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                lb.uc.currentGrade = 1;
                                lb.uc.moneyNeed = Properties.properties.LoadingUpgrade2_Mouse_Price;
                                lb.uc.Updt();
                                lb.moneyNeed = Properties.properties.Loading2Grade_Mouse_Price;
                                lb.text.text = (lb.moneyNeed - lb.curMoneyAmount) + "";
                                break;

                            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                lb.uc.currentGrade = 1;
                                lb.uc.moneyNeed = Properties.properties.LoadingUpgrade2_Keyboard_Price;
                                lb.uc.Updt();
                                lb.moneyNeed = Properties.properties.Loading2Grade_Keyboard_Price;
                                lb.text.text = (lb.moneyNeed - lb.curMoneyAmount) + "";
                                break;

                            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                lb.uc.currentGrade = 1;
                                lb.uc.moneyNeed = Properties.properties.LoadingUpgrade2_Monitor_Price;
                                lb.uc.Updt();
                                lb.moneyNeed = Properties.properties.Loading2Grade_Monitor_Price;
                                lb.text.text = (lb.moneyNeed - lb.curMoneyAmount) + "";
                                break;
                        }

                       
                       
                        break;
                    case 2:
                        loadingBoard lb1 = GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)];

                        switch (shelfType)
                        {
                            case PlayerController.OnTimerEndBehavior.PCGetUp:
                                lb1.moneyNeed = Properties.properties.Loading3Grade_PC_Price;
                                break;

                            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                lb1.moneyNeed = Properties.properties.Loading3Grade_Mouse_Price;
                                break;

                            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                lb1.moneyNeed = Properties.properties.Loading3Grade_Keyboard_Price;
                                break;

                            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                lb1.moneyNeed = Properties.properties.Loading3Grade_Monitor_Price;
                                break;
                        }
                        
                        lb1.text.text = (lb1.moneyNeed - lb1.curMoneyAmount) + "";
                        Destroy(GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].uc.gameObject);
                        break;

                }

                GameManager.manager.RecalculateShelf();
            }

             
        }
        else
        {
            PlayerPrefs.SetString(shelf_name, isShelfBulded + "*" + GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].lvl);
        }

      //  for (int i = 0; i < shelf.Count; i++)
      // shelf[i].Set(shelfType);
    }
    public void Save()
    {
        PlayerPrefs.SetString(shelf_name, isShelfBulded + "*" + GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].lvl);
    }
    private void Update()
    {
        if (!isShelfBulded)
        {
            //ShelfBar.MoveToClickPoint(money_point.position);

        }

    }
    public int Reservate()
    {
        int res = 9;

        for (int i = 0; i < 4; i++)
        {
            if(avalible_item[i])
            {
                res = i;
                avalible_item[i] = false;
                break;
            }
        }

        return res;
    }
   public void GetItem(int a)
    {
        shelf[a].gameObject.SetActive(false);
    }
    public int GetAvalibleItem()
    {
        for (int i = 0; i < shelf.Count; i++)
        {
            if (shelf[i].gameObject.activeSelf)
            {
                shelf[i].gameObject.SetActive(false);
                return shelf[i].lvl;
            }
        }
        return 0;
    }
    public bool isNeded()
    {
      

        for (int i = 0; i < shelf.Count; i++)
        {
            if (!shelf[i].gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
    public bool isEmpty()
    {
        bool r = true;

        for (int i = 0; i < shelf.Count; i++)
        {
            if (shelf[i].gameObject.activeSelf)
            {
                r = false;
            }
        }

        return r;
    }

    public int EmptyAmount()
    {
        int  r = 0;

        for (int i = 0; i < shelf.Count; i++)
        {
            if (!shelf[i].gameObject.activeSelf)
            {
                r++;
            }
        }
  
        return r;
    }
    void SetItem(int a, int lvl)
    {
        avalible_item[a] = true;
        shelf[a].gameObject.SetActive(true);
        shelf[a].Set(shelfType, lvl);
    }

    public void ActiveOne(int a)
    {
        for (int i = 0; i < shelf.Count; i++)
        {
            if(!shelf[i].gameObject.activeSelf)
            {
                shelf[i].gameObject.SetActive(true);
                shelf[i].Set(shelfType,a);
                break;
            }
              

        }
    }

    public void ActiveOne()
    {
        for (int i = 0; i < shelf.Count; i++)
        {
            if (!shelf[i].gameObject.activeSelf)
            {
                shelf[i].gameObject.SetActive(true);
                //shelf[i].Set(shelfType, a);
                break;
            }


        }
    }

    public bool isAvalible()
    {    
        for (int i = 0; i < shelf.Count; i++)
        {
            if (shelf[i].gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
    int IsFreeSpace()
    {
        int r = 99;
        for (int i = 0; i < shelf.Count; i++)
        {
            if (!shelf[i].gameObject.activeSelf)
            {
                r = i;
                break;
            }
        }
        return r;
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cashier")
        {
           for(int q = 0; q < 4;q++)
            {
                int a = IsFreeSpace();

                if (a < 10 && isShelfBulded)
                {
                    for (int i = 0; i < GameManager.manager.player.carryingItems.Count; i++)
                    {
                        if (GameManager.manager.player.carryingItems[i].type == shelfType)//if (GameManager.manager.player.carryingItems[i].type == PlayerController.OnTimerEndBehavior.PCGetUp ||  GameManager.manager.player.carryingItems[i].type == PlayerController.OnTimerEndBehavior.MouseGetUp || GameManager.manager.player.carryingItems[i].type == PlayerController.OnTimerEndBehavior.KeyBoardGetUp || GameManager.manager.player.carryingItems[i].type == PlayerController.OnTimerEndBehavior.MonitorGetUp) 
                        {
                            SetItem(a, GameManager.manager.player.GiveAwayItem(i));
                          
                            if (GameManager.manager.player.carryingItems.Count == 0)
                                GameManager.manager.player.isCarrying = false;


                            break;
                        }
                    }
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
        if (other.tag == "Cashier" && !isShelfBulded && GameManager.manager.player.isStanding() && ((GameManager.manager.tut.oreder == 0 && GameManager.manager.tut.active) || !GameManager.manager.tut.active))
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
            isShelfBulded = true;
            GameManager.manager.max += 1;
        }
        yield return new WaitForSeconds(0.25f);
        // ShelfBar.SetMoneyAmount(moneyNeed - curMoneyAmount);
        text.text = (moneyNeed - curMoneyAmount) + "";

        if (curMoneyAmount == moneyNeed)
        {
           // ShelfBar.DisableBar();
            if (GameManager.manager.ind > 1)
                GameManager.manager.RecalculateShelf();
            shelfBuy.SetActive(true);
            shelf_plane.SetActive(false);

            GameManager.manager.loadingBoards[GameManager.manager.PCshelf.IndexOf(this)].gameObject.SetActive(true);
            Save();
        }
    }
}
