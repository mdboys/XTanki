using System;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x020029A5 RID: 10661
	[NullableContext(1)]
	[Nullable(0)]
	internal class ObjectManager
	{
		// Token: 0x06008F5E RID: 36702 RVA: 0x00139D4C File Offset: 0x00137F4C
		public ObjectManager(ConvexHullInternal hull)
		{
			this.Dimension = hull.Dimension;
			this.Hull = hull;
			this.FacePool = hull.FacePool;
			this.FacePoolSize = 0;
			this.FacePoolCapacity = hull.FacePool.Length;
			this.FreeFaceIndices = new IndexBuffer();
			this.EmptyBufferStack = new SimpleList<IndexBuffer>();
			this.DeferredFaceStack = new SimpleList<DeferredFace>();
		}

		// Token: 0x06008F5F RID: 36703 RVA: 0x00139DB4 File Offset: 0x00137FB4
		public void DepositFace(int faceIndex)
		{
			int[] adjacentFaces = this.FacePool[faceIndex].AdjacentFaces;
			for (int i = 0; i < adjacentFaces.Length; i++)
			{
				adjacentFaces[i] = -1;
			}
			this.FreeFaceIndices.Push(faceIndex);
		}

		// Token: 0x06008F60 RID: 36704 RVA: 0x00139DF0 File Offset: 0x00137FF0
		private void ReallocateFacePool()
		{
			ConvexFaceInternal[] array = new ConvexFaceInternal[2 * this.FacePoolCapacity];
			bool[] array2 = new bool[2 * this.FacePoolCapacity];
			Array.Copy(this.FacePool, array, this.FacePoolCapacity);
			Buffer.BlockCopy(this.Hull.AffectedFaceFlags, 0, array2, 0, this.FacePoolCapacity);
			this.FacePoolCapacity = 2 * this.FacePoolCapacity;
			this.Hull.FacePool = array;
			this.FacePool = array;
			this.Hull.AffectedFaceFlags = array2;
		}

		// Token: 0x06008F61 RID: 36705 RVA: 0x00139E74 File Offset: 0x00138074
		private int CreateFace()
		{
			int facePoolSize = this.FacePoolSize;
			ConvexFaceInternal convexFaceInternal = new ConvexFaceInternal(this.Dimension, facePoolSize, this.GetVertexBuffer());
			this.FacePoolSize++;
			if (this.FacePoolSize > this.FacePoolCapacity)
			{
				this.ReallocateFacePool();
			}
			this.FacePool[facePoolSize] = convexFaceInternal;
			return facePoolSize;
		}

		// Token: 0x06008F62 RID: 36706 RVA: 0x00055E16 File Offset: 0x00054016
		public int GetFace()
		{
			if (this.FreeFaceIndices.Count > 0)
			{
				return this.FreeFaceIndices.Pop();
			}
			return this.CreateFace();
		}

		// Token: 0x06008F63 RID: 36707 RVA: 0x00055E38 File Offset: 0x00054038
		public void DepositConnector(FaceConnector connector)
		{
			if (this.ConnectorStack == null)
			{
				connector.Next = null;
				this.ConnectorStack = connector;
				return;
			}
			connector.Next = this.ConnectorStack;
			this.ConnectorStack = connector;
		}

		// Token: 0x06008F64 RID: 36708 RVA: 0x00055E64 File Offset: 0x00054064
		public FaceConnector GetConnector()
		{
			if (this.ConnectorStack == null)
			{
				return new FaceConnector(this.Dimension);
			}
			FaceConnector connectorStack = this.ConnectorStack;
			this.ConnectorStack = this.ConnectorStack.Next;
			connectorStack.Next = null;
			return connectorStack;
		}

		// Token: 0x06008F65 RID: 36709 RVA: 0x00055E98 File Offset: 0x00054098
		public void DepositVertexBuffer(IndexBuffer buffer)
		{
			buffer.Clear();
			this.EmptyBufferStack.Push(buffer);
		}

		// Token: 0x06008F66 RID: 36710 RVA: 0x00055EAC File Offset: 0x000540AC
		public IndexBuffer GetVertexBuffer()
		{
			if (this.EmptyBufferStack.Count != 0)
			{
				return this.EmptyBufferStack.Pop();
			}
			return new IndexBuffer();
		}

		// Token: 0x06008F67 RID: 36711 RVA: 0x00055ECC File Offset: 0x000540CC
		public void DepositDeferredFace(DeferredFace face)
		{
			this.DeferredFaceStack.Push(face);
		}

		// Token: 0x06008F68 RID: 36712 RVA: 0x00055EDA File Offset: 0x000540DA
		public DeferredFace GetDeferredFace()
		{
			if (this.DeferredFaceStack.Count != 0)
			{
				return this.DeferredFaceStack.Pop();
			}
			return new DeferredFace();
		}

		// Token: 0x04006043 RID: 24643
		private readonly int Dimension;

		// Token: 0x04006044 RID: 24644
		private FaceConnector ConnectorStack;

		// Token: 0x04006045 RID: 24645
		private readonly SimpleList<DeferredFace> DeferredFaceStack;

		// Token: 0x04006046 RID: 24646
		private readonly SimpleList<IndexBuffer> EmptyBufferStack;

		// Token: 0x04006047 RID: 24647
		private ConvexFaceInternal[] FacePool;

		// Token: 0x04006048 RID: 24648
		private int FacePoolCapacity;

		// Token: 0x04006049 RID: 24649
		private int FacePoolSize;

		// Token: 0x0400604A RID: 24650
		private readonly IndexBuffer FreeFaceIndices;

		// Token: 0x0400604B RID: 24651
		private readonly ConvexHullInternal Hull;
	}
}
