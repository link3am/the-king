using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string Game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Game);
    }

    public void OpenOptions()
    {

    }
    public void CloseOptions()
    {

    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("quitting");
    }
}
