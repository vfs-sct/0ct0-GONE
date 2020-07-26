// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Forcefield"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Main Color", Color) = (1,1,1,1)
        _FadeDistance("Fade Distance", Float) = 5
        _FadeFalloff("Fade Falloff", Float) = 4
    }


    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;               
            };

            struct v2f
            {                
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 pos : TEXCOORD1;
            };


            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.pos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            sampler2D _MainTex;
            float4 _Color;
            float _FadeDistance;
            float _FadeFalloff;

            float4 frag(v2f i) : SV_Target
            {
                float mask = tex2D(_MainTex, i.uv + _Time.yy / 24).r;
                float distance_mask = pow(saturate(1 - length(i.pos - _WorldSpaceCameraPos) / _FadeDistance), _FadeFalloff);
                  
                float opacity = _Color.a * mask * distance_mask;
                return float4(_Color.rgb * 27, opacity);
            }
            ENDCG
        }
    }
}