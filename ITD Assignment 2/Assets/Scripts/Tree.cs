using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int numberOfHitsNeeded;
    int hits;
    public GameObject logsPrefab;
    public GameObject leavesPrefab;

    public void HitTree()
    {
        hits += 1;
        if (hits > numberOfHitsNeeded)
        {
            Debug.Log("We're going timber");
            //var log = Instantiate(logsPrefab);
            //var leaves  = Instantiate(leavesPrefab);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "axe")
        {
            Debug.Log("Its going down");
            HitTree();
        }
    }
}
