using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard
{
    public string userName;
    //IDK if you want this to be string or int
    public int speedRunSeconds;
    public string speedRunTime;

    public Leaderboard()
    {

    }

    public Leaderboard(string username,int speedRunSeconds, string speedRunTime)
    {
        this.userName = username;
        this.speedRunSeconds = speedRunSeconds;
        this.speedRunTime = speedRunTime;
    }

    //For returning the data to JSON
    public string LeaderboardToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
