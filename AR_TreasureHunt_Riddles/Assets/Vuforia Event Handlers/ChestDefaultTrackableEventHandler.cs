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
    // The concept and structure of this DefaultTrackableEventHandler script is identical to those that are used for the other image targets used in this game.

    #region PROTECTED_MEMBER_VARIABLES

 

    public static DefaultTrackableEventHandler main;


    //Initialization of variables.
    //startMiniGame starts the mini-game in the corresponding game script.
    //startMiniGameEnding starts the mini-game in the corresponding game script.
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

        //The conditional if-statement in the individual DefaultTrackableEventHandler scripts, are cheking the state of the four main mini-games.
        //Each game script contains a Boolean variable that indicates, whether a game has been completed.
        //By checking the state of every game, it is ensured that every game is only executed when it is supposed to.
        //The chest mini-game is the first game that the player encounters, therefore the conditional if-statement checks if every future is false, i.e. has not been completed yet.
        if (GameObject.Find("Treasure").GetComponent<MiniGameChest>().isChestGameCompleted == false &&
            GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == false &&
            GameObject.Find("Detective").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == false &&
            GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == false)
        {
            //This Boolean variable starts the chest mini-game.
            //The corresponding game script checks, whether this Boolean is true and starts the game when it is.
            startMinigameChest = true;
        }
        
        //This statement has the same functionality, as the one mentioned above.
        if (GameObject.Find("Treasure").GetComponent<MiniGameChest>().isChestGameCompleted == true &&
            GameObject.Find("ImageTargetGinger").GetComponent<MiniGameGingerbread>().isGingerbreadMiniGameCompleted == true &&
            GameObject.Find("Detective").GetComponent<MiniGameCulprit>().isCulpritMiniGameCompleted == true &&
            GameObject.Find("ArcadeMachine").GetComponent<MiniGameArcade>().isArcadeMiniGameCompleted == true)
        {
            
            GameObject.Find("Startscreen").GetComponent<textBubble>().outro.current = true;
            startMinigameEnding = true;
        }

    }


    //This function is provided by Vuforia, by implementing the "ITrackableEventHandler" - Interface.
    //This function is executed when the corresponding marker, that this script is attached to, leaves the field of view of the camera.
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

        //The Boolean variable to start the game is set to false, which stops the execution of the mini-game.
        startMinigameChest = false;


        GameObject.Find("Treasure").GetComponent<ChestAnimationController>().idleToShake = false;
        GameObject.Find("Treasure").GetComponent<ChestAnimationController>().shakeToOpen = false;

    }


    #endregion // PROTECTED_METHODS
}
