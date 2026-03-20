Shader "CustomGoalTexture/GoalShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (0,1,1,1)
        _Color3("Color3", Color) = (0,0,1,1)
        _Iterations("Iterations", Integer) = 1
        _Texture("Texture", 2D) = "white" {}
	}

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "GoalShader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4      _Color;
            float4      _Color2;
            float4      _Color3;
            int     _Iterations;
            sampler2D  _Texture;
            float4 _Texture_ST;


            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                // return float4(1, 0, 0, 0.2); semi-transparent red

                float2 uv = IN.localTexcoord.xy;
                float4 color = _Color;
                // color = float4(uv.x,uv.y,uv.y,1) * _Color;

                // float PI = 3.14159265359;

                float x = uv.x - .5;
                float y = uv.y - .5;
                // float r = sqrt(x*x + y*y);
                // float radAngle = atan2(x,-y);
                // float a2 = radAngle + fmod(_Time.y, 2 * PI);

                if (uv.x <= .1 || uv.x >= .9 || uv.y > .8)
                {
                    color = _Color;
                }
                else
                {
                    float gridscale = 10;
                    float2 st = frac(uv * gridscale);

                    float thickness = 0.1;

                    float LineX = step(st.x, thickness);
                    float LineY = step(st.y, thickness);

                    float netPattern = max(LineX, LineY);

                    color = _Color;

                    clip(netPattern - 0.01);
                }
               
                clip(color.a - 0.01);

				return color;
            }
            ENDCG
        }
    }
}
