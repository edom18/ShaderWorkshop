Shader "Unlit/PositionLightingShader"
{
    Properties
    {
        _LightPosition ("Light Position", Vector) = (0, 1, 0, 0)
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
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            float4 _LightPosition;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                const float distance = max(0, length(_LightPosition));
                const float strength = max(0, 1.0 / pow(distance, 2.0));
                const float3 lightDirection = normalize(_LightPosition);
                const float3 normal = i.normal;
                float diffuse = max(0, dot(normal, lightDirection)) * strength;
                
                return float4(diffuse.xxx, 1);
            }
            ENDCG
        }
    }
}
