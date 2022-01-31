using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SunFlowersController : MonoBehaviour
{

    private Animator _animator;

    [SerializeField]
   public float GrowValue, lifeTime;

    public bool isGrow,isDie;


    [SerializeField]
    string flowerGorowID, FlowerLifeTimeID;

    [SerializeField]
    public GardenController gardenController;

    [SerializeField]
    private int necterNumber;



    private void Awake()
    {
        _animator = GetComponent<Animator>();

        flowerGorowID = gardenController.gameObject.name.ToString() + this.gameObject.name.ToString() + "Grow";
        FlowerLifeTimeID = gardenController.gameObject.name.ToString() + this.gameObject.name.ToString() + "lifeTime";

        GrowValue = PlayerPrefs.GetFloat(flowerGorowID);
        lifeTime = PlayerPrefs.GetFloat(FlowerLifeTimeID);
        isDie = false;

        if (GrowValue >= 1.0f)
        {

            if (isGrow = false)

            {
                isGrow = true;
            }
        
           

            gardenController.sliderBar.SetActive(false);

           
           
        }
        else
        {
        
            isGrow = false;
            gardenController.slider.enabled = true;
            if (gardenController.lockNumber == 1)
            {
                gardenController.sliderBar.SetActive(true);

            }
            else
            {
                PlayerPrefs.SetFloat(FlowerLifeTimeID, 0);
                lifeTime = PlayerPrefs.GetFloat(FlowerLifeTimeID);

                PlayerPrefs.SetFloat(flowerGorowID, 0);
                GrowValue = PlayerPrefs.GetFloat(flowerGorowID);
            }
          
        }

      
        _animator.SetFloat("Blend", lifeTime);
        _animator.SetFloat("Grow", GrowValue);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(gardenController.tag +  "  " +this.gameObject.name);

        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
        }


        if (GrowValue >= 1.0f)
        {
            _animator.SetFloat("Grow", GrowValue);
            //   Debug.Log("Grow  " + GrowValue);


            if (isDie)
            {
                FixLifeTime();
            }
            if (GrowValue >= 1.0000f && isGrow == false)
            {
                isGrow = true;

                PlayerPrefs.SetFloat(flowerGorowID, 1);
                GrowValue = PlayerPrefs.GetFloat(flowerGorowID);

          //      Debug.Log("Grow Data  " + GrowValue);
            }

        }
        else
        {

            if (isGrow == false)
            {
             

                GrowValue = gardenController.GardenGrowValue;
                _animator.SetFloat("Grow", GrowValue);
            }


        }


    }

    //PlayerController.playerController.fullFx.SetActive(false);
    //PlayerController.playerController.fullFx.SetActive(true);



    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {

        if (GameManager.gameManager.nectarCollectLimit > GameManager.gameManager.TotalNectar)
        {
            if (other.gameObject.tag == "Gun")
            {
                if (GrowValue >= 1 && lifeTime < 0.8f)
                {

                    PlayerController.playerController.nectarCollectFx.transform.position = this.gameObject.transform.position;


                    PlayerController.playerController.nactarFx.SetActive(true);
                    PlayerController.playerController.nectarCollectFx.SetActive(true);
                    PlayerController.playerController.isCollect = true;
                    lifeTime += 0.7f * Time.deltaTime;
                    _animator.SetFloat("Blend", lifeTime);


                    PlayerPrefs.SetFloat(FlowerLifeTimeID, lifeTime);
                    lifeTime = PlayerPrefs.GetFloat(FlowerLifeTimeID);

                    if (gardenController.flowrNectarNum > necterNumber)
                    {

                        // StartCoroutine(AddNectar());
                        Invoke("CallToAddNectar", 0.5f);
                       
                    }
                    else
                    {
                      



                     ///   Debug.Log("Player Exit " + lifeTime);

                        CancelInvoke("CallToAddNectar");
                       
                    }

                }
                else
                {
                    

                    PlayerController.playerController.nectarCollectFx.SetActive(false);
                  //  PlayerController.playerController.nactarFx.SetActive(false);
                    _animator.SetFloat("Blend", lifeTime);
                    PlayerPrefs.SetFloat(FlowerLifeTimeID, lifeTime);
                    lifeTime = PlayerPrefs.GetFloat(FlowerLifeTimeID);

                  


                }


            }
        }

     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
         
        }


        if (other.gameObject.tag == "Gun")
        {
            PlayerController.playerController.isCollect = false;
            PlayerController.playerController.nectarCollectFx.SetActive(false);
            PlayerController.playerController.nactarFx.SetActive(false);

            if (GrowValue >= 1)
            {
                if (lifeTime < 0.7f)
                {

                }
                else
                {
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                    this.gardenController.callFlowerDie();
                }
            }
            
               
        }
    }


    private void CallToAddNectar()
    {


        CancelInvoke("CallToAddNectar");

        if (gardenController.flowrNectarNum > necterNumber)
        {
            if (GameManager.gameManager.TotalNectar < GameManager.gameManager.nectarCollectLimit)

            {
               
              ///  GoldCoinCpuntAnim.coinCpuntAnim.GoldCoinMover(PlayerController.playerController.NectarStartPotion, GameManager.gameManager.NecatrParticalse, GameManager.gameManager.nectarEndPose);
                StartCoroutine(AddNectar());
            }
       
        }
        else
        {
           
            PlayerController.playerController.nactarFx.SetActive(false);
        }

    }

    private IEnumerator AddNectar()
    {
        yield return new WaitForSeconds(0.01f);
        GameManager.gameManager.NectarAdd(1);
        necterNumber++;
        PlayerController.playerController.textManager.Add("+" + necterNumber.ToString(), new Vector3(transform.position.x, transform.position.y + 4.5f, transform.position.z - 2.05f), "default");

    }

    public void ResetData()
    {
        PlayerPrefs.SetFloat(FlowerLifeTimeID, 0);
        lifeTime = PlayerPrefs.GetFloat(FlowerLifeTimeID);

        PlayerPrefs.SetFloat(flowerGorowID, 0);
        GrowValue = PlayerPrefs.GetFloat(flowerGorowID);
    }



    public void ResetCurrentData()
    {


        _animator.SetFloat("Blend", lifeTime);
        _animator.SetFloat("Grow", GrowValue);

        isDie = true;
   
        
    }


    void FixLifeTime()
    {

        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        if (lifeTime > 0)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            lifeTime -= 0.07f * Time.deltaTime;
            _animator.SetFloat("Blend", lifeTime);
            gardenController.healEffects2.SetActive(true);
           // Debug.Log("Liftime " + lifeTime);
        }
        else
        {
            gardenController.healEffects2.SetActive(false);

            necterNumber = 0;
            PlayerPrefs.SetFloat(FlowerLifeTimeID, 0);
            lifeTime = PlayerPrefs.GetFloat(FlowerLifeTimeID);
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        //    Debug.Log("Liftime  Done" + lifeTime);
            _animator.SetFloat("Blend", lifeTime);
            isDie = false;
        }

    }
}
