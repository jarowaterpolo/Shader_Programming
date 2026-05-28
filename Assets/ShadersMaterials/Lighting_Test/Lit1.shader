Shader "URP/Lit1"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Ai("Ambient Intensity", float) = 0
		_Ac("Ambient Color", Color) = (1,1,1,1)
		_Smoothness("Smoothness", float) = 1
		_Si("Specular Intensity", float) = 0
	}

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float3 positionWS : TEXCOORD2;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float _Ai;
            float4 _Ac;
            float _Smoothness;
            float _Si;

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.positionWS = TransformObjectToWorld(v.positionOS.xyz);
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                o.uv = v.uv;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float4 albedo = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                float3 N = normalize(i.normalWS);

                Light mainLight = GetMainLight();
                float3 L = normalize(mainLight.direction);

                float NdotL = saturate(dot(N, L));

                // AMBIENT
                float3 ambient = _Ac.rgb * _Ai;

                // DIFFUSE
                float3 diffuse = mainLight.color * NdotL;

                // VIEW DIRECTION
                float3 V = normalize(_WorldSpaceCameraPos - i.positionWS);

                // SPECULAR (Blinn-Phong style for stability)
                float3 H = normalize(L + V);
                float spec = pow(saturate(dot(N, H)), _Smoothness);
                spec = min(spec, 1.0);
                float3 specular = mainLight.color * spec * _Si;

                // FINAL LIGHTING
                float3 lighting = ambient + diffuse;

                float3 col =
                    albedo.rgb * lighting +
                    specular * albedo.rgb;

                return float4(col, albedo.a);
            }

            ENDHLSL
        }
    }
}