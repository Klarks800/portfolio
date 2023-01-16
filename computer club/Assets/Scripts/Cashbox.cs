using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformInt : MonoBehaviour
{
    public Transform transform;
    public int number;

    public TransformInt(int n,Transform t)
    {
        transform = t;
        number = n;
    }
}

public class Cashbox : MonoBehaviour
{
    //public Transform customer_point;
    public Transform startMoneyPoint;
    public Transform endMoneyPoint;
    private Vector3 lastDollarSpawnPoint;
    public List<GameObject> dollarStash = new List<GameObject>();
    public List<Transform> customersLine = new List<Transform>();
    public bool[] isPointFree = new bool[11];
    public bool isOnCashBox;
    public Transform chashierPoint;
    private void Start()
    {
        lastDollarSpawnPoint = startMoneyPoint.position;
    }

    public TransformInt GetLastCustomerPos()
    {
        for(int i = 0; i < isPointFree.Length;i++)
            if(!isPointFree[i])
            {
                isPointFree[i] = true;
                return new TransformInt(i,customersLine[i]);
            }
                  
                
            

        return null;
    }

    public bool isAnyone()
    {
        return isPointFree[0];
    }

    public Transform GetPoint(int n)
    {
        return customersLine[n];
    }

    public TransformInt GetLast()
    {
        for (int i = isPointFree.Length-1; i > -1; i--)
        {
            //Debug.Log(isPointFree[i]+" "+(i < 10));
            if (isPointFree[i] && i < 10)
            {
                
                return new TransformInt(i + 1, customersLine[i + 1]);
            }
                
        }





        return new TransformInt(0, customersLine[0]);
    }

    public void SetPoint(int n)
    {
        isPointFree[n] = true;
    }

    public void LeavePoint(int n)
    {
        isPointFree[n] = false;
    }

    public bool CheckForwardPoint(int n)
    {
        if(n < 11)
        {
            if(!isPointFree[n])
            {
                return true;
            }
        }

        return false;
    }




    public void Pay(int c)
    {
        for (int i = 0; i < c; i++)
        {
            Vector3 d = endMoneyPoint.position - (startMoneyPoint.position - endMoneyPoint.position) / 4;
            d.y = lastDollarSpawnPoint.y;

            if (Vector3.Distance(lastDollarSpawnPoint, d) < 0.1f)
            {
                lastDollarSpawnPoint = new Vector3(startMoneyPoint.position.x, lastDollarSpawnPoint.y, startMoneyPoint.position.z);
                lastDollarSpawnPoint.y += 0.05f;
                dollarStash.Add(Instantiate(GameManager.manager.dollar, lastDollarSpawnPoint, startMoneyPoint.rotation));
                lastDollarSpawnPoint -= (startMoneyPoint.position - endMoneyPoint.position) / 4;
            }
            else
            {
                dollarStash.Add(Instantiate(GameManager.manager.dollar, lastDollarSpawnPoint, startMoneyPoint.rotation));
                lastDollarSpawnPoint -= (startMoneyPoint.position - endMoneyPoint.position) / 4;
            }      
        }
    }

    public void GetMoney(int mo)
    {
       for(int i = 0; i < mo;i++)
        {
            lastDollarSpawnPoint = dollarStash[dollarStash.Count - 1].transform.position;
            money m = dollarStash[dollarStash.Count - 1].AddComponent<money>();
            m.SetPos(dollarStash[dollarStash.Count - 1].transform.position);
            dollarStash.RemoveAt(dollarStash.Count - 1);
            StartCoroutine(PlusOneMoney());
        }
        
       


    }

    public IEnumerator PlusOneMoney()
    {
        yield return new WaitForSeconds(0.25f);
        GameManager.manager.PlusMoney();
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cashier" && isAnyone())
        {
            GameManager.manager.player.curChasbox = this;
            GameManager.manager.player.ActivateTimer();
        }
    }

private float timer = 0.015f;

    public float mon = 1;
    bool monn = false;
    float mm;
   
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cashier")
        {
           if(dollarStash.Count > 0)
            {
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = 0.015f;

                    if (!monn)
                    {
                        mm = dollarStash.Count;
                        monn = true;
                    }


                    mon += 0.001f * dollarStash.Count;

                    GetMoney((int)mon);
                }
            }


           if(GameManager.manager.cashbox_pc == this)
            {
                if (isAnyone() && !GameManager.manager.personal[2].gameObject.activeSelf)
                {
                    if (!GameManager.manager.player.active)
                    {
                        GameManager.manager.player.curChasbox = this;
                        GameManager.manager.player.ActivateTimer();
                    }


                    GameManager.manager.player.timer(PlayerController.OnTimerEndBehavior.PayOffCustomer);
                }
            }
           else
            {
                if (isAnyone() && !GameManager.manager.personal[3].gameObject.activeSelf)
                {
                    if (!GameManager.manager.player.active)
                    {
                        GameManager.manager.player.curChasbox = this;
                        GameManager.manager.player.ActivateTimer();
                    }


                    GameManager.manager.player.timer(PlayerController.OnTimerEndBehavior.PayOffCustomer);
                }
            }


          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cashier")//&& isAnyone()
        {
            mon = 1;
            monn = false;
            GameManager.manager.player.curChasbox = null;
            GameManager.manager.player.DisableTimer();
        }

    }
  

}
