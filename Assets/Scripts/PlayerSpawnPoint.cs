using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    void Start()
    {
     if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterSpawnPoint(transform);
        }   
    }

}
