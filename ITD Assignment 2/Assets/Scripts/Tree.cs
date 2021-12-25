using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Tree : MonoBehaviour
{
    public int numberOfHitsNeeded;
    int hits;
    public GameObject logsPrefab;
    public GameObject leavesPrefab;
    GameManager gmScript;
    public FirebaseManager fbManager;

    //For the sound effect when chopping the tree
    AudioSource woodChopping;

    private void Start()
    {
        gmScript = FindObjectOfType<GameManager>();
        woodChopping = GetComponent<AudioSource>();
    }

    public void HitTree()
    {
        woodChopping.Play();
        hits += 1;
        if (hits > numberOfHitsNeeded)
        {
            //Debug.Log("We're going timber");
            Vector3 treePos = this.transform.position;
            var log = Instantiate(logsPrefab, treePos, Quaternion.identity);
            var leaves  = Instantiate(leavesPrefab, treePos, Quaternion.identity);
            gmScript.taskCompleted["chopTree"] = true;

            //Add the number of Trees chopped
            fbManager.addChopTreeNumber();
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "axe")
        {
            //Debug.Log("Its going down");
            HitTree();
        }
    }
}
