using System.Collections;
using System.Collections.Generic;
using Edgar.Unity;
using UnityEngine;

public class CastleVaniaRoom : RoomBase
{
    public CastleVaniaRoomType Type;
    public bool Outside;
    
    public override List<GameObject> GetRoomTemplates()
    {
        return null;
    }

    public override string GetDisplayName()
    {
        return Type.ToString();
    }
}
