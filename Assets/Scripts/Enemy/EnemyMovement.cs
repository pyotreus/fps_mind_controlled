using nl.ma.utopiaserver.messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpeed, distanceToTarget;

    private Vector3 target;

    public NavMeshAgent agent;

    public GameObject bullet;
    public Transform bulletSpawn;

    public float fireRate;
    private float fireCount;

    //public float shotTimer;

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

        // Handle firing
        fireCount -= Time.deltaTime;
        if (fireCount <= 0)
        {
            fireCount = fireRate;

            bulletSpawn.LookAt(PlayerMovement.instance.transform.position + new Vector3(0, 1.5f, 0));

            Vector3 targetDirection = PlayerMovement.instance.transform.position - transform.position;

            float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

            if (Mathf.Abs(angle) < 45f)
            {
                GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/EnemyBullet") as GameObject, bulletSpawn.position, transform.rotation);
                Vector3 shootDirection = (target - bulletSpawn.transform.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-0.5f, 0.5f), Vector3.up) * shootDirection * 100;
                //shotTimer = 0;
                SoundManager.Instance.shootingHeavySound.Play();
                GameObject.Destroy(bullet, 5f);

            }
            else
            {
                agent.destination = target;
            } 

            if (agent.remainingDistance < 0.3f)
            {
                Debug.Log("RUNNING FALSE");
                animator.SetBool("running", false);
            } else
            {
                Debug.Log("RUNNING TRUE");
                animator.SetBool("running", true);
            }
        }

    }

}
