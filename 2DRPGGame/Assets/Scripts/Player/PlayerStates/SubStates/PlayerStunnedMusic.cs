using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedMusic : MonoBehaviour
{
    public AudioClip SoundClip;
    public bool Loop = false;
    public AudioSource _source;

    public void PlayMusic()
    {
        MMSoundManagerPlayOptions options = MMSoundManagerPlayOptions.Default;
        options.Loop = Loop;
        options.Location = Vector3.zero;
        options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;
        MMSoundManagerSoundPlayEvent.Trigger(SoundClip, options);
    }
}
