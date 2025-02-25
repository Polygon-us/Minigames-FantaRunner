Shader "Unlit/RunBackground"
{
    Properties
    {
        _RunTexture ("Run", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Speed ("UV Speed", Vector) = (0, 0, 0, 0)
        _RunTiling ("Run tiling", Vector) = (1, 1, 0, 0)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            sampler2D _RunTexture;
            sampler2D _Mask;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float2 _RunTiling;
            float2 _Speed;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = v.uv;
                o.color = v.color;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 backgroundColor = tex2D(_MainTex, i.uv);
                backgroundColor.rgb *= i.color.rgb;

                fixed4 mask = tex2D(_Mask, i.uv);

                float2 offset = i.uv * _RunTiling.xy + _Time.y * _Speed.xy;

                fixed4 runColor = tex2D(_RunTexture, offset).a;

                fixed alpha = runColor.a * 2 * (1 - mask.a);

                float4 col = lerp(backgroundColor, 1, alpha);

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return col;
            }
            ENDCG
        }
    }
}