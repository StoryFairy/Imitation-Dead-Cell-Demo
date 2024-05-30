using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Sea/Input setup",fileName = "Sea Input Setup")]
public class SeaInputSetupTask : DungeonGeneratorInputBaseGrid2D
{
    public LevelGraph LevelGraph;

    public SeaRoomTemplatesConfig RoomTemplates;
    
    protected override LevelDescriptionGrid2D GetLevelDescription()
    {
        var levelDesrcription = new LevelDescriptionGrid2D();
        
        foreach (var room in LevelGraph.Rooms.Cast<SeaRoom>())
        {
            levelDesrcription.AddRoom(room,RoomTemplates.GetRoomTemplate(room).ToList());
        }

        foreach (var connection in LevelGraph.Connections.Cast<SeaConnection>())
        {
            var corridorRoom = ScriptableObject.CreateInstance<SeaRoom>();
            corridorRoom.Type = SeaRoomType.Corridor;
            levelDesrcription.AddCorridorConnection(connection,corridorRoom,RoomTemplates.CorridorRoomTemplates.ToList());
        }
        return levelDesrcription;
    }
}
