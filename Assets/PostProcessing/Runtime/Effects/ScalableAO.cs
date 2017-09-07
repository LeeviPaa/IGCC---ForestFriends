﻿using System;

namespace UnityEngine.Rendering.PostProcessing
{
    // Scalable ambient obscurance
    [Serializable]
    public sealed class ScalableAO : IAmbientOcclusionMethod
    {
        // Unity sorts enums by value in the editor (and doesn't handle same-values enums very well
        // so we won't use enum values as sample counts this time
        public enum Quality
        {
            Lowest,
            Low,
            Medium,
            High,
            Ultra
        }

        [Range(0f, 4f), Tooltip("Degree of darkness produced by the effect.")]
        public float intensity = 0.5f;

        [Tooltip("Radius of sample points, which affects extent of darkened areas.")]
        public float radius = 0.25f;

        [Tooltip("Number of sample points, which affects quality and performance. Lowest, Low & Medium passes are downsampled. High and Ultra are not and should only be used on high-end hardware.")]
        public Quality quality = Quality.Medium;

        [ColorUsage(false), Tooltip("Custom color to use for the ambient occlusion.")]
        public Color color = Color.black;

        RenderTexture m_Result;
        PropertySheet m_PropertySheet;

        readonly RenderTargetIdentifier[] m_MRT =
        {
            BuiltinRenderTextureType.GBuffer0, // Albedo, Occ
            BuiltinRenderTextureType.CameraTarget // Ambient
        };

        readonly int[] m_SampleCount = { 4, 6, 10, 8, 12 };

        enum Pass
        {
            OcclusionEstimationForward,
            OcclusionEstimationDeferred,
            HorizontalBlurForward,
            HorizontalBlurDeferred,
            VerticalBlur,
            CompositionForward,
            CompositionDeferred
        }

        public DepthTextureMode GetCameraFlags()
        {
            return DepthTextureMode.Depth | DepthTextureMode.DepthNormals;
        }

        public bool IsSupported(PostProcessRenderContext context)
        {
            return intensity > 0f
                && !RuntimeUtilities.scriptableRenderPipelineActive;
        }

        void DoLazyInitialization(PostProcessRenderContext context)
        {
            m_PropertySheet = context.propertySheets.Get(context.resources.shaders.scalableAO);

            bool reset = false;

            if (m_Result == null || !m_Result.IsCreated())
            {
                // Initial allocation
                m_Result = new RenderTexture(context.width, context.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear)
                {
                    hideFlags = HideFlags.DontSave,
                    filterMode = FilterMode.Bilinear
                };
                reset = true;
            }
            else if (m_Result.width != context.width || m_Result.height != context.height)
            {
                // Release and reallocate
                m_Result.Release();
                m_Result.width = context.width;
                m_Result.height = context.height;
                reset = true;
            }

            if (reset)
                m_Result.Create();
        }

        void Render(PostProcessRenderContext context, CommandBuffer cmd, int occlusionSource)
        {
            DoLazyInitialization(context);
            radius = Mathf.Max(radius, 1e-4f);

            // Material setup
            // Always use a quater-res AO buffer unless High/Ultra quality is set.
            bool downsampling = (int)quality < (int)Quality.High;
            float px = intensity;
            float py = radius;
            float pz = downsampling ? 0.5f : 1f;
            float pw = m_SampleCount[(int)quality];

            var sheet = m_PropertySheet;
            sheet.ClearKeywords();
            sheet.properties.SetVector(ShaderIDs.AOParams, new Vector4(px, py, pz, pw));
            sheet.properties.SetVector(ShaderIDs.AOColor, Color.white - color);

            // In forward fog is applied at the object level in the grometry pass so we need to
            // apply it to AO as well or it'll drawn on top of the fog effect.
            // Not needed in Deferred.
            if (context.camera.actualRenderingPath == RenderingPath.Forward && RenderSettings.fog)
            {
                sheet.EnableKeyword("APPLY_FORWARD_FOG");
                sheet.properties.SetVector(
                    ShaderIDs.FogParams,
                    new Vector3(RenderSettings.fogDensity, RenderSettings.fogStartDistance, RenderSettings.fogEndDistance)
                );
            }

            // Texture setup
            int tw = context.width;
            int th = context.height;
            int ts = downsampling ? 2 : 1;
            const RenderTextureFormat kFormat = RenderTextureFormat.ARGB32;
            const RenderTextureReadWrite kRWMode = RenderTextureReadWrite.Linear;
            const FilterMode kFilter = FilterMode.Bilinear;

            // AO buffer
            var rtMask = ShaderIDs.OcclusionTexture1;
            cmd.GetTemporaryRT(rtMask, tw / ts, th / ts, 0, kFilter, kFormat, kRWMode);

            // AO estimation
            cmd.BlitFullscreenTriangle(BuiltinRenderTextureType.None, rtMask, sheet, (int)Pass.OcclusionEstimationForward + occlusionSource);

            // Blur buffer
            var rtBlur = ShaderIDs.OcclusionTexture2;
            cmd.GetTemporaryRT(rtBlur, tw, th, 0, kFilter, kFormat, kRWMode);

            // Separable blur (horizontal pass)
            cmd.BlitFullscreenTriangle(rtMask, rtBlur, sheet, (int)Pass.HorizontalBlurForward + occlusionSource);
            cmd.ReleaseTemporaryRT(rtMask);

            // Separable blur (vertical pass)
            cmd.BlitFullscreenTriangle(rtBlur, m_Result, sheet, (int)Pass.VerticalBlur);
            cmd.ReleaseTemporaryRT(rtBlur);
        }

        public void RenderAfterOpaque(PostProcessRenderContext context)
        {
            var cmd = context.command;
            cmd.BeginSample("Ambient Occlusion");
            Render(context, cmd, 0);
            cmd.SetGlobalTexture(ShaderIDs.SAOcclusionTexture, m_Result);
            cmd.BlitFullscreenTriangle(BuiltinRenderTextureType.None, BuiltinRenderTextureType.CameraTarget, m_PropertySheet, (int)Pass.CompositionForward);
            cmd.EndSample("Ambient Occlusion");
        }

        public void RenderAmbientOnly(PostProcessRenderContext context)
        {
            var cmd = context.command;
            cmd.BeginSample("Ambient Occlusion Render");
            Render(context, cmd, 1);
            cmd.EndSample("Ambient Occlusion Render");
        }

        public void CompositeAmbientOnly(PostProcessRenderContext context)
        {
            var cmd = context.command;
            cmd.BeginSample("Ambient Occlusion Composite");
            cmd.SetGlobalTexture(ShaderIDs.SAOcclusionTexture, m_Result);
            cmd.BlitFullscreenTriangle(BuiltinRenderTextureType.None, m_MRT, BuiltinRenderTextureType.CameraTarget, m_PropertySheet, (int)Pass.CompositionDeferred);
            cmd.EndSample("Ambient Occlusion Composite");
        }

        public void Release()
        {
            RuntimeUtilities.Destroy(m_Result);
            m_Result = null;
        }
    }
}

