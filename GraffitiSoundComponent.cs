using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using UnityEngine;

// Token: 0x02000033 RID: 51
[NullableContext(1)]
[Nullable(0)]
public class GraffitiSoundComponent : MonoBehaviour, Platform.Kernel.ECS.ClientEntitySystem.API.Component
{
	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005BB8 File Offset: 0x00003DB8
	public AudioSource Sound
	{
		get
		{
			return this.sound;
		}
	}

	// Token: 0x0400006E RID: 110
	[SerializeField]
	private AudioSource sound;
}
