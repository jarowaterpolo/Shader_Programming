Shader "Unlit/Waves"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ReflecTex("Reflect Texture", 2D) = "white" {}
		_WaveSpeed("Wave Speed", float) = 1
		_Height("Height", float) = 1
		_WaveAmount("Amount of Waves", Integer) = 1
	}
	SubShader
	{
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _ReflecTex;

			float _WaveSpeed;
			float _Height;
			int _WaveAmount;

			v2f vert(appdata v)
			{
				v2f o;
				// TODO:
				//  -move the vertex up and down in a wave pattern
				float pi = 3.14159265359;
				float wave = sin(_Time.y * pi * _WaveSpeed + ((v.uv.x) + (v.uv.y)) * pi * _WaveAmount);

				float4 height = tex2Dlod(_MainTex, float4(v.uv, 0, 0));

				v.vertex.y += height + wave * _Height;

				float4 modVertex = v.vertex;
				o.vertex = UnityObjectToClipPos(modVertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) + tex2D(_ReflecTex, i.uv);
				col.a = .5;
				return col;
			}
			ENDCG
		}
	}
}
