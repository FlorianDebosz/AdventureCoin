using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform playerTransform;
    [SerializeField] private MeshCollider chickCollider;

    [SerializeField] private float moveSpeed;


    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    private void Update() {
        if(FriendsScripts.friendsScripts.canMove){
            Vector3 reachPosition = playerTransform.position - (2*Vector3.right);
            agent.destination = reachPosition;
            
            if(chickCollider){
                chickCollider.enabled = false; // Still not sure about disabling ChickCollider
            }
        }

        
        
    }

}
