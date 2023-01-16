using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public static Properties properties;
    void Awake()
    {
        properties = this;
    }
    
    public int PC_Build_Price;
    public int PCUpgrade1_Price;
    public int PCUpgrade2_Price;

    public int Shelf_Build_PC_price;
    public int Shelf_Build_Mouse_price;
    public int Shelf_Build_Keyboard_price;
    public int Shelf_Build_Monitor_price;

    public int LoadingUpgrade1_PC_Price;
    public int LoadingUpgrade2_PC_Price;

    public int LoadingUpgrade1_Mouse_Price;
    public int LoadingUpgrade2_Mouse_Price;

    public int LoadingUpgrade1_Keyboard_Price;
    public int LoadingUpgrade2_Keyboard_Price;

    public int LoadingUpgrade1_Monitor_Price;
    public int LoadingUpgrade2_Monitor_Price;


    public int Storage_Worcker_Price;
    public int Cashier_PC_Price;
    public int Cashier_Items_Price;

    public int Loading1Grade_PC_Price;
    public int Loading2Grade_PC_Price;
    public int Loading3Grade_PC_Price;

    public int Loading1Grade_Mouse_Price;
    public int Loading2Grade_Mouse_Price;
    public int Loading3Grade_Mouse_Price;

    public int Loading1Grade_Keyboard_Price;
    public int Loading2Grade_Keyboard_Price;
    public int Loading3Grade_Keyboard_Price;

    public int Loading1Grade_Monitor_Price;
    public int Loading2Grade_Monitor_Price;
    public int Loading3Grade_Monitor_Price;



    public List<int> PC_Prices = new List<int>();


    public List<int> Upgrade_Prices = new List<int>();



    public float PCUpgrade1_Puyment_ForSec;
    public float PCUpgrade2_Puyment_ForSec;
    public float PCUpgrade3_Puyment_ForSec;








    public int PC1Grade_Puyment;
    public int PC2Grade_Puyment;
    public int PC3Grade_Puyment;

    public int Mouse1Grade_Puyment;
    public int Mouse2Grade_Puyment;
    public int Mouse3Grade_Puyment;

    public int Keyboard1Grade_Puyment;
    public int Keyboard2Grade_Puyment;
    public int Keyboard3Grade_Puyment;

    public int Monitor1Grade_Puyment;
    public int Monitor2Grade_Puyment;
    public int Monitor3Grade_Puyment;

}
