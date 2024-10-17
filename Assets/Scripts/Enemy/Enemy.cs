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
    private Vector3 playersLastKnownPosition;
    public GameObject Player { get => player; }
    public Vector3 PlayersLastKnownPosition { get => playersLastKnownPosition; set => playersLastKnownPosition = value; }

    public GameObject debugSphere;

    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;

    [Header("Weapon values")]
    public Transform gunBarrel;
    [Range(0f, 10f)]
    public float fireRate;

    [SerializeField]
    private string currentState;

    [Header("Highlighting")]
    [SerializeField]
    private GameObject highlightObject;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine = GetComponent<StateMachine>(); 
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize();
        path = GameObject.FindGameObjectWithTag("EnemyPath").GetComponent<EnemyPath>();
        Highlight(false);
    }

    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        //debugSphere.transform.position = playersLastKnownPosition;
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

    public void Highlight(bool isTargeted)
    {
        if (highlightObject != null)
        {
            highlightObject.SetActive(isTargeted);
        }
    }
}
