using System.Collections;
using UnityEngine;

public class Level0Manager : ObjectiveTracker
{

    public GameObject vaseInstance1;
    public GameObject vaseInstance2;
    public GameObject bat;

    public GameObject pickUpScroll;
    public GameObject smashScroll;

    public GameObject promptVasePickupUI;
    public GameObject promptBatPickupUI;

    private bool tutorialComplete;

    void Start()
    {

        vaseInstance1.SetActive(true);
        vaseInstance2.SetActive(false);
        bat.SetActive(false);
        TurnOnPromptPickupVaseUI();
        TurnOffPromptPickupBatUI();

        tutorialComplete = false;
        // subscribe event handlers to vaseInstance1 and vaseInstance2
        // need to update vaseInstance1 and 2 get componenet
        //Breakable breakable1 = vaseInstance1.GetComponent<Breakable>();
        //if (breakable1 != null)
        //{
        //    Debug.Log("breakable1");
        //    breakable1.OnBreak.AddListener(ChangeTutorial); // Add event listener to OnBreak
        //}

        //Breakable breakable2 = vaseInstance2.GetComponent<Breakable>();
        //if (breakable2 != null)
        //{
        //    breakable2.OnBreak.AddListener(OnObjectiveCompleted);
        //}


    }

    public void ChangeTutorial()
    {
        Debug.Log("ChangeTutorial");
        StartCoroutine(ChangeTutorialCoroutine());
    }

    private IEnumerator ChangeTutorialCoroutine()
    {
        // Wait for X seconds
        yield return new WaitForSeconds(5f);

        vaseInstance1.SetActive(false);
        TurnOffPromptPickupVaseUI();
        TurnOnPromptPickupBatUI();
        //vaseInstance2.SetActive(true);
        bat.SetActive(true);
    }


    public void DisplaySmashScroll()
    {
        smashScroll.SetActive(true);
    }

    public void TurnOffSmashScroll()
    {
        smashScroll.SetActive(false);
    }

    public void DisplayPickupScroll()
    {
        pickUpScroll.SetActive(true);
    }

    public void TurnOffPickupScroll()
    {
        pickUpScroll.SetActive(false);
    }

    public void TurnOnVase2()
    {
        if (!tutorialComplete)
        {
            vaseInstance2.SetActive(true);
        }
    }

    public void TurnOffVase2()
    {
        vaseInstance2.SetActive(false);
    }

    public void TurnOnPromptPickupVaseUI()
    {
        promptVasePickupUI.SetActive(true);
    }

    public void TurnOffPromptPickupVaseUI()
    {
        promptVasePickupUI.SetActive(false);
    }

    public void TurnOnPromptPickupBatUI()
    {
        promptBatPickupUI.SetActive(true);
    }

    public void TurnOffPromptPickupBatUI()
    {
        promptBatPickupUI.SetActive(false);
    }

    public void SetTutorialComplete()
    {
        tutorialComplete = true;
        TurnOffPromptPickupBatUI();
        OnObjectiveCompleted(0);
    }


    public void Vase2Destroyed()
    {
        StartCoroutine(Vase2Disappear());
    }

    private IEnumerator Vase2Disappear()
    {
        // Wait for X seconds
        yield return new WaitForSeconds(2.5f);

        vaseInstance2.SetActive(false);
    }
}
