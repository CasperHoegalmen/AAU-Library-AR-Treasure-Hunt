/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// 
/// Changes made to this file could be overwritten when upgrading the Vuforia version. 
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class ChestDefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    //public Transform overlayIconOneOpaque;
    //public Transform overlayIconeOneTransparent;
    //public GameObject companion, companionDifferent;
    //public GameObject navigationArrow;

    public static DefaultTrackableEventHandler main;

    public bool startMinigameChest = false;
    public bool startMinigameEnding = false;


    // public UnityEngine.UI.Image overlayIconeOne;
    public bool isFound;


    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        
        if (GameObject.Find("Treasure").GetComponent<MiniGameChest>().isChestGameCompleted == false &&
            GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == false &&
            GameObject.Find("Detective").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == false &&
            GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == false)
        {
            startMinigameChest = true; //Starts The Mini Game for The Chest in MiniGameChest.cs 
        }
        

        if (GameObject.Find("Treasure").GetComponent<MiniGameChest>().isChestGameCompleted == true &&
            GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == true &&
            GameObject.Find("Detective").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == true &&
            GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == true)
        {
            GameObject.Find("BubbleButton").GetComponent<textBubble>().pressCount = 0;
            GameObject.Find("BubbleButton").GetComponent<textBubble>().outro.current = true;
            startMinigameEnding = true;
        }

        /*
        if (GameObject.Find("Treasure").GetComponent<MiniGameChest>().isChestGameCompleted == true &&
           GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == true &&
           GameObject.Find("Detective").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == true &&
           GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == true)
        {
            startMinigameEnding = true; //Starts the ending sequence (Animation + Text) in MiniGameEnding.cs
            GameObject.Find("BubbleButton").GetComponent<textBubble>().pressCount = 0;
            GameObject.Find("BubbleButton").GetComponent<textBubble>().outro.current = true; //Maybe Set this based on previous condition
        }
        */



    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;

        // overlayIconeOne = GetComponent<UnityEngine.UI.Image>();
        // var tempColor = overlayIconeOne.color;
        // tempColor.a = 1f;


        /*  if (isFound)
          {
              overlayIconOneOpaque.gameObject.SetActive(true);
              overlayIconeOneTransparent.gameObject.SetActive(false);

              companion.SetActive(true);
              companionDifferent.SetActive(false);

              // overlayIconeOne.color = tempColor;
          }

      */
        //navigationArrow.SetActive(false);

        startMinigameChest = false;

        GameObject.Find("Treasure").GetComponent<ChestAnimationController>().idleToShake = false;
        GameObject.Find("Treasure").GetComponent<ChestAnimationController>().shakeToOpen = false;

    }


    #endregion // PROTECTED_METHODS
}
