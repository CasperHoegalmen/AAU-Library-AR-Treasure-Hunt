using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour {

    public GameObject endScreen;
    float counter = 2;

	// Use this for initialization
	void Start () {
        endScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if(GameObject.Find("Treasure").GetComponent<MiniGameChest>().isChestGameCompleted == true &&
           GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == true &&
           GameObject.Find("Detective").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == true &&
           GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == true &&
           GameObject.Find("Treasure").GetComponent<MiniGameEnding>().isGameCompleted == true &&
           GameObject.Find("BubbleButton").GetComponent<textBubble>().outro.current == false)
        {
            if ((Mathf.RoundToInt(counter -= Time.deltaTime)) == 0)
            {
                endScreen.SetActive(true);
            }
        }


    }
}
