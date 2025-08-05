using System;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002862 RID: 10338
	public class ComponentBitIdRegistryImpl : TypeByIdRegistry, ComponentBitIdRegistry, EngineHandlerRegistrationListener
	{
		// Token: 0x06008A34 RID: 35380 RVA: 0x00052BA5 File Offset: 0x00050DA5
		public ComponentBitIdRegistryImpl()
		{
			Func<Type, long> func;
			if ((func = ComponentBitIdRegistryImpl.<>O.<0>__GetNextBitNumber) == null)
			{
				func = (ComponentBitIdRegistryImpl.<>O.<0>__GetNextBitNumber = new Func<Type, long>(ComponentBitIdRegistryImpl.GetNextBitNumber));
			}
			base..ctor(func);
		}

		// Token: 0x06008A35 RID: 35381 RVA: 0x00052BC8 File Offset: 0x00050DC8
		[NullableContext(1)]
		public int GetComponentBitId(Type componentClass)
		{
			return (int)base.GetId(componentClass);
		}

		// Token: 0x06008A36 RID: 35382 RVA: 0x00052BD2 File Offset: 0x00050DD2
		[NullableContext(1)]
		public void OnHandlerAdded(Handler handler)
		{
			handler.HandlerArgumentsDescription.ComponentClasses.ForEach(delegate(Type t)
			{
				base.Register(t);
			});
		}

		// Token: 0x06008A37 RID: 35383 RVA: 0x00052BF0 File Offset: 0x00050DF0
		[NullableContext(1)]
		private static long GetNextBitNumber(Type clazz)
		{
			return ComponentBitIdRegistryImpl._bitSequence += 1L;
		}

		// Token: 0x04005E65 RID: 24165
		private static long _bitSequence;

		// Token: 0x02002863 RID: 10339
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005E66 RID: 24166
			[Nullable(new byte[] { 0, 1 })]
			public static Func<Type, long> <0>__GetNextBitNumber;
		}
	}
}
