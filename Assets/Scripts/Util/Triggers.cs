using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour {
    GameObject Earth;
	// Use this for initialization
	void Start () {
        Earth = GameObject.Find("Earth");

    }

    // Update is called once per frame
    void Update () {
        TextureSwap changeTexture = Earth.GetComponent<TextureSwap>();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 pressed");

            changeTexture.ChangeTextureForAirTraffic();


        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("1 pressed");

            changeTexture.ChangeTextureForPreage();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("1 pressed");

            changeTexture.ChangeTextureBack();
            
        }
    }


}
