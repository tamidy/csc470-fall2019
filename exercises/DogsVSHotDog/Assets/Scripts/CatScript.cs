using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatScript : MonoBehaviour
{
    float speed = 5f;

    /* Set these references in the Unity Inspector
     NOTE: You need to import the Unity UI package to reference the UI objects.*/
    public GameObject treeObj;
    public Text gameLabelText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject is the cat 
        /* How to compute a vector towards something
        Vector3  vecToTree = treeObj.transform.position - gameObject.transform.position;
        vecToTree = vecToTree.normalized;
        transform.position = transform.position + vecToTree * speed * Time.deltaTime;
        */

        //Only if the tree is not destroyed
        if (treeObj != null)
        {
            gameObject.transform.LookAt(treeObj.transform, Vector3.up);
        }
        else
        {
            gameObject.transform.LookAt(Camera.main.transform, Vector3.up);
        }
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    //If two things overlap, then this function gets called 
    private void OnTriggerEnter(Collider other)
    {
        /*When the cat collides with anything, destroy it
         We'll destroy the tree and the cat will move towards the camera which we have a collider for.
         When it hits the collider it'll destroy the camera and break the game.
         */

        Destroy(other.gameObject);
        gameLabelText.text = "Cat Loves YOU!!!";
    }
}
