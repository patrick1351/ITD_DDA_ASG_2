using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class recipe
{
    public string objectName;

    public int stone;
    public int stick;
    public int rope;
    public int grass;

    //The result of the crafting process
    public GameObject result;
}
