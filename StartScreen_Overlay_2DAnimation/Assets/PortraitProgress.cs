using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitProgress : MonoBehaviour {

    public RawImage PortraitChest, PortraitGinger, PortraitCulprit, PortraitArcade;
    public byte opaqueAlpha = 175;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.Find("Chest").GetComponent<MiniGameChest>().isChestGameCompleted == true)
        {
            PortraitChest.gameObject.SetActive(true);
            PortraitChest.color += new Color(0, 0, 0, opaqueAlpha);
        }


        if (GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == true)
        {
            PortraitGinger.gameObject.SetActive(true);
            PortraitGinger.color += new Color(0, 0, 0, opaqueAlpha);
        }


        if (GameObject.Find("Zombie").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == true)
        {
            PortraitCulprit.gameObject.SetActive(true);
            PortraitCulprit.color += new Color(0, 0, 0, opaqueAlpha);
        }


        if (GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == true)
        {
            PortraitArcade.gameObject.SetActive(true);
            PortraitArcade.color += new Color(0, 0, 0, opaqueAlpha);
        }

    }
}
