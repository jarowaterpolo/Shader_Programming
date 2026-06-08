Shader "Unlit/VertexHeightMapper"
{
    Properties
    {
		_HeightMap("HeightMap", 2D) = "black" {}
		_Height("Height scalar", float) = .1
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

			sampler2D _HeightMap;
            float _Height;

            v2f vert (appdata v)
            {
                v2f o;

                float4 height = tex2Dlod(_HeightMap, float4(v.uv,0,0));
                	v.vertex.z += height * _Height;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_HeightMap, i.uv);
				return col;
            }
            ENDCG
        }
    }
}
