using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
public class GardenController : MonoBehaviour
{

    public float GardenPrice;

    [Header("Garden Iteam")]
    public GameObject buybtn;
    public GameObject priceBar;
    public GameObject UpgradeBar;
    public GameObject sliderBar;
    public GameObject BorderBox;
    public GameObject healEffects;
    public GameObject healEffects2;
    public GameObject UnlockFieldIcone;

    [Header("Garden Flowers")]
    public GameObject[] flowersGrp;


    [Header("Garden Float")]
    public float GardenGrowValue;
    public float flowrNectarNum;
    public float flowerNumber;

    [Header("Garden Int")]
    public int flowerDieNumber;
    public int targetFlowerDieNumber;
    public int lockNumber;
    public int levelNumber;
    public int beeWorksLevel;

    [Header("Garden  Flag ")]
    public bool isGrow;

    [Header("Garden  Slder ")]
    public Image slider;


    [SerializeField]
    GameObject thisGardenBee;
    [SerializeField]
    WorkerBeeStage _workerBeeStage;

    [SerializeField]
    Target targetArrow;

    [SerializeField]
    private string gardenLevelName;
    private void Awake()
    {


        gardenLevelName = this.gameObject.tag + "levelNumber";

        lockNumber = PlayerPrefs.GetInt(this.gameObject.tag); // Get seved Garden Data by it tag Name
        flowerDieNumber = PlayerPrefs.GetInt("flowerDieNumber");


        levelNumber = PlayerPrefs.GetInt(gardenLevelName); // Get Level Number
        beeWorksLevel = PlayerPrefs.GetInt("beeWorksLevel"); // Get Bee Wokrs Number
        priceBar.SetActive(true);

        flowrNectarNum = 3;

        // active Flowrs Iteam By it's 


        if (lockNumber == 0)
        {
            sliderBar.SetActive(false);
            healEffects.SetActive(false);
            if (targetArrow != null)
            {
                targetArrow.enabled = true;
                UnlockFieldIcone.SetActive(true);
            }

            isGrow = false;

            if (sliderBar.activeSelf)
            {
                sliderBar.SetActive(false);
            }

            if (buybtn.activeSelf)
            {
                buybtn.SetActive(false);
            }

            slider.GetComponent<Image>();
            slider.fillAmount = 0;

            if (BorderBox != null)
            {
                BorderBox.SetActive(true);
                UnlockFieldIcone.SetActive(false);
            }
        }
        else
        {
            // Arrow for tutorials 
            if (targetArrow != null)
            {
                targetArrow.enabled = false;
                UnlockFieldIcone.SetActive(false);
            }
            SavedData();

            healEffects.SetActive(false);
        }
        buybtn.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "BUY";

    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.gameManager.TotalNectar == GameManager.gameManager.nectarCollectLimit && TutorialController.tutorialController.tutorialState !=4)
        {
            PlayerPrefs.SetInt("tutorialState", 3);
            TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
        }
        else
        {

            if (TutorialController.tutorialController.tutorialState > 1 && TutorialController.tutorialController.tutorialState !=4)
            {
                PlayerPrefs.SetInt("tutorialState", 2);
                TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
            }
       
        }
        //  flowrNectarNum = 3;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGardenData();
        }

        if (!priceBar.activeSelf)
        {
            if (flowersGrp[levelNumber].transform.GetChild(0).gameObject.GetComponent<SunFlowersController>().isGrow == false)
            {
                if (slider.fillAmount < 1.0f)
                {
                    if (lockNumber == 1)
                    {
                        sliderBar.SetActive(true);
                    }
                
                    GardenGrowValue += 0.1f * Time.deltaTime;
                //    healEffects.SetActive(true);
                    slider.fillAmount = GardenGrowValue;

                }
                else
                {
                    if (sliderBar.activeSelf)
                    {
                      
                        isGrow = true;
                        sliderBar.SetActive(false);
                    }
                    if (isGrow)
                    {
                        healEffects.SetActive(false);
                    }
                   



                }

            }
            else
            {
                sliderBar.SetActive(false);
            }

            UnlockFieldIcone.SetActive(false); 
              
         

           

        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (priceBar.GetComponent<Animator>() != null)
            {
                priceBar.GetComponent<Animator>().SetBool("isPop", true);
            }


            if (lockNumber == 1)
            {
                UnlockFieldIcone.SetActive(false);
            }
            else
            {
                UnlockFieldIcone.SetActive(false);
            }


            if (!buybtn.activeSelf)
            {
                buybtn.SetActive(true);
            }

            

          //  Debug.Log("Touch With Player " + other.gameObject.tag);
        }

       

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (lockNumber == 1)
            {
                UnlockFieldIcone.SetActive(false);
            }
            else
            {
                UnlockFieldIcone.SetActive(true);
            }

            if (priceBar.GetComponent<Animator>() != null)
            {
                priceBar.GetComponent<Animator>().SetBool("isPop", false);
            }

            if (buybtn.activeSelf)
            {
                buybtn.SetActive(false);
            }

          //  Debug.Log("Touch With Player " + other.gameObject.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            GameManager.gameManager.isTargetTrigger = true;
            if (GameManager.gameManager.TotalNectar == GameManager.gameManager.nectarCollectLimit)
            {
                PlayerController.playerController.fullFx.SetActive(false);
                PlayerController.playerController.fullFx.SetActive(true);




                TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");

                if (TutorialController.tutorialController.tutorialState != 4 && lockNumber ==1)
                {

                    GameManager.gameManager.isTargetTrigger = false;
                    PlayerPrefs.SetInt("tutorialState", 3);
                    TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
                    Debug.Log(" sell Nectar Arrow ");
                }



                //TutorialController.tutorialController.sellNectarIcone.SetActive(true);

                //TutorialController.tutorialController.collectNectarIcone.SetActive(false);
            }



            ///// Check for upgrade Current Flower 

            if (GameManager.gameManager.currentFlowerLevel > 0 && beeWorksLevel >1 && GameManager.gameManager.currentFlowerLevel != levelNumber)
            {
                UpgradeBar.SetActive(false);
                UpgradeBar.SetActive(true);

                if (UpgradeBar.GetComponent<Animator>() != null)
                {
                    UpgradeBar.GetComponent<Animator>().SetBool("isPop", true);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.playerController.playerSpeed = GameManager.gameManager.maxSpeed;

            if (flowerDieNumber > targetFlowerDieNumber)
            {
                _workerBeeStage.BeeStartWorking();
               

            }
            if (UpgradeBar.GetComponent<Animator>() != null)
            {
                UpgradeBar.GetComponent<Animator>().SetBool("isPop", false);
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {


            PlayerController.playerController.playerSpeed  = GameManager.gameManager.minSpeed;
            if (GameManager.gameManager.TotalNectar == GameManager.gameManager.nectarCollectLimit)
            {



                if (TutorialController.tutorialController.tutorialState != 4 && GameManager.gameManager.TotalNectar ==15 && lockNumber ==1)
                {
                    PlayerPrefs.SetInt("tutorialState", 3);
                    TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
                    Debug.Log(" sell Nectar Arrow ");
                }

                //PlayerPrefs.SetInt("tutorialState", 3);
                //TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");



                //TutorialController.tutorialController.sellNectarIcone.SetActive(true);

                //TutorialController.tutorialController.collectNectarIcone.SetActive(false);


                //PlayerController.playerController.fullFx.SetActive(false);
                //PlayerController.playerController.fullFx.SetActive(true);

                //if (TutorialController.tutorialController != null)
                //{
                //    TutorialController.tutorialController.sellNectarIcone.SetActive(true);

                //    TutorialController.tutorialController.collectNectarIcone.SetActive(false);
                //}
            }

          

        }
    }

    private void SavedData()
    {


        healEffects.SetActive(true);


        PlayerPrefs.SetInt(this.gameObject.tag, 1);
        lockNumber = PlayerPrefs.GetInt(this.gameObject.tag); // Get seved Garden Data by it tag Name

        if (BorderBox != null)
        {
            BorderBox.SetActive(false);
        }

        switch (levelNumber)
        {

            case 2:
                print("Flower 3" + levelNumber);
                flowersGrp[0].SetActive(false);
                flowersGrp[1].SetActive(false);
                flowersGrp[2].SetActive(true);

                flowerNumber = flowersGrp[levelNumber].transform.childCount;
                Debug.Log(levelNumber + " for Flowers " + flowerNumber);
                break;
            case 1:

                flowersGrp[0].SetActive(false);
                flowersGrp[1].SetActive(true);
                flowersGrp[2].SetActive(false);
                print("Flower 2 " + levelNumber);

                flowerNumber = flowersGrp[levelNumber].transform.childCount;
                Debug.Log(levelNumber + " for Flowers " + flowerNumber);
                break;
            case 0:
                flowersGrp[0].SetActive(true);
                flowersGrp[1].SetActive(false);
                flowersGrp[2].SetActive(false);
                print("Flower 1" + levelNumber);

                flowerNumber = flowersGrp[levelNumber].transform.childCount;
                Debug.Log(levelNumber + " for Flowers " + flowerNumber);
                break;
            default:
                flowerNumber = flowersGrp[levelNumber].transform.childCount;
                Debug.Log(levelNumber + " for Flowers " + flowerNumber);
                print("Incorrect intelligence level.");
                break;
        }
        this.GetComponent<BoxCollider>().isTrigger = true;
        priceBar.SetActive(false);



    }

    public void buyProducts()
    {

      

        if (GameManager.gameManager.toalCash >= GardenPrice)
        {

            if (targetArrow != null)
            {
                targetArrow.enabled = false;
            }

            GameObject MoneyFx = Instantiate(GameManager.gameManager.MoneyBlast, this.gameObject.transform.position, Quaternion.identity);

            SavedData();

            PlayerPrefs.SetInt("tutorialState", 1);
            TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");

            // TutorialController.tutorialController.saleCenterUnlock.SetActive(true);
            GameManager.gameManager.SubtractCash(GardenPrice);
        }
        else
        {
           // Debug.Log("Not Enoug Money");
        }
       
    }

    public void ResetGardenData()
    {
        PlayerPrefs.SetInt("flowerDieNumber", 0); // 

        PlayerPrefs.SetInt(gardenLevelName, 0); // Seve Level Number

        PlayerPrefs.SetInt("beeWorksLevel", 0); // Set Worker Round

        PlayerPrefs.SetInt(this.gameObject.tag,0);
    }


    public void callFlowerDie()
    {
        flowerDieNumber++;

        PlayerPrefs.SetInt("flowerDieNumber", flowerDieNumber);
        flowerDieNumber = PlayerPrefs.GetInt("flowerDieNumber");

    }


    public void resetCurrentData()
    {
        PlayerPrefs.SetInt("flowerDieNumber", 0);
        PlayerPrefs.SetInt("beeWorksLevel", beeWorksLevel +1);

        beeWorksLevel = PlayerPrefs.GetInt("beeWorksLevel");
        for (int i = 0; i < flowerNumber;i++)
        {
            flowersGrp[levelNumber].transform.GetChild(i).gameObject.GetComponent<SunFlowersController>().ResetCurrentData();
            Debug.Log("Reset this Flower Data  ");
        }

        flowerDieNumber = PlayerPrefs.GetInt("flowerDieNumber");
        slider.fillAmount = 0;

        isGrow = false;

        sliderBar.SetActive(true);

    }



    public void UpgradeGarden()
    {
        for (int i = 0; i < flowerNumber; i++)
        {
            flowersGrp[levelNumber].transform.GetChild(i).gameObject.GetComponent<SunFlowersController>().ResetData();
            Debug.Log(" last Reset this Flower Data  ");
        }


        UpgradeBar.SetActive(false);

        levelNumber = PlayerPrefs.GetInt(gardenLevelName); // Get Level Number
        levelNumber += 1;
        PlayerPrefs.SetInt(gardenLevelName, levelNumber);

        levelNumber = PlayerPrefs.GetInt(gardenLevelName); // Get Level Number



        healEffects.SetActive(true);


        switch (levelNumber)
        {

           

            case 2:
                print("Flower 3" + levelNumber);
                flowersGrp[0].SetActive(false);
                flowersGrp[1].SetActive(false);
                flowersGrp[2].SetActive(true);

                flowerNumber = flowersGrp[levelNumber].transform.childCount;
                Debug.Log(levelNumber + " for Flowers " + flowerNumber);
                break;
            case 1:

                flowersGrp[0].SetActive(false);
                flowersGrp[1].SetActive(true);
                flowersGrp[2].SetActive(false);
                print("Flower 2 " + levelNumber);

                flowerNumber = flowersGrp[levelNumber].transform.childCount;
                Debug.Log(levelNumber + " for Flowers " + flowerNumber);
                break;
            case 0:
                flowersGrp[0].SetActive(true);
                flowersGrp[1].SetActive(false);
                flowersGrp[2].SetActive(false);
                print("Flower 1" + levelNumber);

                flowerNumber = flowersGrp[levelNumber].transform.childCount;
                Debug.Log(levelNumber + " for Flowers " + flowerNumber);
                break;
            default:
                flowerNumber = flowersGrp[levelNumber].transform.childCount;
                Debug.Log(levelNumber + " for Flowers " + flowerNumber);
                print("Incorrect intelligence level.");
                break;
        }



        for (int i = 0; i < flowerNumber; i++)
        {
            flowersGrp[levelNumber].transform.GetChild(i).gameObject.GetComponent<SunFlowersController>().ResetCurrentData();
            Debug.Log("Reset this Flower Data  ");
        }


        PlayerPrefs.SetInt("flowerDieNumber", 0);
        PlayerPrefs.SetInt("beeWorksLevel", 0);

        beeWorksLevel = PlayerPrefs.GetInt("beeWorksLevel");
        flowerDieNumber = PlayerPrefs.GetInt("flowerDieNumber");


        GardenGrowValue = 0;
        slider.fillAmount = 0;


      //  isGrow = false;


        sliderBar.SetActive(true);

    }


    
}
