using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x020001A1 RID: 417
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Edge Detection/Edge Detection")]
	public class EdgeDetection : PostEffectsBase
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0000A7C3 File Offset: 0x000089C3
		private new void Start()
		{
			this.oldMode = this.mode;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0000A7D1 File Offset: 0x000089D1
		private void OnEnable()
		{
			this.SetCameraFlag();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00078B88 File Offset: 0x00076D88
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			Vector2 vector = new Vector2(this.sensitivityDepth, this.sensitivityNormals);
			this.edgeDetectMaterial.SetVector("_Sensitivity", new Vector4(vector.x, vector.y, 1f, vector.y));
			this.edgeDetectMaterial.SetFloat("_BgFade", this.edgesOnly);
			this.edgeDetectMaterial.SetFloat("_SampleDistance", this.sampleDist);
			this.edgeDetectMaterial.SetVector("_BgColor", this.edgesOnlyBgColor);
			this.edgeDetectMaterial.SetFloat("_Exponent", this.edgeExp);
			this.edgeDetectMaterial.SetFloat("_Threshold", this.lumThreshold);
			Graphics.Blit(source, destination, this.edgeDetectMaterial, (int)this.mode);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00078C6C File Offset: 0x00076E6C
		public override bool CheckResources()
		{
			base.CheckSupport(true);
			this.edgeDetectMaterial = base.CheckShaderAndCreateMaterial(this.edgeDetectShader, this.edgeDetectMaterial);
			if (this.mode != this.oldMode)
			{
				this.SetCameraFlag();
			}
			this.oldMode = this.mode;
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00078CD0 File Offset: 0x00076ED0
		private void SetCameraFlag()
		{
			EdgeDetection.EdgeDetectMode edgeDetectMode = this.mode;
			bool flag = edgeDetectMode - EdgeDetection.EdgeDetectMode.SobelDepth <= 1;
			if (flag)
			{
				base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
				return;
			}
			edgeDetectMode = this.mode;
			flag = edgeDetectMode <= EdgeDetection.EdgeDetectMode.RobertsCrossDepthNormals;
			if (flag)
			{
				base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
			}
		}

		// Token: 0x0400054A RID: 1354
		public EdgeDetection.EdgeDetectMode mode = EdgeDetection.EdgeDetectMode.SobelDepthThin;

		// Token: 0x0400054B RID: 1355
		public float sensitivityDepth = 1f;

		// Token: 0x0400054C RID: 1356
		public float sensitivityNormals = 1f;

		// Token: 0x0400054D RID: 1357
		public float lumThreshold = 0.2f;

		// Token: 0x0400054E RID: 1358
		public float edgeExp = 1f;

		// Token: 0x0400054F RID: 1359
		public float sampleDist = 1f;

		// Token: 0x04000550 RID: 1360
		public float edgesOnly;

		// Token: 0x04000551 RID: 1361
		public Color edgesOnlyBgColor = Color.white;

		// Token: 0x04000552 RID: 1362
		public Shader edgeDetectShader;

		// Token: 0x04000553 RID: 1363
		private Material edgeDetectMaterial;

		// Token: 0x04000554 RID: 1364
		private EdgeDetection.EdgeDetectMode oldMode = EdgeDetection.EdgeDetectMode.SobelDepthThin;

		// Token: 0x020001A2 RID: 418
		[NullableContext(0)]
		public enum EdgeDetectMode
		{
			// Token: 0x04000556 RID: 1366
			TriangleDepthNormals,
			// Token: 0x04000557 RID: 1367
			RobertsCrossDepthNormals,
			// Token: 0x04000558 RID: 1368
			SobelDepth,
			// Token: 0x04000559 RID: 1369
			SobelDepthThin,
			// Token: 0x0400055A RID: 1370
			TriangleLuminance
		}
	}
}
