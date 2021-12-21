using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTasks
{
    public int rope;
    public int axe;
    public int chopTree;
    public int campFire;
    public int tent;

    //Empty constructor
    public UserTasks()
    {

    }

    public UserTasks(int rope, int axe, int chopTree, int tent)
    {
        this.rope = rope;
        this.axe = axe;
        this.chopTree = chopTree;
        this.tent = tent;
    }

    public string TaskToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
