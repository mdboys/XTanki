using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AF1 RID: 10993
	public abstract class DecalProjectorGroupBase : MonoBehaviour
	{
		// Token: 0x0600979D RID: 38813 RVA: 0x001490A0 File Offset: 0x001472A0
		[NullableContext(1)]
		public GenericDecalsBase GetDecalsBase()
		{
			GenericDecalsBase genericDecalsBase = null;
			Transform transform = base.transform;
			while (transform != null && genericDecalsBase == null)
			{
				genericDecalsBase = transform.GetComponent<GenericDecalsBase>();
				transform = transform.parent;
			}
			return genericDecalsBase;
		}
	}
}
