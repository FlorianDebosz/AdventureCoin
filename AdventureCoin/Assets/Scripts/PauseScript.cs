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
    private GameObject[] chickObj;

    private int coinMax;

    private void Start() {
        pauseScript = this;
        coinObj = GameObject.FindGameObjectsWithTag("Coin");
        snailsObj = GameObject.FindGameObjectsWithTag("Snails");
        chickObj = GameObject.FindGameObjectsWithTag("Chick");
        coinMax = coinObj.Length + (snailsObj.Length * PlayerCollide.playerCollider.getsnailsNumberOfLoot());
    }

    public void setChickObjText() {
        if(FriendsScripts.friendsScripts.friendsSaved >= chickObj.Length)
            chickObjText.text = "<color=#838383>- " + FriendsScripts.friendsScripts.friendsSaved + " Amis sur " + chickObj.Length + " libéré </color>";
        else
            chickObjText.text = "- " + FriendsScripts.friendsScripts.friendsSaved + " Amis sur " + chickObj.Length + " libéré";
    }

    public void setCoinObjText() {
        if(PlayerInfos.playerInfos.coinAmount >= coinMax)
            coinObjText.text = "<color=#838383>- " + PlayerInfos.playerInfos.coinAmount + " Pièces collectés sur " + coinMax + "</color>";
        else
            coinObjText.text = "- " + PlayerInfos.playerInfos.coinAmount + " Pièces collectés sur " + coinMax;
    }

    public void setSnailsObjText() {
        if(PlayerInfos.playerInfos.getSnailsKilled() >= snailsObj.Length)
            snailsObjText.text = "<color=#838383>- " + PlayerInfos.playerInfos.getSnailsKilled() + " Ennemis vaincus sur " + snailsObj.Length + "</color>";
        else
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
