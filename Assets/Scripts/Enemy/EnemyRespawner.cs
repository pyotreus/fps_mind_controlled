using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float respawnTime = 5f;
    public int numberOfEnemies;
    public List<Transform> spawnPoints;
    public GameObject player;
    public Weapon weapon;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            weapon = player.GetComponentInChildren<Weapon>();
        }

        for (int i = 0; i < numberOfEnemies; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomIndex];

            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }

    }

    public void RespawnEnemy()
    {
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);

        // Pick a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate the enemy at the spawn point
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    
}
