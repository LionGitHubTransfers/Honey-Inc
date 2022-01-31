using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guirao.UltimateTextDamage;
public class PlayerController : MonoBehaviour
{

    public UltimateTextDamageManager textManager;

    public static PlayerController playerController;


    public GameObject nectarCollectFx,fullFx,nactarFx,beeObj;

    public Transform NectarStartPotion;

    public VariableJoystick variableJoystick;

    public bool isFlay, isCollect;

    private CharacterController controller;
    public Vector3 playerVelocity;

    public Quaternion startRotation;

    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    public float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;


    public Animator _beeAnimator;


    [SerializeField]
    private FactoryController _factoryController;


    void Awake()
    {
       

        nectarCollectFx.SetActive(false);
        fullFx.SetActive(false);

        nactarFx.SetActive(false);

        if (playerController == null)
        {
            playerController = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        startRotation = this.transform.rotation;

        controller = gameObject.GetComponent<CharacterController>();
        isCollect = false;
        isFlay = false;
        playerSpeed = GameManager.gameManager.maxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Jar")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            if (GameManager.gameManager.H_JarLimite > GameManager.gameManager.H_jar)
            {
              //  nactarFx.SetActive(true);
                fullFx.SetActive(false);
                InstatiateJar(other.gameObject.transform, GameManager.gameManager.HoneyJarFx, GameManager.gameManager.hJarEndPos.transform);

                _factoryController.SubtractJar(1);
                Destroy(other.gameObject, 0.1f);
                GameManager.gameManager.addJar(1);
            }
            else
            {
              //  nactarFx.SetActive(false);
                fullFx.SetActive(true);
            }
      
        }
    }


    public void InstatiateJar(Transform startPose, GameObject goldPrefab, Transform target)
    {

        Vector3 TargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(target.position.x
       , target.position.y, Camera.main.transform.position.z * -1));

        GameObject goldCoin = GameObject.Instantiate(goldPrefab, startPose.position, Quaternion.identity);


        StartCoroutine(CoinMovementSequence(goldCoin.transform, startPose.transform.position, TargetPosition));
    }


    private IEnumerator CoinMovementSequence(Transform goldObj, Vector3 startPosition, Vector3 endPosition)
    {
        float time = 0;
        while (time < 1)
        {
            time += 1.0f * Time.deltaTime;
            goldObj.position = Vector3.Lerp(startPosition, endPosition, time);

            yield return new WaitForEndOfFrame();
        }
        nactarFx.SetActive(false);
        //   atComplete.Invoke();
        Destroy(goldObj.gameObject,0.1f);
    }





    // Update is called once per frame
    void Update()
    {



        _beeAnimator.SetBool("isFly", isFlay);
        _beeAnimator.SetBool("isCollect", isCollect);

        if (GameManager.gameManager.VirtualJoyStic.activeSelf)
        {
            Vector3 move = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical);
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
        }
       

    

    }

}

