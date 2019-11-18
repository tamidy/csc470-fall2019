using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    void Start() {
   
    }

    void Update() {

    }

    public void SelectAnimal(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}