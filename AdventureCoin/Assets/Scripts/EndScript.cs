using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public static EndScript endScript;
    [SerializeField] private GameObject panelEnd;
    private bool lvlEnd = false;

    private void Awake() {
        endScript = this;
    }
    
    public void End(){
        PlayerInfos.playerInfos.GetScore();
        StartCoroutine("EndLvl");
        PlayerInfos.playerInfos.SetScoreText();
        panelEnd.SetActive(true);
    }

    IEnumerator EndLvl() {
        yield return new WaitForSeconds(0.5f);
        lvlEnd = true;
    }

    public bool getLvlEnd(){
        return lvlEnd;
    }
}
