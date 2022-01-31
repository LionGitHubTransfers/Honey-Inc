using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gardenUpgradeBtn : MonoBehaviour, IPointerDownHandler // required interface when using 
{

    [SerializeField]
    private float _upGradePrice;

    [SerializeField]
    TMPro.TextMeshProUGUI _factroyLevelText, _upGradePriceText;

    [SerializeField]
    private int _grdenLevel;

    [SerializeField]
    private GardenController gardenController;



    void Update()
    {
        _grdenLevel = GameManager.gameManager.currentFlowerLevel;

        if (_grdenLevel > 0)
        {
            // _upGradePrice = gardenController.GardenPrice;

            _upGradePrice = gardenController.GardenPrice * (_grdenLevel + 1);
            _upGradePriceText.text = "" + _upGradePrice;
         //   Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _grdenLevel);
        }
        else
        {
            _upGradePrice = _upGradePrice = gardenController.GardenPrice;
            _upGradePriceText.text = "" + _upGradePrice;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _grdenLevel = GameManager.gameManager.currentFlowerLevel;


        

        Debug.Log("Upgrade Price " + _upGradePrice);


    }
   
    public void OnPointerDown(PointerEventData eventData)
    {



        if (GameManager.gameManager.toalCash >= _upGradePrice)
        {

           GameManager.gameManager.SubtractCash(_upGradePrice);

            gardenController.UpgradeGarden();


            Debug.Log("Upgrade this Garden Successfully " + _upGradePrice);



        }
        else
        {

            Debug.Log("Not Enough Money ");
        }

        // throw new System.NotImplementedException();
    }


}
