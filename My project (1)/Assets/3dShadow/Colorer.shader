Shader "Unlit/Colorer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RendTex ("Texture", 2D) = "white" {}
        _Intensity ("Intensity", Range(0,2)) = 1.0
        _Sc ("Surface color", Range(0,2)) = 0.5
    }
    SubShader
    {
        CULL OFF

        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            sampler2D _RendTex;
            float _Intensity;
            float _Sc;
            

            struct VertexInput
            {
                float4 vertex : POSITION;   
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };
            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                half2 texcoord : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput vertexInput)
            {
                VertexOutput vertexOutput;

                vertexOutput.vertex = UnityObjectToClipPos(vertexInput.vertex);
                vertexOutput.screenPos = vertexOutput.vertex;   
                vertexOutput.texcoord = vertexInput.texcoord;

                return vertexOutput;
            }

            fixed4 frag (VertexOutput i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.texcoord);
                half4 rendColor = tex2D(_RendTex, i.texcoord);

                //grabColor = grabColor - grabColor* color * (_Intensity);

                rendColor.a = 0;

                return rendColor;
            }
            ENDCG
        }
    }
}
