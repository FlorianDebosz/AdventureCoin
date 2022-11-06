using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMgr : MonoBehaviour
{
    [SerializeField]private Vector3 lastPoint;
    public static CheckpointMgr checkpointMgr;

    void Start()
    {
        lastPoint = transform.position;
        checkpointMgr = this;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "checkpoint"){
             lastPoint = transform.position;
             other.gameObject.GetComponent<CoinAnim>().enabled = true;
        }
    }

    public IEnumerator RespawnByHit() {
        yield return new WaitForSeconds(1.9f);
        transform.position = lastPoint;
    }
}
