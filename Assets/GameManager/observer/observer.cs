using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public abstract class Observer
{
        public abstract void OnNotify(GameObject aa);
    }

public class Death :Observer
{
    GameObject deathobject;

    public Death(GameObject deathobject)
    {
        this.deathobject = deathobject;
    }
    public override void OnNotify(GameObject deadman)
    {
        Debug.Log(deadman + "just fall out!!");
        teamscore.instance.score4team1(1);
    }
}



