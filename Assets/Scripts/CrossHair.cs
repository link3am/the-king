using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrossHairColor;

public class CrossHair : MonoBehaviour
{
    private int a;
    public ChooseColour helperClass;
    
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        helperClass.WhatColour(a);
    }
}
