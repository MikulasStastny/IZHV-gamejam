using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    //public Button level1, level2;
    public void LoadScene(string sceneName){
        print("in load");
        SceneManager.LoadScene(sceneName);
    }

    /*void Start() {
        level1.onClick.AddListener(delegate {LoadScene("Level_01");});
        level2.onClick.AddListener(delegate {LoadScene("Level_02");});
    }*/
}
