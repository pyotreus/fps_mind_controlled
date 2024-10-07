using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject deathMessageUI;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        //deathMessageUI.SetActive(false);
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.anyKeyDown)
            {
                RestartGame();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            //Debug.Log("Player took damage: " + damage + ", Current Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

    }

    private void Die()
    {
        isDead = true;
        // Show the death message UI
        //gameObject.SetActive(false);
        //deathMessageUI.SetActive(true);
    }


    void RestartGame()
    {
        ResetHealth();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
