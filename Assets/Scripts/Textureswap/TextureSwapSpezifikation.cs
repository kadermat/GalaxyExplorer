using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for A Texture swap with a set of pictures. TextureSwapManager handles the start/stop of a Texture swap
/// </summary>

public interface TextureSwapSpezifikation 
{
    /// <summary>
    /// changes the texture of a GameObject at a constant rate
    /// </summary>
    /// <returns>Returns a WaitforSeconds() after changing the Texture of the GameObject </returns>
    IEnumerator TextureLoop();


    /// <summary>
    /// Preparations needed before executing the texture swap to prepare for correct execution. z.b.apply special shader
    /// </summary>
    void Preparation();

    /// <summary>
    ///Changes needed after executing the texture swap to restore the default values. z.b.Enable Clouds \n Shaders and Textures do not need to be restored. 
    /// </summary>
    void Postparation();


    /// <summary>
    /// Control to Check whether the TextureSwapSpezifikation is active or not
    /// </summary>
    /// <returns>true while the loop is active, false otherwise</returns>
    bool isSpezRunning();
}
