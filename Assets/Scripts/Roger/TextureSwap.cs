// Change renderer's texture each changeInterval/
// seconds from the texture array defined in the inspector.

using UnityEngine;
using System.Collections;

public class TextureSwap : MonoBehaviour
{
    public Texture[] textures;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void changeTexture()
    {
        rend.material.mainTexture = textures[1];
    }

    public void changeTextureBack()
    {
        rend.material.mainTexture = textures[0];
    }
    void Update()
    {
    }
}