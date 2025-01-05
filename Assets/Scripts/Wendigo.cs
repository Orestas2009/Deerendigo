using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class Wendigo : MonoBehaviour
{
    GameObject Player;
    NavMeshAgent Agent;
    [SerializeField] LayerMask groundLayer, Playerlayer;
    Vector3 Destpoint;
    bool WalkPointSet;
    [SerializeField] float range;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        if (!WalkPointSet) SearchForDest();
        if (WalkPointSet) Agent.SetDestination(Destpoint);
        if(Vector3.Distance(transform.position,Destpoint) < 10) WalkPointSet = false;
    }
    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);
        Destpoint = new Vector3(transform.position.x + x,transform.position.y,transform.position.z + z);
        if (Physics.Raycast(Destpoint, Vector3.down, groundLayer))
        {
            WalkPointSet = true;
        }
    } 
}
