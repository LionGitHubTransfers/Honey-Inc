﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class StorageUpgradeBtn : MonoBehaviour, IPointerDownHandler // required interface when using 
{
    [SerializeField]
    private int _upGradePrice;

    [SerializeField]
    TMPro.TextMeshProUGUI _storageLevelText, _upGradePriceText;

    [SerializeField]
    private int _storageLevel;



    // Start is called before the first frame update
    void Start()
    {
        _storageLevel = GameManager.gameManager.currentStorageLevel;
        if (_storageLevel > 9)
        {
            _storageLevelText.text = "Level " + _storageLevel;
        }
        else
        {
            _storageLevelText.text = "Level 0" + _storageLevel;
        }

        if (_storageLevel > 0)
        {
            _upGradePrice = GameManager.gameManager.storageUpgradePrice * (_storageLevel + 1);
            _upGradePriceText.text = "$" + _upGradePrice;
            Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _storageLevel);
        }
        else
        {
            _upGradePrice = GameManager.gameManager.storageUpgradePrice;
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


            GameManager.gameManager.UpgradeStorageLevel();




            _storageLevel = GameManager.gameManager.currentStorageLevel;
            if (_storageLevel > 9)
            {
                _storageLevelText.text = "Level " + _storageLevel;
            }
            else
            {
                _storageLevelText.text = "Level 0" + _storageLevel;
            }


            if (_storageLevel > 0)
            {
                _upGradePrice = GameManager.gameManager.storageUpgradePrice * (_storageLevel + 1);
                _upGradePriceText.text = "$" + _upGradePrice;
                Debug.Log("Upgrade Price " + _upGradePrice + "Speed Level " + _storageLevel);
            }
            else
            {
                _upGradePrice = GameManager.gameManager.storageUpgradePrice;
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
