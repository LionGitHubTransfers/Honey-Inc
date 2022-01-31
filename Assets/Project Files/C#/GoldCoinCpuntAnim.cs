
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinCpuntAnim : MonoBehaviour
{
    public static GoldCoinCpuntAnim coinCpuntAnim;


    public float speed;
    //public Transform target;
    //public GameObject goldPrefab;
    public Camera _cam;


  

    // Start is called before the first frame update
    void Start()
    {
        if(_cam == null)
        {
            _cam = Camera.main;
        }

        if (coinCpuntAnim == null)
        {
            coinCpuntAnim = this;
        }
    }


    public void GoldCoinMover(Transform startPose, GameObject goldPrefab, Transform target)
    {


        

        Vector3 TargetPosition = _cam.ScreenToWorldPoint(new Vector3(target.position.x
            , target.position.y, _cam.transform.position.z * -1));

        GameObject goldCoin = GameObject.Instantiate(goldPrefab, transform.position, Quaternion.identity);
        float Yy = UnityEngine.Random.Range(0.1f, 0.5f);

        StartCoroutine(CoinMovementSequence(goldCoin.transform, startPose.transform.position, TargetPosition));

    }

    private IEnumerator CoinMovementSequence(Transform goldObj, Vector3 startPosition, Vector3 endPosition)
    {
        float time = 0;

        while (time < 1)
        {
            time += speed * Time.deltaTime;
            goldObj.position = Vector3.Lerp(startPosition, endPosition,time);

            yield return new WaitForEndOfFrame();
        }
            Destroy(goldObj, 0.5f);

    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
