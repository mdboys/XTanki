using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AFC RID: 11004
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GenericDecalProjectorBaseComponent : MonoBehaviour
	{
		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x0600980B RID: 38923 RVA: 0x0005ABDA File Offset: 0x00058DDA
		public Transform CachedTransform
		{
			get
			{
				if (this.m_CachedTransform == null)
				{
					this.m_CachedTransform = base.transform;
				}
				return this.m_CachedTransform;
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x0600980C RID: 38924 RVA: 0x0005ABFC File Offset: 0x00058DFC
		// (set) Token: 0x0600980D RID: 38925 RVA: 0x0014D1E0 File Offset: 0x0014B3E0
		public float VertexColorBlending
		{
			get
			{
				return this.m_VertexColorBlending;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("The blend value has to be in [0.0f, 1.0f].");
				}
				this.m_VertexColorBlending = value;
			}
		}

		// Token: 0x0600980E RID: 38926 RVA: 0x0005AC04 File Offset: 0x00058E04
		private void OnEnable()
		{
			this.m_CachedTransform = base.GetComponent<Transform>();
		}

		// Token: 0x0600980F RID: 38927 RVA: 0x001490A0 File Offset: 0x001472A0
		public GenericDecalsBase GetDecalsBase()
		{
			GenericDecalsBase genericDecalsBase = null;
			Transform transform = base.transform;
			while (transform != null && genericDecalsBase == null)
			{
				genericDecalsBase = transform.GetComponent<GenericDecalsBase>();
				transform = transform.parent;
			}
			return genericDecalsBase;
		}

		// Token: 0x06009810 RID: 38928 RVA: 0x0014D218 File Offset: 0x0014B418
		public Bounds WorldBounds()
		{
			Matrix4x4 matrix4x = this.UnscaledLocalToWorldMatrix();
			Vector3 vector = 0.5f * this.CachedTransform.localScale;
			Vector3 vector2 = new Vector3(0f, 0f - Mathf.Abs(vector.y), 0f);
			Vector3 vector3 = matrix4x.MultiplyPoint3x4(Vector3.zero);
			Bounds bounds = new Bounds(vector3, Vector3.zero);
			vector3 = vector2 + new Vector3(vector.x, vector.y, vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(vector.x, vector.y, 0f - vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(vector.x, 0f - vector.y, vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(vector.x, 0f - vector.y, 0f - vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(0f - vector.x, vector.y, vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(0f - vector.x, vector.y, 0f - vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(0f - vector.x, 0f - vector.y, vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			vector3 = vector2 + new Vector3(0f - vector.x, 0f - vector.y, 0f - vector.z);
			vector3 = matrix4x.MultiplyPoint3x4(vector3);
			bounds.Encapsulate(vector3);
			return bounds;
		}

		// Token: 0x06009811 RID: 38929 RVA: 0x0005AC12 File Offset: 0x00058E12
		private Matrix4x4 UnscaledLocalToWorldMatrix()
		{
			return Matrix4x4.TRS(this.CachedTransform.position, this.CachedTransform.rotation, Vector3.one);
		}

		// Token: 0x040063D9 RID: 25561
		public LayerMask affectedLayers = -1;

		// Token: 0x040063DA RID: 25562
		public bool affectInactiveRenderers;

		// Token: 0x040063DB RID: 25563
		public bool affectOtherDecals;

		// Token: 0x040063DC RID: 25564
		public bool skipUnreadableMeshes;

		// Token: 0x040063DD RID: 25565
		public DetailsMode detailsMode;

		// Token: 0x040063DE RID: 25566
		public AffectedDetail[] affectedDetails = new AffectedDetail[0];

		// Token: 0x040063DF RID: 25567
		public float cullingAngle = 90f;

		// Token: 0x040063E0 RID: 25568
		public float meshOffset;

		// Token: 0x040063E1 RID: 25569
		public bool projectAfterOffset;

		// Token: 0x040063E2 RID: 25570
		public float normalsSmoothing;

		// Token: 0x040063E3 RID: 25571
		public int uv1RectangleIndex;

		// Token: 0x040063E4 RID: 25572
		public int uv2RectangleIndex;

		// Token: 0x040063E5 RID: 25573
		public Color vertexColor = Color.white;

		// Token: 0x040063E6 RID: 25574
		[SerializeField]
		private float m_VertexColorBlending;

		// Token: 0x040063E7 RID: 25575
		private Transform m_CachedTransform;
	}
}
