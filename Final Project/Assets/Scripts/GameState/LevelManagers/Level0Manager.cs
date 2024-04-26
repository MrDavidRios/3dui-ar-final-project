using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Manager : ObjectiveTracker
{

    public GameObject vaseInstance1;
    public GameObject vaseInstance2;
    public GameObject bat;

    public GameObject pickUpScroll;
    public GameObject smashScroll;

    void Start()
    {

        vaseInstance1.SetActive(true);
        vaseInstance2.SetActive(false);
        bat.SetActive(false);

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
        // Wait for 3 seconds
        yield return new WaitForSeconds(5f);

        vaseInstance1.SetActive(false);
        vaseInstance2.SetActive(true);
        bat.SetActive(true);
    }

    public void TutorialComplete()
    {
        OnObjectiveCompleted();
    }

    public void DisplaySmashScroll()
    {

    }
}
