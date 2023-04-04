using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IPenter : MonoBehaviour
{

    public InputField inputing;
    public static string ip = "start";
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);        
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log(ip);
        }
    }
    public void getIP()
    {
        ip = inputing.text;
        SceneManager.LoadScene(4);
    }
    public string getString()
    {
        return ip;
    }
}
