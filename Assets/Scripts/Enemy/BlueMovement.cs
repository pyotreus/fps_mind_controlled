using nl.ma.utopiaserver.messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueMovement : MonoBehaviour
{

    public float moveSpeed, distanceToTarget;

    private Vector3 target;

    public NavMeshAgent agent;

    public float attackTimer;
    private float attackCooldown = 0.5f;

    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update target position
        target = PlayerMovement.instance.transform.position;

        // Set destination for NavMeshAgent
        agent.destination = target;

        // Stop moving if within distance
        if (Vector3.Distance(transform.position, target) <= distanceToTarget)
        {
            agent.destination = transform.position;
        }

        attackTimer -= Time.deltaTime;
        //Debug.Log("attackerTimer " + attackTimer);
        if (agent.remainingDistance <= 5f)
            {
                animator.SetBool("running", false);
                animator.SetBool("attack", true);
   
                float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
                //Debug.Log("normalizedTime " + normalizedTime);
                if (normalizedTime > 0.4f && normalizedTime < 0.6f) // Assuming the hit occurs at 50-60% of the animation
                {
                    if (attackTimer <= 0f) // Prevent multiple hits during one swing
                    {
                        attackTimer = attackCooldown;
                        DealDamageToPlayer();
                    }
                }

            } else
            {
                animator.SetBool("running", true);
                animator.SetBool("attack", false);
            }
        //}

    }

    void DealDamageToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerMovement.instance.transform.position);
        if (distanceToPlayer <= 5f) // Adjust for bat range
        {
            PlayerHealth playerHealth = PlayerMovement.instance.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
                SoundManager.Instance.bonkSound.Play();
            }
        }
    }

}
