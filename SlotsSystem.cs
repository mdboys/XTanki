using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Lobby.ClientUserProfile.API;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;
using Tanks.Lobby.ClientControls.API;
using Tanks.Lobby.ClientEntrance.API;
using Tanks.Lobby.ClientGarage.API;
using Tanks.Lobby.ClientGarage.Impl;
using UnityEngine;

// Token: 0x0200006C RID: 108
[NullableContext(1)]
[Nullable(0)]
public class SlotsSystem : ECSSystem
{
	// Token: 0x06000207 RID: 519 RVA: 0x00065818 File Offset: 0x00063A18
	[OnEventComplete]
	public void ShowUpgradeIcon(NodeAddedEvent e, SlotsSystem.SlotWithUIAndModuleNode selectedSlot, [Nullable(new byte[] { 0, 1 })] [JoinByModule] Optional<SlotsSystem.UserModuleNode> userModule, [Nullable(new byte[] { 0, 1 })] [JoinByParentGroup] Optional<SlotsSystem.ModuleCardNode> moduleCard, [JoinAll] SlotsSystem.SelfUserNode selfUser)
	{
		if (userModule.IsPresent() && userModule.Get().userGroup.Key != selfUser.userGroup.Key)
		{
			return;
		}
		selectedSlot.slotUI.UpgradeIcon.gameObject.SetActive(false);
		if (!moduleCard.IsPresent())
		{
			return;
		}
		long count = moduleCard.Get().userItemCounter.Count;
		if (!userModule.IsPresent())
		{
			return;
		}
		long num = userModule.Get().moduleUpgradeLevel.Level + 1L;
		if (num <= (long)userModule.Get().moduleCardsComposition.UpgradePrices.Count && (long)userModule.Get().moduleCardsComposition.UpgradePrices[(int)(num - 1L)].Cards <= count)
		{
			selectedSlot.slotUI.UpgradeIcon.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000686E File Offset: 0x00004A6E
	[OnEventFire]
	public void RemoveUpgradeIcon(NodeRemoveEvent e, SlotsSystem.SlotWithUIAndModuleNode userModule)
	{
		userModule.slotUI.UpgradeIcon.gameObject.SetActive(false);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x000658F4 File Offset: 0x00063AF4
	[OnEventFire]
	public void OnModuleWasUpgraded(ModuleUpgradedEvent e, SlotsSystem.UserModuleNode userModule, [JoinByModule] SlotsSystem.SlotWithUIAndModuleNode selectedSlot, SlotsSystem.UserModuleNode userModule2, [Nullable(new byte[] { 0, 1 })] [JoinByParentGroup] Optional<SlotsSystem.ModuleCardNode> moduleCard)
	{
		int num = (int)userModule.moduleUpgradeLevel.Level;
		int count = userModule.moduleCardsComposition.UpgradePrices.Count;
		if (num == count)
		{
			selectedSlot.slotUI.UpgradeIcon.gameObject.SetActive(false);
			return;
		}
		if (moduleCard.IsPresent())
		{
			if ((long)userModule.moduleCardsComposition.UpgradePrices[num].Cards > moduleCard.Get().userItemCounter.Count)
			{
				selectedSlot.slotUI.UpgradeIcon.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			selectedSlot.slotUI.UpgradeIcon.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0006599C File Offset: 0x00063B9C
	[OnEventFire]
	public void SlotsInModulesScreenInit(NodeAddedEvent e, SingleNode<ModulesScreenUIComponent> screen, SlotsSystem.UserWithoutRentedPreset user, [JoinByUser] ICollection<SlotsSystem.NotReservedSlotNode> slots)
	{
		foreach (SlotsSystem.NotReservedSlotNode notReservedSlotNode in slots)
		{
			notReservedSlotNode.Entity.RemoveComponentIfPresent<SlotUIComponent>();
			SlotUIComponent slot = screen.component.GetSlot(notReservedSlotNode.slotUserItemInfo.Slot);
			SlotsSystem.InitSlotUI(slot, notReservedSlotNode);
			this.InitSlotUIToggle(slot, notReservedSlotNode.Entity);
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00065A14 File Offset: 0x00063C14
	[OnEventFire]
	public void SlotsInGarageInitialized(NodeAddedEvent e, SingleNode<GarageSlotsUIPanelComponent> screen, [Context] SlotsSystem.UserNode user, [Context] ICollection<SlotsSystem.NotReservedSlotNode> slots, [Context] SlotsSystem.SelectedPresetNode selectedPreset)
	{
		foreach (SlotsSystem.NotReservedSlotNode notReservedSlotNode in slots)
		{
			if (notReservedSlotNode.Entity.HasComponent<UserGroupComponent>() && notReservedSlotNode.Entity.GetComponent<UserGroupComponent>().Key == selectedPreset.userGroup.Key)
			{
				notReservedSlotNode.Entity.RemoveComponentIfPresent<SlotUIComponent>();
				SlotsSystem.InitSlotUI(screen.component.GetSlot(notReservedSlotNode.slotUserItemInfo.Slot), notReservedSlotNode);
			}
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00065AA8 File Offset: 0x00063CA8
	[OnEventFire]
	public void SetSettingsSlotIcons(NodeAddedEvent e, SingleNode<SettingsSlotsUIComponent> slotsUI, [JoinAll] ICollection<SlotsSystem.SlotNode> slots, [JoinAll] [Context] SlotsSystem.SelectedPresetNode selectedPreset)
	{
		foreach (SlotsSystem.SlotNode slotNode in slots)
		{
			SettingsSlotUIComponent slot = slotsUI.component.GetSlot(slotNode.slotUserItemInfo.Slot);
			if (!(slot == null) && (slotNode.Entity.HasComponent<SlotReservedComponent>() || slotNode.Entity.GetComponent<UserGroupComponent>().Key == selectedPreset.userGroup.Key))
			{
				IList<SlotsSystem.ModuleNode> list = base.Select<SlotsSystem.ModuleNode>(slotNode.Entity, typeof(ModuleGroupComponent));
				string text = ((list.Count > 0) ? list[0].itemBigIcon.SpriteUid : string.Empty);
				bool flag = false;
				if (list.Count > 0)
				{
					IList<SlotsSystem.ModuleUsesCounterNode> list2 = base.Select<SlotsSystem.ModuleUsesCounterNode>(list[0].Entity, typeof(ModuleGroupComponent));
					flag = list2.Count == 0 || (list2.Count > 0 && list2.First<SlotsSystem.ModuleUsesCounterNode>().userItemCounter.Count > 0L);
				}
				slot.SetIcon(text, flag);
			}
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00065BEC File Offset: 0x00063DEC
	private static void InitSlotUI(SlotUIComponent slotUI, SlotsSystem.NotReservedSlotNode notReservedSlotNode)
	{
		if (slotUI == null)
		{
			return;
		}
		slotUI.Locked = false;
		slotUI.Rank = notReservedSlotNode.slotUserItemInfo.UpgradeLevel;
		slotUI.ModuleIcon.color = Color.white;
		slotUI.Slot = notReservedSlotNode.slotUserItemInfo.Slot;
		slotUI.TankPart = notReservedSlotNode.slotTankPart.TankPart;
		if (slotUI.SelectionImage != null)
		{
			slotUI.SelectionImage.color = Color.white;
		}
		notReservedSlotNode.Entity.RemoveComponentIfPresent<SlotUIComponent>();
		notReservedSlotNode.Entity.AddComponent(slotUI);
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00065C84 File Offset: 0x00063E84
	private void InitSlotUIToggle(SlotUIComponent slotUI, Entity slotEntity)
	{
		ToggleListItemComponent component = slotUI.GetComponent<ToggleListItemComponent>();
		if (slotEntity.HasComponent<ToggleListItemComponent>())
		{
			slotEntity.RemoveComponent<ToggleListItemComponent>();
		}
		slotEntity.AddComponent(component);
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00006886 File Offset: 0x00004A86
	[OnEventFire]
	public void SetIcon(NodeAddedEvent e, SlotsSystem.SlotWithUIAndModuleNode slot, [JoinByModule] SlotsSystem.ModuleNode module)
	{
		slot.slotUI.ModuleIconImageSkin.SpriteUid = module.itemBigIcon.SpriteUid;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x000068A3 File Offset: 0x00004AA3
	[OnEventFire]
	public void ResetIcon(NodeRemoveEvent e, SlotsSystem.SlotWithUIAndModuleNode slot)
	{
		slot.slotUI.ModuleIconImageSkin.SpriteUid = null;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x000068B6 File Offset: 0x00004AB6
	[OnEventFire]
	public void LockSlot(NodeAddedEvent e, [Combine] SlotsSystem.LockedSlotNode slot, [Context] [JoinByUser] SlotsSystem.SelectedPresetNode selectedPreset)
	{
		slot.slotUI.Locked = true;
	}

	// Token: 0x06000212 RID: 530 RVA: 0x000068C4 File Offset: 0x00004AC4
	[OnEventFire]
	public void UnlockSlot(NodeRemoveEvent e, [Combine] SlotsSystem.LockedSlotNode slot, [Context] [JoinByUser] SlotsSystem.SelectedPresetNode selectedPreset)
	{
		slot.slotUI.Locked = false;
		slot.slotUI.Rank = slot.slotUserItemInfo.UpgradeLevel;
	}

	// Token: 0x0200006D RID: 109
	[Nullable(0)]
	public class SlotNode : Node
	{
		// Token: 0x04000183 RID: 387
		public SlotTankPartComponent slotTankPart;

		// Token: 0x04000184 RID: 388
		public SlotUserItemInfoComponent slotUserItemInfo;
	}

	// Token: 0x0200006E RID: 110
	[NullableContext(0)]
	[Not(typeof(SlotReservedComponent))]
	public class NotReservedSlotNode : SlotsSystem.SlotNode
	{
	}

	// Token: 0x0200006F RID: 111
	[Nullable(0)]
	public class ModuleNode : Node
	{
		// Token: 0x04000185 RID: 389
		public DescriptionItemComponent descriptionItem;

		// Token: 0x04000186 RID: 390
		public ItemBigIconComponent itemBigIcon;

		// Token: 0x04000187 RID: 391
		public ItemIconComponent itemIcon;

		// Token: 0x04000188 RID: 392
		public ModuleGroupComponent moduleGroup;

		// Token: 0x04000189 RID: 393
		public ModuleItemComponent moduleItem;
	}

	// Token: 0x02000070 RID: 112
	[Nullable(0)]
	public class SlotWithUIAndModuleNode : Node
	{
		// Token: 0x0400018A RID: 394
		public ModuleGroupComponent moduleGroup;

		// Token: 0x0400018B RID: 395
		public SlotUIComponent slotUI;

		// Token: 0x0400018C RID: 396
		public SlotUserItemInfoComponent slotUserItemInfo;
	}

	// Token: 0x02000071 RID: 113
	[Nullable(0)]
	public class LockedSlotNode : SlotsSystem.NotReservedSlotNode
	{
		// Token: 0x0400018D RID: 397
		public SlotLockedComponent slotLocked;

		// Token: 0x0400018E RID: 398
		public SlotUIComponent slotUI;
	}

	// Token: 0x02000072 RID: 114
	[Nullable(0)]
	public class UserModuleNode : SlotsSystem.ModuleNode
	{
		// Token: 0x0400018F RID: 399
		public ModuleCardsCompositionComponent moduleCardsComposition;

		// Token: 0x04000190 RID: 400
		public ModuleUpgradeLevelComponent moduleUpgradeLevel;

		// Token: 0x04000191 RID: 401
		public UserGroupComponent userGroup;

		// Token: 0x04000192 RID: 402
		public UserItemComponent userItem;
	}

	// Token: 0x02000073 RID: 115
	[Nullable(0)]
	public class ModuleCardNode : Node
	{
		// Token: 0x04000193 RID: 403
		public ModuleCardItemComponent moduleCardItem;

		// Token: 0x04000194 RID: 404
		public UserItemComponent userItem;

		// Token: 0x04000195 RID: 405
		public UserItemCounterComponent userItemCounter;
	}

	// Token: 0x02000074 RID: 116
	[Nullable(0)]
	public class SelfUserNode : Node
	{
		// Token: 0x04000196 RID: 406
		public SelfUserComponent selfUser;

		// Token: 0x04000197 RID: 407
		public UserGroupComponent userGroup;
	}

	// Token: 0x02000075 RID: 117
	[NullableContext(0)]
	[Not(typeof(UserUseItemsPrototypeComponent))]
	public class UserWithoutRentedPreset : SlotsSystem.SelfUserNode
	{
	}

	// Token: 0x02000076 RID: 118
	[Nullable(0)]
	public class UserNode : Node
	{
		// Token: 0x04000198 RID: 408
		public SelfUserComponent selfUser;

		// Token: 0x04000199 RID: 409
		public UserGroupComponent userGroup;
	}

	// Token: 0x02000077 RID: 119
	[Nullable(0)]
	public class SelectedPresetNode : Node
	{
		// Token: 0x0400019A RID: 410
		public SelectedPresetComponent selectedPreset;

		// Token: 0x0400019B RID: 411
		public UserGroupComponent userGroup;
	}

	// Token: 0x02000078 RID: 120
	[Nullable(0)]
	public class ModuleUsesCounterNode : Node
	{
		// Token: 0x0400019C RID: 412
		public ModuleUsesCounterComponent moduleUsesCounter;

		// Token: 0x0400019D RID: 413
		public UserGroupComponent userGroup;

		// Token: 0x0400019E RID: 414
		public UserItemComponent userItem;

		// Token: 0x0400019F RID: 415
		public UserItemCounterComponent userItemCounter;
	}
}
