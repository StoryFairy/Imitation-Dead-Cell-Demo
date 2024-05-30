using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Castle/Input setup",fileName = "Castle Input Setup")]
public class CastleInputSetupTask : DungeonGeneratorInputBaseGrid2D
{
    public LevelGraph LevelGraph;

    public CastleRoomTemplatesConfig RoomTemplates;
    
    protected override LevelDescriptionGrid2D GetLevelDescription()
    {
        var levelDesrcription = new LevelDescriptionGrid2D();
        
        foreach (var room in LevelGraph.Rooms.Cast<CastleRoom>())
        {
            levelDesrcription.AddRoom(room,RoomTemplates.GetRoomTemplate(room).ToList());
        }
        
        return levelDesrcription;
    }
}
