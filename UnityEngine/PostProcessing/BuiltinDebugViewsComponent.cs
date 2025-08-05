using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020001DF RID: 479
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public sealed class BuiltinDebugViewsComponent : PostProcessingComponentCommandBuffer<BuiltinDebugViewsModel>
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0000AF62 File Offset: 0x00009162
		public override bool active
		{
			get
			{
				return base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Depth) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Normals) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.MotionVectors);
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0007D73C File Offset: 0x0007B93C
		public override DepthTextureMode GetCameraFlags()
		{
			BuiltinDebugViewsModel.Mode mode = base.model.settings.mode;
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			switch (mode)
			{
			case BuiltinDebugViewsModel.Mode.Depth:
				depthTextureMode |= DepthTextureMode.Depth;
				break;
			case BuiltinDebugViewsModel.Mode.Normals:
				depthTextureMode |= DepthTextureMode.DepthNormals;
				break;
			case BuiltinDebugViewsModel.Mode.MotionVectors:
				depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
				break;
			}
			return depthTextureMode;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0000AF8E File Offset: 0x0000918E
		public override CameraEvent GetCameraEvent()
		{
			if (base.model.settings.mode == BuiltinDebugViewsModel.Mode.MotionVectors)
			{
				return CameraEvent.BeforeImageEffects;
			}
			return CameraEvent.BeforeImageEffectsOpaque;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public override string GetName()
		{
			return "Builtin Debug Views";
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0007D784 File Offset: 0x0007B984
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			ref BuiltinDebugViewsModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			material.shaderKeywords = null;
			if (this.context.isGBufferAvailable)
			{
				material.EnableKeyword("SOURCE_GBUFFER");
			}
			switch (settings.mode)
			{
			case BuiltinDebugViewsModel.Mode.Depth:
				this.DepthPass(cb);
				break;
			case BuiltinDebugViewsModel.Mode.Normals:
				this.DepthNormalsPass(cb);
				break;
			case BuiltinDebugViewsModel.Mode.MotionVectors:
				this.MotionVectorsPass(cb);
				break;
			}
			this.context.Interrupt();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0007D814 File Offset: 0x0007BA14
		private void DepthPass(CommandBuffer cb)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.DepthSettings depth = base.model.settings.depth;
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._DepthScale, 1f / depth.scale);
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, material, 0);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0007D870 File Offset: 0x0007BA70
		private void DepthNormalsPass(CommandBuffer cb)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, material, 1);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0007D8A4 File Offset: 0x0007BAA4
		private void MotionVectorsPass(CommandBuffer cb)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.MotionVectorsSettings motionVectors = base.model.settings.motionVectors;
			int num = BuiltinDebugViewsComponent.Uniforms._TempRT;
			cb.GetTemporaryRT(num, this.context.width, this.context.height, 0, FilterMode.Bilinear);
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.sourceOpacity);
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, num, material, 2);
			if (motionVectors.motionImageOpacity > 0f && motionVectors.motionImageAmplitude > 0f)
			{
				int tempRT = BuiltinDebugViewsComponent.Uniforms._TempRT2;
				cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Bilinear);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionImageOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionImageAmplitude);
				cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, num);
				cb.Blit(num, tempRT, material, 3);
				cb.ReleaseTemporaryRT(num);
				num = tempRT;
			}
			if (motionVectors.motionVectorsOpacity > 0f && motionVectors.motionVectorsAmplitude > 0f)
			{
				this.PrepareArrows();
				float num2 = 1f / (float)motionVectors.motionVectorsResolution;
				float num3 = num2 * (float)this.context.height / (float)this.context.width;
				cb.SetGlobalVector(BuiltinDebugViewsComponent.Uniforms._Scale, new Vector2(num3, num2));
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionVectorsOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionVectorsAmplitude);
				cb.DrawMesh(this.m_Arrows.mesh, Matrix4x4.identity, material, 0, 4);
			}
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, num);
			cb.Blit(num, BuiltinRenderTextureType.CameraTarget);
			cb.ReleaseTemporaryRT(num);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0007DA98 File Offset: 0x0007BC98
		private void PrepareArrows()
		{
			int motionVectorsResolution = base.model.settings.motionVectors.motionVectorsResolution;
			int num = motionVectorsResolution * Screen.width / Screen.height;
			if (this.m_Arrows == null)
			{
				this.m_Arrows = new BuiltinDebugViewsComponent.ArrowArray();
			}
			if (this.m_Arrows.columnCount != num || this.m_Arrows.rowCount != motionVectorsResolution)
			{
				this.m_Arrows.Release();
				this.m_Arrows.BuildMesh(num, motionVectorsResolution);
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0000AFAF File Offset: 0x000091AF
		public override void OnDisable()
		{
			BuiltinDebugViewsComponent.ArrowArray arrows = this.m_Arrows;
			if (arrows != null)
			{
				arrows.Release();
			}
			this.m_Arrows = null;
		}

		// Token: 0x04000688 RID: 1672
		private const string k_ShaderString = "Hidden/Post FX/Builtin Debug Views";

		// Token: 0x04000689 RID: 1673
		private BuiltinDebugViewsComponent.ArrowArray m_Arrows;

		// Token: 0x020001E0 RID: 480
		[NullableContext(0)]
		private static class Uniforms
		{
			// Token: 0x0400068A RID: 1674
			internal static readonly int _DepthScale = Shader.PropertyToID("_DepthScale");

			// Token: 0x0400068B RID: 1675
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");

			// Token: 0x0400068C RID: 1676
			internal static readonly int _Opacity = Shader.PropertyToID("_Opacity");

			// Token: 0x0400068D RID: 1677
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x0400068E RID: 1678
			internal static readonly int _TempRT2 = Shader.PropertyToID("_TempRT2");

			// Token: 0x0400068F RID: 1679
			internal static readonly int _Amplitude = Shader.PropertyToID("_Amplitude");

			// Token: 0x04000690 RID: 1680
			internal static readonly int _Scale = Shader.PropertyToID("_Scale");
		}

		// Token: 0x020001E1 RID: 481
		[NullableContext(0)]
		private enum Pass
		{
			// Token: 0x04000692 RID: 1682
			Depth,
			// Token: 0x04000693 RID: 1683
			Normals,
			// Token: 0x04000694 RID: 1684
			MovecOpacity,
			// Token: 0x04000695 RID: 1685
			MovecImaging,
			// Token: 0x04000696 RID: 1686
			MovecArrows
		}

		// Token: 0x020001E2 RID: 482
		[Nullable(0)]
		private class ArrowArray
		{
			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0000AFD1 File Offset: 0x000091D1
			// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0000AFD9 File Offset: 0x000091D9
			public Mesh mesh { get; private set; }

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0000AFE2 File Offset: 0x000091E2
			// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0000AFEA File Offset: 0x000091EA
			public int columnCount { get; private set; }

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0000AFF3 File Offset: 0x000091F3
			// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0000AFFB File Offset: 0x000091FB
			public int rowCount { get; private set; }

			// Token: 0x060008F7 RID: 2295 RVA: 0x0007DB88 File Offset: 0x0007BD88
			public void BuildMesh(int columns, int rows)
			{
				Vector3[] array = new Vector3[]
				{
					new Vector3(0f, 0f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(-1f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(1f, 1f, 0f)
				};
				int num = 6 * columns * rows;
				List<Vector3> list = new List<Vector3>(num);
				List<Vector2> list2 = new List<Vector2>(num);
				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < columns; j++)
					{
						Vector2 vector = new Vector2((0.5f + (float)j) / (float)columns, (0.5f + (float)i) / (float)rows);
						for (int k = 0; k < 6; k++)
						{
							list.Add(array[k]);
							list2.Add(vector);
						}
					}
				}
				int[] array2 = new int[num];
				for (int l = 0; l < num; l++)
				{
					array2[l] = l;
				}
				this.mesh = new Mesh
				{
					hideFlags = HideFlags.DontSave
				};
				this.mesh.SetVertices(list);
				this.mesh.SetUVs(0, list2);
				this.mesh.SetIndices(array2, MeshTopology.Lines, 0);
				this.mesh.UploadMeshData(true);
				this.columnCount = columns;
				this.rowCount = rows;
			}

			// Token: 0x060008F8 RID: 2296 RVA: 0x0000B004 File Offset: 0x00009204
			public void Release()
			{
				GraphicsUtils.Destroy(this.mesh);
				this.mesh = null;
			}
		}
	}
}
