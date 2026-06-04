// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/MatrixSpaceTransformations"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Degrees ("amountOfDegreesToRotate", Vector) = (45,30,15,0)
        _Scaling ("Scale", Vector) = (1,1,1,0)
        _Translate ("Transform", Vector) = (1,1,1,0)
        _RotateTime ("Rotate Overtime", Vector) = (0,0,0)
        _TranslateTime ("Translate Overtime", Vector) = (0,0,0)
        _RotateSpeed ("Rotate Speed", float) = 1
        _TranslateSpeed ("Move Speed", float) = 1
        _MaxTranslations ("max translations", Vector) = (0,10,0)

        _Move ("Move or not", float) = 0
        _CornerStartTime ("Corner Start Time", Float) = 0

        [HDR] _Emission ("Emission_Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Degrees;
            float RadiansX;
            float RadiansY;
            float RadiansZ;

            float4 _Scaling;

            float4 _Translate;
            
            float _RotateSpeed;
            float _TranslateSpeed;

            float3 _RotateTime;
            float3 _TranslateTime;

            float3 _MaxTranslations;
            float _Move;

            float _CornerStartTime;

            float4 _Emission;

            v2f vert (appdata v)
            {
                v2f o;
                
                float4 result = v.vertex; 

                float PI = 3.14159265359;

                if (_RotateTime.x == 1)
                {
                    _Degrees.x += _Time.y * _RotateSpeed;
                }

                if (_RotateTime.y == 1)
                {
                    _Degrees.y += _Time.y * _RotateSpeed;
                }

                if (_RotateTime.z == 1)
                {    
                    _Degrees.z += _Time.y * _RotateSpeed;
                }

                if (_TranslateTime.x == 1)
                {
                    _Translate.x += _Time.y * _TranslateSpeed;    
                }
                
                if (_TranslateTime.y == 1)
                {
                    _Translate.y += _Time.y * _TranslateSpeed;    
                }
                else
                {
                    if (_Move == 1)
                    {                        
                        if (_TranslateTime.y == .5)
                        {
                            float t = _Time.y - _CornerStartTime;

                            float normalized = saturate(t * _TranslateSpeed);

                            float bounce = sin(normalized * PI);

                            _Translate.y = bounce * _MaxTranslations.y;
                        } 
                    }
                    else
                    {
                        _Translate.y = 0;    
                    }
                }
                
                if (_TranslateTime.z == 1)
                {
                    _Translate.z += _Time.y * _TranslateSpeed;    
                }
                else
                {
                    if (_Move == 1)
                    {   
                        if (_TranslateTime.z == .5)
                        {
                            _Translate.z = (_MaxTranslations.z * .5) * (sin(_Time.y * PI * _TranslateSpeed) + 1);    
                        } 
                    }
                }
                
                RadiansX = radians(_Degrees.x);
                RadiansY = radians(_Degrees.y);
                RadiansZ = radians(_Degrees.z);

                float4x4 RotxM = {
                1,0,0,0,
                0,cos(RadiansX),-sin(RadiansX),0,
                0,sin(RadiansX),cos(RadiansX),0,
                0,0,0,1
                };

                float4x4 RotyM = {
                    cos(RadiansY),0,sin(RadiansY),0,
                    0,1,0,0,
                    -sin(RadiansY),0,cos(RadiansY),0,
                    0,0,0,1
                };

                float4x4 RotzM = {
                    cos(RadiansZ),-sin(RadiansZ),0,0,
                    sin(RadiansZ),cos(RadiansZ),0,0,
                    0,0,1,0,
                    0,0,0,1
                };

                float4x4 ScaleM = {
                    _Scaling.x,0,0,0,
                    0,_Scaling.y,0,0,
                    0,0,_Scaling.z,0,
                    0,0,0,1
                };

                float4x4 TranslateM = {
                    1,0,0,0,
                    0,1,0,0,
                    0,0,1,0,
                    _Translate.x,_Translate.y,_Translate.z,1
                };

                float4x4 ResultM = mul(RotxM,RotyM);
                ResultM = mul(ResultM, RotzM);
                ResultM = mul(ResultM, ScaleM);

                result = mul(ResultM, float4(result));
                result = mul(result, TranslateM);
                result = mul(UNITY_MATRIX_M, result);

                o.vertex = mul(UNITY_MATRIX_VP, result);

                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col * _Emission;
            }
            ENDCG
        }
    }
}
