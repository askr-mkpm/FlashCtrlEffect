Shader "mikipomaid/FlashCtrlEffect"
{
    Properties
	{
        [Header(Color)]
        [KeywordEnum(SingleColor, Gradation,Texture)] _colorType("ColorType", int) = 0
        [HDR] _Color_1 ("Color_1",COLOR) = (1,1,1,1)
        [HDR] _Color_2 ("Color_2",COLOR) = (0,0,0,1)
        _borderBlur("BorderBlur", Range(0,2))= 1
        _gradationPos ("GradationPos", Range(0,1))=1
        _Texture("Texture(x Color_1)", 2D)="white"{}
        _alpha("Alpha", Range(0,1))= 1
        _EmissionIntensity("EmissionIntensity" , Range(1,10)) = 1
        
        [Header(Scroll)]
        [KeywordEnum(Off, On)] _scrollMode("ScrollMode", int) = 0
        _scrollCoord_x ("ScrollCoord.x(TextureOnly)",Range(-1,1))=0
        _scrollCoord_y ("ScrollCoord.y(TextureOnly)",Range(-1,1))=0
        _scrollSpeed("ScrollSpeed", Range(0,20)) = 1

        [Header(Dot)]
        [KeywordEnum(Off, On)] _dotMode("DotMode", int) = 0
        _dotNumber("DotNumber", Range(0,100)) = 64.0
        _dotSize("DotSize", Range(0,1.5)) = 0.5
        
        [Header(Neon)]
        [KeywordEnum(Off, On)] _neonMode("NeonMode", int) = 0
        _flashInterval("NeonFlashInterval", Range(1,150))=70
        _flashPower("NeonFlashPower",Range(0,10))=1

        [Header(UVRotation)]
        [KeywordEnum(Self, Auto)] _rotationMode("RotationMode", int) = 0
        _angle("Angle", Range(0,10))=0
        _autoIntensity("AutoRotationIntensity", Range(-10,10))=1

        [Header(OtherSettings)]
        [Enum(UnityEngine.Rendering.CullMode)]
        _Cull("Cull", Float) = 0               

        [Enum(UnityEngine.Rendering.CompareFunction)]
        _ZTest("ZTest", Float) = 4            

        [Enum(Off, 0, On, 1)]
        _ZWrite("ZWrite", Float) = 0           

        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("Blend Src Factor", Float) = 5     

        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("Blend Dst Factor", Float) = 10    

        [Header(ForFlashCtrl)]
        _flashProperty("FlashProperty", Range(0,1))=1
    }

    SubShader
	{
        Tags
		{
            "IgnoreProjector"="True"
			"Queue" = "Geometry+100"
            "RenderType"="Transparent"
        }
        LOD 100

        Cull [_Cull]
        ZTest [_ZTest]
        ZWrite [_ZWrite]
        Blend [_SrcFactor][_DstFactor]

        Pass
		{
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _Color_1;
            float4 _Color_2;
            float _borderBlur;
            float _gradationPos;
            float _alpha;
            float _EmissionIntensity;
            float _flashInterval;
            float _flashPower;
            float _scrollCoord_x;
            float _scrollCoord_y;
            float _scrollSpeed;
            float _dotNumber;
            float _dotSize;
            float _angle;
            float _autoIntensity;
            float _flashProperty;

            sampler2D _Texture;
            float4 _Texture_ST;

            struct VertexInput 
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_g : TEXCOORD1;
            };

            struct VertexOutput 
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_g : TEXCOORD1;
            };

            VertexOutput vert (VertexInput v)
			{
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                o.uv = TRANSFORM_TEX(v.uv, _Texture);
                o.uv_g = v.uv_g;
                return o;
            }

            float dot(float2 UV, float size)
            {
                float d = length((UV * 2 - 1) / float2(size, size));
                float ell = saturate((1 - d) / fwidth(d));
                return ell;
            }

            fixed2 posterize(fixed2 In, fixed Steps)
            {
                return floor(In * Steps) / Steps;
            }

            float randSin(float r)
            {
                return lerp(frac(sin(_Time*0.1)*r),0.5,0.8)+0.5;
            }

            float2x2 rot(float r)
            {
                float c = cos(r);
                float s = sin(r);
                return float2x2(c,-s,s,c);
            }

            #pragma shader_feature _COLORTYPE_SINGLECOLOR _COLORTYPE_GRADATION _COLORTYPE_TEXTURE     
            #pragma shader_feature _NEONMODE_OFF _NEONMODE_ON 
            #pragma shader_feature _SCROLLMODE_OFF _SCROLLMODE_ON   
            #pragma shader_feature _DOTMODE_OFF _DOTMODE_ON 
            #pragma shader_feature _ROTATIONMODE_SELF _ROTATIONMODE_AUTO

            float4 frag(VertexOutput i) : COLOR
			{
                #define GRID_N _dotNumber
                #define UV_Repeat frac(i.uv * GRID_N)

                float time = _Time.x * _scrollSpeed;
                float t = frac(time) ;
                float gr = 0; 
                float2 uv = i.uv;
                float2 uv_g = i.uv_g;

                float randValue = pow(randSin(_flashInterval*1000),_flashPower);

                #ifdef _SCROLLMODE_OFF
                    
                    uv = uv;
                    gr = _gradationPos;
                    
                #else

                    uv.x += time*10*_scrollCoord_x;
                    uv.y += time*10*_scrollCoord_y;
                    gr = 3*t -1;

                #endif    

                #ifdef _ROTATIONMODE_SELF

                    uv = mul(uv-0.5, rot(_angle))+0.5;
                    uv_g = mul(uv_g-0.5, rot(_angle))+0.5;

                #else 

                    uv = mul(uv-0.5, rot(_Time.x*_autoIntensity*10))+0.5;
                    uv_g = mul(uv_g-0.5, rot(_Time.x*_autoIntensity*10))+0.5;

                #endif

                #ifdef _COLORTYPE_SINGLECOLOR

                    float4 col = _Color_1;

                #elif _COLORTYPE_GRADATION

                        float colorPos = clamp(pow(abs(uv_g.x - gr),_borderBlur),0,1);    
                        float4 col = lerp(_Color_1,_Color_2,colorPos);

                #else

                    float4 col = tex2D(_Texture, uv)*_Color_1;

                #endif

                #ifdef _DOTMODE_OFF

                    col = col;

                #else

                    col *= dot(UV_Repeat, _dotSize);

                #endif

                #ifdef _NEONMODE_OFF

                    col = col;

                #else

                    col *= randValue;

                #endif

                col.a *= _alpha;
                col.rgb *= _flashProperty;
                return col *_EmissionIntensity;
            }

            ENDCG
        }
    }
}

