using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SnailsScript : MonoBehaviour
{   
    [Range(0.5f,50f)] //Slider for adapt distanceDectect
    [SerializeField] private float distanceDectect = 3f;
    [SerializeField] private Transform[] points;
    private int destinationIndex = 0;
    private NavMeshAgent agent;
    private Transform playerTransform;
    private float speedMax, speedMin;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if(agent != null){
            destinationIndex = Random.Range(0,points.Length);
        }
        speedMax = agent.speed + 0.5f;
        speedMin = agent.speed;
    }

    private void Update() {
        Walk();
        SearchPlayer();
        SetMobSize();
    }

    private void SetMobSize(){
        if(Vector3.Distance(transform.position, playerTransform.position) <= distanceDectect + 1.2){
            iTween.ScaleTo(gameObject,Vector3.one, 0.7f);
        }else{
            iTween.ScaleTo(gameObject,new Vector3(0.2f,0.2f,0.2f), 0.7f);
        }
    }

    //Search player
    private void SearchPlayer(){
        float distanceFromPlayer = Vector3.Distance(transform.position,playerTransform.position);
        if (distanceFromPlayer <= distanceDectect){
            //Player Detected
            agent.destination = playerTransform.position;
            agent.speed = speedMax;
        }else {
            agent.destination = points[destinationIndex].position;
            agent.speed = speedMin;
        }
    }

    //Walk Function
    private void Walk(){
        float dist = agent.remainingDistance;
        if(dist <= 0.05f){
            destinationIndex = Random.Range(0,points.Length);
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
