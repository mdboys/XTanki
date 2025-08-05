using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout
{
	// Token: 0x02002A23 RID: 10787
	[NullableContext(1)]
	[Nullable(0)]
	public class XmlLayout : XmlLayoutBase
	{
		// Token: 0x06009327 RID: 37671 RVA: 0x001413F8 File Offset: 0x0013F5F8
		public XmlLayout()
		{
		}

		// Token: 0x06009328 RID: 37672 RVA: 0x00141458 File Offset: 0x0013F658
		public XmlLayout(bool locationInfo)
			: base(locationInfo)
		{
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06009329 RID: 37673 RVA: 0x00057F4F File Offset: 0x0005614F
		// (set) Token: 0x0600932A RID: 37674 RVA: 0x00057F57 File Offset: 0x00056157
		public string Prefix { get; set; } = "log4net";

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x0600932B RID: 37675 RVA: 0x00057F60 File Offset: 0x00056160
		// (set) Token: 0x0600932C RID: 37676 RVA: 0x00057F68 File Offset: 0x00056168
		public bool Base64EncodeMessage { get; set; }

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x0600932D RID: 37677 RVA: 0x00057F71 File Offset: 0x00056171
		// (set) Token: 0x0600932E RID: 37678 RVA: 0x00057F79 File Offset: 0x00056179
		public bool Base64EncodeProperties { get; set; }

		// Token: 0x0600932F RID: 37679 RVA: 0x001414BC File Offset: 0x0013F6BC
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			string prefix = this.Prefix;
			if (prefix != null && prefix.Length > 0)
			{
				this.m_elmEvent = this.Prefix + ":event";
				this.m_elmMessage = this.Prefix + ":message";
				this.m_elmProperties = this.Prefix + ":properties";
				this.m_elmData = this.Prefix + ":data";
				this.m_elmException = this.Prefix + ":exception";
				this.m_elmLocation = this.Prefix + ":locationInfo";
			}
		}

		// Token: 0x06009330 RID: 37680 RVA: 0x0014156C File Offset: 0x0013F76C
		protected override void FormatXml(XmlWriter writer, LoggingEvent loggingEvent)
		{
			writer.WriteStartElement(this.m_elmEvent);
			writer.WriteAttributeString("logger", loggingEvent.LoggerName);
			writer.WriteAttributeString("timestamp", XmlConvert.ToString(loggingEvent.TimeStamp, XmlDateTimeSerializationMode.Local));
			writer.WriteAttributeString("level", loggingEvent.Level.DisplayName);
			writer.WriteAttributeString("thread", loggingEvent.ThreadName);
			string text = loggingEvent.Domain;
			if (text != null && text.Length > 0)
			{
				writer.WriteAttributeString("domain", loggingEvent.Domain);
			}
			text = loggingEvent.Identity;
			if (text != null && text.Length > 0)
			{
				writer.WriteAttributeString("identity", loggingEvent.Identity);
			}
			text = loggingEvent.UserName;
			if (text != null && text.Length > 0)
			{
				writer.WriteAttributeString("username", loggingEvent.UserName);
			}
			writer.WriteStartElement(this.m_elmMessage);
			if (!this.Base64EncodeMessage)
			{
				Transform.WriteEscapedXmlString(writer, loggingEvent.RenderedMessage, base.InvalidCharReplacement);
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes(loggingEvent.RenderedMessage);
				string text2 = Convert.ToBase64String(bytes, 0, bytes.Length);
				Transform.WriteEscapedXmlString(writer, text2, base.InvalidCharReplacement);
			}
			writer.WriteEndElement();
			PropertiesDictionary properties = loggingEvent.GetProperties();
			if (properties.Count > 0)
			{
				writer.WriteStartElement(this.m_elmProperties);
				foreach (object obj in ((IEnumerable)properties))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					writer.WriteStartElement(this.m_elmData);
					writer.WriteAttributeString("name", Transform.MaskXmlInvalidCharacters((string)dictionaryEntry.Key, base.InvalidCharReplacement));
					string text3;
					if (!this.Base64EncodeProperties)
					{
						text3 = Transform.MaskXmlInvalidCharacters(loggingEvent.Repository.RendererMap.FindAndRender(dictionaryEntry.Value), base.InvalidCharReplacement);
					}
					else
					{
						byte[] bytes2 = Encoding.UTF8.GetBytes(loggingEvent.Repository.RendererMap.FindAndRender(dictionaryEntry.Value));
						text3 = Convert.ToBase64String(bytes2, 0, bytes2.Length);
					}
					writer.WriteAttributeString("value", text3);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			string exceptionString = loggingEvent.GetExceptionString();
			if (exceptionString != null && exceptionString.Length > 0)
			{
				writer.WriteStartElement(this.m_elmException);
				Transform.WriteEscapedXmlString(writer, exceptionString, base.InvalidCharReplacement);
				writer.WriteEndElement();
			}
			if (base.LocationInfo)
			{
				LocationInfo locationInformation = loggingEvent.LocationInformation;
				writer.WriteStartElement(this.m_elmLocation);
				writer.WriteAttributeString("class", locationInformation.ClassName);
				writer.WriteAttributeString("method", locationInformation.MethodName);
				writer.WriteAttributeString("file", locationInformation.FileName);
				writer.WriteAttributeString("line", locationInformation.LineNumber);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x040061A3 RID: 24995
		private const string PREFIX = "log4net";

		// Token: 0x040061A4 RID: 24996
		private const string ELM_EVENT = "event";

		// Token: 0x040061A5 RID: 24997
		private const string ELM_MESSAGE = "message";

		// Token: 0x040061A6 RID: 24998
		private const string ELM_PROPERTIES = "properties";

		// Token: 0x040061A7 RID: 24999
		private const string ELM_GLOBAL_PROPERTIES = "global-properties";

		// Token: 0x040061A8 RID: 25000
		private const string ELM_DATA = "data";

		// Token: 0x040061A9 RID: 25001
		private const string ELM_EXCEPTION = "exception";

		// Token: 0x040061AA RID: 25002
		private const string ELM_LOCATION = "locationInfo";

		// Token: 0x040061AB RID: 25003
		private const string ATTR_LOGGER = "logger";

		// Token: 0x040061AC RID: 25004
		private const string ATTR_TIMESTAMP = "timestamp";

		// Token: 0x040061AD RID: 25005
		private const string ATTR_LEVEL = "level";

		// Token: 0x040061AE RID: 25006
		private const string ATTR_THREAD = "thread";

		// Token: 0x040061AF RID: 25007
		private const string ATTR_DOMAIN = "domain";

		// Token: 0x040061B0 RID: 25008
		private const string ATTR_IDENTITY = "identity";

		// Token: 0x040061B1 RID: 25009
		private const string ATTR_USERNAME = "username";

		// Token: 0x040061B2 RID: 25010
		private const string ATTR_CLASS = "class";

		// Token: 0x040061B3 RID: 25011
		private const string ATTR_METHOD = "method";

		// Token: 0x040061B4 RID: 25012
		private const string ATTR_FILE = "file";

		// Token: 0x040061B5 RID: 25013
		private const string ATTR_LINE = "line";

		// Token: 0x040061B6 RID: 25014
		private const string ATTR_NAME = "name";

		// Token: 0x040061B7 RID: 25015
		private const string ATTR_VALUE = "value";

		// Token: 0x040061B8 RID: 25016
		private string m_elmData = "data";

		// Token: 0x040061B9 RID: 25017
		private string m_elmEvent = "event";

		// Token: 0x040061BA RID: 25018
		private string m_elmException = "exception";

		// Token: 0x040061BB RID: 25019
		private string m_elmLocation = "locationInfo";

		// Token: 0x040061BC RID: 25020
		private string m_elmMessage = "message";

		// Token: 0x040061BD RID: 25021
		private string m_elmProperties = "properties";
	}
}
