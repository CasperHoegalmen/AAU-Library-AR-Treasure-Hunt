using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MiniGameArcade : MonoBehaviour
{

    public Button miniGameUIButton1, miniGameUIButton2, miniGameUIButton3, restartGameUIButton;
    public GameObject miniGameUIQuestionText, miniGameUIWrongAnswerText, miniGameUIRightAnswerText, miniGameUIFinishedMessage;
    public Boolean isButtonPressed, isArcadeMiniGameCompleted;


    // Use this for initialization
    void Start()
    {
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


        if (GameObject.Find("ImageTarget1").GetComponent<DefaultTrackableEventHandler>().startMinigameArcade == true && isButtonPressed == false && isArcadeMiniGameCompleted == false)
        {
            miniGameUIButton1.gameObject.SetActive(true);
            miniGameUIButton2.gameObject.SetActive(true);
            miniGameUIButton3.gameObject.SetActive(true);
            miniGameUIQuestionText.SetActive(true);

            miniGameUIButton1.onClick.AddListener(wrongButton1);
            miniGameUIButton2.onClick.AddListener(wrongButton2);
            miniGameUIButton3.onClick.AddListener(correctButton);

        }

        if (GameObject.Find("ImageTarget1").GetComponent<DefaultTrackableEventHandler>().startMinigameArcade == true && isButtonPressed == false && isArcadeMiniGameCompleted == true)
        {
            miniGameUIFinishedMessage.SetActive(true);
        }


        if (GameObject.Find("ImageTarget1").GetComponent<DefaultTrackableEventHandler>().startMinigameArcade == false)
        {
            miniGameUIButton1.gameObject.SetActive(false);
            miniGameUIButton2.gameObject.SetActive(false);
            miniGameUIButton3.gameObject.SetActive(false);
            miniGameUIQuestionText.SetActive(false);
            miniGameUIFinishedMessage.SetActive(false);
        }

    }



    void correctButton()
    {
        isArcadeMiniGameCompleted = true;

        miniGameUIButton1.gameObject.SetActive(false);
        miniGameUIButton2.gameObject.SetActive(false);
        miniGameUIButton3.gameObject.SetActive(true);

        miniGameUIButton3.GetComponent<Image>().color = Color.green;

        miniGameUIQuestionText.SetActive(false);

    }

    void wrongButton1()
    {
        //Deactivate the Two Answers you didn't choose
        //Show the chosen Button in Red
        //"Press On the Screen to try again"
        //Restart Game

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
