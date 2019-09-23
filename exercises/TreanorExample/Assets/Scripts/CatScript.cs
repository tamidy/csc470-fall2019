using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatScript : MonoBehaviour
{
	float speed = 25f;

	//Set these references in the Unity inspector
	//NOTE: You need to import the Unity UI package in order to reference the UI objects
	//		  See above where we say "using Unity.UI;"
	public GameObject treeObj;
	public Text gameLabelText;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//We're not using this, but this is an example of how to computer a vector 
		//towards something.
		//Vector3 vecToTree = treeObj.transform.position - gameObject.transform.position;
		//vecToTree = vecToTree.normalized;
		//transform.position = transform.position + vecToTree * speed * Time.deltaTime;

		//If the treeObj exists (the value to variable was set in the Unity inspector), look 
		//at the tree.
		if (treeObj != null) {
			gameObject.transform.LookAt(treeObj.transform, Vector3.up);
		} else {
			//If the treeObj is gone (which happens in OnTriggerEnter), move toward the camera
			gameObject.transform.LookAt(Camera.main.transform, Vector3.up);
		}

		//Move the cat forward at "speed" scaled by deltaTime
		transform.position = transform.position + transform.forward * speed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		//When the cat collides with anything, destroy it
		//First this will destroy the tree, but it will go on to move towards the camera (which 
		//I added a collider), and when it hits that collider it will destroy the camera and
		//break the game.
		Destroy(other.gameObject);

		gameLabelText.text = "Cat Loves YOU!!!";
	}
}
