using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
   public static HealthManager instance;

    public TextMeshProUGUI healthText;
    public int health = 100;


    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }

        healthText.text = health.ToString();
    }

    private void Update()
    {
        healthText.text = health.ToString();
    }

    public void ChangeHealth(int healthValue)
    {
        if (health >= healthValue)
            health -= healthValue;
        else
            health = 0;
    }
    public int getHP()
    {
        return health;
    }
    public void refillHP()
    {
        health = 100;
        
    }
}
