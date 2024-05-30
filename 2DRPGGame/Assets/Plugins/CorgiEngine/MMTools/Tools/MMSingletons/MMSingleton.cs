using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMSingleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    public static bool HasInstance => _instance != null;
    public static T TryGetInstance() => HasInstance ? _instance : null;
    public static T Current => _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //Debug.Log("MMSingleton----Instance----1");
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name + "_AutoCreated";
                    _instance = obj.AddComponent<T>();
                }
            }
            
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
            return;
        //Debug.Log("MMSingleton----Awake");
        _instance = this as T;
    }
}

