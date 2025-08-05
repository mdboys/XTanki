using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MeshBrush
{
	// Token: 0x020029AB RID: 10667
	[NullableContext(1)]
	[Nullable(0)]
	[ExecuteInEditMode]
	public class MeshBrush : MonoBehaviour
	{
		// Token: 0x06008F7B RID: 36731 RVA: 0x0013A738 File Offset: 0x00138938
		private void OnDestroy()
		{
			if (this.deletionBuffer.Count > 0)
			{
				for (int i = 0; i < this.deletionBuffer.Count; i++)
				{
					if (this.deletionBuffer[i])
					{
						global::UnityEngine.Object.DestroyImmediate(this.deletionBuffer[i]);
					}
				}
				this.deletionBuffer.Clear();
			}
			if (this.paintBuffer.Count <= 0)
			{
				return;
			}
			for (int j = 0; j < this.paintBuffer.Count; j++)
			{
				if (this.paintBuffer[j])
				{
					global::UnityEngine.Object.DestroyImmediate(this.paintBuffer[j]);
				}
			}
			this.paintBuffer.Clear();
		}

		// Token: 0x06008F7C RID: 36732 RVA: 0x00055F48 File Offset: 0x00054148
		public void ResetSlopeSettings()
		{
			this.slopeInfluence = 100f;
			this.maxSlopeFilterAngle = 30f;
			this.activeSlopeFilter = false;
			this.inverseSlopeFilter = false;
			this.manualRefVecSampling = false;
			this.showRefVecInSceneGUI = true;
		}

		// Token: 0x06008F7D RID: 36733 RVA: 0x0013A7EC File Offset: 0x001389EC
		public void ResetRandomizers()
		{
			this.rScale = 0f;
			this.rScaleW = 0f;
			this.rScaleH = 0f;
			this.rRot = 0f;
			this.rUniformRange = Vector2.zero;
			this.rNonUniformRange = Vector4.zero;
		}

		// Token: 0x0400606A RID: 24682
		[HideInInspector]
		public bool isActive = true;

		// Token: 0x0400606B RID: 24683
		[HideInInspector]
		public GameObject brush;

		// Token: 0x0400606C RID: 24684
		[HideInInspector]
		public Transform holderObj;

		// Token: 0x0400606D RID: 24685
		[HideInInspector]
		public Transform brushTransform;

		// Token: 0x0400606E RID: 24686
		[HideInInspector]
		public string groupName = "<group name>";

		// Token: 0x0400606F RID: 24687
		public GameObject[] setOfMeshesToPaint = new GameObject[1];

		// Token: 0x04006070 RID: 24688
		[HideInInspector]
		public List<GameObject> paintBuffer = new List<GameObject>();

		// Token: 0x04006071 RID: 24689
		[HideInInspector]
		public List<GameObject> deletionBuffer = new List<GameObject>();

		// Token: 0x04006072 RID: 24690
		[HideInInspector]
		public KeyCode paintKey = KeyCode.P;

		// Token: 0x04006073 RID: 24691
		[HideInInspector]
		public KeyCode deleteKey = KeyCode.L;

		// Token: 0x04006074 RID: 24692
		[HideInInspector]
		public KeyCode combineAreaKey = KeyCode.K;

		// Token: 0x04006075 RID: 24693
		[HideInInspector]
		public KeyCode increaseRadiusKey = KeyCode.O;

		// Token: 0x04006076 RID: 24694
		[HideInInspector]
		public KeyCode decreaseRadiusKey = KeyCode.I;

		// Token: 0x04006077 RID: 24695
		[HideInInspector]
		public float hRadius = 0.3f;

		// Token: 0x04006078 RID: 24696
		[HideInInspector]
		public Color hColor = Color.white;

		// Token: 0x04006079 RID: 24697
		[HideInInspector]
		public int meshCount = 1;

		// Token: 0x0400607A RID: 24698
		[HideInInspector]
		public bool useRandomMeshCount;

		// Token: 0x0400607B RID: 24699
		[HideInInspector]
		public int minNrOfMeshes = 1;

		// Token: 0x0400607C RID: 24700
		[HideInInspector]
		public int maxNrOfMeshes = 1;

		// Token: 0x0400607D RID: 24701
		[HideInInspector]
		public float delay = 0.25f;

		// Token: 0x0400607E RID: 24702
		[HideInInspector]
		public float meshOffset;

		// Token: 0x0400607F RID: 24703
		[HideInInspector]
		public float slopeInfluence = 100f;

		// Token: 0x04006080 RID: 24704
		[HideInInspector]
		public bool activeSlopeFilter;

		// Token: 0x04006081 RID: 24705
		[HideInInspector]
		public float maxSlopeFilterAngle = 30f;

		// Token: 0x04006082 RID: 24706
		[HideInInspector]
		public bool inverseSlopeFilter;

		// Token: 0x04006083 RID: 24707
		[HideInInspector]
		public bool manualRefVecSampling;

		// Token: 0x04006084 RID: 24708
		[HideInInspector]
		public bool showRefVecInSceneGUI = true;

		// Token: 0x04006085 RID: 24709
		[HideInInspector]
		public Vector3 slopeRefVec = Vector3.up;

		// Token: 0x04006086 RID: 24710
		[HideInInspector]
		public Vector3 slopeRefVec_HandleLocation = Vector3.zero;

		// Token: 0x04006087 RID: 24711
		[HideInInspector]
		public bool yAxisIsTangent;

		// Token: 0x04006088 RID: 24712
		[HideInInspector]
		public bool invertY;

		// Token: 0x04006089 RID: 24713
		[HideInInspector]
		public float scattering = 60f;

		// Token: 0x0400608A RID: 24714
		[HideInInspector]
		public bool autoStatic = true;

		// Token: 0x0400608B RID: 24715
		[HideInInspector]
		public bool uniformScale = true;

		// Token: 0x0400608C RID: 24716
		[HideInInspector]
		public bool constUniformScale = true;

		// Token: 0x0400608D RID: 24717
		[HideInInspector]
		public bool rWithinRange;

		// Token: 0x0400608E RID: 24718
		[HideInInspector]
		public bool b_CustomKeys;

		// Token: 0x0400608F RID: 24719
		[HideInInspector]
		public bool b_Slopes = true;

		// Token: 0x04006090 RID: 24720
		[HideInInspector]
		public bool b_Randomizers = true;

		// Token: 0x04006091 RID: 24721
		[HideInInspector]
		public bool b_AdditiveScale = true;

		// Token: 0x04006092 RID: 24722
		[HideInInspector]
		public bool b_opt = true;

		// Token: 0x04006093 RID: 24723
		[HideInInspector]
		public float rScaleW;

		// Token: 0x04006094 RID: 24724
		[HideInInspector]
		public float rScaleH;

		// Token: 0x04006095 RID: 24725
		[HideInInspector]
		public float rScale;

		// Token: 0x04006096 RID: 24726
		[HideInInspector]
		public Vector2 rUniformRange = Vector2.zero;

		// Token: 0x04006097 RID: 24727
		[HideInInspector]
		public Vector4 rNonUniformRange = Vector4.zero;

		// Token: 0x04006098 RID: 24728
		[HideInInspector]
		public float cScale;

		// Token: 0x04006099 RID: 24729
		[HideInInspector]
		public Vector3 cScaleXYZ = Vector3.zero;

		// Token: 0x0400609A RID: 24730
		[HideInInspector]
		public float rRot;

		// Token: 0x0400609B RID: 24731
		[HideInInspector]
		public bool autoSelectOnCombine = true;
	}
}
