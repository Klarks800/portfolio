using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Service : MonoBehaviour
{

    public PC pc = null;
    public Customer customer = null;
    public PlayerController.OnTimerEndBehavior food = PlayerController.OnTimerEndBehavior.none;
    public bool isItemGoten = false;
    public Service(PC pc_, Customer customer_, PlayerController.OnTimerEndBehavior food_)
    {

        pc = pc_;
        customer = customer_;
        food = food_;

    }

    public Service(Service a)
    {

        pc = a.pc;
        customer = a.customer;
        food = a.food;

    }

    public Service()
    {

        pc = null;
        customer = null;
        food = PlayerController.OnTimerEndBehavior.none;

    }

}

public class Personal : MonoBehaviour
{
    //NavMesh
    private NavMeshAgent navMeshAgent;
    public Transform target;
    public float movementSpeed = 2f;
    private float nullTargetTimer;
    private float nullTargetTime = 1f;
    //Animation
    public Animator animator;
    //Personal
    public Transform carryingParent;
    private float curCarryingYPoint;
    private PlayerController.OnTimerEndBehavior lastCarryingItem = PlayerController.OnTimerEndBehavior.none;
    public PersonalType type;
    private bool isCarrying;
    public List<Item> carryingItems;
    public loadingBoard curLoadingBoard;
    int lvl;
    public enum PersonalType
    {
        none,
        Waiter,
        StashWorker,
        CashierPC,
        CashierShelf
    }

    Service qw;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
        navMeshAgent.isStopped = false;
        // Debug.Log(Vector3.Distance(transform.position,GameManager.manager.PC[0].customerPoint.position) + " eer");
    }

    public void Set(PersonalType t)
    {
        type = t;
    }
    private bool toFoodMachine = false;
    private bool Delivery = false;
    private bool Trash = false;

    private Service s;
    private float t = 15f;
    private bool isTarg;
    public void GiveAwayItem(PlayerController.OnTimerEndBehavior b)
    {
        float y = 0;
        int n = 0;
        for (int i = 0; i < carryingItems.Count; i++)
        {
            Item item = carryingItems[i].GetComponent<Item>();
            if (item.type == b && item.ordered)
            {
                n = i;
                break;
            }
        }
        try
        {



            switch (carryingItems[n].GetComponent<Item>().type)
            {
                case PlayerController.OnTimerEndBehavior.BurgerCarrying:
                    y = 0.35f;
                    break;

                case PlayerController.OnTimerEndBehavior.PizzaCarrying:
                    y = 0.05f;
                    break;

                case PlayerController.OnTimerEndBehavior.CackeCarrying:
                    y = 0.23f;
                    break;

                case PlayerController.OnTimerEndBehavior.SodaCarrying:
                    y = 0.23f;
                    break;

                case PlayerController.OnTimerEndBehavior.PCGetUp:
                    y = 0.6f;
                    break;

                case PlayerController.OnTimerEndBehavior.MouseGetUp:
                    y = 0.23f;
                    break;

                case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                    y = 0.05f;
                    break;

                case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                    y = 0.5f;
                    break;
            }

            curCarryingYPoint -= y;
            Destroy(carryingItems[n].gameObject);
            carryingItems.RemoveAt(n);

            for (int i = n; i < carryingItems.Count; i++)
            {
                carryingItems[i].transform.position -= (Vector3.up * y);
            }

            if (carryingItems.Count == 0)
            {
                animator.SetLayerWeight(1, 0);
                isCarrying = false;
                curCarryingYPoint = 0;
            }
        }
        catch
        {
            // Debug.Log(carryingItems[n].GetComponent<Item>().type+" LOOK");
        }
    }


    public void ClearItems()
    {
        curCarryingYPoint = 0;
        for (int i = 0; i < carryingItems.Count; i++)
        {
            Destroy(carryingItems[i].gameObject);

        }
        animator.SetLayerWeight(1, 0);
        isCarrying = false;
        carryingItems.Clear();
    }


    //private List<PC> pcs;
    // private List<PlayerController.OnTimerEndBehavior> wish;
    // private List<Customer> customers;
    public List<Service> services = new List<Service>();
    public List<PlayerController.OnTimerEndBehavior> lol = new List<PlayerController.OnTimerEndBehavior>();
    public List<bool> bol = new List<bool>();
    void CheckCutomers()
    {
        for (int i = 0; i < GameManager.manager.PC.Count; i++)
        {
            if (GameManager.manager.PC[i].isPCTaken)
            {
                if (GameManager.manager.PC[i].cur_customer.isWaitingForFood)
                {
                    bool q = true;
                    for (int a = 0; a < services.Count; a++)
                    {
                        if (GameManager.manager.PC[i] == services[a].pc)
                        {
                            q = false;
                            break;
                        }

                    }

                    if (q)
                    {

                        //Service b = new Service(GameManager.manager.PC[i], GameManager.manager.PC[i].cur_customer, customer.orderedFood);
                        //Debug.Log(b);
                        //Service b = new Service();
                        //b.pc = GameManager.manager.PC[i];
                        //b.customer = GameManager.manager.PC[i].cur_customer;
                        //b.food = (int)customer.orderedFood;
                        //b.Set(GameManager.manager.PC[i], GameManager.manager.PC[i].cur_customer, customer.orderedFood);

                        //lol.Add(GameManager.manager.PC[i].cur_customer.orderedFood);
                        services.Add(new Service(GameManager.manager.PC[i], GameManager.manager.PC[i].cur_customer, GameManager.manager.PC[i].cur_customer.orderedFood));
                    }

                }
            }
        }
    }

    float GetMinimumTime()
    {
        float timer = 9999999f;
        for (int i = 0; i < services.Count; i++)
        {
            for (int l = 0; l < carryingItems.Count; l++)
            {
                if (!services[i].isItemGoten && services[i].food == carryingItems[l].type && !carryingItems[l].ordered)
                    services[i].isItemGoten = true;


                if (services[i].isItemGoten && services[i].customer.PCRentTimer < timer)
                {
                    timer = services[i].customer.PCRentTimer;
                }
            }
        }


        return timer;
    }

    float GetMinimumDist()
    {
        float dist = 9999999f;
        for (int i = 0; i < services.Count; i++)
        {
            float d = Vector3.Distance(services[i].pc.customerPoint.position, transform.position);


            if (services[i].isItemGoten && d < dist)
            {
                dist = d;
            }
        }


        return timer;
    }

    Service GetMinimumTimeCustomer()
    {
        float timer = 9999999f;
        int n = 999;

        for (int i = 0; i < services.Count; i++)
        {
            if (services[i].isItemGoten && services[i].customer.PCRentTimer < timer)
            {
                //Debug.Log(services[n].food + "  " + services[n].isItemGoten + "  "+n);
                timer = services[i].customer.PCRentTimer;
                n = i;
            }
        }



        if (n == 999)
        {
           // Debug.Log("NONE N");
            //Time.timeScale = 0;
            return services[0];
        }
        else
        {
           // Debug.Log(services[n].food + "  " + services[n].isItemGoten + " | " + n);
            return services[n];
        }
    }

    float timer = 1f;
    bool active = false;
    float delayTimer = 1f;
    float delayTime = 1f;
    public PC pcc;
    PlayerController.OnTimerEndBehavior pcote = PlayerController.OnTimerEndBehavior.none;
    public PCShelf curShelf;
    void grabItem(PlayerController.OnTimerEndBehavior type, Customer cus)
    {
        float y = 0;
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
        switch (type)
        {
            case PlayerController.OnTimerEndBehavior.BurgerCarrying:
                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var o = Instantiate(GameManager.manager.food[0], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                Item item = o.AddComponent(typeof(Item)) as Item;
                item.type = s.food;
                item.ordered = true;
                item.cus = cus;
                o.transform.rotation = Quaternion.Euler(-90, 0, 0);
                isCarrying = true;
                carryingItems.Add(item);
                break;



            case PlayerController.OnTimerEndBehavior.PizzaCarrying:

                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var o1 = Instantiate(GameManager.manager.food[1], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                o1.transform.rotation = Quaternion.Euler(-90, 0, 0);
                Item item1 = o1.AddComponent(typeof(Item)) as Item;
                item1.type = s.food;
                item1.ordered = true;
                item1.cus = cus;
                isCarrying = true;
                carryingItems.Add(item1);
                break;

            case PlayerController.OnTimerEndBehavior.CackeCarrying:
                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var o2 = Instantiate(GameManager.manager.food[2], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                Item item2 = o2.AddComponent(typeof(Item)) as Item;
                item2.type = s.food;
                item2.ordered = true;
                item2.cus = cus;
                o2.transform.rotation = Quaternion.Euler(-90, 0, 0);
                isCarrying = true;
                carryingItems.Add(item2);
                break;

            case PlayerController.OnTimerEndBehavior.SodaCarrying:
                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var o3 = Instantiate(GameManager.manager.food[3], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                Item item3 = o3.AddComponent(typeof(Item)) as Item;
                item3.type = s.food;
                item3.ordered = true;
                item3.cus = cus;
                o3.transform.rotation = Quaternion.Euler(-90, 0, 0);
                isCarrying = true;
                carryingItems.Add(item3);
                break;
        }
        lastCarryingItem = s.food;
    }
    public void DisableTimer()
    {
        active = false;

    }

    public void time(PlayerController.OnTimerEndBehavior oteb)
    {
        if (active)
        {
            if (delayTimer > 0)
            {
                delayTimer -= Time.deltaTime;
            }
            else
            {
                pcote = oteb;
                switch (type)
                {
                    case PersonalType.CashierPC:
                        GameManager.manager.cashbox_pc.isOnCashBox = true;
                        break;

                    case PersonalType.CashierShelf:
                        GameManager.manager.cashbox_items.isOnCashBox = true;
                        break;
                }

                DisableTimer();
            }
        }
    }
    bool dist = false;
    public void ActivateTimer()
    {
        active = true;
        delayTimer = delayTime;

    }
    public float tim;
    bool isLoadingStash;
    bool isShelf;

 
    public void GiveAwayItem(int n)
    {
        float y = 0;
        switch (carryingItems[n].GetComponent<Item>().type)
        {
         
            case PlayerController.OnTimerEndBehavior.PCGetUp:
                y = 0.6f;
                break;

            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                y = 0.23f;
                break;

            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                y = 0.05f;
                break;

            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                y = 0.5f;
                break;
        }
        //Debug.Log(n);
        curCarryingYPoint -= y;
        Destroy(carryingItems[n].gameObject);
        carryingItems.RemoveAt(n);

        for (int i = n; i < carryingItems.Count; i++)
        {
            carryingItems[i].transform.position -= (Vector3.up * y);
        }

        if (carryingItems.Count == 0)
        {
            animator.SetLayerWeight(1, 0);
            curCarryingYPoint = 0;
            isCarrying = false;
        }
    }
    bool worker = false;
    void Update()
    {
        if (type != PersonalType.none)
        {
            switch (type)
            {
                case PersonalType.Waiter:
                    CheckCutomers();




                    if (services.Count > 0 && !isTarg)
                    {
                        tim = GetMinimumTime();

                        if (tim > 10)
                        {
                            //Debug.Log("lol");
                            s = new Service();

                            for (int i = 0; i < services.Count; i++)
                            {
                                if (!services[i].isItemGoten)
                                {
                                    s = services[i];
                                    pcc = s.pc;
                                    break;
                                }
                            }


                            if (s.food != PlayerController.OnTimerEndBehavior.none)
                            {
                                bool r = false;
                                for (int i = 0; i < carryingItems.Count; i++)
                                {
                                    if (!carryingItems[i].ordered && carryingItems[i].type == s.food)
                                    {
                                        r = true;
                                        break;
                                    }
                                }

                                if (r)
                                {
                                    target = s.pc.customerPoint;

                                    toFoodMachine = false;
                                    Delivery = true;
                                    navMeshAgent.enabled = true;
                                    navMeshAgent.isStopped = false;
                                    navMeshAgent.destination = target.position;
                                    animator.SetFloat("Movement", 1f);
                                }
                                else
                                {
                                    switch (s.food)
                                    {
                                        case PlayerController.OnTimerEndBehavior.BurgerCarrying:
                                            target = GameManager.manager.foodMachines[0].point;
                                            break;
                                        case PlayerController.OnTimerEndBehavior.PizzaCarrying:
                                            target = GameManager.manager.foodMachines[1].point;
                                            break;
                                        case PlayerController.OnTimerEndBehavior.CackeCarrying:
                                            target = GameManager.manager.foodMachines[2].point;
                                            break;
                                        case PlayerController.OnTimerEndBehavior.SodaCarrying:
                                            target = GameManager.manager.foodMachines[3].point;
                                            break;
                                    }

                                    if (GetMinimumDist() > 4.5f)
                                    {
                                        isTarg = true;
                                    }


                                    toFoodMachine = true;
                                    Delivery = false;
                                    navMeshAgent.enabled = true;
                                    navMeshAgent.isStopped = false;
                                    navMeshAgent.destination = target.position;
                                    animator.SetFloat("Movement", 1f);
                                }
                            }
                            else
                            {
                                if (services.Count > 0)
                                {

                                    s = GetMinimumTimeCustomer();
                                    pcc = s.pc;
                                    target = s.pc.customerPoint;


                                    toFoodMachine = false;
                                    Delivery = true;
                                    navMeshAgent.enabled = true;
                                    navMeshAgent.isStopped = false;
                                    navMeshAgent.destination = target.position;
                                    animator.SetFloat("Movement", 1f);
                                }
                            }




                        }
                        else
                        {
                            s = GetMinimumTimeCustomer();
                            pcc = s.pc;
                            target = s.pc.customerPoint;

                            toFoodMachine = false;
                            Delivery = true;
                            navMeshAgent.enabled = true;
                            navMeshAgent.isStopped = false;
                            navMeshAgent.destination = target.position;
                            animator.SetFloat("Movement", 1f);
                        }



                    }
                    else
                    {
                        if (isTarg)
                            animator.SetFloat("Movement", 1f);
                    }


                    if (toFoodMachine)
                    {
                        if (Vector3.Distance(transform.position, target.position) < 0.1f)
                        {
                            for (int i = 0; i < services.Count; i++)
                            {
                                if (!services[i].isItemGoten && services[i].food == s.food)
                                {
                                    services[i].isItemGoten = true;
                                    grabItem(s.food, services[i].customer);
                                }
                            }

                            isTarg = false;
                            toFoodMachine = false;
                        }

                        animator.SetFloat("Movement", 1f);
                    }

                    if (Delivery)
                    {
                        if (s.customer.isWaitingForFood)
                        {
                            if (Vector3.Distance(transform.position, target.position) < 0.1f)
                            {
                                s.customer.DeliveredFood();
                                GiveAwayItem(s.food);
                                services.Remove(s);
                                s = null;
                                Delivery = false;
                                animator.SetFloat("Movement", 0f);
                                //Debug.Log(s.food+" AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            }
                        }
                        else
                        {
                            for (int i = 0; i < carryingItems.Count; i++)
                            {
                                if (carryingItems[i].cus == s.customer)
                                {
                                    carryingItems[i].ordered = false;
                                    break;
                                }
                            }

                            Delivery = false;
                            services.Remove(s);
                            s = null;
                        }


                        isTarg = false;
                    }




                    break;

                case PersonalType.CashierPC:
                    if (target != null)
                    {


                        if (Vector3.Distance(transform.position, target.position) < 0.1f)
                        {

                            animator.SetFloat("Movement", 0f);

                            if (GameManager.manager.cashbox_pc.isAnyone())
                            {
                                if (!active)
                                {
                                    ActivateTimer();
                                }


                                time(PlayerController.OnTimerEndBehavior.PayOffCustomer);
                            }
                        }


                    }
                    else
                    {
                        target = GameManager.manager.cashbox_pc.chashierPoint;

                        navMeshAgent.enabled = true;
                        navMeshAgent.isStopped = false;
                        navMeshAgent.destination = target.position;
                        animator.SetFloat("Movement", 1f);
                    }
                    break;
                case PersonalType.CashierShelf:
                    if (target != null)
                    {


                        if (Vector3.Distance(transform.position, target.position) < 0.1f)
                        {

                            animator.SetFloat("Movement", 0f);

                            if (GameManager.manager.cashbox_items.isAnyone())
                            {
                                if (!active)
                                {
                                    ActivateTimer();
                                }


                                time(PlayerController.OnTimerEndBehavior.PayOffCustomer);
                            }
                        }


                    }
                    else
                    {
                        target = GameManager.manager.cashbox_items.chashierPoint;

                        navMeshAgent.enabled = true;
                        navMeshAgent.isStopped = false;
                        navMeshAgent.destination = target.position;
                        animator.SetFloat("Movement", 1f);
                    }
                    break;

                case PersonalType.StashWorker:
                    if (target != null)
                    {
                        //Debug.Log(Vector3.Distance(transform.position, target.position));
                        if (Vector3.Distance(transform.position, target.position) < 0.12f)
                        {

                            if (isShelf)
                            {

                                for(int i = 0; i < carryingItems.Count;i++)
                                    curShelf.ActiveOne(lvl);
                              
                                ClearItems();
                                
                                
                                
                               


                                isShelf = false;
                                isLoadingStash = false;

                                target = null;
                            }

                            if (isLoadingStash)
                            {
                                animator.SetFloat("Movement", 0f);
                                int j = curShelf.EmptyAmount();
                                Debug.Log(j +" j");
                                for (int i = 0; i < j; i++)
                                {
                                   // Debug.Log(curLoadingBoard.isAvalible()+ "  "+ carryingItems.Count);
                                    if (curLoadingBoard.isAvalible() && carryingItems.Count < 2)
                                    {
                                        
                                        
                                        float y = 0;
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

                                        lvl = curLoadingBoard.GetFirstAvalible();
                                        
                                        switch (curShelf.shelfType)
                                        {

                                            case PlayerController.OnTimerEndBehavior.PCGetUp:
                                                curCarryingYPoint += y + 0.25f;
                                                animator.SetLayerWeight(1, 1);
                                                var a = Instantiate(GameManager.manager.item[lvl], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                                                a.transform.rotation = transform.rotation;
                                                a.transform.localScale *= 3;
                                                Item itemA = a.AddComponent(typeof(Item)) as Item;
                                                itemA.type = curShelf.shelfType;
                                                itemA.lvl = lvl;
                                                isCarrying = true;
                                                carryingItems.Add(itemA);
                                                //curLoadingBoard.RemoveItem(curLoadingBoard.curItem);
                                                break;

                                            case PlayerController.OnTimerEndBehavior.MouseGetUp:
                                                curCarryingYPoint += y;
                                                animator.SetLayerWeight(1, 1);
                                                var a1 = Instantiate(GameManager.manager.item[lvl + 3], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                                                a1.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
                                                a1.transform.localScale *= 2;
                                                Item itemA1 = a1.AddComponent(typeof(Item)) as Item;
                                                itemA1.type = curShelf.shelfType;
                                                itemA1.lvl = lvl;
                                                isCarrying = true;
                                                carryingItems.Add(itemA1);
                                                //curLoadingBoard.RemoveItem(curLoadingBoard.curItem);
                                                break;

                                            case PlayerController.OnTimerEndBehavior.KeyBoardGetUp:
                                                curCarryingYPoint += y;
                                                animator.SetLayerWeight(1, 1);
                                                var a2 = Instantiate(GameManager.manager.item[lvl + 6], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                                                a2.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                                                a2.transform.localScale *= 2;
                                                Item itemA2 = a2.AddComponent(typeof(Item)) as Item;
                                                itemA2.type = curShelf.shelfType;
                                                itemA2.lvl = lvl;
                                                isCarrying = true;
                                                carryingItems.Add(itemA2);
                                                //curLoadingBoard.RemoveItem(curLoadingBoard.curItem);
                                                break;

                                            case PlayerController.OnTimerEndBehavior.MonitorGetUp:
                                                curCarryingYPoint += y + 0.15f;
                                                animator.SetLayerWeight(1, 1);
                                                var a3 = Instantiate(GameManager.manager.item[lvl + 9], new Vector3(carryingParent.position.x, carryingParent.position.y + curCarryingYPoint, carryingParent.position.z), Quaternion.identity, carryingParent.transform);
                                                a3.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
                                                a3.transform.localScale *= 3;
                                                Item itemA3 = a3.AddComponent(typeof(Item)) as Item;
                                                itemA3.type = curShelf.shelfType;
                                                itemA3.lvl = lvl;
                                                isCarrying = true;
                                                carryingItems.Add(itemA3);
                                                //curLoadingBoard.RemoveItem(curLoadingBoard.curItem);
                                                break;




                                        }
                                        lastCarryingItem = curShelf.shelfType;

                                        curLoadingBoard.GetFirstAvalible();
                                        curLoadingBoard.RemoveItem(curLoadingBoard.curItem);


                                    }

                                    if (!curLoadingBoard.isAvalible())
                                    {
                                       // Debug.Log("q");
                                      //  target = null;
                                       // navMeshAgent.speed = 0;
                                       // animator.SetFloat("Movement", 0f);
                                    }
                                }

                                if (carryingItems.Count > 0)
                                {
                                    isShelf = true;
                                    isLoadingStash = false;

                                    target = curShelf.customerPoint[0].gameObject.transform;
                                    navMeshAgent.enabled = true;
                                    navMeshAgent.isStopped = false;
                                    navMeshAgent.destination = target.position;
                                    animator.SetFloat("Movement", 1f);
                                }
                            }

                            if(worker)
                            {
                                animator.SetFloat("Movement", 0f);
                                target = null;
                            }

                        }


                    }
                    else
                    {
                        curShelf = null;
                        bool ac = false;

                        for (int i = 0; i < GameManager.manager.PCshelf.Count; i++)
                        {
                            if (GameManager.manager.PCshelf[i].isNeded() && GameManager.manager.PCshelf[i].isShelfBulded && !GameManager.manager.loadingBoards[i].isStashEmpty())
                            {
                                curShelf = GameManager.manager.PCshelf[i];
                                //Debug.Log(curShelf.name);
                                ac = true;
                                break;
                            }

                        }

                        /*
                        if (!ac)
                        {
                            for (int i = 0; i < GameManager.manager.PCshelf.Count; i++)
                            {
                                if (GameManager.manager.PCshelf[i].isEmpty() && GameManager.manager.PCshelf[i].isShelfBulded)
                                {
                                    curShelf = GameManager.manager.PCshelf[i];
                                    //Debug.Log(curShelf.name);
                                    ac = true;
                                    break;
                                }

                            }
                        }
                    */
                        
                        if(ac)
                        {
                            for (int i = 0; i < GameManager.manager.loadingBoards.Count; i++)
                            {
                                if (curShelf.shelfType == GameManager.manager.loadingBoards[i].typeOfItem)
                                {
                                    curLoadingBoard = GameManager.manager.loadingBoards[i];
                                    target = GameManager.manager.loadingBoards[i].point;
                                    break;
                                }
                            }
                            worker = false;
                            isLoadingStash = true;
                            isShelf = false;
                            navMeshAgent.enabled = true;
                            navMeshAgent.isStopped = false;
                            navMeshAgent.destination = target.position;
                            animator.SetFloat("Movement", 1f);
                        }
                       else
                        {
                           if(!worker)
                            {
                                target = GameManager.manager.workerPoint;
                                worker = true;
                                navMeshAgent.enabled = true;
                                navMeshAgent.isStopped = false;
                                navMeshAgent.destination = target.position;
                                animator.SetFloat("Movement", 1f);
                            }
                           else
                            {
                                animator.SetFloat("Movement", 0f);
                            }
                           
                        }

                        
                      


                    }
                    break;
            }
        }
    }
}
