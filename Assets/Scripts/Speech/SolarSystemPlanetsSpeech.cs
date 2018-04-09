using GalaxyExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SolarSystemPlanetsSpeech : MonoBehaviour {

    OrbitUpdater aobitUpdater;
    AsteroidRing asteroidRing;
    KeywordRecognizer KeywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        aobitUpdater = GetComponent<OrbitUpdater>();
        asteroidRing = GetComponent<AsteroidRing>();

        Action nowFunction = () =>
        {
            // Call the xxxx method on the earth object.
            ActualTime();
            if (aobitUpdater != null)
            {
                aobitUpdater.ChangePlanetPositionToNow();
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

        Action GoFunction = () =>
        {
            // Call the xxxx method on the earth object.
            Go();
            if (aobitUpdater != null)
            {
                aobitUpdater.RestartSolarSystemSimulation();
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


        keywords.Add("n", nowFunction);

        keywords.Add("freeze", nowFunction);

        keywords.Add("now", nowFunction);

        keywords.Add("currentTime", nowFunction);

        keywords.Add("go", GoFunction);

        keywords.Add("unfreeze", GoFunction);





        // Tell the KeywordRecognizer about our keywords.
        KeywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

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
            if (aobitUpdater == null) {
                aobitUpdater = GetComponent<OrbitUpdater>();

            }

            if (asteroidRing == null) {
                asteroidRing = GetComponent<AsteroidRing>();

            }
            keywordAction.Invoke();
        }
    }


}
