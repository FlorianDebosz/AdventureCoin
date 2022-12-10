using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMgr : MonoBehaviour
{
    public Vector3 lastPoint;
    public static CheckpointMgr checkpointMgr;
    void Start()
    {
        lastPoint = transform.position;
        checkpointMgr = this;
    }

    public IEnumerator RespawnByHit(){
        yield return new WaitForSeconds(1.9f);
        transform.position = lastPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Checkpoint"){
            lastPoint = transform.position;
            other.gameObject.GetComponent<CoinAnim>().enabled = true;
        }
    }
}
