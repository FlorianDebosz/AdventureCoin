using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    public static EndScript endScript;
    [SerializeField] private GameObject panelEnd;
    private bool lvlEnd = false;
    [SerializeField] private Image[] stars;

    [SerializeField] private Text[] objText;
    private int starsValue;

    private void Awake() {
        endScript = this;
    }
    
    public void End(){
        //Scoring
        PlayerInfos.playerInfos.GetScore();
        StartCoroutine("EndLvl");
        PlayerInfos.playerInfos.SetScoreText();

        //Stars
        ShowStars(SetStarsAndText());
        panelEnd.SetActive(true);
    }

    IEnumerator EndLvl() {
        yield return new WaitForSeconds(0.3f);
        lvlEnd = true;
    }

    public bool getLvlEnd(){
        return lvlEnd;
    }

    public int SetStarsAndText(){
        //Watch Friends
        if(FriendsScripts.friendsScripts.friendsSaved >= PauseScript.pauseScript.GetChickMax()){
            starsValue++;
            objText[0].text = "<color=#44d804> Liberer vos amis </color>";
        }

        //Watch Coin
        if(PlayerInfos.playerInfos.coinAmount >= PauseScript.pauseScript.GetCoinMax()){
            starsValue++;
            objText[1].text = "<color=#44d804> Collecter toutes les pièces </color>";
        }


        //Watch Snails
        if(PlayerInfos.playerInfos.getSnailsKilled() >= PauseScript.pauseScript.GetSnailsMax()){
            starsValue++;
            objText[2].text = "<color=#44d804> Eliminer tous les adversaires </color>";
        }
        return starsValue;
    }


    public void ShowStars(int starsValue){
        for(int i = 0; i<=starsValue--; i++){
            stars[i].gameObject.SetActive(true);
            iTween.PunchScale(stars[i].gameObject,new Vector3(1,1,0),2f);
        }
    }
}
