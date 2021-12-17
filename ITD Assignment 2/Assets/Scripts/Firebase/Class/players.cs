using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class players
{
    //For the Player Profile
    public string email;
    public string name;

    public long dateCreatedOn;

    public string region;

    //For the timestamps
    public long lastLoggedIn;

    public int currentGame = 0;

    //Empty constructor
    public players()
    {

    }

    public players(string email,string name, string region, int currentGame = 0)
    {
        this.email = email;
        this.name = name;
        this.region = region;

        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        this.lastLoggedIn = timestamp;
        this.dateCreatedOn = timestamp;
    }

    //For converting the data to JSON
    public string PlayerToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    //For printing the user details (To check the information)
    public string PrintUser()
    {
        return String.Format("User Email : {0} \n Name : {1} \n  Region :{2}",
            this.email, this.name, this.region
            );
    }
}


