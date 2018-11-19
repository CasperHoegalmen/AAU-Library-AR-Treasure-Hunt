using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBubble : MonoBehaviour {

    public Text talking; //Text object for bibliothecary
    public Text talking2; //Text object for other characters
    public GameObject speechBubble1; //Rawimage object
    public GameObject speechBubble2; //Rawimage object
    

    public struct interaction { //Struct with all info for each interaction
        public bool current;
        public bool finished;
        public string[] talk; // What does each bubble say
        public int bubbleCount; //How many bubbles are in this interaction
        public int bubbleOwner; //Who does the interaction belong to?
    }

    public interaction intro; 
    public interaction introContinued;
    public interaction[] arkade = new interaction[7];
    public interaction[] arkade2 = new interaction[2];
    public interaction[] crime = new interaction[5];
    public interaction[] crime2 = new interaction[3];
    public interaction[] zone = new interaction[6];
    public interaction[] zone2 = new interaction[2];
    public interaction ending;

    interaction arkadeHelp;
    interaction crimeHelp;
    interaction zoneHelp;

    int pressCount = 0; //Number of times button has been pressed in current interaction
    public Button button; //Button for progressing interaction

	// Use this for initialization
	void Start () { 
        interactionSetup(); //initialize text into different interactions
        arkade[0].current = true;
        speechBubble1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        stopDisplayText(arkade);
        
        displayText(intro);
        displayText(introContinued);
        displayText(arkade);
       /* displayText(arkade2);
        displayText(crime);
        displayText(crime2);
        displayText(zone);
        displayText(zone2);
        displayText(arkadeHelp);
        displayText(crimeHelp);
        displayText(zoneHelp);
        displayText(ending); */


        /* if (GameObject.Find("ImageTarget").GetComponent<DefaultTrackableEventHandler>().oneFound == true)
        {
            currentInter = intro;
            speechBubble1.SetActive(true);
        }
        */


        /*if (GameObject.Find("ImageTarget").GetComponent<DefaultTrackableEventHandler>().oneFound == true)
        {
            speechBubble1.SetActive(true);
        }

        if (GameObject.Find("ImageTarget2").GetComponent<DefaultEventHandler2>().twoFound == true)
        {
            speechBubble1.SetActive(true);
        }
        */
    }

    public void displayText(interaction[] interArray) //Display text in corresponding text bubble.
    {
        for(int i = 0; i < interArray.Length; i++)
        {
            if(interArray[i].current == true)
            {
                if (interArray[i].bubbleOwner == 0)
                {
                    speechBubble1.SetActive(true);
                    talking.text = interArray[i].talk[pressCount];
                }
                else
                    speechBubble2.SetActive(true);
                    talking2.text = interArray[i].talk[pressCount];
            }
           
        }
       
    }

    public void displayText(interaction inter) //Display text overload for single interactions
    {
        if(inter.current == true)
        {
            if (inter.bubbleOwner == 0)
            {
                speechBubble1.SetActive(true);
                talking.text = inter.talk[pressCount];
            }
            else
                speechBubble2.SetActive(true);
                talking2.text = inter.talk[pressCount];
        }
    }

    public void stopDisplayText(interaction[] interArray)
    {
        for (int i = 0; i < interArray.Length; i++)
        {
            if(interArray[i].current == true)
            {
                if (pressCount >= interArray[i].bubbleCount)
                {
                    interArray[i].current = false;
                    pressCount = 0;
                    speechBubble1.SetActive(false);
                    speechBubble2.SetActive(false);
                    if (interArray.Length > i)
                    {
                        interArray[i + 1].current = true;
                    }
                }
            }

        }
    }

    public void interactionSetup()
    {

        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);

        intro.bubbleCount = 10;
        intro.talk = new string[10];
        intro.talk[0] = "Jamen halli-hallo";
        intro.talk[1] = "Mit navn er Bib";
        intro.talk[2] = "Bibliotekar";
        intro.talk[3] = "Kan du se den kiste bag mig?";
        intro.talk[4] = "I den ligger min mest værdifulde ejendel";
        intro.talk[5] = "Hvad det nu end er...";
        intro.talk[6] = "Det kan jeg ikke huske.";
        intro.talk[7] = "Men for at åbne kisten skal vi bruge en nøgle.";
        intro.talk[8] = "Den kan jeg heller ikke huske hvad jeg har gjort ved.";
        intro.talk[9] = "Hvis du har lyst til at hjælpe, så sigt dit kamera op mod den blå trylledrik";

        introContinued.bubbleCount = 5;
        introContinued.talk = new string[5];
        introContinued.talk[0] = "Fantastisk!";
        introContinued.talk[1] = "Disse trylledrikke hjælper mig med at huske";
        introContinued.talk[2] = "Jeg kan nu huske at jeg delte nøglen op og gav delene til nogle venner";
        introContinued.talk[3] = "Jeg kan til gengæld ikke huske hvem.";
        introContinued.talk[4] = "Lad os finde en trylledrik mere";

        arkade[0].bubbleCount = 3;
        arkade[0].talk = new string[3];
        arkade[0].talk[0] = "Der var den!";
        arkade[0].talk[1] = "Jeg gav en af nøgledelene til min ven Pixel";
        arkade[0].talk[2] = "Vi kan finde ham nede i arkaden.";

        arkade[1].bubbleOwner = 1;
        arkade[1].bubbleCount = 3;
        arkade[1].talk = new string[3];
        arkade[1].talk[0] = "Jamen hvem kommer her til min bule?";
        arkade[1].talk[1] = "Bib og en kammerat?";
        arkade[1].talk[2] = "Har i lyst til et spil Flight Commander?";

        arkade[2].bubbleCount = 4;
        arkade[2].talk = new string[4];
        arkade[2].talk[0] = "Pixel min dreng!";
        arkade[2].talk[1] = "Nej, ikke idag.";
        arkade[2].talk[2] = "Vi er ude at lede efter nøgledelene.";
        arkade[2].talk[3] = "Har du stadig den jeg gav til dig?";

        arkade[3].bubbleOwner = 1;
        arkade[3].bubbleCount = 4;
        arkade[3].talk = new string[4];
        arkade[3].talk[0] = "Selvfølgelig.";
        arkade[3].talk[1] = "Du gav mig den lige efter Pac-man turneringen.";
        arkade[3].talk[2] = "Vi vandt jo, Bib.";
        arkade[3].talk[3] = "Kan du ikke huske det?";

        arkade[4].bubbleCount = 1;
        arkade[4].talk = new string[1];
        arkade[4].talk[0] = "Alt jeg kan huske er at damerne var ellevilde.";

        arkade[5].bubbleOwner = 1;
        arkade[5].bubbleCount = 3;
        arkade[5].talk = new string[3];
        arkade[5].talk[0] = "Ja og jeg skal nok give dig den...";
        arkade[5].talk[1] = "Efter du beviser at du stadig er en spiller.";
        arkade[5].talk[2] = "Den bib jeg kender ved nemlig hvor mange spøgelser der er i Pac-man";

        arkade[6].bubbleCount = 4;
        arkade[6].talk = new string[4];
        arkade[6].talk[0] = "...hehe...";
        arkade[6].talk[1] = "Ja.";
        arkade[6].talk[2] = "Selvfølgelig.";
        arkade[6].talk[3] = "Psst... Hjælp mig..";

        arkade2[0].bubbleOwner = 1;
        arkade2[0].bubbleCount = 2;
        arkade2[0].talk = new string[2];
        arkade2[0].talk[0] = "Jeg vidste du stadig var en spiller!";
        arkade2[0].talk[1] = "Her er nøgledelen.";

        arkade2[1].bubbleCount = 4;
        arkade2[1].talk = new string[4];
        arkade2[1].talk[0] = "Fantastisk!";
        arkade2[1].talk[1] = "Lad os hoppe tilbage den vej vi kom fra.";
        arkade2[1].talk[2] = "Måske vi finder en trylledrik på vejen som kan lede os videre.";
        arkade2[1].talk[3] = "Tusind tak pixel!";

        /*
        crime[0].talk[0] = "Om det ikke minsandten er min ven Bib og hvem har du med der?";
        crime[0].talk[1] = "Hvad kan jeg hjælpe jer med?";

        crime[1].talk[0] = "En hjælper.";
        crime[1].talk[1] = "Vi leder efter nøgledelene.";
        crime[1].talk[2] = "Har du en?";

        crime[2].talk[0] = "Jeg havde en tidligere.";
        crime[2].talk[1] = "Indtil en af disse mistænkte stjal den!";
        crime[2].talk[2] = "Jeg kan til gengæld ikke finde ham.";
        crime[2].talk[3] = "Vil i hjælpe mig med at finde ud hvor han er?";
        crime[2].talk[4] = "Tjah der er vel ikke andet at gøre.";
        crime[2].talk[5] = "Udemærket";
        crime[2].talk[6] = "Jeg ved følgende om den mistænkte.";
        crime[2].talk[7] = "Mistænkte kan lide at holde sin hånd på maven.";
        crime[2].talk[8] = "Mistænkte kigger opad.";
        crime[2].talk[9] = "Mistænkte har orange sko på.";

        crime2[0].talk[0] = "Jeg vidste det var ham!";
        crime2[0].talk[1] = "Tusind tak de herrer.";
        crime2[0].talk[2] = "Her er nøgledelen.";
        */
    }

    void Awake()
    {
        Button btn = button.GetComponent<Button>(); //or just drag-n-drop the button in the CustomButton field
        btn.onClick.AddListener(button_onClick);  //subscribe to the onClick event
    }

    //Handle the onClick event
    void button_onClick()
    {
        pressCount++;
    }
}
