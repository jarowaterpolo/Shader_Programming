Shader "CustomScoreBoardTexture/ScoreBoardShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (0,1,1,1)
        _Color3("Color3", Color) = (0,0,1,1)
        _Texture("Numbers", 2D) = "white" {}
        _LeftTeamScore("Score Left", float) = 0
        _RightTeamScore("Score Right", float) = 0

        [HDR] _Emission ("Color", Color) = (1,1,1,1)
	}

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "ScoreBoardShader"

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

            float _LeftTeamScore;
            float _RightTeamScore;

            float4 _Emission;

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

                int LeftScoreTents = (int)_LeftTeamScore/10;
                int LeftScore = fmod(_LeftTeamScore, 10);

                int RightScoreTents = (int)_RightTeamScore/10;
                int RightScore = fmod(_RightTeamScore, 10);

                if (uv.x > .8)
                {
                    x = (uv.x - .8) / (.2);
                    uv.x = x;
                    uv = SetUV(uv, RightScore);
                }
                else if (uv.x > .59 && uv.x < .8)
                {
                    x = (uv.x - .6) / (.2);
                    uv.x = x;
                    uv = SetUV(uv, RightScoreTents); 
                }
                else if (uv.x > .39 && uv.x < .6)
                {
                    x = (uv.x - .4) / (.21);
                    uv.x = x;
                    uv = SetUV(uv, 10);
                }
                else if (uv.x > .2 && uv.x < .4)
                {
                    x = (uv.x - .2) / (.2);
                    uv.x = x;
                    uv = SetUV(uv, LeftScore); 
                }
                else if (uv.x < .2)
                {
                    x = (uv.x ) / (.2);
                    uv.x = x;
                    uv = SetUV(uv, LeftScoreTents);
                }

                color = tex2D(_Texture, uv) * _Color3;
				return color * _Emission;
            }
            ENDCG
        }
    }
}
