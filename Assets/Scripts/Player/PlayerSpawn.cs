using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            transform.position = GameManager.Instance.GetSpawnPoint().position;
        }
    }
}
