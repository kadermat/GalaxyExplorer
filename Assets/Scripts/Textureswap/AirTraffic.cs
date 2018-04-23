using GalaxyExplorer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTraffic : TextureSwapSpezifikation {

    private GameObject source;

    private readonly Shader specialShader;
    private bool isRunning = false;

    public AirTraffic(GameObject source) {
        if (source == null)
        {
            Debug.Log("source is null");
        }

        this.source = source;

        specialShader = Shader.Find("Custom/AirTrafficShader");
    }

    public bool isSpezRunning() {
        return isRunning;
    }



    public IEnumerator TextureLoop()
    {
        
        Debug.Log("starting TextureLoop");
        isRunning = true;
        //2879 in Total
            for (int i = 0; i < 2879; i += 5)
            {
                Resources.UnloadUnusedAssets();


                string CounterAsString = i.ToString();
                while (CounterAsString.Length < 5)
                {
                    CounterAsString = "0" + CounterAsString;
                }
                Texture texture = Resources.Load("Textures/airtraffic/AirTrafficWorldwide24h_4096x2048_" + CounterAsString) as Texture;
                if (texture != null)
                {
                    source.GetComponent<Renderer>().material.mainTexture = texture;
                    //rend.material.SetFloat("_Blend", 2.0F);
                    Debug.Log("Texture Name" + i);


                    yield return new WaitForSeconds(0.05f);
                }
                else
                {
                    Debug.Log("null Texture Name" + i);
                    yield return new WaitForSeconds(0.0f);
                }
            }
        isRunning = false;
        
    }

    public void Preparation() {
        setEarthSpecialShader();
        SetEarthSpinningStatus(false);
        ChangeRotationOfEarth(Vector3.zero);
        SetEarthClouds(false);
        SetEarthGlow(false);
    }

    public void Postparation() {
        SetEarthSpinningStatus(true);
        ChangeRotationOfEarth(new Vector3(-90.0f, 0.0f, 0.0f));
        SetEarthClouds(true);
        SetEarthGlow(true);
    }



    private void setEarthSpecialShader() {
        source.GetComponent<Renderer>().material.shader = specialShader;
    }


    private void SetEarthSpinningStatus(bool status)
    {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");
        EarthUpClose.GetComponent<ConstantRotateAxis>().enabled = status;
    }

    private void ChangeRotationOfEarth(Vector3 rot)
    {
        source.transform.eulerAngles = rot;
    }

    private void SetEarthClouds(bool status)
    {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");

        foreach (Transform child in EarthUpClose.transform)
        {
            Debug.Log("childs: " + child.name);
            if (child.name.Equals("EarthClouds"))
            {
                child.gameObject.SetActive(status);
            }
        }
    }

    private void SetEarthGlow(bool status)
    {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");
        foreach (Transform child in EarthUpClose.transform)
        {

            if (child.name.Equals("EarthGlow"))
            {
                child.gameObject.SetActive(status);
            }
        }


    }


}
