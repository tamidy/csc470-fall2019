using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour
{
    GameObject playerObj;

    /*In Update, we'll decrement this value constantly, we'll only move forward when its 
     less than 0. So, we can set this to a positive value to cause the dog to not move for
     that amount of time.*/
    float stopTimer = 3;

    // Start is called before the first frame update
    void Start()
    {
        /* Look in the scene for an object named "Player", we'll use this below 
         to have the dog constantly LookAt() the player. 
         NOTE: Looking up GameObjects at runtime like this can be dangerous as 
         Find() will return null if there's no GameObject found, depending how 
         you use the variable, it'll cause a crash at runtime.*/
        playerObj = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerObj.transform, Vector3.up);

        //Decrement the timer variable
        stopTimer -= Time.deltaTime;
        if (stopTimer < 0)
        {
            transform.position += transform.forward * 2 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Check the tag of the object that the dog collided with. If it's
         marked with the tag of "HotDog", set the timer variable to a positive
         value, thus causing the dog to not move forward for an amount of time.*/
        if (other.gameObject.CompareTag("HotDog"))
        {
            stopTimer = 3;
        }
    }

}
