using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMusic : WeaponComponent<MusicData,AttackMusic>
{
    public bool Loop = false;
    public AudioSource _source;

    public void PlayMusic()
    {
        MMSoundManagerPlayOptions options = MMSoundManagerPlayOptions.Default;
        options.Loop = Loop;
        options.Location = Vector3.zero;
        options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;
        MMSoundManagerSoundPlayEvent.Trigger(currentAttackData.SoundClip, options);
    }
    
    protected override void Start()
    {
        base.Start();
        eventHandler.OnPlayMusic += PlayMusic;
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        eventHandler.OnPlayMusic -= PlayMusic;
    }
}
