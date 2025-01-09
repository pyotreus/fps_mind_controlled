using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public GameObject deathMessageUI;
    private bool isDead = false;
    public CanvasGroup damageEffect;
    public float fadeSpeed = 5f;
    public float effectDuration = 0.5f;
    private bool isTakingDamage;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        //deathMessageUI.SetActive(false);
        damageEffect.alpha = 0f;
        healthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        if (isTakingDamage)
        {
            // Fade the panel in (alpha to 1)
            damageEffect.alpha = Mathf.Lerp(damageEffect.alpha, 1f, fadeSpeed * Time.deltaTime);
        }
        else
        {
            // Fade the panel out (alpha to 0)
            damageEffect.alpha = Mathf.Lerp(damageEffect.alpha, 0f, fadeSpeed * Time.deltaTime);
        }
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
        ShowDamageEffect();
        if (!isDead)
        {
            currentHealth -= damage;
            healthText.text = currentHealth.ToString();
            Debug.Log("Player took damage: " + damage + ", Current Health: " + currentHealth);

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

    public void ShowDamageEffect()
    {
        isTakingDamage = true;
        Invoke("HideDamageEffect", effectDuration);
    }

    private void HideDamageEffect()
    {
        isTakingDamage = false;
    }
}
