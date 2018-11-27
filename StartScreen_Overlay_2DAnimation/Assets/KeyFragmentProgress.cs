using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyFragmentProgress : MonoBehaviour {

    public RawImage keyFragmentOne;
    public RawImage keyFragmentTwo;
    public RawImage keyFragmentThree;
    public RawImage keyFragmentFour;
    public int keyFragmentCounter;
    public byte opaqueAlpha = 175;
    public GameObject arrowKey;
    public GameObject arrowChest;
    int counter = 0;

    // Use this for initialization
    void Start () {
        keyFragmentCounter = 0;
        arrowKey.SetActive(false);
        arrowChest.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.Find("Treasure").GetComponent<MiniGameChest>().isChestGameCompleted == true)
        {
            keyFragmentOne.gameObject.SetActive(true);
            keyFragmentOne.color += new Color(0, 0, 0, opaqueAlpha);

            blink(arrowKey);

            //InvokeRepeating("blink", 1, 1);
            
        }
        

        
        if (GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == true)
        {
            keyFragmentTwo.gameObject.SetActive(true);
            keyFragmentTwo.color += new Color(0, 0, 0, opaqueAlpha);  
        }
        

        if(GameObject.Find("Detective").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == true)
        {
            keyFragmentThree.gameObject.SetActive(true);
            keyFragmentThree.color += new Color(0, 0, 0, opaqueAlpha);
        }

        
        if(GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == true) 
        {
            keyFragmentFour.gameObject.SetActive(true);
            keyFragmentFour.color += new Color(0, 0, 0, opaqueAlpha);
        }
        
	}

    public void blink(GameObject toBlink)
    {
        counter = 0;

        while (counter < 500000)
        {
            toBlink.SetActive(true);
            counter++;
            Debug.Log(counter);
        }

        if (counter >= 500000)
        {
            toBlink.SetActive(false);
        }




        
        //toBlink.SetActive(false);

        


    }
}
