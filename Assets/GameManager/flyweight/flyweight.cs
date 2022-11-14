using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyweight : MonoBehaviour
{
    // Start is called before the first frame update

    List<snowman> allsnowman;
    void Start()
    {
        allsnowman = new List<snowman>();
        loadsnowman();
        place();
    }


    void loadsnowman()
    {
        snowman temper = new snowman();
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {

                temper.snowmanID = i * 100 + j;
                temper.snowmanPos = new Vector3(Random.Range(-10,10), 2, Random.Range(-10, 10));
                allsnowman.Add(temper);
            }
        }

        Debug.Log(allsnowman.Count);
    }
    void place()
    {
        foreach (snowman snowman in allsnowman)
        {
            snowman.snowmanset();
            
        }
    }
    
}
