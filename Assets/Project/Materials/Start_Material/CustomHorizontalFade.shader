Shader "Custom/HorizontalFadeImage"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _FadeAmount ("Fade Amount", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
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
            float _FadeAmount;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float fade = smoothstep(0.0, 0.0, _FadeAmount - i.uv.x);

                half4 col = tex2D(_MainTex, i.uv);
                col.a *= fade;

                return col;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
