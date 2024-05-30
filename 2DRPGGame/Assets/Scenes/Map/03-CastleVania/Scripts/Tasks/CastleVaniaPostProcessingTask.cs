using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Map/CastleVania/Post-processing", fileName = "CastleVania Post Processing")]
public class CastleVaniaPostProcessingTask : DungeonGeneratorPostProcessingGrid2D,IMMEventListener<CorgiEngineEvent>
{
    public GameObject player;
    public bool SpawnEnemies;
    public GameObject[] Enemies;
    public GameObject weapon;
    public DungeonGeneratorLevelGrid2D level;
    
    public override void Run(DungeonGeneratorLevelGrid2D level)
    {
        this.level = level;
    }
    
    private void SetSpawnPosition(DungeonGeneratorLevelGrid2D level)
    {
        var entranceRoomInstance =
            level.RoomInstances.FirstOrDefault(x => ((CastleVaniaRoom)x.Room).Type == CastleVaniaRoomType.Entrance);
        var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;
        var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");
        Instantiate(player, spawnPosition.position, Quaternion.identity);
    }

    private void SetupLayers(DungeonGeneratorLevelGrid2D level)
    {
        var environmentLayer = LayerMask.NameToLayer(CastleVaniaGameManager.StaticEnvironmentLayer);
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
    private void DoSpawnEnemies(DungeonGeneratorLevelGrid2D level)
    {
        foreach (var roomInstance in level.RoomInstances)
        {
            var roomTemplate = roomInstance.RoomTemplateInstance;

            var enemySpawnPoints = roomTemplate.transform.Find("EnemySpawnPoints");

            if (enemySpawnPoints != null)
            {
                foreach (Transform enemySpawnPoint in enemySpawnPoints)
                {
                    var enemyPrefab = Enemies[Random.Next(Enemies.Length)];
                    var enemy = Instantiate(enemyPrefab);
                    enemy.transform.parent = roomTemplate.transform;
                    enemy.transform.position = enemySpawnPoint.position;
                }
            }
        }
    }
    
    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        LevelManager.Instance.GetLevelName();
        if (eventType.EventType == CorgiEngineEventTypes.LevelStart && LevelManager.Instance.LevelName=="03-CastleVania")
        {
            SetSpawnPosition(level);
            SetupLayers(level);

            if (SpawnEnemies)
            {
                DoSpawnEnemies(level);
            }
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
