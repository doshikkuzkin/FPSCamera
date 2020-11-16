using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.IO.Compression;
using UnityEngine.Networking;

namespace Editor
{
    public class PlayerDataLoader : MonoBehaviour
    {
        private static readonly int NormalTex = Shader.PropertyToID("_BumpMap");
        private static readonly string PersistentPath = Application.persistentDataPath;

        [MenuItem("Tools/Load Player Data")]
        public static void LoadPlayerData()
        {
            var outPath = Path.Combine(PersistentPath, "settings.zip");
            var uri = "https://dminsky.com/settings.zip";
            var jsonPath = Path.Combine(PersistentPath, "settings.json");

            try
            {
                if (File.Exists(outPath))
                {
                    Debug.Log("Zip is already downloaded! Path: " + outPath);
                    UnpackArchive(outPath, jsonPath);
                    LoadJson(jsonPath);
                }
                else
                {
                    UnityWebRequest request = UnityWebRequest.Get(uri);
                    request.downloadHandler = new DownloadHandlerFile(outPath);
                    var asyncOp = request.SendWebRequest();
                    asyncOp.completed += (asyncOperation) =>
                    {
                        if (request.isHttpError || request.isNetworkError)
                        {
                            Debug.Log("Error downloading file!");
                        }
                        else
                        {
                            Debug.Log("Archive loaded successfully! Path: " + outPath);
                            UnpackArchive(outPath, jsonPath);
                            LoadJson(jsonPath);
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                EditorUtility.DisplayDialog(ex.GetType().ToString(), ex.Message, "Ok");
                Debug.Log(ex.StackTrace);
            }
        }

        private static void UnpackArchive(string path, string jsonPath)
        {
            try
            {
                if (!File.Exists(jsonPath))
                {
                    ZipFile.ExtractToDirectory(path, PersistentPath);
                    Debug.Log("Archive extracted successfully!");
                }
            }
            catch (IOException ex)
            {
                EditorUtility.DisplayDialog(ex.GetType().ToString(), ex.Message, "Ok");
                Debug.Log(ex.StackTrace);
            }
        }

        private static void LoadJson(string path)
        {
            try
            {
                var jsonData = File.ReadAllText(path);
                var loadedData = JsonUtility.FromJson<LoadedData>(jsonData);
                if (loadedData != null)
                {
                    SetData(loadedData);
                }
                else
                {
                    Debug.Log("Error loading json file!");
                }
            }
            catch (FileNotFoundException ex)
            {
                EditorUtility.DisplayDialog("Could not find file!", ex.Message, "Ok");
            }
        }

        private static void SetData(LoadedData loadedData)
        {
            var players = FindObjectsOfType<FPSMovement>();
            
            if (players != null)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].MoveSpeed = loadedData.speed;
                    players[i].gameObject.name = $"{loadedData.fullName}({i})";
                }
            }

            var material = GameObject.Find("Plane")?.GetComponent<MeshRenderer>()?.sharedMaterial;
            if (material != null)
            {
                material.EnableKeyword("_NORMALMAP");

                var planeTexture = new Texture2D(2, 2);
                var textureData = Convert.FromBase64String(loadedData.base64Texture);
                planeTexture.LoadImage(textureData);

                material.SetTexture(NormalTex, planeTexture);
            }

            Debug.Log("JSON data loaded!");
        }
    }

    public class LoadedData
    {
        public float speed;
        public int health;
        public string fullName;
        public string base64Texture;
    }
}