using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /* Called before rendering a frame, where most of our game code will go
     * void Update () {} 
    */

    /* Rigidbody
     * Controls the object's position through physics simulation
     */

    public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody rb;
    private int count;

    void Start()
    {
        //Get a reference of the component I want to work with
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    //Called before performing any physics calculations, where our physics code will go 
    void FixedUpdate()
    {
        /* Input, record axes, playergame object uses a rigidbody and interacts with a physics engine, 
         * we'll use this input to add forces to the rigidbody and 
         * move the playergame object in this scene
        */
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Applies the movement to the sphere
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        //AddForce() - adds a force to the rigidbody
        rb.AddForce(movement * speed);
    }

    //Will be called when ball in contact with pick up object
    void OnTriggerEnter(Collider other)
    {
        //The word in quotes must match the tag in the prefab asset
        if (other.gameObject.CompareTag("PickUp"))
        {
            //Activates/Deactivates the GameObject depending on true or false 
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}