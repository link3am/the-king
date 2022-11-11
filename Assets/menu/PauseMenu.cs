using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;

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
    

    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;

    string m_Path; 
    string fn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    }

    public void Resume()
    {
        isGamePaused = false;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Resuming");
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Pause()
    {
        isGamePaused = true;
       // Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
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
      //  SaveToFile(1, 0, 0, 0);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("player"))
        {
            if (obj.name.Contains("player"))
            {
                SaveToFile(1,obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            }

            if (obj.name.Contains("enemy"))
            {
                SaveToFile(2, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            }

        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("enemy"))
        {
          
            if (obj.name.Contains("enemy"))
            {
                SaveToFile(2, obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            }

        }

        //need to format how it saves


        Debug.Log("saved");

        EndWriting();


    }

    public void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
        

    }


    public void Load()
    {

        Debug.Log("Loading");

        m_Path = Application.dataPath;
        fn = m_Path + "/save.txt";
        Debug.Log(fn);


        Debug.Log("Loaded");


    }


}
