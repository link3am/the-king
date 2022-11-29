using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   


    public void startgame()
    {
        PauseMenu.isGameOver = false;
        SceneManager.LoadScene(1);
        
    }

    public void quitgame()
    {
        Application.Quit();
    }
}
