using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Map/Castle/Post-processing", fileName = "Castle Post Processing")]
public class CastlePostProcessingTask : DungeonGeneratorPostProcessingGrid2D,IMMEventListener<CorgiEngineEvent>
{
    public GameObject player;
    public GameObject weapon;
    public DungeonGeneratorLevelGrid2D level;
    
    public override void Run(DungeonGeneratorLevelGrid2D level)
    {
        this.level = level;
    }
    
    private void SetSpawnPosition(DungeonGeneratorLevelGrid2D level)
    {
        var entranceRoomInstance =
            level.RoomInstances.FirstOrDefault(x => ((CastleRoom)x.Room).Type == CastleRoomType.Main);
        var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;
        var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");
        Instantiate(player, spawnPosition.position, Quaternion.identity);
    }

    private void SetupLayers(DungeonGeneratorLevelGrid2D level)
    {
        var environmentLayer = LayerMask.NameToLayer(CastleGameManager.StaticEnvironmentLayer);
        if (environmentLayer == -1) return;
        foreach (var roomInstance in level.RoomInstances)
        {
            foreach (var tilemap in RoomTemplateUtilsGrid2D.GetTilemaps(roomInstance.RoomTemplateInstance))
            {
                tilemap.gameObject.layer = environmentLayer;
            }
        }

        foreach (var tilemap in level.GetSharedTilemaps())
        {
            tilemap.gameObject.layer = environmentLayer;
        }
    }

    private void SetupWeapon(DungeonGeneratorLevelGrid2D level)
    {
        var entranceRoomInstance =
            level.RoomInstances.FirstOrDefault(x => ((CastleRoom)x.Room).Type == CastleRoomType.Main);
        var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;
        var spawnPosition = roomTemplateInstance.transform.Find("WeaponSpawn");
        Instantiate(weapon, spawnPosition.position, Quaternion.identity);
    }
    
    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        LevelManager.Instance.GetLevelName();
        if (eventType.EventType == CorgiEngineEventTypes.LevelStart && LevelManager.Instance.LevelName=="00-Castle")
        {
            SetSpawnPosition(level);
            SetupLayers(level);
            SetupWeapon(level);
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening<CorgiEngineEvent>();
    }
    
    private void OnDisable()
    {
        this.MMEventStopListening<CorgiEngineEvent>();
    }
}
