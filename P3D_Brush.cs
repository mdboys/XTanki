using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000049 RID: 73
[NullableContext(1)]
[Nullable(0)]
[Serializable]
public class P3D_Brush
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000122 RID: 290 RVA: 0x00005F3F File Offset: 0x0000413F
	public static P3D_Brush TempInstance
	{
		get
		{
			if (P3D_Brush.tempInstance == null)
			{
				P3D_Brush.tempInstance = new P3D_Brush();
			}
			return P3D_Brush.tempInstance;
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00005F57 File Offset: 0x00004157
	public P3D_Brush GetTempClone()
	{
		this.CopyTo(P3D_Brush.TempInstance);
		return P3D_Brush.tempInstance;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000609F8 File Offset: 0x0005EBF8
	public void CopyTo(P3D_Brush other)
	{
		if (other != null)
		{
			other.Name = this.Name;
			other.Opacity = this.Opacity;
			other.Angle = this.Angle;
			other.Offset = this.Offset;
			other.Size = this.Size;
			other.Blend = this.Blend;
			other.Color = this.Color;
			other.Direction = this.Direction;
			other.Shape = this.Shape;
			other.Detail = this.Detail;
			other.DetailScale = this.DetailScale;
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00060A90 File Offset: 0x0005EC90
	public void Paint(Texture2D newCanvas, P3D_Matrix newMatrix)
	{
		P3D_Brush.canvas = newCanvas;
		P3D_Brush.canvasW = newCanvas.width;
		P3D_Brush.canvasH = newCanvas.height;
		P3D_Brush.matrix = newMatrix;
		if (this.CalculateRect(ref P3D_Brush.rect))
		{
			P3D_Brush.inverse = newMatrix.Inverse;
			P3D_Brush.opacity = this.Opacity;
			P3D_Brush.color = this.Color;
			P3D_Brush.direction = this.Direction;
			P3D_Brush.shape = this.Shape;
			P3D_Brush.detail = this.Detail;
			P3D_Brush.detailScale = this.DetailScale;
			Action<Texture2D, P3D_Rect> onPrePaint = P3D_Brush.OnPrePaint;
			if (onPrePaint != null)
			{
				onPrePaint(P3D_Brush.canvas, P3D_Brush.rect);
			}
			switch (this.Blend)
			{
			case P3D_BlendMode.AlphaBlend:
				P3D_Brush.AlphaBlend.Paint();
				break;
			case P3D_BlendMode.AlphaBlendRgb:
				P3D_Brush.AlphaBlendRGB.Paint();
				break;
			case P3D_BlendMode.AlphaErase:
				P3D_Brush.AlphaErase.Paint();
				break;
			case P3D_BlendMode.AdditiveBlend:
				P3D_Brush.AdditiveBlend.Paint();
				break;
			case P3D_BlendMode.SubtractiveBlend:
				P3D_Brush.SubtractiveBlend.Paint();
				break;
			case P3D_BlendMode.NormalBlend:
				P3D_Brush.NormalBlend.Paint();
				break;
			case P3D_BlendMode.Replace:
				P3D_Brush.Replace.Paint();
				break;
			}
			Action<Texture2D, P3D_Rect> onPostPaint = P3D_Brush.OnPostPaint;
			if (onPostPaint == null)
			{
				return;
			}
			onPostPaint(P3D_Brush.canvas, P3D_Brush.rect);
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00060BAC File Offset: 0x0005EDAC
	private bool CalculateRect(ref P3D_Rect rect)
	{
		Vector2 vector = P3D_Brush.matrix.MultiplyPoint(0f, 0f);
		Vector2 vector2 = P3D_Brush.matrix.MultiplyPoint(1f, 0f);
		Vector2 vector3 = P3D_Brush.matrix.MultiplyPoint(0f, 1f);
		Vector2 vector4 = P3D_Brush.matrix.MultiplyPoint(1f, 1f);
		float num = Mathf.Min(Mathf.Min(vector.x, vector2.x), Mathf.Min(vector3.x, vector4.x));
		float num2 = Mathf.Max(Mathf.Max(vector.x, vector2.x), Mathf.Max(vector3.x, vector4.x));
		float num3 = Mathf.Min(Mathf.Min(vector.y, vector2.y), Mathf.Min(vector3.y, vector4.y));
		float num4 = Mathf.Max(Mathf.Max(vector.y, vector2.y), Mathf.Max(vector3.y, vector4.y));
		if (num < num2 && num3 < num4)
		{
			rect.XMin = Mathf.Clamp(Mathf.FloorToInt(num), 0, P3D_Brush.canvasW);
			rect.XMax = Mathf.Clamp(Mathf.CeilToInt(num2), 0, P3D_Brush.canvasW);
			rect.YMin = Mathf.Clamp(Mathf.FloorToInt(num3), 0, P3D_Brush.canvasH);
			rect.YMax = Mathf.Clamp(Mathf.CeilToInt(num4), 0, P3D_Brush.canvasH);
			return true;
		}
		return false;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00060D1C File Offset: 0x0005EF1C
	private static bool IsInsideShape(P3D_Matrix inverseMatrix, int x, int y, ref Vector2 shapeCoord)
	{
		shapeCoord = inverseMatrix.MultiplyPoint((float)x, (float)y);
		if (shapeCoord.x >= 0f)
		{
			Vector2 vector = shapeCoord;
			if (vector.x < 1f)
			{
				float y2 = vector.y;
				if (y2 >= 0f && y2 < 1f)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00005F69 File Offset: 0x00004169
	private static Color SampleRepeat(Texture2D texture, float u, float v)
	{
		return texture.GetPixelBilinear(u % 1f, v % 1f);
	}

	// Token: 0x040000D3 RID: 211
	public static Action<Texture2D, P3D_Rect> OnPrePaint;

	// Token: 0x040000D4 RID: 212
	public static Action<Texture2D, P3D_Rect> OnPostPaint;

	// Token: 0x040000D5 RID: 213
	private static Texture2D canvas;

	// Token: 0x040000D6 RID: 214
	private static int canvasW;

	// Token: 0x040000D7 RID: 215
	private static int canvasH;

	// Token: 0x040000D8 RID: 216
	private static P3D_Rect rect;

	// Token: 0x040000D9 RID: 217
	private static P3D_Matrix matrix;

	// Token: 0x040000DA RID: 218
	private static P3D_Matrix inverse;

	// Token: 0x040000DB RID: 219
	private static float opacity;

	// Token: 0x040000DC RID: 220
	private static Color color;

	// Token: 0x040000DD RID: 221
	private static Vector2 direction;

	// Token: 0x040000DE RID: 222
	private static Texture2D shape;

	// Token: 0x040000DF RID: 223
	private static Texture2D detail;

	// Token: 0x040000E0 RID: 224
	private static Vector2 detailScale;

	// Token: 0x040000E1 RID: 225
	private static P3D_Brush tempInstance;

	// Token: 0x040000E2 RID: 226
	[Tooltip("The name of this brush (mainly used for saving/loading)")]
	public string Name = "Default";

	// Token: 0x040000E3 RID: 227
	[Tooltip("The opacity of the brush (how solid it is)")]
	[Range(0f, 1f)]
	public float Opacity = 1f;

	// Token: 0x040000E4 RID: 228
	[Tooltip("The angle of the brush in radians")]
	[Range(-3.1415927f, 3.1415927f)]
	public float Angle;

	// Token: 0x040000E5 RID: 229
	[Tooltip("The amount of pixels the brush gets moved from the pain location")]
	public Vector2 Offset;

	// Token: 0x040000E6 RID: 230
	[Tooltip("The size of the brush in pixels")]
	public Vector2 Size = new Vector2(10f, 10f);

	// Token: 0x040000E7 RID: 231
	[Tooltip("The blend mode of the brush")]
	public P3D_BlendMode Blend;

	// Token: 0x040000E8 RID: 232
	[Tooltip("The shape of the brush")]
	public Texture2D Shape;

	// Token: 0x040000E9 RID: 233
	[Tooltip("The color of the brush")]
	public Color Color = Color.white;

	// Token: 0x040000EA RID: 234
	[Tooltip("The normal direction of the brush (used for NormalBlend)")]
	public Vector2 Direction;

	// Token: 0x040000EB RID: 235
	[Tooltip("The detail texture when painting")]
	public Texture2D Detail;

	// Token: 0x040000EC RID: 236
	[Tooltip("The scale of the detail texture, allowing you to tile it")]
	public Vector2 DetailScale = new Vector2(0.5f, 0.5f);

	// Token: 0x0200004A RID: 74
	[NullableContext(0)]
	private static class AdditiveBlend
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00060DD8 File Offset: 0x0005EFD8
		public static void Paint()
		{
			Vector2 vector = default(Vector2);
			float num = P3D_Helper.Reciprocal((float)P3D_Brush.canvasW * P3D_Brush.detailScale.x);
			float num2 = P3D_Helper.Reciprocal((float)P3D_Brush.canvasH * P3D_Brush.detailScale.y);
			P3D_Brush.color.a = P3D_Brush.color.a * P3D_Brush.opacity;
			for (int i = P3D_Brush.rect.XMin; i < P3D_Brush.rect.XMax; i++)
			{
				for (int j = P3D_Brush.rect.YMin; j < P3D_Brush.rect.YMax; j++)
				{
					if (P3D_Brush.IsInsideShape(P3D_Brush.inverse, i, j, ref vector))
					{
						Color pixel = P3D_Brush.canvas.GetPixel(i, j);
						Color color = P3D_Brush.color;
						if (P3D_Brush.shape != null)
						{
							color *= P3D_Brush.shape.GetPixelBilinear(vector.x, vector.y);
						}
						if (P3D_Brush.detail != null)
						{
							color *= P3D_Brush.SampleRepeat(P3D_Brush.detail, num * (float)i, num2 * (float)j);
						}
						P3D_Brush.canvas.SetPixel(i, j, P3D_Brush.AdditiveBlend.Blend(pixel, color));
					}
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00060F10 File Offset: 0x0005F110
		private static Color Blend(Color old, Color add)
		{
			old.r += add.r;
			old.g += add.g;
			old.b += add.b;
			old.a += add.a;
			return old;
		}
	}

	// Token: 0x0200004B RID: 75
	[NullableContext(0)]
	private static class AlphaBlend
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00060F64 File Offset: 0x0005F164
		public static void Paint()
		{
			Vector2 vector = default(Vector2);
			float num = P3D_Helper.Reciprocal((float)P3D_Brush.canvasW * P3D_Brush.detailScale.x);
			float num2 = P3D_Helper.Reciprocal((float)P3D_Brush.canvasH * P3D_Brush.detailScale.y);
			P3D_Brush.color.a = P3D_Brush.color.a * P3D_Brush.opacity;
			for (int i = P3D_Brush.rect.XMin; i < P3D_Brush.rect.XMax; i++)
			{
				for (int j = P3D_Brush.rect.YMin; j < P3D_Brush.rect.YMax; j++)
				{
					if (P3D_Brush.IsInsideShape(P3D_Brush.inverse, i, j, ref vector))
					{
						Color pixel = P3D_Brush.canvas.GetPixel(i, j);
						Color color = P3D_Brush.color;
						if (P3D_Brush.shape != null)
						{
							color *= P3D_Brush.shape.GetPixelBilinear(vector.x, vector.y);
						}
						if (P3D_Brush.detail != null)
						{
							color *= P3D_Brush.SampleRepeat(P3D_Brush.detail, num * (float)i, num2 * (float)j);
						}
						P3D_Brush.canvas.SetPixel(i, j, P3D_Brush.AlphaBlend.Blend(pixel, color));
					}
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0006109C File Offset: 0x0005F29C
		private static Color Blend(Color old, Color add)
		{
			if (add.a > 0f)
			{
				float a = add.a;
				float num = 1f - a;
				float a2 = old.a;
				float num2 = a + a2 * num;
				old.r = (add.r * a + old.r * a2 * num) / num2;
				old.g = (add.g * a + old.g * a2 * num) / num2;
				old.b = (add.b * a + old.b * a2 * num) / num2;
				old.a = num2;
			}
			return old;
		}
	}

	// Token: 0x0200004C RID: 76
	[NullableContext(0)]
	private static class AlphaBlendRGB
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00061130 File Offset: 0x0005F330
		public static void Paint()
		{
			Vector2 vector = default(Vector2);
			float num = P3D_Helper.Reciprocal((float)P3D_Brush.canvasW * P3D_Brush.detailScale.x);
			float num2 = P3D_Helper.Reciprocal((float)P3D_Brush.canvasH * P3D_Brush.detailScale.y);
			P3D_Brush.color.a = P3D_Brush.color.a * P3D_Brush.opacity;
			for (int i = P3D_Brush.rect.XMin; i < P3D_Brush.rect.XMax; i++)
			{
				for (int j = P3D_Brush.rect.YMin; j < P3D_Brush.rect.YMax; j++)
				{
					if (P3D_Brush.IsInsideShape(P3D_Brush.inverse, i, j, ref vector))
					{
						Color pixel = P3D_Brush.canvas.GetPixel(i, j);
						Color color = P3D_Brush.color;
						if (P3D_Brush.shape != null)
						{
							color *= P3D_Brush.shape.GetPixelBilinear(vector.x, vector.y);
						}
						if (P3D_Brush.detail != null)
						{
							color *= P3D_Brush.SampleRepeat(P3D_Brush.detail, num * (float)i, num2 * (float)j);
						}
						P3D_Brush.canvas.SetPixel(i, j, P3D_Brush.AlphaBlendRGB.Blend(pixel, color));
					}
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00061268 File Offset: 0x0005F468
		private static Color Blend(Color old, Color add)
		{
			if (old.a > 0f && add.a > 0f)
			{
				float a = add.a;
				float num = 1f - a;
				float a2 = old.a;
				float num2 = a + a2 * num;
				old.r = (add.r * a + old.r * a2 * num) / num2;
				old.g = (add.g * a + old.g * a2 * num) / num2;
				old.b = (add.b * a + old.b * a2 * num) / num2;
			}
			return old;
		}
	}

	// Token: 0x0200004D RID: 77
	[NullableContext(0)]
	private static class AlphaErase
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00061300 File Offset: 0x0005F500
		public static void Paint()
		{
			Vector2 vector = default(Vector2);
			float num = P3D_Helper.Reciprocal((float)P3D_Brush.canvasW * P3D_Brush.detailScale.x);
			float num2 = P3D_Helper.Reciprocal((float)P3D_Brush.canvasH * P3D_Brush.detailScale.y);
			P3D_Brush.color.a = P3D_Brush.color.a * P3D_Brush.opacity;
			for (int i = P3D_Brush.rect.XMin; i < P3D_Brush.rect.XMax; i++)
			{
				for (int j = P3D_Brush.rect.YMin; j < P3D_Brush.rect.YMax; j++)
				{
					if (P3D_Brush.IsInsideShape(P3D_Brush.inverse, i, j, ref vector))
					{
						Color pixel = P3D_Brush.canvas.GetPixel(i, j);
						float num3 = P3D_Brush.opacity;
						if (P3D_Brush.shape != null)
						{
							num3 *= P3D_Brush.shape.GetPixelBilinear(vector.x, vector.y).a;
						}
						if (P3D_Brush.detail != null)
						{
							num3 *= P3D_Brush.SampleRepeat(P3D_Brush.detail, num * (float)i, num2 * (float)j).a;
						}
						P3D_Brush.canvas.SetPixel(i, j, P3D_Brush.AlphaErase.Blend(pixel, num3));
					}
				}
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005F7F File Offset: 0x0000417F
		private static Color Blend(Color old, float sub)
		{
			old.a -= sub;
			return old;
		}
	}

	// Token: 0x0200004E RID: 78
	[NullableContext(0)]
	private static class NormalBlend
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0006143C File Offset: 0x0005F63C
		public static void Paint()
		{
			Vector2 vector = default(Vector2);
			float num = P3D_Helper.Reciprocal((float)P3D_Brush.canvasW * P3D_Brush.detailScale.x);
			float num2 = P3D_Helper.Reciprocal((float)P3D_Brush.canvasH * P3D_Brush.detailScale.y);
			P3D_Brush.color.a = P3D_Brush.color.a * P3D_Brush.opacity;
			if (P3D_Brush.shape != null && P3D_Brush.shape.format != TextureFormat.Alpha8)
			{
				for (int i = P3D_Brush.rect.XMin; i < P3D_Brush.rect.XMax; i++)
				{
					for (int j = P3D_Brush.rect.YMin; j < P3D_Brush.rect.YMax; j++)
					{
						if (P3D_Brush.IsInsideShape(P3D_Brush.inverse, i, j, ref vector))
						{
							Vector3 vector2 = P3D_Brush.NormalBlend.ColorToNormalXY(P3D_Brush.canvas.GetPixel(i, j));
							Vector3 vector3 = P3D_Brush.NormalBlend.ColorToNormalXY(P3D_Brush.shape.GetPixelBilinear(vector.x, vector.y));
							if (P3D_Brush.detail != null)
							{
								Vector3 vector4 = P3D_Brush.NormalBlend.ColorToNormalXY(P3D_Brush.SampleRepeat(P3D_Brush.detail, num * (float)i, num2 * (float)j));
								vector3 = P3D_Brush.NormalBlend.CombineNormalsXY(vector3, vector4);
							}
							vector2 = P3D_Brush.NormalBlend.CombineNormalsXY(vector2, vector3, P3D_Brush.opacity);
							vector2 = P3D_Brush.NormalBlend.ComputeZ(vector2);
							vector2 = Vector3.Normalize(vector2);
							P3D_Brush.canvas.SetPixel(i, j, P3D_Brush.NormalBlend.NormalToColor(vector2));
						}
					}
				}
				return;
			}
			for (int k = P3D_Brush.rect.XMin; k < P3D_Brush.rect.XMax; k++)
			{
				for (int l = P3D_Brush.rect.YMin; l < P3D_Brush.rect.YMax; l++)
				{
					if (P3D_Brush.IsInsideShape(P3D_Brush.inverse, k, l, ref vector))
					{
						Vector3 vector5 = P3D_Brush.NormalBlend.ColorToNormalXY(P3D_Brush.canvas.GetPixel(k, l));
						Vector2 vector6 = P3D_Brush.direction;
						float num3 = P3D_Brush.opacity;
						if (P3D_Brush.shape != null)
						{
							num3 *= P3D_Brush.shape.GetPixelBilinear(vector.x, vector.y).a;
						}
						if (P3D_Brush.detail != null)
						{
							Vector3 vector7 = P3D_Brush.NormalBlend.ColorToNormalXY(P3D_Brush.SampleRepeat(P3D_Brush.detail, num * (float)k, num2 * (float)l));
							vector6 = P3D_Brush.NormalBlend.CombineNormalsXY(vector6, vector7);
						}
						vector5 = P3D_Brush.NormalBlend.CombineNormalsXY(vector5, vector6, num3);
						vector5 = P3D_Brush.NormalBlend.ComputeZ(vector5);
						vector5 = Vector3.Normalize(vector5);
						P3D_Brush.canvas.SetPixel(k, l, P3D_Brush.NormalBlend.NormalToColor(vector5));
					}
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000616D4 File Offset: 0x0005F8D4
		private static Vector3 ColorToNormalXY(Color c)
		{
			return new Vector3
			{
				x = c.r * 2f - 1f,
				y = c.g * 2f - 1f
			};
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0006171C File Offset: 0x0005F91C
		private static Color NormalToColor(Vector3 n)
		{
			Color color = default(Color);
			color.r = n.x * 0.5f + 0.5f;
			color.g = n.y * 0.5f + 0.5f;
			color.b = n.z * 0.5f + 0.5f;
			color.a = color.r;
			return color;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005F8E File Offset: 0x0000418E
		private static Vector3 ComputeZ(Vector3 a)
		{
			a.z = Mathf.Sqrt(1f - a.x * a.x + a.y * a.y);
			return a;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005FBE File Offset: 0x000041BE
		private static Vector3 CombineNormalsXY(Vector3 a, Vector3 b)
		{
			a.x += b.x;
			a.y += b.y;
			return a;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005FE3 File Offset: 0x000041E3
		private static Vector3 CombineNormalsXY(Vector3 a, Vector3 b, float c)
		{
			a.x += b.x * c;
			a.y += b.y * c;
			return a;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000600C File Offset: 0x0000420C
		private static Vector3 CombineNormalsXY(Vector3 a, Vector2 b, float c)
		{
			a.x += b.x * c;
			a.y += b.y * c;
			return a;
		}
	}

	// Token: 0x0200004F RID: 79
	[NullableContext(0)]
	private static class Replace
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0006178C File Offset: 0x0005F98C
		public static void Paint()
		{
			Vector2 vector = default(Vector2);
			float num = P3D_Helper.Reciprocal((float)P3D_Brush.canvasW * P3D_Brush.detailScale.x);
			float num2 = P3D_Helper.Reciprocal((float)P3D_Brush.canvasH * P3D_Brush.detailScale.y);
			P3D_Brush.color.a = P3D_Brush.color.a * P3D_Brush.opacity;
			for (int i = P3D_Brush.rect.XMin; i < P3D_Brush.rect.XMax; i++)
			{
				for (int j = P3D_Brush.rect.YMin; j < P3D_Brush.rect.YMax; j++)
				{
					if (P3D_Brush.IsInsideShape(P3D_Brush.inverse, i, j, ref vector))
					{
						Color pixel = P3D_Brush.canvas.GetPixel(i, j);
						Color color = P3D_Brush.color;
						float num3 = P3D_Brush.opacity;
						if (P3D_Brush.shape != null)
						{
							num3 *= P3D_Brush.shape.GetPixelBilinear(vector.x, vector.y).a;
						}
						if (P3D_Brush.detail != null)
						{
							color *= P3D_Brush.SampleRepeat(P3D_Brush.detail, num * (float)i, num2 * (float)j);
						}
						P3D_Brush.canvas.SetPixel(i, j, Color.Lerp(pixel, color, num3));
					}
				}
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0006109C File Offset: 0x0005F29C
		private static Color Blend(Color old, Color add)
		{
			if (add.a > 0f)
			{
				float a = add.a;
				float num = 1f - a;
				float a2 = old.a;
				float num2 = a + a2 * num;
				old.r = (add.r * a + old.r * a2 * num) / num2;
				old.g = (add.g * a + old.g * a2 * num) / num2;
				old.b = (add.b * a + old.b * a2 * num) / num2;
				old.a = num2;
			}
			return old;
		}
	}

	// Token: 0x02000050 RID: 80
	[NullableContext(0)]
	private static class SubtractiveBlend
	{
		// Token: 0x0600013B RID: 315 RVA: 0x000618D0 File Offset: 0x0005FAD0
		public static void Paint()
		{
			Vector2 vector = default(Vector2);
			float num = P3D_Helper.Reciprocal((float)P3D_Brush.canvasW * P3D_Brush.detailScale.x);
			float num2 = P3D_Helper.Reciprocal((float)P3D_Brush.canvasH * P3D_Brush.detailScale.y);
			P3D_Brush.color.a = P3D_Brush.color.a * P3D_Brush.opacity;
			for (int i = P3D_Brush.rect.XMin; i < P3D_Brush.rect.XMax; i++)
			{
				for (int j = P3D_Brush.rect.YMin; j < P3D_Brush.rect.YMax; j++)
				{
					if (P3D_Brush.IsInsideShape(P3D_Brush.inverse, i, j, ref vector))
					{
						Color pixel = P3D_Brush.canvas.GetPixel(i, j);
						Color color = P3D_Brush.color;
						if (P3D_Brush.shape != null)
						{
							color *= P3D_Brush.shape.GetPixelBilinear(vector.x, vector.y);
						}
						if (P3D_Brush.detail != null)
						{
							color *= P3D_Brush.SampleRepeat(P3D_Brush.detail, num * (float)i, num2 * (float)j);
						}
						P3D_Brush.canvas.SetPixel(i, j, P3D_Brush.SubtractiveBlend.Blend(pixel, color));
					}
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00061A08 File Offset: 0x0005FC08
		private static Color Blend(Color old, Color sub)
		{
			old.r -= sub.r;
			old.g -= sub.g;
			old.b -= sub.b;
			old.a -= sub.a;
			return old;
		}
	}
}
