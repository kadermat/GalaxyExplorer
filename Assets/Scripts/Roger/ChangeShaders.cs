﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShaders : MonoBehaviour
{

    public Shader shader1;
    public Shader shader2;
    public Renderer rend;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        shader1 = Shader.Find("Diffuse");
        shader2 = Shader.Find("Transparent/Diffuse");
    }

    // Update is called once per frame
    public void changeShader()
    {
        if (rend.material.shader == shader1)
            rend.material.shader = shader2;
        else
            rend.material.shader = shader1;
    }

    void Update()
    {
    }
}