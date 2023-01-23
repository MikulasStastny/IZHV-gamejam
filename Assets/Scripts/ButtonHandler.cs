using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void LoadScene(string sceneName){
        print("in load" + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
