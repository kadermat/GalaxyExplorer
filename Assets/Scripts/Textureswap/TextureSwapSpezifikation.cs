using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TextureSwapSpezifikation 
{
    IEnumerator TextureLoop();

    /***
     * 
     * 
     * 
     * 
     ***/
    void Preparation();

    void Postparation();

    bool isSpezRunning();
}
