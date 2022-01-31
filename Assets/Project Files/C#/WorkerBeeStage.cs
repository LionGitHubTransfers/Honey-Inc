using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBeeStage : MonoBehaviour
{

    public float beePrice;

    public GameObject priceBar,UnlockSing;

    [SerializeField]
    workerBeeController workerBeeController;


    public int beeLockNumber;



    [SerializeField]
    string beeID;
    // Start is called before the first frame update
    void Start()
    {
        beeID = this.gameObject.tag + "beeLockNumber";
        beeLockNumber = PlayerPrefs.GetInt(beeID);
      


        if (beeLockNumber == 0)
        {
            workerBeeController.enabled = false;


            priceBar.SetActive(false);
            UnlockSing.SetActive(true);
        }
        else
        {
            SavedData();
        }

       

      

    }


    private void OnTriggerEnter(Collider other)
    {

        if (beeLockNumber == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                UnlockSing.SetActive(false);
                priceBar.SetActive(true);

                if (priceBar.GetComponent<Animator>() != null)
                {
                    priceBar.GetComponent<Animator>().SetBool("isPop", true);
                }
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {


        if (beeLockNumber == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                UnlockSing.SetActive(true);
                priceBar.SetActive(false);

                if (priceBar.GetComponent<Animator>() != null)
                {
                    priceBar.GetComponent<Animator>().SetBool("isPop", false);
                }

            }
        }


       
    }


    public void buyProducts()
    {



        if (GameManager.gameManager.toalCash >= beePrice)
        {
            GameObject MoneyFx = Instantiate(GameManager.gameManager.MoneyBlast, this.gameObject.transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("tutorialState", 5);
            TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
            SavedData();
        }
        else
        {
            Debug.Log("Not Enoug Money");
        }

    }



    private void SavedData()
    {


        PlayerPrefs.SetInt(beeID, 1);
        priceBar.SetActive(false);
        UnlockSing.SetActive(false);

        this.GetComponent<BoxCollider>().isTrigger = true;
        priceBar.SetActive(false);
        beeLockNumber = PlayerPrefs.GetInt(beeID);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
        }
    }


    public void ResetData()
    {

        PlayerPrefs.SetInt(beeID, 0);

        beeLockNumber = PlayerPrefs.GetInt(beeID);
      
    }


    public void BeeStartWorking()
    {

        if (beeLockNumber == 1)
        {
            workerBeeController.enabled = true;
        }
        else
        {
            PlayerPrefs.SetInt("tutorialState", 4);
            TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");

            Debug.Log("UnLoking Bee ");
        }
       
    }
}
