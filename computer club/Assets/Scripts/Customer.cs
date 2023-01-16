using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    //NavMesh
    private NavMeshAgent navMeshAgent;
    public Transform target;
    public float movementSpeed = 2;
    private float nullTargetTimer;
    private float nullTargetTime = 1f;
    //Animation
    public Animator animator;
    public List<ParticleSystem> particles;

    //Customer
    public List<GameObject> carrying_item;
    public bool toExit = false;
    public bool toCashbox = false;
    private Behavior behavior;
    private float PCRentTime = 0;
    public float PCRentTimer = 0;
    public  bool isEating = false;
    public bool isWaitingForFood = false;
    private bool isCheckCalculated = false;
    private bool isPCUsing = false;
    private int rentPayAmount;
    public Transform indicator_point;
    public indicator indicator;
    private PC curPC;
    public PCShelf curPCShelf;
    public  PCShelf[] PCShelfs = new PCShelf[0];
    public int resetvatedItem;
    public PlayerController.OnTimerEndBehavior orderedFood;
    private Customer forwardCustomerInLine;
    private bool isRealTarget;
    public Transform carryingParent;
    private float curCarryingYPoint;
    private PlayerController.OnTimerEndBehavior lastCarryingItem = PlayerController.OnTimerEndBehavior.none;
    private int rand = 11;
    public wishBar wish;
    public isAvalibe av;
    public enum Behavior
    {
        BuyItem,
        RentPC
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
        navMeshAgent.isStopped = false;
        
        for(int i = 0; i < carrying_item.Count; i++)
            carrying_item[i].SetActive(false);

        switch (1)
        {
            case 0://PC_Shelf
                behavior = Behavior.BuyItem;
                break;

            case 1://PC
                behavior = Behavior.RentPC;
                break;
        }
    }
   
    IEnumerator Emoji()
    {
        yield return new WaitForSeconds(2.5f);
        if(isPCUsing)
        {
            particles[Random.Range(0, particles.Count - 1)].Play();
            StartCoroutine(Emoji());
        }
   
    }
    IEnumerator PayOffDelay(float delay)
    {     
        yield return new WaitForSeconds(delay);

        if (delay > 0)
        {
            if(Random.Range(0,1) == 0)//if go to cashbox or continue use pc (order food)
            {
                transform.position = GameManager.manager.PC[0].customerPoint.position;
                transform.rotation = Quaternion.Euler(0, 90, 0);

                target = GameManager.manager.cashbox_pc.customersLine[0];
            }
            else
            {
              //  PCRentTime *= 2;
                indicator.SetBehavior(Random.Range(1,3));
                
            }         
        }
        else
        {
            target = GameManager.manager.cashbox_items.customersLine[0];
            
            toCashbox = true;
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = target.position;
            animator.Play("Blend Tree");
            animator.SetFloat("Movement", 0.5f);
        }
    }
    void GoToItemCashBox()
    {
        av.isAvalible = false;
        wish.DisableBar();
        target = GameManager.manager.cashbox_items.customersLine[0];
        toCashbox = true;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = target.position;
        animator.Play("Blend Tree");
        animator.SetFloat("Movement", 0.5f);
    }
    void GoToPCRentCashBox()
    {
        indicator.DisableIndicator();
        curPC.LeavePC(this);
        isPCUsing = false;
        transform.position = curPC.customerPoint.position;
        transform.rotation = Quaternion.Euler(0, 90, 0);

      
        target = GameManager.manager.cashbox_pc.customersLine[0];
    
        toCashbox = true;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = target.position;
        animator.Play("Blend Tree");
        animator.SetFloat("Movement", 0.5f);
    }
  
    void OrderFood()
    {
        isWaitingForFood = true;
        int b = Random.Range(1, GameManager.manager.ind);
        indicator.SetBehavior(b);
        
        switch(b)
        {
            case 1:
                orderedFood = PlayerController.OnTimerEndBehavior.BurgerCarrying; 
                break;

            case 2:
                orderedFood = PlayerController.OnTimerEndBehavior.PizzaCarrying;
                break;

            case 3:
                orderedFood = PlayerController.OnTimerEndBehavior.CackeCarrying;
                break;

            case 4:
                orderedFood = PlayerController.OnTimerEndBehavior.SodaCarrying;
                break;

         

        }

        PCRentTimer = 25;//eatingTime = rentTime or eatingTime = const
        PCRentTime = 25;
        indicator.SetOrangeColor();
    }
    void ReRentPC()
    {
        isCheckCalculated = false;
    }
    public void DeliveredFood()
    {
        isWaitingForFood = false;
        isEating = true;
        rentPayAmount += 5;

        if (Random.Range(1, GameManager.manager.end) > 4)
        {
            //Debug.Log("ReRent");
            ReRentPC();
        }
        else
        {
            //Debug.Log("Cashbox");
            GoToPCRentCashBox();
        }
    }
    public bool here = false;
    public int oredInLine = 0;
    int orderShelf = 0;
    bool k = false;
    void Update()
    {
        if(target != null)
        {
            if (!toExit)
            {
                if (!toCashbox)
                {
                    if (Vector3.Distance(transform.position, target.position) < 0.1f)
                    {
                        navMeshAgent.enabled = false;
                        animator.SetFloat("Movement", 0);
                        here = true;
                        switch (behavior)
                        {
                            case Behavior.BuyItem:
                              if(PCShelfs[orderShelf].isAvalible())
                                {
                                    float y = 0;
                                    int lvl = PCShelfs[orderShelf].GetAvalibleItem();
                                    if (lastCarryingItem != PlayerController.OnTimerEndBehavior.none)
                                    {
                                        switch (lastCarryingItem)
                                        {
                                            case PlayerController.OnTimerEndBehavior.BurgerCarrying:
                                                y = 0.35f;
                                                break;

                                            case PlayerController.OnTimerEndBehavior.CackeCarrying:
                                                y = 0.23f;
                                                break;

                                            case PlayerController.OnTimerEndBehavior.SodaCarrying:
                                                y = 0.23f;
                                                break;

                                            case PlayerController.OnTimerEndBehavior.PizzaCarrying:
                                                y = 0.05f;
                                                break;

                                            case PlayerController.OnTimerEndBehavior.PCGetUp:
                                                y = 0.35f;
                                                break;

                                            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                                y = 0.23f;
                                                break;

                                            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                                y = 0.05f;
                                                break;

                                            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                                y = 0.35f;
                                                break;
                                        }
                                    }
                                    switch (PCShelfs[orderShelf].shelfType)
                                    {
                                        case PlayerController.OnTimerEndBehavior.PCGetUp:
                                            curCarryingYPoint += y + 0.25f;
                                            animator.SetLayerWeight(1, 1);
                                            var a = Instantiate(GameManager.manager.item[lvl], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                                            a.transform.rotation = transform.rotation;
                                            a.transform.localScale *= 3;
                                            Item itemA = a.AddComponent(typeof(Item)) as Item;
                                            itemA.type = PCShelfs[orderShelf].shelfType;
                                            //isCarrying = true;
                                            //carryingItems.Add(itemA);
                                            rentPayAmount += lvl == 0 ? Properties.properties.PC1Grade_Puyment : (lvl == 1 ? Properties.properties.PC2Grade_Puyment : Properties.properties.PC3Grade_Puyment);
                                            break;

                                        case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                            curCarryingYPoint += y;
                                            animator.SetLayerWeight(1, 1);
                                            var a1 = Instantiate(GameManager.manager.item[lvl + 3], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                                            a1.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
                                            a1.transform.localScale *= 2;
                                            Item itemA1 = a1.AddComponent(typeof(Item)) as Item;
                                            itemA1.type = PCShelfs[orderShelf].shelfType;
                                            //isCarrying = true;
                                            //carryingItems.Add(itemA1);
                                            rentPayAmount += lvl == 0 ? Properties.properties.Mouse1Grade_Puyment : (lvl == 1 ? Properties.properties.Mouse2Grade_Puyment : Properties.properties.Mouse3Grade_Puyment);
                                            break;

                                        case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                            curCarryingYPoint += y;
                                            animator.SetLayerWeight(1, 1);
                                            var a2 = Instantiate(GameManager.manager.item[lvl + 6], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                                            a2.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                                            a2.transform.localScale *= 2;
                                            Item itemA2 = a2.AddComponent(typeof(Item)) as Item;
                                            itemA2.type = PCShelfs[orderShelf].shelfType;
                                            //isCarrying = true;
                                            //carryingItems.Add(itemA2);
                                            rentPayAmount += lvl == 0 ? Properties.properties.Keyboard1Grade_Puyment : (lvl == 1 ? Properties.properties.Keyboard2Grade_Puyment : Properties.properties.Keyboard3Grade_Puyment);
                                            break;

                                        case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                            curCarryingYPoint += y + 0.15f;
                                            animator.SetLayerWeight(1, 1);
                                            var a3 = Instantiate(GameManager.manager.item[lvl + 9], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                                            a3.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                                            a3.transform.localScale *= 3;
                                            Item itemA3 = a3.AddComponent(typeof(Item)) as Item;
                                            itemA3.type = PCShelfs[orderShelf].shelfType;
                                            //isCarrying = true;
                                            //carryingItems.Add(itemA3);
                                            rentPayAmount += lvl == 0 ? Properties.properties.Keyboard1Grade_Puyment : (lvl == 1 ? Properties.properties.Keyboard2Grade_Puyment : Properties.properties.Keyboard3Grade_Puyment);
                                            break;
                                    }
                                   
                                    
                                    
                                    
                               
                                    
                                   
                                    
                                    
                                    
                                    
                                    lastCarryingItem = PCShelfs[orderShelf].shelfType;
                                    animator.SetLayerWeight(1, 1);
                                   

                                    orderShelf++;
                                    if (PCShelfs.Length > orderShelf)
                                    {
                                        switch (PCShelfs[orderShelf].shelfType)
                                        {
                                            case PlayerController.OnTimerEndBehavior.PCGetUp:
                                                wish.SetBehavior(0);
                                                break;
                                            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                                wish.SetBehavior(1);
                                                break;
                                            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                                wish.SetBehavior(2);
                                                break;
                                            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                                wish.SetBehavior(3);
                                                break;
                                        }

                                        if (PCShelfs[orderShelf - 1] == PCShelfs[orderShelf])
                                        {

                                        }
                                        else
                                        {
                                            for(int i = 0; i < PCShelfs[orderShelf].customerPoint.Count; i++)
                                            {
                                                if(!PCShelfs[orderShelf].customerPoint[i].isAvalible)
                                                {
                                                    PCShelfs[orderShelf].customerPoint[i].isAvalible = true;
                                                    av = PCShelfs[orderShelf].customerPoint[i];
                                                    target = PCShelfs[orderShelf].customerPoint[i].gameObject.transform;
                                                    break;
                                                }
                                            }
                                            
                                           
                                            navMeshAgent.enabled = true;
                                            navMeshAgent.destination = target.position;
                                            animator.SetFloat("Movement", 0.5f);
                                        }
                                    }
                                    else
                                    {
                                        GoToItemCashBox();
                                    }
                                }
                              else
                                {
                                    
                                }
                                break;

                            case Behavior.RentPC:
                                transform.position = curPC.sittingPoint.position;
                                transform.rotation = curPC.sittingPoint.rotation;
                                animator.Play("Typing");
                                isPCUsing = true;
                                break;
                        }

                    }

                    if (isPCUsing)
                    {
                        if (!isCheckCalculated)
                        {
                            indicator.SetBehavior(0);
                            particles[Random.Range(0, particles.Count - 1)].Play();
                            StartCoroutine(Emoji());
                            
                            indicator.SetGreenColor();

                            PCRentTimer = Random.Range(3, 11);
                            PCRentTime = PCRentTimer;
                            int lv = curPC.GetLvL();
                            float forCes = lv == 0 ? Properties.properties.PCUpgrade1_Puyment_ForSec : (lv == 1 ? Properties.properties.PCUpgrade2_Puyment_ForSec : Properties.properties.PCUpgrade3_Puyment_ForSec); ;


                            rentPayAmount += (2 + (int)(PCRentTimer * forCes));

                            isCheckCalculated = true;
                        }

                        indicator.MoveToClickPoint(indicator_point.position);

                        if (PCRentTimer > 0)
                        {
                            PCRentTimer -= Time.deltaTime;
                            indicator.timer.fillAmount = (PCRentTimer / PCRentTime);
                        }
                        else
                        {
                            if (isWaitingForFood)
                            {
                                GoToPCRentCashBox();
                            }
                            else
                            {
                                if (Random.Range(0, 0) == 0)//if go to cashbox or continue use pc (order food)
                                {
                                    GoToPCRentCashBox();
                                }
                                else
                                {
                                    OrderFood();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, target.position) < 0.1f)
                    {
                        navMeshAgent.isStopped = true;
                        animator.SetFloat("Movement", 0);

                      

                       
                        
                        bool b = behavior == Behavior.RentPC ? (target == GameManager.manager.cashbox_pc.customersLine[0]) : (target == GameManager.manager.cashbox_items.customersLine[0]);
                        bool d = behavior == Behavior.RentPC ? (GameManager.manager.cashbox_pc.isOnCashBox) : (GameManager.manager.cashbox_items.isOnCashBox);
                        if ( b)//GameManager.manager.player.active
                        {
                            
                            if(d && !toExit)
                            {
                                switch (behavior)
                                {
                                    case Behavior.BuyItem:
                                        GameManager.manager.cashbox_items.Pay(rentPayAmount);
                                        GameManager.manager.cashbox_items.LeavePoint(0);
                                        GameManager.manager.cashbox_items.isOnCashBox = false;
                                        break;

                                    case Behavior.RentPC:
                                        GameManager.manager.cashbox_pc.Pay(rentPayAmount);
                                        GameManager.manager.cashbox_pc.LeavePoint(0);
                                        GameManager.manager.cashbox_pc.isOnCashBox = false;
                                        break;
                                }


                                //GameManager.manager.cashbox_items.LeavePoint(0);
                                CustomerSpawner.cs.DestorCustomer1(this);
                                toExit = true;
                                target = GameManager.manager.exit;
                                navMeshAgent.destination = target.position;
                                animator.SetFloat("Movement", 0.5f);
                                navMeshAgent.isStopped = false;
                            }
                        }
                        else
                        {
                            switch (behavior)
                            {
                                case Behavior.BuyItem:
                                    if (!GameManager.manager.cashbox_items.isPointFree[oredInLine - 1])
                                    {
                                        oredInLine--;
                                        target = GameManager.manager.cashbox_items.GetPoint(oredInLine);
                                        GameManager.manager.cashbox_items.isPointFree[oredInLine] = true;
                                        GameManager.manager.cashbox_items.LeavePoint(oredInLine + 1);

                                        navMeshAgent.destination = target.position;
                                        animator.SetFloat("Movement", 0.5f);
                                        navMeshAgent.isStopped = false;
                                    }
                                    break;

                                case Behavior.RentPC:
                                    if (!GameManager.manager.cashbox_pc.isPointFree[oredInLine - 1])
                                    {
                                        oredInLine--;
                                        target = GameManager.manager.cashbox_pc.GetPoint(oredInLine);
                                        GameManager.manager.cashbox_pc.isPointFree[oredInLine] = true;
                                        GameManager.manager.cashbox_pc.LeavePoint(oredInLine + 1);

                                        navMeshAgent.destination = target.position;
                                        animator.SetFloat("Movement", 0.5f);
                                        navMeshAgent.isStopped = false;
                                    }
                                    break;
                            }

                          
                        }
                     
                    }
                    else
                    {

                        switch (behavior)
                        {
                            case Behavior.BuyItem:
                                if (Vector3.Distance(transform.position, target.position) < 2f && !k)
                                {
                                    Debug.Log("Hello");
                                    TransformInt ti = GameManager.manager.cashbox_items.GetLastCustomerPos();//GameManager.manager.cashbox_pc.customer_point;
                                    target = ti.transform;
                                    oredInLine = ti.number;
                                    k = true;
                                    navMeshAgent.destination = target.position;
                                }
                                break;

                            case Behavior.RentPC:
                                if (Vector3.Distance(transform.position, target.position) < 2f && !k)
                                {
                                    Debug.Log("Hello");
                                    TransformInt ti = GameManager.manager.cashbox_pc.GetLastCustomerPos();//GameManager.manager.cashbox_pc.customer_point;
                                    target = ti.transform;
                                    oredInLine = ti.number;
                                    k = true;
                                    navMeshAgent.destination = target.position;
                                }
                                break;
                        }

                  


                      
                    }
                  
                   
                  
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, target.position) < 0.1f)
                {

                    switch (behavior)
                    {
                        case Behavior.BuyItem:
                            Destroy(wish.gameObject);
                            break;
                    }
                 

                    CustomerSpawner.cs.DestorCustomer(this);
                }
            }

            
            switch(behavior)
            {
                case Behavior.BuyItem:
                    wish.MoveToClickPoint(indicator_point.position);
                    break;
            }
        }
        else
        {
            if(nullTargetTimer > 0)
            {
                nullTargetTimer -= Time.deltaTime;
            }
            else
            {
                switch (behavior)
                {
                    case Behavior.BuyItem:
                        PCShelfs = new PCShelf[Random.Range(1, GameManager.manager.and )];
                        int max = 4;
                        for(int i = 0; i < GameManager.manager.PCshelf.Count; i++)
                        {
                            if (!GameManager.manager.PCshelf[i].isShelfBulded)
                            {
                                max = i ;
                                break;
                            }
                        }

                        //Debug.Log(max+ " max");
                        for (int i = 0; i < PCShelfs.Length; i++)
                        {
                            int t = Random.Range(0, max);
                            //Debug.Log(t+"  max "+max);
                            PCShelfs[i] = GameManager.manager.PCshelf[t];
                        }
                           


                      
                        wish = GameManager.manager.GetWishBar(this);
                       switch(PCShelfs[orderShelf].shelfType)
                        {
                            case PlayerController.OnTimerEndBehavior.PCGetUp:
                                wish.SetBehavior(0);
                                break;
                            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                wish.SetBehavior(1);
                                break;
                            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                wish.SetBehavior(2);
                                break;
                            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                wish.SetBehavior(3);
                                break;
                        }

                        for (int i = 0; i < PCShelfs[orderShelf].customerPoint.Count; i++)
                        {
                            if (!PCShelfs[orderShelf].customerPoint[i].isAvalible)
                            {
                                PCShelfs[orderShelf].customerPoint[i].isAvalible = true;
                                av = PCShelfs[orderShelf].customerPoint[i];
                                target = PCShelfs[orderShelf].customerPoint[i].gameObject.transform;
                                break;
                            }
                        }
                      
                        navMeshAgent.enabled = true;
                        navMeshAgent.destination = target.position;
                        animator.SetFloat("Movement", 0.5f);
                        break;

                    case Behavior.RentPC:
                        curPC = GameManager.manager.CheckForFreePC(this);
                        if (curPC != null)
                        {
                            target = curPC.customerPoint;
                            rentPayAmount = 0;
                            indicator = GameManager.manager.GetIndicator(this);
                   
                            navMeshAgent.destination = target.position;
                            animator.SetFloat("Movement", 0.5f);
                        }
                        else
                        {
                            target = null;
                            behavior = Behavior.BuyItem;
                        }
                          
                        break;
                }
                nullTargetTimer = nullTargetTime;
            }
         
        }
           
    }
}
