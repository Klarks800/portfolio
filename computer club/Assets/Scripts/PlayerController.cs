using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    private Joystick joystick;
    public float movementSpeed = 2;
    private Vector2 firstTouch;
    private Vector2 secondTouch;
    private float m_currentV = 0;
    private float m_currentH = 0;
    private float m_interpolation = 25;
    private Vector3 m_currentDirection = Vector3.zero;

    //Animation
    public Animator animator;
    public Transform carryingPoint;
    private float curCarryingYPoint;
    public OnTimerEndBehavior OnTimerBehavior;
    private OnTimerEndBehavior lastCarryingItem = OnTimerEndBehavior.none;
    public bool isCarrying = false;
    public List<Item> carryingItems;
    public loadingBoard curLoadingBoard;
    public GameObject isJoyStickActive;
    public Cashbox curChasbox;
    public int maxCapasity = 4;
    public Transform moneyPoint;
    public GameObject effect;
    public bool isAvalibleSpace()
    {
        return maxCapasity <= carryingItems.Count ? false : true;
    }
    float effectTimer = 2f;
    public void Effect()
    {
        effect.SetActive(true);
        effectTimer = 2f;
    }
    public enum OnTimerEndBehavior
    {
        none,
        BurgerCarrying,
        PizzaCarrying,
        CackeCarrying,
        SodaCarrying,
        PayOffCustomer,
        PCGetUp,
        MouseGetUp,
        KeyBoardGetUp,
        MonitorGetUp,
        TrashBucket
    }

    //Camera
    private Camera camera;
    public Transform followCam;

    //UI
    public Transform playerDelayBarPoint;
    private playerDelayBar playerBar;
    public bool active;


    public bool isStanding()
    {
        


        return joystick.Direction == Vector2.zero;
    }

    void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<FixedJoystick>();
        followCam = GameObject.FindGameObjectWithTag("follow").transform;
        playerBar = GameManager.manager.GetPlayerBar(this);
        camera = Camera.main;
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

    public int GiveAwayItem(int n)
    {
        float y = 0;
        switch (carryingItems[n].GetComponent<Item>().type)
        {
            case OnTimerEndBehavior.BurgerCarrying:
                y = 0.35f;
                break;

            case OnTimerEndBehavior.PizzaCarrying:
                y = 0.05f;
                break;

            case OnTimerEndBehavior.CackeCarrying:
                y = 0.23f;
                break;

            case OnTimerEndBehavior.SodaCarrying:
                y = 0.23f;
                break;

            case OnTimerEndBehavior.PCGetUp:
                y = 0.6f;
                break;

            case OnTimerEndBehavior.MouseGetUp:
                y = 0.23f;
                break;

            case OnTimerEndBehavior.KeyBoardGetUp:
                y = 0.05f;
                break;

            case OnTimerEndBehavior.MonitorGetUp:
                y = 0.5f;
                break;
        }
        //Debug.Log(n);
        curCarryingYPoint -= y;
        int lv = carryingItems[n].lvl;
        Destroy(carryingItems[n].gameObject);
        carryingItems.RemoveAt(n);

        for(int i = n; i < carryingItems.Count; i++)
        {
            carryingItems[i].transform.position -= (Vector3.up * y);
        }

        if (carryingItems.Count == 0)
        {
            animator.SetLayerWeight(1, 0);
            curCarryingYPoint = 0;
           isCarrying = false;
        }

        return lv;
    }

    public void Activate()
    {
        if(!isCarrying)
        {
            curCarryingYPoint = 0;
        }
        
        float y = 0;
        if (lastCarryingItem != OnTimerEndBehavior.none)
        {
            switch (lastCarryingItem)
            {
                case OnTimerEndBehavior.BurgerCarrying:
                    y = 0.35f;
                    break;

                case OnTimerEndBehavior.CackeCarrying:
                    y = 0.23f;
                    break;

                case OnTimerEndBehavior.SodaCarrying:
                    y = 0.23f;
                    break;

                case OnTimerEndBehavior.PizzaCarrying:
                    y = 0.05f;
                    break;

                case OnTimerEndBehavior.PCGetUp:
                    y = 0.35f;
                    break;

                case OnTimerEndBehavior.MouseGetUp:
                    y = 0.23f;
                    break;

                case OnTimerEndBehavior.KeyBoardGetUp:
                    y = 0.05f;
                    break;

                case OnTimerEndBehavior.MonitorGetUp:
                    y = 0.35f;
                    break;
            }
        }
        
        
        switch(OnTimerBehavior)
        {
            /*
            case OnTimerEndBehavior.BurgerCarrying:
                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var o = Instantiate(GameManager.manager.food[0], new Vector3(carryingPoint.position.x, carryingPoint.position.y + curCarryingYPoint, carryingPoint.position.z), Quaternion.identity, carryingPoint.transform);
                Item item = o.AddComponent(typeof(Item)) as Item;
                item.type = OnTimerBehavior;
                o.transform.rotation = Quaternion.Euler(-90, 0, 0);
                isCarrying = true;
                carryingItems.Add(item);
                break;

          

            case OnTimerEndBehavior.PizzaCarrying:
               
                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var o1 = Instantiate(GameManager.manager.food[1], new Vector3(carryingPoint.position.x, carryingPoint.position.y + curCarryingYPoint, carryingPoint.position.z), Quaternion.identity, carryingPoint.transform);
                o1.transform.rotation = Quaternion.Euler(-90, 0, 0);
                Item item1 = o1.AddComponent(typeof(Item)) as Item;
                item1.type = OnTimerBehavior;
                isCarrying = true;
                carryingItems.Add(item1);
                break;

            case OnTimerEndBehavior.CackeCarrying:
                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var o2 = Instantiate(GameManager.manager.food[2], new Vector3(carryingPoint.position.x, carryingPoint.position.y + curCarryingYPoint, carryingPoint.position.z), Quaternion.identity, carryingPoint.transform);
                Item item2 = o2.AddComponent(typeof(Item)) as Item;
                item2.type = OnTimerBehavior;
                o2.transform.rotation = Quaternion.Euler(-90, 0, 0);
                isCarrying = true;
                carryingItems.Add(item2);
                break;

            case OnTimerEndBehavior.SodaCarrying:
                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var o3 = Instantiate(GameManager.manager.food[3], new Vector3(carryingPoint.position.x, carryingPoint.position.y + curCarryingYPoint, carryingPoint.position.z), Quaternion.identity, carryingPoint.transform);
                Item item3 = o3.AddComponent(typeof(Item)) as Item;
                item3.type = OnTimerBehavior;
                o3.transform.rotation = Quaternion.Euler(-90, 0, 0);
                isCarrying = true;
                carryingItems.Add(item3);
                break;
          */
            case OnTimerEndBehavior.PCGetUp:
                curCarryingYPoint += y + 0.25f;
                animator.SetLayerWeight(1, 1);
                var a = Instantiate(GameManager.manager.item[lvl], new Vector3(carryingPoint.position.x, carryingPoint.position.y + curCarryingYPoint, carryingPoint.position.z), Quaternion.identity, carryingPoint.transform);
                a.transform.rotation = transform.rotation;
                a.transform.localScale *= 3;
                Item itemA = a.AddComponent(typeof(Item)) as Item;
                itemA.type = OnTimerBehavior;
                itemA.lvl = lvl;
                isCarrying = true;
                carryingItems.Add(itemA);
                curLoadingBoard.RemoveItem(curLoadingBoard.curItem);
                break;
            
            case OnTimerEndBehavior.MouseGetUp:
                curCarryingYPoint += y;
                animator.SetLayerWeight(1, 1);
                var a1 = Instantiate(GameManager.manager.item[lvl+3], new Vector3(carryingPoint.position.x, carryingPoint.position.y + curCarryingYPoint, carryingPoint.position.z), Quaternion.identity, carryingPoint.transform);
                a1.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y+ 90, transform.rotation.eulerAngles.z);
                a1.transform.localScale *= 2;
                Item itemA1 = a1.AddComponent(typeof(Item)) as Item;
                itemA1.type = OnTimerBehavior;
                itemA1.lvl = lvl;
                isCarrying = true;
                carryingItems.Add(itemA1);
                curLoadingBoard.RemoveItem(curLoadingBoard.curItem);
                break;

            case OnTimerEndBehavior.KeyBoardGetUp:
                curCarryingYPoint += y ;
                animator.SetLayerWeight(1, 1);
                var a2 = Instantiate(GameManager.manager.item[lvl + 6], new Vector3(carryingPoint.position.x, carryingPoint.position.y + curCarryingYPoint, carryingPoint.position.z), Quaternion.identity, carryingPoint.transform);
                a2.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                a2.transform.localScale *= 2;
                Item itemA2 = a2.AddComponent(typeof(Item)) as Item;
                itemA2.type = OnTimerBehavior;
                itemA2.lvl = lvl;
                isCarrying = true;
                carryingItems.Add(itemA2);
                curLoadingBoard.RemoveItem(curLoadingBoard.curItem);
                break;

            case OnTimerEndBehavior.MonitorGetUp:
                curCarryingYPoint += y + 0.15f;
                animator.SetLayerWeight(1, 1);
                var a3 = Instantiate(GameManager.manager.item[lvl + 9], new Vector3(carryingPoint.position.x, carryingPoint.position.y + curCarryingYPoint, carryingPoint.position.z), Quaternion.identity, carryingPoint.transform);
                a3.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 90, transform.rotation.eulerAngles.y -90, transform.rotation.eulerAngles.z);
                a3.transform.localScale *= 3;
                Item itemA3 = a3.AddComponent(typeof(Item)) as Item;
                itemA3.type = OnTimerBehavior;
                itemA3.lvl = lvl;
                isCarrying = true;
                carryingItems.Add(itemA3);
                curLoadingBoard.RemoveItem(curLoadingBoard.curItem);
                break;

            case OnTimerEndBehavior.PayOffCustomer:
                curChasbox.isOnCashBox = true;
                break;

            case OnTimerEndBehavior.TrashBucket:
                ClearItems();
                break;


        }
        lastCarryingItem = OnTimerBehavior;
        DisableTimer();
    }

  
    public void ActivateTimer()
    {
        active = true;
        delayTimer = delayTime;
        playerBar.ActivateTimer();
    }
    
    public float delayTimer = 1f;
    public float delayTime = 1f;
    public int lvl;
    public void timer(OnTimerEndBehavior oteb)
    {
        if(active)
        {
            if (delayTimer > 0)
            {
                delayTimer -= Time.deltaTime;
                playerBar.SetTimer(delayTimer);
            }
            else
            {
                OnTimerBehavior = oteb;
                Activate();
                DisableTimer();
            }
        }
    }

    public void DisableTimer()
    {
        active = false;
        playerBar.DisableTimer();
    }

    public bool camControl = true;
    void Update()
    {
     if(effect.activeSelf)
        {
            if (effectTimer > 0)
                effectTimer -= Time.deltaTime;
            else
            {
                effect.SetActive(false);
            }
        }


        
        playerBar.MoveToClickPoint(playerDelayBarPoint.position);
        
        if(camControl)
        followCam.transform.position = Vector3.Lerp(followCam.transform.position, transform.position, Time.deltaTime * 3);
       
        float v = joystick.Vertical;
        float h = joystick.Horizontal;

        Transform cam = camera.transform;

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = cam.forward * m_currentV + cam.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * movementSpeed * Time.deltaTime;

            animator.SetFloat("Movement", direction.magnitude);
        }
    }
}
