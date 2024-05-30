using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;


public interface IMMSaveLoadManagerMethod
{
    void Save(object objectToSave, FileStream saveFile);
    object Load(Type objectType, FileStream saveFile);
}

public enum MMSaveLoadManagerMethods
{
    Json,
    JsonEncrypted, //加密
    Binary, //二进制
    BinaryEncrypted
}

public abstract class MMSaveLoadManagerEncrypter //加密
{
    public string Key { get; set; } = "yourDefaultKey"; //Key
    protected string _saltText = "SaltTextGoesHere"; //混淆代码

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="inputStream"></param>
    /// <param name="outputStream"></param>
    /// <param name="sKey"></param>
    protected virtual void Encrypt(Stream inputStream, Stream outputStream, string sKey)
    {
        RijndaelManaged algorithm = new RijndaelManaged();
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sKey, Encoding.ASCII.GetBytes(_saltText));
        //加密算法Key，IV设置（加密密钥）
        algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
        algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);
        CryptoStream cryptostream = new CryptoStream(inputStream, algorithm.CreateEncryptor(), CryptoStreamMode.Read);
        cryptostream.CopyTo(outputStream);
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="inputStream"></param>
    /// <param name="outputStream"></param>
    /// <param name="sKey"></param>
    protected virtual void Decrypt(Stream inputStream, Stream outputStream, string sKey)
    {
        RijndaelManaged algorithm = new RijndaelManaged();
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sKey, Encoding.ASCII.GetBytes(_saltText));
        //加密算法Key，IV设置（加密密钥）
        algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
        algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);
        CryptoStream cryptostream = new CryptoStream(inputStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read);
        cryptostream.CopyTo(outputStream);
    }
}
