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
}
