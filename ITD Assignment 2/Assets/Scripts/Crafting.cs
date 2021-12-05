using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    //Todo
    //Delate the gameobject

    public Collider craftingSpot;
    public GameObject axePrefab;

    [SerializeField]
    bool craftAxe;

    bool stone;
    bool stick;
    bool rope;

    private void OnTriggerEnter(Collider enter)
    {
        Debug.Log(enter.name + " has entered.");
        if(enter.gameObject.tag == "hand")
        {
            Debug.Log(enter.gameObject.name + " is a hand.");
            if (craftAxe)
            {
                Debug.Log("Crafting axe");
                Vector3 craftingSpotPosition = this.transform.position;
                Vector3 spawnLocation = new Vector3(craftingSpotPosition.x, craftingSpotPosition.y + 0.5f, craftingSpotPosition.z);
                var spawnAxe = Instantiate(axePrefab, spawnLocation, Quaternion.identity);
            }
        }

        if (enter.tag == "stone")
            stone = true;
        if (enter.tag == "stick")
            stick = true;
        if (enter.tag == "rope")
            rope = true;

    }

    private void OnTriggerExit(Collider exit)
    {
        Debug.Log(exit.name + " has exit.");
    }

    private void Update()
    {
        if (stone && rope && stick)
        {
            craftAxe = true; 
            Debug.Log("Please craft the axe");
        }
    }
}
