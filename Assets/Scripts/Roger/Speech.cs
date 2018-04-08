using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Speech : MonoBehaviour
{
    TextureSwap ChangeTexture;
	//OrbitUpdaterTest OrbitUpdater;
	//AsteroidRingTest AsteroidRing;
	KeywordRecognizer KeywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        ChangeTexture = GetComponent<TextureSwap>();
		//OrbitUpdater = GetComponent<OrbitUpdaterTest>();
		//AsteroidRing = GetComponent<AsteroidRingTest>();

		keywords.Add("Preage", () =>
        {
            // Call the xxxx method on the earth object.
            PreageCalled();
            ChangeTexture.ChangeTextureForPreage();
        });

		keywords.Add("BlueMarble", () =>
		{
			// Call the xxxx method on the earth object.
			PreageCalled();
			ChangeTexture.ChangeTextureForPreage();
		});

		keywords.Add("stop", () =>
        {
            // Call the xxxx method on the earth object.
            ChangedTextureBack();
            ChangeTexture.ChangeTextureBack();
        });

        keywords.Add("air", () =>
        {
            // Call the xxxx method on the earth object.
            AirTrafficCalled();
            ChangeTexture.ChangeTextureForAirTraffic();

        });

        keywords.Add("traffic", () =>
        {
            // Call the xxxx method on the earth object.
            AirTrafficCalled();
            ChangeTexture.ChangeTextureForAirTraffic();
        });

        keywords.Add("plane", () =>
        {
            // Call the xxxx method on the earth object.
            AirTrafficCalled();
            ChangeTexture.ChangeTextureForAirTraffic();
        });

		//keywords.Add("now", () =>
		//{
		//	// Call the xxxx method on the earth object.
		//	ActualTime();
		//	OrbitUpdater.ChangePlanetPositionToNow();
		//	AsteroidRing.StopAstroidBelt();
		//});

		//keywords.Add("currentTime", () =>
		//{
		//	// Call the xxxx method on the earth object.
		//	ActualTime();
		//	OrbitUpdater.ChangePlanetPositionToNow();
		//	AsteroidRing.StopAstroidBelt();
		//});

		//keywords.Add("go", () =>
		//{
		//	// Call the xxxx method on the earth object.
		//	Go();
		//	OrbitUpdater.RestartSolarSystemSimulation();
		//	AsteroidRing.RestartAstroidBelt();
		//});


		// Tell the KeywordRecognizer about our keywords.
		KeywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        KeywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        KeywordRecognizer.Start();
    }

    void PreageCalled()
    {
        print("Preage Speech works");
    }

    void AirTrafficCalled()
    {
        print("Airtraffic Speech works");
    }

    void ChangedTextureBack()
    {
        print("Texture is back to normal");
    }

	void ActualTime()
	{
		print("Changed Planet Position to actual time");
	}

	void Go()
	{
		print("SolarSystemView changed back to normal");
	}

	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}