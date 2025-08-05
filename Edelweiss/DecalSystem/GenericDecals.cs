using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Edelweiss.DecalSystem
{
	// Token: 0x02002AFE RID: 11006
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GenericDecals<[Nullable(0)] D, [Nullable(0)] P, [Nullable(0)] DM> : GenericDecalsBase where D : GenericDecals<D, P, DM> where P : GenericDecalProjector<D, P, DM> where DM : GenericDecalsMesh<D, P, DM>
	{
		// Token: 0x06009815 RID: 38933 RVA: 0x0014D480 File Offset: 0x0014B680
		public virtual void UpdateDecalsMeshes(DM a_DecalsMesh)
		{
			if (a_DecalsMesh == null)
			{
				throw new NullReferenceException("The passed decals mesh is null.");
			}
			if (a_DecalsMesh.Decals != this)
			{
				throw new InvalidOperationException("The decals mesh is not linked to this decals instance.");
			}
			a_DecalsMesh.PreservedVertexColorArrays.Clear();
			a_DecalsMesh.PreservedProjectedUVArrays.Clear();
			a_DecalsMesh.PreservedProjectedUV2Arrays.Clear();
		}

		// Token: 0x06009816 RID: 38934 RVA: 0x0014D4F4 File Offset: 0x0014B6F4
		public virtual void UpdateVertexColors(DM a_DecalsMesh)
		{
			if (Edition.IsDecalSystemFree)
			{
				throw new InvalidOperationException("This function is only supported in Decal System Pro.");
			}
			if (a_DecalsMesh == null)
			{
				throw new NullReferenceException("The passed decals mesh is null.");
			}
			if (a_DecalsMesh.Decals != this)
			{
				throw new InvalidOperationException("The decals mesh is not linked to this decals instance.");
			}
			if (!base.UseVertexColors)
			{
				throw new InvalidOperationException("Vertex colors are not used.");
			}
			if (!a_DecalsMesh.PreserveVertexColorArrays)
			{
				throw new InvalidOperationException("Vertex colors can only be updated if the decals mesh preserves them.");
			}
			int num = 0;
			foreach (Color[] array in a_DecalsMesh.PreservedVertexColorArrays)
			{
				num = array.Length;
			}
			if (num != a_DecalsMesh.VertexColors.Count)
			{
				throw new InvalidOperationException("The preserved vertex color count doesn't match the one from the decals mesh. Avoid changes on the decals mesh which affect the number of vertices, after it was used to update the decals instance as preserving arrays are used.");
			}
		}

		// Token: 0x06009817 RID: 38935 RVA: 0x0014D5DC File Offset: 0x0014B7DC
		public virtual void UpdateProjectedUVs(DM a_DecalsMesh)
		{
			if (Edition.IsDecalSystemFree)
			{
				throw new InvalidOperationException("This function is only supported in Decal System Pro.");
			}
			if (a_DecalsMesh == null)
			{
				throw new NullReferenceException("The passed decals mesh is null.");
			}
			if (a_DecalsMesh.Decals != this)
			{
				throw new InvalidOperationException("The decals mesh is not linked to this decals instance.");
			}
			if (base.CurrentUVMode != UVMode.Project && base.CurrentUVMode != UVMode.ProjectWrapped)
			{
				throw new InvalidOperationException("The current uv mode is not projecting.");
			}
			if (!a_DecalsMesh.PreserveProjectedUVArrays)
			{
				throw new InvalidOperationException("Projected UVs can only be updated if the decals mesh preserves them.");
			}
			int num = 0;
			foreach (Vector2[] array in a_DecalsMesh.PreservedProjectedUVArrays)
			{
				num = array.Length;
			}
			if (num != a_DecalsMesh.UVs.Count)
			{
				throw new InvalidOperationException("The preserved UV count doesn't match the one from the decals mesh. Avoid changes on the decals mesh which affect the number of vertices, after it was used to update the decals instance as preserving arrays are used.");
			}
		}

		// Token: 0x06009818 RID: 38936 RVA: 0x0014D6CC File Offset: 0x0014B8CC
		public virtual void UpdateProjectedUV2s(DM a_DecalsMesh)
		{
			if (Edition.IsDecalSystemFree)
			{
				throw new InvalidOperationException("This function is only supported in Decal System Pro.");
			}
			if (a_DecalsMesh == null)
			{
				throw new NullReferenceException("The passed decals mesh is null.");
			}
			if (a_DecalsMesh.Decals != this)
			{
				throw new InvalidOperationException("The decals mesh is not linked to this decals instance.");
			}
			if (base.CurrentUV2Mode != UV2Mode.Project && base.CurrentUV2Mode != UV2Mode.ProjectWrapped)
			{
				throw new InvalidOperationException("The current uv2 mode is not projecting.");
			}
			if (!a_DecalsMesh.PreserveProjectedUV2Arrays)
			{
				throw new InvalidOperationException("Projected UV2s can only be updated if the decals mesh preserves them.");
			}
			int num = 0;
			foreach (Vector2[] array in a_DecalsMesh.PreservedProjectedUV2Arrays)
			{
				num = array.Length;
			}
			if (num != a_DecalsMesh.UV2s.Count)
			{
				throw new InvalidOperationException("The preserved UV2 count doesn't match the one from the decals mesh. Avoid changes on the decals mesh which affect the number of vertices, after it was used to update the decals instance as preserving arrays are used.");
			}
		}
	}
}
