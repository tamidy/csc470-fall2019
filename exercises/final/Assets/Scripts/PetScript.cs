﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/* References: 
 * https://answers.unity.com/questions/1427984/how-to-make-object-move-from-one-random-point-to-a.html
*/

public class PetScript : MonoBehaviour {

    public GameManager gm; //GameManager variable
    public GameObject petObject;  //Getting the pet as an object 
    public GameObject groundObject; //Getting the ground as an object 

    public GameObject foodPrefab; //Food variable for when the player feeds the pet
    bool feeding = false; //Boolean to determine if the player is feeding the pet or not 

    //Emotional levels in the form of integers 
    public float happiness = 0;
    public float sadness = 0;
    public float sleepiness = 0;
    public float boredom = 0;
    public float hunger = 0;
    private float timer = 0;
    public int hungerInt = 0;
    private float timerInt = 0;

    //Quotes from the pet 
    public GameObject statusBox;
    public Text statusAlert;
    public Text continueText;

    //Texts to display the pet's current state 
    public Text happinessText;
    public Text sadnessText;
    public Text sleepinessText;
    public Text boredomText;
    public Text hungerText;

    //Buttons for the actions the player can take
    public Button btnFeed;
    public Button btnPet;
    public Button btnCradle;
    public Button btnIgnore;
    public Button btnPlayWith;

    //Meters 
    public Image hungerMeterFG;
    public Image happinessMeterFG;
    public Image sadnessMeterFG;
    public Image sleepinessMeterFG;
    public Image boredomMeterFG;

    //Variables for the pet's movement
    public static int movespeed = 5;
    public Vector3 userDirection = Vector3.forward;
    private float timeOfMovement = 3.0f;
    private float waitTimeMovement = 3.0f;
    private bool hasArrived = false;
    public Animator animator;

    //Levels 
    private bool level1 = true;
    private bool level2 = false;
    private bool level3 = false;
    private int level1Counter = 0;
    private int level2Counter = 0;
    private int level3Counter = 0;
    private bool levelupbool = false;
    public GameObject levelupText;
    private bool win = false;
    public GameObject trophy1;
    public GameObject trophy2;
    public GameObject trophy3;

    void Start() {

        //Initializing the status variables 
        happiness = 50;
        sadness = 50;
        hunger = 100;
        sleepiness = 100;
        boredom = 100;

        //Initializing the texts 
        statusAlert.text = "";
        continueText.text = "(Touch Screen to Continue)";
        happinessText.text = "Happiness: ";
        sadnessText.text = "Sadness: ";
        sleepinessText.text = "Sleepiness: ";
        boredomText.text = "Boredom: ";
        hungerText.text = "Hunger: ";

        //Initializing the buttons 
        Button feed = btnFeed.GetComponent<Button>();
        Button pet = btnPet.GetComponent<Button>();
        Button cradle = btnCradle.GetComponent<Button>();
        Button ignore = btnIgnore.GetComponent<Button>();
        Button playWith = btnPlayWith.GetComponent<Button>();

        //The states will be changed with user input (button clicks)
        feed.onClick.AddListener(resetHunger);
        pet.onClick.AddListener(UpdateBoredom);
        cradle.onClick.AddListener(UpdateSleepiness);
        ignore.onClick.AddListener(UpdateSadness);
        playWith.onClick.AddListener(UpdateHappiness);

        //Camera movement
        Vector3 camPos = transform.position + transform.forward * 55 + Vector3.up * 20;
        Camera.main.transform.position = camPos + transform.right * -15;
    }

    void Update() {
        timer += Time.deltaTime; //Updating the timer 
        hunger -= Time.deltaTime; //Gets more hungry as time goes on 

        //Converting to integer variables 
        timerInt = Mathf.RoundToInt(timer); //Updating the timer 
        hungerInt = Mathf.RoundToInt(hunger); //Increasing hunger as time goes on 

        //Status alert update and keep the state at its highest value 
        checkRanges();

        //Updating the meters
        hunger = Mathf.Clamp(hunger, 0, 100);
        happiness = Mathf.Clamp(happiness, 0, 100);
        sadness = Mathf.Clamp(sadness, 0, 100);
        sleepiness = Mathf.Clamp(sleepiness, 0, 100);
        boredom = Mathf.Clamp(boredom, 0, 100);

        hungerMeterFG.fillAmount = hunger/100;
        happinessMeterFG.fillAmount = happiness/100;
        sadnessMeterFG.fillAmount = sadness/100;
        sleepinessMeterFG.fillAmount = sleepiness/100.0f;
        boredomMeterFG.fillAmount = boredom/100;

        //Updating texts on the meters 
        happinessText.text = "Happiness: " + happiness.ToString();
        sadnessText.text = "Sadness: " + sadness.ToString();
        sleepinessText.text = "Sleepiness: " + sleepiness.ToString();
        boredomText.text = "Boredom: " + boredom.ToString();
        hungerText.text = "Hunger: " + hungerInt.ToString();

        if (feeding) {
            //Food shows up at a random spot on the plane
            Vector3 pos = transform.position * Random.Range(-20.0f, 20.0f) + Vector3.up * 1.65f + transform.forward * Random.Range(-20.0f, 20.0f);
            GameObject food = Instantiate(foodPrefab, pos, transform.rotation);
            feeding = false;
        }

        //Making the pet move on its own
        if (!hasArrived) {
            movingPet();
        } 

        //Camera Movement, moves with the pet, but doesn't rotate with it 
        Camera.main.transform.LookAt(transform);

    } //end of Update()

    //Functions to update each of the states, they are all correlated to one another 
    void UpdateHappiness() {
        happiness += 1;
        sadness -= 1;
        sleepiness += 2;
        boredom += 2;

        //Status alert update and keep the state at its highest and lowest value 
        checkRanges();
    }

    void resetHunger() {

        //When the pet is more sad than hungry, then the sadness increases and happiness decreases as well 
        if (hunger < sadness) {
            sadness += 1;
            happiness -= 1;
        }
        hunger = 100;
        feeding = true;
    }

    void UpdateSadness() {
        happiness -= 1;
        sadness += 1;

        //When the pet is more hungry than sad, then the hunger increases as well 
        if (sadness < hunger) {
            hunger -= 1;
        }
        //When the pet is more bored than sad, then the boredom increases as well 
        if (sadness < boredom) {
            boredom -= 1; 
        }
        //When the pet is more sleepy than sad, then the sleepiness increases as well 
        if (sadness < sleepiness) {
            sleepiness -= 1;
        }
        //Status alert update and keep the state at its highest and lowest value 
        checkRanges();
    }

    void UpdateSleepiness() {
        sleepiness -= 1;

        //When the pet is less sleepy than happy, then the happiness decreases
        if (sleepiness < happiness) {
            happiness -= 1;
        }
        //When the pet is more sad than sleepy, then the sadness decreases as well 
        if (sleepiness < sadness) {
            sadness -= 1;
        }
        //Status alert update and keep the state at its highest value 
        checkRanges();
    }

    void UpdateBoredom() {
        boredom -= 1;


        //When the pet is less bored than happy, then the happiness decreases
        if (boredom < happiness) {
            happiness -= 1;
        }
        //When the pet is more hungry than bored, then the hunger increases as well 
        if (boredom < hunger) {
            hunger -= 1;
        }
        //When the pet is more sleepy than bored, then the sleepiness increases as well 
        if (boredom < sleepiness) {
            sleepiness -= 1;
        }
        //When the pet is more sad than bored, then the sadness increases as well 
        if (boredom < sadness) {
            sadness -= 1;
        }
        //Status alert update and keep the state at its highest value 
        checkRanges();
    }

    //Function to check the range of emotions, keep it 0-100
    void checkRanges() {
        if (hunger <= 0) {
            statusAlert.text = "FEED ME!";
            speak();
            hunger = 0;
        }

        if (happiness <= 0) {
            happiness = 0;
        } else if (happiness >= 100) {
            happiness = 50;
            statusAlert.text = "I love you!";
            speak();
            checkLevel();
        }

        if (sadness <= 0) {
            happiness = 0;
        } else if (sadness >= 100) {
            sadness = 100;
            statusAlert.text = "BOOHOO! I'M SAD!";
            speak();
        }

        if (sleepiness <= 0) {
            sleepiness = 0;
            statusAlert.text = "Zzzzz...";
            speak();
        } else if (sleepiness >= 100) {
            sleepiness = 100;
        }

        if (boredom <= 0) {
            boredom = 0;
            statusAlert.text = "Play With Me!";
            speak();
        } else if (boredom >= 100) {
            boredom = 100;
        }
    }

    //Function and Coroutine to move the pet randomly throughout the plane
    void movingPet() {
        hasArrived = true;
        //animator.SetBool("Walking", false); 
        float randX = Random.Range(groundObject.transform.position.x - 10, groundObject.transform.position.x + 10);
        float randZ = Random.Range(groundObject.transform.position.z - 10, groundObject.transform.position.z + 10);
        StartCoroutine(MoveToPoint(new Vector3(randX, petObject.transform.position.y, randZ)));
    }

    private IEnumerator MoveToPoint(Vector3 targetPosition) {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < timeOfMovement) {
            timer += Time.deltaTime;

            //Changing position 
            transform.position = Vector3.Lerp(startPos, targetPosition, timer);

            //Rotation
            Vector3 vecToDest = (targetPosition - transform.position).normalized;
            float step = 10 * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, vecToDest, step, 1);
            transform.rotation = Quaternion.LookRotation(targetPosition);
            yield return null;
        }
        hasArrived = false;
        animator.SetBool("Walking", true);
        yield return new WaitForSeconds(waitTimeMovement);
    }

    //Function and Coroutine to updating the status box and displaying it when and where appropriate 
    void speak() {

        //Compute the screen position 2 units above the unit and place the talkBox
        Vector3 pos = petObject.transform.position + Vector3.up * 20;
        pos = Camera.main.WorldToScreenPoint(pos);
        statusBox.transform.position = pos;

        StartCoroutine(displayTalkBoxMessage(statusAlert.text));
    }

    private IEnumerator displayTalkBoxMessage(string t) {

        //Showing the status box
        statusBox.SetActive(true);

        //Wait for the mouse to be pressed on the screen
        while (!Input.GetMouseButtonDown(0)) {
            //Tell the coroutine system that we are done for this update cycle.
            yield return null;
        }

        //If we get here, it means that the mouse was just pressed
        //Tell the coroutine system that we are done for this update cycle
        statusBox.SetActive(false);
        continueText.GetComponent<Text>().enabled = false;
    } 

    void checkLevel() {
        if (level1) {
            level1Counter++;
            if (level1Counter>=3) {
                levelupbool = true;
                StartCoroutine(levelUp());
                levelupText.SetActive(false);
                level1 = false;
                level2 = true;
                trophy1.SetActive(true);
            }
        }
        if (level2) {
            level2Counter++;
            if (level2Counter >= 3) {
                levelupbool = true;
                StartCoroutine(levelUp());
                levelupText.SetActive(false);
                level2 = false;
                level3 = true;
                trophy2.SetActive(true);
            }
        }
        if (level3) {
            level3Counter++;
            if (level1Counter >= 3) {
                levelupbool = true;
                StartCoroutine(levelUp());
                levelupText.SetActive(false);
                level3 = false;
                win = true;
                trophy3.SetActive(true);
            }
        }
        if (win) {
            SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
        }
    }

    private IEnumerator levelUp() {
        levelupText.SetActive(true);
        yield return new WaitForSeconds(5);
    }
}