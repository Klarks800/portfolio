using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public Transform shelf;
    public Transform loading;
    public Transform pc;
    public Transform food_hamburger;
    public Transform upgrade;
    public Transform upgrade2;
    public Transform cash1;
    public Transform cash2;
    public RectTransform rt;
    private RectTransform canvas;
    private Camera camera;
    public GameObject arrow;
    public bool active;
   public int oreder = 0;
    float speed = 2f;
    public Transform updtPoint;
    bool gg = false;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        camera = Camera.main;
        //GameManager.manager.upgrades[2].gameObject.SetActive(true);

        if(PlayerPrefs.HasKey("tut"))
        {
            if(PlayerPrefs.GetInt("tut") == 1)
            {
                done = false;
                GameManager.manager.ind = 5;
                GameManager.manager.and = 3;
                GameManager.manager.end = 11;
                GameManager.manager.RecalculatePC();
                GameManager.manager.RecalculateShelf();
                // GameManager.manager.upgrades[0].gameObject.SetActive(true);
                // GameManager.manager.upgrades[1].gameObject.SetActive(true);
                GameManager.manager.upgrades[2].gameObject.SetActive(true);
                GameManager.manager.upgrades[3].gameObject.SetActive(true);
                Destroy(arrow);
               
                active = false;
            }
            else
            {
                PlayerPrefs.DeleteAll();
                active = true;
            }
        }
        else
        {
            PlayerPrefs.DeleteAll();
            active = true;
            PlayerPrefs.SetInt("tut", 0);
        }

        

        if(PlayerPrefs.HasKey("timer1"))
        {
       
            if (PlayerPrefs.GetInt("timer1")== 1)
            {
                upgrdTimer1 = 0;
                upg = true;
                up = false;
                GameManager.manager.upgrades[2].gameObject.SetActive(true);
                GameManager.manager.upgrades[3].gameObject.SetActive(true);
            }
         
            
           
        }
        else
        {
          if (PlayerPrefs.HasKey("tut"))
                if (PlayerPrefs.GetInt("tut") == 1)
                {
                    upgrdTimer1 = 0;
                    upg = true;
                    up = false;
                    GameManager.manager.upgrades[2].gameObject.SetActive(true);
                    GameManager.manager.upgrades[3].gameObject.SetActive(true);
                    PlayerPrefs.SetInt("timer1", 1);
                }
                  



        }



    }
    public void MoveToClickPoint(Vector3 objectTransformPosition)
    {
        Vector2 uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);

        Vector2 ViewportPosition = camera.WorldToViewportPoint(objectTransformPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvas.sizeDelta.x, ViewportPosition.y * canvas.sizeDelta.y);

        rt.localPosition = proportionalPosition - uiOffset;

    }

    public Customer c;
    float timer = 1f;
    bool done = false;
    bool l = false;
    bool d = false;
    bool q = false;

    float upgrdTimer1 = 120;
    float upgrdTimer2 = 60;
    public bool up = false;
    public bool upg = false;

    IEnumerator wait1()
    {
        q = true;
        yield return new WaitForSeconds(1.75f);

        GameManager.manager.player.camControl = true;
        done = false;
        oreder++;
        timer = 1f;
        GameManager.manager.RecalculatePC();
        d = false;
        l = false;
        q = false;
    }

    IEnumerator wait2()

    {
        q = true;
        yield return new WaitForSeconds(0.75f);
        d = false;
        done = false;
        q = false;
        oreder++;
        timer = 1f;
        GameManager.manager.upgrades[2].gameObject.SetActive(true);
        GameManager.manager.player.camControl = true;

    }

    IEnumerator wait3()

    {
        q = true;
        yield return new WaitForSeconds(1f);
        done = false;
        oreder++;
        timer = 1f;
        q = false;
        GameManager.manager.player.camControl = true;
    }

    void Update()
    {
        Debug.Log(PlayerPrefs.HasKey("timer1") + " Hello");



        if (active)
        {
            switch (oreder)
            {

                case 0:
                    MoveToClickPoint(new Vector3(shelf.position.x + 0.25f, 2, shelf.position.z));
                    if (GameManager.manager.PCshelf[0].isShelfBulded)
                    {
                        oreder++;
                        timer = 1f;
                    }


                    break;

                case 1:

                    MoveToClickPoint(new Vector3(loading.position.x - 0.25f, 2, loading.position.z));

                    if (!done)
                    {
                        GameManager.manager.loadingBoards[0].uc.gameObject.SetActive(false);
                        GameManager.manager.player.camControl = false;

                        GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(loading.position.x, 2, loading.position.z - 2), speed * Time.deltaTime);

                        if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(loading.position.x, 2, loading.position.z - 2)) < 0.1f)
                        {
                            if (timer > 0)
                                timer -= Time.deltaTime;
                            else
                            {
                                GameManager.manager.player.camControl = true;
                                done = true;
                            }
                        }
                    }


                    if (GameManager.manager.player.carryingItems.Count > 0 )// && CustomerSpawner.cs.customers[0].here
                    {
                        if (!q)
                            StartCoroutine(wait3());
                    }

                    break;

                case 2:
                    MoveToClickPoint(new Vector3(shelf.position.x + 0.25f, 2, shelf.position.z));

                    if (!done)
                    {
                        GameManager.manager.player.camControl = false;

                        GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(shelf.position.x, 2, shelf.position.z - 2), speed * Time.deltaTime);

                        if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(shelf.position.x, 2, shelf.position.z - 2)) < 0.1f)
                        {
                            if (timer > 0)
                                timer -= Time.deltaTime;
                            else
                            {
                                GameManager.manager.player.camControl = true;
                                done = true;
                            }
                        }
                    }

                    if (GameManager.manager.player.carryingItems.Count == 0)
                    {
                        done = false;
                        oreder++;
                        timer = 1f;
                        GameManager.manager.player.camControl = true;
                    }

                    break;

                case 3:
                    MoveToClickPoint(new Vector3(cash1.position.x + 0.5f, 2, cash1.position.z));

                    if (!done)
                    {
                        GameManager.manager.player.camControl = false;

                        GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(cash1.position.x, 2, cash1.position.z - 2), speed * Time.deltaTime);

                        if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(cash1.position.x, 2, cash1.position.z - 2)) < 0.1f)
                        {
                            if (timer > 0)
                                timer -= Time.deltaTime;
                            else
                            {
                                GameManager.manager.player.camControl = true;
                                done = true;
                            }
                        }
                    }

                    if (GameManager.manager.cashbox_items.isOnCashBox)
                    {
                        l = true;



                    }

                    if (GameManager.manager.cashbox_items.dollarStash.Count > 0 && l)
                    {
                        d = true;

                    }

                    if (d)
                    {
                        Debug.Log("qweqwe");
                        if (GameManager.manager.cashbox_items.dollarStash.Count == 0)
                        {
                            if (GameManager.manager.mon < 10)
                                GameManager.manager.mon = 10;

                            if (!q)
                                StartCoroutine(wait1());
                        }
                    }

                    break;

                case 4:


                    if (!done)
                    {
                        GameManager.manager.player.camControl = false;

                        GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(pc.position.x, 2, pc.position.z - 2), speed * Time.deltaTime);

                        if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(pc.position.x, 2, pc.position.z - 2)) < 0.1f)
                        {
                            if (timer > 0)
                                timer -= Time.deltaTime;
                            else
                            {
                                GameManager.manager.player.camControl = true;
                                done = true;
                            }
                        }
                    }

                    if (GameManager.manager.PC[0].isPCBuilded)
                    {
                        Debug.Log("Builded");
                        MoveToClickPoint(new Vector3(999, 999, 999));
                        if (!l && GameManager.manager.PC[0].cur_customer != null)
                        {
                            c = GameManager.manager.PC[0].cur_customer;
                            l = true;
                        }


                        if (c != null)
                        {
                            if (c.toCashbox)
                            {
                                Debug.Log("Cash");
                                done = false;
                                oreder++;
                                timer = 1f;
                                c = null;
                                l = false;
                                GameManager.manager.player.camControl = true;
                            }



                        }




                    }
                    else
                        MoveToClickPoint(new Vector3(pc.position.x, 2, pc.position.z));
                    break;




                case 5:
                    MoveToClickPoint(new Vector3(cash2.position.x + 0.5f, 2, cash2.position.z));

                    if (!done)
                    {
                        GameManager.manager.player.camControl = false;

                        GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(cash2.position.x, 2, cash2.position.z - 2), speed * Time.deltaTime);

                        if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(cash2.position.x, 2, cash2.position.z - 2)) < 0.1f)
                        {
                            if (timer > 0)
                                timer -= Time.deltaTime;
                            else
                            {
                                GameManager.manager.player.camControl = true;
                                done = true;
                            }
                        }
                    }

                    if (GameManager.manager.cashbox_pc.isOnCashBox)
                    {
                        l = true;


                    }

                    if (GameManager.manager.cashbox_pc.dollarStash.Count > 0 && l)
                    {
                        d = true;

                    }

                    if (d)
                    {
                        Debug.Log("qweqwe");
                        if (GameManager.manager.cashbox_pc.dollarStash.Count == 0)
                        {
                            if (GameManager.manager.mon < 10)
                                GameManager.manager.mon = 10;

                            if (!q)
                            {
                                AppM.app.SendTutorial(1);
                                PlayerPrefs.SetInt("tut", 1);
                                GameManager.manager.player.camControl = true;
                                GameManager.manager.loadingBoards[0].uc.gameObject.SetActive(true);
                                done = false;
                                timer = 1f;
                                c = null;
                                l = false;
                                GameManager.manager.ind = 5;
                                GameManager.manager.and = 3;
                                GameManager.manager.end = 11;
                                GameManager.manager.RecalculatePC();
                                GameManager.manager.RecalculateShelf();
                                // GameManager.manager.upgrades[0].gameObject.SetActive(true);
                                // GameManager.manager.upgrades[1].gameObject.SetActive(true);
                               // GameManager.manager.upgrades[3].gameObject.SetActive(true);
                                MoveToClickPoint(new Vector3(999, 999, 999));

                                active = false;
                                upg = true;
                            }
                            //StartCoroutine(wait2());


                        }


                    }

                    break;
                    /*
                                case 6:
                                    MoveToClickPoint(new Vector3(upgrade.position.x, 2, upgrade.position.z));

                                    if (!done)
                                    {
                                        GameManager.manager.player.camControl = false;

                                        GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(upgrade.position.x, 2, upgrade.position.z - 2), speed * Time.deltaTime);

                                        if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(upgrade.position.x, 2, upgrade.position.z - 2)) < 0.1f)
                                        {
                                            if (timer > 0)
                                                timer -= Time.deltaTime;
                                            else
                                            {
                                                GameManager.manager.player.camControl = true;
                                                done = true;
                                            }
                                        }
                                    }

                                    if (GameManager.manager.up)
                                    {
                                        PlayerPrefs.SetInt("tut", 1);
                                        done = false;
                                        GameManager.manager.ind = 5;
                                        GameManager.manager.and = 3;
                                        GameManager.manager.end = 11;
                                        GameManager.manager.RecalculatePC();
                                        GameManager.manager.RecalculateShelf();
                                       // GameManager.manager.upgrades[0].gameObject.SetActive(true);
                                       // GameManager.manager.upgrades[1].gameObject.SetActive(true);
                                        GameManager.manager.upgrades[3].gameObject.SetActive(true);
                                        Destroy(arrow);

                                            active = false;
                                        }

                                    break;
                    */
            }
        }

       

        if (PlayerPrefs.HasKey("timer1") && !gg)
        {
            if (PlayerPrefs.GetInt("timer1") == 1)
            {
                if (upg)
                {
                    if (!up)
                    {
                        if (upgrdTimer1 > 0)
                            upgrdTimer1 -= Time.deltaTime;
                        else
                        {
                           
                            //MoveToClickPoint(new Vector3(upgrade.position.x, 2, upgrade.position.z));
                            GameManager.manager.upgrades[2].gameObject.SetActive(true);

                            if (!done)
                            {
                                GameManager.manager.player.camControl = false;

                                GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(updtPoint.position.x, 2, updtPoint.position.z - 2), speed * Time.deltaTime);

                                if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(updtPoint.position.x, 2, updtPoint.position.z - 2)) < 0.1f)
                                {
                                    if (timer > 0)
                                        timer -= Time.deltaTime;
                                    else
                                    {
                                        GameManager.manager.player.camControl = true;
                                        PlayerPrefs.SetInt("timer1", 2);
                                        done = true;
                                    }
                                }
                            }

                            if (GameManager.manager.up)
                            {
                                timer = 1f;
                                c = null;
                                l = false;
                                done = false;
                                AppM.app.SendTutorial(1);
                                up = true;
                                GameManager.manager.up = false;
                                GameManager.manager.player.camControl = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (upg)
            {
                if (!up)
                {
                    if (upgrdTimer1 > 0)
                        upgrdTimer1 -= Time.deltaTime;
                    else
                    {
                        
                        MoveToClickPoint(new Vector3(upgrade.position.x, 2, upgrade.position.z));
                        GameManager.manager.upgrades[2].gameObject.SetActive(true);

                        if (!done)
                        {
                           
                            GameManager.manager.player.camControl = false;

                            GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(upgrade.position.x, 2, upgrade.position.z - 2), speed * Time.deltaTime);

                            if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(upgrade.position.x, 2, upgrade.position.z - 2)) < 0.1f)
                            {
                                if (timer > 0)
                                    timer -= Time.deltaTime;
                                else
                                {
                                   
                                    GameManager.manager.player.camControl = true;
                                    done = true;
                                }
                            }
                        }

                        if (GameManager.manager.up)
                        {
                            timer = 1f;
                            c = null;
                            l = false;
                            done = false;
                            MoveToClickPoint(new Vector3(999, 999, 999));
                            up = true;
                            GameManager.manager.up = false;
                            GameManager.manager.player.camControl = true;
                            AppM.app.SendTutorial(1);
                            PlayerPrefs.SetInt("timer1", 1);
                            gg = true;

                        }
                    }
                }
                else
                {
                    if (upgrdTimer2 > 0)
                        upgrdTimer2 -= Time.deltaTime;
                    else
                    {
                        PlayerPrefs.SetInt("timer1", 2);
                        MoveToClickPoint(new Vector3(upgrade2.position.x, 2, upgrade2.position.z));
                        GameManager.manager.upgrades[3].gameObject.SetActive(true);

                        if (!done)
                        {
                            GameManager.manager.player.camControl = false;

                            GameManager.manager.player.followCam.position = Vector3.Lerp(GameManager.manager.player.followCam.position, new Vector3(upgrade2.position.x, 2, upgrade2.position.z - 2), speed * Time.deltaTime);

                            if (Vector3.Distance(GameManager.manager.player.followCam.position, new Vector3(upgrade2.position.x, 2, upgrade2.position.z - 2)) < 0.1f)
                            {
                                if (timer > 0)
                                    timer -= Time.deltaTime;
                                else
                                {
                                    GameManager.manager.player.camControl = true;
                                    done = true;
                                }
                            }
                        }

                        if (GameManager.manager.up)
                        {
                            AppM.app.SendTutorial(2);
                            timer = 1f;
                            c = null;
                            l = false;
                            done = false;
                            upg = false;
                            Destroy(arrow);
                            GameManager.manager.player.camControl = true;

                        }


                    }
                }




            }
        }
           

        
       



    }
}
