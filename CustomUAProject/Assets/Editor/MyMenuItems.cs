using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class MyMenuItems : MonoBehaviour
    {
        [MenuItem("Tools/Change Objects Color/Red")]
        public static void ColorRed()
        {
            ChangeColor(Color.red);
        }
        
        [MenuItem("Tools/Change Objects Color/Blue")]
        public static void ColorBlue()
        {
            ChangeColor(Color.blue);
        }
        
        [MenuItem("Tools/Change Objects Color/Green")]
        public static void ColorGreen()
        {
            ChangeColor(Color.green);
        }

        private static void ChangeColor(Color color)
        {
            var allObjects = FindObjectsOfType<ColorableObject>();
            foreach (var colorableObject in allObjects)
            {
                var renderer = colorableObject.GetComponent<Renderer>();
                renderer.sharedMaterial.color = color;
            }
        }
    }
}
