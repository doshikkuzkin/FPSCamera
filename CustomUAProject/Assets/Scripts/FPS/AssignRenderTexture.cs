using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AssignRenderTexture : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Start()
    {
        RenderTexture renderTexture = new RenderTexture(512, 512, 5);
        Material material = GetComponent<Renderer>().material;
        material.SetTexture(MainTex, renderTexture);
        camera.targetTexture = renderTexture;
    }
}
