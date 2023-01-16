using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public List<Customer> customer;
    public List<Customer> customers;
    public static CustomerSpawner cs;
    private float timer = 1f;
    public void Awake()
    {
        cs = this;
    }

    public void Spawn()
    {
        customers.Add(Instantiate(customer[Random.Range(0,customer.Count-1)], transform.position, Quaternion.Euler(0,180,0)));
    }
    public void DestorCustomer1(Customer c)
    {
        customers.Remove(c);
    
    }
    public void DestorCustomer(Customer c)
    {
        customers.Remove(c);
        Destroy(c.gameObject);
    }//shelf * 3 + pc

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            if (customers.Count < GameManager.manager.max)
            {
                Spawn();
            }
            timer = Random.Range(0.7f, 1.2f);
        }
        
       
    }
}
