using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TextureSwapSpezifikation 
{
    Shader GetSpecialShader();
    IEnumerator PictureEnumerator();

    void Preparation();

    void Postparation();

}
