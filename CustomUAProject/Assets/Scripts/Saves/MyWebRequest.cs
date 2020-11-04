using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

namespace Saves
{
    public class MyWebRequest : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("DownloadFile"))
            {
                StartCoroutine(DownloadFile());
            }
        }

        IEnumerator DownloadFile()
        {
            string uri = "https://dminsky.com/rock_normal.jpg";
            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Error downloading file!");
            }
            else
            {
                Debug.Log("Path: " + Application.persistentDataPath);
                
                string outPath = Path.Combine(Application.persistentDataPath, "rock_normal.jpg");
                File.WriteAllBytes(outPath, request.downloadHandler.data);
            }
        }
    }
}