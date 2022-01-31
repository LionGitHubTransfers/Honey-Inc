using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltScripts : MonoBehaviour
{

    [SerializeField]
    Material textureMat;
    [SerializeField]
    Texture[] beltTextures;

    [SerializeField]
    private float beltSpeed;
    [SerializeField]
    int i;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startTextureAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        if (i == beltTextures.Length)
        {
            StartCoroutine(startTextureAnimation());
            i = 0;
        }
    }


    IEnumerator startTextureAnimation()
    {

        for (i = 0; i < beltTextures.Length; i++)
        {
            yield return new WaitForSeconds(beltSpeed);
            textureMat.SetTexture("_BaseMap", beltTextures[i]);
            Debug.Log("Change Texture ");
        }



    }

}
