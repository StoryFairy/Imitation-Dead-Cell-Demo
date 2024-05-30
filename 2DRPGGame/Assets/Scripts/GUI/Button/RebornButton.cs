using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornButton : MonoBehaviour
{
    public void OnClick()
    {
        LevelManager.Instance.GotoLevel("00-Castle");
    }
}
