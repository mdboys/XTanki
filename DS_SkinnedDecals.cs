using System;
using System.Runtime.CompilerServices;
using Edelweiss.DecalSystem;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class DS_SkinnedDecals : SkinnedDecals
{
	// Token: 0x060000A7 RID: 167 RVA: 0x00005AA8 File Offset: 0x00003CA8
	[NullableContext(1)]
	protected override SkinnedDecalsMeshRenderer AddSkinnedDecalsMeshRendererComponentToGameObject(GameObject a_GameObject)
	{
		return a_GameObject.AddComponent<DS_SkinnedDecalsMeshRenderer>();
	}
}
