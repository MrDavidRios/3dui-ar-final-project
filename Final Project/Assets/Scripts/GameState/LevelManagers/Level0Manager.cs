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

    public Transform vase2OriginalPos;
    public Transform vase2HidingPoint;

    private bool tutorialComplete;

    void Start()
    {

        vaseInstance1.SetActive(true);

        //vaseInstance2.SetActive(false);
        vaseInstance2.transform.position = vase2HidingPoint.position;

        bat.SetActive(false);

        TurnOnPromptPickupVaseUI();
        TurnOffPromptPickupBatUI();

        tutorialComplete = false;
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


        Destroy(vaseInstance1);
        TurnOffPromptPickupVaseUI();
        TurnOnPromptPickupBatUI();
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

    public void ReappearVase2()
    {
        if (!tutorialComplete)
        {
            //vaseInstance2.SetActive(true);
            vaseInstance2.transform.position = vase2OriginalPos.position;
        }
    }

    public void HideVase2()
    {
        if (!tutorialComplete)
        {
            vaseInstance2.transform.position = vase2HidingPoint.position;
        }
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

        StartCoroutine(SetTutorialCompleteEnumerator());
    }

    private IEnumerator SetTutorialCompleteEnumerator()
    {
        yield return new WaitForSeconds(2.0f);
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
