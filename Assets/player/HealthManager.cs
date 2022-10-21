using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
   public static HealthManager instance;
    public TextMeshProUGUI healthText;
    public float health = 100;


    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        } 
    }

    public void ChangeHealth(int healthValue)
    {

        health -= healthValue;
        healthText.text = healthValue.ToString();   
    }
}
