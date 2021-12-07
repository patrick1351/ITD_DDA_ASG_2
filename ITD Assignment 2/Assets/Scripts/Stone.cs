using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Campfire campfireScript;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "stone")
        {
            //Only works when you are in the boundary of the campfire
            if(campfireScript != null)
            {
                campfireScript.triggerCampefire(this.gameObject);
            }
        }
    }
}
