using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    //Public
    public int nbCoin;
    public GameObject pickUpParticles;
    public GameObject snailsParticles;
    public GameObject mainCam,Camera1,Camera2;
    public PlayController playController;
    public AudioClip hitSound;
    
    //Private
    private Collider otherVarEnter, otherVarExit;
    private CharacterController cc;
    private AudioSource audioSource;
    private bool contact = false;

    private void Start(){
            playController = GetComponent<PlayController>();
            cc = GetComponent<CharacterController>();
            audioSource = GetComponent<AudioSource>();
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
            iTween.PunchPosition(gameObject,Vector3.back * 3,0.5f);
            //iTween.PunchScale(gameObject,Vector3.back * 3,0.5f);
        }else if(collision.gameObject.tag == "SnailHurted" && !contact && !cc.isGrounded) {
                contact = true;
                audioSource.PlayOneShot(hitSound);
                iTween.PunchScale(collision.gameObject.transform.parent.gameObject,new Vector3(30,30,30),0.5f);
                GameObject snailsHit = Instantiate(snailsParticles, collision.transform.position, Quaternion.identity);
                Destroy(snailsHit, 0.7f);
                Destroy(collision.gameObject.transform.parent.gameObject,0.6f);
                StartCoroutine("ResetContact");
        }
    }
        //Coroutine permettant d'attendre 0.8 secondes et de réactiver le contact
    IEnumerator ResetContact() {
        yield return new WaitForSeconds(0.8f);
        contact = false;
    }
}
