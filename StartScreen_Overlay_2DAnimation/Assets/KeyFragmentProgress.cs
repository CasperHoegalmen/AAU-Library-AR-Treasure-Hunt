using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyFragmentProgress : MonoBehaviour {

    public RawImage keyFragmentOne;
    public RawImage keyFragmentTwo;
    public RawImage keyFragmentThree;
    public int keyFragmentCounter;
    public byte opaqueAlpha = 175;

	// Use this for initialization
	void Start () {
        keyFragmentCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(GameObject.Find("arcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == true)
        {
            keyFragmentOne.gameObject.SetActive(true);
            keyFragmentOne.color += new Color(0, 0, 0, opaqueAlpha);
            
        }
        if(GameObject.Find("Zombie").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == true) //Add Boolean for second Game here
        {
            keyFragmentTwo.gameObject.SetActive(true);
            keyFragmentTwo.color += new Color(0, 0, 0, opaqueAlpha);
        }
        if(keyFragmentCounter == 3) //Add Boolean for Third Game here
        {
            keyFragmentThree.gameObject.SetActive(true);
            keyFragmentThree.color += new Color(0, 0, 0, opaqueAlpha);
            //All Fragments are collected -> Game is won.
        }
	}
}
