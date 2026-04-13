Shader "CustomWaterpoloBallTexture/WaterpoloBallShader"
{
	Properties
	{
		_Color("Color", Color) = (1,0.85,0.2,1)
        _Color2("Color2", Color) = (0,0,0,1)
        _Texture("BallImg", 2D) = "white" {}
        _TextureStrength("Texture Strength", Range(0,1)) = 1
        _LineWidth("Line Width", Range(0.001, 0.05)) = 0.01
	}

    SubShader
    {
        Blend One Zero

        Pass
        {
            Name "WaterpoloBallShader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;
            float4      _Color2;

            float _LineWidth;
            float _BandHeight;
            
            sampler2D  _Texture;
            float4 _Texture_ST;
            float _TextureStrength;


            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;

                float PI = 3.14159265359;

                float2 p = uv - .5;

                float angle = atan2(p.y, p.x);
                float radius = length(p);

                float4 color = _Color;

                if (uv.x < _LineWidth || uv.x > 1 - _LineWidth || uv.y < _LineWidth || uv.y > .33 - _LineWidth && uv.y < .33 + _LineWidth  || uv.y > .66 - _LineWidth && uv.y < .66 + _LineWidth || uv.y > 1 - _LineWidth)
                {
                    color = _Color2;
                }
                else
                {
                    color = _Color;
                }

                return color;
            }
            ENDCG
        }
    }
}
