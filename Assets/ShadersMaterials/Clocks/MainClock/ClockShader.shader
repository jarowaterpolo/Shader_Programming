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

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;
                float4 color = _Color;

                float x = uv.x - .5;
                float y = uv.y - .5;

                int Countdown = (int)_Countdown;

                int k = Countdown / 60;
                int sec = Countdown % 60;

                int i = sec % 10;
                int j = sec / 10;

                if (k < 1 && j < 1)
                {
                    color = SetCOLOR(uv, 0, i);
                }
                else
                {
                    if (k < 1)
                    {
                        if (uv.x < .66)
                        {
                                color = SetCOLOR(uv, .33, j);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, i);
                        }
                    }
                    else
                    {
                        if (uv.x < .66)
                        {
                            if (uv.x < .33)
                            {
                                x = uv.x - .28;
                                y = uv.y - .65;
                                float c1 = sqrt(pow(x,2) * 2 + pow(y,2)); 

                                x = uv.x - .28;
                                y = uv.y - .35;
                                float c2 = sqrt(pow(x,2) * 2 + pow(y,2)); 

                                if (.5 < length(c1*10) < 1 || .5 < length(c2*10) < 1)
                                {
                                    color = _Color3;
                                }
                                else
                                {
                                    color = SetCOLOR(uv, .66, k);
                                }
                            }
                            else
                            {
                                color = SetCOLOR(uv, .33, j);
                            }
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, i);
                        }
                    }
                }
                color = tex2D(_Texture, uv);
				return color;
            }
            ENDCG
        }
    }
}
