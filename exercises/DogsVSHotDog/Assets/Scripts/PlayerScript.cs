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
        /*Rotating like a tank, either 0, 1, or -1 depending on whether the player is pressing 
        up, down, left, right, or nothing.*/
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        /*Time.deltaTime amount of time since last update, make it continuous.
        Rotate on the y-axis based on whether left or right are pressed, if not pressed, hAxis = 0, 
        and we'll multiply the amount we would rotate by 0, thus we will not rotate. */
        transform.Rotate(0, rotSpeed * Time.deltaTime * hAxis, 0);

        //Move forward using the same multiply trick described above (w/ vAxis this time).
        transform.position += transform.forward * moveSpeed * Time.deltaTime * vAxis;

        //When soace pressed, create a hotdog and tell it to destroy itself in 3 seconds 
        if (Input.GetKey(KeyCode.Space))
        {
            /* Move it up to 1.65, bc its height is 2
            Move it forward 
            Scale it by 1.5
            f = floating point number 
            */

            /*pos will be the position we'll create the hotdog, we want to position it in 
             in front of the player a bit, and at approx eye level.*/
            Vector3 pos = transform.position + Vector3.up * 1.65f + transform.forward * 1.5f;

            /*Instantiate creates a provided prefab, at a given position, and rotation. The
             * function returns a reference to the object that was created in case you want
             to do anything with it. In this case, we will tell Unity to destroy it after 3 sec. */
            GameObject hDog = Instantiate(hotDogPrefab, pos, transform.rotation);

            //Disappears in 3 seconds 
            Destroy(hDog, 3);
        }
    }
}
