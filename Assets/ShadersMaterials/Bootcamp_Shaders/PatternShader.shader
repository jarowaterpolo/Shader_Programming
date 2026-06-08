Shader "CustomRenderTexture/PatternShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
	}

    SubShader
    {
        Blend One Zero

        Pass
        {
            Name "PatternShader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;
                float4 color = float4(uv.x,uv.y,0,1) * _Color;
				return color;
            }
            ENDCG
        }
    }
}
