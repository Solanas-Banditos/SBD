Shader "Unlit/Colorer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity ("Intensity", Range(0,2)) = 1.0
        _Sc ("Surface color", Range(0,2)) = 0.5
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        //Blend Zero SrcColor
        //Blend Zero Zero

        GrabPass { }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            sampler2D _GrabTexture;
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
                //fixed2 uv = i.uv;
                //fixed3 col = 0.5 + 0.5*cos(_CosTime.w*3 + uv.xyx+fixed3(0,2,4));
                //col = 0;

                float2 grabTexcoord = i.screenPos.xy / i.screenPos.w; 
                grabTexcoord.x = (grabTexcoord.x + 1.0) * .5;
                grabTexcoord.y = (grabTexcoord.y + 1.0) * .5; 
                #if UNITY_UV_STARTS_AT_TOP
                grabTexcoord.y = 1.0 - grabTexcoord.y;
                #endif
                fixed4 grabColor = tex2D(_GrabTexture, grabTexcoord); 

                half4 color = _Sc - tex2D(_MainTex, i.texcoord);

                grabColor = grabColor - grabColor* color * (_Intensity);
                return grabColor;
            }
            ENDCG
        }
    }
}
