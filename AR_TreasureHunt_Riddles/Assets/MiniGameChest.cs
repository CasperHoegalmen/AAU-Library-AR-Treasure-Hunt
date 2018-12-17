using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameChest : MonoBehaviour
{
    public bool isChestGameCompleted = false;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // The mini game is started, once the Boolean variable in the ChestDefaultTrackableEventHandler is set to true.
        // It is also checked that the game that is to played, has not been played yet.
        if (isChestGameCompleted == false && 
            GameObject.Find("ImageTargetChest").GetComponent<ChestDefaultTrackableEventHandler>().startMinigameChest == true)
        {
            // The treasure is animated using the ChestAnimationController.
            // The right speech bubbles are set.
            GameObject.Find("Treasure").GetComponent<ChestAnimationController>().idleToShake = true;
            GameObject.Find("Startscreen").GetComponent<textBubble>().introContinued2.current = true;
            isChestGameCompleted = true;
        }
    }
}
