using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitProgress : MonoBehaviour {

    public RawImage ProgressionImageChest, ProgressionImageGinger, ProgressionImageCulprit, ProgressionImageArcade;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.Find("Treasure").GetComponent<MiniGameChest>().isChestGameCompleted == true)
        {
            ProgressionImageChest.GetComponent<RawImage>().color = Color.green;
        }


        if (GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == true)
        {
            ProgressionImageGinger.GetComponent<RawImage>().color = Color.green;
        }


        if (GameObject.Find("Detective").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == true)
        {
            ProgressionImageCulprit.GetComponent<RawImage>().color = Color.green;
        }


        if (GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == true)
        {
            ProgressionImageArcade.GetComponent<RawImage>().color = Color.green;
        }

    }
}
