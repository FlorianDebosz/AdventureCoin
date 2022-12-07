using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour
{
    //Public
    public static PlayerCollide playerCollider;
    public bool lockRotation = false;

    //Private
    [SerializeField] private GameObject pickUpParticles;
    [SerializeField] private GameObject snailsParticles;
    [SerializeField] private GameObject mainCam,Camera1,Camera2;
    [SerializeField] private PlayController playController;
    [SerializeField] private AudioClip hitSound,coinCollectSound,splashSound,victorySound,gameoverSound;
    [SerializeField] private SkinnedMeshRenderer renderPlayer;

    [SerializeField] private Collider otherVarEnter, otherVarExit;
    [SerializeField] private CharacterController cc;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool contact = false, contactSound = false;
    [SerializeField] private bool isInvincible = false;

    private void Awake(){
            playController = GetComponent<PlayController>();
            audioSource = GetComponent<AudioSource>();
            playerCollider = this;
    }
    
    private void OnTriggerEnter(Collider other) {
        //Collider with coin
        otherVarEnter = other;
        if(otherVarEnter.gameObject.tag == "Cam1" || otherVarEnter.gameObject.tag == "Cam2")
            Invoke("CamOnTriggerEnter",0.2f);
        
        if(other.gameObject.tag == "Coin"){
            GameObject coinParticles = Instantiate(pickUpParticles, other.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(coinCollectSound);
            Destroy(coinParticles, 0.5f);
            Destroy(other.gameObject);
            PlayerInfos.playerInfos.GetCoins();
        }

        if(other.gameObject.name == "EndZone"){
            PlayerInfos.playerInfos.GetScore();
            audioSource.PlayOneShot(victorySound);
        }
        
        if(other.gameObject.tag == "void") {
            audioSource.PlayOneShot(gameoverSound);
            StartCoroutine("Restart");
        }


        if(other.gameObject.tag == "water"){
            //TODO : Ajouter une animation
            StartCoroutine("EnableControls"); //Desactivate Controls
            audioSource.PlayOneShot(splashSound);
            StartCoroutine("Restart");
            
            
        }
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
            if(PlayerInfos.playerInfos.playerHealth > 1) {
                PlayerInfos.playerInfos.SetHealth(-1);
                isInvincible = true;
                
                StartCoroutine("ResetInvincible");
                StartCoroutine("EnableControls"); //Disable Controls

                //Animation
                iTween.MoveAdd(gameObject,Vector3.back * 2, .5f); // Move Back Player
                iTween.PunchScale(gameObject,new Vector3( .3f, .3f, .3f), .6f); // Scale player

                //Teleport
                StartCoroutine(CheckpointMgr.checkpointMgr.RespawnByHit(cc));
                StartCoroutine("EnableControls"); //Enable Controls
            }else{
                if(!contactSound){
                    StartCoroutine("ResetInvincible");
                    StartCoroutine("EnableControls"); //Disable Controls
                    iTween.MoveAdd(gameObject,Vector3.back * 2, .5f); // Move Back Player
                    iTween.PunchScale(gameObject,new Vector3( .3f, .3f, .3f), .6f); // Scale player
                    contactSound = true;
                    audioSource.PlayOneShot(gameoverSound);
                }
                StartCoroutine("Restart");
            }

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
    
    IEnumerator ResetInvincible() {
        for(int i = 0; i < 10; i++){
            yield return new WaitForSeconds(0.2f);
            renderPlayer.enabled = !renderPlayer.enabled;
        }
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    IEnumerator EnableControls() {
        if (cc.enabled == true && lockRotation == false){
            cc.enabled = !cc.enabled;
            lockRotation = !lockRotation;
        }else if(cc.enabled == false && lockRotation == true){
            yield return new WaitForSeconds(1.9f);
            cc.enabled = !cc.enabled;
            lockRotation = !lockRotation;
        }
        yield return new WaitForSeconds(0f);
    }

    //Getter CharacterController Position
    public Vector3 GetCcPosition() {
        return cc.transform.position;
    }

    IEnumerator DesactivateColorHit() {
        yield return new WaitForSeconds(2.1f);
        iTween.StopByName("colorHit");
    }
        IEnumerator Restart() {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Level_One");
    }
}
