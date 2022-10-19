using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Subject
{

    List<Observer> observers = new List<Observer>();

    //Send notifications if something has happened
    public void Notify(GameObject aa)
    {
        for (int i = 0; i < observers.Count; i++)
        {

            observers[i].OnNotify(aa);
        }
    }


    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

}
     
    
