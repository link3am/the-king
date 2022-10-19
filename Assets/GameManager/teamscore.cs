using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class teamscore : MonoBehaviour
{
    public static teamscore instance;
    public Text scoreboard;
    // public TextMeshProUGUI text;
    int team1score;
    int team2score;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        scoreboard.text = "Team 1: "+ team1score + "\r\n" + "Team 2: "+ team2score;

    }

    public void score4team1(int score)
    {
        team1score += score;
        Debug.Log("Team 1 score !");
        scoreboard.text = "Team 1: " + team1score + "\r\n" + "Team 2: " + team2score;
    }
    public void score4team2(int score)
    {
        team2score += score;
        Debug.Log("Team 2 score !");
        scoreboard.text = "Team 1: " + team1score + "\r\n" + "Team 2: " + team2score;
    }
}
