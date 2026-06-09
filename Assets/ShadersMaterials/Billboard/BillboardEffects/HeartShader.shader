Shader "CustomRenderTexture/HeartShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (0,1,1,1)
        _Color3("Color3", Color) = (0,0,1,1)
        _TimeMult("Time Mult", float) = 2
        _MoveSpeed("MoveSpeed", Vector) = (2,2,0,0)
        _MaxSizeChange("Size Grow", float) = .125
        _MaxMoveOnX("Max movement on X", float) = .125
        _MaxMoveOnY("Max movement on Y", float) = 1.5
        _Offset("Offset", float) = .5
        _MiniHeartsSizeDiff("Mini hearts size difference", float) = .01

        [Header(Shape 1)]
        [Space(10)]
        _Center("Center", Vector) = (0.5,0.5,0,0)
        _Size("StartSize", Range(0,1)) = .5
        [Space(40)]
        [Header(Shape 2)]
        [Space(10)]
        _Center2("Center2", Vector) = (0.5,0.5,0,0)
        _Size2("StartSize2", Range(0,1)) = .5
        [Space(40)]
        [Header(Shape 3)]
        [Space(10)]
        _Center3("Center3", Vector) = (0.5,0.5,0,0)
        _Size3("StartSize3", Range(0,1)) = .5
	}

    SubShader
    {
        Blend One Zero

        Pass
        {
            Name "HeartShader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;
            float4      _Color2;
            float4      _Color3;

            float _TimeMult;
            float2 _MoveSpeed;

            float _MaxSizeChange;
            float _MaxMoveOnX;
            float _MaxMoveOnY;
            float _Offset;

            float _MiniHeartsSizeDiff;

            float4 _Center;
            float _Size;

            float4 _Center2;
            float _Size2;

            float4 _Center3;
            float _Size3;

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;
                // uv /= 2;
                // uv.x += .5;
                float4 color = _Color;

                float PI = 3.14159265359;

                        float move = sin(_Time.y * _MoveSpeed.x) * _MaxMoveOnX;
                        float slightSizeChange = (sin(_Time.y * _TimeMult) / 2 + _Offset) * _MaxSizeChange;

                        uv.x += move;
                        uv.y = uv.y - fmod((_Time.y / _MoveSpeed.y), _MaxMoveOnY) + _Offset;
                        
                        _Size += slightSizeChange;
                        _Size2 += slightSizeChange;
                        _Size3 += slightSizeChange;

                        if (
                            abs(uv.x - _Center.x) + abs(uv.y - _Center.y) < _Size ||
                            length(uv - float2(_Center.x + _Size / 2, _Center.y + _Size / 2)) < sqrt(pow(_Size, 2) * 2) / 2||
                            length(uv - float2(_Center.x - _Size / 2, _Center.y + _Size / 2)) < sqrt(pow(_Size, 2) * 2) / 2
                        )
                        {
                            color = _Color2;
                        }

                        if (
                            abs(uv.x - _Center2.x) + abs(uv.y - _Center2.y) < _Size2 ||
                            length(uv - float2(_Center2.x + _Size2 / 2, _Center2.y + _Size2 / 2)) < sqrt(pow(_Size2, 2) * 2) / 2||
                            length(uv - float2(_Center2.x - _Size2 / 2, _Center2.y + _Size2 / 2)) < sqrt(pow(_Size2, 2) * 2) / 2
                        )
                        {
                            color = _Color2;
                        }

                        if (
                            abs(uv.x - _Center3.x) + abs(uv.y - _Center3.y) < _Size3 ||
                            length(uv - float2(_Center3.x + _Size3 / 2, _Center3.y + _Size3 / 2)) < sqrt(pow(_Size3, 2) * 2) / 2||
                            length(uv - float2(_Center3.x - _Size3 / 2, _Center3.y + _Size3 / 2)) < sqrt(pow(_Size3, 2) * 2) / 2
                        )
                        {
                            color = _Color2;
                        }

                        float MiniSize = _Size - _MiniHeartsSizeDiff;
                        float MiniSize2 = _Size2 - _MiniHeartsSizeDiff;
                        float MiniSize3 = _Size3 - _MiniHeartsSizeDiff;

                        if (
                            abs(uv.x - _Center.x) + abs(uv.y - _Center.y) < MiniSize ||
                            length(uv - float2(_Center.x + MiniSize / 2, _Center.y + MiniSize / 2)) < sqrt(pow(MiniSize, 2) * 2) / 2||
                            length(uv - float2(_Center.x - MiniSize / 2, _Center.y + MiniSize / 2)) < sqrt(pow(MiniSize, 2) * 2) / 2
                        )
                        {
                            color = _Color3;
                        }

                        if (
                            abs(uv.x - _Center2.x) + abs(uv.y - _Center2.y) < MiniSize2 ||
                            length(uv - float2(_Center2.x + MiniSize2 / 2, _Center2.y + MiniSize2 / 2)) < sqrt(pow(MiniSize2, 2) * 2) / 2||
                            length(uv - float2(_Center2.x - MiniSize2 / 2, _Center2.y + MiniSize2 / 2)) < sqrt(pow(MiniSize2, 2) * 2) / 2
                        )
                        {
                            color = _Color3;
                        }

                        if (
                            abs(uv.x - _Center3.x) + abs(uv.y - _Center3.y) < MiniSize3 ||
                            length(uv - float2(_Center3.x + MiniSize3 / 2, _Center3.y + MiniSize3 / 2)) < sqrt(pow(MiniSize3, 2) * 2) / 2||
                            length(uv - float2(_Center3.x - MiniSize3 / 2, _Center3.y + MiniSize3 / 2)) < sqrt(pow(MiniSize3, 2) * 2) / 2
                        )
                        {
                            color = _Color3;
                        }

				return color;
            }
            ENDCG
        }
    }
}
