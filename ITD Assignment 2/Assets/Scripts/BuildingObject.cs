using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    public string objectName;
    public GameObject objctToBuild;
    public bool completedBuilding;
    GameManager gmScript;

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
        //Get gamemanage script
        gmScript = FindObjectOfType<GameManager>();

        //Adds how much item must be in there
        totalItemNeeded += rock.Count;
        totalItemNeeded += stick.Count;
        totalItemNeeded += logs.Count;
        totalItemNeeded += leaves.Count;

        //Turn off all items
        DisableGameobject(rock);
        DisableGameobject(stick);
        DisableGameobject(logs);
        DisableGameobject(leaves);
    }

    /// <summary>
    /// activate the material that is being thrown in 
    /// </summary>
    /// <param name="neededmaterial">The currrent numebr of item</param>
    /// <param name="material">All the item in the list for the material</param>
    /// <param name="toDestoy">The object that is being thrown in that needs to get destroyed</param>
    /// <returns></returns>
    int AddBuildingMaterial(int neededmaterial, List<GameObject> material, GameObject toDestoy)
    {
        //Stay within item count
        if(neededmaterial < material.Count)
        {
            Debug.Log("Activated"); 
            material[neededmaterial].SetActive(true);
            //check to see if object is done building
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
            gmScript.Check();
        }
    }

    /// <summary>
    /// Disable the objects
    /// </summary>
    /// <param name="list">The objects in the list that was added</param>
    void DisableGameobject(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(false);
        }
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
}
