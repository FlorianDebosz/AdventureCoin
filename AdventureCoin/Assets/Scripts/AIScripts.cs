using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIScripts : MonoBehaviour
{   
    [Range(0.5f,50f)] //Slider for adapt distanceDectect
    [SerializeField] private float distanceDectect = 3f;

    //Draw Distance visualisation for coding
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,distanceDectect);
    }
}
