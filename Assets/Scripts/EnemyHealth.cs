using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float health;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        print("Remaining health: " + health);
        health -= damage;
        //if (health > 0)
        //{
        //    Debug.Log("HIT");
        //}

        if (health <= 0)
        {
            EnemyDeath();
        }

    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }

    public void SetTarget()
    {
        Weapon weapon = player.GetComponentInChildren<Weapon>();
        weapon.SetCurrentTarget(gameObject);
    }
}
