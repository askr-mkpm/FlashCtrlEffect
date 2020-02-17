using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace mikipomaidLib.FlashCtrl.Timeline
{
    [Serializable]
	public class FlashCtrlBehaviour : PlayableBehaviour
	{
        [Header("Color")]

        public ColorType ColorType = ColorType.SingleColor;

        [ColorUsage(true, true)] public Color color_1 = Color.cyan;

        [ColorUsage(true, true)] public Color color_2 = Color.yellow;

        public Texture texture;

        public Vector2 offset = new Vector2(1,1);

        public Vector2 tiling = new Vector2(1,1);

		[SerializeField,Range(0,2)]
		public float borderBlur = 1;

		[SerializeField,Range(0,1)]
		public float gradationPos = 1;

        [Space(10)]

		[SerializeField,Range(0,1)]
		public float alpha = 1;

		[SerializeField,Range(1,10)]
		public float emissionIntensity = 1;

        [Header("FlashType")]

        public FlashType FlashType = FlashType.On;

		[SerializeField,Range(0,30)]
		public float flashSpeed = 1;


        [Header("Scroll")]

        public bool ScrollMode = false;

		[SerializeField,Range(0,20)]
		public float scrollSpeed = 1;

        [Header("ScrollCoord(TextureOnly)")]

		[SerializeField,Range(-1,1)]
		public float scrollCoord_x = 0;
		
		[SerializeField,Range(-1,1)]
		public float scrollCoord_y = 0;


        [Header("Dot")]

        public bool DotMode = false;

		[SerializeField,Range(0,100)]
		public float dotNumber = 64;

		[SerializeField,Range(0,1.5f)]
		public float dotSize = 0.5f;

        [Header("Neon")]

        public bool NeonMode = false;

		[SerializeField,Range(1,150)]
		public float flashInterval = 70;

		[SerializeField,Range(0,10)]
		public float flashPower = 1;

        [Header("Rotation")]

        public Rotation RotationType = Rotation.Self;

		[SerializeField,Range(0,10)]
		public float angle = 0;

		[SerializeField,Range(-10,10)]
		public float autoIntensity = 1;

        [HideInInspector] public FlashCtrlProperty FlashCtrlProperty;
    }
}
