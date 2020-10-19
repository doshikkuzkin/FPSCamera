using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorableObject : MonoBehaviour
{
    [SerializeField] private Color color;

    public Color Color
    {
        get => color;
        set
        {
            color = value;
        }
    }
}