using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    public EnemyPath path;

    [Header("Sight values")]
    private GameObject player;
    public GameObject Player { get => player; }
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;

    [Header("Weapon values")]
    public Transform gunBarrel;
    [Range(0f, 10f)]
    public float fireRate;

    [SerializeField]
    private string currentState;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine = GetComponent<StateMachine>(); 
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        Vector3 playerPosition = player.transform.position;
        if (player != null)
        {
            
            if (Vector3.Distance(transform.position, playerPosition) < sightDistance)
            {
                
                Vector3 targetDirection = playerPosition - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer <= fieldOfView && angleToPlayer >= -fieldOfView)
                {
                    //checking if sight is blocked by an object
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.IsChildOf(player.transform) || hitInfo.transform == player.transform)
                        {
                            return true;
                        }
                    }
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance);

                }
            }
            
        }
        return false;
    }
}
