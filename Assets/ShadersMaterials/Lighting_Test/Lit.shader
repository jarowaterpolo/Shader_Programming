Shader "Unlit/Lit"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Ai("Ambient Intensity", float) = 0
		_Ac("Ambient Color", Color) = (1,1,1,1)
		_Smoothness("Smoothness", float) = 1
		_Si("Specular Intensity", float) = 0

		_LightColor("Light Color", Color) = (0,0,0,0)
		_LightDir("Light Dir", Vector) = (1,0,0)
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		LOD 100 

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc" // NEEDED FOR LightColor0 !

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
				float4 worldPos : TEXCOORD1;
				float4 normal : NORMAL;
			};

			sampler2D _MainTex;

			v2f vert(appdata v)
			{
				v2f o;
				o.worldPos = mul(UNITY_MATRIX_M, v.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.normal = float4(UnityObjectToWorldNormal(v.normal) ,0.0);
				return o;
			}

			float _Ai;
			float4 _Ac;
			float _Smoothness;
			float _Si;

			float4 _LightColor;
			float3 _LightDir;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 albedo = tex2D(_MainTex, i.uv);

				float4 _A = _Ac * _Ai; 
				float3 N = normalize(i.normal);
				float3 L = normalize(_LightDir.xyz);
				// float3 L  = _WorldSpaceLightPos0;
				float _Di = max(dot(N, -L),0);

				float3 LightReflection = float3(reflect(-L, N));

				// float _Sf = pow(max(dot(normalize(_WorldSpaceCameraPos - i.normal), LightReflection),0),_Smoothness);
				float _Sf = pow(max(dot(normalize(_WorldSpaceCameraPos - i.worldPos.xyz), LightReflection),0),_Smoothness);

				float4 col = (_A + _Di * _LightColor) * albedo + _Si * _Sf * _LightColor;
				
				//return float4(L, 1);
				// return float4(_Di, _Di, _Di, 1);

				// return float4(LightReflection,1);

				// return float4(_WorldSpaceCameraPos, 1);

				return col;
			}
			ENDCG
		}

		// cast shadows:
		// Pass
		// {
		// 	Tags{ "LightMode" = "ShadowCaster" }
		// 	CGPROGRAM
		// 	#pragma vertex VSMain
		// 	#pragma fragment PSMain

		// 	float4 VSMain(float4 vertex:POSITION) : SV_POSITION
		// 	{
		// 		return UnityObjectToClipPos(vertex);
		// 	}

		// 	float4 PSMain(float4 vertex:SV_POSITION) : SV_TARGET
		// 	{
		// 		return 0;
		// 	}

		// 	ENDCG
		// }
	}
}
