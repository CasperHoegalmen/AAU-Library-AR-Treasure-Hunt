using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieAnimControl : MonoBehaviour {

    public GameObject cookieMan;
    private readonly bool cookieNothing = true, cookieIdle = true, cookieWave = true;
 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (cookieNothing == true && Input.GetKeyDown("5")) {
            cookieMan.GetComponent<Animator>().SetBool("isCookieIdle", true);
            cookieMan.GetComponent<Animator>().SetFloat("cookieSpeed", 0.7f);
        }
        if (cookieIdle == true && Input.GetKeyDown("6"))
        {
            cookieMan.GetComponent<Animator>().SetBool("isCookieWave", true);
            cookieMan.GetComponent<Animator>().SetFloat("cookieSpeed", 0.6f);
        }
        if (cookieWave == true && Input.GetKeyDown("7"))
        {
            cookieMan.GetComponent<Animator>().SetBool("isCookieJump", true);
            cookieMan.GetComponent<Animator>().SetFloat("cookieSpeed", 1.5f);
        }
    }
}
