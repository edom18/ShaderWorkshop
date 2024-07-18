Shader "Unlit/GridShader"
{
    Properties
    {
        _Scale ("Scale", Float) = 1.0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
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

            float _Scale;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = frac(i.uv * _Scale);
                float4 color = float4(0, 0, 0, 1);

                if (uv.x <= 0.5)
                {
                    if (uv.y >= 0.5)
                    {
                        color.r = 1.0;
                    }
                }
                else
                {
                    if (uv.y <= 0.5)
                    {
                        color.r = 1.0;
                    }
                }

                return color;
            }
            ENDCG
        }
    }
}