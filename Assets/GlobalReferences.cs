using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{

    public static GlobalReferences Instance { set; get; }

    public GameObject bulletImpactPrefabEffect;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        } else
        {
            Instance = this;
        }
    }
}
