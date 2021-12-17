using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    //Attched only to the building area

    public GameObject objctToBuild;

    public int neededLogs;
    public List<GameObject> logs = new List<GameObject>();

    public int neededLeaves;
    public List<GameObject> leaves = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " entered");
        if(other.tag == "log")
        {
            neededLogs += AddBuildingMaterial(neededLogs, logs);
        }
    }

    int AddBuildingMaterial(int neededmaterial, List<GameObject> material)
    {
        if(neededmaterial < material.Count)
        {
            Debug.Log("Activated"); 
            material[neededmaterial].SetActive(true);
            return 1;
        }
        return 0;
    }
}
