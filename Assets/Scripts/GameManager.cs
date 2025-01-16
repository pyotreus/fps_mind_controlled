using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    private Transform spawnPoint;
    public GameObject playerUIPrefab;
    private GameObject playerUIInstance;

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
        if (playerUIPrefab != null)
        {
            playerUIInstance = Instantiate(playerUIPrefab);
            DontDestroyOnLoad(playerUIInstance);
        }
    }

    public GameObject GetPlayerUI()
    {
        return playerUIInstance;
    }

    public void RegisterSpawnPoint(Transform spawnPoint)
    {
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }       
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

}
