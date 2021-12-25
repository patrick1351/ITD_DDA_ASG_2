using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public GameObject fireSparkPrefab;
    public GameObject fireParticle;
    public FirebaseManager fbManager;

    /// <summary>
    /// Starting chance
    /// </summary>
    float chance;
    List<GameObject> stones = new List<GameObject>();
    public bool fireStarted;

    private void Start()
    {
        chance = 30;
        fireParticle.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Attached this script to the stone
        if(other.gameObject.tag == "stone")
        {
            Stone stoneScript = other.GetComponent<Stone>();
            stoneScript.campfireScript = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Remove the attached script on the stone to prevent traggering campfire outside of zone
        if (other.gameObject.tag == "stone")
        {
            Stone stoneScript = other.GetComponent<Stone>();
            stoneScript.campfireScript = null;
        }
    }

    /// <summary>
    /// Used to ensure script only gets trigger once when colliding
    /// </summary>
    public void triggerCampefire(GameObject stone)
    {
        //Add the stone to the list
        stones.Add(stone);
        Debug.Log(stones.Count);

        //This is to prevent triggering script twice
        if(stones.Count >= 1)
        {
            //create particle effect for spark
            Debug.Log("Triggering");
            Vector3 spawnLocation = stones[0].transform.position;
            var spark = Instantiate(fireSparkPrefab, spawnLocation, Quaternion.identity);
            Destroy(spark, 1f);
            stones.Clear();
            RandomStartFire();
        }
    }

    void RandomStartFire()
    {
        if (!fireStarted)
        {
            float randomRoll = Random.Range(0, 100);
            Debug.LogFormat("Chance needed {0}, Rolled Chance: {1}", chance, randomRoll);
            //Rolls must be lesser than chance if not increase the chance
            if(randomRoll < chance)
            {
                fireStarted = true;
                fireParticle.SetActive(true);
                Debug.Log("CAMPFIRE BURN BURN BURN");

                //Add the campFire Count
                fbManager.addCampFireNumber();
            } else
            {
                Debug.Log("boo hoo better luck next time");
                chance *= 1.5f;
            }
        }
    }
}
