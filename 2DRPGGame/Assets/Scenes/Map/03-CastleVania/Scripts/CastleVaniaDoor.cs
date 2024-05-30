using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleVaniaDoor : MonoBehaviour
{
    void Update()
    {
        if (GameObject.Find("Enemy_Boss") == null)
        {
            Destroy(gameObject);
        }
    }
}
