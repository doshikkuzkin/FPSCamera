using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    [CustomEditor(typeof(ColorableObject))]
    public class ColorableEditor : UnityEditor.Editor
    {
        private Color defaultColor = Color.white;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var colorable = target as ColorableObject;
            if (colorable != null)
            {
                if (GUILayout.Button("Reset Color"))
                {
                    colorable.GetComponent<Renderer>().sharedMaterial.color = colorable.Color;
                }
            }
        }
    }
}
