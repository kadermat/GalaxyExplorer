using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Speech : MonoBehaviour
{
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
            PreAgeCalled();
            changeTexture.changeTexture();
            //changeMaterial.changeMaterial();

        });

        keywords.Add("normal", () =>
        {
            // Call the xxxx method on the earth object.
            changedTextureBack();
            changeTexture.changeTextureBack();
            //changeMaterial.changeMaterialBack();

        });

        keywords.Add("air", () =>
        {
            // Call the xxxx method on the earth object.
            AirTrafficCalled();
            changeTexture.ChangeTextureForAirTraffic();
            //changeMaterial.changeMaterialBack();

        });

        keywords.Add("traffic", () =>
        {
            // Call the xxxx method on the earth object.
            AirTrafficCalled();
            changeTexture.ChangeTextureForAirTraffic();
            //changeMaterial.changeMaterialBack();

        });

        keywords.Add("plane", () =>
        {
            // Call the xxxx method on the earth object.
            AirTrafficCalled();
            changeTexture.ChangeTextureForAirTraffic();
            //changeMaterial.changeMaterialBack();

        });


        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void PreAgeCalled()
    {
        print("PreAge Speech works");
    }

    void AirTrafficCalled()
    {
        print("Airtraffic Speech works");
    }

    void changedTextureBack()
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