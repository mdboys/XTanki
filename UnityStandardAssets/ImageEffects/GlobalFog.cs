using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x020001A4 RID: 420
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Rendering/Global Fog")]
	internal class GlobalFog : PostEffectsBase
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x00078E1C File Offset: 0x0007701C
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources() || (!this.distanceFog && !this.heightFog))
			{
				Graphics.Blit(source, destination);
				return;
			}
			Camera component = base.GetComponent<Camera>();
			Transform transform = component.transform;
			float nearClipPlane = component.nearClipPlane;
			float farClipPlane = component.farClipPlane;
			float fieldOfView = component.fieldOfView;
			float aspect = component.aspect;
			Matrix4x4 identity = Matrix4x4.identity;
			float num = fieldOfView * 0.5f;
			Vector3 vector = transform.right * nearClipPlane * Mathf.Tan(num * 0.017453292f) * aspect;
			Vector3 vector2 = transform.up * nearClipPlane * Mathf.Tan(num * 0.017453292f);
			Vector3 vector3 = transform.forward * nearClipPlane - vector + vector2;
			float num2 = vector3.magnitude * farClipPlane / nearClipPlane;
			vector3.Normalize();
			vector3 *= num2;
			Vector3 vector4 = transform.forward * nearClipPlane + vector + vector2;
			vector4.Normalize();
			vector4 *= num2;
			Vector3 vector5 = transform.forward * nearClipPlane + vector - vector2;
			vector5.Normalize();
			vector5 *= num2;
			Vector3 vector6 = transform.forward * nearClipPlane - vector - vector2;
			vector6.Normalize();
			vector6 *= num2;
			identity.SetRow(0, vector3);
			identity.SetRow(1, vector4);
			identity.SetRow(2, vector5);
			identity.SetRow(3, vector6);
			Vector3 position = transform.position;
			float num3 = position.y - this.height;
			float num4 = ((num3 > 0f) ? 0f : 1f);
			this.fogMaterial.SetMatrix("_FrustumCornersWS", identity);
			this.fogMaterial.SetVector("_CameraWS", position);
			this.fogMaterial.SetVector("_HeightParams", new Vector4(this.height, num3, num4, this.heightDensity * 0.5f));
			this.fogMaterial.SetVector("_DistanceParams", new Vector4(0f - Mathf.Max(this.startDistance, 0f), 0f, 0f, 0f));
			FogMode fogMode = RenderSettings.fogMode;
			float fogDensity = RenderSettings.fogDensity;
			float fogStartDistance = RenderSettings.fogStartDistance;
			float fogEndDistance = RenderSettings.fogEndDistance;
			bool flag = fogMode == FogMode.Linear;
			float num5 = ((!flag) ? 0f : (fogEndDistance - fogStartDistance));
			float num6 = ((Mathf.Abs(num5) <= 0.0001f) ? 0f : (1f / num5));
			Vector4 vector7 = default(Vector4);
			vector7.x = fogDensity * 1.2011224f;
			vector7.y = fogDensity * 1.442695f;
			vector7.z = ((!flag) ? 0f : (0f - num6));
			vector7.w = ((!flag) ? 0f : (fogEndDistance * num6));
			this.fogMaterial.SetVector("_SceneFogParams", vector7);
			this.fogMaterial.SetVector("_SceneFogMode", new Vector4((float)fogMode, this.useRadialDistance > false, 0f, 0f));
			int num7 = ((!this.distanceFog || !this.heightFog) ? (this.distanceFog ? 1 : 2) : 0);
			GlobalFog.CustomGraphicsBlit(source, destination, this.fogMaterial, num7);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0000A82D File Offset: 0x00008A2D
		public override bool CheckResources()
		{
			base.CheckSupport(true);
			this.fogMaterial = base.CheckShaderAndCreateMaterial(this.fogShader, this.fogMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000791A0 File Offset: 0x000773A0
		private static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
		{
			RenderTexture.active = dest;
			fxMaterial.SetTexture("_MainTex", source);
			GL.PushMatrix();
			GL.LoadOrtho();
			fxMaterial.SetPass(passNr);
			GL.Begin(7);
			GL.MultiTexCoord2(0, 0f, 0f);
			GL.Vertex3(0f, 0f, 3f);
			GL.MultiTexCoord2(0, 1f, 0f);
			GL.Vertex3(1f, 0f, 2f);
			GL.MultiTexCoord2(0, 1f, 1f);
			GL.Vertex3(1f, 1f, 1f);
			GL.MultiTexCoord2(0, 0f, 1f);
			GL.Vertex3(0f, 1f, 0f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x0400055F RID: 1375
		[Tooltip("Apply distance-based fog?")]
		public bool distanceFog = true;

		// Token: 0x04000560 RID: 1376
		[Tooltip("Distance fog is based on radial distance from camera when checked")]
		public bool useRadialDistance;

		// Token: 0x04000561 RID: 1377
		[Tooltip("Apply height-based fog?")]
		public bool heightFog = true;

		// Token: 0x04000562 RID: 1378
		[Tooltip("Fog top Y coordinate")]
		public float height = 1f;

		// Token: 0x04000563 RID: 1379
		[Range(0.001f, 10f)]
		public float heightDensity = 2f;

		// Token: 0x04000564 RID: 1380
		[Tooltip("Push fog away from the camera by this amount")]
		public float startDistance;

		// Token: 0x04000565 RID: 1381
		public Shader fogShader;

		// Token: 0x04000566 RID: 1382
		private Material fogMaterial;
	}
}
