using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MiniGameArcade : MonoBehaviour
{
    // The concept and the structure explained in this script, is identical to those for the other mini-games.

    // public Button and GameObject variables are declared. Those are defined in the Unity inspector window. They are used as UI - elements for the game.
    // public Boolean variables are declared and some are instanciated. They are used to check different states of the game.
    public Button miniGameUIButton1, miniGameUIButton2, miniGameUIButton3, restartGameUIButton;
    public GameObject miniGameUIQuestionText, miniGameUIWrongAnswerText, miniGameUIRightAnswerText, miniGameUIFinishedMessage;
    public Boolean isButtonPressed, isArcadeMiniGameCompleted, isGamePartiallyComplete = false, finishedItOnce = false;


    // Use this for initialization
    void Start()
    {
        // Since this script is executed at runtime and the game is not meant to play yet,
        // all UI - elements are initially set to false.
        miniGameUIButton1.gameObject.SetActive(false);
        miniGameUIButton2.gameObject.SetActive(false);
        miniGameUIButton3.gameObject.SetActive(false);
        restartGameUIButton.gameObject.SetActive(false);
        miniGameUIQuestionText.SetActive(false);
        miniGameUIWrongAnswerText.SetActive(false);
        miniGameUIRightAnswerText.SetActive(false);
        miniGameUIFinishedMessage.SetActive(false);
        isButtonPressed = false;
        isArcadeMiniGameCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // The condition in the if-statement checks that none of the prior conversations to the game are still runnning.
        // Also, it checks the Boolean variable "startMiniGameArcade" that is initialized in the corresponding DefaultTrackableEventHandler. 
        // The Boolean variable is set to false by the TrackableEventHandler, once the marker for the section is in the field of view of the camera.
        // Furthermore, the condition also ensures that the game has not been played yet, by checking if the "isArcadeMiniGameCompleted" Boolean is false.
        if (GameObject.Find("Startscreen").GetComponent<textBubble>().arkade[0].current == false &&
            GameObject.Find("Startscreen").GetComponent<textBubble>().arkade[1].current == false &&
            GameObject.Find("Startscreen").GetComponent<textBubble>().arkade[2].current == false &&
            GameObject.Find("Startscreen").GetComponent<textBubble>().arkade[3].current == false &&
            GameObject.Find("Startscreen").GetComponent<textBubble>().arkade[4].current == false &&
            GameObject.Find("ImageTargetArcade").GetComponent<ArcadeDefaultTrackableEventHandler>().startMinigameArcade == true &&  
            isButtonPressed == false && 
            isArcadeMiniGameCompleted == false)
        {
            // When the condition in the if-statement is true, the UI-elements for the game are activated.
            miniGameUIButton1.gameObject.SetActive(true);
            miniGameUIButton2.gameObject.SetActive(true);
            miniGameUIButton3.gameObject.SetActive(true);
            miniGameUIQuestionText.SetActive(true);
            finishedItOnce = true;

            // The buttons are assigned onClikListeners that are executed once the buttons are pressed (clicked).
            miniGameUIButton1.onClick.AddListener(wrongButton1);
            miniGameUIButton2.onClick.AddListener(wrongButton2);
            miniGameUIButton3.onClick.AddListener(correctButton);

        }

        // This condition is true, once the first part of the game has been succesfully completed and the marker is still in the field of view of the camera.
        // When the first part of the game has been completed, the "isGamePartiallyComplete" Boolean variable is set to true. This is done in the corresponding TrackableEventHandler.
        if(GameObject.Find("ImageTargetArcade").GetComponent<ArcadeDefaultTrackableEventHandler>().startMinigameArcade == true &&
            isGamePartiallyComplete == true &&
            isButtonPressed == false &&
            isArcadeMiniGameCompleted == false)
        {
            miniGameUIButton1.gameObject.SetActive(true);
            miniGameUIButton2.gameObject.SetActive(true);
            miniGameUIButton3.gameObject.SetActive(true);
            miniGameUIQuestionText.SetActive(true);

            miniGameUIButton1.onClick.AddListener(wrongButton1);
            miniGameUIButton2.onClick.AddListener(wrongButton2);
            miniGameUIButton3.onClick.AddListener(correctButton);
        }



        if (GameObject.Find("ImageTargetArcade").GetComponent<ArcadeDefaultTrackableEventHandler>().startMinigameArcade == true && isButtonPressed == false && isArcadeMiniGameCompleted == true)
        {
            miniGameUIFinishedMessage.SetActive(true);
        }


        if (GameObject.Find("ImageTargetArcade").GetComponent<ArcadeDefaultTrackableEventHandler>().startMinigameArcade == false)
        {
            miniGameUIButton1.gameObject.SetActive(false);
            miniGameUIButton2.gameObject.SetActive(false);
            miniGameUIButton3.gameObject.SetActive(false);
            miniGameUIQuestionText.SetActive(false);
            miniGameUIFinishedMessage.SetActive(false);
        }

    }


    // This function is given as an argument to the listener that is attached to the button for the right answer - line 77.
    // The function sets the UI - elements for all buttons to false, except the one for the chosen answer.
    // The RGB-Channel for the image assigned to the chosen button is manipulated to appear green. This is doe in line 111.
    // When the right button has been pressed this function progresses the game, by setting the next speech bubble in the textBubble script to true.
    void correctButton()
    {
        isArcadeMiniGameCompleted = true;

        miniGameUIButton1.gameObject.SetActive(false);
        miniGameUIButton2.gameObject.SetActive(false);
        miniGameUIButton3.gameObject.SetActive(true);

        miniGameUIButton3.GetComponent<Image>().color = Color.green;

        miniGameUIQuestionText.SetActive(false);

        GameObject.Find("Startscreen").GetComponent<textBubble>().arkade2[0].current = true;

    }


    // This function is given as an argument to the Listener attached to the button for the first wrong answer - line 75.
    // The function sets the UI - elements for all buttons to false, except the one for the chosen answer - line 130 to 132.
    // The RGB-Channel for the image assigned to the chosen button is manipulated to appear red. This is doe in line 134.
    // The text for the chosen, wrong answer is shown and the text for the question is deactivated - line 136 to 137.
    // A button to retry the question is activated in line 139. A Listener function has been assigned to the button - line 140.
    void wrongButton1()
    {
        isButtonPressed = true;

        miniGameUIButton1.gameObject.SetActive(true);
        miniGameUIButton2.gameObject.SetActive(false);
        miniGameUIButton3.gameObject.SetActive(false);

        miniGameUIButton1.GetComponent<Image>().color = Color.red;

        miniGameUIQuestionText.SetActive(false);
        miniGameUIWrongAnswerText.SetActive(true);

        restartGameUIButton.gameObject.SetActive(true);
        restartGameUIButton.onClick.AddListener(restartGame);

    }


    // This function is given as an argument to the Listener attached to the button for the second wrong answer - line 76.
    // The function sets the UI - elements for all buttons to false, except the one for the chosen answer - line 154 to 156.
    // The RGB-Channel for the image assigned to the chosen button is manipulated to appear red. This is doe in line 158.
    // The text for the chosen, wrong answer is shown and the text for the question is deactivated - line 160 to 161.
    // A button to retry the question is activated in line 163. A Listener function has been assigned to the button - line 164.
    void wrongButton2()
    {
        isButtonPressed = true;

        miniGameUIButton1.gameObject.SetActive(false);
        miniGameUIButton2.gameObject.SetActive(true);
        miniGameUIButton3.gameObject.SetActive(false);

        miniGameUIButton2.GetComponent<Image>().color = Color.red;

        miniGameUIQuestionText.SetActive(false);
        miniGameUIWrongAnswerText.SetActive(true);

        restartGameUIButton.gameObject.SetActive(true);
        restartGameUIButton.onClick.AddListener(restartGame);
    }


    // This Listener function is given as an argument to the restart button, that is set active when one of the two buttons for the wrong answers has been pressed.
    // This function sets the UI objects and colors of this game to its initial state. This is done to let the player retry the game.
    void restartGame()
    {
        miniGameUIButton1.GetComponent<Image>().color = Color.white;
        miniGameUIButton2.GetComponent<Image>().color = Color.white;

        miniGameUIButton1.gameObject.SetActive(true);
        miniGameUIButton2.gameObject.SetActive(true);
        miniGameUIButton3.gameObject.SetActive(true);

        miniGameUIQuestionText.SetActive(true);
        miniGameUIWrongAnswerText.SetActive(false);
        miniGameUIRightAnswerText.SetActive(false);

        restartGameUIButton.gameObject.SetActive(false);

        isButtonPressed = false;
    }
}
