using System;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002B03 RID: 11011
	internal class GeometryUtilities
	{
		// Token: 0x0600988F RID: 39055 RVA: 0x0014F4B8 File Offset: 0x0014D6B8
		public static bool IsQuadrangleConvex(Vector3 a_Vertex1, Vector3 a_Vertex2, Vector3 a_Vertex3, Vector3 a_Vertex4)
		{
			Vector3 vector;
			return GeometryUtilities.LineIntersection(a_Vertex1, a_Vertex3, a_Vertex2, a_Vertex4, out vector);
		}

		// Token: 0x06009890 RID: 39056 RVA: 0x0014F4D0 File Offset: 0x0014D6D0
		public static bool LineIntersection(Vector3 a_Line1Start, Vector3 a_Line1End, Vector3 a_Line2Start, Vector3 a_Line2End, out Vector3 a_IntersectionPoint)
		{
			bool flag;
			return GeometryUtilities.LineIntersection(a_Line1Start, a_Line1End, a_Line2Start, a_Line2End, out a_IntersectionPoint, out flag);
		}

		// Token: 0x06009891 RID: 39057 RVA: 0x0014F4EC File Offset: 0x0014D6EC
		public static bool LineIntersection(Vector3 a_Line1Start, Vector3 a_Line1End, Vector3 a_Line2Start, Vector3 a_Line2End, out Vector3 a_IntersectionPoint, out bool a_IsUnique)
		{
			bool flag = false;
			a_IntersectionPoint = Vector3.zero;
			a_IsUnique = false;
			Vector3 vector = a_Line1End - a_Line1Start;
			Vector3 vector2 = a_Line2End - a_Line2Start;
			Vector3 vector3 = a_Line2Start - a_Line1Start;
			Vector3 vector4 = Vector3.Cross(vector, vector2);
			if (Mathf.Approximately(Vector3.Dot(vector3, vector4), 0f))
			{
				float sqrMagnitude = vector4.sqrMagnitude;
				if (Mathf.Approximately(sqrMagnitude, 0f))
				{
					if (Vector3Extension.Approximately(vector, Vector3.zero))
					{
						if (GeometryUtilities.IsPointOnLine(a_Line1Start, a_Line2Start, a_Line2End))
						{
							flag = true;
							a_IsUnique = true;
							a_IntersectionPoint = a_Line1Start;
						}
					}
					else if (Vector3Extension.Approximately(vector2, Vector3.zero))
					{
						if (GeometryUtilities.IsPointOnLine(a_Line2Start, a_Line1Start, a_Line1End))
						{
							flag = true;
							a_IsUnique = true;
							a_IntersectionPoint = a_Line2Start;
						}
					}
					else
					{
						float num = GeometryUtilities.FactorOfPointOnLine(a_Line2Start, a_Line1Start, a_Line1End);
						float num2 = GeometryUtilities.FactorOfPointOnLine(a_Line2End, a_Line1Start, a_Line1End);
						if (num >= 0f && num <= 1f)
						{
							flag = true;
							a_IntersectionPoint = a_Line2Start;
						}
						else if (num2 >= 0f && num2 <= 1f)
						{
							flag = true;
							a_IntersectionPoint = a_Line2End;
						}
						else if ((num < 0f && num2 > 1f) || (num2 < 0f && num > 1f))
						{
							flag = true;
							a_IntersectionPoint = a_Line1Start;
						}
					}
				}
				else
				{
					float num3 = Vector3.Dot(Vector3.Cross(vector3, vector2), vector4) / sqrMagnitude;
					if (num3 >= 0f && num3 <= 1f)
					{
						flag = true;
						a_IntersectionPoint = a_Line1Start + num3 * vector;
						a_IsUnique = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06009892 RID: 39058 RVA: 0x0014F67C File Offset: 0x0014D87C
		private static float FactorOfPointOnLine(Vector3 a_Point, Vector3 a_LineStart, Vector3 a_LineEnd)
		{
			Vector3 vector = a_LineEnd - a_LineStart;
			Vector3 vector2 = vector;
			vector2.x = Mathf.Abs(vector2.x);
			vector2.y = Mathf.Abs(vector2.y);
			vector2.z = Mathf.Abs(vector2.z);
			float num;
			float num2;
			float num3;
			if (vector2.x > vector2.y && vector2.x > vector2.z)
			{
				num = a_Point.x;
				num2 = a_LineStart.x;
				num3 = vector.x;
			}
			else if (vector2.y > vector2.x && vector2.y > vector2.z)
			{
				num = a_Point.y;
				num2 = a_LineStart.y;
				num3 = vector.y;
			}
			else
			{
				num = a_Point.z;
				num2 = a_LineStart.z;
				num3 = vector.z;
			}
			return (num - num2) / num3;
		}

		// Token: 0x06009893 RID: 39059 RVA: 0x0014F750 File Offset: 0x0014D950
		public static bool IsPointOnLine(Vector3 a_Point, Vector3 a_LineStart, Vector3 a_LineEnd)
		{
			bool flag = false;
			Vector3 vector = a_Point - a_LineStart;
			Vector3 vector2 = a_Point - a_LineEnd;
			if (Vector3.Cross(vector, vector2).sqrMagnitude < 1E-10f)
			{
				float num = Vector3.Dot(vector, vector2);
				if (num <= 0f)
				{
					float sqrMagnitude = (a_LineEnd - a_LineStart).sqrMagnitude;
					if (num <= sqrMagnitude)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06009894 RID: 39060 RVA: 0x0014F7B4 File Offset: 0x0014D9B4
		public static Vector3 TriangleNormal(Vector3 a_Vertex1, Vector3 a_Vertex2, Vector3 a_Vertex3)
		{
			Vector3 vector = a_Vertex2 - a_Vertex1;
			Vector3 vector2 = a_Vertex3 - a_Vertex2;
			vector.Normalize();
			vector2.Normalize();
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			vector3.Normalize();
			return vector3;
		}

		// Token: 0x06009895 RID: 39061 RVA: 0x0014F7F0 File Offset: 0x0014D9F0
		public static bool AreLinesParallel(Vector3 a_Line1Start, Vector3 a_Line1End, Vector3 a_Line2Start, Vector3 a_Line2End)
		{
			bool flag = false;
			Vector3 vector = a_Line1End - a_Line1Start;
			Vector3 vector2 = a_Line2End - a_Line2Start;
			vector.Normalize();
			vector2.Normalize();
			if (Mathf.Approximately(Mathf.Abs(Vector3.Dot(vector, vector2)), 0f))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x04006415 RID: 25621
		private const float c_Epsilon = 1E-10f;
	}
}
