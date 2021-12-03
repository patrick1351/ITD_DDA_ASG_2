using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    //Object will turn emission and highlight object
    public void OnHover()
    {
        //Get all mesh randerers and store them
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        //look through all meshrenderers and turn on emission
        foreach (MeshRenderer mesh in meshRenderers)
        {
            mesh.material.EnableKeyword("_EMISSION");
        }
    }

    //Turn off emission and stop highlighting
    public void ExitHover()
    {
        //Get all mesh randerers and store them
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        //look through all meshrenderers and turn on emission
        foreach (MeshRenderer mesh in meshRenderers)
        {
            mesh.material.DisableKeyword("_EMISSION");
        }
    }
}
