using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class miniGameArcade : MonoBehaviour {

    public GameObject miniGameUIButton1;
    public GameObject miniGameUIButton2;
    public GameObject miniGameUIButton3;
    public GameObject miniGameUIText;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        miniGameUIButton1.SetActive(false);
        miniGameUIButton2.SetActive(false);
        miniGameUIButton3.SetActive(false);
        miniGameUIText.SetActive(false);

        if (GameObject.Find("ImageTarget").GetComponent<DefaultTrackableEventHandler>().startMinigame == true)
        {
            miniGameUIButton1.SetActive(true);
            miniGameUIButton2.SetActive(true);
            miniGameUIButton3.SetActive(true);
            miniGameUIText.SetActive(true);
        }
	}
}
