using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AFD RID: 11005
	public abstract class GenericDecalProjectorComponent<D, P, DM> : GenericDecalProjectorBaseComponent where D : GenericDecals<D, P, DM> where P : GenericDecalProjector<D, P, DM> where DM : GenericDecalsMesh<D, P, DM>
	{
		// Token: 0x06009813 RID: 38931 RVA: 0x0014D43C File Offset: 0x0014B63C
		[NullableContext(1)]
		public D GetDecals()
		{
			D d = default(D);
			Transform transform = base.CachedTransform;
			while (transform != null && d == null)
			{
				d = transform.GetComponent<D>();
				transform = transform.parent;
			}
			return d;
		}
	}
}
