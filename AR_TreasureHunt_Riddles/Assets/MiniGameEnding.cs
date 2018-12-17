using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameEnding : MonoBehaviour {

    //Initialization of Boolean variable to indicate whether the game has been completed.
    public bool isGameCompleted= false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //The mini game is started, once the Boolean variable in the ChestDefaultTrackableEventHandler is set to true.
        if(GameObject.Find("ImageTargetChest").GetComponent<ChestDefaultTrackableEventHandler>().startMinigameEnding == true)
        {
            //Boolean variables in the chest animation controller are set to true, to trigger animations for the 3D model of the treasure chest.
            GameObject.Find("Treasure").GetComponent<ChestAnimationController>().idleToShake = true;
            GameObject.Find("Treasure").GetComponent<ChestAnimationController>().shakeToOpen = true;

            //The variable to indicate whether the game has been completed, is set to true.
            isGameCompleted = true;
        }
		
	}
}
