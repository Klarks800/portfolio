using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public List<PCShelf> PCshelf;
    public List<PC> PC;
    public bool onCashbox;
    public Transform exit;
    public Cashbox cashbox_pc;
    public Cashbox cashbox_items;
    public Transform startMoneyPoint;
    public Transform endMoneyPoint;
    public GameObject dollar;
    public indicator indicator;
    public bar food_bar;
    public pcBar PCBar;
    public playerDelayBar playerBar;
    public PlayerController player;
    public List<GameObject> food;
    public List<GameObject> item;
    //public NavMeshSurface surface;
    public Animator truck;
    public money money;
    public int mon;
    public TextMeshProUGUI moneyText;
    public List<Transform> waitingPoints;
    public int max;
    public List<Sprite> images;
    public float serviceSpeed = 2;
    public trashBucket bucket;
    public wishBar wishbar;
    public List<foodMachine> foodMachines;
    public List<loadingBoard> loadingBoards;
    public Transform workerPoint;
    public Personal[] personal;
    public int ind = 1;//5
    public int and = 1;//3
    public int end = 1;//11
    public bool up = false;
    public tutorial tut;
    public List<upgrade> upgrades;
    void Awake()
    {
        //PlayerPrefs.DeleteAll();



       

        Application.targetFrameRate = 60;
        manager = this;
        
     
        
       
        //RecalculatePC();
        RecalculateShelf();
        ChangeServiceSpeed(2);
    }


   // private void OnApplicationQuit()
   // {
     //   AppM.app.SendLevelFinish();
   // }

    bool once = false;
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus && !once)
        {
            once = true;
            AppM.app.SendLevelFinish();
        }

    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("mon"))
        {
            mon = PlayerPrefs.GetInt("mon");

            moneyText.text = mon + "";
        }
        else
        {
            PlayerPrefs.SetInt("mon", 20);
            mon = PlayerPrefs.GetInt("mon");
            moneyText.text = mon + "";
        }

        AppM.app.SendLevelStart();
    }
    public void ChangeServiceSpeed(float f)
    {
        serviceSpeed = f;
        player.delayTimer = f;
        player.delayTime = f;
    }

    public Sprite getImage(PlayerController.OnTimerEndBehavior beh)
    {
       switch(beh)
        {
            case PlayerController.OnTimerEndBehavior.PCGetUp:
                return images[0];
                

            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                return images[1];
               

            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                return images[2]; 

            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                return images[3];
            
        }

        return null;
    }
    public Transform GetRandomWaitingPoint()
    {
        GameObject emptyGO = new GameObject();
        emptyGO.transform.position = waitingPoints[Random.Range(0, waitingPoints.Count - 1)].position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        Transform newTransform = emptyGO.transform;
        return newTransform;
    }

    public void MinusMoney(int i)
    {
        if(mon > 0)
        mon -= i;

        PlayerPrefs.SetInt("mon", mon);
        
     

        moneyText.text = mon + "";
    }

    public void PlusMoney()
    {
        mon++;
        PlayerPrefs.SetInt("mon", mon);
        moneyText.text = mon + "";
    }

    public void SpawnMoney(Vector3 pos)
    {
        money m = Instantiate(money, player.moneyPoint.position, Quaternion.identity);

        m.SetPos(player.moneyPoint.position,pos);
    }

    public void TruckDilivery()
    {
        truck.Play("truck_anim");
    }

    public void RecalculatePC()
    {
        int n = PC.Count - 1;

        for(int i = 0; i < PC.Count; i++)
        {
            if (!PC[i].isPCBuilded)
            {
                n = i;
                break;
            }   
        }

        for (int i = 0; i < PC.Count; i++)
        {
            PC[i].gameObject.SetActive(true);
        }

        if(PC.Count-1 > n)
        for (int i = n + 1; i < PC.Count; i++)
        {
            PC[i].gameObject.SetActive(false);
        }
    }
    public void RecalculateShelf()
    {
        int n = PCshelf.Count - 1;

        for (int i = 0; i < PCshelf.Count; i++)
        {
            if (!PCshelf[i].isShelfBulded)
            {
                n = i;
                break;
            }
        }

        for (int i = 0; i < PCshelf.Count; i++)
        {
            PCshelf[i].gameObject.SetActive(true);
        }

        if (PCshelf.Count - 1 > n)
            for (int i = n + 1; i < PCshelf.Count; i++)
            {
                PCshelf[i].gameObject.SetActive(false);
            }
    }
    public PC CheckForFreePC(Customer customer)
    {
        for(int i = 0; i < PC.Count; i++)
        {
            if (!PC[i].isPCTaken && PC[i].isPCBuilded)
            {
                PC[i].SetCustomer(customer);
                return PC[i];
            }  
        }
        return null;
    }
  
    public PCShelf CheckForFreePCSfelf(Customer customer)
    {
        for (int i = 0; i < PCshelf.Count; i++)
        {
            if(PCshelf[i].isShelfBulded)
            {
                int l = PCshelf[i].Reservate();

                if (l == 9)
                {

                }
                else
                {

                    customer.resetvatedItem = l;
                    return PCshelf[i];
                }
            }
        }
        return null;
    }

    public pcBar GetUpdrageBar(upgrade up)
    {
        pcBar pc_bar = Instantiate(PCBar, up.moneyPoint.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        return pc_bar;
    }
    public indicator GetIndicator(Customer c)
    {
        indicator ind = Instantiate(indicator,c.indicator_point.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        return ind;
    }

    public bar GetBar(foodMachine o)
    {
        bar bar = Instantiate(food_bar, o.bar_point.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        return bar;
    }

    public playerDelayBar GetPlayerBar(PlayerController p)
    {
        playerDelayBar pc = Instantiate(playerBar, p.playerDelayBarPoint.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        return pc;
    }

    public pcBar GetPCBar(PC pc)
    {
        pcBar pc_bar = Instantiate(PCBar, pc.moneyBarPoint.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        return pc_bar;
    }

    public pcBar GetShelfBar(PCShelf shelf)
    {
        pcBar pc_bar = Instantiate(PCBar, shelf.money_point.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        return pc_bar;
    }

    public pcBar GetMoneyBar(loadingBoard board)
    {
        pcBar pc_bar = Instantiate(PCBar, board.money_point.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        return pc_bar;
    }
    public wishBar GetWishBar(Customer c)
    {
        wishBar wish_bar = Instantiate(wishbar, c.indicator_point.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        return wish_bar;
    }

}
