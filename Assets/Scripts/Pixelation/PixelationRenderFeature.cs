using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

namespace PSX
{
    public class PixelationRenderFeature : ScriptableRendererFeature
    {
        PixelationPass pixelationPass;

        public override void Create()
        {
            pixelationPass = new PixelationPass(RenderPassEvent.BeforeRenderingPostProcessing);
        }

        //ScripstableRendererFeature is an abstract class, you need this method
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(pixelationPass);
        }
    }
    
    
    public class PixelationPass : ScriptableRenderPass
    {
        class PassData
        {
            internal TextureHandle textureToRead;
        }

        private static readonly string shaderPath = "PostEffect/Pixelation";
        
        //PROPERTIES
        static readonly int WidthPixelation = Shader.PropertyToID("_WidthPixelation");
        static readonly int HeightPixelation = Shader.PropertyToID("_HeightPixelation");
        static readonly int ColorPrecison = Shader.PropertyToID("_ColorPrecision");

        Pixelation pixelation;
        Material pixelationMaterial;

        public PixelationPass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
            var shader = Shader.Find(shaderPath);
            if (shader == null)
            {
                Debug.LogError("Shader not found.");
                return;
            }
            this.pixelationMaterial = new Material(shader);
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (this.pixelationMaterial == null)
            {
                Debug.LogError("Material not created.");
                return;
            }

            UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
            if (!cameraData.postProcessEnabled) return;

            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
            TextureHandle srcCamColor = resourceData.cameraColor;
            //Debug.Log($"descriptorName: {srcCamColor.GetDescriptor(renderGraph).name}");

            var stack = VolumeManager.instance.stack;

            this.pixelation = stack.GetComponent<Pixelation>();
            if (this.pixelation == null) { return; }
            if (!this.pixelation.IsActive()) { return; }

            // Render (start)
            //Debug.Log("Start render");
            var source = srcCamColor;

            //getting camera width and height 
            var w = cameraData.camera.scaledPixelWidth;
            var h = cameraData.camera.scaledPixelHeight;

            //setting parameters here 
            cameraData.camera.depthTextureMode = cameraData.camera.depthTextureMode | DepthTextureMode.Depth;
            this.pixelationMaterial.SetFloat(WidthPixelation, this.pixelation.widthPixelation.value);
            this.pixelationMaterial.SetFloat(HeightPixelation, this.pixelation.heightPixelation.value);
            this.pixelationMaterial.SetFloat(ColorPrecison, this.pixelation.colorPrecision.value);

            TextureDesc pixelationTextureDescriptor = srcCamColor.GetDescriptor(renderGraph);
            pixelationTextureDescriptor.name = "_TempPixelation";
            pixelationTextureDescriptor.depthBufferBits = 0;
            pixelationTextureDescriptor.msaaSamples = MSAASamples.None;
            pixelationTextureDescriptor.filterMode = FilterMode.Point;
            var dst = renderGraph.CreateTexture(pixelationTextureDescriptor);

            if (!srcCamColor.IsValid() || !dst.IsValid())
            {
                Debug.Log($"srcCamColor: {srcCamColor.IsValid()} | dst: {dst.IsValid()}");
                return;
            }

            RenderGraphUtils.BlitMaterialParameters simplePass = new(source, dst, Blitter.GetBlitMaterial(TextureDimension.Tex2D), 0);
            renderGraph.AddBlitPass(simplePass, "Source -> TempTexture");

            RenderGraphUtils.BlitMaterialParameters pixelationPass = new(dst, source, pixelationMaterial, 0);
            renderGraph.AddBlitPass(pixelationPass, "TempTexture -> Pixelation -> Source");

        }

    }
}