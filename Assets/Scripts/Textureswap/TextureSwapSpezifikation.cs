using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TextureSwapSpezifikation 
{

    /***
     * Texture loop over a series of pictures
     * 
     ***/
    IEnumerator TextureLoop();

    /***
     * Actions needed before executing the texture swap to prepare for correct execution.
     * z.b. apply special shader
     * 
     ***/
    void Preparation();


    /***
     * Actions needed after executing the texture swap to restore the default values.
     * Shaders and Textures do not need to be restored.
     * z.b. Enable Clouds for AirTraffic
     * 
     ***/
    void Postparation();


    /***
     * Is true while the loop is active, false otherwise
     * 
     ***/
    bool isSpezRunning();
}
