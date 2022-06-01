using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    public int nbCoin;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Coin"){
            Destroy(other.gameObject);
            nbCoin++;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.tag == "SnailDamage"){
            print("Aie !");
        }else if(hit.gameObject.tag == "SnailHurted") {
            print("Coulé !");
            Destroy(hit.gameObject.transform.parent.gameObject,0.3f);
        }
    }
}
