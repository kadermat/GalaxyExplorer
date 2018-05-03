using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles the Texture swap of GameObjects. Needs to be placed on a GameObject whose Textures should be swapped
/// </summary>
public class TextureSwapManager : MonoBehaviour {

    

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





    /// <summary>
    /// Starts a Texture swap with a given spezifikation. If another spezfikation is already running, it gets terminated and the new spezfikation starts
    /// </summary>
    /// <param name="spezifikation"> Spezification of the swap to occour.</param>
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

    /// <summary>
    /// Changes the Texture of the GameObject back to the default Earth Shader. If a TextureSwapSpezfikation is running, it gets terminated its postparation() method gets called to restore the default values.
    /// </summary>
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
