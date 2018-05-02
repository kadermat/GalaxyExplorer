using GalaxyExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SolarSystemPlanetsSpeech : MonoBehaviour {

    OrbitUpdater aorbitUpdater;
    AsteroidRing asteroidRing;
    KeywordRecognizer KeywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        aorbitUpdater = GetComponent<OrbitUpdater>();
        asteroidRing = GetComponent<AsteroidRing>();

		/// <summary>
		/// This nowFunction checks if there is a orbitUpdater. If there is one the ChangePlanetPositionToNow() is called on this aorbitUpdater.
		/// When there isn't aorbitUpdater it checks all Children of it and call the method ChangePlanetPositionToNow() on every Children.
		/// Same procedure with astroidRings and StopAstroidBelt() method.
		/// </summary>
		Action nowFunction = () =>
        {
            ActualTime();
            if (aorbitUpdater != null)
            {
                aorbitUpdater.ChangePlanetPositionToNow();
            }
            else
            {
                OrbitUpdater[] orbitUpdaters = GetComponentsInChildren<OrbitUpdater>();
                foreach (OrbitUpdater orbitUpdater in orbitUpdaters)
                {
                    orbitUpdater.ChangePlanetPositionToNow();
                }
            }

            if (asteroidRing != null)
            {
                asteroidRing.StopAstroidBelt();
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
		/// <summary>
		/// This GoFunction checks if there is a orbitUpdater. If there is one the RestartSolarSystemSimulation() is called on this aorbitUpdater.
		/// When there isn't aorbitUpdater it checks all Children of it and call the method RestartSolarSystemSimulation() on every Children.
		/// Same procedure with astroidRings and RestartAstroidBelt() method.
		/// </summary>
		Action GoFunction = () =>
        {
            Go();
            if (aorbitUpdater != null)
            {
                aorbitUpdater.RestartSolarSystemSimulation();
            }
            else
            {
                OrbitUpdater[] orbitUpdaters = GetComponentsInChildren<OrbitUpdater>();
                foreach (OrbitUpdater orbitUpdater in orbitUpdaters)
                {
                    orbitUpdater.RestartSolarSystemSimulation();
                }
            }

            if (asteroidRing != null)
            {
                asteroidRing.RestartAstroidBelt();
            }
            else
            {
                AsteroidRing[] asteroidRings = GetComponentsInChildren<AsteroidRing>();
                foreach (AsteroidRing asteroidRing in asteroidRings)
                {
                    asteroidRing.RestartAstroidBelt();
                }
            }
        };



        keywords.Add("freeze", nowFunction);

        keywords.Add("now", nowFunction);

        keywords.Add("currentTime", nowFunction);

        keywords.Add("go", GoFunction);

        keywords.Add("unfreeze", GoFunction);





        // Tell the KeywordRecognizer about our keywords.
        KeywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(), ConfidenceLevel.Medium);

        // Register a callback for the KeywordRecognizer and start recognizing!
        KeywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        KeywordRecognizer.Start();
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
            if (aorbitUpdater == null) {
                aorbitUpdater = GetComponent<OrbitUpdater>();

            }

            if (asteroidRing == null) {
                asteroidRing = GetComponent<AsteroidRing>();

            }
            keywordAction.Invoke();
        }
    }


}
