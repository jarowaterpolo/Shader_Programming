Shader "CustomRenderTexture/FirstShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (0,1,1,1)
        _Color3("Color3", Color) = (0,0,1,1)
        _ShaderNums("ShaderNums", Integer) = 0
        _Iterations("Iterations", Integer) = 1
        _Texture("Texture", 2D) = "white" {}

        [Header(Shape 1)]
        [Space(10)]
        _Center("Center", Vector) = (0.5,0.5,0,0)
        _Size("Size", Range(0,1)) = .5
        [Space(40)]
        [Header(Shape 2)]
        [Space(10)]
        _Center2("Center2", Vector) = (0.5,0.5,0,0)
        _Size2("Size2", Range(0,1)) = .5
        [Space(40)]
        [Header(Shape 3)]
        [Space(10)]
        _Center3("Center3", Vector) = (0.5,0.5,0,0)
        _Size3("Size3", Range(0,1)) = .5
	}

    SubShader
    {
        Blend One Zero

        Pass
        {
            Name "FirstShader"

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
                float4 color = float4(uv.x,uv.y,uv.y,1) * _Color;

                float PI = 3.14159265359;

                float x = uv.x - .5;
                float y = uv.y - .5;
                float r = sqrt(x*x + y*y);
                float radAngle = atan2(x,-y);
                float a2 = radAngle + fmod(_Time.y, 2 * PI);

                switch(_ShaderNums)
                {
                    default:
                        color = color;
                    break;
                    case 0:
                    if (uv.x > 0.66)
                    {
                        color = float4(1,.5,0,1);
                    }
                    else if (0.33 < uv.x  < 0.66)
                    {
                        color = float4(0,1,0,1);
                    }
                    else
                    {
                        color = float4(0,1,1,1);
                    }
                    break;

                    case 1:
                    if (uv.y > 0.66)
                    {
                        color = float4(1,.5,0,1);
                    }
                    else if (0.33 < uv.y  < 0.66)
                    {
                        color = float4(0,1,0,1);
                    }
                    else
                    {
                        color = float4(0,1,1,1);
                    }
                    break;

                    case 2:
                    if (fmod(int(uv.x * 10), 2) == 0)
                    {
                        color = float4(0,1,1,1);
                    }
                    else
                    {
                        color = float4(uv.x * 100,0,0,1);
                    }
                    break;

                    case 3:
                    if (sin(uv.x) < .66)
                    {
                        color = float4(0,1,1,1);
                    }
                    else
                    {
                        color = float4(0,0,0,0);
                    }
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

                    case 5:
                        color = float4(cos(uv.x),sin(uv.y),cos(uv.y),1);
                    break;

                    case 6:
                        color = float4(sin(uv.x),cos(uv.y),sin(uv.y),1);
                    break;

                    case 7:
                        color = float4(sin(uv.y),cos(uv.x),sin(uv.x),1);
                    break;

                    case 8:
                        color = float4(cos(uv.y),sin(uv.x),cos(uv.x),1);
                    break;

                    case 9:
                        color = float4(uv.x * uv.y,uv.x / uv.y,1,1);
                    break;

                    case 10:
                        color = float4(uv.x / uv.y,uv.x * uv.y,1,1);
                    break;

                    case 11:
                        color = float4(1,uv.x / uv.y,uv.x * uv.y,1);
                    break;

                    case 12:
                        color = float4(1,uv.x * uv.y,uv.x / uv.y,1);
                    break;

                    case 13:
                        color = float4(uv.x / uv.y,1,uv.x * uv.y,1);
                    break;

                    case 14:
                      color = float4(uv.x * uv.y,1,uv.x / uv.y,1);
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

                    case 16:
                        color = float4(0, sin(_Time.y), sin(_Time.y), 1);
                    break;

                    case 17:
                        if (a2 < radians(180))
                        {
                            color = _Color;
                        }
                        else
                        {
                            color = _Color2;
                        }
                    break;

                    case 18:
                        if (.5 < length(x+y) < 1)
                        {
                            color = _Color2;
                        }
                        else
                        {
                            color = _Color;
                        }
                    break;

                    case 19:
                        if (.5 < length(r*4) < 1)
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

                    case 21:
                        float c1 = length(r*1.5);
                        float dist = pow(c1, 2);
                        float ring = abs(dist - 0.35);
                        if (ring < .05)
                        {
                            color = _Color2;
                        }
                        else
                        {
                            color = _Color;
                        }
                    break;

                    case 22:
                        color = tex2D(_Texture, uv);

                    break;

                    case 23:
                        color = tex2D(_Texture, uv) * _Color2;
                    break;
                     
                    case 24:
                        color = tex2D(_Texture, -uv);
                    break;

                    case 25:
                        color = tex2D(_Texture, uv);
                        color = float4(color.z, color.x, color.y, 1);
                    break;

                    case 26:
                        color = tex2D(_Texture, uv);
                        color = float4(color.y, color.z, color.x, 1);
                    break;

                    case 27:
                        color = tex2D(_Texture, uv);
                        color = float4(1 - color);
                    break;

                    case 28:
                        if (a2 < radians(180))
                        {
                            color = tex2D(_Texture, uv);
                        }
                        else
                        {
                            color = tex2D(_Texture, uv);
                            color = float4(1 - color);
                        }
                    break;

                    case 29:
                        if (length(uv.x + uv.y) >= .6 && length(uv.x - uv.y) <= .4 && uv.y <= .5)
                        {
                            color = _Color2;
   
                        }
                        else
                        {
                            x = uv.x - .25;
                            y = uv.y - .5;
                            float c1 = sqrt(pow(x,2) * 2 + pow(y,2)); 
                            x = uv.x - .75;
                            y = uv.y - .5;
                            float c2 = sqrt(pow(x,2) * 2 + pow(y,2)); 
                            //
                            if (0.5 < length(c1) < 1 && uv.y >= .5)
                            {
                                color = _Color2;
                            }
                            else if (0.5 < length(c2) < 1 && uv.y >= .5)
                            {
                                color = _Color2;
                            }
                            else
                            {
                                color = _Color;
                            }
                            //
                        }
                    break;

                    case 30:
                        x = uv.x - fmod(_Time.y, 1.5) +.5;
                        y = uv.y - .5;

                        float r2 = sqrt(x*x + y*y);
                        float dist2 = pow(r, 2);
                        radAngle = atan2(x,-y);

                        if (radAngle > 0 && radAngle < 180 && r2 < .5 && r2 > .4)
                        {
                            color = lerp(_Color2, _Color3, 1 - uv.x);
                            // color = _Color2;
                        }
                        else
                        {
                            color = _Color;
                        }
                    break;
                    
                    case 31:
                        float move = sin(_Time.y / .5) * .125;
                        float slightSizeChange = (sin(_Time.y / .25) * .5 + .5) * .125;

                        uv.x += move;
                        uv.y = uv.y - fmod(_Time.y / 2, 1.5) + .5;
                        
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

                    break;

                }

				return color;
            }
            ENDCG
        }
    }
}
