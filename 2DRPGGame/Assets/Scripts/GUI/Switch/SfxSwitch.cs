using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SfxSwitch : MonoBehaviour
{
    public virtual void On()
    {
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.UnmuteTrack,
            MMSoundManager.MMSoundManagerTracks.Sfx);
    }

    public virtual void Off()
    {
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.MuteTrack,
            MMSoundManager.MMSoundManagerTracks.Sfx);
    }

}
