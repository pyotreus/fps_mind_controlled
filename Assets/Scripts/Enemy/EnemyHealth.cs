using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private GameObject player;
    private Weapon weapon;
    [SerializeField] float health;
    private EnemyRespawner respawner;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        respawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemyRespawner>();
        weapon = player.GetComponentInChildren<Weapon>();
    }


    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        print("Remaining health: " + health);
        health -= damage;

        if (health <= 0)
        {
            EnemyDeath();
        }

    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
        CancelTarget();
        if (respawner != null)
        {
            respawner.RespawnEnemy();
        }
    }

    public void SetTarget()
    {
        weapon.SelectTarget(gameObject);
    }

    public void CancelTarget()
    {
        weapon.CancelTarget();
    }
}
