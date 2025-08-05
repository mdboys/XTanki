using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tanks.Battle.ClientGraphics.API;
using UnityEngine;
using UnityEngine.Rendering;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000011 RID: 17
[NullableContext(1)]
[Nullable(0)]
[RequireComponent(typeof(Bloom))]
public class BurningTargetBloom : MonoBehaviour
{
	// Token: 0x06000039 RID: 57 RVA: 0x000056A8 File Offset: 0x000038A8
	private void Reset()
	{
		this.bloomMaskShader = Shader.Find("Alternativa/PostEffects/TargetBloom/BloomMask");
		this.brightPassFilterShader = Shader.Find("Alternativa/PostEffects/TargetBloom/BrightPassFilter");
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0005E148 File Offset: 0x0005C348
	private void Update()
	{
		this.commandBuffer.Clear();
		this.commandBuffer.SetRenderTarget(this.bloomMask);
		this.commandBuffer.ClearRenderTarget(true, true, Color.black, 1f);
		foreach (Renderer renderer in this.targets)
		{
			if (!(renderer == null))
			{
				Material[] materials = renderer.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					if (materials[i].HasProperty(TankMaterialPropertyNames.COLORING_MAP_ALPHA_DEF_ID))
					{
						if (materials[i].GetFloat(TankMaterialPropertyNames.COLORING_MAP_ALPHA_DEF_ID).Equals(1f))
						{
							this.commandBuffer.DrawRenderer(renderer, this.bloomMaskMaterial, i);
						}
						else if (materials[i].HasProperty(TankMaterialPropertyNames.TEMPERATURE_ID) && materials[i].GetFloat(TankMaterialPropertyNames.TEMPERATURE_ID) > 0f)
						{
							this.commandBuffer.DrawRenderer(renderer, this.bloomMaskMaterial, i);
						}
					}
					else
					{
						this.commandBuffer.DrawRenderer(renderer, this.bloomMaskMaterial, i);
					}
				}
			}
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0005E288 File Offset: 0x0005C488
	private void OnEnable()
	{
		this.commandBuffer = this.CreateCommandBuffer();
		base.GetComponent<Camera>().AddCommandBuffer(CameraEvent.AfterDepthTexture, this.commandBuffer);
		this.bloomMask = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
		this.bloomMaskMaterial = new Material(this.bloomMaskShader);
		this.bloom = base.GetComponent<Bloom>();
		this.bloom.enabled = false;
		this.bloom.brightPassFilterShader = this.brightPassFilterShader;
		this.bloom.brightPassFilterMaterial = new Material(this.brightPassFilterShader)
		{
			hideFlags = HideFlags.DontSave
		};
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000056CA File Offset: 0x000038CA
	private void OnDisable()
	{
		if (this.commandBuffer != null)
		{
			base.GetComponent<Camera>().RemoveCommandBuffer(CameraEvent.AfterDepthTexture, this.commandBuffer);
			this.commandBuffer.Dispose();
			this.bloomMask.Release();
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000056FC File Offset: 0x000038FC
	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.bloom.brightPassFilterMaterial.SetTexture("_TargetBloomMask", this.bloomMask);
		this.bloom.OnRenderImage(source, destination);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00005726 File Offset: 0x00003926
	private CommandBuffer CreateCommandBuffer()
	{
		return new CommandBuffer
		{
			name = "HotBloomCommandBuffer"
		};
	}

	// Token: 0x04000019 RID: 25
	[HideInInspector]
	public Shader bloomMaskShader;

	// Token: 0x0400001A RID: 26
	[HideInInspector]
	public Shader brightPassFilterShader;

	// Token: 0x0400001B RID: 27
	public List<Renderer> targets;

	// Token: 0x0400001C RID: 28
	private Bloom bloom;

	// Token: 0x0400001D RID: 29
	private Material bloomFilterMaterial;

	// Token: 0x0400001E RID: 30
	private RenderTexture bloomMask;

	// Token: 0x0400001F RID: 31
	private Material bloomMaskMaterial;

	// Token: 0x04000020 RID: 32
	private CommandBuffer commandBuffer;
}
