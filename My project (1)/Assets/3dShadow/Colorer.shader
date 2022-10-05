Shader "Unlit/Colorer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        //Blend Zero SrcColor
        Blend Zero SrcColor

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            half4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                // Normalized pixel coordinates (from 0 to 1)
                fixed2 uv = i.uv;

                // Time varying pixel color
                fixed3 col = 0.5 + 0.5*cos(_CosTime.w*3 + uv.xyx+fixed3(0,2,4));

                // Output to screen
               // fragColor = vec4(col,1.0);


                col = tex2D(_MainTex, i.uv);
                //col = 0;
                return fixed4(col,0);
            }
            ENDCG
        }
    }
}
