using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    //Todo
    //Delate the gameobject

    public Collider craftingSpot;
    public GameObject axePrefab;
    //public Dictionary<string, int> curentItem = new Dictionary<string, int>();
    public List<recipe> craftingRecipe = new List<recipe>();

    [SerializeField]
    bool craftAxe;

    //GameObject[] stone; 


    int stone;
    int stick;
    int grass;
    int rope;

    private void OnTriggerEnter(Collider enter)
    {
        Debug.Log(enter.name + " has entered.");
        if(enter.gameObject.tag == "hand")
        {
            Debug.Log(enter.gameObject.name + " is a hand.");

            //Loop through all the crafting recipe
            foreach(recipe i in craftingRecipe)
            {
                //if the amount is the same for all then craft item
                if(i.stone == stone && i.stick == stick && i.rope == rope && i.grass == grass)
                {
                    //Todo
                    //Destory object used in crafting recipe

                    Vector3 craftingSpotPosition = this.transform.position;
                    Vector3 spawnLocation = new Vector3(craftingSpotPosition.x, craftingSpotPosition.y + 0.5f, craftingSpotPosition.z);
                    var spawn = Instantiate(i.result, spawnLocation, Quaternion.identity);
                    Debug.Log("Spawned Succefully");
                    return;
                } else
                {
                    Debug.LogFormat("{0} does not have the same recipie", i.objectName);
                }
            }
        }

        if (enter.tag == "stone")
            stone += 1;
        if (enter.tag == "stick")
            stick += 1;
        if (enter.tag == "grass")
            grass += 1;
        if (enter.tag == "rope")
            rope += 1;

    }

    private void OnTriggerExit(Collider exit)
    {
        Debug.Log(exit.name + " has exit.");
        if (exit.tag == "stone")
            stone -= 1;
        if (exit.tag == "stick")
            stick -= 1;
        if (exit.tag == "grass")
            grass -= 1;
        if (exit.tag == "rope")
            rope -= 1;
    }

    private void Update()
    {
        
    }
}
