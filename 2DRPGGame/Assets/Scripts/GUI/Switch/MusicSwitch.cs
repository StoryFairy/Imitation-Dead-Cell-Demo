using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{
    public virtual void On()
    {
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.UnmuteTrack,
            MMSoundManager.MMSoundManagerTracks.Music);
    }

    public virtual void Off()
    {
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.MuteTrack,
            MMSoundManager.MMSoundManagerTracks.Music);
    }
}
