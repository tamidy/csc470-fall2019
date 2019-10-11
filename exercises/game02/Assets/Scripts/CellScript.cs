using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour { 
    public bool alive = false;
    public bool nextAlive;
    bool prevAlive;
    public int x = -1;
    public int y = -1;

    //Every game object have a renderer, each renderer has materials 
    Renderer renderer; 

    // Start is called before the first frame update
    void Start() {
        prevAlive = alive;
    }

    // Update is called once per frame
    void Update() {
        //Checks if the previous cell was alive 
        if (prevAlive != alive) {
            updateColor();
        }
        prevAlive = alive;
    }

    public void updateColor() {
        if (renderer == null) {
            renderer = gameObject.GetComponent<Renderer>();
        }

        if (this.alive) {
            renderer.material.color = Color.black;
        } else {
            renderer.material.color = Color.grey;
        }
    }

    private void OnMouseDown() {
        alive = !alive;
    }
}
