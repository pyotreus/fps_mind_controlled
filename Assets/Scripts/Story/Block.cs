using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : StoryEventObject
{
    public override void Activate()
    {
        MeshRenderer parentMeshRenderer = GetComponent<MeshRenderer>();
        if (parentMeshRenderer != null )
        {
            parentMeshRenderer.enabled = true;
        }
    }
}
