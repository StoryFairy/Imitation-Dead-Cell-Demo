using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Underground/Input setup",fileName = "Underground Input Setup")]
public class UnderGroundInputSetupTask : DungeonGeneratorInputBaseGrid2D
{
    public LevelGraph LevelGraph;

    public UnderGroundRoomTemplatesConfig RoomTemplates;
    
    protected override LevelDescriptionGrid2D GetLevelDescription()
    {
        var levelDesrcription = new LevelDescriptionGrid2D();
        
        foreach (var room in LevelGraph.Rooms.Cast<UnderGroundRoom>())
        {
            levelDesrcription.AddRoom(room,RoomTemplates.GetRoomTemplate(room).ToList());
        }

        foreach (var connection in LevelGraph.Connections.Cast<UnderGroundConnection>())
        {
            var corridorRoom = ScriptableObject.CreateInstance<UnderGroundRoom>();
            corridorRoom.Type = UnderGroundRoomType.Corridor;
            levelDesrcription.AddCorridorConnection(connection,corridorRoom,RoomTemplates.CorridorRoomTemplates.ToList());
        }
        return levelDesrcription;
    }
}
