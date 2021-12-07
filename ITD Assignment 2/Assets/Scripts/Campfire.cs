using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    /// <summary>
    /// Starting chance
    /// </summary>
    float chance;

    public GameObject fireSparkPrefab;

    bool fireStarted;

    /// <summary>
    /// This is used as a buffer for the number of time the stone is triggering this script 
    /// due to having two stone script attached to the stone
    /// </summary>
    [HideInInspector]
    public int stoneTrigger;

    private void Start()
    {
        stoneTrigger = 0;
        chance = 30;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "stone")
        {
            Stone stoneScript = other.GetComponent<Stone>();
            stoneScript.campfireScript = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "stone")
        {
            Stone stoneScript = other.GetComponent<Stone>();
            stoneScript.campfireScript = null;
        }
    }

    public void triggerCampefire()
    {
        stoneTrigger += 1;
        if(stoneTrigger >= 2)
        {
            Debug.Log("Triggering");
            stoneTrigger = 0;
            var spark = Instantiate(fireSparkPrefab);
            Destroy(spark, 1f);
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
                Debug.Log("CAMPFIRE BURN BURN BURN");
            } else
            {
                Debug.Log("boo hoo better luck next time");
                chance *= 1.5f;
            }
        }
    }
}
