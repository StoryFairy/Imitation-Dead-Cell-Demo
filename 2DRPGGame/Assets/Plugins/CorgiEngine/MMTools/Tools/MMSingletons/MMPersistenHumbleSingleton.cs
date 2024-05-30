using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPersistenHumbleSingleton<T> : MonoBehaviour where T : Component
{
    public static bool HasInstance => _instance != null;
    public static T Current => _instance;
    protected static T _instance;

    [MMReadOnly] // 只读属性
    public float InitializationTime;//单例初始化时间

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
        InitializationTime = Time.time;
        DontDestroyOnLoad(this.gameObject);
        T[] check=FindObjectsOfType<T>();
        foreach (T searched in check)
        {
            if (searched != this)
            {
                if (searched.GetComponent<MMPersistenHumbleSingleton<T>>().InitializationTime < InitializationTime)
                {
                    Destroy(searched.gameObject);
                }
            }
        }

        if (_instance == null)
        {
            _instance = this as T;
        }
    }
}
