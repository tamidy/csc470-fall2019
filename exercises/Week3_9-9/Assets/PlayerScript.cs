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
        //Rotating like a tank
        float hAxis = Input.GetAxis("Horizontal"); //either 0, 1, or -1 
        float vAxis = Input.GetAxis("Vertical"); //either 0, 1, or -1 

        //Time.deltaTime amount of time since last update, make it continuous
        transform.Rotate(0, rotSpeed * Time.deltaTime * hAxis, 0);
        transform.position += transform.forward * moveSpeed * Time.deltaTime * vAxis;


        if (Input.GetKey(KeyCode.Space))
        {
            //Move it up to 1.65, bc its height is 2
            //Move it forward 
            //Scale it by 1.5
            //f = floating point number 
            Vector3 pos = transform.position + Vector3.up * 1.65f + transform.forward * 1.5f;
            GameObject hDog = Instantiate(hotDogPrefab, pos, transform.rotation);

            //Disappears in 3 seconds 
            Destroy(hDog, 3);
        }
    }
}
