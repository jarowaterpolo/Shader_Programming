Shader "CustomClockTexture/ClockShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (0,1,1,1)
        _Color3("Color3", Color) = (0,0,1,1)
        _Countdown("Countdown", Float) = 30
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

            float4 SetCOLOR(float2 p, float offset, int num)
            {
                float4 col;

                switch(num)
                {
                    case 0:
                        if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9|| .6 - offset < p.x && p.x < .7 - offset&& p.y > .1 && p.y < .9)
                        {   
                            col = _Color3;
                        }
                        else
                        {
                            if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                            {
                                col = _Color3;
                            }
                            else
                            {
                                col = _Color2;
                            }
                        }
                    break;

                    case 1:
                        if (.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            col = _Color2;
                        }
                    break;

                    case 2:
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .45 && p.y < .55 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .45 && p.y < .9|| .6 - offset < p.x && p.x < .7 - offset&& p.y > .1 && p.y < .45)
                            {
                                col = _Color3;
                            }
                            else
                            {
                                col = _Color2;
                            }
                        }
                    break;

                    case 3:
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .45 && p.y < .55 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9)
                            {
                                col = _Color3;
                            }
                            else
                            {
                                col = _Color2;
                            }
                        }
                    break;

                    case 4:
                        if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9|| .6 - offset < p.x && p.x < .7 - offset&& p.y > .45 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.6 - offset < p.x && p.x < .9 - offset && p.y > .45 && p.y < .55)
                            {
                                col = _Color3;
                            }
                            else
                            {
                                col = _Color2;
                            }
                        }
                    break;

                    case 5:
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .45 && p.y < .55 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .45 || .6 - offset < p.x && p.x < .7 - offset&& p.y > .45 && p.y < .9)
                            {
                                col = _Color3;
                            }
                            else
                            {
                                col = _Color2;
                            }
                        }
                    break;

                    case 6:
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .45 && p.y < .55 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .45 || .6 - offset < p.x && p.x < .7 - offset&& p.y > .1 && p.y < .9)
                            {
                                col = _Color3;
                            }
                            else
                            {
                                col = _Color2;
                            }
                        }
                    break;

                    case 7:
                        if (.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9 || .6 - offset < p.x && p.x < .7 - offset && p.y > .6 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                            {
                                col = _Color3;
                            }
                            else
                            {
                            col = _Color2;
                            }
                        }
                    break;

                    case 8:
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .45 && p.y < .55 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9 || .6 - offset < p.x && p.x < .7 - offset&& p.y > .1 && p.y < .9)
                            {
                                col = _Color3;
                            }
                            else
                            {
                                col = _Color2;
                            }
                        }
                    break;

                    case 9:
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .45 && p.y < .55 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9 || .6 - offset < p.x && p.x < .7 - offset&& p.y > .45 && p.y < .9)
                            {
                                col = _Color3;
                            }
                            else
                            {
                                col = _Color2;
                            }
                        }
                    break;
                }

                return col;
            }

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;
                float4 color = _Color;

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
                                color = SetCOLOR(uv, .66, k);
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
				return color;
            }
            ENDCG
        }
    }
}
