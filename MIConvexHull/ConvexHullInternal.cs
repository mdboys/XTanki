using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MIConvexHull
{
	// Token: 0x0200299B RID: 10651
	[NullableContext(1)]
	[Nullable(0)]
	internal class ConvexHullInternal
	{
		// Token: 0x06008F1F RID: 36639 RVA: 0x00137760 File Offset: 0x00135960
		private ConvexHullInternal(IVertex[] vertices, bool lift, ConvexHullComputationConfig config)
		{
			if (config.PointTranslationType != PointTranslationType.None && config.PointTranslationGenerator == null)
			{
				throw new InvalidOperationException("PointTranslationGenerator cannot be null if PointTranslationType is enabled.");
			}
			this.IsLifted = lift;
			this.Vertices = vertices;
			this.PlaneDistanceTolerance = config.PlaneDistanceTolerance;
			this.Dimension = this.DetermineDimension();
			if (this.Dimension < 2)
			{
				throw new InvalidOperationException("Dimension of the input must be 2 or greater.");
			}
			if (lift)
			{
				this.Dimension++;
			}
			this.InitializeData(config);
		}

		// Token: 0x06008F20 RID: 36640 RVA: 0x00055CBA File Offset: 0x00053EBA
		private void TagAffectedFaces(ConvexFaceInternal currentFace)
		{
			this.AffectedFaceBuffer.Clear();
			this.AffectedFaceBuffer.Add(currentFace.Index);
			this.TraverseAffectedFaces(currentFace.Index);
		}

		// Token: 0x06008F21 RID: 36641 RVA: 0x001377E0 File Offset: 0x001359E0
		private void TraverseAffectedFaces(int currentFace)
		{
			this.TraverseStack.Clear();
			this.TraverseStack.Push(currentFace);
			this.AffectedFaceFlags[currentFace] = true;
			while (this.TraverseStack.Count > 0)
			{
				ConvexFaceInternal convexFaceInternal = this.FacePool[this.TraverseStack.Pop()];
				for (int i = 0; i < this.Dimension; i++)
				{
					int num = convexFaceInternal.AdjacentFaces[i];
					if (!this.AffectedFaceFlags[num] && this.MathHelper.GetVertexDistance(this.CurrentVertex, this.FacePool[num]) >= this.PlaneDistanceTolerance)
					{
						this.AffectedFaceBuffer.Add(num);
						this.AffectedFaceFlags[num] = true;
						this.TraverseStack.Push(num);
					}
				}
			}
		}

		// Token: 0x06008F22 RID: 36642 RVA: 0x00055CE4 File Offset: 0x00053EE4
		private DeferredFace MakeDeferredFace(ConvexFaceInternal face, int faceIndex, ConvexFaceInternal pivot, int pivotIndex, ConvexFaceInternal oldFace)
		{
			DeferredFace deferredFace = this.ObjectManager.GetDeferredFace();
			deferredFace.Face = face;
			deferredFace.FaceIndex = faceIndex;
			deferredFace.Pivot = pivot;
			deferredFace.PivotIndex = pivotIndex;
			deferredFace.OldFace = oldFace;
			return deferredFace;
		}

		// Token: 0x06008F23 RID: 36643 RVA: 0x0013789C File Offset: 0x00135A9C
		private void ConnectFace(FaceConnector connector)
		{
			uint num = connector.HashCode % 2017U;
			ConnectorList connectorList = this.ConnectorTable[(int)num];
			for (FaceConnector faceConnector = connectorList.First; faceConnector != null; faceConnector = faceConnector.Next)
			{
				if (FaceConnector.AreConnectable(connector, faceConnector, this.Dimension))
				{
					connectorList.Remove(faceConnector);
					FaceConnector.Connect(faceConnector, connector);
					faceConnector.Face = null;
					connector.Face = null;
					this.ObjectManager.DepositConnector(faceConnector);
					this.ObjectManager.DepositConnector(connector);
					return;
				}
			}
			connectorList.Add(connector);
		}

		// Token: 0x06008F24 RID: 36644 RVA: 0x00137920 File Offset: 0x00135B20
		private bool CreateCone()
		{
			int currentVertex = this.CurrentVertex;
			this.ConeFaceBuffer.Clear();
			for (int i = 0; i < this.AffectedFaceBuffer.Count; i++)
			{
				int num = this.AffectedFaceBuffer[i];
				ConvexFaceInternal convexFaceInternal = this.FacePool[num];
				int num2 = 0;
				for (int j = 0; j < this.Dimension; j++)
				{
					int num3 = convexFaceInternal.AdjacentFaces[j];
					if (!this.AffectedFaceFlags[num3])
					{
						this.UpdateBuffer[num2] = num3;
						this.UpdateIndices[num2] = j;
						num2++;
					}
				}
				for (int k = 0; k < num2; k++)
				{
					ConvexFaceInternal convexFaceInternal2 = this.FacePool[this.UpdateBuffer[k]];
					int num4 = 0;
					int[] adjacentFaces = convexFaceInternal2.AdjacentFaces;
					for (int l = 0; l < adjacentFaces.Length; l++)
					{
						if (num == adjacentFaces[l])
						{
							num4 = l;
							break;
						}
					}
					int num5 = this.UpdateIndices[k];
					int face = this.ObjectManager.GetFace();
					ConvexFaceInternal convexFaceInternal3 = this.FacePool[face];
					int[] vertices = convexFaceInternal3.Vertices;
					for (int m = 0; m < this.Dimension; m++)
					{
						vertices[m] = convexFaceInternal.Vertices[m];
					}
					int num6 = vertices[num5];
					int num7;
					if (currentVertex < num6)
					{
						num7 = 0;
						for (int n = num5 - 1; n >= 0; n--)
						{
							if (vertices[n] <= currentVertex)
							{
								num7 = n + 1;
								break;
							}
							vertices[n + 1] = vertices[n];
						}
					}
					else
					{
						num7 = this.Dimension - 1;
						for (int num8 = num5 + 1; num8 < this.Dimension; num8++)
						{
							if (vertices[num8] >= currentVertex)
							{
								num7 = num8 - 1;
								break;
							}
							vertices[num8 - 1] = vertices[num8];
						}
					}
					vertices[num7] = this.CurrentVertex;
					if (!this.MathHelper.CalculateFacePlane(convexFaceInternal3, this.Center))
					{
						return false;
					}
					this.ConeFaceBuffer.Add(this.MakeDeferredFace(convexFaceInternal3, num7, convexFaceInternal2, num4, convexFaceInternal));
				}
			}
			return true;
		}

		// Token: 0x06008F25 RID: 36645 RVA: 0x00137B20 File Offset: 0x00135D20
		private void CommitCone()
		{
			for (int i = 0; i < this.ConeFaceBuffer.Count; i++)
			{
				DeferredFace deferredFace = this.ConeFaceBuffer[i];
				ConvexFaceInternal face = deferredFace.Face;
				ConvexFaceInternal pivot = deferredFace.Pivot;
				ConvexFaceInternal oldFace = deferredFace.OldFace;
				int faceIndex = deferredFace.FaceIndex;
				face.AdjacentFaces[faceIndex] = pivot.Index;
				pivot.AdjacentFaces[deferredFace.PivotIndex] = face.Index;
				for (int j = 0; j < this.Dimension; j++)
				{
					if (j != faceIndex)
					{
						FaceConnector connector = this.ObjectManager.GetConnector();
						connector.Update(face, j, this.Dimension);
						this.ConnectFace(connector);
					}
				}
				if (pivot.VerticesBeyond.Count == 0)
				{
					this.FindBeyondVertices(face, oldFace.VerticesBeyond);
				}
				else if (pivot.VerticesBeyond.Count < oldFace.VerticesBeyond.Count)
				{
					this.FindBeyondVertices(face, pivot.VerticesBeyond, oldFace.VerticesBeyond);
				}
				else
				{
					this.FindBeyondVertices(face, oldFace.VerticesBeyond, pivot.VerticesBeyond);
				}
				if (face.VerticesBeyond.Count == 0)
				{
					this.ConvexFaces.Add(face.Index);
					this.UnprocessedFaces.Remove(face);
					this.ObjectManager.DepositVertexBuffer(face.VerticesBeyond);
					face.VerticesBeyond = this.EmptyBuffer;
				}
				else
				{
					this.UnprocessedFaces.Add(face);
				}
				this.ObjectManager.DepositDeferredFace(deferredFace);
			}
			for (int k = 0; k < this.AffectedFaceBuffer.Count; k++)
			{
				int num = this.AffectedFaceBuffer[k];
				this.UnprocessedFaces.Remove(this.FacePool[num]);
				this.ObjectManager.DepositFace(num);
			}
		}

		// Token: 0x06008F26 RID: 36646 RVA: 0x00137CE4 File Offset: 0x00135EE4
		private void IsBeyond(ConvexFaceInternal face, IndexBuffer beyondVertices, int v)
		{
			double vertexDistance = this.MathHelper.GetVertexDistance(v, face);
			if (vertexDistance < this.PlaneDistanceTolerance)
			{
				return;
			}
			if (vertexDistance > this.MaxDistance)
			{
				if (vertexDistance - this.MaxDistance < this.PlaneDistanceTolerance)
				{
					if (this.LexCompare(v, this.FurthestVertex) > 0)
					{
						this.MaxDistance = vertexDistance;
						this.FurthestVertex = v;
					}
				}
				else
				{
					this.MaxDistance = vertexDistance;
					this.FurthestVertex = v;
				}
			}
			beyondVertices.Add(v);
		}

		// Token: 0x06008F27 RID: 36647 RVA: 0x00137D58 File Offset: 0x00135F58
		private void FindBeyondVertices(ConvexFaceInternal face, IndexBuffer beyond, IndexBuffer beyond1)
		{
			IndexBuffer beyondBuffer = this.BeyondBuffer;
			this.MaxDistance = double.NegativeInfinity;
			this.FurthestVertex = 0;
			for (int i = 0; i < beyond1.Count; i++)
			{
				this.VertexMarks[beyond1[i]] = true;
			}
			this.VertexMarks[this.CurrentVertex] = false;
			for (int j = 0; j < beyond.Count; j++)
			{
				int num = beyond[j];
				if (num != this.CurrentVertex)
				{
					this.VertexMarks[num] = false;
					this.IsBeyond(face, beyondBuffer, num);
				}
			}
			for (int k = 0; k < beyond1.Count; k++)
			{
				int num2 = beyond1[k];
				if (this.VertexMarks[num2])
				{
					this.IsBeyond(face, beyondBuffer, num2);
				}
			}
			face.FurthestVertex = this.FurthestVertex;
			IndexBuffer verticesBeyond = face.VerticesBeyond;
			face.VerticesBeyond = beyondBuffer;
			if (verticesBeyond.Count > 0)
			{
				verticesBeyond.Clear();
			}
			this.BeyondBuffer = verticesBeyond;
		}

		// Token: 0x06008F28 RID: 36648 RVA: 0x00137E4C File Offset: 0x0013604C
		private void FindBeyondVertices(ConvexFaceInternal face, IndexBuffer beyond)
		{
			IndexBuffer beyondBuffer = this.BeyondBuffer;
			this.MaxDistance = double.NegativeInfinity;
			this.FurthestVertex = 0;
			for (int i = 0; i < beyond.Count; i++)
			{
				int num = beyond[i];
				if (num != this.CurrentVertex)
				{
					this.IsBeyond(face, beyondBuffer, num);
				}
			}
			face.FurthestVertex = this.FurthestVertex;
			IndexBuffer verticesBeyond = face.VerticesBeyond;
			face.VerticesBeyond = beyondBuffer;
			if (verticesBeyond.Count > 0)
			{
				verticesBeyond.Clear();
			}
			this.BeyondBuffer = verticesBeyond;
		}

		// Token: 0x06008F29 RID: 36649 RVA: 0x00137ED4 File Offset: 0x001360D4
		private void UpdateCenter()
		{
			for (int i = 0; i < this.Dimension; i++)
			{
				this.Center[i] *= (double)this.ConvexHullSize;
			}
			this.ConvexHullSize++;
			double num = 1.0 / (double)this.ConvexHullSize;
			int num2 = this.CurrentVertex * this.Dimension;
			for (int j = 0; j < this.Dimension; j++)
			{
				this.Center[j] = num * (this.Center[j] + this.Positions[num2 + j]);
			}
		}

		// Token: 0x06008F2A RID: 36650 RVA: 0x00137F68 File Offset: 0x00136168
		private void RollbackCenter()
		{
			for (int i = 0; i < this.Dimension; i++)
			{
				this.Center[i] *= (double)this.ConvexHullSize;
			}
			this.ConvexHullSize--;
			double num = ((this.ConvexHullSize <= 0) ? 0.0 : (1.0 / (double)this.ConvexHullSize));
			int num2 = this.CurrentVertex * this.Dimension;
			for (int j = 0; j < this.Dimension; j++)
			{
				this.Center[j] = num * (this.Center[j] - this.Positions[num2 + j]);
			}
		}

		// Token: 0x06008F2B RID: 36651 RVA: 0x00138010 File Offset: 0x00136210
		private void HandleSingular()
		{
			this.RollbackCenter();
			this.SingularVertices.Add(this.CurrentVertex);
			for (int i = 0; i < this.AffectedFaceBuffer.Count; i++)
			{
				ConvexFaceInternal convexFaceInternal = this.FacePool[this.AffectedFaceBuffer[i]];
				IndexBuffer verticesBeyond = convexFaceInternal.VerticesBeyond;
				for (int j = 0; j < verticesBeyond.Count; j++)
				{
					this.SingularVertices.Add(verticesBeyond[j]);
				}
				this.ConvexFaces.Add(convexFaceInternal.Index);
				this.UnprocessedFaces.Remove(convexFaceInternal);
				this.ObjectManager.DepositVertexBuffer(convexFaceInternal.VerticesBeyond);
				convexFaceInternal.VerticesBeyond = this.EmptyBuffer;
			}
		}

		// Token: 0x06008F2C RID: 36652 RVA: 0x001380C8 File Offset: 0x001362C8
		private void FindConvexHull()
		{
			this.InitConvexHull();
			while (this.UnprocessedFaces.First != null)
			{
				ConvexFaceInternal first = this.UnprocessedFaces.First;
				this.CurrentVertex = first.FurthestVertex;
				this.UpdateCenter();
				this.TagAffectedFaces(first);
				if (!this.SingularVertices.Contains(this.CurrentVertex) && this.CreateCone())
				{
					this.CommitCone();
				}
				else
				{
					this.HandleSingular();
				}
				int count = this.AffectedFaceBuffer.Count;
				for (int i = 0; i < count; i++)
				{
					this.AffectedFaceFlags[this.AffectedFaceBuffer[i]] = false;
				}
			}
		}

		// Token: 0x06008F2D RID: 36653 RVA: 0x00138168 File Offset: 0x00136368
		private void InitializeData(ConvexHullComputationConfig config)
		{
			this.UnprocessedFaces = new FaceList();
			this.ConvexFaces = new IndexBuffer();
			this.FacePool = new ConvexFaceInternal[(this.Dimension + 1) * 10];
			this.AffectedFaceFlags = new bool[(this.Dimension + 1) * 10];
			this.ObjectManager = new ObjectManager(this);
			this.Center = new double[this.Dimension];
			this.TraverseStack = new IndexBuffer();
			this.UpdateBuffer = new int[this.Dimension];
			this.UpdateIndices = new int[this.Dimension];
			this.EmptyBuffer = new IndexBuffer();
			this.AffectedFaceBuffer = new IndexBuffer();
			this.ConeFaceBuffer = new SimpleList<DeferredFace>();
			this.SingularVertices = new HashSet<int>();
			this.BeyondBuffer = new IndexBuffer();
			this.ConnectorTable = new ConnectorList[2017];
			for (int i = 0; i < 2017; i++)
			{
				this.ConnectorTable[i] = new ConnectorList();
			}
			this.VertexMarks = new bool[this.Vertices.Length];
			this.InitializePositions(config);
			this.MathHelper = new MathHelper(this.Dimension, this.Positions);
		}

		// Token: 0x06008F2E RID: 36654 RVA: 0x00138298 File Offset: 0x00136498
		private void InitializePositions(ConvexHullComputationConfig config)
		{
			this.Positions = new double[this.Vertices.Length * this.Dimension];
			int num = 0;
			if (this.IsLifted)
			{
				int num2 = this.Dimension - 1;
				Func<double> pointTranslationGenerator = config.PointTranslationGenerator;
				PointTranslationType pointTranslationType = config.PointTranslationType;
				if (pointTranslationType == PointTranslationType.None)
				{
					foreach (IVertex vertex in this.Vertices)
					{
						double num3 = 0.0;
						for (int j = 0; j < num2; j++)
						{
							double num4 = vertex.Position[j];
							this.Positions[num++] = num4;
							num3 += num4 * num4;
						}
						this.Positions[num++] = num3;
					}
					return;
				}
				if (pointTranslationType != PointTranslationType.TranslateInternal)
				{
					return;
				}
				foreach (IVertex vertex2 in this.Vertices)
				{
					double num5 = 0.0;
					for (int k = 0; k < num2; k++)
					{
						double num6 = vertex2.Position[k] + pointTranslationGenerator();
						this.Positions[num++] = num6;
						num5 += num6 * num6;
					}
					this.Positions[num++] = num5;
				}
				return;
			}
			else
			{
				Func<double> pointTranslationGenerator2 = config.PointTranslationGenerator;
				PointTranslationType pointTranslationType = config.PointTranslationType;
				if (pointTranslationType == PointTranslationType.None)
				{
					foreach (IVertex vertex3 in this.Vertices)
					{
						for (int l = 0; l < this.Dimension; l++)
						{
							this.Positions[num++] = vertex3.Position[l];
						}
					}
					return;
				}
				if (pointTranslationType != PointTranslationType.TranslateInternal)
				{
					return;
				}
				foreach (IVertex vertex4 in this.Vertices)
				{
					for (int m = 0; m < this.Dimension; m++)
					{
						this.Positions[num++] = vertex4.Position[m] + pointTranslationGenerator2();
					}
				}
				return;
			}
		}

		// Token: 0x06008F2F RID: 36655 RVA: 0x00055D16 File Offset: 0x00053F16
		private double GetCoordinate(int v, int i)
		{
			return this.Positions[v * this.Dimension + i];
		}

		// Token: 0x06008F30 RID: 36656 RVA: 0x00138490 File Offset: 0x00136690
		private int DetermineDimension()
		{
			Random random = new Random();
			int num = this.Vertices.Length;
			List<int> list = new List<int>();
			for (int i = 0; i < 10; i++)
			{
				list.Add(this.Vertices[random.Next(num)].Position.Length);
			}
			int num2 = list.Min();
			if (num2 != list.Max())
			{
				throw new ArgumentException("Invalid input data (non-uniform dimension).");
			}
			return num2;
		}

		// Token: 0x06008F31 RID: 36657 RVA: 0x001384F4 File Offset: 0x001366F4
		private int[] CreateInitialHull(List<int> initialPoints)
		{
			int[] array = new int[this.Dimension + 1];
			for (int i = 0; i < this.Dimension + 1; i++)
			{
				int[] array2 = new int[this.Dimension];
				int j = 0;
				int num = 0;
				while (j <= this.Dimension)
				{
					if (i != j)
					{
						array2[num++] = initialPoints[j];
					}
					j++;
				}
				ConvexFaceInternal convexFaceInternal = this.FacePool[this.ObjectManager.GetFace()];
				convexFaceInternal.Vertices = array2;
				Array.Sort<int>(array2);
				this.MathHelper.CalculateFacePlane(convexFaceInternal, this.Center);
				array[i] = convexFaceInternal.Index;
			}
			for (int k = 0; k < this.Dimension; k++)
			{
				for (int l = k + 1; l < this.Dimension + 1; l++)
				{
					this.UpdateAdjacency(this.FacePool[array[k]], this.FacePool[array[l]]);
				}
			}
			return array;
		}

		// Token: 0x06008F32 RID: 36658 RVA: 0x001385E8 File Offset: 0x001367E8
		private void UpdateAdjacency(ConvexFaceInternal l, ConvexFaceInternal r)
		{
			int[] vertices = l.Vertices;
			int[] vertices2 = r.Vertices;
			int i;
			for (i = 0; i < vertices.Length; i++)
			{
				this.VertexMarks[vertices[i]] = false;
			}
			for (i = 0; i < vertices2.Length; i++)
			{
				this.VertexMarks[vertices2[i]] = true;
			}
			i = 0;
			while (i < vertices.Length && this.VertexMarks[vertices[i]])
			{
				i++;
			}
			if (i == this.Dimension)
			{
				return;
			}
			for (int j = i + 1; j < vertices.Length; j++)
			{
				if (!this.VertexMarks[vertices[j]])
				{
					return;
				}
			}
			l.AdjacentFaces[i] = r.Index;
			for (i = 0; i < vertices.Length; i++)
			{
				this.VertexMarks[vertices[i]] = false;
			}
			i = 0;
			while (i < vertices2.Length && !this.VertexMarks[vertices2[i]])
			{
				i++;
			}
			r.AdjacentFaces[i] = l.Index;
		}

		// Token: 0x06008F33 RID: 36659 RVA: 0x001386C8 File Offset: 0x001368C8
		private void InitSingle()
		{
			int[] array = new int[this.Dimension];
			for (int i = 0; i < this.Vertices.Length; i++)
			{
				array[i] = i;
			}
			ConvexFaceInternal convexFaceInternal = this.FacePool[this.ObjectManager.GetFace()];
			convexFaceInternal.Vertices = array;
			Array.Sort<int>(array);
			this.MathHelper.CalculateFacePlane(convexFaceInternal, this.Center);
			if (convexFaceInternal.Normal[this.Dimension - 1] >= 0.0)
			{
				for (int j = 0; j < this.Dimension; j++)
				{
					convexFaceInternal.Normal[j] *= -1.0;
				}
				convexFaceInternal.Offset = 0.0 - convexFaceInternal.Offset;
				convexFaceInternal.IsNormalFlipped = !convexFaceInternal.IsNormalFlipped;
			}
			this.ConvexFaces.Add(convexFaceInternal.Index);
		}

		// Token: 0x06008F34 RID: 36660 RVA: 0x001387A8 File Offset: 0x001369A8
		private void InitConvexHull()
		{
			if (this.Vertices.Length < this.Dimension)
			{
				return;
			}
			if (this.Vertices.Length == this.Dimension)
			{
				this.InitSingle();
				return;
			}
			List<int> list = this.FindExtremes();
			List<int> list2 = this.FindInitialPoints(list);
			foreach (int num in list2)
			{
				int num2 = (this.CurrentVertex = num);
				this.UpdateCenter();
				this.VertexMarks[num2] = true;
			}
			foreach (int num3 in this.CreateInitialHull(list2))
			{
				ConvexFaceInternal convexFaceInternal = this.FacePool[num3];
				this.FindBeyondVertices(convexFaceInternal);
				if (convexFaceInternal.VerticesBeyond.Count == 0)
				{
					this.ConvexFaces.Add(convexFaceInternal.Index);
				}
				else
				{
					this.UnprocessedFaces.Add(convexFaceInternal);
				}
			}
			foreach (int num4 in list2)
			{
				this.VertexMarks[num4] = false;
			}
		}

		// Token: 0x06008F35 RID: 36661 RVA: 0x001388EC File Offset: 0x00136AEC
		private void FindBeyondVertices(ConvexFaceInternal face)
		{
			IndexBuffer verticesBeyond = face.VerticesBeyond;
			this.MaxDistance = double.NegativeInfinity;
			this.FurthestVertex = 0;
			int num = this.Vertices.Length;
			for (int i = 0; i < num; i++)
			{
				if (!this.VertexMarks[i])
				{
					this.IsBeyond(face, verticesBeyond, i);
				}
			}
			face.FurthestVertex = this.FurthestVertex;
		}

		// Token: 0x06008F36 RID: 36662 RVA: 0x0013894C File Offset: 0x00136B4C
		private List<int> FindInitialPoints(List<int> extremes)
		{
			List<int> list = new List<int>();
			int num = -1;
			int num2 = -1;
			double num3 = 0.0;
			double[] array = new double[this.Dimension];
			for (int i = 0; i < extremes.Count - 1; i++)
			{
				int num4 = extremes[i];
				for (int j = i + 1; j < extremes.Count; j++)
				{
					int num5 = extremes[j];
					this.MathHelper.SubtractFast(num4, num5, array);
					double num6 = MathHelper.LengthSquared(array);
					if (num6 > num3)
					{
						num = num4;
						num2 = num5;
						num3 = num6;
					}
				}
			}
			list.Add(num);
			list.Add(num2);
			for (int k = 2; k <= this.Dimension; k++)
			{
				double num7 = double.NegativeInfinity;
				int num8 = -1;
				for (int l = 0; l < extremes.Count; l++)
				{
					int num9 = extremes[l];
					if (!list.Contains(num9))
					{
						double squaredDistanceSum = this.GetSquaredDistanceSum(num9, list);
						if (squaredDistanceSum > num7)
						{
							num7 = squaredDistanceSum;
							num8 = num9;
						}
					}
				}
				if (num8 >= 0)
				{
					list.Add(num8);
				}
				else
				{
					int num10 = this.Vertices.Length;
					for (int m = 0; m < num10; m++)
					{
						if (!list.Contains(m))
						{
							double squaredDistanceSum2 = this.GetSquaredDistanceSum(m, list);
							if (squaredDistanceSum2 > num7)
							{
								num7 = squaredDistanceSum2;
								num8 = m;
							}
						}
					}
					if (num8 >= 0)
					{
						list.Add(num8);
					}
					else
					{
						this.ThrowSingular();
					}
				}
			}
			return list;
		}

		// Token: 0x06008F37 RID: 36663 RVA: 0x00138AC4 File Offset: 0x00136CC4
		private double GetSquaredDistanceSum(int pivot, List<int> initialPoints)
		{
			int count = initialPoints.Count;
			double num = 0.0;
			for (int i = 0; i < count; i++)
			{
				int num2 = initialPoints[i];
				for (int j = 0; j < this.Dimension; j++)
				{
					double num3 = this.GetCoordinate(num2, j) - this.GetCoordinate(pivot, j);
					num += num3 * num3;
				}
			}
			return num;
		}

		// Token: 0x06008F38 RID: 36664 RVA: 0x00138B2C File Offset: 0x00136D2C
		private int LexCompare(int u, int v)
		{
			int num = u * this.Dimension;
			int num2 = v * this.Dimension;
			for (int i = 0; i < this.Dimension; i++)
			{
				double num3 = this.Positions[num + i];
				double num4 = this.Positions[num2 + i];
				int num5 = num3.CompareTo(num4);
				if (num5 != 0)
				{
					return num5;
				}
			}
			return 0;
		}

		// Token: 0x06008F39 RID: 36665 RVA: 0x00138B88 File Offset: 0x00136D88
		private List<int> FindExtremes()
		{
			List<int> list = new List<int>(2 * this.Dimension);
			int num = this.Vertices.Length;
			for (int i = 0; i < this.Dimension; i++)
			{
				double num2 = double.MaxValue;
				double num3 = double.MinValue;
				int num4 = 0;
				int num5 = 0;
				for (int j = 0; j < num; j++)
				{
					double coordinate = this.GetCoordinate(j, i);
					double num6 = num2 - coordinate;
					if (num6 >= 0.0)
					{
						if (num6 < this.PlaneDistanceTolerance)
						{
							if (this.LexCompare(j, num4) < 0)
							{
								num2 = coordinate;
								num4 = j;
							}
						}
						else
						{
							num2 = coordinate;
							num4 = j;
						}
					}
					num6 = coordinate - num3;
					if (num6 >= 0.0)
					{
						if (num6 < this.PlaneDistanceTolerance)
						{
							if (this.LexCompare(j, num5) > 0)
							{
								num3 = coordinate;
								num5 = j;
							}
						}
						else
						{
							num3 = coordinate;
							num5 = j;
						}
					}
				}
				if (num4 != num5)
				{
					list.Add(num4);
					list.Add(num5);
				}
				else
				{
					list.Add(num4);
				}
			}
			HashSet<int> hashSet = new HashSet<int>();
			foreach (int num7 in list)
			{
				hashSet.Add(num7);
			}
			HashSet<int> hashSet2 = hashSet;
			if (hashSet2.Count <= this.Dimension)
			{
				int num8 = 0;
				while (num8 < num && hashSet2.Count <= this.Dimension)
				{
					hashSet2.Add(num8);
					num8++;
				}
			}
			return hashSet2.ToList<int>();
		}

		// Token: 0x06008F3A RID: 36666 RVA: 0x00055D29 File Offset: 0x00053F29
		private void ThrowSingular()
		{
			throw new InvalidOperationException("Singular input data (i.e. trying to triangulate a data that contain a regular lattice of points) detected. Introducing some noise to the data might resolve the issue.");
		}

		// Token: 0x06008F3B RID: 36667 RVA: 0x00138D28 File Offset: 0x00136F28
		internal static ConvexHull<TVertex, TFace> GetConvexHull<[Nullable(0)] TVertex, [Nullable(0)] TFace>(IList<TVertex> data, ConvexHullComputationConfig config) where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
		{
			if (config == null)
			{
				config = new ConvexHullComputationConfig();
			}
			IVertex[] array = new IVertex[data.Count];
			for (int i = 0; i < data.Count; i++)
			{
				array[i] = data[i];
			}
			ConvexHullInternal convexHullInternal = new ConvexHullInternal(array, false, config);
			convexHullInternal.FindConvexHull();
			TVertex[] hullVertices = convexHullInternal.GetHullVertices<TVertex>(data);
			return new ConvexHull<TVertex, TFace>
			{
				Points = hullVertices,
				Faces = convexHullInternal.GetConvexFaces<TVertex, TFace>()
			};
		}

		// Token: 0x06008F3C RID: 36668 RVA: 0x00138D9C File Offset: 0x00136F9C
		private TVertex[] GetHullVertices<[Nullable(2)] TVertex>(IList<TVertex> data)
		{
			int count = this.ConvexFaces.Count;
			int num = 0;
			int num2 = this.Vertices.Length;
			for (int i = 0; i < num2; i++)
			{
				this.VertexMarks[i] = false;
			}
			for (int j = 0; j < count; j++)
			{
				foreach (int num3 in this.FacePool[this.ConvexFaces[j]].Vertices)
				{
					if (!this.VertexMarks[num3])
					{
						this.VertexMarks[num3] = true;
						num++;
					}
				}
			}
			TVertex[] array = new TVertex[num];
			for (int l = 0; l < num2; l++)
			{
				if (this.VertexMarks[l])
				{
					array[--num] = data[l];
				}
			}
			return array;
		}

		// Token: 0x06008F3D RID: 36669 RVA: 0x00138E70 File Offset: 0x00137070
		private TFace[] GetConvexFaces<[Nullable(0)] TVertex, [Nullable(0)] TFace>() where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
		{
			IndexBuffer convexFaces = this.ConvexFaces;
			int count = convexFaces.Count;
			TFace[] array = new TFace[count];
			for (int i = 0; i < count; i++)
			{
				ConvexFaceInternal convexFaceInternal = this.FacePool[convexFaces[i]];
				TVertex[] array2 = new TVertex[this.Dimension];
				for (int j = 0; j < this.Dimension; j++)
				{
					array2[j] = (TVertex)((object)this.Vertices[convexFaceInternal.Vertices[j]]);
				}
				TFace[] array3 = array;
				int num = i;
				TFace tface = new TFace();
				tface.Vertices = array2;
				tface.Adjacency = new TFace[this.Dimension];
				tface.Normal = ((!this.IsLifted) ? convexFaceInternal.Normal : null);
				array3[num] = tface;
				convexFaceInternal.Tag = i;
			}
			for (int k = 0; k < count; k++)
			{
				ConvexFaceInternal convexFaceInternal2 = this.FacePool[convexFaces[k]];
				TFace tface2 = array[k];
				for (int l = 0; l < this.Dimension; l++)
				{
					if (convexFaceInternal2.AdjacentFaces[l] >= 0)
					{
						tface2.Adjacency[l] = array[this.FacePool[convexFaceInternal2.AdjacentFaces[l]].Tag];
					}
				}
				if (convexFaceInternal2.IsNormalFlipped)
				{
					TVertex tvertex = tface2.Vertices[0];
					tface2.Vertices[0] = tface2.Vertices[this.Dimension - 1];
					tface2.Vertices[this.Dimension - 1] = tvertex;
					TFace tface3 = tface2.Adjacency[0];
					tface2.Adjacency[0] = tface2.Adjacency[this.Dimension - 1];
					tface2.Adjacency[this.Dimension - 1] = tface3;
				}
			}
			return array;
		}

		// Token: 0x0400600E RID: 24590
		private const int ConnectorTableSize = 2017;

		// Token: 0x0400600F RID: 24591
		internal readonly int Dimension;

		// Token: 0x04006010 RID: 24592
		private readonly bool IsLifted;

		// Token: 0x04006011 RID: 24593
		private readonly double PlaneDistanceTolerance;

		// Token: 0x04006012 RID: 24594
		private IndexBuffer AffectedFaceBuffer;

		// Token: 0x04006013 RID: 24595
		internal bool[] AffectedFaceFlags;

		// Token: 0x04006014 RID: 24596
		private IndexBuffer BeyondBuffer;

		// Token: 0x04006015 RID: 24597
		private double[] Center;

		// Token: 0x04006016 RID: 24598
		private SimpleList<DeferredFace> ConeFaceBuffer;

		// Token: 0x04006017 RID: 24599
		private ConnectorList[] ConnectorTable;

		// Token: 0x04006018 RID: 24600
		private IndexBuffer ConvexFaces;

		// Token: 0x04006019 RID: 24601
		private int ConvexHullSize;

		// Token: 0x0400601A RID: 24602
		private int CurrentVertex;

		// Token: 0x0400601B RID: 24603
		private IndexBuffer EmptyBuffer;

		// Token: 0x0400601C RID: 24604
		internal ConvexFaceInternal[] FacePool;

		// Token: 0x0400601D RID: 24605
		private int FurthestVertex;

		// Token: 0x0400601E RID: 24606
		private MathHelper MathHelper;

		// Token: 0x0400601F RID: 24607
		private double MaxDistance;

		// Token: 0x04006020 RID: 24608
		private ObjectManager ObjectManager;

		// Token: 0x04006021 RID: 24609
		private double[] Positions;

		// Token: 0x04006022 RID: 24610
		private HashSet<int> SingularVertices;

		// Token: 0x04006023 RID: 24611
		private IndexBuffer TraverseStack;

		// Token: 0x04006024 RID: 24612
		private FaceList UnprocessedFaces;

		// Token: 0x04006025 RID: 24613
		private int[] UpdateBuffer;

		// Token: 0x04006026 RID: 24614
		private int[] UpdateIndices;

		// Token: 0x04006027 RID: 24615
		private bool[] VertexMarks;

		// Token: 0x04006028 RID: 24616
		private readonly IVertex[] Vertices;
	}
}
