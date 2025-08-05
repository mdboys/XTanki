using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000056 RID: 86
[NullableContext(1)]
[Nullable(0)]
public static class P3D_Helper
{
	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600014D RID: 333 RVA: 0x000060F0 File Offset: 0x000042F0
	public static Material ClearMaterial
	{
		get
		{
			if (P3D_Helper.clearMaterial == null)
			{
				P3D_Helper.clearMaterial = new Material(Shader.Find("Transparent/Diffuse"))
				{
					color = Color.clear
				};
			}
			return P3D_Helper.clearMaterial;
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00061D50 File Offset: 0x0005FF50
	public static TextureFormat GetTextureFormat(P3D_Format format)
	{
		TextureFormat textureFormat;
		switch (format)
		{
		case P3D_Format.TruecolorRGBA:
			textureFormat = TextureFormat.RGBA32;
			break;
		case P3D_Format.TruecolorRGB:
			textureFormat = TextureFormat.RGB24;
			break;
		case P3D_Format.TruecolorA:
			textureFormat = TextureFormat.Alpha8;
			break;
		default:
			textureFormat = (TextureFormat)0;
			break;
		}
		return textureFormat;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00006123 File Offset: 0x00004323
	public static bool IndexInMask(int index, LayerMask mask)
	{
		mask &= 1 << index;
		return mask != 0;
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00006142 File Offset: 0x00004342
	public static Texture2D CreateTexture(int width, int height, TextureFormat format, bool mipMaps)
	{
		if (width > 0 && height > 0)
		{
			return new Texture2D(width, height, format, mipMaps);
		}
		return null;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00061D80 File Offset: 0x0005FF80
	public static void ClearTexture(Texture2D texture2D, Color color, bool apply = true)
	{
		if (!(texture2D != null))
		{
			return;
		}
		for (int i = texture2D.height - 1; i >= 0; i--)
		{
			for (int j = texture2D.width - 1; j >= 0; j--)
			{
				texture2D.SetPixel(j, i, color);
			}
		}
		if (apply)
		{
			texture2D.Apply();
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00061DD0 File Offset: 0x0005FFD0
	public static Mesh GetMesh(GameObject gameObject, ref Mesh bakedMesh)
	{
		Mesh mesh = null;
		if (gameObject != null)
		{
			MeshFilter component = gameObject.GetComponent<MeshFilter>();
			if (component != null)
			{
				mesh = component.sharedMesh;
			}
			else
			{
				SkinnedMeshRenderer component2 = gameObject.GetComponent<SkinnedMeshRenderer>();
				if (component2 != null)
				{
					mesh = component2.sharedMesh;
					if (mesh != null)
					{
						if (bakedMesh == null)
						{
							bakedMesh = new Mesh
							{
								name = "Baked Mesh"
							};
						}
						component2.BakeMesh(bakedMesh);
						return bakedMesh;
					}
				}
			}
		}
		P3D_Helper.DestroyMesh(ref bakedMesh);
		return mesh;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00006157 File Offset: 0x00004357
	private static void DestroyMesh(ref Mesh mesh)
	{
		if (mesh != null)
		{
			P3D_Helper.Destroy<Mesh>(mesh);
			mesh = null;
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00061E50 File Offset: 0x00060050
	public static Material GetMaterial(GameObject gameObject, int materialIndex = 0)
	{
		if (gameObject != null && materialIndex >= 0)
		{
			Renderer component = gameObject.GetComponent<Renderer>();
			if (component != null)
			{
				Material[] sharedMaterials = component.sharedMaterials;
				if (materialIndex < sharedMaterials.Length)
				{
					return sharedMaterials[materialIndex];
				}
			}
		}
		return null;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00061E8C File Offset: 0x0006008C
	public static Material CloneMaterial(GameObject gameObject, int materialIndex = 0)
	{
		if (gameObject != null && materialIndex >= 0)
		{
			Renderer component = gameObject.GetComponent<Renderer>();
			if (component != null)
			{
				Material[] sharedMaterials = component.sharedMaterials;
				if (materialIndex < sharedMaterials.Length)
				{
					Material material = sharedMaterials[materialIndex];
					material = (sharedMaterials[materialIndex] = P3D_Helper.Clone<Material>(material, true));
					component.sharedMaterials = sharedMaterials;
					return material;
				}
			}
		}
		return null;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00061EE0 File Offset: 0x000600E0
	public static Material AddMaterial(Renderer renderer, Shader shader, int materialIndex = -1)
	{
		if (renderer != null)
		{
			List<Material> list = renderer.sharedMaterials.ToList<Material>();
			Material material = new Material(shader);
			if (materialIndex <= 0)
			{
				materialIndex = list.Count;
			}
			list.Insert(materialIndex, material);
			renderer.sharedMaterials = list.ToArray();
			return material;
		}
		return null;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00061F2C File Offset: 0x0006012C
	public static Rect SplitHorizontal(ref Rect rect, int separation)
	{
		Rect rect2 = rect;
		rect2.xMax -= rect.width / 2f + (float)separation;
		Rect rect3 = rect;
		rect3.xMin += rect.width / 2f + (float)separation;
		rect = rect2;
		return rect3;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00061F8C File Offset: 0x0006018C
	public static Rect SplitVertical(ref Rect rect, int separation)
	{
		Rect rect2 = rect;
		rect2.yMax -= rect.height / 2f + (float)separation;
		Rect rect3 = rect;
		rect3.yMin += rect.height / 2f + (float)separation;
		rect = rect2;
		return rect3;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000616E File Offset: 0x0000436E
	public static bool Zero(float v)
	{
		return v == 0f;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00006178 File Offset: 0x00004378
	public static float Divide(float a, float b)
	{
		if (!P3D_Helper.Zero(b))
		{
			return a / b;
		}
		return 0f;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000618B File Offset: 0x0000438B
	public static float Reciprocal(float a)
	{
		if (!P3D_Helper.Zero(a))
		{
			return 1f / a;
		}
		return 0f;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00061FEC File Offset: 0x000601EC
	public static float GetUniformScale(Transform transform)
	{
		Vector3 lossyScale = transform.lossyScale;
		return (lossyScale.x + lossyScale.y + lossyScale.z) / 3f;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0006201C File Offset: 0x0006021C
	public static Vector2 GetUV(RaycastHit hit, P3D_CoordType coord)
	{
		Vector2 vector;
		if (coord != P3D_CoordType.UV1)
		{
			if (coord != P3D_CoordType.UV2)
			{
				vector = default(Vector2);
			}
			else
			{
				vector = hit.textureCoord2;
			}
		}
		else
		{
			vector = hit.textureCoord;
		}
		return vector;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000061A2 File Offset: 0x000043A2
	public static float DampenFactor(float dampening, float elapsed)
	{
		return 1f - Mathf.Pow(2.7182817f, (0f - dampening) * elapsed);
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00062054 File Offset: 0x00060254
	public static Vector2 CalculatePixelFromCoord(Vector2 uv, Vector2 tiling, Vector2 offset, int width, int height)
	{
		uv.x = Mathf.Repeat(uv.x * tiling.x + offset.x, 1f);
		uv.y = Mathf.Repeat(uv.y * tiling.y + offset.y, 1f);
		uv.x = (float)Mathf.Clamp(Mathf.RoundToInt(uv.x * (float)width), 0, width - 1);
		uv.y = (float)Mathf.Clamp(Mathf.RoundToInt(uv.y * (float)height), 0, height - 1);
		return uv;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x000620EC File Offset: 0x000602EC
	public static P3D_Matrix CreateMatrix(Vector2 position, Vector2 size, float angle)
	{
		P3D_Matrix p3D_Matrix = P3D_Matrix.Translation(position.x, position.y);
		P3D_Matrix p3D_Matrix2 = P3D_Matrix.Rotation(angle);
		P3D_Matrix p3D_Matrix3 = P3D_Matrix.Translation(size.x * -0.5f, size.y * -0.5f);
		P3D_Matrix p3D_Matrix4 = P3D_Matrix.Scaling(size.x, size.y);
		return p3D_Matrix * p3D_Matrix2 * p3D_Matrix3 * p3D_Matrix4;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00062154 File Offset: 0x00060354
	public static float Dampen(float current, float target, float dampening, float elapsed, float minStep = 0f)
	{
		float num = P3D_Helper.DampenFactor(dampening, elapsed);
		float num2 = Mathf.Abs(target - current) * num + minStep * elapsed;
		return Mathf.MoveTowards(current, target, num2);
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00062184 File Offset: 0x00060384
	public static Vector3 Dampen3(Vector3 current, Vector3 target, float dampening, float elapsed, float minStep = 0f)
	{
		float num = P3D_Helper.DampenFactor(dampening, elapsed);
		float num2 = (target - current).magnitude * num + minStep * elapsed;
		return Vector3.MoveTowards(current, target, num2);
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000621B8 File Offset: 0x000603B8
	public static T Destroy<[Nullable(0)] T>(T o) where T : global::UnityEngine.Object
	{
		global::UnityEngine.Object.Destroy(o);
		return default(T);
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000621DC File Offset: 0x000603DC
	public static bool IntersectBarycentric(Vector3 start, Vector3 end, P3D_Triangle triangle, out Vector3 weights, out float distance01)
	{
		weights = default(Vector3);
		distance01 = 0f;
		Vector3 edge = triangle.Edge1;
		Vector3 edge2 = triangle.Edge2;
		Vector3 vector = end - start;
		Vector3 vector2 = Vector3.Cross(vector, edge2);
		float num = Vector3.Dot(edge, vector2);
		if (Mathf.Abs(num) < 1E-45f)
		{
			return false;
		}
		float num2 = 1f / num;
		Vector3 vector3 = start - triangle.PointA;
		weights.x = Vector3.Dot(vector3, vector2) * num2;
		float num3 = weights.x;
		bool flag = num3 < -1E-45f || num3 > 1f;
		if (flag)
		{
			return false;
		}
		Vector3 vector4 = Vector3.Cross(vector3, edge);
		weights.y = Vector3.Dot(vector, vector4) * num2;
		float num4 = weights.x + weights.y;
		if (weights.y < -1E-45f || num4 > 1f)
		{
			return false;
		}
		weights = new Vector3(1f - num4, weights.x, weights.y);
		distance01 = Vector3.Dot(edge2, vector4) * num2;
		num3 = distance01;
		return num3 >= 0f && num3 <= 1f;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0006230C File Offset: 0x0006050C
	public static float ClosestBarycentric(Vector3 point, P3D_Triangle triangle, out Vector3 weights)
	{
		Vector3 pointA = triangle.PointA;
		Vector3 pointB = triangle.PointB;
		Vector3 pointC = triangle.PointC;
		Quaternion quaternion = Quaternion.Inverse(Quaternion.LookRotation(-Vector3.Cross(pointA - pointB, pointA - pointC)));
		Vector3 vector = quaternion * pointA;
		Vector3 vector2 = quaternion * pointB;
		Vector3 vector3 = quaternion * pointC;
		Vector3 vector4 = quaternion * point;
		if (P3D_Helper.PointLeftOfLine(vector, vector2, vector4))
		{
			float num = P3D_Helper.ClosestBarycentric(vector4, vector, vector2);
			weights = new Vector3(1f - num, num, 0f);
		}
		else if (P3D_Helper.PointLeftOfLine(vector2, vector3, vector4))
		{
			float num2 = P3D_Helper.ClosestBarycentric(vector4, vector2, vector3);
			weights = new Vector3(0f, 1f - num2, num2);
		}
		else if (P3D_Helper.PointLeftOfLine(vector3, vector, vector4))
		{
			float num3 = P3D_Helper.ClosestBarycentric(vector4, vector3, vector);
			weights = new Vector3(num3, 0f, 1f - num3);
		}
		else
		{
			Vector3 vector5 = vector2 - vector;
			Vector3 vector6 = vector3 - vector;
			Vector3 vector7 = vector4 - vector;
			float num4 = Vector2.Dot(vector5, vector5);
			float num5 = Vector2.Dot(vector5, vector6);
			float num6 = Vector2.Dot(vector6, vector6);
			float num7 = Vector2.Dot(vector7, vector5);
			float num8 = Vector2.Dot(vector7, vector6);
			float num9 = P3D_Helper.Reciprocal(num4 * num6 - num5 * num5);
			weights.y = (num6 * num7 - num5 * num8) * num9;
			weights.z = (num4 * num8 - num5 * num7) * num9;
			weights.x = 1f - weights.y - weights.z;
		}
		Vector3 vector8 = weights.x * pointA + weights.y * pointB + weights.z * pointC;
		return (point - vector8).sqrMagnitude;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0006258C File Offset: 0x0006078C
	public static bool ClosestBarycentric(Vector3 point, P3D_Triangle triangle, ref Vector3 weights, ref float distanceSqr)
	{
		Vector3 pointA = triangle.PointA;
		Vector3 pointB = triangle.PointB;
		Vector3 pointC = triangle.PointC;
		Quaternion quaternion = Quaternion.Inverse(Quaternion.LookRotation(-Vector3.Cross(pointA - pointB, pointA - pointC)));
		Vector3 vector = quaternion * pointA;
		Vector3 vector2 = quaternion * pointB;
		Vector3 vector3 = quaternion * pointC;
		Vector3 vector4 = quaternion * point;
		if (P3D_Helper.PointRightOfLine(vector, vector2, vector4) && P3D_Helper.PointRightOfLine(vector2, vector3, vector4) && P3D_Helper.PointRightOfLine(vector3, vector, vector4))
		{
			Vector3 vector5 = vector2 - vector;
			Vector3 vector6 = vector3 - vector;
			Vector3 vector7 = vector4 - vector;
			float num = Vector2.Dot(vector5, vector5);
			float num2 = Vector2.Dot(vector5, vector6);
			float num3 = Vector2.Dot(vector6, vector6);
			float num4 = Vector2.Dot(vector7, vector5);
			float num5 = Vector2.Dot(vector7, vector6);
			float num6 = P3D_Helper.Reciprocal(num * num3 - num2 * num2);
			weights.y = (num3 * num4 - num2 * num5) * num6;
			weights.z = (num * num5 - num2 * num4) * num6;
			weights.x = 1f - weights.y - weights.z;
			Vector3 vector8 = weights.x * pointA + weights.y * pointB + weights.z * pointC;
			distanceSqr = (point - vector8).sqrMagnitude;
			return true;
		}
		return false;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00062768 File Offset: 0x00060968
	public static float ClosestBarycentric(Vector2 point, Vector2 start, Vector2 end)
	{
		Vector2 vector = end - start;
		float sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude > 0f)
		{
			return Mathf.Clamp01(Vector2.Dot(point - start, vector / sqrMagnitude));
		}
		return 0.5f;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000627AC File Offset: 0x000609AC
	public static bool PointLeftOfLine(Vector2 a, Vector2 b, Vector2 p)
	{
		return (b.x - a.x) * (p.y - a.y) - (p.x - a.x) * (b.y - a.y) >= 0f;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000627FC File Offset: 0x000609FC
	public static bool PointRightOfLine(Vector2 a, Vector2 b, Vector2 p)
	{
		return (b.x - a.x) * (p.y - a.y) - (p.x - a.x) * (b.y - a.y) <= 0f;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0006284C File Offset: 0x00060A4C
	public static T Clone<[Nullable(0)] T>(T o, bool keepName = true) where T : global::UnityEngine.Object
	{
		if (o != null)
		{
			T t = global::UnityEngine.Object.Instantiate<T>(o);
			if (t != null && keepName)
			{
				t.name = o.name;
			}
			return t;
		}
		return default(T);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000628A0 File Offset: 0x00060AA0
	public static bool IsWritableFormat(TextureFormat format)
	{
		switch (format)
		{
		case TextureFormat.Alpha8:
			return true;
		case TextureFormat.ARGB4444:
			break;
		case TextureFormat.RGB24:
			return true;
		case TextureFormat.RGBA32:
			return true;
		case TextureFormat.ARGB32:
			return true;
		default:
			if (format == TextureFormat.BGRA32)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x040000FC RID: 252
	public const string ComponentMenuPrefix = "Paint in 3D/P3D ";

	// Token: 0x040000FD RID: 253
	private static Material clearMaterial;
}
