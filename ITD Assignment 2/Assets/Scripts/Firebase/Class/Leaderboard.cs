using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public string userName;
    //IDK if you want this to be string or int
    public string time;

    public Leaderboard()
    {

    }

    public Leaderboard(string username,string time)
    {
        this.userName = username;
        this.time = time;
    }

    //For returning the data to JSON
    public string LeaderboardToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
