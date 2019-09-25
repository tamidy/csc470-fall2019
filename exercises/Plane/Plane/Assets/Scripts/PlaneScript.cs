using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneScript : MonoBehaviour
{
    /* No physics, no forward key, just transform.forward by itself, floating in space, 
     * left and right keys for rotations*/

    //FIXME: count and win text 
    private Rigidbody rb;
    public float speed;
    float rotateSpeed = 90;
    public Text scoreText;
    public Text winAlert;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        winAlert.text = "";
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        //Recording the axes 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Rotate on y-axis and move forward
        //FIXME: the "front" of my object is not the "front of the plane"
        transform.Rotate(0, rotateSpeed * Time.deltaTime * moveHorizontal, 0);
        transform.position += transform.forward * speed * Time.deltaTime * moveVertical;
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score>=10)
        {
            //Must add the .text for it to be correct 
            winAlert.text = "You're rich now!";
        }
    }

    //Built-in, called when a game object collides with another 
    private void OnTriggerEnter(Collider other)
    {
        //Each Ring must be a trigger 
        //Each Ring must have a tag that is the same as that in the quotations
        //To create a tag, their must be a prefab 
        if (other.gameObject.CompareTag("Ring"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetScoreText();
        }
    }
}
