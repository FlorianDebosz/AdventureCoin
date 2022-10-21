using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    //Public
    public GameObject pickUpParticles;
    public GameObject snailsParticles;
    public GameObject mainCam,Camera1,Camera2;
    public PlayController playController;
    public AudioClip hitSound;
    public SkinnedMeshRenderer renderPlayer;
    
    //Private
    private Collider otherVarEnter, otherVarExit;
    private CharacterController cc;
    private AudioSource audioSource;
    private bool contact = false;
    private bool isInvincible = false;

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
            PlayerInfos.playerInfos.GetCoins();
        }

        if(other.gameObject.name == "EndZone"){
            PlayerInfos.playerInfos.GetScore();
        }

        if(other.gameObject.tag == "water"){
            //TODO : Ajouter une animation

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
        if(collision.gameObject.tag == "SnailDamage" && !isInvincible){
            PlayerInfos.playerInfos.SetHealth(-1);
            isInvincible = true;
            iTween.MoveAdd(gameObject,Vector3.back * 10, .5f); // Move Back Player
            iTween.PunchScale(gameObject,new Vector3( .3f, .3f, .3f), .6f); // Scale player
            //Change Color
            StartCoroutine("ResetInvincible");
            //Teleport


        }else if(collision.gameObject.tag == "SnailHurted" && !contact && !cc.isGrounded) {
                contact = true;
                audioSource.PlayOneShot(hitSound);
                iTween.PunchScale(collision.gameObject.transform.parent.gameObject,new Vector3(30,30,30),0.5f);
                GameObject snailsHit = Instantiate(snailsParticles, collision.transform.position, Quaternion.identity);
                Destroy(snailsHit, 0.7f);
                Destroy(collision.gameObject.transform.parent.gameObject,0.6f);
                StartCoroutine("ResetContact");
        }else if(collision.gameObject.tag == "void") {
            
        }
    }
        //Coroutine permettant d'attendre 0.8 secondes et de réactiver le contact
    IEnumerator ResetContact() {
        yield return new WaitForSeconds(0.8f);
        contact = false;
    }
    
    IEnumerator ResetInvincible() {
        for(int i = 0; i < 10; i++){
            yield return new WaitForSeconds(0.2f);
            renderPlayer.enabled = !renderPlayer.enabled;
        }
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    IEnumerator GetHitted() {
        yield return new WaitForSeconds(0.8f);
    }
}
