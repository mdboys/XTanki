using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000227 RID: 551
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class ScreenSpaceReflectionComponent : PostProcessingComponentCommandBuffer<ScreenSpaceReflectionModel>
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00081FA8 File Offset: 0x000801A8
		public override bool active
		{
			get
			{
				if (base.model.enabled)
				{
					PostProcessingContext context = this.context;
					return context != null && context.isGBufferAvailable && !context.interrupted;
				}
				return false;
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00005B7A File Offset: 0x00003D7A
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00081FE4 File Offset: 0x000801E4
		public override void OnEnable()
		{
			this.m_ReflectionTextures[0] = Shader.PropertyToID("_ReflectionTexture0");
			this.m_ReflectionTextures[1] = Shader.PropertyToID("_ReflectionTexture1");
			this.m_ReflectionTextures[2] = Shader.PropertyToID("_ReflectionTexture2");
			this.m_ReflectionTextures[3] = Shader.PropertyToID("_ReflectionTexture3");
			this.m_ReflectionTextures[4] = Shader.PropertyToID("_ReflectionTexture4");
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		public override string GetName()
		{
			return "Screen Space Reflection";
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00009DB6 File Offset: 0x00007FB6
		public override CameraEvent GetCameraEvent()
		{
			return CameraEvent.AfterFinalPass;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0008204C File Offset: 0x0008024C
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			ScreenSpaceReflectionModel.Settings settings = base.model.settings;
			Camera camera = this.context.camera;
			int num = ((settings.reflection.reflectionQuality == ScreenSpaceReflectionModel.SSRResolution.High) ? 1 : 2);
			int num2 = this.context.width / num;
			int num3 = this.context.height / num;
			float num4 = (float)this.context.width;
			float num5 = (float)this.context.height;
			float num6 = num4 / 2f;
			float num7 = num5 / 2f;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Screen Space Reflection");
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._RayStepSize, settings.reflection.stepSize);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._AdditiveReflection, (settings.reflection.blendType == ScreenSpaceReflectionModel.SSRReflectionBlendType.Additive) ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._BilateralUpsampling, (this.k_BilateralUpsample > false) ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._TreatBackfaceHitAsMiss, (this.k_TreatBackfaceHitAsMiss > false) ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._AllowBackwardsRays, (settings.reflection.reflectBackfaces > false) ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._TraceBehindObjects, (this.k_TraceBehindObjects > false) ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._MaxSteps, settings.reflection.iterationCount);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._FullResolutionFiltering, 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._HalfResolution, (settings.reflection.reflectionQuality > ScreenSpaceReflectionModel.SSRResolution.High) ? 1 : 0);
			material.SetInt(ScreenSpaceReflectionComponent.Uniforms._HighlightSuppression, (this.k_HighlightSuppression > false) ? 1 : 0);
			float num8 = num4 / (-2f * Mathf.Tan(camera.fieldOfView / 180f * 3.1415927f * 0.5f));
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._PixelsPerMeterAtOneMeter, num8);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._ScreenEdgeFading, settings.screenEdgeMask.intensity);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._ReflectionBlur, settings.reflection.reflectionBlur);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._MaxRayTraceDistance, settings.reflection.maxDistance);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._FadeDistance, settings.intensity.fadeDistance);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._LayerThickness, settings.reflection.widthModifier);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._SSRMultiplier, settings.intensity.reflectionMultiplier);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._FresnelFade, settings.intensity.fresnelFade);
			material.SetFloat(ScreenSpaceReflectionComponent.Uniforms._FresnelFadePower, settings.intensity.fresnelFadePower);
			Matrix4x4 projectionMatrix = camera.projectionMatrix;
			Vector4 vector = new Vector4(-2f / (num4 * projectionMatrix[0]), -2f / (num5 * projectionMatrix[5]), (1f - projectionMatrix[2]) / projectionMatrix[0], (1f + projectionMatrix[6]) / projectionMatrix[5]);
			Vector3 vector2 = ((!float.IsPositiveInfinity(camera.farClipPlane)) ? new Vector3(camera.nearClipPlane * camera.farClipPlane, camera.nearClipPlane - camera.farClipPlane, camera.farClipPlane) : new Vector3(camera.nearClipPlane, -1f, 1f));
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._ReflectionBufferSize, new Vector2((float)num2, (float)num3));
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._ScreenSize, new Vector2(num4, num5));
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._InvScreenSize, new Vector2(1f / num4, 1f / num5));
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._ProjInfo, vector);
			material.SetVector(ScreenSpaceReflectionComponent.Uniforms._CameraClipInfo, vector2);
			Matrix4x4 matrix4x = default(Matrix4x4);
			matrix4x.SetRow(0, new Vector4(num6, 0f, 0f, num6));
			matrix4x.SetRow(1, new Vector4(0f, num7, 0f, num7));
			matrix4x.SetRow(2, new Vector4(0f, 0f, 1f, 0f));
			matrix4x.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
			Matrix4x4 matrix4x2 = matrix4x * projectionMatrix;
			material.SetMatrix(ScreenSpaceReflectionComponent.Uniforms._ProjectToPixelMatrix, matrix4x2);
			material.SetMatrix(ScreenSpaceReflectionComponent.Uniforms._WorldToCameraMatrix, camera.worldToCameraMatrix);
			material.SetMatrix(ScreenSpaceReflectionComponent.Uniforms._CameraToWorldMatrix, camera.worldToCameraMatrix.inverse);
			RenderTextureFormat renderTextureFormat = (this.context.isHdr ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.ARGB32);
			int normalAndRoughnessTexture = ScreenSpaceReflectionComponent.Uniforms._NormalAndRoughnessTexture;
			int hitPointTexture = ScreenSpaceReflectionComponent.Uniforms._HitPointTexture;
			int blurTexture = ScreenSpaceReflectionComponent.Uniforms._BlurTexture;
			int filteredReflections = ScreenSpaceReflectionComponent.Uniforms._FilteredReflections;
			int finalReflectionTexture = ScreenSpaceReflectionComponent.Uniforms._FinalReflectionTexture;
			int tempTexture = ScreenSpaceReflectionComponent.Uniforms._TempTexture;
			cb.GetTemporaryRT(normalAndRoughnessTexture, -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.GetTemporaryRT(hitPointTexture, num2, num3, 0, FilterMode.Bilinear, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
			for (int i = 0; i < 5; i++)
			{
				cb.GetTemporaryRT(this.m_ReflectionTextures[i], num2 >> i, num3 >> i, 0, FilterMode.Bilinear, renderTextureFormat);
			}
			cb.GetTemporaryRT(filteredReflections, num2, num3, 0, (!this.k_BilateralUpsample) ? FilterMode.Bilinear : FilterMode.Point, renderTextureFormat);
			cb.GetTemporaryRT(finalReflectionTexture, num2, num3, 0, FilterMode.Point, renderTextureFormat);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, normalAndRoughnessTexture, material, 6);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, hitPointTexture, material, 0);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, filteredReflections, material, 5);
			cb.Blit(filteredReflections, this.m_ReflectionTextures[0], material, 8);
			for (int j = 1; j < 5; j++)
			{
				int num9 = this.m_ReflectionTextures[j - 1];
				cb.GetTemporaryRT(blurTexture, num2 >> j, num3 >> j, 0, FilterMode.Bilinear, renderTextureFormat);
				cb.SetGlobalVector(ScreenSpaceReflectionComponent.Uniforms._Axis, new Vector4(1f, 0f, 0f, 0f));
				cb.SetGlobalFloat(ScreenSpaceReflectionComponent.Uniforms._CurrentMipLevel, (float)j - 1f);
				cb.Blit(num9, blurTexture, material, 2);
				cb.SetGlobalVector(ScreenSpaceReflectionComponent.Uniforms._Axis, new Vector4(0f, 1f, 0f, 0f));
				num9 = this.m_ReflectionTextures[j];
				cb.Blit(blurTexture, num9, material, 2);
				cb.ReleaseTemporaryRT(blurTexture);
			}
			cb.Blit(this.m_ReflectionTextures[0], finalReflectionTexture, material, 3);
			cb.GetTemporaryRT(tempTexture, camera.pixelWidth, camera.pixelHeight, 0, FilterMode.Bilinear, renderTextureFormat);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, tempTexture, material, 1);
			cb.Blit(tempTexture, BuiltinRenderTextureType.CameraTarget);
			cb.ReleaseTemporaryRT(tempTexture);
		}

		// Token: 0x040007CD RID: 1997
		private readonly int[] m_ReflectionTextures = new int[5];

		// Token: 0x040007CE RID: 1998
		private readonly bool k_BilateralUpsample = true;

		// Token: 0x040007CF RID: 1999
		private bool k_HighlightSuppression;

		// Token: 0x040007D0 RID: 2000
		private readonly bool k_TraceBehindObjects = true;

		// Token: 0x040007D1 RID: 2001
		private bool k_TreatBackfaceHitAsMiss;

		// Token: 0x02000228 RID: 552
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x040007D2 RID: 2002
			internal static readonly int _RayStepSize = Shader.PropertyToID("_RayStepSize");

			// Token: 0x040007D3 RID: 2003
			internal static readonly int _AdditiveReflection = Shader.PropertyToID("_AdditiveReflection");

			// Token: 0x040007D4 RID: 2004
			internal static readonly int _BilateralUpsampling = Shader.PropertyToID("_BilateralUpsampling");

			// Token: 0x040007D5 RID: 2005
			internal static readonly int _TreatBackfaceHitAsMiss = Shader.PropertyToID("_TreatBackfaceHitAsMiss");

			// Token: 0x040007D6 RID: 2006
			internal static readonly int _AllowBackwardsRays = Shader.PropertyToID("_AllowBackwardsRays");

			// Token: 0x040007D7 RID: 2007
			internal static readonly int _TraceBehindObjects = Shader.PropertyToID("_TraceBehindObjects");

			// Token: 0x040007D8 RID: 2008
			internal static readonly int _MaxSteps = Shader.PropertyToID("_MaxSteps");

			// Token: 0x040007D9 RID: 2009
			internal static readonly int _FullResolutionFiltering = Shader.PropertyToID("_FullResolutionFiltering");

			// Token: 0x040007DA RID: 2010
			internal static readonly int _HalfResolution = Shader.PropertyToID("_HalfResolution");

			// Token: 0x040007DB RID: 2011
			internal static readonly int _HighlightSuppression = Shader.PropertyToID("_HighlightSuppression");

			// Token: 0x040007DC RID: 2012
			internal static readonly int _PixelsPerMeterAtOneMeter = Shader.PropertyToID("_PixelsPerMeterAtOneMeter");

			// Token: 0x040007DD RID: 2013
			internal static readonly int _ScreenEdgeFading = Shader.PropertyToID("_ScreenEdgeFading");

			// Token: 0x040007DE RID: 2014
			internal static readonly int _ReflectionBlur = Shader.PropertyToID("_ReflectionBlur");

			// Token: 0x040007DF RID: 2015
			internal static readonly int _MaxRayTraceDistance = Shader.PropertyToID("_MaxRayTraceDistance");

			// Token: 0x040007E0 RID: 2016
			internal static readonly int _FadeDistance = Shader.PropertyToID("_FadeDistance");

			// Token: 0x040007E1 RID: 2017
			internal static readonly int _LayerThickness = Shader.PropertyToID("_LayerThickness");

			// Token: 0x040007E2 RID: 2018
			internal static readonly int _SSRMultiplier = Shader.PropertyToID("_SSRMultiplier");

			// Token: 0x040007E3 RID: 2019
			internal static readonly int _FresnelFade = Shader.PropertyToID("_FresnelFade");

			// Token: 0x040007E4 RID: 2020
			internal static readonly int _FresnelFadePower = Shader.PropertyToID("_FresnelFadePower");

			// Token: 0x040007E5 RID: 2021
			internal static readonly int _ReflectionBufferSize = Shader.PropertyToID("_ReflectionBufferSize");

			// Token: 0x040007E6 RID: 2022
			internal static readonly int _ScreenSize = Shader.PropertyToID("_ScreenSize");

			// Token: 0x040007E7 RID: 2023
			internal static readonly int _InvScreenSize = Shader.PropertyToID("_InvScreenSize");

			// Token: 0x040007E8 RID: 2024
			internal static readonly int _ProjInfo = Shader.PropertyToID("_ProjInfo");

			// Token: 0x040007E9 RID: 2025
			internal static readonly int _CameraClipInfo = Shader.PropertyToID("_CameraClipInfo");

			// Token: 0x040007EA RID: 2026
			internal static readonly int _ProjectToPixelMatrix = Shader.PropertyToID("_ProjectToPixelMatrix");

			// Token: 0x040007EB RID: 2027
			internal static readonly int _WorldToCameraMatrix = Shader.PropertyToID("_WorldToCameraMatrix");

			// Token: 0x040007EC RID: 2028
			internal static readonly int _CameraToWorldMatrix = Shader.PropertyToID("_CameraToWorldMatrix");

			// Token: 0x040007ED RID: 2029
			internal static readonly int _Axis = Shader.PropertyToID("_Axis");

			// Token: 0x040007EE RID: 2030
			internal static readonly int _CurrentMipLevel = Shader.PropertyToID("_CurrentMipLevel");

			// Token: 0x040007EF RID: 2031
			internal static readonly int _NormalAndRoughnessTexture = Shader.PropertyToID("_NormalAndRoughnessTexture");

			// Token: 0x040007F0 RID: 2032
			internal static readonly int _HitPointTexture = Shader.PropertyToID("_HitPointTexture");

			// Token: 0x040007F1 RID: 2033
			internal static readonly int _BlurTexture = Shader.PropertyToID("_BlurTexture");

			// Token: 0x040007F2 RID: 2034
			internal static readonly int _FilteredReflections = Shader.PropertyToID("_FilteredReflections");

			// Token: 0x040007F3 RID: 2035
			internal static readonly int _FinalReflectionTexture = Shader.PropertyToID("_FinalReflectionTexture");

			// Token: 0x040007F4 RID: 2036
			internal static readonly int _TempTexture = Shader.PropertyToID("_TempTexture");
		}

		// Token: 0x02000229 RID: 553
		[NullableContext(0)]
		private enum PassIndex
		{
			// Token: 0x040007F6 RID: 2038
			RayTraceStep,
			// Token: 0x040007F7 RID: 2039
			CompositeFinal,
			// Token: 0x040007F8 RID: 2040
			Blur,
			// Token: 0x040007F9 RID: 2041
			CompositeSSR,
			// Token: 0x040007FA RID: 2042
			MinMipGeneration,
			// Token: 0x040007FB RID: 2043
			HitPointToReflections,
			// Token: 0x040007FC RID: 2044
			BilateralKeyPack,
			// Token: 0x040007FD RID: 2045
			BlitDepthAsCSZ,
			// Token: 0x040007FE RID: 2046
			PoissonBlur
		}
	}
}
