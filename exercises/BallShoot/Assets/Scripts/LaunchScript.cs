using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScript : MonoBehaviour
{
    //References
    public Rigidbody rigidBody;
    float chargeEnergy = 0;
    public float chargeRate = 10;

    // Update is called once per frame
    void Update()
    {
        //If the spacebar is pressed but not released yet 
        if (Input.GetKey(KeyCode.Space))
        {
            //deltaTime = how much time has passed after the first function call 
            chargeEnergy += chargeRate * Time.deltaTime;
        }

        //When the spacebar is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //useGravity - controls whether gravity affects this rigidbody 
            rigidBody.useGravity = true;
            rigidBody.AddForce(transform.forward * 1000);
        }
    }
}