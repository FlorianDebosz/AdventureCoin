using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public static PauseScript pauseScript;
    public bool isPaused = false;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private Text chickObjText;

    private void Start() {
        pauseScript = this;
    }

    public void setChickObjText() {
        chickObjText.text = "- " + FriendsScripts.friendsScripts.friendsSaved + " Amis sur 3 libéré";
    }

    public void OnPause() {
        //TODO: play sound
        isPaused = !isPaused;
        menuPause.SetActive(isPaused);

        if(isPaused){
            Time.timeScale = 0f;
        }else{
            Time.timeScale = 1f;
        }
    }
}
