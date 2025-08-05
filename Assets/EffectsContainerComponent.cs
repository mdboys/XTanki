using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientUnityIntegration.API;
using UnityEngine;

namespace Assets
{
	// Token: 0x02002CCC RID: 11468
	[NullableContext(1)]
	[Nullable(0)]
	public class EffectsContainerComponent : BehaviourComponent
	{
		// Token: 0x06009FE5 RID: 40933 RVA: 0x0005CD3B File Offset: 0x0005AF3B
		public void SpawnBuff(Entity entity)
		{
			this.SpawnEffect(this.buffContainer, entity);
		}

		// Token: 0x06009FE6 RID: 40934 RVA: 0x0005CD4A File Offset: 0x0005AF4A
		public void SpawnDebuff(Entity entity)
		{
			this.SpawnEffect(this.debuffContainer, entity);
		}

		// Token: 0x06009FE7 RID: 40935 RVA: 0x0005CD59 File Offset: 0x0005AF59
		private void SpawnEffect(RectTransform container, Entity entity)
		{
			EntityBehaviour entityBehaviour = global::UnityEngine.Object.Instantiate<EntityBehaviour>(this.effectPrefab, container, false);
			entityBehaviour.BuildEntity(entity);
			entityBehaviour.transform.SetAsFirstSibling();
			base.SendMessage("RefreshCurve", SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x04006769 RID: 26473
		[SerializeField]
		private RectTransform buffContainer;

		// Token: 0x0400676A RID: 26474
		[SerializeField]
		private RectTransform debuffContainer;

		// Token: 0x0400676B RID: 26475
		[SerializeField]
		private EntityBehaviour effectPrefab;
	}
}
