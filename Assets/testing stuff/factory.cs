using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factory : MonoBehaviour
{
    private MonoBehaviour snowpoint;
    private MonoBehaviour enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MonoBehaviour getAsnowpoint()
    {
        return Instantiate(snowpoint);
        
    }
}
