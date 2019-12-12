using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    void Start() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update() {

    }

    public void SelectAnimal(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void resetButton(string startScreen) {
        SceneManager.LoadScene(startScreen, LoadSceneMode.Single);
    }
}