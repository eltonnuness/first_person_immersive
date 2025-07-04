Shader "PostEffect/Pixelation"
{
    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

        // for Pixelation
        float _WidthPixelation;
        float _HeightPixelation;
        
        // for color precision
        float _ColorPrecision;

        float4 Frag(Varyings input) : SV_Target
        {
            //UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

            // Pixelation
            float2 uv = input.texcoord;
            uv.x = floor(uv.x * _WidthPixelation) / _WidthPixelation;
            uv.y = floor(uv.y * _HeightPixelation) / _HeightPixelation;
            
            float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, uv);

            // Color precision
            color = floor(color * _ColorPrecision) / _ColorPrecision;
            return color;
        }
    ENDHLSL

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZWrite Off Cull Off

        Pass
        {
            Name "PixelationPass"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            ENDHLSL
        }
    }
}
