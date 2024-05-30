using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineBoss : MonoBehaviour
{
    public PlayableDirector director;
    public Enemy_Boss enemyBoss;

    private void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    private void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector _director)
    {
        if(director=_director)
        {
            LevelStart();
            enemyBoss.stateMachine.Initialize(enemyBoss.IdleState);
        }
    }


    public void LevelStart()
    {
        CorgiEngineEvent.Trigger(CorgiEngineEventTypes.LevelStart);
    }
}
