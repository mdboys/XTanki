using System;
using System.Runtime.CompilerServices;
using Edelweiss.DecalSystem;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class DS_Decals : Decals
{
	// Token: 0x060000A2 RID: 162 RVA: 0x00005A80 File Offset: 0x00003C80
	[NullableContext(1)]
	protected override DecalsMeshRenderer AddDecalsMeshRendererComponentToGameObject(GameObject a_GameObject)
	{
		return a_GameObject.AddComponent<DS_DecalsMeshRenderer>();
	}
}
