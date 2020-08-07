// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Standard_NovaShade"
{
	Properties
	{
		[NoScaleOffset]_Albedo("Albedo", 2D) = "white" {}
		[NoScaleOffset]_Metalness("Metalness", 2D) = "black" {}
		[NoScaleOffset]_Normalmap("Normal map", 2D) = "bump" {}
		[NoScaleOffset]_AO("AO", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.7
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float2 uv2_texcoord2;
		};

		uniform sampler2D _Normalmap;
		uniform sampler2D _Albedo;
		uniform sampler2D _AO;
		uniform sampler2D _Metalness;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normalmap = i.uv_texcoord;
			o.Normal = UnpackNormal( tex2D( _Normalmap, uv_Normalmap ) );
			float2 uv_Albedo = i.uv_texcoord;
			float2 uv2_AO = i.uv2_texcoord2;
			float4 tex2DNode4 = tex2D( _AO, uv2_AO );
			o.Albedo = ( tex2D( _Albedo, uv_Albedo ) * tex2DNode4 ).rgb;
			float2 uv_Metalness = i.uv_texcoord;
			o.Metallic = tex2D( _Metalness, uv_Metalness ).r;
			o.Smoothness = _Smoothness;
			o.Occlusion = tex2DNode4.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13801
299;513;1666;974;603.3925;148.3142;1;True;True
Node;AmplifyShaderEditor.SamplerNode;1;-644,-24;Float;True;Property;_Albedo;Albedo;0;1;[NoScaleOffset];Assets/sattelite_DefaultMaterial_AlbedoTransparency.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;4;-514,692;Float;True;Property;_AO;AO;3;1;[NoScaleOffset];None;True;1;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;151,-6;Float;False;2;2;0;COLOR;0.0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;6;403.3749,202.57;Float;False;Property;_Smoothness;Smoothness;4;0;0.7;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-518.4,228.8;Float;True;Property;_Metalness;Metalness;1;1;[NoScaleOffset];None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;3;-527.6999,448.1;Float;True;Property;_Normalmap;Normal map;2;1;[NoScaleOffset];None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;696.7412,-11.33201;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/Standard_NovaShade;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;1;0
WireConnection;5;1;4;0
WireConnection;0;0;5;0
WireConnection;0;1;3;0
WireConnection;0;3;2;0
WireConnection;0;4;6;0
WireConnection;0;5;4;0
ASEEND*/
//CHKSM=4645A8FCCC8D4CB6553F775090D174E36DFCF1F0