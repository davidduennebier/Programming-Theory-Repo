using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nav Mesh Navigation
using UnityEngine.AI;

public class Enemy : Unit
{

    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float movementSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
