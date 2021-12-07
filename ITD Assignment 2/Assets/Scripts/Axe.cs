using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "tree")
        {
            Tree treeScript = collision.gameObject.GetComponent<Tree>();
            treeScript.HitTree();
        }
    }
}
