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
            GameObject.Find("Treasure").GetComponent<ChestAnimationController>().idleToShake = true;
            GameObject.Find("Startscreen").GetComponent<textBubble>().pressCount = 0;
            GameObject.Find("Startscreen").GetComponent<textBubble>().introContinued2.current = true;
            isChestGameCompleted = true;
        }
    }
}
