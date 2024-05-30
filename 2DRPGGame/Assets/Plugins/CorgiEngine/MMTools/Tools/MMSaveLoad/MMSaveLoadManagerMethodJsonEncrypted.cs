using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class MMSaveLoadManagerMethodJsonEncrypted : MMSaveLoadManagerEncrypter, IMMSaveLoadManagerMethod
{
    public void Save(object objectToSave, FileStream saveFile)
    {
        string json = JsonUtility.ToJson(objectToSave);
        // 如果您更喜欢使用NewtonSoft的JSON库，请取消对下面一行的注释，并对上面一行进行注释
        //string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectToSave);
        using (MemoryStream memoryStream = new MemoryStream())
        using (StreamWriter streamWriter = new StreamWriter(memoryStream))
        {
            streamWriter.Write(json);
            streamWriter.Flush();
            memoryStream.Position = 0;
            Encrypt(memoryStream, saveFile, Key);
        }
        saveFile.Close();
    }

    public object Load(Type objectType, FileStream saveFile)
    {
        object savedObject = null;
        using (MemoryStream memoryStream = new MemoryStream())
        using (StreamReader streamReader = new StreamReader(memoryStream))
        {
            try
            {
                Decrypt(saveFile, memoryStream, Key);
            }
            catch (CryptographicException ce)
            {
                Debug.LogError("[MMSaveLoadManager] Encryption key error: " + ce.Message);
                return null;
            }
            memoryStream.Position = 0;
            savedObject = JsonUtility.FromJson(streamReader.ReadToEnd(), objectType);
            // 如果您更喜欢使用NewtonSoft的JSON库，请取消对下面一行的注释，并对上面一行进行注释
            //savedObject = Newtonsoft.Json.JsonConvert.DeserializeObject(sr.ReadToEnd(), objectType); 
        }
        saveFile.Close();
        return savedObject;
    }
}
