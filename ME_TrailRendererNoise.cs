using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000046 RID: 70
[NullableContext(1)]
[Nullable(0)]
public class ME_TrailRendererNoise : MonoBehaviour
{
	// Token: 0x06000111 RID: 273 RVA: 0x00060198 File Offset: 0x0005E398
	private void Start()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.lineRenderer.useWorldSpace = true;
		this.t = base.transform;
		this.prevPos = this.t.position;
		this.points.Insert(0, this.t.position);
		this.lifeTimes.Insert(0, this.VertexTime);
		this.velocities.Insert(0, Vector3.zero);
		this.randomOffset = (float)global::UnityEngine.Random.Range(0, 10000000) / 1000000f;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0006022C File Offset: 0x0005E42C
	private void Update()
	{
		if (this.IsActive)
		{
			this.AddNewPoints();
		}
		this.UpdatetPoints();
		if (this.SmoothCurves && this.points.Count > 2)
		{
			this.UpdateLineRendererBezier();
		}
		else
		{
			this.UpdateLineRenderer();
		}
		if (this.AutodestructWhenNotActive && !this.IsActive && this.points.Count <= 1)
		{
			global::UnityEngine.Object.Destroy(base.gameObject, this.TotalLifeTime);
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00005EE1 File Offset: 0x000040E1
	private void OnEnable()
	{
		this.points.Clear();
		this.lifeTimes.Clear();
		this.velocities.Clear();
	}

	// Token: 0x06000114 RID: 276 RVA: 0x000602A0 File Offset: 0x0005E4A0
	private void AddNewPoints()
	{
		if ((this.t.position - this.prevPos).magnitude > this.MinVertexDistance || (this.IsRibbon && this.points.Count == 0) || (this.IsRibbon && this.points.Count > 0 && (this.t.position - this.points[0]).magnitude > this.MinVertexDistance))
		{
			this.prevPos = this.t.position;
			this.points.Insert(0, this.t.position);
			this.lifeTimes.Insert(0, this.VertexTime);
			this.velocities.Insert(0, Vector3.zero);
		}
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00060378 File Offset: 0x0005E578
	private void UpdatetPoints()
	{
		for (int i = 0; i < this.lifeTimes.Count; i++)
		{
			List<float> list = this.lifeTimes;
			int num = i;
			list[num] -= Time.deltaTime;
			if (this.lifeTimes[i] <= 0f)
			{
				int num2 = this.lifeTimes.Count - i;
				this.lifeTimes.RemoveRange(i, num2);
				this.points.RemoveRange(i, num2);
				this.velocities.RemoveRange(i, num2);
				return;
			}
			this.CalculateTurbuelence(this.points[i], this.TimeScale, this.Frequency, this.Amplitude, this.Gravity, i);
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00060434 File Offset: 0x0005E634
	private void UpdateLineRendererBezier()
	{
		if (this.SmoothCurves && this.points.Count > 2)
		{
			this.InterpolateBezier(this.points, 0.5f);
			List<Vector3> drawingPoints = this.GetDrawingPoints();
			this.lineRenderer.positionCount = drawingPoints.Count - 1;
			this.lineRenderer.SetPositions(drawingPoints.ToArray());
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00005F04 File Offset: 0x00004104
	private void UpdateLineRenderer()
	{
		this.lineRenderer.positionCount = Mathf.Clamp(this.points.Count - 1, 0, int.MaxValue);
		this.lineRenderer.SetPositions(this.points.ToArray());
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00060494 File Offset: 0x0005E694
	private void CalculateTurbuelence(Vector3 position, float speed, float scale, float height, float gravity, int index)
	{
		float num = Time.timeSinceLevelLoad * speed + this.randomOffset;
		float num2 = position.x * scale + num;
		float num3 = position.y * scale + num + 10f;
		float num4 = position.z * scale + num + 25f;
		position.x = (Mathf.PerlinNoise(num3, num4) - 0.5f) * height * Time.deltaTime;
		position.y = (Mathf.PerlinNoise(num2, num4) - 0.5f) * height * Time.deltaTime - gravity * Time.deltaTime;
		position.z = (Mathf.PerlinNoise(num2, num3) - 0.5f) * height * Time.deltaTime;
		List<Vector3> list = this.points;
		list[index] += position * this.TurbulenceStrength;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0006056C File Offset: 0x0005E76C
	public void InterpolateBezier(List<Vector3> segmentPoints, float scale)
	{
		this.controlPoints.Clear();
		if (segmentPoints.Count < 2)
		{
			return;
		}
		for (int i = 0; i < segmentPoints.Count; i++)
		{
			if (i == 0)
			{
				Vector3 vector = segmentPoints[i];
				Vector3 vector2 = segmentPoints[i + 1] - vector;
				Vector3 vector3 = vector + scale * vector2;
				this.controlPoints.Add(vector);
				this.controlPoints.Add(vector3);
			}
			else if (i == segmentPoints.Count - 1)
			{
				Vector3 vector4 = segmentPoints[i - 1];
				Vector3 vector5 = segmentPoints[i];
				Vector3 vector6 = vector5 - vector4;
				Vector3 vector7 = vector5 - scale * vector6;
				this.controlPoints.Add(vector7);
				this.controlPoints.Add(vector5);
			}
			else
			{
				Vector3 vector8 = segmentPoints[i - 1];
				Vector3 vector9 = segmentPoints[i];
				Vector3 vector10 = segmentPoints[i + 1];
				Vector3 normalized = (vector10 - vector8).normalized;
				Vector3 vector11 = vector9 - scale * normalized * (vector9 - vector8).magnitude;
				Vector3 vector12 = vector9 + scale * normalized * (vector10 - vector9).magnitude;
				this.controlPoints.Add(vector11);
				this.controlPoints.Add(vector9);
				this.controlPoints.Add(vector12);
			}
		}
		this.curveCount = (this.controlPoints.Count - 1) / 3;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00060704 File Offset: 0x0005E904
	public List<Vector3> GetDrawingPoints()
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < this.curveCount; i++)
		{
			List<Vector3> list2 = this.FindDrawingPoints(i);
			if (i != 0)
			{
				list2.RemoveAt(0);
			}
			list.AddRange(list2);
		}
		return list;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00060744 File Offset: 0x0005E944
	private List<Vector3> FindDrawingPoints(int curveIndex)
	{
		List<Vector3> list = new List<Vector3>();
		Vector3 vector = this.CalculateBezierPoint(curveIndex, 0f);
		Vector3 vector2 = this.CalculateBezierPoint(curveIndex, 1f);
		list.Add(vector);
		list.Add(vector2);
		this.FindDrawingPoints(curveIndex, 0f, 1f, list, 1);
		return list;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00060794 File Offset: 0x0005E994
	private int FindDrawingPoints(int curveIndex, float t0, float t1, List<Vector3> pointList, int insertionIndex)
	{
		Vector3 vector = this.CalculateBezierPoint(curveIndex, t0);
		Vector3 vector2 = this.CalculateBezierPoint(curveIndex, t1);
		if ((vector - vector2).sqrMagnitude < 0.01f)
		{
			return 0;
		}
		float num = (t0 + t1) / 2f;
		Vector3 vector3 = this.CalculateBezierPoint(curveIndex, num);
		Vector3 normalized = (vector - vector3).normalized;
		Vector3 normalized2 = (vector2 - vector3).normalized;
		if (Vector3.Dot(normalized, normalized2) > -0.99f || Mathf.Abs(num - 0.5f) < 0.0001f)
		{
			int num2 = 0;
			num2 += this.FindDrawingPoints(curveIndex, t0, num, pointList, insertionIndex);
			pointList.Insert(insertionIndex + num2, vector3);
			num2++;
			return num2 + this.FindDrawingPoints(curveIndex, num, t1, pointList, insertionIndex + num2);
		}
		return 0;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00060860 File Offset: 0x0005EA60
	public Vector3 CalculateBezierPoint(int curveIndex, float t)
	{
		int num = curveIndex * 3;
		Vector3 vector = this.controlPoints[num];
		Vector3 vector2 = this.controlPoints[num + 1];
		Vector3 vector3 = this.controlPoints[num + 2];
		Vector3 vector4 = this.controlPoints[num + 3];
		return this.CalculateBezierPoint(t, vector, vector2, vector3, vector4);
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000608B8 File Offset: 0x0005EAB8
	private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float num = 1f - t;
		float num2 = t * t;
		float num3 = num * num;
		float num4 = num3 * num;
		float num5 = num2 * t;
		return num4 * p0 + 3f * num3 * t * p1 + 3f * num * num2 * p2 + num5 * p3;
	}

	// Token: 0x040000B3 RID: 179
	private const float MinimumSqrDistance = 0.01f;

	// Token: 0x040000B4 RID: 180
	private const float DivisionThreshold = -0.99f;

	// Token: 0x040000B5 RID: 181
	private const float SmoothCurvesScale = 0.5f;

	// Token: 0x040000B6 RID: 182
	[Range(0.01f, 10f)]
	public float MinVertexDistance = 0.1f;

	// Token: 0x040000B7 RID: 183
	public float VertexTime = 1f;

	// Token: 0x040000B8 RID: 184
	public float TotalLifeTime = 3f;

	// Token: 0x040000B9 RID: 185
	public bool SmoothCurves;

	// Token: 0x040000BA RID: 186
	public bool IsRibbon;

	// Token: 0x040000BB RID: 187
	public bool IsActive = true;

	// Token: 0x040000BC RID: 188
	[Range(0.001f, 10f)]
	public float Frequency = 1f;

	// Token: 0x040000BD RID: 189
	[Range(0.001f, 10f)]
	public float TimeScale = 0.1f;

	// Token: 0x040000BE RID: 190
	[Range(0.001f, 10f)]
	public float Amplitude = 1f;

	// Token: 0x040000BF RID: 191
	public float Gravity = 1f;

	// Token: 0x040000C0 RID: 192
	public float TurbulenceStrength = 1f;

	// Token: 0x040000C1 RID: 193
	public bool AutodestructWhenNotActive;

	// Token: 0x040000C2 RID: 194
	private readonly List<Vector3> controlPoints = new List<Vector3>();

	// Token: 0x040000C3 RID: 195
	private int curveCount;

	// Token: 0x040000C4 RID: 196
	private readonly List<float> lifeTimes = new List<float>(500);

	// Token: 0x040000C5 RID: 197
	private LineRenderer lineRenderer;

	// Token: 0x040000C6 RID: 198
	private readonly List<Vector3> points = new List<Vector3>(500);

	// Token: 0x040000C7 RID: 199
	private Vector3 prevPos;

	// Token: 0x040000C8 RID: 200
	private float randomOffset;

	// Token: 0x040000C9 RID: 201
	private Transform t;

	// Token: 0x040000CA RID: 202
	private readonly List<Vector3> velocities = new List<Vector3>(500);
}
