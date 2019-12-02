using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //public Text petName;
    public string namePet;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {

    }

    public void SelectAnimal(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void getName(string n) {
        namePet = n;
        Debug.Log(n);
        //petName.text = namePet;
    }
}