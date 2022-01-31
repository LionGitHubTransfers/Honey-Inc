using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public static TutorialController tutorialController;
    public GameObject DragUI;
    public GameObject fieldUnlock,saleCenterUnlock,sellNectarIcone,collectNectarIcone,beeHirePos;

    [SerializeField]
    private int tutorialDone;

    public int tutorialState;

    private void Awake()
    {
        tutorialDone = PlayerPrefs.GetInt("tutorialDone");

        tutorialState = PlayerPrefs.GetInt("tutorialState");

        if (tutorialDone == 0)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
        //    this.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialController == null)
        {
            tutorialController = this;
        }

        fieldUnlock.SetActive(false);
        saleCenterUnlock.SetActive(false);
        sellNectarIcone.SetActive(false);
        collectNectarIcone.SetActive(false);
        beeHirePos.SetActive(false);
        if (!DragUI.activeSelf)
        {
            DragUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

      

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevelData();
        }


        if (PlayerController.playerController.isFlay)
        {
            if (DragUI.activeSelf)
            {
                DragUI.SetActive(false);
                GameManager.gameManager._MainCamera.GetComponent<Animator>().enabled = true;
                //  fieldUnlock.SetActive(true);
            }
        }


        if (tutorialDone == 0 && tutorialState == 0)
        {
            tutorialState = PlayerPrefs.GetInt("tutorialState");
            saleCenterUnlock.SetActive(false);
        }
        else if (tutorialDone == 0 && tutorialState == 1)
        {
            // fieldUnlock.SetActive(false);
            saleCenterUnlock.SetActive(true);
            collectNectarIcone.SetActive(false);
            sellNectarIcone.SetActive(false);
            beeHirePos.SetActive(false);
        }
        else if (tutorialDone == 0 && tutorialState == 2)
        {

            collectNectarIcone.SetActive(true);
            saleCenterUnlock.SetActive(false);
            sellNectarIcone.SetActive(false);
            beeHirePos.SetActive(false);

        }
        else if (tutorialDone == 0 && tutorialState == 3)
        {
            sellNectarIcone.SetActive(true);
            collectNectarIcone.SetActive(false);
            saleCenterUnlock.SetActive(false);
            beeHirePos.SetActive(false);
        }
        else if (tutorialDone == 0 && tutorialState == 4)
        {

            beeHirePos.SetActive(true);
            sellNectarIcone.SetActive(false);
            collectNectarIcone.SetActive(false);
            saleCenterUnlock.SetActive(false);
        }
       
    }




    public void callFieldUnlock()
    {
     //   fieldUnlock.SetActive(false);
      //  saleCenterUnlock.SetActive(true);
        PlayerPrefs.SetInt("tutorialState",1);
        tutorialState = PlayerPrefs.GetInt("tutorialState");
    }



    public void ResetLevelData()
    {

        PlayerPrefs.SetInt("tutorialDone",0);
        PlayerPrefs.SetInt("tutorialState", 0);
    }
}
