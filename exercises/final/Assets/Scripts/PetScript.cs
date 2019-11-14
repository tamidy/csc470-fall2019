using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetScript : MonoBehaviour {

    //Emotional levels in the form of integers 
    public int happiness = 0;
    public int sadness = 0;
    public int sleepiness = 0;
    public int boredom = 0;
    public float hunger = 0;
    private float timer = 0;
    public int hungerInt = 0;
    private float timerInt = 0;

    //Texts to display the pet's current state 
    public Text statusAlert;
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

    public GameObject foodPrefab; //Carrot variable for when the player feeds the rabbit
    public GameObject petObject;  //Getting the pet as an object 
    bool feeding = false; //Boolean to determine if the player is feeding the pet or not 

    // Start is called before the first frame update
    void Start() {
       
        //Initializing the status variables 
        happiness = 50;
        sadness = 50;
        hunger = 0;
        sleepiness = 0;
        boredom = 0;

        //Initializing the texts 
        statusAlert.text = "";
        happinessText.text = "Happiness: ";
        sadnessText.text = "Sadness: ";
        sleepinessText.text = "Sleepiness: ";
        boredomText.text = "Boredom: ";
        hungerText.text = "Hunger: ";

        Button feed = btnFeed.GetComponent<Button>();
        Button pet = btnPet.GetComponent<Button>();
        Button cradle = btnCradle.GetComponent<Button>();
        Button ignore = btnIgnore.GetComponent<Button>();
        Button playWith = btnPlayWith.GetComponent<Button>();

        feed.onClick.AddListener(resetHunger);
        pet.onClick.AddListener(UpdateBoredom);
        cradle.onClick.AddListener(UpdateSleepiness);
        ignore.onClick.AddListener(UpdateSadness);
        playWith.onClick.AddListener(UpdateHappiness);
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime; //Updating the timer 
        hunger += Time.deltaTime; //Increasing hunger as time goes on 

        //Converting to integer variables 
        timerInt = Mathf.RoundToInt(timer); //Updating the timer 
        hungerInt = Mathf.RoundToInt(hunger); //Increasing hunger as time goes on 

        if (hunger>=100) {
            statusAlert.text = "FEED ME!";
        }

        //Updating the meters 
        hungerMeterFG.fillAmount = 5 - Mathf.Max(0, (5 - Time.time) / 0.5f);
        happinessMeterFG.fillAmount = 5 - Mathf.Max(0, (5 - Time.time) / 0.5f);
        sadnessMeterFG.fillAmount = 5 - Mathf.Max(0, (5 - Time.time) / 0.5f);
        sleepinessMeterFG.fillAmount = 5 - Mathf.Max(0, (5 - Time.time) / 0.5f);
        boredomMeterFG.fillAmount = 5 - Mathf.Max(0, (5 - Time.time) / 0.5f);

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

    void UpdateHappiness() {
        happiness += 1;
        sadness -= 1;

        if (happiness>=100) {
            statusAlert.text = "I love you!";
        }
    }

    void resetHunger() {
        hunger = 0;
        feeding = true;
    }

    void UpdateSadness() {
        happiness -= 1;
        sadness += 1;

        if (sadness>=100) {
            statusAlert.text = "BOOHOO! I'M SAD!";
        }
    }

    void UpdateSleepiness() {
        sleepiness += 1;

        if (sleepiness>=100) {
            statusAlert.text = "Zzzzz...";
        }
    }

    void UpdateBoredom() {
        boredom += 1;
        
        if (boredom>=100) {
            statusAlert.text = "Play With Me!";
        }
    }
}
