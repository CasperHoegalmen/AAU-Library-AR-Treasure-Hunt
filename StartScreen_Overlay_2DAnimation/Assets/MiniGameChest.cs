using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameChest : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("chest").GetComponent<DefaultTrackableEventHandler>().startMinigameChest == true)
        {
            GameObject.Find("chest").GetComponent<ChestAnimationController>().idleToShake = true;
        }
    }
}
