using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesCenterController : MonoBehaviour
{

    public static SalesCenterController salesCenterController;

    public float GardenPrice;

    public float nectarNumber;

    [SerializeField]
    Transform MoneyStartPos;
    [Header("Sales Cneter Iteam")]
    public GameObject buybtn, priceBar, sliderBar,SalesCenters,salesTransparent;

    [Header("Sales Cneter Fx")]
    public GameObject portalFx;

    [SerializeField]
    Target targetArrow;


    public int lockNumber = 0;


    private void Awake()
    {
        if (salesCenterController == null)
            salesCenterController = this;



        lockNumber = PlayerPrefs.GetInt(this.gameObject.name); // Get seved Sales Data by it tag Name

        priceBar.SetActive(true);

        if (lockNumber == 0)
        {


            // Tutorials Arrow 
            if (targetArrow != null)
            {
                targetArrow.enabled = true;
            }


            SalesCenters.SetActive(false);
            salesTransparent.SetActive(true);
            if (sliderBar.activeSelf)
            {
                sliderBar.SetActive(false);
            }

            if (buybtn.activeSelf)
            {
                buybtn.SetActive(false);
            }

        }
        else
        {
            if (targetArrow != null)
            {
                targetArrow.enabled = false;
            }

            SavedData();
        }

    }

        // Start is called before the first frame update
    void Start()
    {
        portalFx.SetActive(false);
        nectarNumber = PlayerPrefs.GetFloat(nectarNumber.ToString());
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
            //if (TutorialController.tutorialController != null)
            //{
            //    TutorialController.tutorialController.saleCenterUnlock.SetActive(true);
            //}
        }
        }



    private void SavedData()
    {
        salesTransparent.SetActive(false);
        SalesCenters.SetActive(true);
        PlayerPrefs.SetInt(this.gameObject.name, 1);
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().isTrigger = true;
        priceBar.SetActive(false);
    }

    public void ResetGardenData()
    {
        PlayerPrefs.SetInt(this.gameObject.name, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

            

             if (priceBar.GetComponent<Animator>() != null)
                {
                    
                    priceBar.GetComponent<Animator>().SetBool("isPop", true);
                }
                if (!buybtn.activeSelf)
                {
                    buybtn.SetActive(true);
                }
         
        //    Debug.Log("Touch With Player " + other.gameObject.tag);
        }



    }

    public void buyProducts()
    {



        if (GameManager.gameManager.toalCash >= GardenPrice && GameManager.gameManager.TotalNectar >=15)
        {
            GameObject MoneyFx = Instantiate(GameManager.gameManager.MoneyBlast, this.gameObject.transform.position, Quaternion.identity);

            SavedData();
            GameManager.gameManager.SubtractCash(GardenPrice);

            GameManager.gameManager.NectarSubratct(15);

            if (TutorialController.tutorialController != null)
            {
                PlayerPrefs.SetInt("tutorialState", 2);
                TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");

              //  TutorialController.tutorialController.collectNectarIcone.SetActive(true);
            }

        }
        else
        {

            PlayerPrefs.SetInt("tutorialState", 2);
            TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
            Debug.Log(" sell Nectar Arrow ");
            Debug.Log("Not Enoug Money");
        }

    }


    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            portalFx.SetActive(false);

            if (TutorialController.tutorialController != null)
            {
                if (TutorialController.tutorialController.fieldUnlock.activeSelf == false)
                {
                   TutorialController.tutorialController.saleCenterUnlock.SetActive(true);
                }
            }


            if (priceBar.GetComponent<Animator>() != null)
            {
                priceBar.GetComponent<Animator>().SetBool("isPop", false);
            }


            if (buybtn.activeSelf)
            {
                buybtn.SetActive(false);
            }

       //     Debug.Log("Touch With Player " + other.gameObject.tag);
        }
    }


    private void OnTriggerExit(Collider other)
    {

        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.gameManager.isTargetTrigger = true;

            if (TutorialController.tutorialController != null)
            {
                TutorialController.tutorialController.saleCenterUnlock.SetActive(false);
            }

           // Debug.Log("Sales Nectar ");
            if (GameManager.gameManager.TotalNectar > 0)
            {
                //   Debug.Log("Sales Nectar ");
                portalFx.SetActive(true);
                Invoke("SalesNecter", 0.3f);

                if (TutorialController.tutorialController != null)
                {

                    //if (TutorialController.tutorialController.sellNectarIcone.activeSelf)
                    //{
                    // //   PlayerPrefs.SetInt("tutorialDone",1);
                    //  TutorialController.tutorialController.gameObject.SetActive(false);
                  //  }

                   

                }

            }
            else
            {

                if (TutorialController.tutorialController.beeHirePos.activeSelf == false)
                {
                    TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
                    if (TutorialController.tutorialController.tutorialState != 4)
                    {
                        PlayerPrefs.SetInt("tutorialState", 2);
                        TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
                        Debug.Log(" Exit From SalesCenter " + TutorialController.tutorialController.tutorialState);
                    }

                }
                GameManager.gameManager.isTargetTrigger = false;

                portalFx.SetActive(false);
                CancelInvoke("SalesNecter");
            }


            // Honey Jar Sell 

            if (GameManager.gameManager.H_jar > 0)
            {

                //   Debug.Log("Sales Nectar ");
                portalFx.SetActive(true);
                Invoke("salesHoneyJar", 0.3f);


            }
        }
    }


    private void salesHoneyJar()
    {
        CancelInvoke("salesHoneyJar");

        GameManager.gameManager.subtractJar(1);
        GameManager.gameManager.AddCash(15);

        GoldCoinCpuntAnim.coinCpuntAnim.GoldCoinMover(MoneyStartPos.transform, GameManager.gameManager.moneyParticls, GameManager.gameManager.moneyEndPose);
        PlayerController.playerController.textManager.Add("-" + nectarNumber.ToString(), new Vector3(PlayerController.playerController.transform.position.x, PlayerController.playerController.transform.position.y + 3.5f, PlayerController.playerController.transform.position.z), "default");

        JarMover(PlayerController.playerController.NectarStartPotion, GameManager.gameManager.HoneyJarFx, MoneyStartPos);
   
    }

    private void SalesNecter()
    {
        CancelInvoke("SalesNecter");

        GameManager.gameManager.NectarSubratct(1);

        nectarNumber++;
        GoldCoinCpuntAnim.coinCpuntAnim.GoldCoinMover(MoneyStartPos.transform, GameManager.gameManager.moneyParticls, GameManager.gameManager.moneyEndPose);
        PlayerController.playerController.textManager.Add("-" + nectarNumber.ToString(), new Vector3(PlayerController.playerController.transform.position.x, PlayerController.playerController.transform.position.y + 3.5f, PlayerController.playerController.transform.position.z), "default");
        //   PlayerController.playerController.textManager.Add("-"+nectarNumber.ToString(), new Vector3(PlayerController.playerController.transform.position.x, PlayerController.playerController.transform.position.y+3.5f, PlayerController.playerController.transform.position.z), "default");

        PlayerPrefs.SetFloat(nectarNumber.ToString(), nectarNumber);
        nectarNumber = PlayerPrefs.GetFloat(nectarNumber.ToString());

        if (nectarNumber == 1)
        {
           

            GameManager.gameManager.AddCash(1);
            nectarNumber = 0;
            PlayerPrefs.SetFloat(nectarNumber.ToString(), nectarNumber);
            nectarNumber = PlayerPrefs.GetFloat(nectarNumber.ToString());



        }
        else
        {
            GoldCoinMover(PlayerController.playerController.NectarStartPotion, GameManager.gameManager.NecatrParticalse, MoneyStartPos);
        }
    }



    public void GoldCoinMover(Transform startPose, GameObject goldPrefab, Transform target)
    {



        GameObject goldCoin = Instantiate(goldPrefab, startPose);

        float Yy = UnityEngine.Random.Range(0.1f, 0.5f);
        goldCoin.transform.position = new Vector3(startPose.transform.position.x, startPose.transform.position.y + Yy, startPose.transform.position.z + Yy);

        StartCoroutine(CoinMovementSequence(goldCoin.transform, startPose.transform.position, target.transform.position));
    }


    public void JarMover(Transform startPose, GameObject goldPrefab, Transform target)
    {



        GameObject goldCoin = Instantiate(goldPrefab, startPose);

       // float Yy = UnityEngine.Random.Range(0.1f, 0.5f);
        goldCoin.transform.position = new Vector3(startPose.transform.position.x, startPose.transform.position.y, startPose.transform.position.z);

        StartCoroutine(CoinMovementSequence(goldCoin.transform, startPose.transform.position, target.transform.position));
    }

    private IEnumerator CoinMovementSequence(Transform goldObj, Vector3 startPosition, Vector3 endPosition)
    {
        float time = 0;

        while (time < 1)
        {
            time += GoldCoinCpuntAnim.coinCpuntAnim.speed * Time.deltaTime;
            goldObj.position = Vector3.Lerp(startPosition, endPosition, time);

            yield return new WaitForEndOfFrame();
        }

        //   atComplete.Invoke();
        Destroy(goldObj.gameObject);
    }

}
