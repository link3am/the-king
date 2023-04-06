using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class teamscore : MonoBehaviour
{
    public static teamscore instance;
    public Text scoreboard;
    public float timelimit;
    
    int team1score;
    int team2score;
    float timer;

    //time trial
    int enemyLeft;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        scoreboard.text = 
            "Team 1: "+ team1score + "\r\n" + 
            "Team 2: "+ team2score + "\r\n" + 
            "Time: " + timer ;
        timer = 0;

        enemyLeft = GameObject.FindGameObjectsWithTag("enemy").Length;

        team1score = 0;
        team2score = 0;
    }

    //private void Update()
    //{
    //    timer += Time.deltaTime;
    //
    //    //if (timer >= timelimit)
    //    //{
    //    //    PauseMenu.isGameOver = true;
    //    //}
    //    int timeleft = (int)(timelimit - timer);
    //    scoreboard.text =
    //        "Team 1: " + team1score + "\r\n" +
    //        "Team 2: " + team2score + "\r\n" +
    //        "Time: " + timeleft;
    //
    //    //time trial mode
    //
    //    //if (enemyLeft < 1)
    //    //{
    //    //    PauseMenu.isGameOver = true;
    //    //}
    //
    //
    //    //scoreboard.text =
    //    //    "Find enemy and knock them out !" + "\r\n" +
    //    //    "Enemy left: " + enemyLeft + "\r\n" +           
    //    //    "Time: " + (int)timer;
    //
    //    //enemy number
    //    enemyLeft = GameObject.FindGameObjectsWithTag("enemy").Length;
    //}
    private void Update()
    {
        timer += Time.deltaTime;

      
        int timeleft = (int)(timelimit - timer);
        scoreboard.text =
            "Your score: " + team1score + "\r\n" +
            "Enemy score: " + team2score + "\r\n"; //+"Time: " + timeleft;

        if (team1score >= 5)
            PauseMenu.p1win = true;
        if (team2score >= 5)
            PauseMenu.p2win = true;
    }
    public void score4team1(int score)
    {
        team1score += score;
        Debug.Log("Team 1 score !");
        //scoreboard.text = "Team 1: " + team1score + "\r\n" + "Team 2: " + team2score;
    }
    public void score4team2(int score)
    {
        team2score += score;
        Debug.Log("Team 2 score !");
        //scoreboard.text = "Team 1: " + team1score + "\r\n" + "Team 2: " + team2score;
    }
    public void zerotimer()
    {
        timer = 0;
    }
    public int getScore4team1()
    {
        return team1score;
    }
    public int getScore4team2()
    {
        return team2score;
    }
    public int getTime4player()
    {
        return (int)timer;
    }
    public void setscore4team1(int aa)
    {
        team1score = aa;
    }
    public void setscore4team2(int aa)
    {
        team2score = aa;
    }
}
