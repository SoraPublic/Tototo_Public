Shader "Custom/Vertex Alpha"
{
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

        CGPROGRAM

        #pragma surface surf Lambert alpha:fade

        struct Input
        {
            float4 color: COLOR;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = IN.color.rgb;
            o.Alpha = IN.color.w;
        }
        ENDCG

    }
        FallBack off
}