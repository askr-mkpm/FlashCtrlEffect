using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mikipomaidLib.FlashCtrl.Timeline
{
	public enum FlashType
	{
		On,	Off, Blink
	}
	public enum ColorType
    {
		SingleColor, Gradation, Texture
	}

    public enum Rotation
    {
        Self, Auto
    }

	public class FlashCtrlProperty : MonoBehaviour
	{
		Renderer[] rendList;

		public FlashType flashtype = FlashType.On;

		[SerializeField,Range(0,30)]
		public float flashSpeed = 1;

		public ColorType colortype = ColorType.SingleColor;

		private int colortypeint ;

		[ColorUsage(true, true)] public Color Color_1 = Color.cyan;

		[ColorUsage(true, true)] public Color Color_2 = Color.yellow;

		public Texture texture;

		public Vector2 offset = new Vector2(1,1);
		
		public Vector2 tiling = new Vector2(1,1);

		[SerializeField,Range(0,2)]
		public float borderBlur = 1;

		[SerializeField,Range(0,1)]
		public float gradationPos = 1;

		[SerializeField,Range(0,1)]
		public float alpha = 1;

		[SerializeField,Range(1,10)]
		public float emissionIntensity = 1;

		public bool scrollMode = false;

		private int scrollModeint = 0;

		[SerializeField,Range(-1,1)]
		public float scrollCoord_x = 0;
		
		[SerializeField,Range(-1,1)]
		public float scrollCoord_y = 0;

		[SerializeField,Range(0,20)]
		public float scrollSpeed = 1;

		public bool dotMode = false;

		private int dotModeint = 0;

		[SerializeField,Range(0,100)]
		public float dotNumber = 64;

		[SerializeField,Range(0,1.5f)]
		public float dotSize = 0.5f;

		public bool neonMode = false;

		private int neonModeint = 0;

		[SerializeField,Range(1,150)]
		public float flashInterval = 70;

		[SerializeField,Range(0,10)]
		public float flashPower = 1;

		public Rotation rotation = Rotation.Self;

		private int rotationint;

		[SerializeField,Range(0,10)]
		public float angle = 0;

		[SerializeField,Range(-10,10)]
		public float autoIntensity = 1;

		private float flashProperty = 1;

		void Start ()
		{
			rendList = GetComponentsInChildren<Renderer>();
		}

		void Update ()
		{
			switch(flashtype)
			{
				case FlashType.On:
					flashProperty = 1;
					break;

				case FlashType.Off:
					flashProperty = 0;
					break;
				
				case FlashType.Blink:
					flashProperty = 0.5f*Mathf.Sin(flashSpeed*Mathf.PI*Time.time)+0.5f;
					break;
			}

			switch (colortype)
			{
				case ColorType.SingleColor:
					colortypeint = 0;
					break;

				case ColorType.Gradation:
					colortypeint = 1;
					break;

				case ColorType.Texture:
					colortypeint = 2;
					break;
			}

			switch(rotation)
			{
				case Rotation.Self:
					rotationint = 0;
					break;

				case Rotation.Auto:
					rotationint = 1;
					break;
			}

			if(scrollMode)
			{
				scrollModeint =1;
			}else{
				scrollModeint = 0;
			}

			if(dotMode)
			{
				dotModeint = 1;
			}else{
				dotModeint =0;
			}

			if(neonMode)
			{
				neonModeint = 1;
			}else{
				neonModeint = 0;
			}

			for(int i = 0; i < rendList.Length;i++)
			{
				foreach(Material j in rendList[i].sharedMaterials)
				{
					j.SetColor("_Color_1", Color_1);
					j.SetColor("_Color_2", Color_2);
					j.SetTexture("_Texture", texture);
					j.SetTextureOffset("_Texture", offset);
					j.SetTextureScale("_Texture", tiling);
					j.SetFloat("_borderBlur",borderBlur);
					j.SetFloat("_gradationPos", gradationPos);
					j.SetFloat("_alpha", alpha);
					j.SetFloat("_EmissionIntensity", emissionIntensity);
					j.SetFloat("_scrollCoord_x", scrollCoord_x);
					j.SetFloat("_scrollCoord_y", scrollCoord_y);
					j.SetFloat("_scrollSpeed", scrollSpeed);
					j.SetFloat("_dotNumber", dotNumber);
					j.SetFloat("_dotSize", dotSize);
					j.SetFloat("_flashInterval", flashInterval);
					j.SetFloat("_flashPower", flashPower);
					j.SetFloat("_angle", angle);
					j.SetFloat("_autoIntensity", autoIntensity);
					j.SetFloat("_flashProperty", flashProperty);

					j.SetInt("_colorType",colortypeint);
					j.SetInt("_scrollMode", scrollModeint);
					j.SetInt("_dotMode", dotModeint);
					j.SetInt("_neonMode", neonModeint);
					j.SetInt("_rotationMode", rotationint);

					if(colortypeint == 0)
					{
						j.EnableKeyword("_COLORTYPE_SINGLECOLOR");
						j.DisableKeyword("_COLORTYPE_GRADATION");
						j.DisableKeyword("_COLORTYPE_TEXTURE");
					}
					else if(colortypeint ==1)
					{
						j.DisableKeyword("_COLORTYPE_SINGLECOLOR");
						j.EnableKeyword("_COLORTYPE_GRADATION");
						j.DisableKeyword("_COLORTYPE_TEXTURE");
					}
					else
					{
						j.DisableKeyword("_COLORTYPE_SINGLECOLOR");
						j.DisableKeyword("_COLORTYPE_GRADATION");
						j.EnableKeyword("_COLORTYPE_TEXTURE");
					}

					if(scrollModeint == 0)
					{
						j.EnableKeyword("_SCROLLMODE_OFF");
						j.DisableKeyword("_SCROLLMODE_ON");
					}
					else
					{
						j.DisableKeyword("_SCROLLMODE_OFF");
						j.EnableKeyword("_SCROLLMODE_ON");
					}

					if(dotModeint == 0)
					{
						j.EnableKeyword("_DOTMODE_OFF");
						j.DisableKeyword("_DOTMODE_ON");
					}
					else
					{
						j.DisableKeyword("_DOTMODE_OFF");
						j.EnableKeyword("_DOTMODE_ON");
					}

					if(neonModeint == 0)
					{
						j.EnableKeyword("_NEONMODE_OFF");
						j.DisableKeyword("_NEONMODE_ON");
					}
					else
					{
						j.EnableKeyword("_NEONMODE_ON");
						j.DisableKeyword("_NEONMODE_OFF");
					}

					if(rotationint == 0)
					{
						j.EnableKeyword("_ROTATIONMODE_SELF");
						j.DisableKeyword("_ROTATIONMODE_AUTO");
					}
					else
					{
						j.EnableKeyword("_ROTATIONMODE_AUTO");
						j.DisableKeyword("_ROTATIONMODE_SELF");
					}					
				}
			}
		}

		public void ChangeColorType(ColorType ColorType)
		{
			colortype = ColorType;
		}

		public void ChangescrollMode (bool ScrollMode)
		{
			scrollMode = ScrollMode;
		}

		public void ChangeRotationMode (Rotation RotationType)
		{
			rotation = RotationType;
		}

		public void ChangedotMode (bool DotMode)
		{
			dotMode = DotMode;
		}

		public void ChangeneonMode (bool NeonMode)
		{
			neonMode = NeonMode;
		}		

		public void ChangeFlashMode(FlashType FlashType)
		{
			flashtype = FlashType;
		}
	}
}

