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
    private TextureSwapManager manager;

    // Use this for initialization
    void Start()
    {
        ChangeTexture = GetComponent<TextureSwap>();
		OrbitUpdater = GetComponent<OrbitUpdater>();
		AsteroidRing = GetComponent<AsteroidRing>();
        manager = GetComponent<TextureSwapManager>();

		keywords.Add("Preage", () =>
        {
            manager.startTextureSwap(new BlueMarble(GameObject.Find("Earth")));
            PreageCalled();
        });

		keywords.Add("BlueMarble", () =>
		{
            manager.startTextureSwap(new BlueMarble(GameObject.Find("Earth")));
            PreageCalled();
        });


        keywords.Add("two", () =>
        {
            manager.startTextureSwap(new BlueMarble(GameObject.Find("Earth")));
            PreageCalled();
        });

        keywords.Add("stop", () =>
        {
            manager.ChangeTextureBack();
        });

        keywords.Add("air", () =>
        {
            AirTrafficCalled();
            manager.startTextureSwap(new AirTraffic(GameObject.Find("Earth")));
        });

        keywords.Add("traffic", () =>
        {
            AirTrafficCalled();
            manager.startTextureSwap(new AirTraffic(GameObject.Find("Earth")));
        });

        keywords.Add("plane", () =>
        {
            AirTrafficCalled();
            manager.startTextureSwap(new AirTraffic(GameObject.Find("Earth")));
        });

        keywords.Add("one", () =>
        {
            AirTrafficCalled();
            manager.startTextureSwap(new AirTraffic(GameObject.Find("Earth")));
        });

		
		// Tell the KeywordRecognizer about our keywords.
		KeywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(), ConfidenceLevel.Low);

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


	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}