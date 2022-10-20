using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string Game;

    public GameObject Options;
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
        Options.SetActive(true);
    }
    public void CloseOptions()
    {
        Options.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("quitting");
    }
}
