using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneScript : MonoBehaviour
{
    /* No physics, no forward key, just transform.forward by itself, floating in space, 
     * left and right keys for rotations.
    */

    private Rigidbody rb;
    public float speed;
    float rotateSpeed = 90;
    public Text scoreText;
    public Text winAlert;
    public Text countDownAlert;
    public Text BigCoinAlert;
    private int score;
    float countDown = 10;
    public float chargeRate = 5;
    float charge = 0;
    int randScoreAdder;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        winAlert.text = "";
        countDownAlert.text = "";
        BigCoinAlert.text = "";
        SetScoreText();
        randScoreAdder = Random.Range(2, 5);

        //Getting the camera in a good spot to view the plane and the gameplay 
        Vector3 camPos = transform.position - transform.forward * 8 + Vector3.up * 5;
        Camera.main.transform.position = camPos;
        Camera.main.transform.LookAt(transform.position + transform.forward*5);
    }

    // Update is called once per frame
    void Update()
    {
        //Recording the axes 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Rotate on y-axis and move forward
        //I didn't make the main camera a child of the plane because of that
        transform.Rotate(0, rotateSpeed * Time.deltaTime * moveHorizontal, 0);
        transform.position += transform.forward * speed * Time.deltaTime * moveVertical;

        countDown -= Time.deltaTime; 
        if (countDown <= 0)
        {
            countDownAlert.text = "Hurry up!";
        }

        if (Input.GetKey(KeyCode.Space))
        {
            charge += chargeRate * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.position*charge);
        }

        //Finding another game object(s) ***doesn't do anything 
        //GameObject gameObj = GameObject.Find("Rings/CHAH-COIN/default");
    }

    public void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score>=(9+randScoreAdder))
        {
            //Must add the .text for it to be correct 
            winAlert.text = "You're rich now!";
        }
    }

    //Built-in, called when a game object collides with another 
    private void OnTriggerEnter(Collider other)
    {
        //Finding distance between two vectors and normalizing it, ***doesn't do anything 
        //Vector3 direction = transform.position - other.gameObject.transform.position;
        //direction.Normalize();
        
        //Each Ring must be a trigger 
        //Each Ring must have a tag that is the same as that in the quotations
        //To create a tag, their must be a prefab 
        //NOTE: Create first prefab, then copy and paste 
        if (other.gameObject.CompareTag("Ring"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("BigCoin"))
        {
            other.gameObject.SetActive(false);
            score += randScoreAdder; //or Random.value()
            BigCoinAlert.text = "Big Coin, Big Points!";
            SetScoreText();
        }
    }
}
