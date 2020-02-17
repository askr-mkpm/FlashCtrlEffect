using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;

namespace mikipomaidLib.FlashCtrl.Timeline
{
  [TrackColor(1, 1, 1)]
  [TrackClipType(typeof(FlashCtrlClip))]
  [TrackBindingType(typeof(FlashCtrlProperty))]
  public class FlashCtrlTrack : TrackAsset
  {
      public override Playable CreateTrackMixer(PlayableGraph graph, GameObject bind, int inputnumber)
      {
          var mixer = ScriptPlayable<FlashCtrlMixerBehaviour>.Create(graph, inputnumber);

          mixer.GetBehaviour().clips = GetClips().ToArray();
          PlayableDirector pd = bind.GetComponent<PlayableDirector>();
          if (pd != null)
          {
            mixer.GetBehaviour().playableDirector = pd;
          }

          return mixer;
      }
  }
}
  
