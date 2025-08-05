using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000059 RID: 89
[NullableContext(1)]
[Nullable(0)]
public class P3D_Paintable : MonoBehaviour
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x0600017A RID: 378 RVA: 0x000630D4 File Offset: 0x000612D4
	public bool IsReady
	{
		get
		{
			P3D_Tree p3D_Tree = this.tree;
			return p3D_Tree != null && p3D_Tree.IsReady;
		}
	}

	// Token: 0x0600017B RID: 379 RVA: 0x000630F8 File Offset: 0x000612F8
	protected virtual void Awake()
	{
		if (this.Textures != null)
		{
			for (int i = this.Textures.Count - 1; i >= 0; i--)
			{
				P3D_PaintableTexture p3D_PaintableTexture = this.Textures[i];
				if (p3D_PaintableTexture != null)
				{
					p3D_PaintableTexture.Awake(base.gameObject);
				}
			}
		}
		this.UpdateTree();
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00063148 File Offset: 0x00061348
	protected virtual void Update()
	{
		this.applyCooldown -= Time.deltaTime;
		if (this.applyCooldown <= 0f && this.Textures != null)
		{
			this.applyCooldown = this.ApplyInterval;
			for (int i = this.Textures.Count - 1; i >= 0; i--)
			{
				P3D_PaintableTexture p3D_PaintableTexture = this.Textures[i];
				if (p3D_PaintableTexture != null && p3D_PaintableTexture.Painter != null)
				{
					p3D_PaintableTexture.Painter.Apply();
				}
			}
		}
		this.updateCooldown -= Time.deltaTime;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00006206 File Offset: 0x00004406
	protected virtual void OnEnable()
	{
		P3D_Paintable.AllPaintables.Add(this);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00006213 File Offset: 0x00004413
	protected virtual void OnDisable()
	{
		P3D_Paintable.AllPaintables.Remove(this);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x000631D8 File Offset: 0x000613D8
	public static void ScenePaintNearest(P3D_Brush brush, Vector3 position, float maxDistance, int layerMask = -1, int groupMask = -1)
	{
		P3D_Paintable p3D_Paintable = null;
		P3D_Result p3D_Result = null;
		for (int i = P3D_Paintable.AllPaintables.Count - 1; i >= 0; i--)
		{
			P3D_Paintable p3D_Paintable2 = P3D_Paintable.AllPaintables[i];
			if (P3D_Helper.IndexInMask(p3D_Paintable2.gameObject.layer, layerMask))
			{
				P3D_Tree p3D_Tree = p3D_Paintable2.GetTree();
				if (p3D_Tree != null)
				{
					Transform transform = p3D_Paintable2.transform;
					if (P3D_Helper.GetUniformScale(transform) != 0f)
					{
						Vector3 vector = transform.InverseTransformPoint(position);
						P3D_Result p3D_Result2 = p3D_Tree.FindNearest(vector, maxDistance);
						if (p3D_Result2 != null)
						{
							p3D_Paintable = p3D_Paintable2;
							p3D_Result = p3D_Result2;
							maxDistance *= p3D_Result2.Distance01;
						}
					}
				}
			}
		}
		if (p3D_Paintable != null)
		{
			p3D_Paintable.Paint(brush, p3D_Result, groupMask);
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00063284 File Offset: 0x00061484
	public static void ScenePaintBetweenNearestRaycast(P3D_Brush brush, Vector3 startPosition, Vector3 endPosition, int layerMask = -1, int groupMask = -1)
	{
		float num = Vector3.Distance(startPosition, endPosition);
		if (num == 0f)
		{
			return;
		}
		P3D_Paintable p3D_Paintable = null;
		P3D_Result p3D_Result = null;
		RaycastHit raycastHit;
		if (Physics.Raycast(startPosition, endPosition - startPosition, out raycastHit, num, layerMask))
		{
			p3D_Paintable = raycastHit.collider.GetComponent<P3D_Paintable>();
			num = raycastHit.distance;
		}
		for (int i = P3D_Paintable.AllPaintables.Count - 1; i >= 0; i--)
		{
			P3D_Paintable p3D_Paintable2 = P3D_Paintable.AllPaintables[i];
			if (P3D_Helper.IndexInMask(p3D_Paintable2.gameObject.layer, layerMask))
			{
				P3D_Tree p3D_Tree = p3D_Paintable2.GetTree();
				if (p3D_Tree != null)
				{
					Transform transform = p3D_Paintable2.transform;
					Vector3 vector = transform.InverseTransformPoint(startPosition);
					Vector3 normalized = (transform.InverseTransformPoint(endPosition) - vector).normalized;
					P3D_Result p3D_Result2 = p3D_Tree.FindBetweenNearest(vector, vector + normalized * num);
					if (p3D_Result2 != null)
					{
						p3D_Paintable = p3D_Paintable2;
						p3D_Result = p3D_Result2;
						num *= p3D_Result2.Distance01;
					}
				}
			}
		}
		if (p3D_Paintable != null)
		{
			if (p3D_Result != null)
			{
				p3D_Paintable.Paint(brush, p3D_Result, groupMask);
				return;
			}
			p3D_Paintable.Paint(brush, raycastHit, groupMask);
		}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0006339C File Offset: 0x0006159C
	public static void ScenePaintBetweenNearest(P3D_Brush brush, Vector3 startPosition, Vector3 endPosition, int layerMask = -1, int groupMask = -1)
	{
		float num = Vector3.Distance(startPosition, endPosition);
		if (num == 0f)
		{
			return;
		}
		P3D_Paintable p3D_Paintable = null;
		P3D_Result p3D_Result = null;
		for (int i = P3D_Paintable.AllPaintables.Count - 1; i >= 0; i--)
		{
			P3D_Paintable p3D_Paintable2 = P3D_Paintable.AllPaintables[i];
			if (P3D_Helper.IndexInMask(p3D_Paintable2.gameObject.layer, layerMask))
			{
				P3D_Tree p3D_Tree = p3D_Paintable2.GetTree();
				if (p3D_Tree != null)
				{
					Transform transform = p3D_Paintable2.transform;
					Vector3 vector = transform.InverseTransformPoint(startPosition);
					Vector3 normalized = (transform.InverseTransformPoint(endPosition) - vector).normalized;
					P3D_Result p3D_Result2 = p3D_Tree.FindBetweenNearest(vector, vector + normalized * num);
					if (p3D_Result2 != null)
					{
						p3D_Paintable = p3D_Paintable2;
						p3D_Result = p3D_Result2;
						num *= p3D_Result2.Distance01;
					}
				}
			}
		}
		if (p3D_Paintable != null && p3D_Result != null)
		{
			p3D_Paintable.Paint(brush, p3D_Result, groupMask);
		}
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0006347C File Offset: 0x0006167C
	public static void ScenePaintBetweenAll(P3D_Brush brush, Vector3 startPosition, Vector3 endPosition, int layerMask = -1, int groupMask = -1)
	{
		for (int i = P3D_Paintable.AllPaintables.Count - 1; i >= 0; i--)
		{
			P3D_Paintable p3D_Paintable = P3D_Paintable.AllPaintables[i];
			if (P3D_Helper.IndexInMask(p3D_Paintable.gameObject.layer, layerMask))
			{
				p3D_Paintable.PaintBetweenAll(brush, startPosition, endPosition, groupMask);
			}
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x000634D0 File Offset: 0x000616D0
	public static void ScenePaintPerpedicularNearest(P3D_Brush brush, Vector3 position, float maxDistance, int layerMask = -1, int groupMask = -1)
	{
		P3D_Paintable p3D_Paintable = null;
		P3D_Result p3D_Result = null;
		for (int i = P3D_Paintable.AllPaintables.Count - 1; i >= 0; i--)
		{
			P3D_Paintable p3D_Paintable2 = P3D_Paintable.AllPaintables[i];
			if (P3D_Helper.IndexInMask(p3D_Paintable2.gameObject.layer, layerMask))
			{
				P3D_Tree p3D_Tree = p3D_Paintable2.GetTree();
				if (p3D_Tree != null)
				{
					Transform transform = p3D_Paintable2.transform;
					if (P3D_Helper.GetUniformScale(transform) != 0f)
					{
						Vector3 vector = transform.InverseTransformPoint(position);
						P3D_Result p3D_Result2 = p3D_Tree.FindPerpendicularNearest(vector, maxDistance);
						if (p3D_Result2 != null)
						{
							p3D_Paintable = p3D_Paintable2;
							p3D_Result = p3D_Result2;
							maxDistance *= p3D_Result2.Distance01;
						}
					}
				}
			}
		}
		if (p3D_Paintable != null)
		{
			p3D_Paintable.Paint(brush, p3D_Result, groupMask);
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0006357C File Offset: 0x0006177C
	public static void ScenePaintPerpedicularAll(P3D_Brush brush, Vector3 position, float maxDistance, int layerMask = -1, int groupMask = -1)
	{
		for (int i = P3D_Paintable.AllPaintables.Count - 1; i >= 0; i--)
		{
			P3D_Paintable p3D_Paintable = P3D_Paintable.AllPaintables[i];
			if (P3D_Helper.IndexInMask(p3D_Paintable.gameObject.layer, layerMask))
			{
				p3D_Paintable.PaintPerpendicularAll(brush, position, maxDistance, groupMask);
			}
		}
	}

	// Token: 0x06000185 RID: 389 RVA: 0x000635D0 File Offset: 0x000617D0
	public void PaintPerpendicularNearest(P3D_Brush brush, Vector3 position, float maxDistance, int groupMask = -1)
	{
		if (this.CheckTree())
		{
			float uniformScale = P3D_Helper.GetUniformScale(base.transform);
			if (uniformScale != 0f)
			{
				Vector3 vector = base.transform.InverseTransformPoint(position);
				P3D_Result p3D_Result = this.tree.FindPerpendicularNearest(vector, maxDistance / uniformScale);
				this.Paint(brush, p3D_Result, groupMask);
			}
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00063620 File Offset: 0x00061820
	public void PaintPerpendicularAll(P3D_Brush brush, Vector3 position, float maxDistance, int groupMask = -1)
	{
		if (this.CheckTree())
		{
			float uniformScale = P3D_Helper.GetUniformScale(base.transform);
			if (uniformScale != 0f)
			{
				Vector3 vector = base.transform.InverseTransformPoint(position);
				List<P3D_Result> list = this.tree.FindPerpendicularAll(vector, maxDistance / uniformScale);
				this.Paint(brush, list, groupMask);
			}
		}
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00063670 File Offset: 0x00061870
	public void PaintNearest(P3D_Brush brush, Vector3 position, float maxDistance, int groupMask = -1)
	{
		if (this.CheckTree())
		{
			float uniformScale = P3D_Helper.GetUniformScale(base.transform);
			if (uniformScale != 0f)
			{
				Vector3 vector = base.transform.InverseTransformPoint(position);
				P3D_Result p3D_Result = this.tree.FindNearest(vector, maxDistance / uniformScale);
				this.Paint(brush, p3D_Result, groupMask);
			}
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000636C0 File Offset: 0x000618C0
	public void PaintBetweenNearest(P3D_Brush brush, Vector3 startPosition, Vector3 endPosition, int groupMask = -1)
	{
		if (this.CheckTree())
		{
			Vector3 vector = base.transform.InverseTransformPoint(startPosition);
			Vector3 vector2 = base.transform.InverseTransformPoint(endPosition);
			P3D_Result p3D_Result = this.tree.FindBetweenNearest(vector, vector2);
			this.Paint(brush, p3D_Result, groupMask);
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00063708 File Offset: 0x00061908
	public void PaintBetweenAll(P3D_Brush brush, Vector3 startPosition, Vector3 endPosition, int groupMask = -1)
	{
		if (this.CheckTree())
		{
			Vector3 vector = base.transform.InverseTransformPoint(startPosition);
			Vector3 vector2 = base.transform.InverseTransformPoint(endPosition);
			List<P3D_Result> list = this.tree.FindBetweenAll(vector, vector2);
			this.Paint(brush, list, groupMask);
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00063750 File Offset: 0x00061950
	public void Paint(P3D_Brush brush, List<P3D_Result> results, int groupMask = -1)
	{
		if (results != null)
		{
			for (int i = 0; i < results.Count; i++)
			{
				this.Paint(brush, results[i], groupMask);
			}
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00063780 File Offset: 0x00061980
	public void Paint(P3D_Brush brush, P3D_Result result, int groupMask = -1)
	{
		if (result == null || this.Textures == null)
		{
			return;
		}
		for (int i = this.Textures.Count - 1; i >= 0; i--)
		{
			P3D_PaintableTexture p3D_PaintableTexture = this.Textures[i];
			if (p3D_PaintableTexture != null && P3D_Helper.IndexInMask(p3D_PaintableTexture.Group, groupMask))
			{
				p3D_PaintableTexture.Paint(brush, result.GetUV(p3D_PaintableTexture.Coord));
			}
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x000637EC File Offset: 0x000619EC
	public void Paint(P3D_Brush brush, RaycastHit hit, int groupMask = -1)
	{
		if (this.Textures == null)
		{
			return;
		}
		for (int i = this.Textures.Count - 1; i >= 0; i--)
		{
			P3D_PaintableTexture p3D_PaintableTexture = this.Textures[i];
			if (p3D_PaintableTexture != null && P3D_Helper.IndexInMask(p3D_PaintableTexture.Group, groupMask))
			{
				p3D_PaintableTexture.Paint(brush, P3D_Helper.GetUV(hit, p3D_PaintableTexture.Coord));
			}
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00063858 File Offset: 0x00061A58
	public void Paint(P3D_Brush brush, Vector2 uv, int groupMask = -1)
	{
		if (this.Textures == null)
		{
			return;
		}
		for (int i = this.Textures.Count - 1; i >= 0; i--)
		{
			P3D_PaintableTexture p3D_PaintableTexture = this.Textures[i];
			if (p3D_PaintableTexture != null && P3D_Helper.IndexInMask(p3D_PaintableTexture.Group, groupMask))
			{
				p3D_PaintableTexture.Paint(brush, uv);
			}
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00006221 File Offset: 0x00004421
	public P3D_Tree GetTree()
	{
		if (this.tree != null && this.UpdateInterval >= 0f && this.updateCooldown < 0f)
		{
			this.updateCooldown = this.UpdateInterval;
			this.UpdateTree();
		}
		return this.tree;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000638B8 File Offset: 0x00061AB8
	[ContextMenu("Add Texture")]
	public void AddTexture()
	{
		P3D_PaintableTexture p3D_PaintableTexture = new P3D_PaintableTexture();
		if (this.Textures == null)
		{
			this.Textures = new List<P3D_PaintableTexture>();
		}
		this.Textures.Add(p3D_PaintableTexture);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x000638EC File Offset: 0x00061AEC
	[ContextMenu("Update Tree")]
	public void UpdateTree()
	{
		bool flag = false;
		Mesh mesh = P3D_Helper.GetMesh(base.gameObject, ref this.bakedMesh);
		if (this.bakedMesh != null)
		{
			flag = true;
		}
		if (this.tree == null)
		{
			this.tree = new P3D_Tree();
		}
		this.tree.SetMesh(mesh, this.SubMeshIndex, flag);
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000625D File Offset: 0x0000445D
	private bool CheckTree()
	{
		if (this.tree != null)
		{
			if (this.UpdateInterval >= 0f && this.updateCooldown < 0f)
			{
				this.updateCooldown = this.UpdateInterval;
				this.UpdateTree();
			}
			return true;
		}
		return false;
	}

	// Token: 0x0400010E RID: 270
	public static List<P3D_Paintable> AllPaintables = new List<P3D_Paintable>();

	// Token: 0x0400010F RID: 271
	[Tooltip("The submesh in the attached renderer we want to paint to")]
	public int SubMeshIndex;

	// Token: 0x04000110 RID: 272
	[Tooltip("The amount of seconds it takes for the mesh data to be updated (useful for animated meshes). -1 = No updates")]
	public float UpdateInterval = -1f;

	// Token: 0x04000111 RID: 273
	[Tooltip("The amount of seconds it takes for texture modifications to get applied")]
	public float ApplyInterval = 0.01f;

	// Token: 0x04000112 RID: 274
	[Tooltip("All the textures this paintable is associated with")]
	public List<P3D_PaintableTexture> Textures;

	// Token: 0x04000113 RID: 275
	private float applyCooldown;

	// Token: 0x04000114 RID: 276
	private Mesh bakedMesh;

	// Token: 0x04000115 RID: 277
	private P3D_Tree tree;

	// Token: 0x04000116 RID: 278
	private float updateCooldown;
}
