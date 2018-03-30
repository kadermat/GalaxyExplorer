// Change renderer's texture each changeInterval/
// seconds from the texture array defined in the inspector.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextureSwap : MonoBehaviour
{
	private float duration = 0.5f;
	private bool changeTextureOK = false;
	private int preAgeStartTime = 19000;
	private int preAgeTime = 0;
	private int preAgeCounter = 0;
	private string BCAD = "BC";
	public Texture[] textures;
	public Text preAgeText;
    private Shader BlueMarbleShader;
    private Shader DefaultEarthShader;


    private Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
        BlueMarbleShader = Shader.Find("Custom/testShader");
        DefaultEarthShader = Shader.Find("Planets/Earth");
    }

	public void changeTexture()
	{
        if (BlueMarbleShader != null) {
            rend.material.shader = BlueMarbleShader;
        }


		StartCoroutine(DoTextureLoop());
        //	changeTextureOK = true;
        //	if (changeTextureOK)
        //	{
        //Update();
        //	enabled = true;
        //	}
        //	//for (int i = 1; i < textures.Length; i++)
        //	//{
        //	//	rend.material.mainTexture = textures[i];
        //	//	StartCoroutine(Wait());
        //	//	print("Texture on position" + i);
        //	//}




    }

    public void ChangeTextureForAirTraffic() {
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
        changeTextureBack();
    }


    public IEnumerator DoTextureLoop()
	{
		changeTextureOK = true;
		print(changeTextureOK);
		for (int i = 1; i < textures.Length; i++)
		{
			if (changeTextureOK)
			{
				if (BCAD == "BC")
				{
					preAgeTime = preAgeStartTime - preAgeCounter;
				}

				if (preAgeTime == 0 && BCAD == "BC")
				{
					BCAD = "AD";
					preAgeCounter = 0;
				}

				if (BCAD == "AD")
				{
					preAgeTime = preAgeTime + preAgeCounter;
				}
								
				preAgeText.text = "Earth: " + preAgeTime + " " + BCAD;
				rend.material.mainTexture = textures[i];
				rend.material.SetFloat("_Blend", 2.0F);
				print("Texture Name" + textures[i]);
				yield return new WaitForSeconds(duration);
				preAgeCounter = preAgeCounter + 500; 
			}
		}
		changeTextureBack();
		preAgeCounter = 0;
		BCAD = "BC";
		preAgeText.text = "";
	}

	//public IEnumerator Wait()
	//{
	//	yield return new WaitForSeconds(8.0f);
	//}

	public void changeTextureBack()
    {
		changeTextureOK = false;
		rend.material.mainTexture = textures[0];
        if (DefaultEarthShader != null)
        {
            rend.material.shader = DefaultEarthShader;
        }
    }

 //   void FixedUpdate()
 //   {
	//		if (textures.Length == 0)
	//			return;

	//		int index = Mathf.FloorToInt(Time.time / changeInterval);
	//		index = index % textures.Length;
	//		rend.material.mainTexture = textures[index];
	//}
}