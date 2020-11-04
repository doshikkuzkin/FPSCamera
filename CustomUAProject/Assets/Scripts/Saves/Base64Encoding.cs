using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEngine.Networking;

public class Base64Encoding : MonoBehaviour
{
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    
    //[MenuItem("Tools/ConvertToBase64")]
    public static void ConvertToBase64()
    {
        string dataPath = Application.persistentDataPath;
        Debug.Log(dataPath);

        //Read bytes from file
        //byte[] myBytes = File.ReadAllBytes("SavesPractice/myBinaryAsset.asset");
        //Convert byte[] to text
        //string myConvertedBytes = Convert.ToBase64String(myBytes);
        
        //Write text to file
        //File.WriteAllText("SavesPractice/myTextAsset.txt", myConvertedBytes);

        //Read text from file
        string checkMyText = File.ReadAllText("SavesPractice/myTextAsset.txt");
        //Convert text to byte[]
        byte[] checkMyBytes = Convert.FromBase64String(checkMyText);
        //Write bytes to file
        File.WriteAllBytes("SavesPractice/checkMyAsset.asset", checkMyBytes);

        //Write byte[] to file
        //File.WriteAllBytes("SavesPractice/myBinaryAsset.asset", new byte[]{10, 20, 30, 40});
    }

    //[MenuItem("Tools/ArchiveDirectory")]
    public static void ArchiveDirectory()
    {
        //Add csc to use System.IO.Compression
        //string fileContent = "-r:System.IO.Compression.FileSystem.dll";
        //File.WriteAllText("Assets/csc.rsp", fileContent);
        //ArchiveDirectory
        ZipFile.CreateFromDirectory("SavesPractice", "savesArchive.zip");
        
        //extract - ZipFile.ExtractToDirectory(file, directory)
    }
    
    //[MenuItem("Tools/LoadTexture")]
    public static void LoadTexture()
    {
        byte[] textureData = File.ReadAllBytes("Road.jpg");
        Texture2D texture2D = new Texture2D(2,2);
        texture2D.LoadImage(textureData);

        var material = GameObject.Find("Plane").GetComponent<MeshRenderer>().sharedMaterial;
        material.SetTexture(MainTex, texture2D);
        
        //Enable keywords if texture doesn't assign
        //material.EnableKeyword("_NORMALMAP");
    }
    
    //[MenuItem("Tools/CreateJSON")]
    public static void CreateJSON()
    {
        MyData data = new MyData();
        data.speed = 1;
        data.health = 100;
        data.name = "Doshik";

        //Serialize object
        string JSONData = JsonUtility.ToJson(data);
        Debug.Log(JSONData);

        //Deserialize object
        MyData readData = JsonUtility.FromJson<MyData>(JSONData);
        Debug.Log("Read " + readData.name + " data!");
    }
    
    //[MenuItem("Tools/WebRequest")]
    public static void WebRequest()
    {
        string uri = "https://dminsky.com/rock_normal.jpg";
        UnityWebRequest request = UnityWebRequest.Get(uri);
        string outPath = Path.Combine(Application.persistentDataPath, "rock_normal_async.jpg");
        request.downloadHandler = new DownloadHandlerFile(outPath);
        var asyncOp = request.SendWebRequest();
        asyncOp.completed += (aO) =>
        {
            if (!request.isHttpError && !request.isNetworkError)
            {
                Debug.Log("Completed");
            }
        };
    }
}

[Serializable]
public class MyData
{
    public float health;
    public float speed;
    public string name;
    public string[] items = new string[] {"plate", "spoon", "water"};
}
