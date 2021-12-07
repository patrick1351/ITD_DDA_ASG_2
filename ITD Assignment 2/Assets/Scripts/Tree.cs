using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int numberOfHitsNeeded;
    int hits;
    public GameObject logsPrefab;

    public void HitTree()
    {
        hits += 1;
        if (hits > numberOfHitsNeeded)
        {
            Debug.Log("Spawning Logs");
            //var log = Instantiate(logsPrefab);
            Destroy(this.gameObject);
        }
    }
}
