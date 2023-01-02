using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FriendsScripts : MonoBehaviour
{
    public static FriendsScripts friendsScripts;
    private GameObject actualCage;
    [SerializeField] private Text infoText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fallPadlock;

    public int friendsSaved;

    private bool canOpen = false;

    private void Start() {
        friendsScripts = this;
        friendsSaved = 0;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cage"){
            actualCage = other.gameObject;
            SetInfoText("Appuyer sur e pour ouvrir la cage");
            canOpen = true;
        }

    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Cage"){
            actualCage = null;
            SetInfoText("");
            canOpen = false;
        }
    }

    private void OnOpenCage(){
        if(canOpen){
            StartCoroutine(PlayerCollide.playerCollider.EnableControls(0f));
            //Animation
            iTween.ShakeScale(actualCage, new Vector3(25,25,80), 1);
            
            //Audio
            audioSource.PlayOneShot(fallPadlock);
            
            canOpen = false;
            infoText.text = "";
            
            StartCoroutine("HideCage");
            StartCoroutine("HideText");
            StartCoroutine(PlayerCollide.playerCollider.EnableControls(2.7f));
            Destroy(actualCage,2.7f);
            friendsSaved += 1;
            PauseScript.pauseScript.setChickObjText();
        }
    }

    private IEnumerator HideCage(){
        yield return new WaitForSeconds(1f);
        //Destroy Cage
        Destroy(actualCage.GetComponent<MeshRenderer>());
        Destroy(actualCage.GetComponent<BoxCollider>());
        Destroy(actualCage.GetComponent<SphereCollider>());

        // Show Text
        actualCage.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = true;
    }

    private IEnumerator HideText(){
        yield return new WaitForSeconds(2.5f);
    
        // Hide Text
        actualCage.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void SetInfoText(string text){
        infoText.text = text;
    }

    public string GetInfoText(){
        return infoText.text;
    }
}
