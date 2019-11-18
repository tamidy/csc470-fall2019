using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PetScript : MonoBehaviour {

    public GameObject foodPrefab; //Food variable for when the player feeds the pet
    public GameObject petObject;  //Getting the pet as an object 
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

    // Start is called before the first frame update
    void Start() {
       
        //Initializing the status variables 
        happiness = 50;
        sadness = 50;
        hunger = 100;
        sleepiness = 100;
        boredom = 100;

        //Initializing the texts 
        statusAlert.text = "";
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
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime; //Updating the timer 
        hunger -= Time.deltaTime; //Gets more hungry as time goes on 

        //Converting to integer variables 
        timerInt = Mathf.RoundToInt(timer); //Updating the timer 
        hungerInt = Mathf.RoundToInt(hunger); //Increasing hunger as time goes on 

        //Status alert update and keep the state at its highest value 
        if (hunger>=100) {
            statusAlert.text = "FEED ME!";
            speak();
            hunger = 100;
        }

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
    }

    //Functions to update each of the states, they are all correlated to one another 
    void UpdateHappiness() {
        happiness += 1;
        sadness -= 1;

        //Status alert update and keep the state at its highest and lowest value 
        if (happiness>=100) {
            statusAlert.text = "I love you!";
            speak();
            happiness = 100;
            sadness = 0;
        }
    }

    void resetHunger() {

        //When the pet is more sad than hungry, then the sadness increases as well 
        if (hunger < sadness) {
            sadness += 1;
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
        if (sadness>=100) {
            statusAlert.text = "BOOHOO! I'M SAD!";
            speak();
            sadness = 100;
            happiness = 0;
        }
    }

    void UpdateSleepiness() {
        sleepiness -= 1;

        //When the pet is more sad than sleepy, then the sadness increases as well 
        if (sleepiness < sadness) {
            sadness -= 1;
        }

        //Status alert update and keep the state at its highest value 
        if (sleepiness>=100) {
            statusAlert.text = "Zzzzz...";
            speak();
            sleepiness = 100;
        }
    }

    void UpdateBoredom() {
        boredom -= 1;

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
        if (boredom>=100) {
            statusAlert.text = "Play With Me!";
            speak();
            boredom = 100;
        }
    }

    //FIXME: status text 
    //Updating the status box and displaying it when and where appropriate 
    void speak() {

        // Compute the screen position 2 units above the unit and place the talkBox.
        Vector3 pos = petObject.transform.position + Vector3.up * 2;
        pos = Camera.main.WorldToScreenPoint(pos);
        statusBox.transform.position = pos;

        StartCoroutine(displayTalkBoxMessages(statusAlert.text));
    }

    IEnumerator displayTalkBoxMessages(string t) {

        statusBox.SetActive(true);
        // Wait for the mouse to be pressed
        while (!Input.GetMouseButtonDown(0)) {
            // Tell the coroutine system that we are done for this update cycle.
            yield return null;
        }

        // If we get here, it means that the mouse was just pressed. Tell the coroutine
        // system that we are done for this update cycle.
        yield return null;
        
        statusBox.SetActive(false);
    }
}