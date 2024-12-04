using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public void ToggleBarricade()
    {
        // Disable the MeshRenderer of the parent GameObject
        MeshRenderer parentMeshRenderer = GetComponent<MeshRenderer>();
        if (parentMeshRenderer != null)
        {
            parentMeshRenderer.enabled = false;
        }
        else
        {
            Debug.LogWarning("No MeshRenderer found on the parent GameObject.");
        }

        // Enable the first child GameObject
        if (transform.childCount > 0)
        {
            GameObject child = transform.GetChild(0).gameObject;
            child.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No child GameObject found under the parent.");
        }
    }
}
