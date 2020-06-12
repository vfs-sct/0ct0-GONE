Shader "Outlined/Custom Constant Width2" 
{
	Properties
	{
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0, 5)) = .1
		_Limiter("Outline width limiter", Range(0, 5)) = 0
	}				

	SubShader
	{
		Pass
		{
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGINCLUDE
			#include "UnityCG.cginc"

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : POSITION;
				float4 color : COLOR;
			};

			uniform float _Outline;
			uniform float _Limiter;
			uniform float4 _OutlineColor;

			v2f vert(appdata v)
			{
				v2f o;

				float multiplier = unity_OrthoParams.x * 0.1;

				if (multiplier < _Limiter)
				{
					multiplier = _Limiter;
				}

				v.vertex *= _Outline * multiplier;

				o.pos = UnityObjectToClipPos(v.vertex);

				o.color = _OutlineColor;
				return o;
			}

			half4 frag(v2f i) :COLOR
			{
				return float4(1,0,0,1);
				return i.color;
			}

			ENDCG
		}
	}
}