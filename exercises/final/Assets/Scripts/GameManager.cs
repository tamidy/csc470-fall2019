using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //public Text petName;
    public string namePet;
    //public string test = "1"; FIXME: delete later

    void Start() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update() {

    }

    public void SelectAnimal(string sceneName) {
        //test = "it works"; FIXME: delete later
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void getName(string n) {
        namePet = n;
        //Debug.Log(n); FIXME: delete later
        //petName.text = namePet;
    }
}