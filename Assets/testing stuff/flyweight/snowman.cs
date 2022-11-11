using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowman 
{

    public int snowmanID;
    public Vector3 snowmanPos;
    // Start is called before the first frame update

    void start ()
    {
       
    }
    public void printpos()
    {
        objectPooler.instance.getFromPool("snowman", snowmanPos, Quaternion.identity);
        Debug.Log(snowmanPos);
    }
    
}
