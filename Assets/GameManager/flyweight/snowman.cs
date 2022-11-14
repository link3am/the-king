using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowman 
{

    public int snowmanID;
    public Vector3 snowmanPos;
    public int HP;
    // Start is called before the first frame update

    void start ()
    {
        HP = 1;
    }
    public void snowmanset()
    {
        //objectPooler.instance.getFromPool("snowman", snowmanPos, Quaternion.identity);
        //Debug.Log(snowmanPos);
    }
    
}
