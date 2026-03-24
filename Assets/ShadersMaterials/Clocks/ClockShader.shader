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
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .4 && p.y < .5 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .4 && p.y < .9|| .6 - offset < p.x && p.x < .7 - offset&& p.y > .1 && p.y < .4)
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
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .4 && p.y < .5 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
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
                        if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9|| .6 - offset < p.x && p.x < .7 - offset&& p.y > .4 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.6 - offset < p.x && p.x < .9 - offset && p.y > .4 && p.y < .5)
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
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .4 && p.y < .5 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .4 || .6 - offset < p.x && p.x < .7 - offset&& p.y > .4 && p.y < .9)
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
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .4 && p.y < .5 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .4 || .6 - offset < p.x && p.x < .7 - offset&& p.y > .1 && p.y < .9)
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
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .4 && p.y < .5 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
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
                        if(.6 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .2 || .6 - offset < p.x && p.x < .9 - offset && p.y > .4 && p.y < .5 || .6 - offset < p.x && p.x < .9 - offset && p.y > .8 && p.y < .9)
                        {
                            col = _Color3;
                        }
                        else
                        {
                            if(.8 - offset < p.x && p.x < .9 - offset && p.y > .1 && p.y < .9 || .6 - offset < p.x && p.x < .7 - offset&& p.y > .4 && p.y < .9)
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

                switch(Countdown)
                {
                    case 30:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 3);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 0);
                        }
                    break;
                                        
                    case 29:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 9);
                        }
                    break;
                                        
                    case 28:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 8);
                        }
                    break;
                                        
                    case 27:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 7);
                        }
                    break;
                                        
                    case 26:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 6);
                        }
                    break;
                                        
                    case 25:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 5);
                        }
                    break;
                                        
                    case 24:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 4);
                        }
                    break;
                                        
                    case 23:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 3);
                        }
                    break;
                                        
                    case 22:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 2);
                        }
                    break;
                                        
                    case 21:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 1);
                        }
                    break;
                                        
                    case 20:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 2);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 0);
                        }
                    break;
                                        
                    case 19:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 9);
                        }
                    break;
                                        
                    case 18:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 8);
                        }
                    break;
                                        
                    case 17:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 7);
                        }
                    break;
                                        
                    case 16:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 6);
                        }
                    break;
                                        
                    case 15:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 5);
                        }
                    break;
                                        
                    case 14:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 4);
                        }
                    break;
                    
                    case 13:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 3);
                        }
                    break;
                    
                    case 12:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 2);
                        }
                    break;

                    case 11:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 1);
                        }
                    break;

                    case 10:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 1);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 0);
                        }
                    break;

                    case 9:
                        color = SetCOLOR(uv, 0, 9);
                    break;
                    
                    case 8:
                        color = SetCOLOR(uv, 0, 8);
                    break;

                    case 7:
                        color = SetCOLOR(uv, 0, 7);
                    break;

                    case 6:
                        color = SetCOLOR(uv, 0, 6);
                    break;

                    case 5:
                        color = SetCOLOR(uv, 0, 5);
                    break;

                    case 4:
                        color = SetCOLOR(uv, 0, 4);
                    break;

                    case 3:
                        color = SetCOLOR(uv, 0, 3);
                    break;

                    case 2:
                        color = SetCOLOR(uv, 0, 2);
                    break;

                    case 1:
                        color = SetCOLOR(uv, 0, 1);
                    break;

                    case 0:
                        color = SetCOLOR(uv, 0, 0);
                    break;

                    default:
                        if(uv.x < .5)
                        {
                            color = SetCOLOR(uv, .5, 0);
                        }
                        else
                        {    
                            color = SetCOLOR(uv, 0, 0);
                        }
                    break;
                }

				return color;
            }
            ENDCG
        }
    }
}
