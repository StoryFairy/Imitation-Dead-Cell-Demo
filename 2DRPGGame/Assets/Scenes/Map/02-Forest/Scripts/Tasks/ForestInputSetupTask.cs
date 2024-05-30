using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Forest/Input setup",fileName = "Forest Input Setup")]
public class ForestInputSetupTask : DungeonGeneratorInputBaseGrid2D
{
    public LevelGraph LevelGraph;

    public ForestRoomTemplatesConfig RoomTemplates;
    
    protected override LevelDescriptionGrid2D GetLevelDescription()
    {
        var levelDesrcription = new LevelDescriptionGrid2D();
        
        foreach (var room in LevelGraph.Rooms.Cast<ForestRoom>())
        {
            levelDesrcription.AddRoom(room,RoomTemplates.GetRoomTemplate(room).ToList());
        }

        foreach (var connection in LevelGraph.Connections.Cast<ForestConnection>())
        {
            var corridorRoom = ScriptableObject.CreateInstance<ForestRoom>();
            corridorRoom.Type = ForestRoomType.Corridor;
            levelDesrcription.AddCorridorConnection(connection,corridorRoom,RoomTemplates.CorridorRoomTemplates.ToList());
        }
        return levelDesrcription;
    }
}
