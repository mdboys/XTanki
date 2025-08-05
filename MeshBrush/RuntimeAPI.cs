using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020029AD RID: 10669
	[NullableContext(1)]
	[Nullable(0)]
	public class RuntimeAPI : MonoBehaviour
	{
		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06008F81 RID: 36737 RVA: 0x00055F7C File Offset: 0x0005417C
		// (set) Token: 0x06008F82 RID: 36738 RVA: 0x00055F84 File Offset: 0x00054184
		public GameObject[] setOfMeshesToPaint { get; set; }

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06008F83 RID: 36739 RVA: 0x00055F8D File Offset: 0x0005418D
		// (set) Token: 0x06008F84 RID: 36740 RVA: 0x00055F95 File Offset: 0x00054195
		public ushort amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				this._amount = (ushort)Mathf.Clamp((int)value, 1, 100);
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06008F85 RID: 36741 RVA: 0x00055FA7 File Offset: 0x000541A7
		// (set) Token: 0x06008F86 RID: 36742 RVA: 0x00055FAF File Offset: 0x000541AF
		public float delayBetweenPaintStrokes
		{
			get
			{
				return this._delayBetweenPaintStrokes;
			}
			set
			{
				this._delayBetweenPaintStrokes = Mathf.Clamp(value, 0.1f, 1f);
			}
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06008F87 RID: 36743 RVA: 0x00055FC7 File Offset: 0x000541C7
		// (set) Token: 0x06008F88 RID: 36744 RVA: 0x00055FCF File Offset: 0x000541CF
		public float brushRadius
		{
			get
			{
				return this._brushRadius;
			}
			set
			{
				this._brushRadius = value;
				if (this._brushRadius <= 0.1f)
				{
					this._brushRadius = 0.1f;
				}
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06008F89 RID: 36745 RVA: 0x00055FF0 File Offset: 0x000541F0
		// (set) Token: 0x06008F8A RID: 36746 RVA: 0x00055FF8 File Offset: 0x000541F8
		public float meshOffset { get; set; }

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06008F8B RID: 36747 RVA: 0x00056001 File Offset: 0x00054201
		// (set) Token: 0x06008F8C RID: 36748 RVA: 0x00056009 File Offset: 0x00054209
		public float scattering { get; set; }

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06008F8D RID: 36749 RVA: 0x00056012 File Offset: 0x00054212
		// (set) Token: 0x06008F8E RID: 36750 RVA: 0x0005601A File Offset: 0x0005421A
		public bool yAxisIsTangent { get; set; }

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06008F8F RID: 36751 RVA: 0x00056023 File Offset: 0x00054223
		// (set) Token: 0x06008F90 RID: 36752 RVA: 0x0005602B File Offset: 0x0005422B
		public float slopeInfluence
		{
			get
			{
				return this._slopeInfluence;
			}
			set
			{
				this._slopeInfluence = Mathf.Clamp(value, 0f, 100f);
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06008F91 RID: 36753 RVA: 0x00056043 File Offset: 0x00054243
		// (set) Token: 0x06008F92 RID: 36754 RVA: 0x0005604B File Offset: 0x0005424B
		public bool activeSlopeFilter { get; set; }

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06008F93 RID: 36755 RVA: 0x00056054 File Offset: 0x00054254
		// (set) Token: 0x06008F94 RID: 36756 RVA: 0x0005605C File Offset: 0x0005425C
		public float maxSlopeFilterAngle
		{
			get
			{
				return this._maxSlopeFilterAngle;
			}
			set
			{
				this._maxSlopeFilterAngle = Mathf.Clamp(value, 0f, 180f);
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06008F95 RID: 36757 RVA: 0x00056074 File Offset: 0x00054274
		// (set) Token: 0x06008F96 RID: 36758 RVA: 0x0005607C File Offset: 0x0005427C
		public bool inverseSlopeFilter { get; set; }

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06008F97 RID: 36759 RVA: 0x00056085 File Offset: 0x00054285
		// (set) Token: 0x06008F98 RID: 36760 RVA: 0x0005608D File Offset: 0x0005428D
		public bool manualRefVecSampling { get; set; }

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06008F99 RID: 36761 RVA: 0x00056096 File Offset: 0x00054296
		// (set) Token: 0x06008F9A RID: 36762 RVA: 0x0005609E File Offset: 0x0005429E
		public Vector3 sampledSlopeRefVector { get; set; }

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06008F9B RID: 36763 RVA: 0x000560A7 File Offset: 0x000542A7
		// (set) Token: 0x06008F9C RID: 36764 RVA: 0x000560AF File Offset: 0x000542AF
		public float randomRotation { get; set; }

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06008F9D RID: 36765 RVA: 0x000560B8 File Offset: 0x000542B8
		// (set) Token: 0x06008F9E RID: 36766 RVA: 0x0013A978 File Offset: 0x00138B78
		public Vector4 randomScale
		{
			get
			{
				return this._randomScale;
			}
			set
			{
				this._randomScale = new Vector4(Mathf.Clamp(value.x, 0.01f, value.x), Mathf.Clamp(value.y, 0.01f, value.y), Mathf.Clamp(value.z, 0.01f, value.z), Mathf.Clamp(value.w, 0.01f, value.w));
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06008F9F RID: 36767 RVA: 0x000560C0 File Offset: 0x000542C0
		// (set) Token: 0x06008FA0 RID: 36768 RVA: 0x000560C8 File Offset: 0x000542C8
		public Vector3 additiveScale { get; set; }

		// Token: 0x06008FA1 RID: 36769 RVA: 0x000560D1 File Offset: 0x000542D1
		private void Start()
		{
			this.thisTransform = base.transform;
			this.scattering = 75f;
		}

		// Token: 0x06008FA2 RID: 36770 RVA: 0x0013A9E8 File Offset: 0x00138BE8
		public void Paint_SingleMesh(RaycastHit paintHit)
		{
			if (!paintHit.collider.transform.Find("Holder"))
			{
				this.holder = new GameObject("Holder");
				this.holderTransform = this.holder.transform;
				this.holderTransform.position = paintHit.collider.transform.position;
				this.holderTransform.rotation = paintHit.collider.transform.rotation;
				this.holderTransform.parent = paintHit.collider.transform;
			}
			this.slopeAngle = (this.activeSlopeFilter ? Vector3.Angle(paintHit.normal, (!this.manualRefVecSampling) ? Vector3.up : this.sampledSlopeRefVector) : ((!this.inverseSlopeFilter) ? 0f : 180f));
			if ((!this.inverseSlopeFilter) ? (this.slopeAngle < this.maxSlopeFilterAngle) : (this.slopeAngle > this.maxSlopeFilterAngle))
			{
				this.paintedMesh = global::UnityEngine.Object.Instantiate<GameObject>(this.setOfMeshesToPaint[global::UnityEngine.Random.Range(0, this.setOfMeshesToPaint.Length)], paintHit.point, Quaternion.LookRotation(paintHit.normal));
				this.paintedMeshTransform = this.paintedMesh.transform;
				if (this.yAxisIsTangent)
				{
					this.paintedMeshTransform.up = Vector3.Lerp(this.paintedMeshTransform.up, this.paintedMeshTransform.forward, this.slopeInfluence * 0.01f);
				}
				else
				{
					this.paintedMeshTransform.up = Vector3.Lerp(Vector3.up, this.paintedMeshTransform.forward, this.slopeInfluence * 0.01f);
				}
				this.paintedMeshTransform.parent = this.holderTransform;
				this.ApplyRandomScale(this.paintedMesh);
				this.ApplyRandomRotation(this.paintedMesh);
				this.ApplyMeshOffset(this.paintedMesh, this.hit.normal);
			}
		}

		// Token: 0x06008FA3 RID: 36771 RVA: 0x0013ABE0 File Offset: 0x00138DE0
		public void Paint_MultipleMeshes(RaycastHit paintHit)
		{
			this.scatteringInsetThreshold = this.brushRadius * 0.01f * this.scattering;
			if (this.brushObj == null)
			{
				this.brushObj = new GameObject("Brush");
				this.brushTransform = this.brushObj.transform;
				this.brushTransform.position = this.thisTransform.position;
				this.brushTransform.parent = paintHit.collider.transform;
			}
			if (!paintHit.collider.transform.Find("Holder"))
			{
				this.holder = new GameObject("Holder");
				this.holderTransform = this.holder.transform;
				this.holderTransform.position = paintHit.collider.transform.position;
				this.holderTransform.rotation = paintHit.collider.transform.rotation;
				this.holderTransform.parent = paintHit.collider.transform;
			}
			for (int i = (int)this.amount; i > 0; i--)
			{
				this.brushTransform.position = paintHit.point + paintHit.normal * 0.5f;
				this.brushTransform.rotation = Quaternion.LookRotation(paintHit.normal);
				this.brushTransform.up = this.brushTransform.forward;
				this.brushTransform.Translate(global::UnityEngine.Random.Range((0f - global::UnityEngine.Random.insideUnitCircle.x) * this.scatteringInsetThreshold, global::UnityEngine.Random.insideUnitCircle.x * this.scatteringInsetThreshold), 0f, global::UnityEngine.Random.Range((0f - global::UnityEngine.Random.insideUnitCircle.y) * this.scatteringInsetThreshold, global::UnityEngine.Random.insideUnitCircle.y * this.scatteringInsetThreshold), Space.Self);
				if (Physics.Raycast(this.brushTransform.position, -paintHit.normal, out this.hit, 2.5f))
				{
					this.slopeAngle = (this.activeSlopeFilter ? Vector3.Angle(this.hit.normal, (!this.manualRefVecSampling) ? Vector3.up : this.sampledSlopeRefVector) : ((!this.inverseSlopeFilter) ? 0f : 180f));
					if ((!this.inverseSlopeFilter) ? (this.slopeAngle < this.maxSlopeFilterAngle) : (this.slopeAngle > this.maxSlopeFilterAngle))
					{
						this.paintedMesh = global::UnityEngine.Object.Instantiate<GameObject>(this.setOfMeshesToPaint[global::UnityEngine.Random.Range(0, this.setOfMeshesToPaint.Length)], this.hit.point, Quaternion.LookRotation(this.hit.normal));
						this.paintedMeshTransform = this.paintedMesh.transform;
						if (this.yAxisIsTangent)
						{
							this.paintedMeshTransform.up = Vector3.Lerp(this.paintedMeshTransform.up, this.paintedMeshTransform.forward, this.slopeInfluence * 0.01f);
						}
						else
						{
							this.paintedMeshTransform.up = Vector3.Lerp(Vector3.up, this.paintedMeshTransform.forward, this.slopeInfluence * 0.01f);
						}
						this.paintedMeshTransform.parent = this.holderTransform;
					}
					this.ApplyRandomScale(this.paintedMesh);
					this.ApplyRandomRotation(this.paintedMesh);
					this.ApplyMeshOffset(this.paintedMesh, this.hit.normal);
				}
			}
		}

		// Token: 0x06008FA4 RID: 36772 RVA: 0x0013AF5C File Offset: 0x0013915C
		private void ApplyRandomScale(GameObject sMesh)
		{
			this.randomWidth = global::UnityEngine.Random.Range(this.randomScale.x, this.randomScale.y);
			this.randomHeight = global::UnityEngine.Random.Range(this.randomScale.z, this.randomScale.w);
			sMesh.transform.localScale = new Vector3(this.randomWidth, this.randomHeight, this.randomWidth);
		}

		// Token: 0x06008FA5 RID: 36773 RVA: 0x0013AFD0 File Offset: 0x001391D0
		private void AddConstantScale(GameObject sMesh)
		{
			sMesh.transform.localScale += new Vector3(Mathf.Clamp(this.additiveScale.x, -0.9f, this.additiveScale.x), Mathf.Clamp(this.additiveScale.y, -0.9f, this.additiveScale.y), Mathf.Clamp(this.additiveScale.z, -0.9f, this.additiveScale.z));
		}

		// Token: 0x06008FA6 RID: 36774 RVA: 0x0013B058 File Offset: 0x00139258
		private void ApplyRandomRotation(GameObject rMesh)
		{
			rMesh.transform.Rotate(new Vector3(0f, global::UnityEngine.Random.Range(0f, 3.6f * Mathf.Clamp(this.randomRotation, 0f, 100f)), 0f));
		}

		// Token: 0x06008FA7 RID: 36775 RVA: 0x000560EA File Offset: 0x000542EA
		private void ApplyMeshOffset(GameObject oMesh, Vector3 offsetDirection)
		{
			oMesh.transform.Translate(offsetDirection.normalized * this.meshOffset * 0.01f, Space.World);
		}

		// Token: 0x040060A7 RID: 24743
		private ushort _amount = 1;

		// Token: 0x040060A8 RID: 24744
		private float _brushRadius = 1f;

		// Token: 0x040060A9 RID: 24745
		private float _delayBetweenPaintStrokes = 0.15f;

		// Token: 0x040060AA RID: 24746
		private float _maxSlopeFilterAngle = 30f;

		// Token: 0x040060AB RID: 24747
		private Vector4 _randomScale = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x040060AC RID: 24748
		private float _slopeInfluence = 100f;

		// Token: 0x040060AD RID: 24749
		private GameObject brushObj;

		// Token: 0x040060AE RID: 24750
		private Transform brushTransform;

		// Token: 0x040060AF RID: 24751
		private RaycastHit hit;

		// Token: 0x040060B0 RID: 24752
		private GameObject holder;

		// Token: 0x040060B1 RID: 24753
		private Transform holderTransform;

		// Token: 0x040060B2 RID: 24754
		private GameObject paintedMesh;

		// Token: 0x040060B3 RID: 24755
		private Transform paintedMeshTransform;

		// Token: 0x040060B4 RID: 24756
		private float randomHeight;

		// Token: 0x040060B5 RID: 24757
		private float randomWidth;

		// Token: 0x040060B6 RID: 24758
		private float scatteringInsetThreshold;

		// Token: 0x040060B7 RID: 24759
		private float slopeAngle;

		// Token: 0x040060B8 RID: 24760
		private Transform thisTransform;
	}
}
