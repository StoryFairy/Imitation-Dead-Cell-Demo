using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveResetButton : MonoBehaviour
{
    public virtual void ResetAllSaves()
    {
        //MMSaveLoadManager.DeleteSaveFolder("CorgiEngine");
        MMSaveLoadManager.DeleteSaveFolder("MMGameData");
        MMSaveLoadManager.DeleteSaveFolder("MMSoundManager");
        
    }
}
