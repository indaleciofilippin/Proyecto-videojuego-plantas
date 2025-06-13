Shader "Custom/GlowSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _GlowColor ("Glow Color", Color) = (1,1,0,1)
        _GlowStrength ("Glow Strength", Range(0, 3)) = 0
        _PulseSpeed ("Pulse Speed", Range(0, 10)) = 2
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _GlowColor;
            float _GlowStrength;
            float _PulseSpeed;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                
                // Solo aplicar glow si _GlowStrength > 0
                if (_GlowStrength > 0)
                {
                    // Efecto de pulso
                    float pulse = (sin(_Time.y * _PulseSpeed) + 1.0) * 0.5;
                    float glowIntensity = _GlowStrength * (0.4 + pulse * 0.3);
                    
                    // Mezclar color original con glow
                    fixed4 glowColor = lerp(texColor, _GlowColor, glowIntensity * 0.5);
                    glowColor.a = texColor.a;
                    
                    return glowColor;
                }
                
                return texColor;
            }
            ENDCG
        }
    }
}