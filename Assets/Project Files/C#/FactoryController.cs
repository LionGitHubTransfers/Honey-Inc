using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryController : MonoBehaviour
{


    [SerializeField]
    Text StorageText;


    [SerializeField]
    float price = 200;



    [SerializeField]
    GameObject _honeyFactoryMain, _honeyFactoryTransparent,HoneyJar;

    [SerializeField]
    GameObject buyBtn, priceBar, storageBar, fullIcone;

    [SerializeField]
    GameObject honeyEndPose;

    [SerializeField]
    public int lockNumber = 0;

    public int nectarLimite, currentJarNumber;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    HoneyStorageController _honeyStorageController;


    // Start is called before the first frame update
    void Start()
    {

        lockNumber = PlayerPrefs.GetInt(this.gameObject.name);

        currentJarNumber = PlayerPrefs.GetInt("currentJarNumber");
    //    GameManager.gameManager.H_jar = currentJarNumber;
        //  StorageText.text = currentNectar + " / " + nectarLimite;

        if (lockNumber == 0)
        {
            if (buyBtn.activeSelf)
            {
                fullIcone.SetActive(false);
                priceBar.SetActive(true);
                _honeyFactoryTransparent.SetActive(true);
                _honeyFactoryMain.SetActive(false);
                 storageBar.SetActive(false);
                 buyBtn.SetActive(false);
            }
        }
        else
        {

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


        }

        if (other.gameObject.CompareTag("HoneyJar"))
        {
            Debug.Log("New Honey Jar Export ");


            if (other.gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                currentJarNumber++;

                PlayerPrefs.SetInt("currentJarNumber", currentJarNumber);


                currentJarNumber = PlayerPrefs.GetInt("currentJarNumber");
             //   GameManager.gameManager.H_jar = currentJarNumber;
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                InstatiateJar(honeyEndPose.transform, HoneyJar, honeyEndPose.transform);


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
        if (other.gameObject.CompareTag("HoneyJar"))
        {
            Debug.Log("New Honey Jar Exit ");
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            _honeyStorageController.SubractHoney();
            //   InstatiateJar(other.gameObject.transform, HoneyJar, honeyEndPose.transform);

        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

           
        }
    }



    public void SubtractJar(int Amount)
    {
        currentJarNumber -= Amount;
        PlayerPrefs.SetInt("currentJarNumber", currentJarNumber);
        currentJarNumber = PlayerPrefs.GetInt("currentJarNumber");
    }

    //private void StoreNecter()
    //{
    //    CancelInvoke("StoreNecter");
    //    if (nectarLimite > currentNectar && GameManager.gameManager.TotalNectar > 0)
    //    {
    //        InstatiateJar(PlayerController.playerController.NectarStartPotion, GameManager.gameManager.NecatrParticalse, priceBar.transform);
    //        GameManager.gameManager.NectarSubratct(1);
    //        currentNectar++;
    //     //   StorageText.text = currentNectar + " / " + nectarLimite;

    //        PlayerPrefs.SetInt("currentNectar", currentNectar);
    //    }
    //}



    public void InstatiateJar(Transform startPose, GameObject goldPrefab, Transform target)
    {



        // GameObject goldCoin = Instantiate(goldPrefab, startPose);

        GameObject goldCoin = GameObject.Instantiate(goldPrefab, startPose.position, Quaternion.identity);

        float Yy = UnityEngine.Random.Range(0.1f, 0.5f);
        goldCoin.transform.position = new Vector3(startPose.transform.position.x, startPose.transform.position.y + Yy, startPose.transform.position.z + Yy);


        float zPose = UnityEngine.Random.Range(0.03999999f, -1.3f);
        float xPose = UnityEngine.Random.Range(-21.25f, -18.29f);
        Vector3 newTargetPose = new Vector3(xPose, target.transform.position.y, zPose);

        target.transform.position = newTargetPose;

        StartCoroutine(CoinMovementSequence(goldCoin.transform, startPose.transform.position, target.transform.position));
    }

    public void startJarInstatiateJar(Transform startPose, GameObject goldPrefab, Transform target)
    {



        // GameObject goldCoin = Instantiate(goldPrefab, startPose);

        GameObject goldCoin = GameObject.Instantiate(goldPrefab, startPose.position, Quaternion.identity);

        float Yy = UnityEngine.Random.Range(0.1f, 0.5f);
        goldCoin.transform.position = new Vector3(startPose.transform.position.x, startPose.transform.position.y + Yy, startPose.transform.position.z + Yy);


        float zPose = UnityEngine.Random.Range(0.03999999f, -1.3f);
        float xPose = UnityEngine.Random.Range(-21.25f, -18.29f);
        Vector3 newTargetPose = new Vector3(xPose, target.transform.position.y, zPose);

        target.transform.position = newTargetPose;

        StartCoroutine(jarStartSequence(goldCoin.transform, startPose.transform.position, target.transform.position));
    }

    private IEnumerator jarStartSequence(Transform goldObj, Vector3 startPosition, Vector3 endPosition)
    {
        float time = 0;


        yield return new WaitForSeconds(0.1f);
        while (time < 1)
        {
            time += GoldCoinCpuntAnim.coinCpuntAnim.speed * Time.deltaTime;
            goldObj.position = Vector3.Lerp(startPosition, endPosition, time);

            yield return new WaitForEndOfFrame();
        }

        //   atComplete.Invoke();
        //   Destroy(goldObj.gameObject);
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
     //   Destroy(goldObj.gameObject);
    }

    void SavedData()
    {

        _honeyFactoryTransparent.SetActive(false);
        _honeyFactoryMain.SetActive(true);
        //storageBar.SetActive(true);

        if (priceBar.activeSelf)
        {
            priceBar.SetActive(false);
        }


        if (currentJarNumber > 0)
        {
            for (int i = 0; i < currentJarNumber; i++)
            {
                startJarInstatiateJar(this.gameObject.transform, HoneyJar, honeyEndPose.transform);
            }

         
        }
    }

    public void ResetGardenData()
    {
        PlayerPrefs.SetInt(this.gameObject.name, 0);
        PlayerPrefs.SetInt("currentNectar", 0);
        PlayerPrefs.SetInt("currentJarNumber", 0);
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

        if (lockNumber == 1)
        {
            if (GameManager.gameManager.H_StorageNectar > 5 )
            {


                if (GameManager.gameManager.H_JarLimite > currentJarNumber)
                {
                    fullIcone.SetActive(false);
                    _honeyFactoryMain.GetComponent<Animator>().SetBool("isIdl", false);
                }
                else
                {
                    _honeyFactoryMain.GetComponent<Animator>().SetBool("isIdl", true);

                    fullIcone.SetActive(true);
                }
                   
           


            }
            else
            {
                _honeyFactoryMain.GetComponent<Animator>().SetBool("isIdl", true);
            }
        }

       
    }



}
