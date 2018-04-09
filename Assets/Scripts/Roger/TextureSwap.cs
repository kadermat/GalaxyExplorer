// Change renderer's texture each changeInterval/
// seconds from the texture array defined in the inspector.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GalaxyExplorer;

public class TextureSwap : MonoBehaviour
{
	private float Duration = 0.1f;
	private bool ChangeTextureOK = false;
	private int PreageStartTime = 19000;
	private int PreageTime = 0;
	private int PreageCounter = 0;
	private string BCAD = "BC";
	//public Text PreageText;
	public TextMesh PreageText2;
	private Shader BlueMarbleShader;
	private Shader AirTrafficShader;
	private Shader DefaultEarthShader;
    private Texture defaultTexture;

    private Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
        AirTrafficShader = Shader.Find("Custom/AirTrafficShader");
		BlueMarbleShader = Shader.Find("Custom/BlueMarbleShader");
		DefaultEarthShader = Shader.Find("Planets/Earth");
        defaultTexture = rend.material.mainTexture;
		//TextMesh PreageText2 = GameObject.Find("PreageText2").GetComponent<TextMesh>();
		//PreageText2.text = "Bye World";
	}

   

    public void ChangeTextureForPreage()
	{
        ChangeTextureOK = false;


        if (BlueMarbleShader != null)
		{
            rend.material.shader = BlueMarbleShader;
        }

		StartCoroutine(DoTextureLoopPreage());

    }

    public void ChangeTextureForAirTraffic()
	{
        ChangeTextureOK = false;



        if (AirTrafficShader != null)
        {
            Debug.Log("Airtraffic shader found");
            rend.material.shader = AirTrafficShader;
        }
        else {
            Debug.Log("Airtraffic shader missing");

        }
        PrepareEarthModelForAirtraffic();
        StartCoroutine(DoTextureLoopAirtraffic());

    }

    public IEnumerator DoTextureLoopAirtraffic()
    {
        for (int i = 0; i < 2879; i+=5)
        {

            Resources.UnloadUnusedAssets();
            ChangeTextureOK = true;
            if (ChangeTextureOK == false)
            {
                break;
            }

            string CounterAsString = i.ToString();
            while (CounterAsString.Length < 5)
            {
                CounterAsString = "0" + CounterAsString;
            }
			Texture texture = Resources.Load("Textures/airtraffic/AirTrafficWorldwide24h_4096x2048_" + CounterAsString) as Texture;
            if (texture != null)
            {
                rend.material.mainTexture = texture;

                print("Texture Name" + i);


                yield return new WaitForSeconds(0.1f);
            }
            else {
                print("null Texture Name" + i);
                yield return new WaitForSeconds(0.0f);
            }


        }
        revertStatusToNormal();
        ChangeTextureBack();
    }

	public IEnumerator DoTextureLoopPreage()
	{
        ChangeTextureOK = true;
		for (int i = 0; i < 68; i += 1)
		{

			if (ChangeTextureOK == false)
			{
				break;
			}

			//string CounterAsString = i.ToString();
			//while (CounterAsString.Length < 5)
			//{
			//	CounterAsString = "0" + CounterAsString;
			//}
			SetPreageText();
			
			Texture texture = Resources.Load("Textures/preage/EarthVisualizer_" + i.ToString("00000")) as Texture;
			print("Texture Name" + texture);
			rend.material.SetTexture("_MainTex", texture);

			Texture texture2 = Resources.Load("Textures/preage/EarthVisualizer_" + (i + 1).ToString("00000")) as Texture;
			print("Texture2 Name" + texture2);
			rend.material.SetTexture("_SecTex", texture2);

			for(int j = 0; j < 100; j++)
			{
				rend.material.SetFloat("_Blend", 0.01F * j);
				yield return new WaitForSeconds(Duration / 100);
			}
		}
		ChangeTextureBack();
		PreageCounter = 0;
		BCAD = "BC";
		//PreageText.text = "";
		PreageText2.text = "";
	}


    private void cleanForNextTextureLoop() {

        revertStatusToNormal();
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
			print("Switch");
		}

		if (BCAD == "AD")
		{
			PreageTime = 0 + PreageCounter;
		}
		//PreageText.text = "Earth: " + PreageTime + " " + BCAD;
		PreageText2.text = "Earth: " + PreageTime + " " + BCAD;
		if (BCAD == "BC" || (BCAD == "AD" && PreageTime < 2000))
		{			
			PreageCounter = PreageCounter + 500;
		}
		else
		{
			PreageCounter = PreageCounter + 40;
		}
	}

    /*
     * Reverts the status earth back to default. Call this before starting a new textureloop
     * 
     */

	public void ChangeTextureBack()
    {
		ChangeTextureOK = false;
		rend.material.mainTexture = defaultTexture;
        if (DefaultEarthShader != null)
        {
            rend.material.shader = DefaultEarthShader;
        }
        else {
            print("Defaultshader is null");
        }
        StopAllCoroutines();
        revertStatusToNormal();
        PreageCounter = 0;
        //PreageText.text = "";
		PreageText2.text = "";

	}

    private void revertStatusToNormal() {

        SetEarthSpinningStatus(true);
        ChangeRotationOfEarth(new Vector3(-90.0f,0.0f, 0.0f));
        SetEarthClouds(true);
        SetEarthGlow(true);
    }



    private void PrepareEarthModelForAirtraffic() {
        SetEarthSpinningStatus(false);
        ChangeRotationOfEarth(Vector3.zero);
        SetEarthClouds(false);
        SetEarthGlow(false);
    }

    private void SetEarthSpinningStatus(bool status) {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");
        EarthUpClose.GetComponent<ConstantRotateAxis>().enabled = status;
    }

    private void ChangeRotationOfEarth(Vector3 rot) {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");
        print("Earth up close found");
        transform.eulerAngles = rot;
    }

    private void SetEarthClouds(bool status) {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");

        foreach (Transform child in EarthUpClose.transform)
        {
            if (child.name.Equals("EarthClouds"))
            {
                Debug.Log("set clouds to : " + status.ToString());
                child.gameObject.SetActive(status);
            } else {
                Debug.Log("child name: : " + child.name);

            }
        }
    }

    private void SetEarthGlow(bool status) {
        GameObject EarthUpClose = GameObject.Find("EarthUpClose");
        foreach (Transform child in EarthUpClose.transform)
        {
            if (child.name.Equals("EarthGlow")) {
                child.gameObject.SetActive(status);
            }
        }


    }


}