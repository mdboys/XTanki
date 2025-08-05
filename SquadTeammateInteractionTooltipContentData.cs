using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;

// Token: 0x02000083 RID: 131
[NullableContext(1)]
[Nullable(0)]
public class SquadTeammateInteractionTooltipContentData
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x0600026D RID: 621 RVA: 0x00006CD9 File Offset: 0x00004ED9
	// (set) Token: 0x0600026E RID: 622 RVA: 0x00006CE1 File Offset: 0x00004EE1
	public Entity teammateEntity { get; set; }

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x0600026F RID: 623 RVA: 0x00006CEA File Offset: 0x00004EEA
	// (set) Token: 0x06000270 RID: 624 RVA: 0x00006CF2 File Offset: 0x00004EF2
	public bool ShowProfileButton { get; set; }

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000271 RID: 625 RVA: 0x00006CFB File Offset: 0x00004EFB
	// (set) Token: 0x06000272 RID: 626 RVA: 0x00006D03 File Offset: 0x00004F03
	public bool ShowLeaveSquadButton { get; set; }

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000273 RID: 627 RVA: 0x00006D0C File Offset: 0x00004F0C
	// (set) Token: 0x06000274 RID: 628 RVA: 0x00006D14 File Offset: 0x00004F14
	public bool ShowRemoveFromSquadButton { get; set; }

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000275 RID: 629 RVA: 0x00006D1D File Offset: 0x00004F1D
	// (set) Token: 0x06000276 RID: 630 RVA: 0x00006D25 File Offset: 0x00004F25
	public bool ActiveRemoveFromSquadButton { get; set; }

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000277 RID: 631 RVA: 0x00006D2E File Offset: 0x00004F2E
	// (set) Token: 0x06000278 RID: 632 RVA: 0x00006D36 File Offset: 0x00004F36
	public bool ShowGiveLeaderButton { get; set; }

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000279 RID: 633 RVA: 0x00006D3F File Offset: 0x00004F3F
	// (set) Token: 0x0600027A RID: 634 RVA: 0x00006D47 File Offset: 0x00004F47
	public bool ActiveGiveLeaderButton { get; set; }

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600027B RID: 635 RVA: 0x00006D50 File Offset: 0x00004F50
	// (set) Token: 0x0600027C RID: 636 RVA: 0x00006D58 File Offset: 0x00004F58
	public bool ShowAddFriendButton { get; set; }

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x0600027D RID: 637 RVA: 0x00006D61 File Offset: 0x00004F61
	// (set) Token: 0x0600027E RID: 638 RVA: 0x00006D69 File Offset: 0x00004F69
	public bool ShowFriendRequestSentButton { get; set; }
}
