// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Spaces"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Degrees ("amountOfDegreesToRotate", Vector) = (45,30,15,0)
        _Translate ("Transform", Vector) = (1,1,1,0)
        _RotateTime ("Rotate Overtime", Vector) = (0,0,0)
        _TranslateTime ("Translate Overtime", Vector) = (0,0,0)
        _RotateSpeed ("Rotate Speed", float) = 1
        _TranslateSpeed ("Translate Speed", float) = 1
        _MaxTranslations ("max translations", Vector) = (0,10,0)

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

            float4 _Emission;

            float2 RotateOnZ(float2 XY)
            {
                float x = XY.x * cos(RadiansZ) - XY.y * sin(RadiansZ);
                float y = XY.x * sin(RadiansZ) + XY.y * cos(RadiansZ);

                XY = float2(x,y);

                return XY;
            }

            float2 RotateOnX(float2 YZ)
            {
                float y = YZ.x * cos(RadiansX) - YZ.y * sin(RadiansX);
                float z = YZ.x * sin(RadiansX) + YZ.y * cos(RadiansX);

                YZ = float2(y,z);

                return YZ;
            }

            float2 RotateOnY(float2 XZ)
            {
                float x = XZ.x * cos(RadiansY) + XZ.y * sin(RadiansY);
                float z = XZ.x * -sin(RadiansY) + XZ.y * cos(RadiansY);

                XZ = float2(x,z);

                return XZ;
            }

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
                    _Translate.x += sin(_Time.y * _TranslateSpeed);    
                }
                
                if (_TranslateTime.y == 1)
                {
                    _Translate.y += sin(_Time.y * _TranslateSpeed);    
                }
                
                if (_TranslateTime.z == 1)
                {
                    _Translate.z += sin(_Time.y * _TranslateSpeed);    
                }

                
                RadiansX = radians(_Degrees.x);
                RadiansY = radians(_Degrees.y);
                RadiansZ = radians(_Degrees.z);

                result = float4(RotateOnZ(result.xy),result.z,result.w);
                result = float4(result.x,RotateOnX(result.yz),result.w);
                float2 xz = RotateOnY(result.xz);
                result = float4(xz.x,result.y,xz.y,result.w);

                result = mul(UNITY_MATRIX_M, result);

                result.y += _Translate.y;

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
