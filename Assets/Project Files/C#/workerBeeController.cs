using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class workerBeeController : MonoBehaviour
{

    public float speed = 20f;
    public float waitTime,lookTime;

    [SerializeField]
    GardenController GardenController;

    [SerializeField]
    public GameObject flowerGrp,startPositon;

    [SerializeField]
    private GameObject[] Flowers;

    [SerializeField]
    private GameObject HireBanner;

    [SerializeField]
    private int beelockNumber;

   [SerializeField]
   Transform  currentFlower;

    [SerializeField]
    public int flowersNumber,targetNumber;

    [SerializeField]
    float dist;

    [SerializeField]
    private bool startPolling,isPooling;
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    GardenController _gardenController;




    // Start is called before the first frame update
    void Start()
    {

        targetNumber = 0;
        startPolling = false;
        isPooling = true;
        navMeshAgent = GetComponent<NavMeshAgent>();

        flowersNumber = flowerGrp.gameObject.transform.childCount;
        flowersNumber -= 4;
        Searching();
    }

    // Update is called once per frame
    void Update()
    {



        if (flowersNumber != targetNumber)
        {
            Partolling();
        }
        else
        {


            if (dist > 0.4911933f)
            {
                startPolling = false;
                currentFlower = startPositon.gameObject.transform;
                dist = Vector3.Distance(currentFlower.position, transform.position);

                float move = speed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, currentFlower.position, move * Time.deltaTime);


                Vector3 lTargetDir = currentFlower.position - transform.position;
                lTargetDir.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * lookTime);

              //  currentFlower.gameObject.GetComponent<SunFlowersController>().gardenController.resetCurrentData();
            }
            else
            {

                if (isPooling)
                {
                    isPooling = true;
                    this.transform.position = startPositon.transform.position;
                    this.transform.rotation = startPositon.transform.rotation;
                    _gardenController.resetCurrentData();
                    targetNumber = 0;
                    this.gameObject.GetComponent<workerBeeController>().enabled = false;
                }

            }
           
        }
           



    }


    void Searching()
    {

        CancelInvoke("Searching");
        if (flowersNumber != targetNumber && flowerGrp.gameObject.transform.GetChild(targetNumber).gameObject != null)
        {
            startPolling = false;
            targetNumber++;
         
                currentFlower = flowerGrp.gameObject.transform.GetChild(targetNumber).gameObject.transform;
                dist = Vector3.Distance(currentFlower.position, transform.position);
                Partolling();
                //Debug.Log("serching  " + targetNumber);
        }
        else
        {
            
        }
               
           

       
    }


    void Partolling()
    {
        if (currentFlower != null)
        {


           dist  = Vector3.Distance(currentFlower.position, transform.position);

            if (dist > 1.5f)
            {
                float move = speed * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, currentFlower.position, move * Time.deltaTime);


                Vector3 lTargetDir = currentFlower.position - transform.position;
                lTargetDir.y = 0.0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * lookTime);
            }
            else
            {



                if (flowersNumber != targetNumber)
                {
                    if (startPolling == false)
                    {
                        startPolling = true;
                        Invoke("Searching", waitTime);
                    }

                }





            }
          
        }

        //flowersNumber = flowerGrp.gameObject.transform.childCount;
    }


}
