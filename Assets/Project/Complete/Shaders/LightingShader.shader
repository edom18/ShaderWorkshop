Shader "Unlit/LightingShader"
{
    Properties
    {
        _LightDirection ("Light Direction", Vector) = (0.5, 0.5, 0.5, 0)
        _NormalMap ("Normal Map", 2D) = "bump" {}
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
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 tangent : TEXCOORD2;
                float3 binormal : TEXCOORD3;
            };
            
            float4 _LightDirection;
            sampler2D _NormalMap;
            float4 _NormalMap_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _NormalMap);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.tangent = normalize(mul(unity_ObjectToWorld, v.tangent)).xyz;
                o.binormal = cross(v.normal, v.tangent) * v.tangent.w;
                o.binormal = normalize(mul(unity_ObjectToWorld, o.binormal));
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normalMap = UnpackNormal(tex2D(_NormalMap, i.uv));
                float3 normal = (i.tangent * normalMap.x) + (i.binormal * normalMap.y) + (i.normal * normalMap.z);
                // const float3 lightDirection = normalize(_LightDirection.xyz);
                const float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float diffuse = max(0, dot(normal, lightDirection));
                
                return float4(diffuse.xxx, 1);
            }
            ENDCG
        }
    }
}
