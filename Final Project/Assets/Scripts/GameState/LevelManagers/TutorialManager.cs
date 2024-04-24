using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    // Tutorial manager for Level0

    // Main states:
    // 1. Prompt pickup vase
    // 2. Prompt smash

    // 3. Prompt pickup bat
    // 4. Prompt smash


    // Tutorial states
    //private enum TutorialState
    //{
    //    Start,
    //    PickUpThrowVase,
    //    PickBat,
    //    SmashVase,
    //    Complete
    //}

    //private TutorialState currentState = TutorialState.Start;


    //public GameObject bat;

    //public Transform vaseSpawnPoint;
    //public GameObject vaseInstance;
    //public GameObject vasePrefab;

    //public GameObject smashScroll;
    //public GameObject pickupScroll;

    //public GameObject uiElement1; // pickup UI instructions
    //public GameObject uiElement2; // pickup and swing tutorial
    //public GameObject uiElement3; // tutorial complete notification
    //// add more as necessary


    //// Start is called before the first frame update
    //void Start()
    //{
    //    SetState(TutorialState.PickUpThrowVase);
    //}

    //private void SetState(TutorialState newState)
    //{
    //    currentState = newState;

    //    switch (currentState)
    //    {
    //        case TutorialState.PickUpThrowVase:
    //            SpawnVase();

    //            ShowPickupScroll();
    //            HideSmashScroll();

    //            uiElement1.SetActive(true);
    //            uiElement2.SetActive(false);
    //            uiElement3.SetActive(false);
    //            break;

    //        case TutorialState.ThrowVase:
    //            ShowSmashScroll();
    //            HidePickupScroll();

    //            uiElement1.SetActive(true);
    //            uiElement2.SetActive(false);
    //            uiElement3.SetActive(false);
    //            break;

    //        case TutorialState.PickBat:
    //            bat.SetActive(true);
    //            ShowPickupScroll();
    //            HideSmashScroll();
    //            break;

    //        case TutorialState.SmashVase:
    //            ShowSmashScroll(); 
    //            HidePickupScroll();
    //            break;

    //        case TutorialState.Complete:
    //            ShowCompleteScroll(); 
    //            break;
    //    }
    //}

    //void ShowPickupScroll()
    //{
    //    pickupScroll.SetActive(true);
    //}

    //void HidePickupScroll()
    //{
    //    pickupScroll.SetActive(false);
    //}

    //void ShowSmashScroll()
    //{
    //    smashScroll.SetActive(true);
    //}

    //void HideSmashScroll()
    //{
    //    smashScroll.SetActive(false);
    //}

    //void ShowCompleteScroll()
    //{
    //    uiComplete.SetActive(true);
    //}

    //void HideCompleteScroll()
    //{
    //    uiComplete.SetActive(false);
    //}


    //public void SpawnVase()
    //{
    //    if (vaseInstance != null)
    //        Destroy(vaseInstance);

    //    vaseInstance = Instantiate(vasePrefab, vaseSpawnPoint);
    //}

    //public void OnVaseDestroyed()
    //{
    //    if (currentState == TutorialState.PickVase)
    //    {
    //        SetState(TutorialState.ThrowVase);
    //    }
    //    else if (currentState == TutorialState.SmashVase)
    //    {
    //        SetState(TutorialState.Complete);
    //    }
    //}

    //public void OnBatPickedUp()
    //{
    //    if (currentState == TutorialState.ThrowVase)
    //    {
    //        SetState(TutorialState.PickBat);
    //    }
    //}
}
