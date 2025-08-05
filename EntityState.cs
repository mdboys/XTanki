using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Platform.Library.ClientDataStructures.API;

namespace Platform.Kernel.ECS.ClientEntitySystem.Impl
{
	// Token: 0x02002896 RID: 10390
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityState
	{
		// Token: 0x06008B5E RID: 35678 RVA: 0x000538ED File Offset: 0x00051AED
		public EntityState(Type nodeClass, NodeDescription nodeDescription)
		{
			this.nodeDescription = nodeDescription;
			this.fields = new Dictionary<Type, FieldInfo>();
			this.CreateNode(nodeClass);
			this.ParseFields();
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06008B5F RID: 35679 RVA: 0x00053914 File Offset: 0x00051B14
		// (set) Token: 0x06008B60 RID: 35680 RVA: 0x0005391C File Offset: 0x00051B1C
		public Node Node { get; private set; }

		// Token: 0x170015A0 RID: 5536
		// (set) Token: 0x06008B61 RID: 35681 RVA: 0x00053925 File Offset: 0x00051B25
		public Entity Entity
		{
			set
			{
				this.Node.Entity = value;
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06008B62 RID: 35682 RVA: 0x00053933 File Offset: 0x00051B33
		public ICollection<Type> Components
		{
			get
			{
				return this.nodeDescription.BaseComponents;
			}
		}

		// Token: 0x06008B63 RID: 35683 RVA: 0x00053940 File Offset: 0x00051B40
		private void CreateNode(Type nodeClass)
		{
			this.Node = (Node)nodeClass.GetConstructors()[0].Invoke(Collections.EmptyArray);
		}

		// Token: 0x06008B64 RID: 35684 RVA: 0x0013314C File Offset: 0x0013134C
		private void ParseFields()
		{
			foreach (FieldInfo fieldInfo in this.Node.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (typeof(Component).IsAssignableFrom(fieldInfo.FieldType))
				{
					this.fields[fieldInfo.FieldType] = fieldInfo;
				}
			}
		}

		// Token: 0x06008B65 RID: 35685 RVA: 0x0005395F File Offset: 0x00051B5F
		public void AssignValue(Type componentClass, Component value)
		{
			this.fields[componentClass].SetValue(this.Node, value);
		}

		// Token: 0x04005EBB RID: 24251
		private readonly IDictionary<Type, FieldInfo> fields;

		// Token: 0x04005EBC RID: 24252
		private readonly NodeDescription nodeDescription;
	}
}
