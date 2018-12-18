using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The purpose of the script is to handle the monologue of the 2D companion, the dialogue between the companion and the 3D characters
// and also the appearing of the visual feedback in the form of speech bubbles that appear and disappear according to current conversation.
// Furthermore, the script plays audiofiles that correspond to the ongoing conversation, to provide audible feedback to the player.

public class textBubble : MonoBehaviour
{
    // public Text, GameObject, RawImage and AudioSource variables are declared at the start of the script.
    // They are defined in the inspector window of Unity.

    public Text talking; //Text object for bibliothecary's displayed text.  
    public Text talking2; //Text object for other characters' displayed text.
    public GameObject speechBubble1; //RawImage object containing bibliothecary's speechbubble picture.
    public GameObject speechBubble2; //RawImage object containing other characters' speechbubble picture.
    public GameObject companion, happyCompanion, arcade_Machine, gingerbread, gingerbread_NoButton, detective; //RawImage objects containing character portraits
    public RawImage arcadeImage, GBImage, detectiveImage; //Image targets

    //Initialization of audio file variables.
    public AudioSource introAud, introAud2, introAud3, introAud4, introAud5, introAud6, introAud7, introAud8,
                       introAud9, introAud10; 
    public AudioSource introContAud, introContAud2, introContAud3, introContAud4, introContAud5;
    public AudioSource introContChest, introContChest2, introContChest3, introContChest4, introContChest5, introContChest6, introContChest7;
    public AudioSource outroAud, outroAud2, outroAud3, outroAud4;
    public AudioSource pixel, pixel2, pixel3;
    public AudioSource pixeltwo, pixeltwo2, pixeltwo3, pixeltwo4;
    public AudioSource pixelthree, pixelthree2;
    public AudioSource sherlock, sherlock2;
    public AudioSource sherlocktwo, sherlocktwo2, sherlocktwo3, sherlocktwo4;
    public AudioSource sherlockthree, sherlockthree2, sherlockthree3, sherlockthree4, sherlockthree5;
    public AudioSource sherlockfour, sherlockfour2, sherlockfour3;
    public AudioSource kagemand;
    public AudioSource kagemandtwo, kagemandtwo2, kagemandtwo3;
    public AudioSource kagemandthree, kagemandthree2, kagemandthree3, kagemandthree4;
    public AudioSource kagemandfour;
    public AudioSource arcade, arcade2, arcade3, arcade4;
    public AudioSource arcadetwo, arcadetwo2, arcadetwo3, arcadetwo4;
    public AudioSource arcadethree, arcadethree2, arcadethree3, arcadethree4;
    public AudioSource arcadeHelp, arcadeHelp2, arcadeHelp3;
    public AudioSource crimeAud, crimeAud2, crimeAud3;
    public AudioSource crimetwo;
    public AudioSource crimethree, crimethree2;
    public AudioSource crimeHelpAud, crimeHelpAud2, crimeHelpAud3;
    public AudioSource zoneAud, zoneAud2, zoneAud3;
    public AudioSource zonetwo, zonetwo2;
    public AudioSource zonethree;
    public AudioSource zonefour, zonefour2, zonefour3, zonefour4;
    public AudioSource zoneHelpAud, zoneHelpAud2, zoneHelpAud3, zoneHelpAud4;
    public AudioSource zoneButton, zoneButton2;
    public AudioSource arkHelp2, arkHelp3, gerningsstedHelp;



    //Struct containing everything neccessary for a conversation in game.
    public struct interaction
    {
        public bool current;            //Determines whether this interaction is the current one.

        public int bubbleCount;         //Number of talks in an interaction
        public int bubbleOwner;         //Determines who the interaction belongs to.
        
        //Arrays for storing each talk element's variables
        public string[] talk;           //Determines what is written
        public int[] animNum;           //Corresponding animation to the talk 
        public bool[] animFin;          //Determines if animation has been played
        public AudioSource[] audArray;  //Stores each talk's audio file
        public bool[] audFin;           //Determines if audio file has already played
    }

    //Instancing of interaction Arrays and structs.
    public interaction intro;                                   //Starting talk
    public interaction introContinued, introContinued2;        //Starting talk after first object scan
    public interaction[] arkade = new interaction[5];           //Arkade Grandmaster intro talk.
    public interaction[] arkade2 = new interaction[2];          //Arkade Grandmaster post-question talk
    public interaction[] crime = new interaction[7];            //Crime Grandmaster intro talk.
    public interaction[] crime2 = new interaction[2];           //Crime Grandmaster post-question talk
    public interaction[] zone = new interaction[9];             //Zone Grandmaster intro talk.
    public interaction[] zone2 = new interaction[2];            //Zone Grandmaster post-question talk
    public interaction zone3;                                   //Zone Post-grandmaster companion talk
    public interaction outro;                                   //Outro talk

    public interaction arkadeHelp;                              //Arkade help poster talk
    public interaction crimeHelp;                               //Crime help poster talk
    public interaction zoneHelp;                                //Zone help poster talk

    public int pressCount = 0;                                  //Number of times button has been pressed in current interaction
    public Button button;                                       //Button for progressing interaction


    public GameObject startButton;

    //Function that runs once upon start
    void Start()
    {
        interactionSetup();                                     //Set up variables into different interactions
    }

    // Update is called once per frame
    void Update()
    {
        // The condition for the if-statement checks, whether the player has pressed the button to play the game
        // If the button has been pressed, the conversation struct containing the intro is set active, by setting the Boolean "current" inside the struct to true.
        if (startButton.GetComponent<startScreen>().startTheGame == true)
        {
            intro.current = true;
            startButton.GetComponent<startScreen>().startTheGame = false;
        }

        // These are all the available conversations that are encountered throughout the game.
        // Whenever the "current" Boolean inside one of these structs are is set to true, the conversation is started.
        allStopDisplay();
        allDisplayText();
        playCompAnimation(intro);
        playCompAnimation(introContinued);
        playCompAnimation(introContinued2);
        playCompAnimation(arkade2);
        zoneSectionAnimation(zone);
        zoneSectionAnimation(zone2);
        playCompAnimation(zone3);
        crimeSectionAnimation(crime);
        crimeSectionAnimation(crime2);
        playCompAnimation(arkade);
        gameOver(outro);
    }


    // Run displayText on each interaction in the program.
    public void allDisplayText()
    {
        displayText(intro);
        displayText(introContinued);
        displayText(introContinued2);
        displayText(arkade);
        displayText(arkade2);
        displayText(crime);
        displayText(crime2);
        displayText(zone);
        displayText(zone2);
        displayText(zone3);
        displayText(outro);
    }


    // DisplayText overload for single interactions
    // This function runs through every line of text, if the "current" Boolean of one of the structs for the conversations is set to true.
    // While displaying all text the text in a struct, this function also plays the audio files in the conversation.
    public void displayText(interaction inter)
    {
        if (inter.current == true)
        {
            if (inter.bubbleOwner == 0)
            {
                speechBubble1.SetActive(true);
                button.gameObject.SetActive(true);
                talking.text = inter.talk[pressCount];
                Debug.Log(pressCount);
                if (inter.audFin[pressCount] == false)
                {
                    inter.audArray[pressCount].Play();
                    inter.audFin[pressCount] = true;
                    if (pressCount > 0)
                    {
                        inter.audArray[pressCount - 1].Stop();
                    }
                    debugvar++;
                    Debug.Log(debugvar);
                }

                if (outro.current == true)
                {
                    happyCompanion.SetActive(true);
                    companion.SetActive(false);
                    arcadeImage.gameObject.SetActive(false);
                    GBImage.gameObject.SetActive(false);
                    detectiveImage.gameObject.SetActive(false);
                }
                else
                {
                    companion.SetActive(true);
                    happyCompanion.SetActive(false);
                    arcadeImage.gameObject.SetActive(false);
                    GBImage.gameObject.SetActive(false);
                    detectiveImage.gameObject.SetActive(false);
                }
            }
        }
    }


    // Display text in the owner's corresponding textbubble
    // This function provides the same functionality as the prior function.
    // Instead of taking a single interaction struct as an argument, this function takes an interaction-array struct as argument.
    public void displayText(interaction[] interArray)
    {
        for (int i = 0; i < interArray.Length; i++)
        {
            if (interArray[i].current == true)
            {
                if (interArray[i].bubbleOwner == 0)
                {
                    speechBubble1.SetActive(true);
                    button.gameObject.SetActive(true);
                    talking.text = interArray[i].talk[pressCount];

                    if (zone2[1].current == true || crime2[1].current == true || arkade2[1].current == true)
                    {
                        happyCompanion.SetActive(true);
                        companion.SetActive(false);
                        arcadeImage.gameObject.SetActive(false);
                        GBImage.gameObject.SetActive(false);
                        detectiveImage.gameObject.SetActive(false);
                    }
                    else
                    {
                        companion.SetActive(true);
                        happyCompanion.SetActive(false);
                        arcadeImage.gameObject.SetActive(false);
                        GBImage.gameObject.SetActive(false);
                        detectiveImage.gameObject.SetActive(false);
                    }

                    if (interArray[i].audFin[pressCount] == false)
                    {
                        interArray[i].audArray[pressCount].Play();
                        if (pressCount > 0)
                        {
                            interArray[i].audArray[pressCount - 1].Stop();
                        }
                        interArray[i].audFin[pressCount] = true;
                        debugvar++;
                        Debug.Log(debugvar);
                    }
                }
                else
                {
                    speechBubble2.SetActive(true);
                    button.gameObject.SetActive(true);
                    talking2.text = interArray[i].talk[pressCount];

                    if ((zone[3].current || zone[5].current || zone[7].current) == true || zone2[0].current == true)
                    {
                        GBImage.gameObject.SetActive(true);
                        companion.SetActive(false);
                        happyCompanion.SetActive(false);
                        arcadeImage.gameObject.SetActive(false);
                        detectiveImage.gameObject.SetActive(false);
                    }
                    else if ((crime[2].current || crime[4].current || crime[6].current) == true || crime2[0].current == true)
                    {
                        detectiveImage.gameObject.SetActive(true);
                        companion.SetActive(false);
                        happyCompanion.SetActive(false);
                        arcadeImage.gameObject.SetActive(false);
                        GBImage.gameObject.SetActive(false);
                    }
                    else if ((arkade[1].current || arkade[3].current) == true || arkade2[0].current == true)
                    {
                        arcadeImage.gameObject.SetActive(true);
                        companion.SetActive(false);
                        happyCompanion.SetActive(false);
                        GBImage.gameObject.SetActive(false);
                        detectiveImage.gameObject.SetActive(false);
                    }

                    if (interArray[i].audFin[pressCount] == false)
                    {
                        interArray[i].audArray[pressCount].Play();
                        interArray[i].audFin[pressCount] = true;
                        if (pressCount > 0)
                        {
                            interArray[i].audArray[pressCount - 1].Stop();
                        }
                    }
                }
            }
        }
    }

    // Check if pressCount is above the number of speechbubbles in the interaction and if 'TRUE' set that interaction.current to false and both speechbubbles to inactive.
    // This will function will make the speechbubbles disappear, when a conversation is over.
    public bool stopDisplayText(interaction inter)
    {
        if (pressCount >= inter.bubbleCount && inter.current == true)
        {
            pressCount = 0;
            speechBubble1.SetActive(false);
            speechBubble2.SetActive(false);
            companion.SetActive(false);
            happyCompanion.SetActive(false);
            arcadeImage.gameObject.SetActive(false);
            GBImage.gameObject.SetActive(false);
            detectiveImage.gameObject.SetActive(false);
            speechBubble1.SetActive(false);
            button.gameObject.SetActive(false);

            return false;
        }
        return true;
    }

    // This function sets all the current Booleans inside the individual interaction struct variables to false, if they are true.
    // This ensures that only a single conversation is played.
    public void allStopDisplay()
    {
        if (intro.current == true)
        {
            intro.current = stopDisplayText(intro);
        }

        if (introContinued.current == true)
        {
            introContinued.current = stopDisplayText(introContinued);
        }

        if (introContinued2.current == true)
        {
            introContinued2.current = stopDisplayText(introContinued2);
        }

        if (zone3.current == true)
        {
            zone3.current = stopDisplayText(zone3);
        }

        if (outro.current == true)
        {
            outro.current = stopDisplayText(outro);
        }

        for (int i = 0; i < arkade.Length; i++)
        {
            if (arkade[i].current == true)
            {
                arkade[i].current = stopDisplayText(arkade[i]);

                if (i == 4 && arkade[4].current == false)
                {
                    break;
                }

                if (GameObject.Find("ImageTargetArcade").GetComponent<ArcadeDefaultTrackableEventHandler>().notTheArcadePotion == true &&
                    arkade.Length > i && arkade[i].current == false)
                {
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

                if (i == 8 && zone[8].current == false)
                {
                    break;
                }

                if (GameObject.Find("ImageTargetGinger").GetComponent<GingerDefaultTrackableEventHandler>().notTheGingerPotion == true &&
                    zone.Length > i && zone[i].current == false)
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

                if (i == 6 && crime[6].current == false)
                {
                    break;
                }

                if (GameObject.Find("ImageTargetCulprit").GetComponent<CulpritDefaultTrackableEventHandler>().notTheCulpritPotion == true &&
                    crime.Length > i && crime[i].current == false)
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
                    break;
                }

                if (crime2.Length > i && crime2[i].current == false)
                {
                    crime2[i + 1].current = true;
                }
            }
        }
    }

    // This function initalizes the individual variables for each struct, that are declared at the start of this script.
    public void interactionSetup()
    {
        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);

        intro.bubbleCount = 10;
        intro.talk = new string[10];
        intro.animNum = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        intro.animFin = new bool[10];
        intro.audArray = new AudioSource[10];
        intro.audFin = new bool[10];
        intro.audArray[0] = introAud;
        intro.audArray[1] = introAud2;
        intro.audArray[2] = introAud3;
        intro.audArray[3] = introAud4;
        intro.audArray[4] = introAud5;
        intro.audArray[5] = introAud6;
        intro.audArray[6] = introAud7;
        intro.audArray[7] = introAud8;
        intro.audArray[8] = introAud9;
        intro.audArray[9] = introAud10;
        intro.talk[0] = "Jamen halli-hallo";
        intro.talk[1] = "Mit navn er Bib";
        intro.talk[2] = "Bibliotekar";
        intro.talk[3] = "Idag er en stor dag.";
        intro.talk[4] = "Det er dagen hvor jeg gør et eller andet.";
        intro.talk[5] = "Hvad det nu end er...";
        intro.talk[6] = "Det kan jeg ikke huske.";
        intro.talk[7] = "Kan du ikke lige hjælpe mig?";
        intro.talk[8] = "Sigt dit kamera op mod den trylledrik der.";
        intro.talk[9] = "Det vil hjælpe mig med at huske.";

        introContinued.bubbleCount = 1;
        introContinued.talk = new string[1];
        introContinued.animNum = new int[1] { 1 };
        introContinued.animFin = new bool[1];
        introContinued.audFin = new bool[1];
        introContinued.audArray = new AudioSource[1];
        introContinued.audArray[0] = introContAud;                         
        introContinued.talk[0] = "";


        introContinued2.bubbleCount = 7;
        introContinued2.talk = new string[7];
        introContinued2.animNum = new int[7] { 1, 1, 1, 1, 1, 1, 1 };
        introContinued2.animFin = new bool[7];
        introContinued2.audFin = new bool[7];
        introContinued2.audArray = new AudioSource[7];
        introContinued2.audArray[0] = introContChest;
        introContinued2.audArray[1] = introContChest2;
        introContinued2.audArray[2] = introContChest3;
        introContinued2.audArray[3] = introContChest4;
        introContinued2.audArray[4] = introContChest5;
        introContinued2.audArray[5] = introContChest6;
        introContinued2.audArray[6] = introContChest7;
        introContinued2.talk[0] = "Nååå ja.";
        introContinued2.talk[1] = "Det er selvfølgelig med en nøgle.";
        introContinued2.talk[2] = "Min nøgle er til gengæld gået i stykker...";
        introContinued2.talk[3] = "Og jeg gav nøgledelene til nogle venner.";
        introContinued2.talk[4] = "Jeg kan til gengæld ikke huske hvor de alle er henne.";
        introContinued2.talk[5] = "Lad os gå ud af børnesektionen og finde flere trylledrikke.";
        introContinued2.talk[6] = "De vil sikkert hjælpe mig med at huske hvor mine venner er blevet af.";

        arkade[0].bubbleCount = 1;
        arkade[0].audFin = new bool[1];
        arkade[0].audArray = new AudioSource[1];
        arkade[0].audArray[0] = arcadeHelp;                                        
        arkade[0].animNum = new int[1] { 1 };
        arkade[0].animFin = new bool[1];
        arkade[0].talk = new string[1];
        arkade[0].talk[0] = "";

        arkade[1].bubbleOwner = 1;
        arkade[1].bubbleCount = 3;
        arkade[1].talk = new string[3];
        arkade[1].animNum = new int[3] { 1, 1, 1 };
        arkade[1].animFin = new bool[3];
        arkade[1].audArray = new AudioSource[3];
        arkade[1].audFin = new bool[3];
        arkade[1].audArray[0] = pixel;
        arkade[1].audArray[1] = pixel2;
        arkade[1].audArray[2] = pixel3;
        arkade[1].talk[0] = "Jamen hvem kommer her til min bule?";
        arkade[1].talk[1] = "Bib og en kammerat?";
        arkade[1].talk[2] = "Har i lyst til et spil Pac-Man?";

        arkade[2].bubbleCount = 4;
        arkade[2].talk = new string[4];
        arkade[2].animNum = new int[4] { 1, 1, 1, 1 };
        arkade[2].animFin = new bool[4];
        arkade[2].audFin = new bool[4];
        arkade[2].audArray = new AudioSource[4];
        arkade[2].audArray[0] = arcade;
        arkade[2].audArray[1] = arcade2;
        arkade[2].audArray[2] = arcade3;
        arkade[2].audArray[3] = arcade4;
        arkade[2].talk[0] = "Hejsa, Pixel min dreng!";
        arkade[2].talk[1] = "Nej, ikke idag.";
        arkade[2].talk[2] = "Vi er ude at lede efter nøgledelene.";
        arkade[2].talk[3] = "Har du stadig den jeg gav til dig?";

        arkade[3].bubbleOwner = 1;
        arkade[3].bubbleCount = 3;
        arkade[3].talk = new string[3];
        arkade[3].animNum = new int[3] { 1, 1, 1 };
        arkade[3].animFin = new bool[3];
        arkade[3].audArray = new AudioSource[3];
        arkade[3].audFin = new bool[3];
        arkade[3].audArray[0] = pixeltwo;
        arkade[3].audArray[1] = pixeltwo2;
        arkade[3].audArray[2] = pixeltwo3;
        arkade[3].talk[0] = "Ja og jeg skal nok give dig den...";
        arkade[3].talk[1] = "Efter du beviser at du stadig er en spiller.";
        arkade[3].talk[2] = "Den bib jeg kender ved nemlig hvor mange spøgelser der er i Pac-man";

        arkade[4].bubbleCount = 4;
        arkade[4].talk = new string[4];
        arkade[4].animNum = new int[4] { 1, 1, 1, 1 };
        arkade[4].animFin = new bool[4];
        arkade[4].audFin = new bool[4];
        arkade[4].audArray = new AudioSource[4];
        arkade[4].audArray[0] = arcadetwo;
        arkade[4].audArray[1] = arcadetwo2;
        arkade[4].audArray[2] = arcadetwo3;
        arkade[4].audArray[3] = arcadetwo4;
        arkade[4].talk[0] = "...hehe...";
        arkade[4].talk[1] = "Ja.";
        arkade[4].talk[2] = "Selvfølgelig.";
        arkade[4].talk[3] = "Psst... Hjælp mig..";

        arkade2[0].bubbleOwner = 1;
        arkade2[0].bubbleCount = 2;
        arkade2[0].talk = new string[2];
        arkade2[0].animNum = new int[2] { 1, 1 };
        arkade2[0].animFin = new bool[2];
        arkade2[0].audArray = new AudioSource[2];
        arkade2[0].audFin = new bool[2];
        arkade2[0].audArray[0] = pixelthree;
        arkade2[0].audArray[1] = pixelthree2;
        arkade2[0].talk[0] = "Jeg vidste du stadig var en spiller!";
        arkade2[0].talk[1] = "Her er nøgledelen.";

        arkade2[1].bubbleCount = 4;
        arkade2[1].talk = new string[4];
        arkade2[1].animNum = new int[4] { 1, 1, 1, 1 };
        arkade2[1].animFin = new bool[4];
        arkade2[1].audFin = new bool[4];
        arkade2[1].audArray = new AudioSource[4];
        arkade2[1].audArray[0] = zonefour;
        arkade2[1].audArray[1] = zonefour2;
        arkade2[1].audArray[2] = zonefour3;
        arkade2[1].audArray[3] = zonefour4;
        arkade2[1].talk[0] = "Jamen det er jo fantastisk!!";
        arkade2[1].talk[1] = "Vi har fundet alle nøgledelene!";
        arkade2[1].talk[2] = "Lad os gå tilbage til kisten.";
        arkade2[1].talk[3] = "Det er tid til vores beløning";

        crime[0].bubbleCount = 1;    
        crime[0].audFin = new bool[1];
        crime[0].audArray = new AudioSource[1];
        crime[0].audArray[0] = crimeHelpAud;                                
        crime[0].animNum = new int[1] { 1 };
        crime[0].animFin = new bool[1];
        crime[0].talk = new string[1];
        crime[0].talk[0] = "";

        crime[1].bubbleCount = 1;
        crime[1].audArray = new AudioSource[1];
        crime[1].audFin = new bool[1];
        crime[1].audArray[0] = gerningsstedHelp;                          
        crime[1].animNum = new int[1] { 1 };
        crime[1].animFin = new bool[1];
        crime[1].talk = new string[1];
        crime[1].talk[0] = "";

        crime[2].bubbleOwner = 1;
        crime[2].bubbleCount = 2;
        crime[2].talk = new string[2];
        crime[2].animNum = new int[2] { 1, 1 };
        crime[2].animFin = new bool[2];
        crime[2].audArray = new AudioSource[2];
        crime[2].audFin = new bool[2];
        crime[2].audArray[0] = sherlock;
        crime[2].audArray[1] = sherlock2;
        crime[2].talk[0] = "Om det ikke minsandten er min ven Bib og hvem har du med der?";
        crime[2].talk[1] = "Hvad kan jeg hjælpe jer med?";

        crime[3].bubbleCount = 3;
        crime[3].talk = new string[3];
        crime[3].animNum = new int[3] { 1, 1, 1 };
        crime[3].animFin = new bool[3];
        crime[3].audFin = new bool[3];
        crime[3].audArray = new AudioSource[3];
        crime[3].audArray[0] = crimeAud;
        crime[3].audArray[1] = crimeAud2;
        crime[3].audArray[2] = crimeAud3;
        crime[3].talk[0] = "En hjælper.";
        crime[3].talk[1] = "Vi leder efter nøgledelene.";
        crime[3].talk[2] = "Har du en?";

        crime[4].bubbleOwner = 1;
        crime[4].bubbleCount = 4;
        crime[4].talk = new string[4];
        crime[4].animNum = new int[4] { 1, 1, 1, 1 };
        crime[4].animFin = new bool[4];
        crime[4].audArray = new AudioSource[4];
        crime[4].audFin = new bool[4];
        crime[4].audArray[0] = sherlocktwo;
        crime[4].audArray[1] = sherlocktwo2;
        crime[4].audArray[2] = sherlocktwo3;
        crime[4].audArray[3] = sherlocktwo4;
        crime[4].talk[0] = "Jeg havde en tidligere.";
        crime[4].talk[1] = "Indtil en eller anden stjal den!";
        crime[4].talk[2] = "Jeg kan til gengæld ikke finde ham. Han må være her et eller andet sted";
        crime[4].talk[3] = "Vil I hjælpe mig med at finde ud af hvor han er?";

        crime[5].bubbleCount = 1;
        crime[5].talk = new string[1];
        crime[5].animNum = new int[1] { 1 };
        crime[5].animFin = new bool[1];
        crime[5].audFin = new bool[1];
        crime[5].audArray = new AudioSource[1];
        crime[5].audArray[0] = crimetwo;
        crime[5].talk[0] = "Tjah der er vel ikke andet at gøre.";

        crime[6].bubbleOwner = 1;
        crime[6].bubbleCount = 5;
        crime[6].talk = new string[5];
        crime[6].animNum = new int[5] { 1, 1, 1, 1, 1 };
        crime[6].animFin = new bool[5];
        crime[6].audArray = new AudioSource[5];
        crime[6].audFin = new bool[5];
        crime[6].audArray[0] = sherlockthree;
        crime[6].audArray[1] = sherlockthree2;
        crime[6].audArray[2] = sherlockthree3;
        crime[6].audArray[3] = sherlockthree4;
        crime[6].audArray[4] = sherlockthree5;
        crime[6].talk[0] = "Udmærket";
        crime[6].talk[1] = "Jeg ved følgende om den mistænkte.";
        crime[6].talk[2] = "Mistænkte kan lide at holde sin hånd på maven.";
        crime[6].talk[3] = "Mistænkte kigger opad.";
        crime[6].talk[4] = "Mistænkte har orange sko på.";

        crime2[0].bubbleOwner = 1;
        crime2[0].bubbleCount = 3;
        crime2[0].talk = new string[3];
        crime2[0].animNum = new int[3] { 1, 1, 1 };
        crime2[0].animFin = new bool[3];
        crime2[0].audArray = new AudioSource[3];
        crime2[0].audFin = new bool[3];
        crime2[0].audArray[0] = sherlockfour;
        crime2[0].audArray[1] = sherlockfour2;
        crime2[0].audArray[2] = sherlockfour3;
        crime2[0].talk[0] = "Jeg vidste det var ham!";
        crime2[0].talk[1] = "Tusind tak de herrer.";
        crime2[0].talk[2] = "Her er nøgledelen.";

        crime2[1].bubbleCount = 2;
        crime2[1].talk = new string[2];
        crime2[1].animNum = new int[2] { 1, 1 };
        crime2[1].animFin = new bool[2];
        crime2[1].audFin = new bool[2];
        crime2[1].audArray = new AudioSource[2];
        crime2[1].audArray[0] = crimethree;
        crime2[1].audArray[1] = crimethree2;
        crime2[1].talk[0] = "Fantastisk!";
        crime2[1].talk[1] = "Lad os finde den næste trylledrik.";

        zone[0].bubbleCount = 1;  
        zone[0].audFin = new bool[1];
        zone[0].audArray = new AudioSource[1];
        zone[0].audArray[0] = zoneHelpAud;                              
        zone[0].animNum = new int[1] { 1 };
        zone[0].animFin = new bool[1];
        zone[0].talk = new string[1];
        zone[0].talk[0] = "";

        zone[1].bubbleCount = 1;      
        zone[1].audArray = new AudioSource[1];
        zone[1].audFin = new bool[1];
        zone[1].audArray[0] = arkHelp2;                               
        zone[1].animNum = new int[1] { 1 };
        zone[1].animFin = new bool[1];
        zone[1].talk = new string[1];
        zone[1].talk[0] = "";

        zone[2].bubbleCount = 1;    
        zone[2].audArray = new AudioSource[1];
        zone[2].audFin = new bool[1];
        zone[2].audArray[0] = arkHelp3;                              
        zone[2].animNum = new int[1] { 1 };
        zone[2].animFin = new bool[1];
        zone[2].talk = new string[1];
        zone[2].talk[0] = "";

        zone[3].bubbleOwner = 1;
        zone[3].bubbleCount = 1;
        zone[3].talk = new string[1];
        zone[3].animNum = new int[1] { 1 };
        zone[3].animFin = new bool[1];
        zone[3].audArray = new AudioSource[1];
        zone[3].audArray[0] = kagemand;
        zone[3].audFin = new bool[1];
        zone[3].talk[0] = "Hej, h-h-hvem er du?";

        zone[4].bubbleCount = 3;
        zone[4].talk = new string[3];
        zone[4].animNum = new int[3] { 1, 1, 1 };
        zone[4].animFin = new bool[3];
        zone[4].audFin = new bool[3];
        zone[4].audArray = new AudioSource[3];
        zone[4].audArray[0] = zoneAud;
        zone[4].audArray[1] = zoneAud2;
        zone[4].audArray[2] = zoneAud3;
        zone[4].talk[0] = "Nej, dig kender jeg ikke.";
        zone[4].talk[1] = "Men mit navn er Bib.";
        zone[4].talk[2] = "Har du set en nøgledel heromkring?";

        zone[5].bubbleOwner = 1;
        zone[5].bubbleCount = 3;
        zone[5].talk = new string[3];
        zone[5].animNum = new int[3] { 1, 1, 1 };
        zone[5].animFin = new bool[3];
        zone[5].audArray = new AudioSource[3];
        zone[5].audFin = new bool[3];
        zone[5].audArray[0] = kagemandtwo;
        zone[5].audArray[1] = kagemandtwo2;
        zone[5].audArray[2] = kagemandtwo3;
        zone[5].talk[0] = "Mit navn er Kagemand.";
        zone[5].talk[1] = "Je-je-jeg fandt en der lå og flød.";
        zone[5].talk[2] = "Hvad rager det dig?";

        zone[6].bubbleCount = 2;
        zone[6].talk = new string[2];
        zone[6].animNum = new int[2] { 1, 1 };
        zone[6].animFin = new bool[2];
        zone[6].audFin = new bool[2];
        zone[6].audArray = new AudioSource[2];
        zone[6].audArray[0] = zonetwo;
        zone[6].audArray[1] = zonetwo2;
        zone[6].talk[0] = "Vi skal bruge den til at åbne min kiste.";
        zone[6].talk[1] = "Må vi få den?";

        zone[7].bubbleOwner = 1;
        zone[7].bubbleCount = 4;
        zone[7].talk = new string[4];
        zone[7].animNum = new int[4] { 1, 1, 1, 1 };
        zone[7].animFin = new bool[4];
        zone[7].audArray = new AudioSource[4];
        zone[7].audFin = new bool[4];
        zone[7].audArray[0] = kagemandthree;
        zone[7].audArray[1] = kagemandthree2;
        zone[7].audArray[2] = kagemandthree3;
        zone[7].audArray[3] = kagemandthree4;
        zone[7].talk[0] = "Må-må-måske...";
        zone[7].talk[1] = "Jeg er på vej hen til julemanden men på vejen tabte jeg en af mine knapper.";
        zone[7].talk[2] = "Den trillede ned ad trapperne og jeg tør ikke gå derned.";
        zone[7].talk[3] = "Hvis i hjælper mig med at finde den må i få jeres del.";

        zone[8].bubbleCount = 1;
        zone[8].talk = new string[1];
        zone[8].animNum = new int[1] { 1 };
        zone[8].animFin = new bool[1];
        zone[8].audFin = new bool[1];
        zone[8].audArray = new AudioSource[1];
        zone[8].audArray[0] = zonethree;
        zone[8].talk[0] = "Tjah, der er vist ikke andet at gøre.";

        zone2[0].bubbleOwner = 1;
        zone2[0].bubbleCount = 1;
        zone2[0].talk = new string[1];
        zone2[0].animNum = new int[1] { 1 };
        zone2[0].animFin = new bool[1];
        zone2[0].audArray = new AudioSource[1];
        zone2[0].audFin = new bool[1];
        zone2[0].audArray[0] = kagemandfour;
        zone2[0].talk[0] = "T-t-tusind tak. Her er jeres nøglestykke.";

        zone2[1].bubbleCount = 3;
        zone2[1].talk = new string[3];
        zone2[1].animNum = new int[3] { 1, 1, 1 };
        zone2[1].animFin = new bool[3];
        zone2[1].audFin = new bool[3];
        zone2[1].audArray = new AudioSource[3];
        zone2[1].audArray[0] = zonefour;
        zone2[1].audArray[1] = arcadethree2;
        zone2[1].audArray[2] = arcadethree3;
        zone2[1].talk[0] = "Jamen det er jo fantastisk!";
        zone2[1].talk[1] = "Lad os kigge lidt rundt her i området.";
        zone2[1].talk[2] = "Måske vi finder en trylledrik som kan lede os videre.";

        zone3.bubbleCount = 2;
        zone3.talk = new string[2];
        zone3.animNum = new int[2] { 1, 1, };
        zone3.animFin = new bool[2];
        zone3.audFin = new bool[2];
        zone3.audArray = new AudioSource[2];
        zone3.audArray[0] = zoneButton;
        zone3.audArray[1] = zoneButton2;
        zone3.talk[0] = "Super!";
        zone3.talk[1] = "Lad os gå tilbage til kagemand for at få vores del.";

        outro.bubbleCount = 4;
        outro.talk = new string[4];
        outro.animNum = new int[4] { 1, 1, 1, 1 };
        outro.animFin = new bool[4];
        outro.audFin = new bool[4];
        outro.audArray = new AudioSource[4];
        outro.audArray[0] = outroAud;
        outro.audArray[1] = outroAud2;
        outro.audArray[2] = outroAud3;
        outro.audArray[3] = outroAud4;
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
                if (inter.animFin[pressCount] == false)                 //If the animation of the talk hasn't already played
                {
                    if (inter.animNum[pressCount] == 1)                 //If animation number of the current talk is equal to 1, play the talking animation.
                    {
                        //Pointing at treasure animation is triggered once the next button has been pressed three times
                        //Animation for the conversation of the introduction instance of the struct
                        if (inter.current == intro.current) {
                            if (pressCount == 3)
                            {
                                companion.GetComponent<Animator>().Play("PointingAtTreasure_Animation");
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                            }
                            else
                            {
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.2f);
                                companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                            }                      
                        } 
                        //Thinking animation once the first potion is scanned
                        //Animation for the conversation of the tutorial instance of the struct
                        if (inter.current == introContinued2.current) 
                        {
                            if (pressCount == 0)
                            {
                                companion.GetComponent<Animator>().Play("Thinking_Animation");
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                                companion.GetComponent<Animator>().SetBool("isCompanionThinking", true);
                            }
                            else
                            {
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.2f);
                                companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                            }
                        }
                        inter.animFin[pressCount] = true;
                    }
                }
            }
        }
    }

    //Function that plays animation for the in-game characters at the arcade section
    public void playCompAnimation(interaction[] interArray)
    {
        //Animation for the pre minigame conversation in the arcade section
        if (interArray == arkade)
        {
            //For loop that goes through each dialogue that the conversation consists of
            for (int i = 0; i < interArray.Length; i++)
            {
                //For each loop that goes through each line in the dialogue
                foreach (string str in interArray[i].talk)
                {
                    //Check whether the conversation instance has been triggered from scanning its respective marker
                    if (interArray[i].current == true)
                    {
                        //Check if no animation has been played yet
                        if (interArray[i].animFin[pressCount] == false && interArray[i].animNum[pressCount] == 1)
                        {
                            //Play an animation for the comapnion at a specific dialogue line
                            if (interArray[i].current == arkade[2].current)
                            {
                                //Determines what animation state to play, its speed, and transition
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.2f);
                                companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                            }

                            if (interArray[i].current == arkade[4].current)
                            {
                                if (pressCount == 0)
                                {
                                    companion.GetComponent<Animator>().Play("Thinking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 0.7f);
                                    companion.GetComponent<Animator>().SetBool("isCompanionThinking", true);
                                }
                                else
                                {
                                    companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.2f);
                                    companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                    companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                                }

                                Debug.Log("This is arcade point number " + i);
                            }

                            //Play an animation for the arcade machine at a specific dialogue line
                            if (interArray[i].current == arkade[1].current || interArray[i].current == arkade[3].current)
                            {
                                arcade_Machine.GetComponent<Animator>().SetFloat("arcadeAnimationSpeed", 0.7f);
                                arcade_Machine.GetComponent<Animator>().Play("Arcade_Talking", -1, 0f);
                                arcade_Machine.GetComponent<Animator>().SetBool("state_ArcadeIdle", true);

                                Debug.Log("This is arcade point number " + i);
                            }

                            //Set the animation index to true, to ensure that it is not played again
                            interArray[i].animFin[pressCount] = true;
                        }
                    }
                }
            }
        }

        //Animation the post minigame conversation in the arcade section. 
        //The procedure of when and how the animation is triggered is the same as the code from line 923-981
        if (interArray == arkade2)
        {
            for (int i = 0; i < interArray.Length; i++)
            {
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
            }
        }
    }

    //Function that plays animation for the in-game characters at the zone section
    public void zoneSectionAnimation(interaction[] interArray)
    {
        //Animation the pre minigame conversation in the zone section
        //The procedure of when and how the animation is triggered is the same as the code from line 923-981
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
                            if (interArray[i].current == zone[4].current || interArray[i].current == zone[6].current || interArray[i].current == zone[8].current)
                            {
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.2f);
                                companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                            }

                            if (interArray[i].current == zone[3].current || interArray[i].current == zone[5].current || interArray[i].current == zone[7].current)
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
        //The procedure of when and how the animation is triggered is the same as the code from line 923-981
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

    //Function that plays animation for the in-game characters at the crime section
    public void crimeSectionAnimation(interaction[] interArray)
    {
        //Animation for the pre minigame conversation in the crime section
        //The procedure of when and how the animation is triggered is the same as the code from line 923-981
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
                            if (interArray[i].current == crime[2].current)
                            {
                                detective.GetComponent<Animator>().Play("detectiveIdleTalk", -1, 0f);
                                detective.GetComponent<Animator>().SetFloat("detectiveSpeed", 0.6f);
                                detective.GetComponent<Animator>().SetBool("detective_talkToIdle", true);
                            }

                            if (interArray[i].current == crime[3].current || interArray[i].current == crime[5].current)
                            {
                                companion.GetComponent<Animator>().SetFloat("companionSpeed", 1.2f);
                                companion.GetComponent<Animator>().Play("Talking_Animation", -1, 0f);
                                companion.GetComponent<Animator>().SetBool("isCompanionTalking", true);
                            }

                            if (interArray[i].current == crime[4].current)
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

                            if (interArray[i].current == crime[6].current)
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

        //Animation for the post minigame conversation in the crime section
        //The procedure of when and how the animation is triggered is the same as the code from line 923-981
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

    //Function that plays animation for the in-game characters once all key fragments have been collected
    public void gameOver(interaction inter)
    {
        //The procedure of when and how the animation is triggered is the same as the code from line 923-981
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