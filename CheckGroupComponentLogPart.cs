using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200285E RID: 10334
	[NullableContext(1)]
	[Nullable(0)]
	public class CheckGroupComponentLogPart : LogPart
	{
		// Token: 0x06008A2D RID: 35373 RVA: 0x0013147C File Offset: 0x0012F67C
		public CheckGroupComponentLogPart(HandlerArgument handlerArgument, ICollection<Entity> entities)
		{
			Optional<Type> contextComponent = handlerArgument.JoinType.Get().ContextComponent;
			if (!contextComponent.IsPresent())
			{
				return;
			}
			this.groupComponent = contextComponent.Get();
			this.entitiesWithMissingGroupComponentByEntity = new List<Entity>();
			foreach (Entity entity in entities)
			{
				if (entity.HasComponent(this.groupComponent))
				{
					break;
				}
				this.entitiesWithMissingGroupComponentByEntity.Add(entity);
			}
		}

		// Token: 0x06008A2E RID: 35374 RVA: 0x00052B4D File Offset: 0x00050D4D
		[return: Nullable(new byte[] { 0, 1 })]
		public Optional<string> GetSkipReason()
		{
			if (this.entitiesWithMissingGroupComponentByEntity.Count == 0)
			{
				return Optional<string>.Empty();
			}
			return new Optional<string>(this.GetMessageForGroupComponent());
		}

		// Token: 0x06008A2F RID: 35375 RVA: 0x00131520 File Offset: 0x0012F720
		private string GetMessageForGroupComponent()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\tMissing group component=" + this.groupComponent.Name + "\n\t");
			foreach (Entity entity in this.entitiesWithMissingGroupComponentByEntity)
			{
				stringBuilder.Append("\tEntity=" + EcsToStringUtil.ToString(entity));
				stringBuilder.Append("\n\t");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04005E62 RID: 24162
		private readonly IList<Entity> entitiesWithMissingGroupComponentByEntity = Collections.EmptyList<Entity>();

		// Token: 0x04005E63 RID: 24163
		private readonly Type groupComponent;
	}
}
