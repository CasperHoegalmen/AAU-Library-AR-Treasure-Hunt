using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameEnding : MonoBehaviour {

    public bool isGameCompleted= false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GameObject.Find("ImageTargetChest").GetComponent<ChestDefaultTrackableEventHandler>().startMinigameEnding == true)
        {
            GameObject.Find("Chest").GetComponent<ChestAnimationController>().idleToShake = true;
            GameObject.Find("Chest").GetComponent<ChestAnimationController>().shakeToOpen = true;
            isGameCompleted = true;
        }
		
	}
}
