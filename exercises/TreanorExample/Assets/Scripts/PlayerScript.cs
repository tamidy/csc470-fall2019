using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public GameObject hotDogPrefab;

	float rotSpeed = 95;
	float moveSpeed = 5;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//hAxis and vAxis will be -1, 0, or 1 based on whether the player is pressing
		//up, down, left, right, or nothing.
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");

		//Rotate on the y axis based on whether left or right are pressed. See how
		//if left or right is not pressed, hAxis will be 0, and we will multiple the 
		//amount we would rotate by 0, thus we will not rotate.
		transform.Rotate(0, rotSpeed * Time.deltaTime * hAxis, 0);

		//Move forward using the same multiply trick described above (with vAxis this time).
		transform.position += transform.forward * moveSpeed * Time.deltaTime * vAxis;

		//When space is pressed, create a hotdog and tell it to destroy itself in 3 seconds
		if (Input.GetKeyDown(KeyCode.Space)) {
			//pos will be the position we will create the hotdog, we want to position it in
			//front of the player a bit, and at approximately eye level.
			Vector3 pos = transform.position + Vector3.up * 1.65f + transform.forward * 1.5f;

			//Instantiate creates a provided prefab, at a give position, and rotation. The 
			//function returns a reference to the object that was created in case you want
			//to do anything with it. In this case, we will tell Unity to destroy it after
			//3 seconds.
			GameObject hDog = Instantiate(hotDogPrefab, pos, transform.rotation);
			Destroy(hDog, 3);
		}
	}
}
