using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FriendsScripts : MonoBehaviour
{
    private GameObject actualCage;
    [SerializeField] private Text infoText;
    private bool canOpen = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cage"){
            actualCage = other.gameObject;
            infoText.text = "Appuyer sur e pour ouvrir la cage";
            canOpen = true;
        }       
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Cage"){
            actualCage = null;
            infoText.text = "";
            canOpen = false;
        }
    }

    public void OnOpenCage(){
        if(canOpen){
            iTween.ShakeScale(actualCage, new Vector3(25,25,80), 1);
            Destroy(actualCage,1.2f);
            canOpen = false;
            infoText.text = "";
        }
    }
}
