using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class factoryUpgradeBtn : MonoBehaviour, IPointerDownHandler // required interface when using 
{
    [SerializeField]
    private int _upGradePrice;

    [SerializeField]
    TMPro.TextMeshProUGUI _factroyLevelText, _upGradePriceText;

    [SerializeField]
    private int _factroyLevel;



    // Start is called before the first frame update
    void Start()
    {
        _factroyLevel = GameManager.gameManager.currentFactoryLevel;
        if (_factroyLevel > 9)
        {
            _factroyLevelText.text = "Level " + _factroyLevel;
        }
        else
        {
            _factroyLevelText.text = "Level 0" + _factroyLevel;
        }

        if (_factroyLevel > 0)
        {
            _upGradePrice = GameManager.gameManager.factoryUpgradePrice * (_factroyLevel + 1);
            _upGradePriceText.text = "$" + _upGradePrice;
            Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _factroyLevel);
        }
        else
        {
            _upGradePrice = GameManager.gameManager.factoryUpgradePrice;
            _upGradePriceText.text = "$" + _upGradePrice;
        }

        Debug.Log("Upgrade Price " + _upGradePrice);


    }
    public void OnPointerDown(PointerEventData eventData)
    {



        if (GameManager.gameManager.toalCash >= _upGradePrice)
        {

            GameManager.gameManager.SubtractCash(_upGradePrice);

            GameObject MoneyFx = Instantiate(GameManager.gameManager.MoneyBlast, PlayerController.playerController.transform.position, Quaternion.identity);
            MoneyFx.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            MoneyFx.transform.transform.position = new Vector3(MoneyFx.transform.position.x, 0.8f, 10.56f);


            GameManager.gameManager.UpgradeFactoryLevel();




            _factroyLevel = GameManager.gameManager.currentFactoryLevel;
            if (_factroyLevel > 9)
            {
                _factroyLevelText.text = "Level " + _factroyLevel;
            }
            else
            {
                _factroyLevelText.text = "Level 0" + _factroyLevel;
            }


            if (_factroyLevel > 0)
            {
                _upGradePrice = GameManager.gameManager.factoryUpgradePrice * (_factroyLevel + 1);
                _upGradePriceText.text = "$" + _upGradePrice;
                Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _factroyLevel);
            }
            else
            {
                _upGradePrice = GameManager.gameManager.factoryUpgradePrice;
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
