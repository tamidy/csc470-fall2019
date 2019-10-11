using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public CharacterController cc;
    float moveSpeed = 8;
    float rotateSpeed = 70;
    float yVel = 0;
    float jumpForce = 1;
    float gravityModifier = 0.015f;
    bool prevIsGrounded;
    public Text winText;
    int count = 0;
    public Text countText;
    public GameManagerScript gm;

    // Start is called before the first frame update
    void Start() { 
        //Another way to initialize it (on Unity Engine and code)
        cc = gameObject.GetComponent<CharacterController>();
        prevIsGrounded = cc.isGrounded;
        winText.text = "";
        countText.text = "Count: ";
    }

    // Update is called once per frame
    void Update() {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.Rotate(0, rotateSpeed * Time.deltaTime * hAxis, 0);

        Vector3 amountToMove = transform.forward * moveSpeed * Time.deltaTime * vAxis;

        //Jumping: creating our own gravity 
        if (cc.isGrounded) {

            //Just touched the ground 
            if (!prevIsGrounded && cc.isGrounded) {
                yVel = 0;
            }

            yVel = 0;
            if (Input.GetKeyDown(KeyCode.Space)) {
                yVel = jumpForce;
            }
        } else {
            if (Input.GetKeyUp(KeyCode.Space)) {
                yVel = 0;
            }
            yVel += Physics.gravity.y * gravityModifier;
        }

        amountToMove.y = yVel;
        cc.Move(amountToMove);
        prevIsGrounded = cc.isGrounded;

        Vector3 camPos = transform.position + transform.forward * -5 + Vector3.up * 2;
        Camera.main.transform.position = camPos;
        Camera.main.transform.LookAt(transform);
    }

    void OnTriggerEnter(Collider other)
    {
        //The word in quotes must match the tag in the prefab asset
        if (other.gameObject.CompareTag("Treasure"))
        {
            //Activates/Deactivates the GameObject depending on true or false 
            other.gameObject.SetActive(false);
            count++;
            countText.text = "Count: " + count.ToString();

            if (count>=gm.tresCounter) {
                winText.text = "Well done!";
            }
        }
    }
}