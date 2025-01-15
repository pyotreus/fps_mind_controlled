using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Transform spawnPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RegisterSpawnPoint(Transform spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}
