using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMLoadScene : MonoBehaviour
{
    public string SceneName;
    
    public virtual void LoadScene()
    {
        Debug.Log("MMLoadScene-------LoadScene");
    }
}
