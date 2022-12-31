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
    [SerializeField] private Text coinObjText;
    [SerializeField] private Text snailsObjText;

    

    private GameObject[] coinObj;
    private GameObject[] snailsObj;

    private int coinMax;

    private void Start() {
        pauseScript = this;
        coinObj = GameObject.FindGameObjectsWithTag("Coin");
        snailsObj = GameObject.FindGameObjectsWithTag("Snails");
        coinMax = coinObj.Length + (snailsObj.Length * PlayerCollide.playerCollider.getsnailsNumberOfLoot());
    }

    public void setChickObjText() {
        chickObjText.text = "- " + FriendsScripts.friendsScripts.friendsSaved + " Amis sur 3 libéré";
    }

    public void setCoinObjText() {
        coinObjText.text = "- " + PlayerInfos.playerInfos.coinAmount + " Pièces collectés sur " + coinMax;
    }

    public void setSnailsObjText() {
        snailsObjText.text = "- " + PlayerInfos.playerInfos.getSnailsKilled() + " Ennemis vaincus sur " + snailsObj.Length;
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
