using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    /*No physics, no forward key, just transform.forward by itself, floating in space, left and right keys for rotations*/

    private Rigidbody rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
