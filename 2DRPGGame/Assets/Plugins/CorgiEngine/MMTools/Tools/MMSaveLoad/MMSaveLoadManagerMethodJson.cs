using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MMSaveLoadManagerMethodJson : IMMSaveLoadManagerMethod
{
    public void Save(object objectToSave, FileStream saveFile)
    {
        string json = JsonUtility.ToJson(objectToSave);
        StreamWriter streamWriter = new StreamWriter(saveFile);
        streamWriter.Write(json);
        streamWriter.Close();
        saveFile.Close();
    }

    public object Load(Type objectType, FileStream saveFile)
    {
        object savedObject;
        StreamReader streamReader = new StreamReader(saveFile, Encoding.UTF8);
        string json = streamReader.ReadToEnd();
        savedObject = JsonUtility.FromJson(json, objectType);
        streamReader.Close();
        saveFile.Close();
        return savedObject;
    }
}
