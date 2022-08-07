using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Singleton<T> : NetworkBehaviour where T : NetworkBehaviour
{
    static T _instance;

    public static T GetInstance()
    {
        return _instance;
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<T>();
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }
}
