using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceText : MonoBehaviour
{

    [SerializeField]
    GardenController GardenController;



    // Start is called before the first frame update
    void Start()
    {
       gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = GardenController.GardenPrice  + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
