using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public Collider craftingSpot;
    public GameObject axePrefab;
    public List<Recipe> craftingRecipe = new List<Recipe>();
    public List<GameObject> objectInSpot = new List<GameObject>();

    [SerializeField]
    bool craftAxe;

    int stone;
    int stick;
    int grass;
    int rope;

    GameManager gmScript;

    private void Start()
    {
        gmScript = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider enter)
    {
        Debug.Log(enter.name + " has entered.");
        if(enter.gameObject.tag == "hand")
        {
            Debug.Log(enter.gameObject.name + " is a hand.");
            Debug.Log(objectInSpot);

            //Loop through all the crafting recipe
            foreach(Recipe i in craftingRecipe)
            {
                //if the amount is the same for all then craft item
                if(i.stone == stone && i.stick == stick && i.rope == rope && i.grass == grass)
                {
                    Debug.LogFormat("{0} matches! Time to craft.", i.objectName);
                    Vector3 craftingSpotPosition = this.transform.position;
                    Vector3 spawnLocation = new Vector3(craftingSpotPosition.x, craftingSpotPosition.y + 0.5f, craftingSpotPosition.z);
                    var spawn = Instantiate(i.result, spawnLocation, Quaternion.identity);
                    Debug.Log("Spawned Succefully");

                    if(i.objectName == "rope")
                    {
                        gmScript.taskCompleted["rope"] = true;
                    } 
                    else if (i.objectName == "axe")
                    {
                        gmScript.taskCompleted["axe"] = true;
                    }

                    //Destory object used in crafting recipe
                    for (int x = 0; x < objectInSpot.Count; ++x)
                    {
                        Destroy(objectInSpot[x]);
                    }

                    ResetCount();
                    return;
                } else
                {
                    Debug.LogFormat("{0} does not have the same recipie", i.objectName);
                }
            }
        }


        //Todo - make this simplified for in case need upscale
        if (enter.tag == "stone")
        {
            stone += 1;
            objectInSpot.Add(enter.gameObject);
        }
        if (enter.tag == "stick")
        {
            stick += 1;
            objectInSpot.Add(enter.gameObject);
        }
        if (enter.tag == "grass")
        {
            grass += 1;
            objectInSpot.Add(enter.gameObject);
        }
        if (enter.tag == "rope")
        {
            rope += 1;
            objectInSpot.Add(enter.gameObject);
        }

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

        for(int i = 0; i < objectInSpot.Count; ++i)
        {
            if(objectInSpot[i] == exit.gameObject)
            {
                objectInSpot.Remove(objectInSpot[i]);
            }
        }
    }

    void ResetCount()
    {
        stone = 0;
        stick = 0;
        grass = 0;
        rope = 0;
    }
}
