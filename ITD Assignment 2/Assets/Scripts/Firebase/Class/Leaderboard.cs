using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public string userName;
    //IDK if you want this to be string or int
    public int speedRunSeconds;

    public Leaderboard()
    {

    }

    public Leaderboard(string username,int speedRunSeconds)
    {
        this.userName = username;
        this.speedRunSeconds = speedRunSeconds;
    }

    //For returning the data to JSON
    public string LeaderboardToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
