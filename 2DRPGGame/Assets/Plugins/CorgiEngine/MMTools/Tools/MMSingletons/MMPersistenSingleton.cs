using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPersistenSingleton<T> : MonoBehaviour where T : Component
{
    [Header("持久化单例")] public bool AutomaticallyUnparentOnAwake = true;
    protected static T _instance;
    public static bool HasInstance => _instance != null;
    public static T Current => _instance;
    protected bool _enable;

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
        if (AutomaticallyUnparentOnAwake)
            this.transform.SetParent(null); //脱离父层级
        if (_instance == null)
        {
            //Debug.Log("MMSingleton----Awake----1");
            _instance = this as T;
            DontDestroyOnLoad(transform.gameObject);
            _enable = true;
        }
        else
        {
            //Debug.Log("MMSingleton----Awake----2");
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }
}

