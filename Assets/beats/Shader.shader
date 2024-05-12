Shader "Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Offset ("Offset", Float) = 0.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            CBUFFER_START(UnityPerMaterial)
            float _Offset;
            CBUFFER_END

            fixed4 frag (v2f i) : SV_Target
            {
                const float pi = 3.14159265359;
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 adjacent1 = tex2D(_MainTex, i.uv + float2(_Offset*sin(i.uv.y * pi * 16), 0));
                fixed4 adjacent2 = tex2D(_MainTex, i.uv + float2(-_Offset*sin(i.uv.x * pi * 16), 0));
                // just invert the colors
                col.r = adjacent1.b;
                col.g = adjacent2.r;
                // col.b = col.b;
                
                return col;
            }
            ENDCG
        }
    }
}
