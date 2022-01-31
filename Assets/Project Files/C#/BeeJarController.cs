using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BeeJarController : MonoBehaviour
{

    float _FillRateValue = 0.5f; //progress bar starts empty

    [SerializeField]
    bool isFull = false;

    [SerializeField]
    Material objectMaterial;

    float stepSize = 0.1f; //progress is done by this value


    float balance, totalPayment, to;
    // Start is called before the first frame update
    void Start()
    {

        isFull = false;

        DOTween.Init();

        gameObject.GetComponent<Renderer>().material = objectMaterial; //new material is applied to the game object
        _FillRateValue = (1.0f / GameManager.gameManager.nectarCollectLimit) * GameManager.gameManager.TotalNectar;

       

        objectMaterial.SetFloat("Vector1_D45DB484", _FillRateValue); //initial value is set 


         to = balance + totalPayment;
       

        // objectMaterial = this.GetComponent<MeshRenderer>().

        // objectMaterial.SetFloat("Fill Level", _FillRateValue);
    }

    // Update is called once per frame
    void Update()
    {

        _FillRateValue = (1.0f / GameManager.gameManager.nectarCollectLimit) * GameManager.gameManager.TotalNectar;
        objectMaterial.SetFloat("Vector1_D45DB484", _FillRateValue); //initial value is set 


        if (_FillRateValue >= 1.0f )
        {

            if (isFull == false)
            {
                PlayerController.playerController.fullFx.SetActive(false);
                PlayerController.playerController.fullFx.SetActive(true);
                isFull = true;

                if (TutorialController.tutorialController != null)
                {
                    if (TutorialController.tutorialController.tutorialState == 2)
                    {

                        if (TutorialController.tutorialController.tutorialState != 4)
                        {
                            PlayerPrefs.SetInt("tutorialState", 3);
                            TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");

                            Debug.Log(" sell Nectar Arrow ");
                        }


                        //PlayerPrefs.SetInt("tutorialState", 3);

                        //TutorialController.tutorialController.tutorialState = PlayerPrefs.GetInt("tutorialState");
                    }
                }

            }
    
        }
        else
        {
            isFull = false;
           PlayerController.playerController.fullFx.SetActive(false);
        }
       
    }
}
