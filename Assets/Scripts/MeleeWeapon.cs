using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the weapon
    public float attackDuration = 0.5f; // Duration for which the attack is active

    private Collider weaponCollider;
    private bool isAttacking = false;

    void Start()
    {
        // Find and disable the weapon's collider
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Swing");
            Attack();
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        // Enable the weapon collider to detect hits
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }

        // Wait for the duration of the attack
        yield return new WaitForSeconds(attackDuration);

        // Disable the weapon collider after the attack
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }

        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider hit an enemy
        if (other.CompareTag("Target"))
        {
            // Deal damage to the enemy
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                SoundManager.Instance.hitSound.Play();
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    public void ActivateWeapon()
    {
        if (PlayerMovement.instance != null)
        {
            Transform gunTransform = PlayerMovement.instance.transform.Find("Melee");
            if (gunTransform != null)
            {
                GameObject gun = gunTransform.gameObject;
                gun.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Melee GameObject not found as a child of Player.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerMovement instance is not set.");
        }
    }
}
