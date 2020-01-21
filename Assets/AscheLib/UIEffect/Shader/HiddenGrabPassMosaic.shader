Shader "Hidden/UI/GrabPass/Mosaic"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

		_ParameterTexture ("Parameter Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		GrabPass{}
		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			struct v2f
			{
				float4 grabPos : TEXCOORD0;
				half2 texcoord  : TEXCOORD1;
				fixed4 color : COLOR;
			};

			sampler2D _GrabTexture;
			float4 _GrabTexture_TexelSize;
			sampler2D _MainTex;
			sampler2D _ParameterTexture;
			float4 _ClipRect;

			v2f vert(appdata_t IN, out float4 vertex : SV_POSITION) {
				v2f OUT;
				vertex = UnityObjectToClipPos(IN.vertex);
				OUT.grabPos = ComputeGrabScreenPos(vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				return OUT;
			}
			half4 frag(v2f IN, UNITY_VPOS_TYPE vpos : VPOS) : SV_Target {
				vpos.xy /= _ScreenParams.xy;
				float blockNum = tex2D(_ParameterTexture, float2(0, 0)).x * 1000;
				float4 f = floor(vpos * blockNum);
				fixed size = 1.0 / blockNum;
				float4 leftBottomPosision = f / blockNum;
				float4 leftTopPosition = leftBottomPosision + float4(0.0, size, 0.0, 0.0);
				float4 rightBottomPosition = leftBottomPosision + float4(size, 0.0, 0.0, 0.0);
				float4 rightTopPosition = leftBottomPosision + float4(size, size, 0.0, 0.0);
				half4 resultColor = (tex2D(_GrabTexture, UNITY_PROJ_COORD(leftBottomPosision.xy)) +
					tex2D(_GrabTexture, UNITY_PROJ_COORD(leftTopPosition.xy)) +
					tex2D(_GrabTexture, UNITY_PROJ_COORD(rightBottomPosition.xy)) +
					tex2D(_GrabTexture, UNITY_PROJ_COORD(rightTopPosition.xy))) / 4.0;

				half4 mainTexColor = tex2D(_MainTex, IN.texcoord);
				resultColor *= IN.color;
				resultColor.a *= mainTexColor.a;

				#ifdef UNITY_UI_CLIP_RECT
				resultColor.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				#endif

				#ifdef UNITY_UI_ALPHACLIP
				clip (resultColor.a - 0.001);
				#endif

				return resultColor;
			}
			ENDCG
		}
	}
}
