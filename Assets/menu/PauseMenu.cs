using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class PauseMenu : MonoBehaviour
{

    // save dll imports
    [DllImport("SaveDLL")]
    private static extern int GetID();

    [DllImport("SaveDLL")]
    private static extern void SetID(int id);

    [DllImport("SaveDLL")]
    private static extern Vector3 GetPosition();

    [DllImport("SaveDLL")]
    private static extern void SetPosition(float x, float y, float z);

    [DllImport("SaveDLL")]
    private static extern void SaveToFile(int id, float x, float y, float z);

    [DllImport("SaveDLL")]
    private static extern void StartWriting(string fileName);

    [DllImport("SaveDLL")]
    private static extern void EndWriting();


    public static bool isGamePaused;
    public static bool isGameOver;
    public GameObject pauseMenuUI;
    public GameObject overMenuUI;
    public TextMeshProUGUI showScore;
    public GameObject gamingUI;
    string m_Path; 
    string fn;
    public GameObject enemyFab;
    

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isGamePaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& !isGameOver)
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if(isGameOver)
        {
            gameover();
        }

    }

    public void Resume()
    {
        isGamePaused = false;       
        pauseMenuUI.SetActive(false);
        gamingUI.SetActive(true);
        Time.timeScale = 1;
        Debug.Log("Resuming");
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Pause()
    {
        isGamePaused = true;       
        // Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        gamingUI.SetActive(false);
        Time.timeScale = 0;
        Debug.Log("Pausing");
        Cursor.lockState = CursorLockMode.Confined;
    }

    
    public void Save()
    {

        Debug.Log("Saving");

        m_Path = Application.dataPath;
        fn = m_Path + "/save.txt";
        Debug.Log(fn);
        StartWriting(fn);
        //  SaveToFile(1, x, y, z);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("player"))
        {
            if (obj.name.Contains("player"))
            {
                SaveToFile(1,obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
                // 1, x, y, z 
            }

        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("enemy"))
        {
          
            if (obj.name.Contains("enemy"))
            {
                SaveToFile(2, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
                          //2 , x , y, z
            }

        }
        //need to format how it saves

        Debug.Log("saved");

        EndWriting();

    }
    public void Load()
    {

        Debug.Log("Loading");
        m_Path = Application.dataPath;
        fn = m_Path + "/save.txt";

        string line = "";

        foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Destroy(obj2);

        }
        GameObject findplayer = GameObject.FindGameObjectWithTag("player");
        using (StreamReader file = new StreamReader(fn)) //open file
        {
            while ((line = file.ReadLine()) != null) //read text file, line by line until it is empty
            {
                char[] delimiters = new char[] { ',', '\n' }; //delimieters to check and remve when sorting out cordinates
                string[] lines = line.Split(delimiters); //split lines in text file given delimiters

                //load in player
               
                    if (lines[0] == "1") // 1 = player
                    {
                        findplayer.GetComponent<CharacterController>().enabled = false;
                        findplayer.transform.position = new Vector3(float.Parse(lines[1]), float.Parse(lines[2]), float.Parse(lines[3]));
                        findplayer.GetComponent<CharacterController>().enabled = true;
                    }
                
                
                if (lines[0] == "2") //2 = enemy
                {
                    enemyFab = Instantiate(enemyFab, new Vector3(float.Parse(lines[1]), float.Parse(lines[2]), float.Parse(lines[3])), transform.rotation);
                                       
                }
 
            }
            file.Close();
        }
    }

    //Quit 
    public void Quit()
    {       
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    //gameover

    //Game Over
    void gameover()
    {

        //normal
        //gamingUI.SetActive(false);
        //overMenuUI.SetActive(true);
        //Time.timeScale = 0;
        //Cursor.lockState = CursorLockMode.Confined;
        //showScore.text = "Your score: " + teamscore.instance.getScore4team1();

        //time trial
        gamingUI.SetActive(false);
        overMenuUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        if(GameObject.FindGameObjectsWithTag("enemy").Length != 0)
        {
            showScore.text = "You died!";
        }
        else
            showScore.text = "You did it !" + "\r\n" + "Time used: " + teamscore.instance.getTime4player() + "s";
    }

    //Try Again
    public void tryagain()
    {
        
        SceneManager.LoadScene(1);
    }

}
