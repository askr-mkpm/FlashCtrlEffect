using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace mikipomaidLib.FlashCtrl.Timeline
{
	[Serializable]
    public class FlashCtrlClip : PlayableAsset, ITimelineClipAsset
    {
        public FlashCtrlBehaviour access = new FlashCtrlBehaviour ();

        public ClipCaps clipCaps
        {
            get {return ClipCaps.Blending;}
        }
        public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
        {
            var tlplayable = ScriptPlayable<FlashCtrlBehaviour>.Create (graph, access);
            FlashCtrlBehaviour clone = tlplayable.GetBehaviour();

            return tlplayable;
        }
    }
}
