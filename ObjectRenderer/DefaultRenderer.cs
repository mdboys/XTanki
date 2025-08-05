using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Util;

namespace log4net.ObjectRenderer
{
	// Token: 0x02002A14 RID: 10772
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefaultRenderer : IObjectRenderer
	{
		// Token: 0x060092E3 RID: 37603 RVA: 0x001409BC File Offset: 0x0013EBBC
		public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
		{
			if (rendererMap == null)
			{
				throw new ArgumentNullException("rendererMap");
			}
			if (obj == null)
			{
				writer.Write(SystemInfo.NullText);
				return;
			}
			Array array = obj as Array;
			if (array != null)
			{
				this.RenderArray(rendererMap, array, writer);
				return;
			}
			IEnumerable enumerable = obj as IEnumerable;
			if (enumerable != null)
			{
				ICollection collection = obj as ICollection;
				if (collection != null && collection.Count == 0)
				{
					writer.Write("{}");
					return;
				}
				IDictionary dictionary = obj as IDictionary;
				if (dictionary != null)
				{
					this.RenderEnumerator(rendererMap, dictionary.GetEnumerator(), writer);
					return;
				}
				this.RenderEnumerator(rendererMap, enumerable.GetEnumerator(), writer);
				return;
			}
			else
			{
				IEnumerator enumerator = obj as IEnumerator;
				if (enumerator != null)
				{
					this.RenderEnumerator(rendererMap, enumerator, writer);
					return;
				}
				if (obj is DictionaryEntry)
				{
					this.RenderDictionaryEntry(rendererMap, (DictionaryEntry)obj, writer);
					return;
				}
				string text = obj.ToString();
				writer.Write((text != null) ? text : SystemInfo.NullText);
				return;
			}
		}

		// Token: 0x060092E4 RID: 37604 RVA: 0x00140A94 File Offset: 0x0013EC94
		private void RenderArray(RendererMap rendererMap, Array array, TextWriter writer)
		{
			if (array.Rank != 1)
			{
				writer.Write(array.ToString());
				return;
			}
			writer.Write(array.GetType().Name + " {");
			int length = array.Length;
			if (length > 0)
			{
				rendererMap.FindAndRender(array.GetValue(0), writer);
				for (int i = 1; i < length; i++)
				{
					writer.Write(", ");
					rendererMap.FindAndRender(array.GetValue(i), writer);
				}
			}
			writer.Write("}");
		}

		// Token: 0x060092E5 RID: 37605 RVA: 0x00140B1C File Offset: 0x0013ED1C
		private void RenderEnumerator(RendererMap rendererMap, IEnumerator enumerator, TextWriter writer)
		{
			writer.Write("{");
			if (enumerator != null && enumerator.MoveNext())
			{
				rendererMap.FindAndRender(enumerator.Current, writer);
				while (enumerator.MoveNext())
				{
					writer.Write(", ");
					rendererMap.FindAndRender(enumerator.Current, writer);
				}
			}
			writer.Write("}");
		}

		// Token: 0x060092E6 RID: 37606 RVA: 0x00057C8A File Offset: 0x00055E8A
		private void RenderDictionaryEntry(RendererMap rendererMap, DictionaryEntry entry, TextWriter writer)
		{
			rendererMap.FindAndRender(entry.Key, writer);
			writer.Write("=");
			rendererMap.FindAndRender(entry.Value, writer);
		}
	}
}
