using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200005E RID: 94
[NullableContext(1)]
[Nullable(0)]
[ExecuteInEditMode]
public class P3D_TexturePreview : MonoBehaviour
{
	// Token: 0x060001B6 RID: 438 RVA: 0x000064AA File Offset: 0x000046AA
	protected virtual void Update()
	{
		if (this.age >= 2)
		{
			P3D_Helper.Destroy<GameObject>(base.gameObject);
			return;
		}
		this.age++;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000064D0 File Offset: 0x000046D0
	protected virtual void OnEnable()
	{
		P3D_TexturePreview.AllPreviews.Add(this);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x000064DD File Offset: 0x000046DD
	protected virtual void OnDisable()
	{
		P3D_TexturePreview.AllPreviews.Remove(this);
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000064EB File Offset: 0x000046EB
	protected virtual void OnDestroy()
	{
		P3D_Helper.Destroy<Material>(this.material);
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00063E80 File Offset: 0x00062080
	public static void Show(Mesh mesh, int submeshIndex, Transform transform, float opacity, Texture2D texture, Vector2 tiling, Vector2 offset)
	{
		for (int i = P3D_TexturePreview.AllPreviews.Count - 1; i >= 0; i--)
		{
			P3D_TexturePreview p3D_TexturePreview = P3D_TexturePreview.AllPreviews[i];
			if (p3D_TexturePreview != null && p3D_TexturePreview.age > 0)
			{
				p3D_TexturePreview.UpdateShow(mesh, submeshIndex, transform, opacity, texture, tiling, offset);
				return;
			}
		}
		P3D_TexturePreview p3D_TexturePreview2 = new GameObject("P3D_TexturePreview")
		{
			hideFlags = HideFlags.HideAndDontSave
		}.AddComponent<P3D_TexturePreview>();
		p3D_TexturePreview2.hideFlags = HideFlags.HideAndDontSave;
		p3D_TexturePreview2.UpdateShow(mesh, submeshIndex, transform, opacity, texture, tiling, offset);
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00063F04 File Offset: 0x00062104
	public static void Mark()
	{
		for (int i = P3D_TexturePreview.AllPreviews.Count - 1; i >= 0; i--)
		{
			P3D_TexturePreview p3D_TexturePreview = P3D_TexturePreview.AllPreviews[i];
			if (p3D_TexturePreview != null)
			{
				p3D_TexturePreview.age = 5;
			}
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00063F44 File Offset: 0x00062144
	public static void Sweep()
	{
		for (int i = P3D_TexturePreview.AllPreviews.Count - 1; i >= 0; i--)
		{
			P3D_TexturePreview p3D_TexturePreview = P3D_TexturePreview.AllPreviews[i];
			if (p3D_TexturePreview != null && p3D_TexturePreview.age > 1)
			{
				P3D_TexturePreview.AllPreviews.RemoveAt(i);
				P3D_Helper.Destroy<GameObject>(p3D_TexturePreview.gameObject);
			}
		}
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00063FA0 File Offset: 0x000621A0
	private void UpdateShow(Mesh mesh, int submeshIndex, Transform target, float opacity, Texture2D texture, Vector2 tiling, Vector2 offset)
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
				this.material = new Material(Shader.Find("Hidden/P3D_TexturePreview"));
			}
			base.transform.position = target.position;
			base.transform.rotation = target.rotation;
			base.transform.localScale = target.lossyScale;
			this.material.hideFlags = HideFlags.HideAndDontSave;
			this.material.SetMatrix("_Matrix", target.localToWorldMatrix);
			this.material.SetTexture("_Texture", texture);
			this.material.SetColor("_Tint", new Color(1f, 1f, 1f, opacity));
			this.material.SetTextureScale("_Texture", tiling);
			this.material.SetTextureOffset("_Texture", offset);
			Color white = Color.white;
			Color clear = Color.clear;
			Color clear2 = Color.clear;
			white.a = opacity;
			this.material.SetColor("_Tint", white);
			this.material.SetColor("_Base", clear);
			this.material.SetColor("_Opac", clear2);
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

	// Token: 0x04000130 RID: 304
	private static readonly List<P3D_TexturePreview> AllPreviews = new List<P3D_TexturePreview>();

	// Token: 0x04000131 RID: 305
	private int age;

	// Token: 0x04000132 RID: 306
	private Material material;

	// Token: 0x04000133 RID: 307
	private Material[] materials = new Material[1];

	// Token: 0x04000134 RID: 308
	private MeshFilter meshFilter;

	// Token: 0x04000135 RID: 309
	private MeshRenderer meshRenderer;
}
