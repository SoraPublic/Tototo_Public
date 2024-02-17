Shader "Custom/FlowerShaderWithColorChange"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.0
        _Cutoff("Cutoff",Range(0,1))=0.5
        [Toggle(IS_CLEARED)]_IsCleared("Is Cleared",Float) = 0
        [Toggle(IS_OPENED)]_IsOpened("Is Opened",Float) = 0
    }
    SubShader
    {
        Tags {
            "RenderType" = "Transparent"
            "Queue" = "AlphaTest"
        }
        LOD 200
        Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alphatest:_Cutoff

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

#pragma multi_compile _ IS_CLEARED IS_OPENED

        sampler2D _MainTex;
        half _Glossiness;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

#if IS_CLEARED
            o.Albedo = c.rgb;

#elif IS_OPENED

            float gray = dot(c.rgb, fixed3(0.299, 0.587, 0.114));
            o.Albedo = fixed4(gray, gray, gray, 1);
#else
            o.Albedo = fixed4(0, 0, 0, 1);
#endif
            // Metallic and smoothness come from slider variables
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Transprent/Cutout/Diffuse"
}
