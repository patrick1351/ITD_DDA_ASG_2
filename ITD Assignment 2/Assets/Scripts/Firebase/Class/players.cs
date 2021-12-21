using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Players
{
    //For the Player Profile
    public string email;
    public string name;

    public string dateCreatedOn;
    public string timeCreatedOn;

    public string region;

    //For the timestamps
    public string lastLoggedIn;

    public int currentGame = 0;

    //Empty constructor
    public Players()
    {

    }

    public Players(string email,string name, string region, int currentGame = 0)
    {
        this.email = email;
        this.name = name;

        this.lastLoggedIn = System.DateTime.Now.ToString("dd/MM/yyyy");
        //To create the time thats base on local Time
        this.timeCreatedOn = System.DateTime.Now.ToString("HH:mm");

        this.region = region;
        
        this.dateCreatedOn = System.DateTime.Now.ToString("HH:mm " + " dd/MM/yyyy");

        
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


