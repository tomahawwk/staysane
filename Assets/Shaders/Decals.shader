Shader "Unlit/Decal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Decal("decal", 2D) = "white" {}
 
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
                float2 decalUV : TEXCOORD1;
    };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 decalUV : TEXCOORD1;
            };
 
            sampler2D _MainTex;
            sampler2D _Decal;
            float4 _Decal_ST;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.decalUV = TRANSFORM_TEX(v.uv, _Decal);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 dec = tex2D(_Decal, i.decalUV);
                col = lerp(col, dec, dec.a);
            return col;
            }
            ENDCG
        }
    }
}