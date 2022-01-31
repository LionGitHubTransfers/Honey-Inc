using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class capaCityUpgradeBtn : MonoBehaviour, IPointerDownHandler // required interface when using 
{
    [SerializeField]
    private int _upGradePrice;

    [SerializeField]
    TMPro.TextMeshProUGUI capaCityLevelText, _upGradePriceText;

    [SerializeField]
    private int _capacityLevel;



    // Start is called before the first frame update
    void Start()
    {

        _capacityLevel = GameManager.gameManager.capaCityLevel;
        if (_capacityLevel > 9)
        {
            capaCityLevelText.text = "Level " + _capacityLevel;
        }
        else
        {
            capaCityLevelText.text = "Level 0" + _capacityLevel;
        }


        if (_capacityLevel > 0)
        {
            _upGradePrice = GameManager.gameManager.capaCityUpgradePrice * (_capacityLevel + 1);
            _upGradePriceText.text = "$" + _upGradePrice;
            Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _capacityLevel);
        }
        else
        {
            _upGradePrice = GameManager.gameManager.capaCityUpgradePrice;
            _upGradePriceText.text = "$" + _upGradePrice;
        }


        Debug.Log("Upgrade Price " + _upGradePrice);


    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMouseDown()
    {
        Debug.Log("Hello World ");
    }

    public void OnPointerDown(PointerEventData eventData)
    {



        if (GameManager.gameManager.toalCash >= _upGradePrice)
        {

            GameManager.gameManager.SubtractCash(_upGradePrice);

            GameObject MoneyFx = Instantiate(GameManager.gameManager.MoneyBlast, PlayerController.playerController.transform.position, Quaternion.identity);
            MoneyFx.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            MoneyFx.transform.transform.position = new Vector3(MoneyFx.transform.position.x, 0.8f, 10.56f);


            GameManager.gameManager.UpgradeCapacityLevel();




            _capacityLevel = GameManager.gameManager.capaCityLevel;
            if (_capacityLevel > 9)
            {
                capaCityLevelText.text = "Level " + _capacityLevel;
            }
            else
            {
                capaCityLevelText.text = "Level 0" + _capacityLevel;
            }


            if (_capacityLevel > 0)
            {
                _upGradePrice = GameManager.gameManager.capaCityUpgradePrice * (_capacityLevel + 1);
                _upGradePriceText.text = "$" + _upGradePrice;
                Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _capacityLevel);
            }
            else
            {
                _upGradePrice = GameManager.gameManager.capaCityUpgradePrice;
                _upGradePriceText.text = "$" + _upGradePrice;
            }


            Debug.Log("Upgrade Price " + _upGradePrice);



        }
        else
        {
        
            Debug.Log("Not Enough Money ");
        }

        // throw new System.NotImplementedException();
    }
}

