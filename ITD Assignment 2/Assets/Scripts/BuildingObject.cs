using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    public GameObject objctToBuild;
    public bool completedBuilding;

    /// <summary>
    /// The total item needed to complete
    /// </summary>
    int totalItemNeeded;
    /// <summary>
    /// How many item is inside
    /// </summary>
    int currentItem;

    int neededRock;
    public List<GameObject> rock = new List<GameObject>();

    int neededStick;
    public List<GameObject> stick = new List<GameObject>();

    int neededLogs;
    public List<GameObject> logs = new List<GameObject>();

    int neededLeaves;
    public List<GameObject> leaves = new List<GameObject>();

    private void Start()
    {
        totalItemNeeded += rock.Count;
        totalItemNeeded += stick.Count;
        totalItemNeeded += logs.Count;
        totalItemNeeded += leaves.Count;

        DisableGameobject(rock);
        DisableGameobject(stick);
        DisableGameobject(logs);
        DisableGameobject(leaves);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " entered");
        if(other.tag == "log")
        {
            neededLogs += AddBuildingMaterial(neededLogs, logs, other.gameObject);
        }

        if (other.tag == "leaf")
        {
            neededLeaves += AddBuildingMaterial(neededLeaves, leaves, other.gameObject);
        }

        if (other.tag == "stone")
        {
            neededRock += AddBuildingMaterial(neededRock, rock, other.gameObject);
        }

        if (other.tag == "stick")
        {
            neededStick += AddBuildingMaterial(neededStick, stick, other.gameObject);
        }
    }

    int AddBuildingMaterial(int neededmaterial, List<GameObject> material, GameObject toDestoy)
    {
        if(neededmaterial < material.Count)
        {
            Debug.Log("Activated"); 
            material[neededmaterial].SetActive(true);
            CheckBuildingStatus();
            Destroy(toDestoy);
            return 1;
        }
        return 0;
    }

    void CheckBuildingStatus()
    {
        currentItem += 1;
        if(currentItem == totalItemNeeded)
        {
            completedBuilding = true;
        }
    }

    void DisableGameobject(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(false);
        }
    }
}
