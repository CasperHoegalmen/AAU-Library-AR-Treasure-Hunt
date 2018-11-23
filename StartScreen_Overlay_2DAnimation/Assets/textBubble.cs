using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBubble : MonoBehaviour
{

    public Text talking;                //Text object for bibliothecary
    public Text talking2;               //Text object for other characters
    public GameObject speechBubble1;    //Rawimage object
    public GameObject speechBubble2;    //Rawimage object
    public GameObject companion, happyCompanion, arcade_Machine, gingerbread, gingerbread_NoButton, detective;
    public RawImage arcadeImage, GBImage, GB_NoButtonImage, detectiveImage;

    //Struct with all info for each interaction
    public struct interaction
    {
        public bool current;            //Is this the current interaction? Functions will largely only run on object if this is set to TRUE.
        public string[] talk;           // What does each bubble say
        public int[] animNum;           //Corresponding animation to the talk (array length should always be equal to talk.length)
        public bool[] animFin;          //Has the animation already been played? If FALSE: No, if TRUE: Yes. Animations will only play once.
        public int bubbleCount;         //How many bubbles are in this interaction
        public int bubbleOwner;         //Who is speaking in this interaction? 0 = Companion.
    }

    public interaction intro;                                   //Starting talk
    public interaction introContinued;                          //Starting talk after first object scan
    public interaction[] arkade = new interaction[7];           //Arkade Grandmaster intro talk.
    public interaction[] arkade2 = new interaction[2];          //Arkade Grandmaster post-question talk
    public interaction[] crime = new interaction[6];            //Crime Grandmaster intro talk.
    public interaction[] crime2 = new interaction[2];           //Crime Grandmaster post-question talk
    public interaction[] zone = new interaction[7];             //Zone Grandmaster intro talk.
    public interaction[] zone2 = new interaction[2];            //Zone2 Grandmaster post-question talk
    public interaction outro;

    public interaction arkadeHelp;                              //Arkade help poster talk
    public interaction crimeHelp;                               //Crime help poster talk
    public interaction zoneHelp;                                //Zone help poster talk

    public int pressCount = 0;                                         //Number of times button has been pressed in current interaction
    public Button button;                                       //Button for progressing interaction


    // Use this for initialization
    void Start()
    {
        interactionSetup();                                     //Set up variables into different interactions
        intro.current = true;

        //speechBubble1.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        allStopDisplay();
        allDisplayText();
        playCompAnimation(intro);
        playCompAnimation(introContinued);
        playCompAnimation(arkade);
        playCompAnimation(arkade2);
        zoneSectionAnimation(zone);
        zoneSectionAnimation(zone2);
        crimeSectionAnimation(crime);
        crimeSectionAnimation(crime2);
        gameOver(outro);
    }


    //Run displayText on each interaction in the program.
    public void allDisplayText()
    {
        displayText(intro);
        displayText(introContinued);
        displayText(arkade);
        displayText(arkade2);
        displayText(crime);
        displayText(crime2);
        displayText(zone);
        displayText(zone2);
        //displayText(arkadeHelp);
        //displayText(crimeHelp);
        //displayText(zoneHelp);
        displayText(outro);
    }


    //DisplayText overload for single interactions
    public void displayText(interaction inter)
    {
        if (inter.current == true)
        {
            if (inter.bubbleOwner == 0)
            {
                speechBubble1.SetActive(true);
                talking.text = inter.talk[pressCount];
                Debug.Log(pressCount);
            }
            else
                speechBubble2.SetActive(true);
            talking2.text = inter.talk[pressCount];
        }
    }


    //Display text in the owner's corresponding textbubble   
    public void displayText(interaction[] interArray)
    {
        for (int i = 0; i < interArray.Length; i++)
        {
            if (interArray[i].current == true)
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


    //Check if pressCount is above the number of speechbubbles in the interaction and if 'TRUE' set that interaction.current to false and both speechbubbles to inactive.
    public bool stopDisplayText(interaction inter)
    {
        if (pressCount >= inter.bubbleCount && inter.current == true)
        {
            pressCount = 0;
            speechBubble1.SetActive(false);
            speechBubble2.SetActive(false);

            return false;
        }
        return true;
    }


    //Run stopDisplayText on all interactions whose current is equals to TRUE.
    public void allStopDisplay()
    {
        if (intro.current == true)
        {
            intro.current = stopDisplayText(intro);
            //if (intro.current == false)
            //{
            //    introContinued.current = true;
            //};
        }

        if (introContinued.current == true)
        {
            introContinued.current = stopDisplayText(introContinued);
            //if (introContinued.current == false)
            //{
            //    arkade[0].current = true;
            //}
        }

        if (outro.current == true)
        {
            outro.current = stopDisplayText(outro);
        }

        //if (arkadeHelp.current == true)
        //{
        //    arkadeHelp.current = stopDisplayText(arkadeHelp);
        //}

        //if (crimeHelp.current == true)
        //{
        //    crimeHelp.current = stopDisplayText(crimeHelp);
        //}

        //if(zoneHelp.current == true)
        //{
        //    zoneHelp.current = stopDisplayText(zoneHelp);
        //}


        for (int i = 0; i < arkade.Length; i++)
        {
            if (arkade[i].current == true)
            {
                arkade[i].current = stopDisplayText(arkade[i]);

                if (i == 6 && arkade[6].current == false)
                {
                    //arkade2[0].current = true;
                    break;
                }

                if (arkade.Length > i && arkade[i].current == false)
                {
                    //Debug.Log("The number i is........................... " + i + " and its value is " + arkade[i + 1].current);
                    arkade[i + 1].current = true;
                }
            }
        }

        for (int i = 0; i < arkade2.Length; i++)
        {
            if (arkade2[i].current == true)
            {
                arkade2[i].current = stopDisplayText(arkade2[i]);

                if (i == 1 && arkade2[1].current == false)
                {
                    //zone[0].current = true;
                    break;
                }

                if (arkade2.Length > i && arkade2[i].current == false)
                {
                    arkade2[i + 1].current = true;
                }
            }
        }

        for (int i = 0; i < zone.Length; i++)
        {
            if (zone[i].current == true)
            {
                zone[i].current = stopDisplayText(zone[i]);

                if (i == 6 && zone[6].current == false)
                {
                    //zone2[0].current = true;
                    break;
                }

                if (zone.Length > i && zone[i].current == false)
                {
                    zone[i + 1].current = true;
                }
            }
        }

        for (int i = 0; i < zone2.Length; i++)
        {
            if (zone2[i].current == true)
            {
                zone2[i].current = stopDisplayText(zone2[i]);

                if (i == 1 && zone2[1].current == false)
                {
                    //crime[0].current = true;
                    break;
                }

                if (zone2.Length > i && zone2[i].current == false)
                {
                    zone2[i + 1].current = true;
                }
            }
        }

        for (int i = 0; i < crime.Length; i++)
        {
            if (crime[i].current == true)
            {
                crime[i].current = stopDisplayText(crime[i]);

                if (i == 5 && crime[5].current == false)
                {
                    //crime2[0].current = true;
                    break;
                }

                if (crime.Length > i && crime[i].current == false)
                {
                    crime[i + 1].current = true;
                }
            }
        }

        for (int i = 0; i < crime2.Length; i++)
        {
            if (crime2[i].current == true)
            {
                crime2[i].current = stopDisplayText(crime2[i]);

                if (i == 1 && crime2[1].current == false)
                {
                    //outro.current = true;
                    break;
                }

                if (crime2.Length > i && crime2[i].current == false)
                {
                    crime2[i + 1].current = true;
                }
            }
        }
    }

    //Function for inputting each interaction struct with its relevant information. All struct set-up should happen here.
    public void interactionSetup()
    {
        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);

        //arkadeHelp.bubbleCount = 3;
        //arkadeHelp.talk = new string[3];
        //arkadeHelp.talk[0] = "Der var den!";
        //arkadeHelp.talk[1] = "Jeg gav en af nøgledelene til min ven pixel";
        //arkadeHelp.talk[2] = "Vi kan finde ham nede i Arkaden";

        //crimeHelp.bubbleCount = 3;
        //crimeHelp.talk = new string[3];
        //crimeHelp.talk[0] = "Jamen det er jo elementært.";
        //crimeHelp.talk[1] = "Jeg har selvfølgelig givet en nøgledel til Sherlock.";
        //crimeHelp.talk[2] = "Til Gerningsstedet!";

        //zoneHelp.bubbleCount = 4;
        //zoneHelp.talk = new string[4];
        //zoneHelp.talk[0] = "Øøøh.";
        //zoneHelp.talk[1] = "Jeg kan huske jeg gav den til nogen i zonen.";
        //zoneHelp.talk[2] = "Men jeg ved ikke hvem der er på skansen idag.";
        //zoneHelp.talk[3] = "Skal vi ikke checke det ud?";

        intro.bubbleCount = 10;
        intro.talk = new string[10];
        intro.animNum = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        intro.animFin = new bool[10];
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
        introContinued.animNum = new int[5] { 1, 1, 1, 1, 1 };
        introContinued.animFin = new bool[5];
        introContinued.talk[0] = "Fantastisk!";
        introContinued.talk[1] = "Disse trylledrikke hjælper mig med at huske";
        introContinued.talk[2] = "Jeg kan nu huske at jeg delte nøglen op og gav delene til nogle venner";
        introContinued.talk[3] = "Jeg kan til gengæld ikke huske hvem.";
        introContinued.talk[4] = "Lad os finde en trylledrik mere";

        arkade[0].bubbleCount = 3;
        arkade[0].talk = new string[3];
        arkade[0].animNum = new int[3] { 1, 1, 1 };
        arkade[0].animFin = new bool[3];
        arkade[0].talk[0] = "Der var den!";
        arkade[0].talk[1] = "Jeg gav en af nøgledelene til min ven Pixel";
        arkade[0].talk[2] = "Vi kan finde ham nede i arkaden.";

        arkade[1].bubbleOwner = 1;
        arkade[1].bubbleCount = 3;
        arkade[1].talk = new string[3];
        arkade[1].animNum = new int[3] { 1, 1, 1 };
        arkade[1].animFin = new bool[3];
        arkade[1].talk[0] = "Jamen hvem kommer her til min bule?";
        arkade[1].talk[1] = "Bib og en kammerat?";
        arkade[1].talk[2] = "Har i lyst til et spil Flight Commander?";

        arkade[2].bubbleCount = 4;
        arkade[2].talk = new string[4];
        arkade[2].animNum = new int[4] { 1, 1, 1, 1 };
        arkade[2].animFin = new bool[4];
        arkade[2].talk[0] = "Pixel min dreng!";
        arkade[2].talk[1] = "Nej, ikke idag.";
        arkade[2].talk[2] = "Vi er ude at lede efter nøgledelene.";
        arkade[2].talk[3] = "Har du stadig den jeg gav til dig?";

        arkade[3].bubbleOwner = 1;
        arkade[3].bubbleCount = 4;
        arkade[3].talk = new string[4];
        arkade[3].animNum = new int[4] { 1, 1, 1, 1 };
        arkade[3].animFin = new bool[4];
        arkade[3].talk[0] = "Selvfølgelig.";
        arkade[3].talk[1] = "Du gav mig den lige efter Pac-man turneringen.";
        arkade[3].talk[2] = "Vi vandt jo, Bib.";
        arkade[3].talk[3] = "Kan du ikke huske det?";

        arkade[4].bubbleCount = 1;
        arkade[4].talk = new string[1];
        arkade[4].animNum = new int[1] { 1 };
        arkade[4].animFin = new bool[1];
        arkade[4].talk[0] = "Alt jeg kan huske er at damerne var ellevilde.";

        arkade[5].bubbleOwner = 1;
        arkade[5].bubbleCount = 3;
        arkade[5].talk = new string[3];
        arkade[5].animNum = new int[3] { 1, 1, 1 };
        arkade[5].animFin = new bool[3];
        arkade[5].talk[0] = "Ja og jeg skal nok give dig den...";
        arkade[5].talk[1] = "Efter du beviser at du stadig er en spiller.";
        arkade[5].talk[2] = "Den bib jeg kender ved nemlig hvor mange spøgelser der er i Pac-man";

        arkade[6].bubbleCount = 4;
        arkade[6].talk = new string[4];
        arkade[6].animNum = new int[4] { 1, 1, 1, 1 };
        arkade[6].animFin = new bool[4];
        arkade[6].talk[0] = "...hehe...";
        arkade[6].talk[1] = "Ja.";
        arkade[6].talk[2] = "Selvfølgelig.";
        arkade[6].talk[3] = "Psst... Hjælp mig..";

        arkade2[0].bubbleOwner = 1;
        arkade2[0].bubbleCount = 2;
        arkade2[0].talk = new string[2];
        arkade2[0].animNum = new int[2] { 1, 1 };
        arkade2[0].animFin = new bool[2];
        arkade2[0].talk[0] = "Jeg vidste du stadig var en spiller!";
        arkade2[0].talk[1] = "Her er nøgledelen.";

        arkade2[1].bubbleCount = 4;
        arkade2[1].talk = new string[4];
        arkade2[1].animNum = new int[4] { 1, 1, 1, 1 };
        arkade2[1].animFin = new bool[4];
        arkade2[1].talk[0] = "Fantastisk!";
        arkade2[1].talk[1] = "Vi har fundet alle nøgledelene!";
        arkade2[1].talk[2] = "Lad os gå tilbage til kisten.";
        arkade2[1].talk[3] = "Det er tid til vores beløning";

        crime[0].bubbleCount = 3;
        crime[0].talk = new string[3];
        crime[0].animNum = new int[3] { 1, 1, 1 };
        crime[0].animFin = new bool[3];
        crime[0].talk[0] = "Jamen det er jo elementært.";
        crime[0].talk[1] = "Jeg har selvfølgelig givet en nøgledel til Sherlock.";
        crime[0].talk[2] = "Til Gerningsstedet!";

        crime[1].bubbleOwner = 1;
        crime[1].bubbleCount = 2;
        crime[1].talk = new string[2];
        crime[1].animNum = new int[2] { 1, 1 };
        crime[1].animFin = new bool[2];
        crime[1].talk[0] = "Om det ikke minsandten er min ven Bib og hvem har du med der?";
        crime[1].talk[1] = "Hvad kan jeg hjælpe jer med?";

        crime[2].bubbleCount = 3;
        crime[2].talk = new string[3];
        crime[2].animNum = new int[3] { 1, 1, 1 };
        crime[2].animFin = new bool[3];
        crime[2].talk[0] = "En hjælper.";
        crime[2].talk[1] = "Vi leder efter nøgledelene.";
        crime[2].talk[2] = "Har du en?";

        crime[3].bubbleOwner = 1;
        crime[3].bubbleCount = 4;
        crime[3].talk = new string[4];
        crime[3].animNum = new int[4] { 1, 1, 1, 1 };
        crime[3].animFin = new bool[4];
        crime[3].talk[0] = "Jeg havde en tidligere.";
        crime[3].talk[1] = "Indtil en af disse mistænkte stjal den!";
        crime[3].talk[2] = "Jeg kan til gengæld ikke finde ham.";
        crime[3].talk[3] = "Vil i hjælpe mig med at finde ud hvor han er?";

        crime[4].bubbleCount = 1;
        crime[4].talk = new string[1];
        crime[4].animNum = new int[1] { 1 };
        crime[4].animFin = new bool[1];
        crime[4].talk[0] = "Tjah der er vel ikke andet at gøre.";

        crime[5].bubbleOwner = 1;
        crime[5].bubbleCount = 5;
        crime[5].talk = new string[5];
        crime[5].animNum = new int[5] { 1, 1, 1, 1, 1 };
        crime[5].animFin = new bool[5];
        crime[5].talk[0] = "Udemærket";
        crime[5].talk[1] = "Jeg ved følgende om den mistænkte.";
        crime[5].talk[2] = "Mistænkte kan lide at holde sin hånd på maven.";
        crime[5].talk[3] = "Mistænkte kigger opad.";
        crime[5].talk[4] = "Mistænkte har orange sko på.";

        crime2[0].bubbleOwner = 1;
        crime2[0].bubbleCount = 3;
        crime2[0].talk = new string[3];
        crime2[0].animNum = new int[3] { 1, 1, 1 };
        crime2[0].animFin = new bool[3];
        crime2[0].talk[0] = "Jeg vidste det var ham!";
        crime2[0].talk[1] = "Tusind tak de herrer.";
        crime2[0].talk[2] = "Her er nøgledelen.";

        crime2[1].bubbleCount = 2;
        crime2[1].talk = new string[2];
        crime2[1].animNum = new int[2] { 1, 1 };
        crime2[1].animFin = new bool[2];
        crime2[1].talk[0] = "Fantastisk!";
        crime2[1].talk[1] = "Lad os finde den næste trylledrik.";

        zone[0].bubbleCount = 4;
        zone[0].talk = new string[4];
        zone[0].animNum = new int[4] { 1, 1, 1, 1 };
        zone[0].animFin = new bool[4];
        zone[0].talk[0] = "Øøøh.";
        zone[0].talk[1] = "Jeg kan huske jeg gav den til nogen i zonen.";
        zone[0].talk[2] = "Men jeg ved ikke hvem der er på skansen idag.";
        zone[0].talk[3] = "Skal vi ikke checke det ud?";

        zone[1].bubbleOwner = 1;
        zone[1].bubbleCount = 1;
        zone[1].talk = new string[1];
        zone[1].animNum = new int[1] { 1 };
        zone[1].animFin = new bool[1];
        zone[1].talk[0] = "h-h-hvem er du?";

        zone[2].bubbleCount = 3;
        zone[2].talk = new string[3];
        zone[2].animNum = new int[3] { 1, 1, 1 };
        zone[2].animFin = new bool[3];
        zone[2].talk[0] = "Nej, dig kender jeg ikke.";
        zone[2].talk[1] = "Men mit navn er Bib.";
        zone[2].talk[2] = "Har du set en nøgledel heromkring?";

        zone[3].bubbleOwner = 1;
        zone[3].bubbleCount = 3;
        zone[3].talk = new string[3];
        zone[3].animNum = new int[3] { 1, 1, 1 };
        zone[3].animFin = new bool[3];
        zone[3].talk[0] = "Mit navn er Kagemand.";
        zone[3].talk[1] = "Je-je-jeg fandt en der lå og flød.";
        zone[3].talk[2] = "Hvad rager det dig?";

        zone[4].bubbleCount = 2;
        zone[4].talk = new string[2];
        zone[4].animNum = new int[2] { 1, 1 };
        zone[4].animFin = new bool[2];
        zone[4].talk[0] = "Vi skal bruge den til at åbne min kiste.";
        zone[4].talk[1] = "Må vi få den?";

        zone[5].bubbleOwner = 1;
        zone[5].bubbleCount = 5;
        zone[5].talk = new string[5];
        zone[5].animNum = new int[5] { 1, 1, 1, 1, 1 };
        zone[5].animFin = new bool[5];
        zone[5].talk[0] = "Må-må-måske...";
        zone[5].talk[1] = "Jeg er på vej hen til julemanden...";
        zone[5].talk[2] = "...Men på vejen tabte jeg en af mine knapper.";
        zone[5].talk[3] = "Den trillede ned ad trapperne og jeg tør ikke gå derned.";
        zone[5].talk[4] = "Hvis i hjælper mig med at finde den må i få jeres del.";

        zone[6].bubbleCount = 1;
        zone[6].talk = new string[1];
        zone[6].animNum = new int[1] { 1 };
        zone[6].animFin = new bool[1];
        zone[6].talk[0] = "Tjah, der er vist ikke andet at gøre.";

        zone2[0].bubbleOwner = 1;
        zone2[0].bubbleCount = 2;
        zone2[0].talk = new string[2];
        zone2[0].animNum = new int[2] { 1, 1 };
        zone2[0].animFin = new bool[2];
        zone2[0].talk[0] = "T-t-tusind tak.";
        zone2[0].talk[1] = "Her er jeres nøglestykke.";

        zone2[1].bubbleCount = 4;
        zone2[1].talk = new string[3];
        zone2[1].animNum = new int[3] { 1, 1, 1 };
        zone2[1].animFin = new bool[3];
        zone2[1].talk[0] = "Jamen det er jo fantastisk!";
        zone2[1].talk[1] = "Lad os hoppe tilbage den vej vi kom fra.";
        zone2[1].talk[2] = "Måske vi finder en trylledrik på vejen som kan lede os videre.";

        outro.bubbleCount = 4;
        outro.talk = new string[4];
        outro.animNum = new int[4] { 1, 1, 1, 1 };
        outro.animFin = new bool[4];
        outro.talk[0] = "JADA!";
        outro.talk[1] = "Du kan vise den næste skærm ved receptionen.";
        outro.talk[2] = "Så kan du få din del af skatten.";
        outro.talk[3] = "Tusind tak for hjælpen!";
    }

    void Awake()
    {
        Button btn = button.GetComponent<Button>();                     //or just drag-n-drop the button in the CustomButton field
        btn.onClick.AddListener(button_onClick);                        //subscribe to the onClick event
    }

    //Handle the onClick event
    void button_onClick()
    {
        pressCount++;
    }

    public void playCompAnimation(interaction inter)                    //Function for different companion animations. 
    {
        for (int i = 0; i < inter.talk.Length; i++)                     //Go through each talk in the interaction
        {
            if (inter.current == true)                                  //But only if the interaction is the current one.
            {
                if (inter.animFin[pressCount] == false)                 //If this talk's animation hasn't already played
                {
                    if (inter.animNum[pressCount] == 1)                 //If this talk's animation number is equal to 1, play the talking animation.
                    {
                        companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.0f);
                        companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                        companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);

                        //Pointing at treasure animation
                        if (inter.current == intro.current && pressCount == 3)
                        {
                            companion.GetComponent<Animator>().Play("PointingAtTreasure_Animation");
                            companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                        }

                        //Thinking animation once the first potion is scanned
                        if (inter.current == introContinued.current && pressCount == 0)
                        {
                            companion.GetComponent<Animator>().Play("Thinking_Animation");
                            companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                            companion.GetComponent<Animator>().SetBool("isCompanionThinking", true);
                        }

                        inter.animFin[pressCount] = true;
                    }
                }
            }
            //companion.SetActive(false);
        }
    }

    public void playCompAnimation(interaction[] interArray)
    {
        //Animation for the pre minigame conversation in the arcade section
        if (interArray == arkade)
        {
            for (int i = 0; i < interArray.Length; i++)
            {
                foreach (string str in interArray[i].talk)
                {
                    if (interArray[i].current == true)
                    {
                        if (interArray[i].animFin[pressCount] == false && interArray[i].animNum[pressCount] == 1)
                        {
                            if (interArray[i].current == arkade[0].current)
                            {
                                if (pressCount == 0)
                                {
                                    companion.GetComponent<Animator>().Play("Thinking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                                    companion.GetComponent<Animator>().SetBool("thinkToTalk", true);
                                    companion.GetComponent<Animator>().SetBool("isCompanionThinking", false);
                                }
                                else
                                {
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.0f);
                                    companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                                }
                            }

                            if (interArray[i].current == arkade[2].current || interArray[i].current == arkade[4].current)
                            {
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.0f);
                                companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);

                                Debug.Log("This is arcade point number " + i);
                            }

                            if (interArray[i].current == arkade[6].current)
                            {
                                if (pressCount == 0)
                                {
                                    companion.GetComponent<Animator>().Play("Thinking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                                    companion.GetComponent<Animator>().SetBool("isCompanionThinking", true);
                                }
                                else
                                {
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.0f);
                                    companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                                }

                                Debug.Log("This is arcade point number " + i);
                            }

                            if (interArray[i].current == arkade[1].current || interArray[i].current == arkade[3].current || interArray[i].current == arkade[5].current)
                            {
                                arcade_Machine.GetComponent<Animator>().SetFloat("arcadeAnimationSpeed", 0.7f);
                                arcade_Machine.GetComponent<Animator>().Play("Arcade_Talking", -1, 0f);
                                arcade_Machine.GetComponent<Animator>().SetBool("state_ArcadeIdle", true);

                                Debug.Log("This is arcade point number " + i);
                            }

                            interArray[i].animFin[pressCount] = true;
                        }
                    }
                }
            }
        }

        //Animation the post minigame conversation in the arcade section
        if (interArray == arkade2)
        {
            for (int i = 0; i < interArray.Length; i++)
            {
                //for (int j = 0; j < interArray[1].talk.Length; j++)
                //{
                foreach (string str in interArray[i].talk)
                {
                    if (interArray[i].current == true)
                    {
                        if (interArray[i].animFin[pressCount] == false && interArray[i].animNum[pressCount] == 1)
                        {
                            if (interArray[i].current == arkade2[0].current)
                            {
                                if (pressCount == 0)
                                {
                                    arcade_Machine.GetComponent<Animator>().SetFloat("arcadeAnimationSpeed", 0.7f);
                                    arcade_Machine.GetComponent<Animator>().Play("Arcade_Talking", -1, 0f);
                                    arcade_Machine.GetComponent<Animator>().SetBool("state_ArcadeIdle", true);
                                }
                                else
                                {
                                    arcade_Machine.GetComponent<Animator>().SetFloat("arcadeAnimationSpeed", 0.7f);
                                    arcade_Machine.GetComponent<Animator>().Play("Arcade_Smiling");
                                    arcade_Machine.GetComponent<Animator>().SetBool("state_ArcadeSmileToWave", true);
                                }
                            }

                            if (interArray[i].current == arkade2[1].current)
                            {
                                if (pressCount == 0)
                                {
                                    happyCompanion.GetComponent<Animator>().Play("Wiggle_HappyState");
                                }
                                else
                                {
                                    happyCompanion.GetComponent<Animator>().Play("Talking_HappyState", -1, 0f);
                                    happyCompanion.GetComponent<Animator>().SetBool("isHappyCompanionTalking", true);
                                }
                            }

                            interArray[i].animFin[pressCount] = true;
                        }
                    }
                }
                //}
            }
        }
    }

    public void zoneSectionAnimation(interaction[] interArray)
    {
        //Animation the pre- minigame conversation in the zone section
        if (interArray == zone)
        {
            for (int i = 0; i < interArray.Length; i++)
            {
                foreach (string str in interArray[i].talk)
                {
                    if (interArray[i].current == true)
                    {
                        if (interArray[i].animFin[pressCount] == false && interArray[i].animNum[pressCount] == 1)
                        {
                            if (interArray[i].current == zone[0].current)
                            {
                                if (pressCount == 0)
                                {
                                    companion.GetComponent<Animator>().Play("Thinking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                                    companion.GetComponent<Animator>().SetBool("thinkToTalk", true);
                                    companion.GetComponent<Animator>().SetBool("isCompanionThinking", false);
                                }
                                else
                                {
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.0f);
                                    companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                                }
                            }

                            if (interArray[i].current == zone[2].current || interArray[i].current == zone[4].current || interArray[i].current == zone[6].current)
                            {
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.0f);
                                companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                            }

                            if (interArray[i].current == zone[1].current || interArray[i].current == zone[3].current || interArray[i].current == zone[5].current)
                            {
                                gingerbread_NoButton.GetComponent<Animator>().Play("CookieIdle", -1, 0f);
                                gingerbread_NoButton.GetComponent<Animator>().SetFloat("cookieSpeed", 0.8f);
                            }

                            interArray[i].animFin[pressCount] = true;
                        }
                    }
                }
            }
        }

        //Animation the post minigame conversation in the zone section
        if (interArray == zone2)
        {
            for (int i = 0; i < interArray.Length; i++)
            {
                foreach (string str in interArray[i].talk)
                {
                    if (interArray[i].current == true)
                    {
                        if (interArray[i].animFin[pressCount] == false && interArray[i].animNum[pressCount] == 1)
                        {
                            if (interArray[i].current == zone2[0].current)
                            {
                                if (pressCount == 0)
                                {
                                    gingerbread.GetComponent<Animator>().Play("CookieJump");
                                    gingerbread.GetComponent<Animator>().SetFloat("cookieSpeed", 1.5f);
                                }
                                else
                                {
                                    gingerbread.GetComponent<Animator>().Play("CookieWave");
                                    gingerbread.GetComponent<Animator>().SetFloat("cookieSpeed", 0.6f);
                                }
                            }

                            if (interArray[i].current == zone2[1].current)
                            {
                                if (pressCount == 0)
                                {
                                    happyCompanion.GetComponent<Animator>().Play("Wiggle_HappyState");
                                }
                                else
                                {
                                    happyCompanion.GetComponent<Animator>().Play("Talking_HappyState", -1, 0f);
                                    happyCompanion.GetComponent<Animator>().SetBool("isHappyCompanionTalking", true);
                                }
                            }

                            interArray[i].animFin[pressCount] = true;
                        }
                    }
                }
            }
        }
    }

    public void crimeSectionAnimation(interaction[] interArray)
    {
        if (interArray == crime)
        {
            for (int i = 0; i < interArray.Length; i++)
            {
                foreach (string str in interArray[i].talk)
                {
                    if (interArray[i].current == true)
                    {
                        if (interArray[i].animFin[pressCount] == false && interArray[i].animNum[pressCount] == 1)
                        {
                            if (interArray[i].current == crime[0].current)
                            {
                                if (pressCount == 0)
                                {
                                    companion.GetComponent<Animator>().Play("Thinking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                                    companion.GetComponent<Animator>().SetBool("thinkToTalk", true);
                                    companion.GetComponent<Animator>().SetBool("isCompanionThinking", false);
                                }
                                else
                                {
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.0f);
                                    companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                                }
                            }

                            if (interArray[i].current == crime[1].current)
                            {
                                detective.GetComponent<Animator>().Play("detectiveIdleTalk", -1, 0f);
                                detective.GetComponent<Animator>().SetFloat("detectiveSpeed", 0.6f);
                                detective.GetComponent<Animator>().SetBool("detective_talkToIdle", true);
                            }

                            if (interArray[i].current == crime[2].current || interArray[i].current == crime[4].current)
                            {
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.0f);
                                companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                            }

                            if (interArray[i].current == crime[3].current)
                            {
                                if (pressCount == 0)
                                {
                                    detective.GetComponent<Animator>().Play("detectiveIdleTalk", -1, 0f);
                                    detective.GetComponent<Animator>().SetFloat("detectiveSpeed", 0.6f);
                                    detective.GetComponent<Animator>().SetBool("detective_talkToIdle", true);
                                }

                                else
                                {
                                    detective.GetComponent<Animator>().Play("detectiveThinkTalk", -1, 0);
                                    detective.GetComponent<Animator>().SetFloat("detectiveSpeed", 0.6f);
                                    detective.GetComponent<Animator>().SetBool("detective_thinkToIdle", true);
                                }
                            }

                            if (interArray[i].current == crime[5].current)
                            {
                                if (pressCount == 0)
                                {
                                    detective.GetComponent<Animator>().Play("detectiveThumbsUp");
                                    detective.GetComponent<Animator>().SetFloat("detectiveSpeed", 0.6f);
                                    detective.GetComponent<Animator>().SetBool("detective_ThumbsToIdle", true);
                                }
                                else
                                {
                                    detective.GetComponent<Animator>().Play("detectiveThinkTalk", -1, 0);
                                    detective.GetComponent<Animator>().SetFloat("detectiveSpeed", 0.6f);
                                    detective.GetComponent<Animator>().SetBool("detective_thinkToIdle", true);
                                }
                            }

                            interArray[i].animFin[pressCount] = true;
                        }
                    }
                }
            }
        }

        if (interArray == crime2)
        {
            for (int i = 0; i < interArray.Length; i++)
            {
                foreach (string str in interArray[i].talk)
                {
                    if (interArray[i].current == true)
                    {
                        if (interArray[i].animFin[pressCount] == false && interArray[i].animNum[pressCount] == 1)
                        {
                            if (interArray[i].current == crime2[0].current)
                            {
                                if (pressCount == 0)
                                {
                                    detective.GetComponent<Animator>().Play("detectiveThumbsUp");
                                    detective.GetComponent<Animator>().SetFloat("detectiveSpeed", 0.6f);
                                    detective.GetComponent<Animator>().SetBool("detective_ThumbsToIdle", true);
                                }
                                else
                                {
                                    detective.GetComponent<Animator>().Play("detectiveIdleTalk", -1, 0);
                                    detective.GetComponent<Animator>().SetFloat("detectiveSpeed", 0.6f);
                                    detective.GetComponent<Animator>().SetBool("detective_TalkToWave", true);
                                    detective.GetComponent<Animator>().SetBool("detective_talkToIdle", false);
                                    detective.GetComponent<Animator>().SetBool("detective_waveToIdle", true);
                                }
                            }

                            if (interArray[i].current == crime2[1].current)
                            {
                                if (pressCount == 0)
                                {
                                    happyCompanion.GetComponent<Animator>().Play("Wiggle_HappyState");
                                }
                                else
                                {
                                    happyCompanion.GetComponent<Animator>().Play("Talking_HappyState", -1, 0f);
                                    happyCompanion.GetComponent<Animator>().SetBool("isHappyCompanionTalking", true);
                                }
                            }

                            interArray[i].animFin[pressCount] = true;
                        }
                    }
                }
            }
        }
    }

    public void gameOver(interaction inter)
    {
        for (int i = 0; i < inter.talk.Length; i++)
        {
            if (inter.current == true)
            {
                if (inter.animFin[pressCount] == false && inter.animNum[pressCount] == 1)
                {
                    if (pressCount == 0)
                    {
                        happyCompanion.GetComponent<Animator>().Play("Wiggle_HappyState");
                    }
                    else
                    {
                        happyCompanion.GetComponent<Animator>().Play("Talking_HappyState", -1, 0f);
                        happyCompanion.GetComponent<Animator>().SetBool("happyCompanionTalkToWiggle", true);
                        happyCompanion.GetComponent<Animator>().SetBool("isHappyCompanionTalking", false);
                    }

                    inter.animFin[pressCount] = true;
                }
            }
        }
    }
}