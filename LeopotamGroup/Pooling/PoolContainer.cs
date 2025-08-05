using System;
using System.Runtime.CompilerServices;
using LeopotamGroup.Collections;
using UnityEngine;

namespace LeopotamGroup.Pooling
{
	// Token: 0x02002AE1 RID: 10977
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PoolContainer : MonoBehaviour
	{
		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06009734 RID: 38708 RVA: 0x0005A545 File Offset: 0x00058745
		// (set) Token: 0x06009735 RID: 38709 RVA: 0x0005A54D File Offset: 0x0005874D
		public string PrefabPath
		{
			get
			{
				return this._prefabPath;
			}
			set
			{
				this._prefabPath = value;
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06009736 RID: 38710 RVA: 0x0005A556 File Offset: 0x00058756
		// (set) Token: 0x06009737 RID: 38711 RVA: 0x0005A55E File Offset: 0x0005875E
		public Transform ItemsRoot
		{
			get
			{
				return this._itemsRoot;
			}
			set
			{
				this._itemsRoot = value;
			}
		}

		// Token: 0x06009738 RID: 38712 RVA: 0x001484D8 File Offset: 0x001466D8
		private bool LoadPrefab()
		{
			GameObject gameObject = ((!(this.CustomPrefab != null)) ? Resources.Load<GameObject>(this._prefabPath) : this.CustomPrefab);
			if (gameObject == null)
			{
				Debug.LogWarning("Cant load asset " + this._prefabPath, base.gameObject);
				return false;
			}
			this._cachedAsset = gameObject.GetComponent(typeof(IPoolObject));
			this._needToAddPoolObject = this._cachedAsset == null;
			if (this._needToAddPoolObject)
			{
				this._cachedAsset = gameObject;
				this._overridedType = typeof(PoolObject);
			}
			else if (this._cachedAsset.GetType() != this._overridedType)
			{
				Debug.LogWarning("Prefab already contains another IPoolObject-component", base.gameObject);
				return false;
			}
			this._cachedScale = gameObject.transform.localScale;
			this._store.UseCastToObjectComparer(true);
			if (this._itemsRoot == null)
			{
				this._itemsRoot = base.transform;
			}
			return true;
		}

		// Token: 0x06009739 RID: 38713 RVA: 0x001485CC File Offset: 0x001467CC
		public IPoolObject Get()
		{
			bool flag;
			return this.Get(out flag);
		}

		// Token: 0x0600973A RID: 38714 RVA: 0x001485E4 File Offset: 0x001467E4
		public IPoolObject Get(out bool isNew)
		{
			if (this._cachedAsset == null && !this.LoadPrefab())
			{
				isNew = true;
				return null;
			}
			IPoolObject poolObject;
			if (this._store.Count > 0)
			{
				poolObject = this._store.Pop();
				isNew = false;
			}
			else
			{
				poolObject = ((!this._needToAddPoolObject) ? ((IPoolObject)global::UnityEngine.Object.Instantiate(this._cachedAsset)) : ((IPoolObject)((GameObject)global::UnityEngine.Object.Instantiate(this._cachedAsset)).AddComponent(this._overridedType)));
				poolObject.PoolContainer = this;
				Transform poolTransform = poolObject.PoolTransform;
				if (poolTransform != null)
				{
					poolTransform.gameObject.SetActive(false);
					poolTransform.SetParent(this._itemsRoot, false);
					poolTransform.localScale = this._cachedScale;
				}
				isNew = true;
			}
			return poolObject;
		}

		// Token: 0x0600973B RID: 38715 RVA: 0x0014869C File Offset: 0x0014689C
		public void Recycle(IPoolObject obj, bool checkForDoubleRecycle = true)
		{
			if (obj == null)
			{
				return;
			}
			Transform poolTransform = obj.PoolTransform;
			if (poolTransform != null)
			{
				poolTransform.gameObject.SetActive(false);
				if (poolTransform.parent != this._itemsRoot)
				{
					poolTransform.SetParent(this._itemsRoot, true);
				}
			}
			if (!checkForDoubleRecycle || !this._store.Contains(obj))
			{
				this._store.Push(obj);
			}
		}

		// Token: 0x0600973C RID: 38716 RVA: 0x0005A567 File Offset: 0x00058767
		public static PoolContainer CreatePool<[Nullable(0)] T>(string prefabPath, Transform itemsRoot = null) where T : IPoolObject
		{
			return PoolContainer.CreatePool(prefabPath, itemsRoot, typeof(T));
		}

		// Token: 0x0600973D RID: 38717 RVA: 0x001486FC File Offset: 0x001468FC
		public static PoolContainer CreatePool(string prefabPath, Transform itemsRoot = null, Type overridedType = null)
		{
			if (string.IsNullOrEmpty(prefabPath))
			{
				return null;
			}
			if (overridedType != null && !typeof(IPoolObject).IsAssignableFrom(overridedType))
			{
				Debug.LogWarningFormat("Invalid IPoolObject-type \"{0}\" for prefab \"{1}\"", new object[] { overridedType, prefabPath });
				return null;
			}
			PoolContainer poolContainer = new GameObject().AddComponent<PoolContainer>();
			poolContainer._prefabPath = prefabPath;
			poolContainer._itemsRoot = itemsRoot;
			poolContainer._overridedType = overridedType;
			return poolContainer;
		}

		// Token: 0x0400637F RID: 25471
		[SerializeField]
		private string _prefabPath = "UnknownPrefab";

		// Token: 0x04006380 RID: 25472
		public GameObject CustomPrefab;

		// Token: 0x04006381 RID: 25473
		[SerializeField]
		private Transform _itemsRoot;

		// Token: 0x04006382 RID: 25474
		private readonly FastStack<IPoolObject> _store = new FastStack<IPoolObject>(32, null);

		// Token: 0x04006383 RID: 25475
		private global::UnityEngine.Object _cachedAsset;

		// Token: 0x04006384 RID: 25476
		private Vector3 _cachedScale;

		// Token: 0x04006385 RID: 25477
		private bool _needToAddPoolObject;

		// Token: 0x04006386 RID: 25478
		private Type _overridedType;
	}
}
