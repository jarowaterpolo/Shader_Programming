Shader "CustomWaterTexture/WaterShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (0,1,1,1)
        _Color3("Color3", Color) = (0,0,1,1)
        _ShaderNums("ShaderNums", Integer) = 0
        _Iterations("Iterations", Integer) = 1
        _Texture("Texture", 2D) = "white" {}
	}

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "WaterShader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;
            float4      _Color2;
            float4      _Color3;
            int    _ShaderNums; 
            int     _Iterations;
            sampler2D  _Texture;
            float4 _Texture_ST;


            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                // return float4(1, 0, 0, 0.2); semi-transparent red

                float2 uv = IN.localTexcoord.xy;
                float4 color = _Color;
                // color = float4(uv.x,uv.y,uv.y,1) * _Color;

                float PI = 3.14159265359;

                float x = uv.x - .5;
                float y = uv.y - .5;
                float r = sqrt(x*x + y*y);
                float radAngle = atan2(x,-y);
                float a2 = radAngle + fmod(_Time.y, 2 * PI);

                switch(_ShaderNums)
                {
                    case 0:
                        float2 noiseUV = uv + _Time.y * 0.05;
                        float noise = tex2D(_Texture, noiseUV).r;

                        float2 distortion = (noise - 0.5) * 0.05;

                        float2 finalUV = uv + distortion;

                        float depth = uv.x;

                        color = lerp(_Color3, _Color2, finalUV.y);
                        color.a = lerp(0.3, 0.7, depth);
                    break;

                    case 1:
                        float2 noiseUV1 = uv + float2(_Time.y * 0.1, _Time.y * 0.05);

                        float2 noiseSample1 = tex2D(_Texture, noiseUV1).rg;

                        float2 distortion1 = (noiseSample1 - 0.5) * 0.15;

                        float2 finalUV1 = uv + distortion1;

                        float depth1 = uv.x;

                        color = lerp(_Color3, _Color2, finalUV1.x);
                        color.a = lerp(0.3, 0.7, depth1);
                    break;

                    case 4:
                    if (fmod(int(sin(uv.x) * 50), 2) == 0 && fmod(int(sin(uv.y) * 50), 2) == 0)
                    {
                        color = float4(0,1,1,1);
                    }
                    else
                    {
                        color = float4(0,0,0,0);
                    }
                    break;

                    case 9:
                        color = float4(uv.x * uv.y,uv.x / uv.y,1,1);
                    break;

                    case 15:
                        float a = sin(uv.x * PI * _Iterations);
                        // float a = sin(uv.x * PI);
                        // float a = uv.x;
                        if (a < 0)
                        {
                            a *= -1;
                        }

                        //step == step(a,b)
                        //if a >= b then return 1 else 0
                        if (step(a, uv.y) == 0)
                        {
                            color = _Color2;
                        }
                        else
                        {
                            color = _Color;
                        }
                    break;

                    case 20:
                        float c = fmod((uv.x + uv.y / 2) * _Iterations, 2);
                        color = float4(0,c,1,1);
                    break;

                    case 30:
                        x = uv.x - fmod(_Time.y, 1.5) +.5;
                        y = uv.y - .5;

                        float r2 = sqrt(x*x + y*y);
                        float dist2 = pow(r, 2);
                        radAngle = atan2(x,-y);

                        if (radAngle > 0 && radAngle < 180 && r2 < .5 && r2 > .4)
                        {
                            float t = 1 - uv.x;
                            // float t = saturate(1 - uv.x);
                            // float t = step(0.5, uv.x);
                            color = lerp(_Color2, _Color3, t);
                            // color = _Color2;
                        }
                        else
                        {
                            color = _Color;
                        }
                    break;
                }

				return color;
            }
            ENDCG
        }
    }
}
