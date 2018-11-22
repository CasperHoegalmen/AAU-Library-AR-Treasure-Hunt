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

        if (isChestGameCompleted == false && 
            GameObject.Find("ImageTargetChest").GetComponent<ChestDefaultTrackableEventHandler>().startMinigameChest == true)
        {
            GameObject.Find("Chest").GetComponent<ChestAnimationController>().idleToShake = true;
            GameObject.Find("BubbleButton").GetComponent<textBubble>().pressCount = 0;
            GameObject.Find("BubbleButton").GetComponent<textBubble>().introContinued.current = true;
            isChestGameCompleted = true;
        }

        
    }
}
