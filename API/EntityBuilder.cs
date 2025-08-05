using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x0200295F RID: 10591
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EntityBuilder
	{
		// Token: 0x06008E67 RID: 36455 RVA: 0x0005573F File Offset: 0x0005393F
		public EntityBuilder(EngineServiceInternal engineServiceInternal, EntityRegistry entityRegistry, TemplateRegistry templateRegistry)
		{
		}

		// Token: 0x06008E68 RID: 36456 RVA: 0x0005575C File Offset: 0x0005395C
		public EntityBuilder SetId(long id)
		{
			this._id = new long?(id);
			return this;
		}

		// Token: 0x06008E69 RID: 36457 RVA: 0x0005576B File Offset: 0x0005396B
		public EntityBuilder SetTemplateAccessor([Nullable(new byte[] { 0, 1 })] Optional<TemplateAccessor> templateAccessor)
		{
			this._templateAccessor = templateAccessor;
			return this;
		}

		// Token: 0x06008E6A RID: 36458 RVA: 0x00055775 File Offset: 0x00053975
		public EntityBuilder SetTemplate(Type templateType)
		{
			return this.SetTemplate(this.<templateRegistry>P.GetTemplateInfo(templateType));
		}

		// Token: 0x06008E6B RID: 36459 RVA: 0x00055789 File Offset: 0x00053989
		public EntityBuilder SetTemplate(TemplateDescription templateInfo)
		{
			this._templateDescription = templateInfo;
			return this;
		}

		// Token: 0x06008E6C RID: 36460 RVA: 0x00055793 File Offset: 0x00053993
		public EntityBuilder SetName(string name)
		{
			this._name = name;
			return this;
		}

		// Token: 0x06008E6D RID: 36461 RVA: 0x0005579D File Offset: 0x0005399D
		public EntityBuilder SetConfig(string configPath)
		{
			this._configPath = configPath;
			return this;
		}

		// Token: 0x06008E6E RID: 36462 RVA: 0x0013724C File Offset: 0x0013544C
		public EntityInternal Build(bool registerInEngine = true)
		{
			long num = this._id.GetValueOrDefault();
			if (this._id == null)
			{
				long idCounter = EntityBuilder._idCounter;
				EntityBuilder._idCounter = idCounter + 1L;
				num = idCounter;
				this._id = new long?(num);
			}
			if (this._name == null)
			{
				TemplateDescription templateDescription = this._templateDescription;
				this._name = ((templateDescription != null) ? templateDescription.TemplateName : null) ?? Convert.ToString(this._id);
			}
			if (!this._templateAccessor.IsPresent())
			{
				this._templateAccessor = this.CreateTemplateAccessor();
			}
			EntityImpl entityImpl = this.CreateEntity();
			if (!registerInEngine)
			{
				return entityImpl;
			}
			this.<entityRegistry>P.RegisterEntity(entityImpl);
			entityImpl.AddComponent<NewEntityComponent>();
			return entityImpl;
		}

		// Token: 0x06008E6F RID: 36463 RVA: 0x000557A7 File Offset: 0x000539A7
		private EntityImpl CreateEntity()
		{
			return new EntityImpl(this.<engineServiceInternal>P, this._id.Value, this._name, this._templateAccessor);
		}

		// Token: 0x06008E70 RID: 36464 RVA: 0x000557CB File Offset: 0x000539CB
		[return: Nullable(new byte[] { 0, 1 })]
		private Optional<TemplateAccessor> CreateTemplateAccessor()
		{
			if (this._templateDescription != null)
			{
				return new Optional<TemplateAccessor>(new TemplateAccessor(this._templateDescription, this._configPath));
			}
			return Optional<TemplateAccessor>.Empty();
		}

		// Token: 0x04005FC2 RID: 24514
		[CompilerGenerated]
		private EngineServiceInternal <engineServiceInternal>P = engineServiceInternal;

		// Token: 0x04005FC3 RID: 24515
		[CompilerGenerated]
		private EntityRegistry <entityRegistry>P = entityRegistry;

		// Token: 0x04005FC4 RID: 24516
		[CompilerGenerated]
		private TemplateRegistry <templateRegistry>P = templateRegistry;

		// Token: 0x04005FC5 RID: 24517
		private static long _idCounter = 4294967296L;

		// Token: 0x04005FC6 RID: 24518
		[Nullable(2)]
		private string _name;

		// Token: 0x04005FC7 RID: 24519
		[Nullable(2)]
		private string _configPath;

		// Token: 0x04005FC8 RID: 24520
		private long? _id;

		// Token: 0x04005FC9 RID: 24521
		[Nullable(new byte[] { 0, 1 })]
		private Optional<TemplateAccessor> _templateAccessor;

		// Token: 0x04005FCA RID: 24522
		[Nullable(2)]
		private TemplateDescription _templateDescription;
	}
}
