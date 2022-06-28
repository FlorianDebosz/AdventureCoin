using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    public int nbCoin;
    public GameObject pickUpParticles;
    public GameObject snailsParticles;
    public GameObject mainCam,Camera1,Camera2;
    private bool contact = false;

    public PlayController playController;
    private Collider otherVarEnter, otherVarExit;


    private void Start(){
            playController = GetComponent<PlayController>();
    }
    private void OnTriggerEnter(Collider other) {
        //Collider with coin
        if(other.gameObject.tag == "Coin"){
            GameObject coinParticles = Instantiate(pickUpParticles, other.transform.position, Quaternion.identity);
            Destroy(coinParticles, 0.5f);
            Destroy(other.gameObject);
            nbCoin++;
        }

        otherVarEnter = other;
        Invoke("CamOnTriggerEnter",0.2f);
    }

    private void CamOnTriggerEnter(){   
        //Camera Manager
        if(otherVarEnter.gameObject.tag == "Cam1"){
            Camera1.SetActive(true);
            playController.camActive = 1;
            mainCam.SetActive(false);
        } else if(otherVarEnter.gameObject.tag == "Cam2"){
            Camera2.SetActive(true);
            playController.camActive = 2;
            mainCam.SetActive(false);
        } 
    }

    private void OnTriggerExit(Collider other) {
        otherVarExit = other;
        Invoke("CamOnTriggerExit",0.2f);
    }

    private void CamOnTriggerExit(){
        if(otherVarExit.gameObject.tag == "Cam1"){
            Camera1.SetActive(false);
            mainCam.SetActive(true);
            playController.camActive = 0;
        }else if(otherVarExit.gameObject.tag == "Cam2"){
            Camera2.SetActive(false);
            mainCam.SetActive(true);
            playController.camActive = 0;
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
