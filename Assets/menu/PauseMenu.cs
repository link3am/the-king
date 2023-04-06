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


    public static bool inpause;
    public static bool ingaming = false;
    public static bool p1win;
    public static bool p2win;
    public GameObject pauseMenuUI;
    public GameObject overMenuUI;
    public TextMeshProUGUI showScore;
    public GameObject gamingUI;

    public GameObject manager;

    string m_Path; 
    string fn;
    public GameObject enemyFab;
    

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
        //gamingUI.SetActive(false);
        //ingaming = false;
        inpause = false;
        p1win = false;
        p2win = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)&& ingaming)
        {
            if (inpause)
                Resume();
            else
                Pause();
        }
        if (ingaming && !inpause)
        {
            gamingUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            
        }
        else
        {
            gamingUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
        }

    }

    public void Resume()
    {
        inpause = false;       
        pauseMenuUI.SetActive(false);
        gamingUI.SetActive(true);
        Debug.Log("Resuming");
        //Cursor.lockState = CursorLockMode.Locked;

    }

    void Pause()
    {
        inpause = true;       
        // Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        gamingUI.SetActive(false);
        Debug.Log("Pausing");
        //Cursor.lockState = CursorLockMode.Confined;
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

    public void Serverquit()
    {
        MPhoster.serverclose();
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    //void gameover()
    //{
    //
    //    //normal
    //    //gamingUI.SetActive(false);
    //    //overMenuUI.SetActive(true);
    //    //Time.timeScale = 0;
    //    //Cursor.lockState = CursorLockMode.Confined;
    //    //showScore.text = "Your score: " + teamscore.instance.getScore4team1();
    //    //time trial
    //    gamingUI.SetActive(false);
    //    overMenuUI.SetActive(true);
    //    Cursor.lockState = CursorLockMode.Confined;
    //    if(GameObject.FindGameObjectsWithTag("enemy").Length != 0)
    //    {
    //        showScore.text = "You died!";
    //    }
    //    else
    //        showScore.text = "You did it !" + "\r\n" + "Time used: " + teamscore.instance.getTime4player() + "s";
    //}
    //public void tryagain()
    //{
    //    
    //    //SceneManager.LoadScene(1);
    //}

}
