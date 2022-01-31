using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.


public class BeeSpeedUpgrade : MonoBehaviour, IPointerDownHandler // required interface when using the 
{

    [SerializeField]
    private int _upGradePrice;

    [SerializeField]
    TMPro.TextMeshProUGUI _speedLevelText, _upGradePriceText;

    [SerializeField]
    private int _SpeedLevel;



    // Start is called before the first frame update
    void Start()
    {


        _SpeedLevel = GameManager.gameManager.speedLevel;
        if (_SpeedLevel > 9)
        {
            _speedLevelText.text = "Level " + _SpeedLevel;
        }
        else
        {
            _speedLevelText.text = "Level 0" + _SpeedLevel;
        }


        if (_SpeedLevel > 0)
        {
            _upGradePrice = GameManager.gameManager.speedUpgradePrice * (_SpeedLevel + 1);
            _upGradePriceText.text = "$" + _upGradePrice;
            Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _SpeedLevel);
        }
        else
        {
            _upGradePrice = GameManager.gameManager.speedUpgradePrice;
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
            GameManager.gameManager.UpgradeSpeedLevel();


            _SpeedLevel = GameManager.gameManager.speedLevel;

            if (_SpeedLevel > 9)
            {
                _speedLevelText.text = "Level " + _SpeedLevel;
            }
            else
            {
                _speedLevelText.text = "Level 0" + _SpeedLevel;
            }


         
            if (_SpeedLevel > 0)
            {
                _upGradePrice = GameManager.gameManager.speedUpgradePrice * (_SpeedLevel + 1);
                Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _SpeedLevel);
                _upGradePriceText.text = "$" + _upGradePrice;
            }
            else
            {
                _upGradePrice = GameManager.gameManager.speedUpgradePrice;
                _upGradePriceText.text = "$" + _upGradePrice;
            }
        }
        else
        {
            //GameManager.gameManager.UpgradeSpeedLevel();
            //GameObject MoneyFx = Instantiate(GameManager.gameManager.MoneyBlast, PlayerController.playerController.transform.position, Quaternion.identity);
            //MoneyFx.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            //MoneyFx.transform.transform.position = new Vector3(MoneyFx.transform.position.x, 0.8f, 10.56f);



            

            Debug.Log("Not Enough Money ");
        }
       
        // throw new System.NotImplementedException();
    }
}
