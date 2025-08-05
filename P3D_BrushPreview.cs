using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000051 RID: 81
[NullableContext(1)]
[Nullable(0)]
[ExecuteInEditMode]
public class P3D_BrushPreview : MonoBehaviour
{
	// Token: 0x0600013D RID: 317 RVA: 0x00006035 File Offset: 0x00004235
	protected virtual void Update()
	{
		if (this.age >= 2)
		{
			P3D_Helper.Destroy<GameObject>(base.gameObject);
			return;
		}
		this.age++;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000605B File Offset: 0x0000425B
	protected virtual void OnEnable()
	{
		P3D_BrushPreview.AllPreviews.Add(this);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00006068 File Offset: 0x00004268
	protected virtual void OnDisable()
	{
		P3D_BrushPreview.AllPreviews.Remove(this);
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00006076 File Offset: 0x00004276
	protected virtual void OnDestroy()
	{
		P3D_Helper.Destroy<Material>(this.material);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00061A5C File Offset: 0x0005FC5C
	public static void Show(Mesh mesh, int submeshIndex, Transform transform, float opacity, P3D_Matrix paintMatrix, Vector2 canvasResolution, Texture2D shape, Vector2 tiling, Vector2 offset)
	{
		for (int i = P3D_BrushPreview.AllPreviews.Count - 1; i >= 0; i--)
		{
			P3D_BrushPreview p3D_BrushPreview = P3D_BrushPreview.AllPreviews[i];
			if (p3D_BrushPreview != null && p3D_BrushPreview.age > 0)
			{
				p3D_BrushPreview.UpdateShow(mesh, submeshIndex, transform, opacity, paintMatrix, canvasResolution, shape, tiling, offset);
				return;
			}
		}
		P3D_BrushPreview p3D_BrushPreview2 = new GameObject("P3D_BrushPreview")
		{
			hideFlags = HideFlags.HideAndDontSave
		}.AddComponent<P3D_BrushPreview>();
		p3D_BrushPreview2.hideFlags = HideFlags.HideAndDontSave;
		p3D_BrushPreview2.UpdateShow(mesh, submeshIndex, transform, opacity, paintMatrix, canvasResolution, shape, tiling, offset);
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00061AE8 File Offset: 0x0005FCE8
	public static void Mark()
	{
		for (int i = P3D_BrushPreview.AllPreviews.Count - 1; i >= 0; i--)
		{
			P3D_BrushPreview p3D_BrushPreview = P3D_BrushPreview.AllPreviews[i];
			if (p3D_BrushPreview != null)
			{
				p3D_BrushPreview.age = 5;
			}
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00061B28 File Offset: 0x0005FD28
	public static void Sweep()
	{
		for (int i = P3D_BrushPreview.AllPreviews.Count - 1; i >= 0; i--)
		{
			P3D_BrushPreview p3D_BrushPreview = P3D_BrushPreview.AllPreviews[i];
			if (p3D_BrushPreview != null && p3D_BrushPreview.age > 1)
			{
				P3D_BrushPreview.AllPreviews.RemoveAt(i);
				P3D_Helper.Destroy<GameObject>(p3D_BrushPreview.gameObject);
			}
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00061B84 File Offset: 0x0005FD84
	private void UpdateShow(Mesh mesh, int submeshIndex, Transform target, float opacity, P3D_Matrix paintMatrix, Vector2 canvasResolution, Texture2D shape, Vector2 tiling, Vector2 offset)
	{
		if (target != null)
		{
			if (this.meshRenderer == null)
			{
				this.meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
			}
			if (this.meshFilter == null)
			{
				this.meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.material == null)
			{
				this.material = new Material(Shader.Find("Hidden/P3D_BrushPreview"));
			}
			base.transform.position = target.position;
			base.transform.rotation = target.rotation;
			base.transform.localScale = target.lossyScale;
			this.material.hideFlags = HideFlags.HideAndDontSave;
			this.material.SetMatrix("_WorldMatrix", target.localToWorldMatrix);
			this.material.SetMatrix("_PaintMatrix", paintMatrix.Matrix4x4);
			this.material.SetVector("_CanvasResolution", canvasResolution);
			this.material.SetVector("_Tiling", tiling);
			this.material.SetVector("_Offset", offset);
			this.material.SetColor("_Color", new Color(1f, 1f, 1f, opacity));
			this.material.SetTexture("_Shape", shape);
			if (this.materials.Length != submeshIndex + 1)
			{
				this.materials = new Material[submeshIndex + 1];
			}
			for (int i = 0; i < submeshIndex; i++)
			{
				this.materials[i] = P3D_Helper.ClearMaterial;
			}
			this.materials[submeshIndex] = this.material;
			this.meshRenderer.sharedMaterials = this.materials;
			this.meshFilter.sharedMesh = mesh;
			this.age = 0;
		}
	}

	// Token: 0x040000ED RID: 237
	private static readonly List<P3D_BrushPreview> AllPreviews = new List<P3D_BrushPreview>();

	// Token: 0x040000EE RID: 238
	private int age;

	// Token: 0x040000EF RID: 239
	private Material material;

	// Token: 0x040000F0 RID: 240
	private Material[] materials = new Material[1];

	// Token: 0x040000F1 RID: 241
	private MeshFilter meshFilter;

	// Token: 0x040000F2 RID: 242
	private MeshRenderer meshRenderer;
}
