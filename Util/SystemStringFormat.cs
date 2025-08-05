using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace log4net.Util
{
	// Token: 0x020029D7 RID: 10711
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SystemStringFormat
	{
		// Token: 0x06009149 RID: 37193 RVA: 0x00056F41 File Offset: 0x00055141
		public SystemStringFormat(IFormatProvider provider, string format, params object[] args)
		{
			this.m_provider = provider;
			this.m_format = format;
			this.m_args = args;
		}

		// Token: 0x0600914A RID: 37194 RVA: 0x00056F5E File Offset: 0x0005515E
		public override string ToString()
		{
			return SystemStringFormat.StringFormat(this.m_provider, this.m_format, this.m_args);
		}

		// Token: 0x0600914B RID: 37195 RVA: 0x0013D220 File Offset: 0x0013B420
		private static string StringFormat(IFormatProvider provider, string format, params object[] args)
		{
			string text;
			try
			{
				if (format == null)
				{
					text = null;
				}
				else if (args == null)
				{
					text = format;
				}
				else
				{
					text = string.Format(provider, format, args);
				}
			}
			catch (Exception ex)
			{
				LogLog.Warn(SystemStringFormat.declaringType, "Exception while rendering format [" + format + "]", ex);
				text = SystemStringFormat.StringFormatError(ex, format, args);
			}
			return text;
		}

		// Token: 0x0600914C RID: 37196 RVA: 0x0013D280 File Offset: 0x0013B480
		private static string StringFormatError(Exception formatException, string format, object[] args)
		{
			string text;
			try
			{
				StringBuilder stringBuilder = new StringBuilder("<log4net.Error>");
				if (formatException != null)
				{
					stringBuilder.Append("Exception during StringFormat: ").Append(formatException.Message);
				}
				else
				{
					stringBuilder.Append("Exception during StringFormat");
				}
				stringBuilder.Append(" <format>").Append(format).Append("</format>");
				stringBuilder.Append("<args>");
				SystemStringFormat.RenderArray(args, stringBuilder);
				stringBuilder.Append("</args>");
				stringBuilder.Append("</log4net.Error>");
				text = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				LogLog.Error(SystemStringFormat.declaringType, "INTERNAL ERROR during StringFormat error handling", ex);
				text = "<log4net.Error>Exception during StringFormat. See Internal Log.</log4net.Error>";
			}
			return text;
		}

		// Token: 0x0600914D RID: 37197 RVA: 0x0013D33C File Offset: 0x0013B53C
		private static void RenderArray(Array array, StringBuilder buffer)
		{
			if (array == null)
			{
				buffer.Append(SystemInfo.NullText);
				return;
			}
			if (array.Rank != 1)
			{
				buffer.Append(array);
				return;
			}
			buffer.Append("{");
			int length = array.Length;
			if (length > 0)
			{
				SystemStringFormat.RenderObject(array.GetValue(0), buffer);
				for (int i = 1; i < length; i++)
				{
					buffer.Append(", ");
					SystemStringFormat.RenderObject(array.GetValue(i), buffer);
				}
			}
			buffer.Append("}");
		}

		// Token: 0x0600914E RID: 37198 RVA: 0x0013D3C0 File Offset: 0x0013B5C0
		private static void RenderObject(object obj, StringBuilder buffer)
		{
			if (obj == null)
			{
				buffer.Append(SystemInfo.NullText);
				return;
			}
			try
			{
				buffer.Append(obj);
			}
			catch (Exception ex)
			{
				buffer.Append("<Exception: ").Append(ex.Message).Append(">");
			}
		}

		// Token: 0x04006125 RID: 24869
		private static readonly Type declaringType = typeof(SystemStringFormat);

		// Token: 0x04006126 RID: 24870
		private readonly object[] m_args;

		// Token: 0x04006127 RID: 24871
		private readonly string m_format;

		// Token: 0x04006128 RID: 24872
		private readonly IFormatProvider m_provider;
	}
}
