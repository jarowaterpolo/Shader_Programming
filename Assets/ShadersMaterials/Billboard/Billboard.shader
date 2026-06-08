Shader "Unlit/Billboard"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Degrees ("DegreesToRotateY", float) = 45
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;

                float4 origin = float4(0,0,0,1);
                float4 worldOrigin = mul(UNITY_MATRIX_M, origin);
                float4 viewOrigin = mul(UNITY_MATRIX_V, worldOrigin);
                float4 translation = viewOrigin - worldOrigin;
                
                float4 worldPos = mul(UNITY_MATRIX_M, v.vertex);
                float4 viewPos = worldPos + translation;

                o.vertex = mul(UNITY_MATRIX_P, viewPos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
