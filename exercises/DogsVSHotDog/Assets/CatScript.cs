using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    float speed = 5f;
    public GameObject treeObj;
    //public Text GameObjectText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject is the cat 
        //Vector3  vecToTree = treeObj.transform.position - gameObject.transform.position;
        //vecToTree = vecToTree.normalized;
        //transform.position = transform.position + vecToTree * speed * Time.deltaTime;

        //Only if the tree is not destroyed
        if (treeObj != null)
        {
            gameObject.transform.LookAt(treeObj.transform, Vector3.up);
            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        }
        else;
        {
            gameObject.transform.LookAt(Camera.main.transform, Vector3.up);
        }
    }

    //If two things overlap, then this function gets called 
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        Destroy(other.gameObject);
        //gameLabelText.text = "Cat Loves YOU!!!";
    }
}
