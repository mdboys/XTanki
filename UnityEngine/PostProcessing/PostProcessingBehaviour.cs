using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200021E RID: 542
	[NullableContext(1)]
	[Nullable(0)]
	[ImageEffectAllowedInSceneView]
	[RequireComponent(typeof(Camera))]
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	[AddComponentMenu("Effects/Post-Processing Behaviour", -1)]
	public class PostProcessingBehaviour : MonoBehaviour
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x00081274 File Offset: 0x0007F474
		private void OnEnable()
		{
			this.m_CommandBuffers = new Dictionary<Type, KeyValuePair<CameraEvent, CommandBuffer>>();
			this.m_MaterialFactory = new MaterialFactory();
			this.m_RenderTextureFactory = new RenderTextureFactory();
			this.m_Context = new PostProcessingContext();
			this.m_Components = new List<PostProcessingComponentBase>();
			this.m_DebugViews = this.AddComponent<BuiltinDebugViewsComponent>(new BuiltinDebugViewsComponent());
			this.m_AmbientOcclusion = this.AddComponent<AmbientOcclusionComponent>(new AmbientOcclusionComponent());
			this.m_ScreenSpaceReflection = this.AddComponent<ScreenSpaceReflectionComponent>(new ScreenSpaceReflectionComponent());
			this.m_FogComponent = this.AddComponent<FogComponent>(new FogComponent());
			this.m_MotionBlur = this.AddComponent<MotionBlurComponent>(new MotionBlurComponent());
			this.m_Taa = this.AddComponent<TaaComponent>(new TaaComponent());
			this.m_EyeAdaptation = this.AddComponent<EyeAdaptationComponent>(new EyeAdaptationComponent());
			this.m_DepthOfField = this.AddComponent<DepthOfFieldComponent>(new DepthOfFieldComponent());
			this.m_Bloom = this.AddComponent<BloomComponent>(new BloomComponent());
			this.m_ChromaticAberration = this.AddComponent<ChromaticAberrationComponent>(new ChromaticAberrationComponent());
			this.m_ColorGrading = this.AddComponent<ColorGradingComponent>(new ColorGradingComponent());
			this.m_UserLut = this.AddComponent<UserLutComponent>(new UserLutComponent());
			this.m_Grain = this.AddComponent<GrainComponent>(new GrainComponent());
			this.m_Vignette = this.AddComponent<VignetteComponent>(new VignetteComponent());
			this.m_Dithering = this.AddComponent<DitheringComponent>(new DitheringComponent());
			this.m_Fxaa = this.AddComponent<FxaaComponent>(new FxaaComponent());
			this.m_ComponentStates = new Dictionary<PostProcessingComponentBase, bool>();
			foreach (PostProcessingComponentBase postProcessingComponentBase in this.m_Components)
			{
				this.m_ComponentStates.Add(postProcessingComponentBase, false);
			}
			base.useGUILayout = false;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00081428 File Offset: 0x0007F628
		private void OnDisable()
		{
			foreach (KeyValuePair<CameraEvent, CommandBuffer> keyValuePair in this.m_CommandBuffers.Values)
			{
				this.m_Camera.RemoveCommandBuffer(keyValuePair.Key, keyValuePair.Value);
				keyValuePair.Value.Dispose();
			}
			this.m_CommandBuffers.Clear();
			if (this.profile != null)
			{
				this.DisableComponents();
			}
			this.m_Components.Clear();
			if (this.m_Camera != null)
			{
				this.m_Camera.depthTextureMode = DepthTextureMode.None;
			}
			this.m_MaterialFactory.Dispose();
			this.m_RenderTextureFactory.Dispose();
			GraphicsUtils.Dispose();
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00081500 File Offset: 0x0007F700
		private void OnGUI()
		{
			if (Event.current.type == EventType.Repaint && !(this.profile == null) && !(this.m_Camera == null))
			{
				if (this.m_EyeAdaptation.active && this.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.EyeAdaptation))
				{
					this.m_EyeAdaptation.OnGUI();
					return;
				}
				if (this.m_ColorGrading.active && this.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.LogLut))
				{
					this.m_ColorGrading.OnGUI();
					return;
				}
				if (this.m_UserLut.active && this.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.UserLut))
				{
					this.m_UserLut.OnGUI();
				}
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000815C4 File Offset: 0x0007F7C4
		private void OnPostRender()
		{
			if (!(this.profile == null) && !(this.m_Camera == null) && !this.m_RenderingInSceneView && this.m_Taa.active && !this.profile.debugViews.willInterrupt)
			{
				this.m_Context.camera.ResetProjectionMatrix();
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00081624 File Offset: 0x0007F824
		private void OnPreCull()
		{
			this.m_Camera = base.GetComponent<Camera>();
			if (this.profile == null || this.m_Camera == null)
			{
				return;
			}
			PostProcessingContext postProcessingContext = this.m_Context.Reset();
			postProcessingContext.profile = this.profile;
			postProcessingContext.renderTextureFactory = this.m_RenderTextureFactory;
			postProcessingContext.materialFactory = this.m_MaterialFactory;
			postProcessingContext.camera = this.m_Camera;
			this.m_DebugViews.Init(postProcessingContext, this.profile.debugViews);
			this.m_AmbientOcclusion.Init(postProcessingContext, this.profile.ambientOcclusion);
			this.m_ScreenSpaceReflection.Init(postProcessingContext, this.profile.screenSpaceReflection);
			this.m_FogComponent.Init(postProcessingContext, this.profile.fog);
			this.m_MotionBlur.Init(postProcessingContext, this.profile.motionBlur);
			this.m_Taa.Init(postProcessingContext, this.profile.antialiasing);
			this.m_EyeAdaptation.Init(postProcessingContext, this.profile.eyeAdaptation);
			this.m_DepthOfField.Init(postProcessingContext, this.profile.depthOfField);
			this.m_Bloom.Init(postProcessingContext, this.profile.bloom);
			this.m_ChromaticAberration.Init(postProcessingContext, this.profile.chromaticAberration);
			this.m_ColorGrading.Init(postProcessingContext, this.profile.colorGrading);
			this.m_UserLut.Init(postProcessingContext, this.profile.userLut);
			this.m_Grain.Init(postProcessingContext, this.profile.grain);
			this.m_Vignette.Init(postProcessingContext, this.profile.vignette);
			this.m_Dithering.Init(postProcessingContext, this.profile.dithering);
			this.m_Fxaa.Init(postProcessingContext, this.profile.antialiasing);
			if (this.m_PreviousProfile != this.profile)
			{
				this.DisableComponents();
				this.m_PreviousProfile = this.profile;
			}
			this.CheckObservers();
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			foreach (PostProcessingComponentBase postProcessingComponentBase in this.m_Components)
			{
				if (postProcessingComponentBase.active)
				{
					depthTextureMode |= postProcessingComponentBase.GetCameraFlags();
				}
			}
			postProcessingContext.camera.depthTextureMode = depthTextureMode;
			if (!this.m_RenderingInSceneView && this.m_Taa.active && !this.profile.debugViews.willInterrupt)
			{
				this.m_Taa.SetProjectionMatrix(this.jitteredMatrixFunc);
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000818C8 File Offset: 0x0007FAC8
		private void OnPreRender()
		{
			if (!(this.profile == null))
			{
				this.TryExecuteCommandBuffer<BuiltinDebugViewsModel>(this.m_DebugViews);
				this.TryExecuteCommandBuffer<AmbientOcclusionModel>(this.m_AmbientOcclusion);
				this.TryExecuteCommandBuffer<ScreenSpaceReflectionModel>(this.m_ScreenSpaceReflection);
				this.TryExecuteCommandBuffer<FogModel>(this.m_FogComponent);
				if (!this.m_RenderingInSceneView)
				{
					this.TryExecuteCommandBuffer<MotionBlurModel>(this.m_MotionBlur);
				}
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00081928 File Offset: 0x0007FB28
		[ImageEffectTransformsToLDR]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.profile == null || this.m_Camera == null)
			{
				Graphics.Blit(source, destination);
				return;
			}
			bool flag = false;
			bool active = this.m_Fxaa.active;
			bool flag2 = this.m_Taa.active && !this.m_RenderingInSceneView;
			bool flag3 = this.m_DepthOfField.active && !this.m_RenderingInSceneView;
			Material material = this.m_MaterialFactory.Get("Hidden/Post FX/Uber Shader");
			material.shaderKeywords = null;
			RenderTexture renderTexture = source;
			if (flag2)
			{
				RenderTexture renderTexture2 = this.m_RenderTextureFactory.Get(renderTexture);
				this.m_Taa.Render(renderTexture, renderTexture2);
				renderTexture = renderTexture2;
			}
			Texture texture = GraphicsUtils.whiteTexture;
			if (this.m_EyeAdaptation.active)
			{
				flag = true;
				texture = this.m_EyeAdaptation.Prepare(renderTexture, material);
			}
			material.SetTexture("_AutoExposure", texture);
			if (flag3)
			{
				flag = true;
				this.m_DepthOfField.Prepare(renderTexture, material, flag2, this.m_Taa.jitterVector, this.m_Taa.model.settings.taaSettings.motionBlending);
			}
			if (this.m_Bloom.active)
			{
				flag = true;
				this.m_Bloom.Prepare(renderTexture, material, texture);
			}
			flag |= this.TryPrepareUberImageEffect<ChromaticAberrationModel>(this.m_ChromaticAberration, material);
			flag |= this.TryPrepareUberImageEffect<ColorGradingModel>(this.m_ColorGrading, material);
			flag |= this.TryPrepareUberImageEffect<VignetteModel>(this.m_Vignette, material);
			flag |= this.TryPrepareUberImageEffect<UserLutModel>(this.m_UserLut, material);
			Material material2 = ((!active) ? null : this.m_MaterialFactory.Get("Hidden/Post FX/FXAA"));
			if (active)
			{
				material2.shaderKeywords = null;
				this.TryPrepareUberImageEffect<GrainModel>(this.m_Grain, material2);
				this.TryPrepareUberImageEffect<DitheringModel>(this.m_Dithering, material2);
				if (flag)
				{
					RenderTexture renderTexture3 = this.m_RenderTextureFactory.Get(renderTexture);
					Graphics.Blit(renderTexture, renderTexture3, material, 0);
					renderTexture = renderTexture3;
				}
				this.m_Fxaa.Render(renderTexture, destination);
			}
			else
			{
				flag |= this.TryPrepareUberImageEffect<GrainModel>(this.m_Grain, material);
				flag |= this.TryPrepareUberImageEffect<DitheringModel>(this.m_Dithering, material);
				if (flag)
				{
					if (!GraphicsUtils.isLinearColorSpace)
					{
						material.EnableKeyword("UNITY_COLORSPACE_GAMMA");
					}
					Graphics.Blit(renderTexture, destination, material, 0);
				}
			}
			if (!flag && !active)
			{
				Graphics.Blit(renderTexture, destination);
			}
			this.m_RenderTextureFactory.ReleaseAll();
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0000B73B File Offset: 0x0000993B
		public void ResetTemporalEffects()
		{
			this.m_Taa.ResetHistory();
			this.m_MotionBlur.ResetHistory();
			this.m_EyeAdaptation.ResetHistory();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00081B70 File Offset: 0x0007FD70
		private void CheckObservers()
		{
			foreach (KeyValuePair<PostProcessingComponentBase, bool> keyValuePair in this.m_ComponentStates)
			{
				PostProcessingComponentBase key = keyValuePair.Key;
				bool enabled = key.GetModel().enabled;
				if (enabled != keyValuePair.Value)
				{
					if (enabled)
					{
						this.m_ComponentsToEnable.Add(key);
					}
					else
					{
						this.m_ComponentsToDisable.Add(key);
					}
				}
			}
			for (int i = 0; i < this.m_ComponentsToDisable.Count; i++)
			{
				PostProcessingComponentBase postProcessingComponentBase = this.m_ComponentsToDisable[i];
				this.m_ComponentStates[postProcessingComponentBase] = false;
				postProcessingComponentBase.OnDisable();
			}
			for (int j = 0; j < this.m_ComponentsToEnable.Count; j++)
			{
				PostProcessingComponentBase postProcessingComponentBase2 = this.m_ComponentsToEnable[j];
				this.m_ComponentStates[postProcessingComponentBase2] = true;
				postProcessingComponentBase2.OnEnable();
			}
			this.m_ComponentsToDisable.Clear();
			this.m_ComponentsToEnable.Clear();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00081C8C File Offset: 0x0007FE8C
		private void DisableComponents()
		{
			foreach (PostProcessingComponentBase postProcessingComponentBase in this.m_Components)
			{
				PostProcessingModel model = postProcessingComponentBase.GetModel();
				if (model != null && model.enabled)
				{
					postProcessingComponentBase.OnDisable();
				}
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00081CF0 File Offset: 0x0007FEF0
		private CommandBuffer AddCommandBuffer<[Nullable(0)] T>(CameraEvent evt, string name) where T : PostProcessingModel
		{
			KeyValuePair<CameraEvent, CommandBuffer> keyValuePair = new KeyValuePair<CameraEvent, CommandBuffer>(evt, new CommandBuffer
			{
				name = name
			});
			this.m_CommandBuffers.Add(typeof(T), keyValuePair);
			this.m_Camera.AddCommandBuffer(evt, keyValuePair.Value);
			return keyValuePair.Value;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00081D44 File Offset: 0x0007FF44
		[NullableContext(0)]
		private void RemoveCommandBuffer<T>() where T : PostProcessingModel
		{
			Type typeFromHandle = typeof(T);
			KeyValuePair<CameraEvent, CommandBuffer> keyValuePair;
			if (this.m_CommandBuffers.TryGetValue(typeFromHandle, out keyValuePair))
			{
				this.m_Camera.RemoveCommandBuffer(keyValuePair.Key, keyValuePair.Value);
				this.m_CommandBuffers.Remove(typeFromHandle);
				keyValuePair.Value.Dispose();
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00081DA0 File Offset: 0x0007FFA0
		private CommandBuffer GetCommandBuffer<[Nullable(0)] T>(CameraEvent evt, string name) where T : PostProcessingModel
		{
			KeyValuePair<CameraEvent, CommandBuffer> keyValuePair;
			if (!this.m_CommandBuffers.TryGetValue(typeof(T), out keyValuePair))
			{
				return this.AddCommandBuffer<T>(evt, name);
			}
			if (keyValuePair.Key != evt)
			{
				this.RemoveCommandBuffer<T>();
				return this.AddCommandBuffer<T>(evt, name);
			}
			return keyValuePair.Value;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00081DF0 File Offset: 0x0007FFF0
		private void TryExecuteCommandBuffer<[Nullable(0)] T>(PostProcessingComponentCommandBuffer<T> component) where T : PostProcessingModel
		{
			if (component.active)
			{
				CommandBuffer commandBuffer = this.GetCommandBuffer<T>(component.GetCameraEvent(), component.GetName());
				commandBuffer.Clear();
				component.PopulateCommandBuffer(commandBuffer);
				return;
			}
			this.RemoveCommandBuffer<T>();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000B75E File Offset: 0x0000995E
		private bool TryPrepareUberImageEffect<[Nullable(0)] T>(PostProcessingComponentRenderTexture<T> component, Material material) where T : PostProcessingModel
		{
			if (!component.active)
			{
				return false;
			}
			component.Prepare(material);
			return true;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000B772 File Offset: 0x00009972
		private T AddComponent<[Nullable(0)] T>(T component) where T : PostProcessingComponentBase
		{
			this.m_Components.Add(component);
			return component;
		}

		// Token: 0x04000798 RID: 1944
		public PostProcessingProfile profile;

		// Token: 0x04000799 RID: 1945
		public Func<Vector2, Matrix4x4> jitteredMatrixFunc;

		// Token: 0x0400079A RID: 1946
		private AmbientOcclusionComponent m_AmbientOcclusion;

		// Token: 0x0400079B RID: 1947
		private BloomComponent m_Bloom;

		// Token: 0x0400079C RID: 1948
		private Camera m_Camera;

		// Token: 0x0400079D RID: 1949
		private ChromaticAberrationComponent m_ChromaticAberration;

		// Token: 0x0400079E RID: 1950
		private ColorGradingComponent m_ColorGrading;

		// Token: 0x0400079F RID: 1951
		[Nullable(new byte[] { 1, 1, 0, 1 })]
		private Dictionary<Type, KeyValuePair<CameraEvent, CommandBuffer>> m_CommandBuffers;

		// Token: 0x040007A0 RID: 1952
		private List<PostProcessingComponentBase> m_Components;

		// Token: 0x040007A1 RID: 1953
		private Dictionary<PostProcessingComponentBase, bool> m_ComponentStates;

		// Token: 0x040007A2 RID: 1954
		private readonly List<PostProcessingComponentBase> m_ComponentsToDisable = new List<PostProcessingComponentBase>();

		// Token: 0x040007A3 RID: 1955
		private readonly List<PostProcessingComponentBase> m_ComponentsToEnable = new List<PostProcessingComponentBase>();

		// Token: 0x040007A4 RID: 1956
		private PostProcessingContext m_Context;

		// Token: 0x040007A5 RID: 1957
		private BuiltinDebugViewsComponent m_DebugViews;

		// Token: 0x040007A6 RID: 1958
		private DepthOfFieldComponent m_DepthOfField;

		// Token: 0x040007A7 RID: 1959
		private DitheringComponent m_Dithering;

		// Token: 0x040007A8 RID: 1960
		private EyeAdaptationComponent m_EyeAdaptation;

		// Token: 0x040007A9 RID: 1961
		private FogComponent m_FogComponent;

		// Token: 0x040007AA RID: 1962
		private FxaaComponent m_Fxaa;

		// Token: 0x040007AB RID: 1963
		private GrainComponent m_Grain;

		// Token: 0x040007AC RID: 1964
		private MaterialFactory m_MaterialFactory;

		// Token: 0x040007AD RID: 1965
		private MotionBlurComponent m_MotionBlur;

		// Token: 0x040007AE RID: 1966
		private PostProcessingProfile m_PreviousProfile;

		// Token: 0x040007AF RID: 1967
		private bool m_RenderingInSceneView;

		// Token: 0x040007B0 RID: 1968
		private RenderTextureFactory m_RenderTextureFactory;

		// Token: 0x040007B1 RID: 1969
		private ScreenSpaceReflectionComponent m_ScreenSpaceReflection;

		// Token: 0x040007B2 RID: 1970
		private TaaComponent m_Taa;

		// Token: 0x040007B3 RID: 1971
		private UserLutComponent m_UserLut;

		// Token: 0x040007B4 RID: 1972
		private VignetteComponent m_Vignette;
	}
}
