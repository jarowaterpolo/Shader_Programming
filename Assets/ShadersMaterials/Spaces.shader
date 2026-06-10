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

        _ACol("Ambient Color", Color) = (1,1,1,1)
		_AInt("Ambient Intensity", Float) = 0.1
		_SInt("Specular Intensity", Float) = 0.1
		_smoothness("Smoothness", Float) = 0.1

        _LightColor("Light Color", Color) = (0,0,0,0)
		_LightDir("Light Dir", Vector) = (1,0,0,0)

        [HDR] _Emission ("Emission_Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc" // NEEDED FOR LightColor0 !

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
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

            float4		_ACol;
			float		_AInt;
			float		_SInt;
			float		_smoothness;

            float4 _LightColor;
			float4 _LightDir;

            float4 _Emission;

            float2 RotateOnZ(float2 XY, float RadZ)
            {
                float x = XY.x * cos(RadZ) - XY.y * sin(RadZ);
                float y = XY.x * sin(RadZ) + XY.y * cos(RadZ);

                XY = float2(x,y);

                return XY;
            }

            float2 RotateOnX(float2 YZ, float RadX)
            {
                float y = YZ.x * cos(RadX) - YZ.y * sin(RadX);
                float z = YZ.x * sin(RadX) + YZ.y * cos(RadX);

                YZ = float2(y,z);

                return YZ;
            }

            float2 RotateOnY(float2 XZ, float RadY)
            {
                float x = XZ.x * cos(RadY) + XZ.y * sin(RadY);
                float z = XZ.x * -sin(RadY) + XZ.y * cos(RadY);

                XZ = float2(x,z);

                return XZ;
            }

            v2f vert (appdata v)
            {
                v2f o;
                
                float4 result = v.vertex; 
                float3 RotatedNormal = v.normal;

                float3 calculatedDegrees = _Degrees.xyz;
                float3 calculatedTranslate = _Translate.xyz;

                float PI = 3.14159265359;

                if (_RotateTime.x == 1)
                {
                    calculatedDegrees.x += _Time.y * _RotateSpeed;
                }

                if (_RotateTime.y == 1)
                {
                    calculatedDegrees.y += _Time.y * _RotateSpeed;
                }

                if (_RotateTime.z == 1)
                {    
                    calculatedDegrees.z += _Time.y * _RotateSpeed;
                }

                if (_TranslateTime.x == 1)
                {
                    calculatedTranslate.x += sin(_Time.y * _TranslateSpeed);    
                }
                
                if (_TranslateTime.y == 1)
                {
                    calculatedTranslate.y += sin(_Time.y * _TranslateSpeed);    
                }
                
                if (_TranslateTime.z == 1)
                {
                    calculatedTranslate.z += sin(_Time.y * _TranslateSpeed);    
                }

                
                float radX = radians(calculatedDegrees.x);
                float radY = radians(calculatedDegrees.y);
                float radZ = radians(calculatedDegrees.z);

                result = float4(RotateOnZ(result.xy, radZ),result.z,result.w);
                result = float4(result.x,RotateOnX(result.yz, radX),result.w);
                float2 xz = RotateOnY(result.xz, radY);
                result = float4(xz.x,result.y,xz.y,result.w);

                result = mul(UNITY_MATRIX_M, result);

                result.y += calculatedTranslate.y;
                o.worldPos = result.xyz;

                o.vertex = mul(UNITY_MATRIX_VP, result);

                o.uv = v.uv;

                RotatedNormal = float3(RotateOnZ(RotatedNormal.xy, radZ),RotatedNormal.z);
                RotatedNormal = float3(RotatedNormal.x,RotateOnX(RotatedNormal.yz, radX));
                float2 Rotxz = RotateOnY(RotatedNormal.xz, radY);
                RotatedNormal = float3(Rotxz.x,RotatedNormal.y,Rotxz.y);

                o.normal = normalize(mul((float3x3)UNITY_MATRIX_M, RotatedNormal));

                // o.normal = mul(UNITY_MATRIX_VP, RotatedNormal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 Texcol = tex2D(_MainTex, i.uv);

                float3 normal = normalize(i.normal);

                float diffInt = max(dot(normal, -_LightDir), 0);

                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
				float3 reflection = reflect(_LightDir, normal);
				float specForm = pow(max(dot(viewDir, reflection), 0), _smoothness);
				float4 col = _AInt * _ACol + (diffInt + _SInt * specForm) * _LightColor * Texcol;

                return col;
            }
            ENDCG
        }
    }
}
