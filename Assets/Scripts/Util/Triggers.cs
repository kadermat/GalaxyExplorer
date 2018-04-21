using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour {
    private GameObject earth;
    private TextureSwapManager manager;

    // Use this for initialization
    void Start () {
        earth = GameObject.Find("Earth");
        manager = earth.GetComponent<TextureSwapManager>();

    }

    // Update is called once per frame
    void Update () {
        TextureSwap changeTexture = earth.GetComponent<TextureSwap>();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 pressed");

            //changeTexture.ChangeTextureForAirTraffic();

            manager.startTextureSwap(new AirTraffic(earth));


        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2 pressed");

            //changeTexture.ChangeTextureForPreage();
            manager.startTextureSwap(new BlueMarble(earth));

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3 pressed");
            manager.ChangeTextureBack();
            //changeTexture.ChangeTextureBack();
            
        }
    }


}
