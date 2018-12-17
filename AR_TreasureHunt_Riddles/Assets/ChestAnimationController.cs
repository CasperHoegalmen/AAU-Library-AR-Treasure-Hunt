using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimationController : MonoBehaviour
{
    public GameObject chest;
    public bool idleToShake = false, shakeToOpen = false, openToClose = false;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Boolean variables that are set in the Unity Animation controller.
        if (idleToShake == true)
        {
            chest.GetComponent<Animator>().SetBool("isIdle", true);
        }

        if (shakeToOpen == true)
        {
            chest.GetComponent<Animator>().SetBool("isShake", true);
        }

        if (openToClose == true)
        {
            chest.GetComponent<Animator>().SetBool("isOpen", true);
        }

        
    }
}
