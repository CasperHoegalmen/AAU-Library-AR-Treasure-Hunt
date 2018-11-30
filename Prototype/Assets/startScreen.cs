using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startScreen : MonoBehaviour {
    public GameObject button;
    public GameObject image1;
    public GameObject image2;
    public bool startTheGame = false;


public void buttonEvent()
    {
        if (button.activeSelf)
        {
            button.SetActive(false);
            image1.SetActive(false);
            image2.SetActive(false);
            startTheGame = true;
        }
        else
            button.SetActive(true);
    }
}
