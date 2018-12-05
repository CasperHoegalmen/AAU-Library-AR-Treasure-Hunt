using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameGingerbread : MonoBehaviour {

    public bool isGingerbreadMiniGameCompleted = false;
    //public bool isGingerbreadMiniGamePartiallyCompleted = false;
    public GameObject GingerbreadWithButton;
    public GameObject GingerbreadWithoutButton;
    public bool gingerbreadButtonFound = false;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if(GameObject.Find("ImageTargetGinger").GetComponent<GingerDefaultTrackableEventHandler>().startMinigameGingerbread == true &&
            isGingerbreadMiniGameCompleted == false)
        {
            GingerbreadWithButton.SetActive(false);
            GingerbreadWithoutButton.SetActive(true);
        }

        
        if (GameObject.Find("ImageTargetGinger").GetComponent<GingerDefaultTrackableEventHandler>().startMinigameGingerbread == true &&
            gingerbreadButtonFound == true &&
            isGingerbreadMiniGameCompleted == false)
        {
            GingerbreadWithButton.SetActive(true);
            GingerbreadWithoutButton.SetActive(false);
            isGingerbreadMiniGameCompleted = true;
        }
        


    }
}
