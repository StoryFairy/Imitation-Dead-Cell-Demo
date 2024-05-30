using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using UnityEngine;

public class MMSaveLoadManagerMethodBinaryEncrypted : MMSaveLoadManagerEncrypter, IMMSaveLoadManagerMethod
{
    public void Save(object objectToSave, FileStream saveFile)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();
        formatter.Serialize(memoryStream, objectToSave);
        memoryStream.Position = 0;
        Encrypt(memoryStream, saveFile, Key);
        saveFile.Flush();
        memoryStream.Close();
        saveFile.Close();
    }

    public object Load(Type objectType, FileStream saveFile)
    {
        object savedObject;
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();
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
        savedObject = formatter.Deserialize(memoryStream);
        memoryStream.Close();
        saveFile.Close();
        return savedObject;
    }
}
