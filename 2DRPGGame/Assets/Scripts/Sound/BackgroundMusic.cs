using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Corgi Engine/Audio/Background Music")]
public class BackgroundMusic : MMPersistenHumbleSingleton<BackgroundMusic>
{
    public AudioClip SoundClip;
    public bool Loop = true;
    public AudioSource _source;

    protected virtual void Start()
    {
        MMSoundManagerPlayOptions options = MMSoundManagerPlayOptions.Default;
        options.Loop = Loop;
        options.Location = Vector3.zero;
        options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Music;
        MMSoundManagerSoundPlayEvent.Trigger(SoundClip, options);
    }
}
