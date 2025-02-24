Shader "Unlit/RunBackground"
{
    Properties
    {
        _RunTexture ("Run", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Speed ("UV Speed", Vector) = (0, 0, 0, 0)
        _RunTiling ("Run tiling", Vector) = (1, 1, 0, 0)
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
            sampler2D _RunTexture;
            sampler2D _Mask;
            float4 _MainTex_ST;
            float2 _RunTiling;
            float2 _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                o.uv = v.uv;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 backgroundColor = tex2D(_MainTex, i.uv);
                
                fixed4 mask = tex2D(_Mask, i.uv);
                
                float2 offset = i.uv * _RunTiling.xy + _Time.y * _Speed.xy;
                
                fixed4 runColor = tex2D(_RunTexture, offset).a;
                
                fixed alpha = runColor.a * (1 - mask.a);
                
                float4 col = lerp(backgroundColor, 1, alpha);

                return col;
            }
            ENDCG
        }
    }
}
