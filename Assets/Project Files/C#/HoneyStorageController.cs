using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HoneyStorageController : MonoBehaviour
{




    [SerializeField]
    Text StorageText;


    [SerializeField]
    float price = 200;

    [SerializeField]
    GameObject honeyStorage, honeyStorageAlpha;

    [SerializeField]
    GameObject buyBtn, priceBar,storageBar,fullIcone;

    [SerializeField]
    public int lockNumber = 0;

    public int nectarLimite,currentNectar;


    // Start is called before the first frame update
    void Start()
    {

        lockNumber = PlayerPrefs.GetInt(this.gameObject.name);

        currentNectar = PlayerPrefs.GetInt("currentNectar");


        StorageText.text = currentNectar + " / " + nectarLimite;

        GameManager.gameManager.H_StorageNectar = currentNectar;

        if (lockNumber == 0)
        {
            if (buyBtn.activeSelf)
            {
                fullIcone.SetActive(false);
                priceBar.SetActive(true);
                honeyStorageAlpha.SetActive(true);
                honeyStorage.SetActive(false);
                storageBar.SetActive(false);
                buyBtn.SetActive(false);

            }
        }
        else
        {
            GameManager.gameManager.H_StorageNectar = currentNectar;
            SavedData();
        }


      
        nectarLimite = GameManager.gameManager.H_StorageNectarLimite;



    }

    public void SubractHoney()
    {
        currentNectar -= 5;
        StorageText.text = currentNectar + " / " + nectarLimite;

        PlayerPrefs.SetInt("currentNectar", currentNectar);

        //   currentNectar = PlayerPrefs.GetInt("currentNectar");

        GameManager.gameManager.H_StorageNectar = currentNectar;

     //   Debug.Log(other.gameObject.tag + " is Exit Now ");
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

           
        }


       
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

            fullIcone.SetActive(false);
        }
      
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (honeyStorage.activeSelf)
            {
                Debug.Log("Sales Nectar ");

                if (nectarLimite > currentNectar)
                {
                    Invoke("StoreNecter", 0.2f);
                }
                else
                {
                    fullIcone.SetActive(true);
                }
            }
        }
    }


    private void StoreNecter()
    {
        CancelInvoke("StoreNecter");
        if (nectarLimite > currentNectar && GameManager.gameManager.TotalNectar >0)
        {
            GoldCoinMover(PlayerController.playerController.NectarStartPotion, GameManager.gameManager.NecatrParticalse, priceBar.transform);
            GameManager.gameManager.NectarSubratct(1);
            currentNectar++;
            StorageText.text = currentNectar + " / " + nectarLimite;

            PlayerPrefs.SetInt("currentNectar", currentNectar);

         //   currentNectar = PlayerPrefs.GetInt("currentNectar");

            GameManager.gameManager.H_StorageNectar = currentNectar;
        }
    }



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

        honeyStorageAlpha.SetActive(false);
        honeyStorage.SetActive(true);
        storageBar.SetActive(true);

        if (priceBar.activeSelf)
       {
            priceBar.SetActive(false);
       }
    }




    public void ResetGardenData()
    {
        PlayerPrefs.SetInt(this.gameObject.name, 0);
        PlayerPrefs.SetInt("currentNectar",0);
    }


    public void buyProducts()
    {


        if (GameManager.gameManager.toalCash > price)
        {
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
