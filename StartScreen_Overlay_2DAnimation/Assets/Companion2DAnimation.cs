using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion2DAnimation : MonoBehaviour {

    public Animator companion;

	// Use this for initialization
	void Start () {
        companion = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            companion.Play("Talking_Animation");
            companion.SetBool("isHappyCompanionIdle", true);
        }
	}
}
