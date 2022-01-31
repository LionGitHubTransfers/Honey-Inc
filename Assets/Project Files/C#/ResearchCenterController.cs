using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchCenterController : MonoBehaviour
{

    [SerializeField]
    Text StorageText;


    [SerializeField]
    float price = 200;

    [SerializeField]
    GameObject _honeyResearchCenterMain, _honeyResearchCenterTransparent;

    [SerializeField]
    GameObject buyBtn, priceBar, storageBar, fullIcone,UpgradeUi;

    [SerializeField]
    public int lockNumber = 0;

    public int nectarLimite, currentNectar;


    // Start is called before the first frame update
    void Start()
    {

        lockNumber = PlayerPrefs.GetInt(this.gameObject.name);

        currentNectar = PlayerPrefs.GetInt("currentNectar");


        // StorageText.text = currentNectar + " / " + nectarLimite;

        if (lockNumber == 0)
        {
            if (buyBtn.activeSelf)
            {

                UpgradeUi.SetActive(false);
               // fullIcone.SetActive(false);
                priceBar.SetActive(true);
                _honeyResearchCenterTransparent.SetActive(true);
                _honeyResearchCenterMain.SetActive(false);
               // storageBar.SetActive(false);
                buyBtn.SetActive(false);
            }
        }
        else
        {
            UpgradeUi.SetActive(false);
            SavedData();
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (lockNumber == 0)
            {
                if (priceBar.GetComponent<Animator>() != null)
                {
                    priceBar.GetComponent<Animator>().SetBool("isPop", true);
                }

                if (!buyBtn.activeSelf)
                {
                    buyBtn.SetActive(true);
                }
            }
            else
            {
               PlayerController.playerController.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                GameManager.gameManager.VirtualJoyStic.SetActive(false);
                GameManager.gameManager._MainCamera.GetComponent<Animator>().SetBool("IDL", true);
            //    Vector3 newRotation = new Vector3(0, 180, 0);
               // PlayerController.playerController.beeObj.transform.eulerAngles = newRotation;
                UpgradeUi.SetActive(true);
            }


        }
    }


    public void ExitShop()
    {
        GameManager.gameManager.VirtualJoyStic.SetActive(true);
        //  PlayerController.playerController.beeObj.transform.rotation = PlayerController.playerController.startRotation;
        PlayerController.playerController.GetComponent<Rigidbody>().velocity = new  Vector3(0,0,0);
        GameManager.gameManager._MainCamera.GetComponent<Animator>().SetBool("IDL", false);
        UpgradeUi.SetActive(false);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (buyBtn.activeSelf)
            {
                buyBtn.SetActive(false);

                if (priceBar.GetComponent<Animator>() != null)
                {
                    priceBar.GetComponent<Animator>().SetBool("isPop", false);
                }
            }

            UpgradeUi.SetActive(false);

         //   PlayerController.playerController.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            // Vector3 newRotation = new Vector3(0, 0, 0);
          //  PlayerController.playerController.beeObj.transform.rotation = PlayerController.playerController.startRotation;
           // GameManager.gameManager._MainCamera.GetComponent<Animator>().SetBool("IDL", false);
            // fullIcone.SetActive(false);
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (_honeyResearchCenterMain.activeSelf)
            {
                //Debug.Log("Sales Nectar ");

             
                //    Invoke("StoreNecter", 0.2f);
              
            }
        }
    }




    //private void StoreNecter()
    //{
    //    CancelInvoke("StoreNecter");
    //    if (nectarLimite > currentNectar && GameManager.gameManager.TotalNectar > 0)
    //    {
    //        GoldCoinMover(PlayerController.playerController.NectarStartPotion, GameManager.gameManager.NecatrParticalse, priceBar.transform);
    //        GameManager.gameManager.NectarSubratct(1);
    //        currentNectar++;
    //        //   StorageText.text = currentNectar + " / " + nectarLimite;

    //        PlayerPrefs.SetInt("currentNectar", currentNectar);
    //    }
    //}



    public void GoldCoinMover(Transform startPose, GameObject goldPrefab, Transform target)
    {



        // GameObject goldCoin = Instantiate(goldPrefab, startPose);

        GameObject goldCoin = GameObject.Instantiate(goldPrefab, startPose.position, Quaternion.identity);

        float Yy = UnityEngine.Random.Range(0.1f, 0.5f);
        goldCoin.transform.position = new Vector3(startPose.transform.position.x, startPose.transform.position.y + Yy, startPose.transform.position.z + Yy);

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





    void SavedData()
    {

        _honeyResearchCenterTransparent.SetActive(false);
        _honeyResearchCenterMain.SetActive(true);
        // storageBar.SetActive(true);
      //  
        if (priceBar.activeSelf)
        {
            priceBar.SetActive(false);
        }
    }




    public void ResetGardenData()
    {
        PlayerPrefs.SetInt(this.gameObject.name, 0);
        PlayerPrefs.SetInt("currentNectar", 0);
    }


    public void buyProducts()
    {


        if (GameManager.gameManager.toalCash > price)
        {
            GameObject MoneyFx = Instantiate(GameManager.gameManager.MoneyBlast, this.gameObject.transform.position, Quaternion.identity);

            PlayerPrefs.SetInt(this.gameObject.name, 1);
            lockNumber = PlayerPrefs.GetInt(this.gameObject.name);


            SavedData();
            GameManager.gameManager.SubtractCash(price);

        }
        else
        {
            Debug.Log(" Not Enough Money ");
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGardenData();
        }
    }


}
