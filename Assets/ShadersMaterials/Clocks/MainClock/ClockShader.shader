Shader "CustomClockTexture/ClockShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (0,1,1,1)
        _Color3("Color3", Color) = (0,0,1,1)
        _Countdown("Countdown", Float) = 30
        _Texture("Numbers", 2D) = "white" {}
	}

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "ClockShader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;
            float4      _Color2;
            float4      _Color3;
            float     _Countdown;
            sampler2D   _Texture;

                float2 SetUV(float2 uv, int num)
                {
                    uv.x /= 4;
                    uv.y /= 3;

                    int h = fmod(num, 4.0);
                    int v = num / 4;

                    uv.x += (h / 4.0);
                    uv.y += (v / 3.0);

                    return uv;
                }

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;
                float4 color = _Color;

                float x;

                int Countdown = (int)_Countdown;

                int k = Countdown / 60;
                int sec = Countdown % 60;

                int i = sec % 10;
                int j = sec / 10;

                // u 1 == u 800
                // v 1 == v 600

                // u 1/4 == u 200
                // v 1/3 == v 200

                // uv.x /= 4;
                // uv.y /= 3;

                // uv.x += (1.0/4.0);
                // uv.y += (1.0/3.0);

                if (uv.x > .75)
                {
                    x = (uv.x - .75) / (.25);
                    uv.x = x;
                    uv = SetUV(uv, i);
                }
                else if (uv.x > .5 && uv.x < .75)
                {
                    x = (uv.x - .5) / (.25);
                    uv.x = x;
                    uv = SetUV(uv, j);
                }
                else if (uv.x > .25 && uv.x < .5)
                {
                    x = (uv.x - .25) / (.25);
                    uv.x = x;
                    uv = SetUV(uv, 10); 
                }
                else if (uv.x < .25)
                {
                    x = (uv.x ) / (.25);
                    uv.x = x;
                    uv = SetUV(uv, k);
                }

                color = tex2D(_Texture, uv) * _Color3;
				return color;
            }
            ENDCG
        }
    }
}
