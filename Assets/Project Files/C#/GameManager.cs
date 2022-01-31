using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    [Header("Cameras & & Enviroments Setup")]
    public GameObject TutorialObj;
    public GameObject _MainCamera;
    public GameObject otherGarden;
    public GameObject VirtualJoyStic;


    [Header("Player FX")]
    public GameObject NecatrParticalse;
    public GameObject moneyParticls;
    public GameObject MoneyBlast;
    public GameObject HoneyJarFx;

    [Header("Game  Tranformation")]
    public Transform nectarEndPose;
    public Transform moneyEndPose;
    public Transform hJarEndPos;


    [Header("Player Storage Data")]
    public float toalCash;
    public float startCash;
    public float nectarCollectLimit;
    public float TotalNectar;

    public int flowerUpgradeNumber;
    [Header("Honey Storage Data")]
    public int H_StorageNectarLimite;
    public int H_StorageNectar;

    [Header("Honey JUar Data")]
    public int H_JarLimite;
    public int H_jar;


    [Header("Player Speed Data")]
    public float maxSpeed;
    public float minSpeed;

    [Header("Upgrade Level")]
    public int speedLevel;
    public int capaCityLevel;
    public int collectSpeedLevel;


    [Header("Flowe Level Data")] /////
    public int currentFlowerLevel;
    public int flowerUpgradePrice;

    // -------------------------- \\\\\
    [Header("Storage Level Data")] /////
    public int currentStorageLevel;
    public int storageUpgradePrice;

    // -------------------------- \\\\\
    [Header("Factory Level Data")] /////
    public int currentFactoryLevel;
    public int factoryUpgradePrice;


    [Header("UpgradePrice")]
    public int speedUpgradePrice;
    public int capaCityUpgradePrice;
    public int collectUpradePrice;


    public int playState;


    public bool isTargetTrigger;


    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        playState = PlayerPrefs.GetInt("playState");

        if (playState == 0)
        {
            // To Save player State
            playState = PlayerPrefs.GetInt("playState");

            PlayerPrefs.SetInt("TotalNectar",0);
            /// Save totalCash 
            toalCash = startCash;
            PlayerPrefs.SetFloat("toalCash", toalCash);
            toalCash = PlayerPrefs.GetFloat("toalCash");
            _MainCamera.GetComponent<Animator>().enabled = false;
           // TutorialObj.SetActive(true);

        }
        else
        {

          
            toalCash = PlayerPrefs.GetFloat("toalCash");
            _MainCamera.GetComponent<Animator>().enabled = true;

            speedLevel = PlayerPrefs.GetInt("speedLevel");

            flowerUpgradeNumber = PlayerPrefs.GetInt("flowerUpgradeNumber");

            if (speedLevel > 0)
            {

                float newSpeed = speedLevel + 0.25f;

                maxSpeed += newSpeed;
                minSpeed += newSpeed;




                Debug.Log("Upgrade maxSpeed " + maxSpeed);
                Debug.Log("Upgrade minSpeed " + minSpeed);
            }


                H_jar = PlayerPrefs.GetInt("H_jar");

            currentFlowerLevel = PlayerPrefs.GetInt("currentFlowerLevel");
            currentStorageLevel = PlayerPrefs.GetInt("currentStorageLevel");
            currentFactoryLevel = PlayerPrefs.GetInt("currentFactoryLevel");

            capaCityLevel = PlayerPrefs.GetInt("capaCityLevel");

            if (capaCityLevel > 0)
            {

                int newCapacity = capaCityLevel + 1;
                nectarCollectLimit = nectarCollectLimit * newCapacity;
                Debug.Log("Upgrade maxSpeed " + nectarCollectLimit);
            }

        }

        TotalNectar = PlayerPrefs.GetFloat("TotalNectar");
    }

    // Start is called before the first frame update
    void Start()
    {

        PlayerController.playerController.playerSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            ResetLevelData();
        }

        
    }




    public void SubtractCash(float Amount)
    {


        PlayerPrefs.SetInt("playState", 1);
        toalCash -= Amount;

        PlayerPrefs.SetFloat("toalCash", toalCash);
        toalCash = PlayerPrefs.GetFloat("toalCash");
    }

    public void AddCash(float Amount)
    {
        toalCash += Amount;

        PlayerPrefs.SetFloat("toalCash", toalCash);
        toalCash = PlayerPrefs.GetFloat("toalCash");
    }


    public void NectarAdd(float Amount)
    {
        if (TotalNectar <= nectarCollectLimit)
        {
            TotalNectar += Amount;

            PlayerPrefs.SetFloat("TotalNectar", TotalNectar);
        }
        else
        {
            Debug.Log("Nectar Store Full " + TotalNectar);

            PlayerPrefs.SetFloat("TotalNectar", TotalNectar);
            TotalNectar = PlayerPrefs.GetFloat("TotalNectar");
        }
      
    }

    public void NectarSubratct(float Amount)
    {

        TotalNectar -= Amount;
        PlayerPrefs.SetFloat("TotalNectar", TotalNectar);
        TotalNectar = PlayerPrefs.GetFloat("TotalNectar");
    }


   
    // Calculation Honey Jar
    public void addJar(int Amount)
    {
        if (H_jar <= H_JarLimite)
        {
            H_jar += Amount;

            PlayerPrefs.SetInt("H_jar", H_jar);
        }
        else
        {
            Debug.Log("Honey Jar Full " + H_jar);
            PlayerPrefs.SetInt("H_jar", H_jar);
            H_jar = PlayerPrefs.GetInt("H_jar");
        }
    }

    public void subtractJar(int Amount)
    {
        H_jar -= Amount;
        PlayerPrefs.SetInt("H_jar", H_jar);
        H_jar = PlayerPrefs.GetInt("H_jar");
    }



    public void UpgradeSpeedLevel()
    {
        speedLevel = PlayerPrefs.GetInt("speedLevel");
        speedLevel += 1;
        PlayerPrefs.SetInt("speedLevel", speedLevel);

        speedLevel = PlayerPrefs.GetInt("speedLevel");

        if (speedLevel > 0)
        {

            float newSpeed = speedLevel + 0.25f;

            
            maxSpeed += speedLevel;
            minSpeed += speedLevel;
            PlayerController.playerController.playerSpeed = maxSpeed;
            Debug.Log("Upgrade maxSpeed " + maxSpeed);
            Debug.Log("Upgrade minSpeed " + minSpeed);
        }

    }

    public void UpgradeCapacityLevel()
    {
        capaCityLevel = PlayerPrefs.GetInt("capaCityLevel");
        capaCityLevel += 1;
        PlayerPrefs.SetInt("capaCityLevel", capaCityLevel);

        capaCityLevel = PlayerPrefs.GetInt("capaCityLevel");

        if (capaCityLevel > 0)
        {

            int newCapacity = capaCityLevel + 1;


            nectarCollectLimit = nectarCollectLimit * newCapacity;

            Debug.Log("Upgrade maxSpeed " + nectarCollectLimit);
         //   Debug.Log("Upgrade minSpeed " + minSpeed);
        }
    }


    public void UpgradeFlowerLevel()
    {
        currentFlowerLevel = PlayerPrefs.GetInt("currentFlowerLevel");
        if (currentFlowerLevel < 3)
        {
            currentFlowerLevel += 1;
        }
       
        PlayerPrefs.SetInt("currentFlowerLevel", currentFlowerLevel);
        currentFlowerLevel = PlayerPrefs.GetInt("currentFlowerLevel");

    }

    public void UpgradeStorageLevel()
    {
        currentStorageLevel = PlayerPrefs.GetInt("currentStorageLevel");
        currentStorageLevel += 1;
        PlayerPrefs.SetInt("currentStorageLevel", currentStorageLevel);
        currentStorageLevel = PlayerPrefs.GetInt("currentStorageLevel");
    }

    public void UpgradeFactoryLevel()
    {
        currentFactoryLevel = PlayerPrefs.GetInt("currentFactoryLevel");
        currentFactoryLevel += 1;
        PlayerPrefs.SetInt("currentFactoryLevel", currentFactoryLevel);
        currentFactoryLevel = PlayerPrefs.GetInt("currentFactoryLevel");
    }

    public void UpgradeCollectLevel()
    { 
        
    }

    public void ResetLevelData()
    {
        PlayerPrefs.SetFloat("toalCash", 0);
        PlayerPrefs.SetInt("playState",0);
        PlayerPrefs.SetInt("speedLevel", 0);
        PlayerPrefs.SetInt("tutorialDone", 0);
        PlayerPrefs.SetInt("tutorialState", 0);
        PlayerPrefs.SetInt("capaCityLevel", 0);
        PlayerPrefs.SetInt("flowerUpgradeNumber",0);
        PlayerPrefs.SetInt("currentFlowerLevel", 0);
        PlayerPrefs.SetInt("currentStorageLevel", 0);
        PlayerPrefs.SetInt("currentFactoryLevel", 0);
        PlayerPrefs.SetInt("H_jar", 0);
    }
}
