using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrossHairColor;

public class CrossHair : MonoBehaviour
{
    public int Color;
    
    public ChooseColour helperClass;
    
    // Start is called before the first frame update
    void Start()
    {
        helperClass.WhatColour(Color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
