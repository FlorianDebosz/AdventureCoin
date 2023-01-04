using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
     [SerializeField] private Text version;
     private void Awake() {
          version.text = Application.version;
     }


     public void LoadLevelOne(){
          SceneManager.LoadScene(1);
     }
     
     public void Exit(){
          Application.Quit();
     }
   
     public void GitSite(string url) {
          Application.OpenURL(url);
     }  
}
