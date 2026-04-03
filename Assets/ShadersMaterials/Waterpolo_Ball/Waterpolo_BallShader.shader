Shader "CustomWaterpoloBallTexture/WaterpoloBallShader"
{
	Properties
	{
		_Color("Color", Color) = (1,0.85,0.2,1)
        _Color2("Color2", Color) = (0,0,0,1)
        _Texture("BallImg", 2D) = "white" {}
        _TextureStrength("Texture Strength", Range(0,1)) = 1
        _LineWidth("Line Width", Range(0.001, 0.05)) = 0.01
        _BandHeight("Center Band Height", Range(0.01, 0.3)) = 0.12
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

            float DrawSeam(float value, float width)
            {
                return smoothstep(width, 0.0, abs(value));
            }

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;

                float PI = 3.14159265359;

                float2 p = uv - .5;

                float angle = atan2(p.y, p.x);
                float radius = length(p);

                float seams = 0;


                seams += DrawSeam(uv.y - .33, _LineWidth);
                seams += DrawSeam(uv.y - .66, _LineWidth);

                float segmentCount = 4.0;

                float VerticalLines = frac(uv.x * segmentCount);

                if (uv.y > .33 && uv.y < .66)
                {
                    seams += DrawSeam(VerticalLines - .5, _LineWidth);
                }
                else
                {
                    seams += DrawSeam(frac(VerticalLines + .5) - .5, _LineWidth);
                }

                float band = smoothstep(_BandHeight, 0.0, abs(p.y));

                float3 col = _Color.rgb;

                col = lerp(col, _Color2.rgb, saturate(seams));

                col *= lerp(1.0, .9, band);

                float4 tex = tex2D(_Texture, uv);

                col = lerp(col, tex.rgb, tex.a * band * _TextureStrength);

                return float4(col, 1);
            }
            ENDCG
        }
    }
}
