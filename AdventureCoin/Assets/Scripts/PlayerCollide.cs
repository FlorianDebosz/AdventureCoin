using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    public int nbCoin;
    public GameObject pickUpParticles;
    public GameObject SnailsParticles;
        private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Coin"){
            GameObject CoinParticles = Instantiate(pickUpParticles, other.transform.position, Quaternion.identity);
            Destroy(CoinParticles, 0.5f);
            Destroy(other.gameObject);
            nbCoin++;
            print(nbCoin);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.tag == "SnailDamage"){
            print("Aie !");
        }else if(hit.gameObject.tag == "SnailHurted") {
            print("Coulé !");
            GameObject SnailsHit = Instantiate(SnailsParticles, hit.transform.position, Quaternion.identity);
            Destroy(SnailsHit, 0.6f);
            Destroy(hit.gameObject.transform.parent.gameObject,0.1f);
        }
    }
}
