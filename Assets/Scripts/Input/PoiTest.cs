using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PoiTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        createTexture();
        if (rend != null)
        {

            print("renderer found");
            Texture2D texture = Resources.Load("Textures/info/test") as Texture2D;
            print(texture);
            //rend.material.SetTexture("_MainTex", texture);


        }
        else {
            print("renderer is null");
        }

	}
	
	// Update is called once per frame
	void Update () {

    }


    private void createTexture() {
        Texture2D texture = new Texture2D(2048, 2048,TextureFormat.RGBA32,true,true);
        
        for (int i = 0; i < texture.width; i++) {
            for (int j = 0; j < i; j++) {
                texture.SetPixel(i, j, Color.red);
            }
        }
        

        byte[] textureAsByte = texture.EncodeToJPG(10);
        File.WriteAllBytes(@Path.Combine(Application.dataPath, "Resources\\Textures\\info\\test.png"), textureAsByte);
        
        //StreamWriter writer = new StreamWriter(Path.Combine(Application.dataPath, "Resources\\Textures\\info\\test.png"), true);
        //writer.Write(textureAsByte);
        //writer.Close();

    }
}
