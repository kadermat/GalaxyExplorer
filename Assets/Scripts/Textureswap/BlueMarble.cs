using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMarble : TextureSwapSpezifikation
{

    private GameObject source;
    private readonly Shader specialShader;
    private bool isRunning = false;

    private float Duration = 0.1f;
    private int PreageStartTime = 19000;
    private int PreageTime = 0;
    private int PreageCounter = 0;
    public TextMesh PreageText2;
    private string BCAD = "BC";

    public BlueMarble(GameObject source)
    {
        if (source == null)
        {
            Debug.Log("source is null");
        }

        this.source = source;

        specialShader = Shader.Find("Custom/BlueMarbleShader");
        PreageText2 = GameObject.Find("PreageText2").GetComponent<TextMesh>();

}

    public bool isSpezRunning()
    {
        return isRunning;
    }


    public IEnumerator TextureLoop()
    {
        isRunning = true;
        Renderer rend = source.GetComponent<Renderer>();
        for (int i = 0; i < 68; i += 1)
        {
            Resources.UnloadUnusedAssets();

            SetPreageText();

            Texture texture = Resources.Load("Textures/preage/EarthVisualizer_" + i.ToString("00000")) as Texture;
            Debug.Log("Texture Name" + texture);
            rend.material.SetTexture("_MainTex", texture);

            Texture texture2 = Resources.Load("Textures/preage/EarthVisualizer_" + (i + 1).ToString("00000")) as Texture;
            Debug.Log("Texture2 Name" + texture2);
            rend.material.SetTexture("_SecTex", texture2);

            for (int j = 0; j < 100; j++)
            {
                rend.material.SetFloat("_Blend", 0.01F * j);
                yield return new WaitForSeconds(Duration / 100);
            }
        }

        isRunning = false;

    }

    public void Preparation()
    {
        source.GetComponent<Renderer>().material.shader = specialShader;
    }

    public void Postparation()
    {
        PreageCounter = 0;
        BCAD = "BC";
        PreageText2.text = "";

    }

    public void SetPreageText()
    {
        if (BCAD == "BC")
        {
            PreageTime = PreageStartTime - PreageCounter;
        }

        if (PreageTime == 0 && BCAD == "BC")
        {
            BCAD = "AD";
            PreageCounter = 0;
            Debug.Log("Switch");
        }

        if (BCAD == "AD")
        {
            PreageTime = 0 + PreageCounter;
        }
        //PreageText.text = "Earth: " + PreageTime + " " + BCAD;
        PreageText2.text = "Earth: " + PreageTime + " " + BCAD;
        if (BCAD == "BC" || (BCAD == "AD" && PreageTime < 2000))
        {
            PreageCounter = PreageCounter + 500;
        }
        else
        {
            PreageCounter = PreageCounter + 40;
        }
    }


}
