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

    [SerializeField] private GameObject pickUpParticles,snailsParticles,mainCam,Camera1,Camera2,coinLoot;
    [SerializeField] private PlayController playController;
    [SerializeField] private AudioClip hitSound,coinCollectSound,splashSound,victorySound,gameoverSound,hurtedSound;
    [SerializeField] private SkinnedMeshRenderer renderPlayer;

    [SerializeField] private Collider otherVarEnter, otherVarExit;
    [SerializeField] private CharacterController cc;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool contact = false, contactSound = false;
    [SerializeField] private bool isInvincible = false;
    private int snailsNumberOfLoot = 2;

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
            PauseScript.pauseScript.setCoinObjText();
        }

        if(other.gameObject.name == "EndZone"){
            EndScript.endScript.End();
            audioSource.PlayOneShot(victorySound);
        }
        
        if(other.gameObject.tag == "void") {
            audioSource.PlayOneShot(gameoverSound);
            StartCoroutine("Restart");
        }


        if(other.gameObject.tag == "water"){
            StartCoroutine("EnableControls",1.9f); //Desactivate Controls
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
                StartCoroutine(EnableControls(1.9f)); //Disable Controls

                //Animation
                iTween.MoveAdd(gameObject,Vector3.back * 2, .5f); // Move Back Player
                iTween.PunchScale(gameObject,new Vector3( .3f, .3f, .3f), .6f); // Scale player

                //Sound
                audioSource.PlayOneShot(hurtedSound);

                //Verify InfoText
                if(FriendsScripts.friendsScripts.GetInfoText() != ""){
                    FriendsScripts.friendsScripts.SetInfoText("");
                    FriendsScripts.friendsScripts.ChangeTextState(false);
                }

                //Teleport
                StartCoroutine(CheckpointMgr.checkpointMgr.RespawnByHit());
                StartCoroutine(EnableControls(1.9f)); //Enable Controls
            }else{
                if(!contactSound){
                    PlayerInfos.playerInfos.SetHealth(-1);
                    StartCoroutine("ResetInvincible");
                    StartCoroutine(EnableControls(1.9f)); //Disable Controls

                    //Animation
                    iTween.MoveAdd(gameObject,Vector3.back * 2, .5f); // Move Back Player
                    iTween.PunchScale(gameObject,new Vector3( .3f, .3f, .3f), .6f); // Scale player
                    contactSound = true;
                    audioSource.PlayOneShot(gameoverSound);
                }
                StartCoroutine("Restart");
            }

        }else if(collision.gameObject.tag == "SnailHurted") {
            collision.gameObject.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
            if (!contact){
                contact = true;
                audioSource.PlayOneShot(hitSound);
                iTween.PunchScale(collision.gameObject.transform.parent.gameObject,new Vector3(30,30,30),0.5f);
                GameObject snailsHit = Instantiate(snailsParticles, collision.transform.position, Quaternion.identity);

                //Call Looting function
                Loot(coinLoot,snailsNumberOfLoot,collision.transform.position);

                Destroy(snailsHit, 0.7f);
                Destroy(collision.gameObject.transform.parent.gameObject,0.6f);
                StartCoroutine("ResetContact");
                PlayerInfos.playerInfos.setSnailsKilled();
                PauseScript.pauseScript.setSnailsObjText();
            }
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

    public IEnumerator EnableControls(float timer) {
        if (cc.enabled == true && lockRotation == false){
            cc.enabled = !cc.enabled;
            lockRotation = !lockRotation;
            yield return new WaitForSeconds(timer);
        }else if(cc.enabled == false && lockRotation == true){
            yield return new WaitForSeconds(timer);
            cc.enabled = !cc.enabled;
            lockRotation = !lockRotation;
        }
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

    private void Loot(GameObject loot, int numberOfLoot, Vector3 position){
        for(int i = 0; i < numberOfLoot; i++){
            Instantiate(loot, position + new Vector3(Random.Range(-2f,2f),0,Random.Range(-2f,2f)) , Quaternion.identity * Quaternion.Euler(-90,0,0));
        }
    }

    public int getsnailsNumberOfLoot(){
        return snailsNumberOfLoot;
    }
}
