using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/CastleVania/Input setup",fileName = "CastleVania Input Setup")]
public class CastleVaniaInputSetupTask : DungeonGeneratorInputBaseGrid2D
{
    public LevelGraph LevelGraph;

    public CastleVaniaRoomTemplatesConfig RoomTemplates;
    
    protected override LevelDescriptionGrid2D GetLevelDescription()
    {
        var levelDesrcription = new LevelDescriptionGrid2D();
        
        foreach (var room in LevelGraph.Rooms.Cast<CastleVaniaRoom>())
        {
            levelDesrcription.AddRoom(room,RoomTemplates.GetRoomTemplate(room).ToList());
        }
        foreach (var connection in LevelGraph.Connections.Cast<CastleVaniaConnection>())
        {
            var corridorRoom = ScriptableObject.CreateInstance<CastleVaniaRoom>();
            corridorRoom.Type = CastleVaniaRoomType.Corridor;
            levelDesrcription.AddCorridorConnection(connection,corridorRoom,RoomTemplates.CorridorRoomTemplates.ToList());
        }
        return levelDesrcription;
    }
}
