using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimationController : MonoBehaviour
{
    public GameObject chest;
    private readonly bool idleToShake = true, shakeToOpen = true, openToClose = true;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (idleToShake == true && Input.GetKeyDown("1"))
        {
            chest.GetComponent<Animator>().SetBool("isIdle", true);
        }

        if (shakeToOpen == true && Input.GetKeyDown("2"))
        {
            chest.GetComponent<Animator>().SetBool("isShake", true);
        }

        if (openToClose == true && Input.GetKeyDown("3"))
        {
            chest.GetComponent<Animator>().SetBool("isOpen", true);
        }

        
    }
}
