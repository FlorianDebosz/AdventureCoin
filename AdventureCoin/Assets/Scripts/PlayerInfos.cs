using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos playerInfos;
    public int playerHealth = 3;
    public int coinAmount = 0;
    public Image[] hearts;
    public Text coinTxtAmount,scoreTxt;

    private void Awake() {
        playerInfos = this;
    }
    
    public void SetHealth(int health){
        //Change Health Value
        playerHealth += health;
        SetHealthBar();
        if(playerHealth > 3)
            playerHealth = 3;
        if(playerHealth < 0)
            playerHealth = 0;
    }

    public void GetCoins(){
        coinAmount ++;
        coinTxtAmount.text = coinAmount.ToString(); //Converti et affiche le nombre de pièces
    }

    public void SetHealthBar(){
        //Delete Every Hearts
        foreach(Image img in hearts){
            img.enabled = false;
        }

        //Show Hearts
        for(int i = 0; i<playerHealth; i++){
            hearts[i].enabled = true;
        }
    }

    public int GetScore(){
        int scoreFinal = (coinAmount * 5) + (playerHealth * 10);
        scoreTxt.text = "Score : " + scoreFinal;
        return scoreFinal;
    }
}
