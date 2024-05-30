using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButton : MonoBehaviour
{
    public int nextID;
    public void OnClick()
    {
        DialogueManager.Instance.ShowDialogue(nextID);
        DialogueManager.Instance.isSelect = false;
        SelectionPool.Instance.ReturnAllPool();
    }
}
