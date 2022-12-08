using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIScripts : MonoBehaviour
{   
    [Range(0.5f,50f)] //Slider for adapt distanceDectect
    [SerializeField] private float distanceDectect = 3f;
    [SerializeField] private Transform[] points;
    private int destinationIndex = 0;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent != null){
            agent.destination = points[destinationIndex].position;
        }
    }

    private void Update() {
        float dist = agent.remainingDistance;
        if(dist <= 0.05f){
            destinationIndex++;
            if(destinationIndex > points.Length-1){
                destinationIndex = 0;
            }
        }
        agent.destination = points[destinationIndex].position;
    }

    //Draw Distance visualisation for coding
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,distanceDectect);
    }
}
