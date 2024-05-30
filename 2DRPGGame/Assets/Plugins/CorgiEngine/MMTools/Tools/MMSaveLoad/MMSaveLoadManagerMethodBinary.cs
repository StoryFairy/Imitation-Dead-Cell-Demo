using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MMSaveLoadManagerMethodBinary : IMMSaveLoadManagerMethod
{
    public void Save(object objectToSave, FileStream saveFile)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(saveFile, objectToSave);
        saveFile.Close();
    }

    public object Load(Type objectType, FileStream saveFile)
    {
        object savedObject;
        BinaryFormatter formatter = new BinaryFormatter();
        savedObject = formatter.Deserialize(saveFile);
        saveFile.Close();
        return savedObject;
    }
}
