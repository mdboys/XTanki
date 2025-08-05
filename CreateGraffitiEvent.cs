using System;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class CreateGraffitiEvent : Platform.Kernel.ECS.ClientEntitySystem.API.Event
{
	// Token: 0x0600005B RID: 91 RVA: 0x00005814 File Offset: 0x00003A14
	public CreateGraffitiEvent(Vector3 position, Vector3 direction, Vector3 up)
	{
		this.Position = position;
		this.Direction = direction;
		this.Up = up;
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600005C RID: 92 RVA: 0x00005831 File Offset: 0x00003A31
	// (set) Token: 0x0600005D RID: 93 RVA: 0x00005839 File Offset: 0x00003A39
	public Vector3 Position { get; set; }

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600005E RID: 94 RVA: 0x00005842 File Offset: 0x00003A42
	// (set) Token: 0x0600005F RID: 95 RVA: 0x0000584A File Offset: 0x00003A4A
	public Vector3 Direction { get; set; }

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000060 RID: 96 RVA: 0x00005853 File Offset: 0x00003A53
	// (set) Token: 0x06000061 RID: 97 RVA: 0x0000585B File Offset: 0x00003A5B
	public Vector3 Up { get; set; }

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000062 RID: 98 RVA: 0x00005864 File Offset: 0x00003A64
	// (set) Token: 0x06000063 RID: 99 RVA: 0x0000586C File Offset: 0x00003A6C
	public bool Success { get; set; }
}
