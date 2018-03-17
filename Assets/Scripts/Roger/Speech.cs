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

        keywords.Add("show", () =>
        {
            // Call the xxxx method on the earth object.
            PreAgeCalled();
            changeTexture.changeTexture();
            //changeMaterial.changeMaterial();

        });

        keywords.Add("back", () =>
        {
            // Call the xxxx method on the earth object.
            changedMaterialBack();
            changeTexture.changeTextureBack();
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

    void changedMaterialBack()
    {
        print("Material is back to normal");
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