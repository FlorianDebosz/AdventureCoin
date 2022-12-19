using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private MeshCollider chickCollider;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float actualSpeed;
    [SerializeField] private Vector3 oldPosition;
    [SerializeField] private Transform target;

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = moveSpeed;
        actualSpeed = Vector3.Distance(oldPosition,transform.position) * 100f;
    }

    private void FixedUpdate() {
        if(FriendsScripts.friendsScripts.canMove){
            if(FriendsScripts.friendsScripts.canMove){
                //Get Actual Speed
                actualSpeed = Vector3.Distance(oldPosition,transform.position) * 100f;
                oldPosition = transform.position;

                Vector3 reachPosition = playerTransform.position - (2.5f*Vector3.right);
                
                if(Vector3.Distance(transform.position,reachPosition) > 1){
                    agent.destination = reachPosition;
                }

                if(chickCollider){
                    chickCollider.enabled = false; // Still not sure about disabling ChickCollider
                }
            }
            
            //Watch Player
            transform.rotation = Quaternion.LookRotation(transform.position - target.position);
        }
    }
}
