using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorableObject : MonoBehaviour
{
    [SerializeField] private Color color;
    private bool isColor;

    public Color Color
    {
        get
        {
            CheckDefaultColor();
            return color;
        }
        set
        {
            CheckDefaultColor();
            GetComponent<Renderer>().sharedMaterial.color = value;
        }
    }

    private void CheckDefaultColor()
    {
        if (!isColor)
        {
            color = GetComponent<Renderer>().sharedMaterial.color;
            isColor = true;
        }
    }

}