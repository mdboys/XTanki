using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000190 RID: 400
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Camera/Camera Motion Blur")]
	public class CameraMotionBlur : PostEffectsBase
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x000751BC File Offset: 0x000733BC
		private new void Start()
		{
			this.CheckResources();
			if (this._camera == null)
			{
				this._camera = base.GetComponent<Camera>();
			}
			this.wasActive = base.gameObject.activeInHierarchy;
			this.CalculateViewProjection();
			this.Remember();
			this.wasActive = false;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0000A4A9 File Offset: 0x000086A9
		private void OnEnable()
		{
			if (this._camera == null)
			{
				this._camera = base.GetComponent<Camera>();
			}
			this._camera.depthTextureMode |= DepthTextureMode.Depth;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00075210 File Offset: 0x00073410
		private void OnDisable()
		{
			if (null != this.motionBlurMaterial)
			{
				global::UnityEngine.Object.DestroyImmediate(this.motionBlurMaterial);
				this.motionBlurMaterial = null;
			}
			if (null != this.dx11MotionBlurMaterial)
			{
				global::UnityEngine.Object.DestroyImmediate(this.dx11MotionBlurMaterial);
				this.dx11MotionBlurMaterial = null;
			}
			if (null != this.tmpCam)
			{
				global::UnityEngine.Object.DestroyImmediate(this.tmpCam);
				this.tmpCam = null;
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00075280 File Offset: 0x00073480
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
			{
				this.StartFrame();
			}
			RenderTextureFormat renderTextureFormat = ((!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGHalf)) ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.RGHalf);
			RenderTexture temporary = RenderTexture.GetTemporary(CameraMotionBlur.divRoundUp(source.width, this.velocityDownsample), CameraMotionBlur.divRoundUp(source.height, this.velocityDownsample), 0, renderTextureFormat);
			this.maxVelocity = Mathf.Max(2f, this.maxVelocity);
			float num = this.maxVelocity;
			bool flag = this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11 && this.dx11MotionBlurMaterial == null;
			int num2;
			int num3;
			if (this.filterType == CameraMotionBlur.MotionBlurFilter.Reconstruction || flag || this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDisc)
			{
				this.maxVelocity = Mathf.Min(this.maxVelocity, CameraMotionBlur.MAX_RADIUS);
				num2 = CameraMotionBlur.divRoundUp(temporary.width, (int)this.maxVelocity);
				num3 = CameraMotionBlur.divRoundUp(temporary.height, (int)this.maxVelocity);
				num = (float)(temporary.width / num2);
			}
			else
			{
				num2 = CameraMotionBlur.divRoundUp(temporary.width, (int)this.maxVelocity);
				num3 = CameraMotionBlur.divRoundUp(temporary.height, (int)this.maxVelocity);
				num = (float)(temporary.width / num2);
			}
			RenderTexture temporary2 = RenderTexture.GetTemporary(num2, num3, 0, renderTextureFormat);
			RenderTexture temporary3 = RenderTexture.GetTemporary(num2, num3, 0, renderTextureFormat);
			temporary.filterMode = FilterMode.Point;
			temporary2.filterMode = FilterMode.Point;
			temporary3.filterMode = FilterMode.Point;
			if (this.noiseTexture)
			{
				this.noiseTexture.filterMode = FilterMode.Point;
			}
			source.wrapMode = TextureWrapMode.Clamp;
			temporary.wrapMode = TextureWrapMode.Clamp;
			temporary3.wrapMode = TextureWrapMode.Clamp;
			temporary2.wrapMode = TextureWrapMode.Clamp;
			this.CalculateViewProjection();
			if (base.gameObject.activeInHierarchy && !this.wasActive)
			{
				this.Remember();
			}
			this.wasActive = base.gameObject.activeInHierarchy;
			Matrix4x4 matrix4x = Matrix4x4.Inverse(this.currentViewProjMat);
			this.motionBlurMaterial.SetMatrix("_InvViewProj", matrix4x);
			this.motionBlurMaterial.SetMatrix("_PrevViewProj", this.prevViewProjMat);
			this.motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", this.prevViewProjMat * matrix4x);
			this.motionBlurMaterial.SetFloat("_MaxVelocity", num);
			this.motionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num);
			this.motionBlurMaterial.SetFloat("_MinVelocity", this.minVelocity);
			this.motionBlurMaterial.SetFloat("_VelocityScale", this.velocityScale);
			this.motionBlurMaterial.SetFloat("_Jitter", this.jitter);
			this.motionBlurMaterial.SetTexture("_NoiseTex", this.noiseTexture);
			this.motionBlurMaterial.SetTexture("_VelTex", temporary);
			this.motionBlurMaterial.SetTexture("_NeighbourMaxTex", temporary3);
			this.motionBlurMaterial.SetTexture("_TileTexDebug", temporary2);
			if (this.preview)
			{
				Matrix4x4 worldToCameraMatrix = this._camera.worldToCameraMatrix;
				Matrix4x4 identity = Matrix4x4.identity;
				identity.SetTRS(this.previewScale * 0.3333f, Quaternion.identity, Vector3.one);
				Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this._camera.projectionMatrix, true);
				this.prevViewProjMat = gpuprojectionMatrix * identity * worldToCameraMatrix;
				this.motionBlurMaterial.SetMatrix("_PrevViewProj", this.prevViewProjMat);
				this.motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", this.prevViewProjMat * matrix4x);
			}
			if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
			{
				Vector4 zero = Vector4.zero;
				float num4 = Vector3.Dot(base.transform.up, Vector3.up);
				Vector3 vector = this.prevFramePos - base.transform.position;
				float magnitude = vector.magnitude;
				float num5 = Vector3.Angle(base.transform.up, this.prevFrameUp) / this._camera.fieldOfView * ((float)source.width * 0.75f);
				zero.x = this.rotationScale * num5;
				num5 = Vector3.Angle(base.transform.forward, this.prevFrameForward) / this._camera.fieldOfView * ((float)source.width * 0.75f);
				zero.y = this.rotationScale * num4 * num5;
				num5 = Vector3.Angle(base.transform.forward, this.prevFrameForward) / this._camera.fieldOfView * ((float)source.width * 0.75f);
				zero.z = this.rotationScale * (1f - num4) * num5;
				if (magnitude > Mathf.Epsilon && this.movementScale > Mathf.Epsilon)
				{
					zero.w = this.movementScale * Vector3.Dot(base.transform.forward, vector) * ((float)source.width * 0.5f);
					zero.x += this.movementScale * Vector3.Dot(base.transform.up, vector) * ((float)source.width * 0.5f);
					zero.y += this.movementScale * Vector3.Dot(base.transform.right, vector) * ((float)source.width * 0.5f);
				}
				if (this.preview)
				{
					this.motionBlurMaterial.SetVector("_BlurDirectionPacked", new Vector4(this.previewScale.y, this.previewScale.x, 0f, this.previewScale.z) * 0.5f * this._camera.fieldOfView);
				}
				else
				{
					this.motionBlurMaterial.SetVector("_BlurDirectionPacked", zero);
				}
			}
			else
			{
				Graphics.Blit(source, temporary, this.motionBlurMaterial, 0);
				Camera camera = null;
				if (this.excludeLayers.value != 0)
				{
					camera = this.GetTmpCam();
				}
				if (camera && this.excludeLayers.value != 0 && this.replacementClear && this.replacementClear.isSupported)
				{
					camera.targetTexture = temporary;
					camera.cullingMask = this.excludeLayers;
					camera.RenderWithShader(this.replacementClear, string.Empty);
				}
			}
			if (!this.preview && Time.frameCount != this.prevFrameCount)
			{
				this.prevFrameCount = Time.frameCount;
				this.Remember();
			}
			source.filterMode = FilterMode.Bilinear;
			if (this.showVelocity)
			{
				this.motionBlurMaterial.SetFloat("_DisplayVelocityScale", this.showVelocityScale);
				Graphics.Blit(temporary, destination, this.motionBlurMaterial, 1);
			}
			else if (this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11 && !flag)
			{
				this.dx11MotionBlurMaterial.SetFloat("_MinVelocity", this.minVelocity);
				this.dx11MotionBlurMaterial.SetFloat("_VelocityScale", this.velocityScale);
				this.dx11MotionBlurMaterial.SetFloat("_Jitter", this.jitter);
				this.dx11MotionBlurMaterial.SetTexture("_NoiseTex", this.noiseTexture);
				this.dx11MotionBlurMaterial.SetTexture("_VelTex", temporary);
				this.dx11MotionBlurMaterial.SetTexture("_NeighbourMaxTex", temporary3);
				this.dx11MotionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
				this.dx11MotionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num);
				Graphics.Blit(temporary, temporary2, this.dx11MotionBlurMaterial, 0);
				Graphics.Blit(temporary2, temporary3, this.dx11MotionBlurMaterial, 1);
				Graphics.Blit(source, destination, this.dx11MotionBlurMaterial, 2);
			}
			else if (this.filterType == CameraMotionBlur.MotionBlurFilter.Reconstruction || flag)
			{
				this.motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
				Graphics.Blit(temporary, temporary2, this.motionBlurMaterial, 2);
				Graphics.Blit(temporary2, temporary3, this.motionBlurMaterial, 3);
				Graphics.Blit(source, destination, this.motionBlurMaterial, 4);
			}
			else if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
			{
				Graphics.Blit(source, destination, this.motionBlurMaterial, 6);
			}
			else if (this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDisc)
			{
				this.motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
				Graphics.Blit(temporary, temporary2, this.motionBlurMaterial, 2);
				Graphics.Blit(temporary2, temporary3, this.motionBlurMaterial, 3);
				Graphics.Blit(source, destination, this.motionBlurMaterial, 7);
			}
			else
			{
				Graphics.Blit(source, destination, this.motionBlurMaterial, 5);
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00075AF0 File Offset: 0x00073CF0
		private void CalculateViewProjection()
		{
			Matrix4x4 worldToCameraMatrix = this._camera.worldToCameraMatrix;
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this._camera.projectionMatrix, true);
			this.currentViewProjMat = gpuprojectionMatrix * worldToCameraMatrix;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00075B28 File Offset: 0x00073D28
		public override bool CheckResources()
		{
			base.CheckSupport(true, true);
			this.motionBlurMaterial = base.CheckShaderAndCreateMaterial(this.shader, this.motionBlurMaterial);
			if (this.supportDX11 && this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11)
			{
				this.dx11MotionBlurMaterial = base.CheckShaderAndCreateMaterial(this.dx11MotionBlurShader, this.dx11MotionBlurMaterial);
			}
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00075B94 File Offset: 0x00073D94
		private void Remember()
		{
			this.prevViewProjMat = this.currentViewProjMat;
			this.prevFrameForward = base.transform.forward;
			this.prevFrameUp = base.transform.up;
			this.prevFramePos = base.transform.position;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00075BE0 File Offset: 0x00073DE0
		private Camera GetTmpCam()
		{
			if (this.tmpCam == null)
			{
				string text = "_" + this._camera.name + "_MotionBlurTmpCam";
				GameObject gameObject = GameObject.Find(text);
				if (null == gameObject)
				{
					this.tmpCam = new GameObject(text, new Type[] { typeof(Camera) });
				}
				else
				{
					this.tmpCam = gameObject;
				}
			}
			this.tmpCam.hideFlags = HideFlags.DontSave;
			this.tmpCam.transform.position = this._camera.transform.position;
			this.tmpCam.transform.rotation = this._camera.transform.rotation;
			this.tmpCam.transform.localScale = this._camera.transform.localScale;
			this.tmpCam.GetComponent<Camera>().CopyFrom(this._camera);
			this.tmpCam.GetComponent<Camera>().enabled = false;
			this.tmpCam.GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
			this.tmpCam.GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
			return this.tmpCam.GetComponent<Camera>();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0000A4D8 File Offset: 0x000086D8
		private void StartFrame()
		{
			this.prevFramePos = Vector3.Slerp(this.prevFramePos, base.transform.position, 0.75f);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0000A4FB File Offset: 0x000086FB
		private static int divRoundUp(int x, int d)
		{
			return (x + d - 1) / d;
		}

		// Token: 0x04000490 RID: 1168
		private static readonly float MAX_RADIUS = 10f;

		// Token: 0x04000491 RID: 1169
		public CameraMotionBlur.MotionBlurFilter filterType = CameraMotionBlur.MotionBlurFilter.Reconstruction;

		// Token: 0x04000492 RID: 1170
		public bool preview;

		// Token: 0x04000493 RID: 1171
		public Vector3 previewScale = Vector3.one;

		// Token: 0x04000494 RID: 1172
		public float movementScale;

		// Token: 0x04000495 RID: 1173
		public float rotationScale = 1f;

		// Token: 0x04000496 RID: 1174
		public float maxVelocity = 8f;

		// Token: 0x04000497 RID: 1175
		public float minVelocity = 0.1f;

		// Token: 0x04000498 RID: 1176
		public float velocityScale = 0.375f;

		// Token: 0x04000499 RID: 1177
		public float softZDistance = 0.005f;

		// Token: 0x0400049A RID: 1178
		public int velocityDownsample = 1;

		// Token: 0x0400049B RID: 1179
		public LayerMask excludeLayers = 0;

		// Token: 0x0400049C RID: 1180
		public Shader shader;

		// Token: 0x0400049D RID: 1181
		public Shader dx11MotionBlurShader;

		// Token: 0x0400049E RID: 1182
		public Shader replacementClear;

		// Token: 0x0400049F RID: 1183
		public Texture2D noiseTexture;

		// Token: 0x040004A0 RID: 1184
		public float jitter = 0.05f;

		// Token: 0x040004A1 RID: 1185
		public bool showVelocity;

		// Token: 0x040004A2 RID: 1186
		public float showVelocityScale = 1f;

		// Token: 0x040004A3 RID: 1187
		private Camera _camera;

		// Token: 0x040004A4 RID: 1188
		private Matrix4x4 currentViewProjMat;

		// Token: 0x040004A5 RID: 1189
		private Material dx11MotionBlurMaterial;

		// Token: 0x040004A6 RID: 1190
		private Material motionBlurMaterial;

		// Token: 0x040004A7 RID: 1191
		private int prevFrameCount;

		// Token: 0x040004A8 RID: 1192
		private Vector3 prevFrameForward = Vector3.forward;

		// Token: 0x040004A9 RID: 1193
		private Vector3 prevFramePos = Vector3.zero;

		// Token: 0x040004AA RID: 1194
		private Vector3 prevFrameUp = Vector3.up;

		// Token: 0x040004AB RID: 1195
		private Matrix4x4 prevViewProjMat;

		// Token: 0x040004AC RID: 1196
		private GameObject tmpCam;

		// Token: 0x040004AD RID: 1197
		private bool wasActive;

		// Token: 0x02000191 RID: 401
		[NullableContext(0)]
		public enum MotionBlurFilter
		{
			// Token: 0x040004AF RID: 1199
			CameraMotion,
			// Token: 0x040004B0 RID: 1200
			LocalBlur,
			// Token: 0x040004B1 RID: 1201
			Reconstruction,
			// Token: 0x040004B2 RID: 1202
			ReconstructionDX11,
			// Token: 0x040004B3 RID: 1203
			ReconstructionDisc
		}
	}
}
