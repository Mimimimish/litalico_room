Shader "Custom/CircleMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
        _Radius ("Radius", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags {"Queue"="Overlay"}
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float2 _Center;
            float _Radius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 aspectFix = float2(1.0, _ScreenParams.y / _ScreenParams.x); // 画面比補正
                float dist = distance((i.uv - _Center) * aspectFix, float2(0,0)); // 正円補正

                float alpha = step(_Radius, dist); // ぼかしなしで境界を明確にする
                return fixed4(0, 0, 0, alpha);
            }
            ENDCG
        }
    }
}
