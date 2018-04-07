using GalaxyExplorer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTraffic : TextureSwapSpezifikation {

    private GameObject source;

    public AirTraffic(GameObject source) {
        this.source = source;
    }

	public IEnumerator PictureEnumerator()
	{
		return null;
	}


	public Shader GetSpecialShader() {
        return Shader.Find("Custom/AirTrafficShader");

    }
    public IEnumerator TextureLoop()
    {
        bool ChangeTextureOK = false;
        {//2879
            for (int i = 0; i < 200; i += 5)
            {
                ChangeTextureOK = true;
                if (ChangeTextureOK == false)
                {
                    break;
                }

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
        }
    }

    public void Preparation() {
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


    private void SetEarthSpinningStatus(bool status)
    {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");
        EarthUpClose.GetComponent<ConstantRotateAxis>().enabled = status;
    }

    private void ChangeRotationOfEarth(Vector3 rot)
    {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");

        source.transform.eulerAngles = rot;
    }

    private void SetEarthClouds(bool status)
    {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");

        foreach (Transform child in source.transform)
        {
            if (child.name.Equals("EarthClouds"))
            {
                child.gameObject.SetActive(status);
            }
        }
    }

    private void SetEarthGlow(bool status)
    {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");
        foreach (Transform child in source.transform)
        {
            if (child.name.Equals("EarthGlow"))
            {
                child.gameObject.SetActive(status);
            }
        }


    }


}
