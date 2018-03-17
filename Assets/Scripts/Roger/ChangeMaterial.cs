using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    public Material[] material;
    Renderer rend;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    public void changeMaterial()
    {
        rend.sharedMaterial = material[1];
    }

    public void changeMaterialBack()
    {
        rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}