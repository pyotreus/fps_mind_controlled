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
        if (other.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
