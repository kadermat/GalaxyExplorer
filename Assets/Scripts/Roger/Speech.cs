using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using GalaxyExplorer;

public class Speech : MonoBehaviour
{
    TextureSwap ChangeTexture;
	OrbitUpdater OrbitUpdater;
	AsteroidRing AsteroidRing;
	KeywordRecognizer KeywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        ChangeTexture = GetComponent<TextureSwap>();
		OrbitUpdater = GetComponent<OrbitUpdater>();
		AsteroidRing = GetComponent<AsteroidRing>();

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

		Action nowFunction = () =>
		{
			// Call the xxxx method on the earth object.
			ActualTime();
			if (OrbitUpdater != null)
			{
				OrbitUpdater.ChangePlanetPositionToNow();
			}
			else
			{
				OrbitUpdater[] orbitUpdaters = GetComponentsInChildren<OrbitUpdater>();
				foreach (OrbitUpdater orbitUpdater in orbitUpdaters)
				{
					orbitUpdater.ChangePlanetPositionToNow();
				}
			}

			if (AsteroidRing != null)
			{
				AsteroidRing.StopAstroidBelt();
			}
			else
			{
				AsteroidRing[] asteroidRings = GetComponentsInChildren<AsteroidRing>();
				foreach (AsteroidRing asteroidRing in asteroidRings)
				{
					asteroidRing.StopAstroidBelt();
				}
			}
		};

		keywords.Add("now", nowFunction);

		keywords.Add("currentTime", nowFunction);

		keywords.Add("go", () =>
		{
			// Call the xxxx method on the earth object.
			Go();
			if (OrbitUpdater != null)
			{
				OrbitUpdater.RestartSolarSystemSimulation();
			}
			else
			{
				OrbitUpdater[] orbitUpdaters = GetComponentsInChildren<OrbitUpdater>();
				foreach (OrbitUpdater orbitUpdater in orbitUpdaters)
				{
					orbitUpdater.RestartSolarSystemSimulation();
				}
			}

			if (AsteroidRing != null)
			{
				AsteroidRing.RestartAstroidBelt();
			}
			else
			{
				AsteroidRing[] asteroidRings = GetComponentsInChildren<AsteroidRing>();
				foreach(AsteroidRing asteroidRing in asteroidRings)
				{
					asteroidRing.RestartAstroidBelt();
				}				
			}
		});

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