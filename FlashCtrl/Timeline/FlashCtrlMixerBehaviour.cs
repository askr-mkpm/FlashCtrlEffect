using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace mikipomaidLib.FlashCtrl.Timeline
{
   public class FlashCtrlMixerBehaviour : PlayableBehaviour
    {
        FlashCtrlProperty trackBinding;

        public TimelineClip[] clips;

        public PlayableDirector playableDirector;

        public override void ProcessFrame(Playable tlplayable, FrameData info, object playerData) 
        {
            trackBinding = playerData as FlashCtrlProperty;

            if (!trackBinding)
                return;

            int inputnumber = tlplayable.GetInputCount();

            if(inputnumber == 0)
                return;

            double time = playableDirector.time;
            int clipNum = clips.Length;

            for(int i = 0; i < clipNum; i++)
            {
                TimelineClip clip = clips[i];

                bool isPlaying = clip.start <= time && time <= clip.end;
                if (!isPlaying) continue;

                double elapseRation = (clip.start - time) / clip.duration;

                FlashCtrlClip clipProp = clip.asset as FlashCtrlClip;
                if (clipProp == null)
                    return;
                FlashCtrlBehaviour flashCtrl = clipProp.access;
                float currentweight = tlplayable.GetInputWeight(i);

                trackBinding.Color_1 = flashCtrl.color_1*currentweight;
                trackBinding.Color_2 = flashCtrl.color_2*currentweight;
                trackBinding.texture = flashCtrl.texture;
                trackBinding.offset = flashCtrl.offset*currentweight;
                trackBinding.tiling = flashCtrl.tiling*currentweight;
                trackBinding.borderBlur = flashCtrl.borderBlur*currentweight;
                trackBinding.gradationPos = flashCtrl.gradationPos*currentweight;
                trackBinding.alpha = flashCtrl.alpha*currentweight;
                trackBinding.emissionIntensity = flashCtrl.emissionIntensity*currentweight;
                trackBinding.scrollCoord_x = flashCtrl.scrollCoord_x*currentweight;
                trackBinding.scrollCoord_y = flashCtrl.scrollCoord_y*currentweight;
                trackBinding.scrollSpeed = flashCtrl.scrollSpeed*currentweight;
                trackBinding.dotNumber = flashCtrl.dotNumber*currentweight;
                trackBinding.dotSize = flashCtrl.dotSize*currentweight;
                trackBinding.flashInterval = flashCtrl.flashInterval*currentweight;
                trackBinding.flashPower = flashCtrl.flashPower*currentweight;
                trackBinding.angle = flashCtrl.angle*currentweight;
                trackBinding.autoIntensity = flashCtrl.autoIntensity*currentweight;
                trackBinding.flashSpeed = flashCtrl.flashSpeed*currentweight;

                trackBinding.ChangeColorType(flashCtrl.ColorType);
                trackBinding.ChangescrollMode(flashCtrl.ScrollMode);
                trackBinding.ChangeRotationMode(flashCtrl.RotationType);
                trackBinding.ChangedotMode(flashCtrl.DotMode);
                trackBinding.ChangeneonMode(flashCtrl.NeonMode);
                trackBinding.ChangeFlashMode(flashCtrl.FlashType);
                
                if(i > 0)
                {
                    TimelineClip preClip = clips[i - 1];
                    bool preIsPlaying = preClip.start <= time && time <= preClip.end;

                    if (preIsPlaying) 
                    {
                        double elapseRation_pre = (preClip.start - time) / preClip.duration;
                        double blendRation = (time - preClip.start) / (clip.end - preClip.start);
                        FlashCtrlClip preClipProp = preClip.asset as FlashCtrlClip;
                        if (preClipProp == null) return;
                        FlashCtrlBehaviour preflashCtrl = preClipProp.access;
                        float preWeight = tlplayable.GetInputWeight (i - 1);

                        trackBinding.Color_1 += preflashCtrl.color_1*preWeight;
                        trackBinding.Color_2 += preflashCtrl.color_2*preWeight;
                        trackBinding.offset += preflashCtrl.offset*preWeight;
                        trackBinding.tiling += preflashCtrl.tiling*preWeight;
                        trackBinding.borderBlur += preflashCtrl.borderBlur*preWeight;
                        trackBinding.gradationPos += preflashCtrl.gradationPos*preWeight;
                        trackBinding.alpha += preflashCtrl.alpha*preWeight;
                        trackBinding.emissionIntensity += preflashCtrl.emissionIntensity*preWeight;
                        trackBinding.scrollCoord_x += preflashCtrl.scrollCoord_x*preWeight;
                        trackBinding.scrollCoord_y += preflashCtrl.scrollCoord_y*preWeight;
                        trackBinding.scrollSpeed += preflashCtrl.scrollSpeed*preWeight;
                        trackBinding.dotNumber += preflashCtrl.dotNumber*preWeight;
                        trackBinding.dotSize += preflashCtrl.dotSize*preWeight;
                        trackBinding.flashInterval += preflashCtrl.flashInterval*preWeight;
                        trackBinding.flashPower += preflashCtrl.flashPower*preWeight;
                        trackBinding.angle += preflashCtrl.angle*preWeight;
                        trackBinding.autoIntensity += preflashCtrl.autoIntensity*preWeight;
                        trackBinding.flashSpeed += preflashCtrl.flashSpeed*preWeight;
                    }
                }
            }
        }
    }
}
        
