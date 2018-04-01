// Change renderer's texture each changeInterval/
// seconds from the texture array defined in the inspector.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextureSwap : MonoBehaviour
{
	private float Duration = 0.5f;
	private bool ChangeTextureOK = false;
	private int PreageStartTime = 19000;
	private int PreageTime = 0;
	private int PreageCounter = 0;
	private string BCAD = "BC";
	public Text PreageText;
    private Shader BlueMarbleShader;
    private Shader DefaultEarthShader;


    private Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
        BlueMarbleShader = Shader.Find("Custom/testShader");
        DefaultEarthShader = Shader.Find("Planets/Earth");
    }

	public void ChangeTextureForPreage()
	{
        if (BlueMarbleShader != null)
		{
            rend.material.shader = BlueMarbleShader;
        }

		StartCoroutine(DoTextureLoopPreage());

    }

    public void ChangeTextureForAirTraffic()
	{
        if (BlueMarbleShader != null)
        {
            rend.material.shader = BlueMarbleShader;
        }

        StartCoroutine(DoTextureLoopAirtraffic());

    }

    public IEnumerator DoTextureLoopAirtraffic()
    {
        for (int i = 0; i < 2879; i+=5)
        {
            string CounterAsString = i.ToString();
            while (CounterAsString.Length < 5)
            {
                CounterAsString = "0" + CounterAsString;
            }
			Texture texture = Resources.Load("Textures/airtraffic/AirTrafficWorldwide24h_4096x2048_" + CounterAsString) as Texture;

            rend.material.mainTexture = texture;
            //rend.material.SetFloat("_Blend", 2.0F);
            print("Texture Name" + i);
            yield return new WaitForSeconds(0.05f);
        }
        ChangeTextureBack();
    }


 //   public IEnumerator DoTextureLoopPreageOld()
	//{
	//	changeTextureOK = true;
	//	print(changeTextureOK);
	//	for (int i = 1; i < textures.Length; i++)
	//	{
	//		if (changeTextureOK)
	//		{
	//			if (BCAD == "BC")
	//			{
	//				preageTime = preageStartTime - preageCounter;
	//			}

	//			if (preageTime == 0 && BCAD == "BC")
	//			{
	//				BCAD = "AD";
	//				preageCounter = 0;
	//			}

	//			if (BCAD == "AD")
	//			{
	//				preageTime = preageTime + preageCounter;
	//			}
								
	//			preageText.text = "Earth: " + preageTime + " " + BCAD;
	//			rend.material.mainTexture = textures[i];
	//			rend.material.SetFloat("_Blend", 2.0F);
	//			print("Texture Name" + textures[i]);
	//			yield return new WaitForSeconds(duration);
	//			preageCounter = preageCounter + 500; 
	//		}
	//	}
	//	ChangeTextureBack();
	//	preageCounter = 0;
	//	BCAD = "BC";
	//	preageText.text = "";
	//}

	public IEnumerator DoTextureLoopPreage()
	{
		ChangeTextureOK = true;
		for (int i = 0; i < 3686; i += 46)
		{

			if (ChangeTextureOK == false)
			{
				break;
			}

			string CounterAsString = i.ToString();
			while (CounterAsString.Length < 5)
			{
				CounterAsString = "0" + CounterAsString;
			}
			SetPreageText();
			Texture texture = Resources.Load("Textures/preage/EarthVisualizer_" + CounterAsString) as Texture;
			rend.material.mainTexture = texture;
			yield return new WaitForSeconds(Duration);
		}
		ChangeTextureBack();
		PreageCounter = 0;
		BCAD = "BC";
		PreageText.text = "";
	}

	public void SetPreageText()
	{
		if (BCAD == "BC")
		{
			PreageTime = PreageStartTime - PreageCounter;
		}

		if (PreageTime == 0 && BCAD == "BC")
		{
			BCAD = "AD";
			PreageCounter = 0;
		}

		if (BCAD == "AD")
		{
			PreageTime = PreageTime + PreageCounter;
		}
		PreageText.text = "Earth: " + PreageTime + " " + BCAD;
		PreageCounter = PreageCounter + 500;
	}

	public void ChangeTextureBack()
    {
		ChangeTextureOK = false;
		Texture texture = Resources.Load("Textures/EarthDiffuseSpecular") as Texture;
		rend.material.mainTexture = texture;
        if (DefaultEarthShader != null)
        {
            rend.material.shader = DefaultEarthShader;
        }
    }
}