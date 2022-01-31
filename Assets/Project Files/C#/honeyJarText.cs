using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class honeyJarText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager != null)
        {
            float limit = GameManager.gameManager.nectarCollectLimit;

            this.GetComponent<Text>().text = GameManager.gameManager.H_jar + " / " + GameManager.gameManager.H_JarLimite;
        }
    }
}
