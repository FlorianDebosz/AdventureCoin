using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    public int nbCoin;
    public GameObject pickUpParticles;
    public GameObject snailsParticles;
    public GameObject Camera1;
    private bool contact = false;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Coin"){
            GameObject coinParticles = Instantiate(pickUpParticles, other.transform.position, Quaternion.identity);
            Destroy(coinParticles, 0.5f);
            Destroy(other.gameObject);
            nbCoin++;
        }

        //Camera Gestion
        if(other.gameObject.tag == "Cam1"){
            Camera1.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
         if(other.gameObject.tag == "Cam1"){
            Camera1.SetActive(false);
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit collision) {
        if(collision.gameObject.tag == "SnailDamage"){
            print("Aie !");
        }else if(collision.gameObject.tag == "SnailHurted" && !contact) {
            contact = true;
            GameObject snailsHit = Instantiate(snailsParticles, collision.transform.position, Quaternion.identity);
            Destroy(snailsHit, 0.6f);
            Destroy(collision.gameObject.transform.parent.gameObject,0.1f);
            StartCoroutine("ResetContact");
        }
    }

    //Coroutine permettant d'attendre 0.8 secondes et de réactiver le contact
    IEnumerator ResetContact() {
            yield return new WaitForSeconds(0.8f);
            contact = false;
    } 
}
