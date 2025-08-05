using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.Impl;
using Platform.Library.ClientDataStructures.API;
using Platform.Library.ClientDataStructures.Impl.Cache;

namespace Platform.Kernel.ECS.ClientEntitySystem.API
{
	// Token: 0x02002968 RID: 10600
	[NullableContext(1)]
	[Nullable(0)]
	public class FlowInstancesCache : AbstratFlowInstancesCache
	{
		// Token: 0x06008E89 RID: 36489 RVA: 0x001372FC File Offset: 0x001354FC
		public FlowInstancesCache()
		{
			this.listType = base.Register<List<Type>>(delegate(List<Type> list)
			{
				list.Clear();
			});
			this.listNodeDescription = base.Register<List<NodeDescription>>(delegate(List<NodeDescription> list)
			{
				list.Clear();
			});
			this.listHandlersInvokeData = base.Register<List<HandlerInvokeData>>(delegate(List<HandlerInvokeData> list)
			{
				list.Clear();
			});
			this.flowInvokeData = base.Register<FlowHandlerInvokeDate>();
			this.flowInvokeData.SetMaxSize(2000);
			this.entityNode = base.Register<EntityNode>(delegate(EntityNode e)
			{
				e.Clear();
			});
			this.entityNode.SetMaxSize(1000);
			this.eventBuilder = base.Register<EventBuilderImpl>();
			this.handlerExecutor = base.Register<HandlerExecutor>();
			this.handlerExecutor.SetMaxSize(1000);
		}

		// Token: 0x06008E8A RID: 36490 RVA: 0x0013743C File Offset: 0x0013563C
		public override void OnFlowClean()
		{
			base.OnFlowClean();
			this.array.FreeAll();
			foreach (NodeInstanceCache nodeInstanceCache in this.nodeInstancesCache.Values)
			{
				nodeInstanceCache.OnFlowClean();
			}
		}

		// Token: 0x06008E8B RID: 36491 RVA: 0x001374A4 File Offset: 0x001356A4
		public IList GetGenericListInstance(Type nodeClass, int count)
		{
			return (IList)Activator.CreateInstance(this.genericListInstances.ComputeIfAbsent(nodeClass, (Type k) => typeof(List<>).MakeGenericType(new Type[] { k })), new object[] { count });
		}

		// Token: 0x06008E8C RID: 36492 RVA: 0x001374F8 File Offset: 0x001356F8
		public Node GetNodeInstance(Type nodeClass)
		{
			NodeInstanceCache nodeInstanceCache;
			if (this.nodeInstancesCache.TryGetValue(nodeClass, out nodeInstanceCache))
			{
				return nodeInstanceCache.GetInstance();
			}
			nodeInstanceCache = new NodeInstanceCache(nodeClass);
			this.nodeInstancesCache.Add(nodeClass, nodeInstanceCache);
			return nodeInstanceCache.GetInstance();
		}

		// Token: 0x06008E8D RID: 36493 RVA: 0x00137538 File Offset: 0x00135738
		public void FreeNodeInstance(Node node)
		{
			NodeInstanceCache nodeInstanceCache;
			if (this.nodeInstancesCache.TryGetValue(node.GetType(), out nodeInstanceCache))
			{
				nodeInstanceCache.Free(node);
			}
		}

		// Token: 0x04005FCD RID: 24525
		public readonly Cache<EntityNode> entityNode;

		// Token: 0x04005FCE RID: 24526
		public readonly Cache<EventBuilderImpl> eventBuilder;

		// Token: 0x04005FCF RID: 24527
		public readonly Cache<FlowHandlerInvokeDate> flowInvokeData;

		// Token: 0x04005FD0 RID: 24528
		public readonly Cache<HandlerExecutor> handlerExecutor;

		// Token: 0x04005FD1 RID: 24529
		public readonly Cache<List<HandlerInvokeData>> listHandlersInvokeData;

		// Token: 0x04005FD2 RID: 24530
		public readonly Cache<List<NodeDescription>> listNodeDescription;

		// Token: 0x04005FD3 RID: 24531
		public readonly Cache<List<Type>> listType;

		// Token: 0x04005FD4 RID: 24532
		public CacheMultisizeArray<object> array = new CacheMultisizeArrayImpl<object>();

		// Token: 0x04005FD5 RID: 24533
		public CacheMultisizeArray<Entity> entityArray = new CacheMultisizeArrayImpl<Entity>();

		// Token: 0x04005FD6 RID: 24534
		private readonly Dictionary<Type, Type> genericListInstances = new Dictionary<Type, Type>();

		// Token: 0x04005FD7 RID: 24535
		private readonly Dictionary<Type, NodeInstanceCache> nodeInstancesCache = new Dictionary<Type, NodeInstanceCache>();
	}
}
