using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class JSONUtil
{
    public static void WrileFile(string filename, string content)
    {
        File.WriteAllText(filename, content);
    }

    public static T LoadDataFromJson<T>(string filename)
    {
        TextAsset textFile = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        //bool fileExist = File.Exists("Assets);
        string fileContent = textFile.ToString();

        return JsonUtility.FromJson<T>(fileContent);
    }
}
