using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x0200289B RID: 10395
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityYamlConverter : IYamlTypeConverter
	{
		// Token: 0x06008B9C RID: 35740 RVA: 0x00053A62 File Offset: 0x00051C62
		public EntityYamlConverter(EngineServiceInternal engine)
		{
		}

		// Token: 0x06008B9D RID: 35741 RVA: 0x00053A71 File Offset: 0x00051C71
		public bool Accepts(Type type)
		{
			return typeof(Entity).IsAssignableFrom(type);
		}

		// Token: 0x06008B9E RID: 35742 RVA: 0x00133558 File Offset: 0x00131758
		public object ReadYaml(IParser parser, Type type)
		{
			string value = ((Scalar)parser.Current).Value;
			parser.MoveNext();
			return this.<engine>P.EntityRegistry.GetEntity((long)ConfigurationEntityIdCalculator.Calculate(value));
		}

		// Token: 0x06008B9F RID: 35743 RVA: 0x00007FCD File Offset: 0x000061CD
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04005EC7 RID: 24263
		[CompilerGenerated]
		private EngineServiceInternal <engine>P = engine;
	}
}
