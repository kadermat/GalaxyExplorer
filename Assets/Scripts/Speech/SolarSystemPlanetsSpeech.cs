using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SolarSystemPlanetsSpeech : MonoBehaviour {

    ChangeMaterial changeMaterial;
    TextureSwap changeTexture;
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        changeMaterial = GetComponent<ChangeMaterial>();
        changeTexture = GetComponent<TextureSwap>();

        keywords.Add("PreAge", () =>
        {
            // Call the xxxx method on the earth object.

            //changeMaterial.changeMaterial();

        });

        keywords.Add("normal", () =>
        {
            // Call the xxxx method on the earth object.
            changeTexture.changeTextureBack();
            //changeMaterial.changeMaterialBack();

        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update () {
		
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
