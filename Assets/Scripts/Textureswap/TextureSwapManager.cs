using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSwapManager : MonoBehaviour {

    // Use this for initialization

    private TextureSwapSpezifikation spezifikationRunning;
    private Shader DefaultEarthShader;
    private Texture defaultTexture;
    void Start () {
 

        DefaultEarthShader = Shader.Find("Planets/Earth");
        defaultTexture = gameObject.GetComponent<Renderer>().material.mainTexture;
        spezifikationRunning = null;
	}

  

	void Update () {

        if (spezifikationRunning != null && spezifikationRunning.isSpezRunning() == false) {
            print("restoring default");
            StopAllCoroutines();
            spezifikationRunning.Postparation();
            ChangeTextureBack();
            spezifikationRunning = null;

        }


    }






    public void  startTextureSwap(TextureSwapSpezifikation spezifikation) {
        if (spezifikation == null) {
            print("spez is null");
        }


        if (spezifikationRunning != null)
        {
            StopCoroutine(spezifikationRunning.TextureLoop());

            spezifikationRunning.Postparation();
            StopAllCoroutines();
            ChangeTextureBack();

        }

        spezifikation.Preparation();


        if (spezifikation.TextureLoop() == null) {
            Debug.Log("loop is null");
        }
        StartCoroutine(spezifikation.TextureLoop());
        spezifikationRunning = spezifikation;
    }

    public void ChangeTextureBack()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.mainTexture = defaultTexture;
        if (DefaultEarthShader != null)
        {
            rend.material.shader = DefaultEarthShader;
        }
        else
        {
            print("Defaultshader is null");
        }

        if (spezifikationRunning != null) {
            StopAllCoroutines();
            spezifikationRunning.Postparation();
            spezifikationRunning = null;
        }


    }

}
