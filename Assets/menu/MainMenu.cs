using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
   
    


    public void startgame()
    {
        PauseMenu.isGameOver = false;
        SceneManager.LoadScene(1);

        p1.SetActive(true);
        p2.SetActive(false);
    }

    public void quitgame()
    {
        Application.Quit();
    }
    public void gohelpPage()
    {
        p1.SetActive(false);
        p2.SetActive(true);
    }
    public void gomainPage()
    {
        p1.SetActive(true);
        p2.SetActive(false);
    }
}
