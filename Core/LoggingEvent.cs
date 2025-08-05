using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using log4net.Repository;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A67 RID: 10855
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class LoggingEvent : ISerializable
	{
		// Token: 0x0600947E RID: 38014 RVA: 0x0014369C File Offset: 0x0014189C
		public LoggingEvent(Type callerStackBoundaryDeclaringType, ILoggerRepository repository, string loggerName, Level level, object message, Exception exception)
		{
			this.m_callerStackBoundaryDeclaringType = callerStackBoundaryDeclaringType;
			this.MessageObject = message;
			this.Repository = repository;
			this.ExceptionObject = exception;
			this.m_data.LoggerName = loggerName;
			this.m_data.Level = level;
			this.m_data.TimeStamp = DateTime.Now;
		}

		// Token: 0x0600947F RID: 38015 RVA: 0x00058BC5 File Offset: 0x00056DC5
		public LoggingEvent(Type callerStackBoundaryDeclaringType, ILoggerRepository repository, LoggingEventData data, FixFlags fixedData)
		{
			this.m_callerStackBoundaryDeclaringType = callerStackBoundaryDeclaringType;
			this.Repository = repository;
			this.m_data = data;
			this.m_fixFlags = fixedData;
		}

		// Token: 0x06009480 RID: 38016 RVA: 0x00058BF1 File Offset: 0x00056DF1
		public LoggingEvent(Type callerStackBoundaryDeclaringType, ILoggerRepository repository, LoggingEventData data)
			: this(callerStackBoundaryDeclaringType, repository, data, FixFlags.All)
		{
		}

		// Token: 0x06009481 RID: 38017 RVA: 0x00058C01 File Offset: 0x00056E01
		public LoggingEvent(LoggingEventData data)
			: this(null, null, data)
		{
		}

		// Token: 0x06009482 RID: 38018 RVA: 0x00143700 File Offset: 0x00141900
		protected LoggingEvent(SerializationInfo info, StreamingContext context)
		{
			this.m_data.LoggerName = info.GetString("LoggerName");
			this.m_data.Level = (Level)info.GetValue("Level", typeof(Level));
			this.m_data.Message = info.GetString("Message");
			this.m_data.ThreadName = info.GetString("ThreadName");
			this.m_data.TimeStamp = info.GetDateTime("TimeStamp");
			this.m_data.LocationInfo = (LocationInfo)info.GetValue("LocationInfo", typeof(LocationInfo));
			this.m_data.UserName = info.GetString("UserName");
			this.m_data.ExceptionString = info.GetString("ExceptionString");
			this.m_data.Properties = (PropertiesDictionary)info.GetValue("Properties", typeof(PropertiesDictionary));
			this.m_data.Domain = info.GetString("Domain");
			this.m_data.Identity = info.GetString("Identity");
			this.m_fixFlags = FixFlags.All;
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06009483 RID: 38019 RVA: 0x00058C0C File Offset: 0x00056E0C
		public static DateTime StartTime
		{
			get
			{
				return SystemInfo.ProcessStartTime;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06009484 RID: 38020 RVA: 0x00058C13 File Offset: 0x00056E13
		public Level Level
		{
			get
			{
				return this.m_data.Level;
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06009485 RID: 38021 RVA: 0x00058C20 File Offset: 0x00056E20
		public DateTime TimeStamp
		{
			get
			{
				return this.m_data.TimeStamp;
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06009486 RID: 38022 RVA: 0x00058C2D File Offset: 0x00056E2D
		public string LoggerName
		{
			get
			{
				return this.m_data.LoggerName;
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06009487 RID: 38023 RVA: 0x00058C3A File Offset: 0x00056E3A
		public LocationInfo LocationInformation
		{
			get
			{
				if (this.m_data.LocationInfo == null && this.m_cacheUpdatable)
				{
					this.m_data.LocationInfo = new LocationInfo(this.m_callerStackBoundaryDeclaringType);
				}
				return this.m_data.LocationInfo;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06009488 RID: 38024 RVA: 0x00058C72 File Offset: 0x00056E72
		public object MessageObject { get; }

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06009489 RID: 38025 RVA: 0x00058C7A File Offset: 0x00056E7A
		public Exception ExceptionObject { get; }

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x0600948A RID: 38026 RVA: 0x00058C82 File Offset: 0x00056E82
		// (set) Token: 0x0600948B RID: 38027 RVA: 0x00058C8A File Offset: 0x00056E8A
		public ILoggerRepository Repository { get; private set; }

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x0600948C RID: 38028 RVA: 0x00143844 File Offset: 0x00141A44
		public string RenderedMessage
		{
			get
			{
				if (this.m_data.Message != null || !this.m_cacheUpdatable)
				{
					return this.m_data.Message;
				}
				LoggingEventData data = this.m_data;
				object messageObject = this.MessageObject;
				string text2;
				if (messageObject != null)
				{
					string text = messageObject as string;
					if (text == null)
					{
						text2 = ((this.Repository == null) ? this.MessageObject.ToString() : this.Repository.RendererMap.FindAndRender(this.MessageObject));
					}
					else
					{
						text2 = text;
					}
				}
				else
				{
					text2 = string.Empty;
				}
				this.m_data.Message = text2;
				return this.m_data.Message;
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x0600948D RID: 38029 RVA: 0x001438E0 File Offset: 0x00141AE0
		public string ThreadName
		{
			get
			{
				if (this.m_data.ThreadName == null && this.m_cacheUpdatable)
				{
					this.m_data.ThreadName = Thread.CurrentThread.Name;
					if (string.IsNullOrEmpty(this.m_data.ThreadName))
					{
						try
						{
							this.m_data.ThreadName = SystemInfo.CurrentThreadId.ToString(NumberFormatInfo.InvariantInfo);
						}
						catch (SecurityException)
						{
							LogLog.Debug(LoggingEvent.declaringType, "Security exception while trying to get current thread ID. Error Ignored. Empty thread name.");
							this.m_data.ThreadName = Thread.CurrentThread.GetHashCode().ToString(CultureInfo.InvariantCulture);
						}
					}
				}
				return this.m_data.ThreadName;
			}
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x0600948E RID: 38030 RVA: 0x00058C93 File Offset: 0x00056E93
		public string UserName
		{
			get
			{
				if (this.m_data.UserName == null && this.m_cacheUpdatable)
				{
					this.m_data.UserName = SystemInfo.NotAvailableText;
				}
				return this.m_data.UserName;
			}
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x0600948F RID: 38031 RVA: 0x00058CC5 File Offset: 0x00056EC5
		public string Identity
		{
			get
			{
				if (this.m_data.Identity == null && this.m_cacheUpdatable)
				{
					this.m_data.Identity = SystemInfo.NotAvailableText;
				}
				return this.m_data.Identity;
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06009490 RID: 38032 RVA: 0x00058CF7 File Offset: 0x00056EF7
		public string Domain
		{
			get
			{
				if (this.m_data.Domain == null && this.m_cacheUpdatable)
				{
					this.m_data.Domain = SystemInfo.ApplicationFriendlyName;
				}
				return this.m_data.Domain;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06009491 RID: 38033 RVA: 0x00058D29 File Offset: 0x00056F29
		public PropertiesDictionary Properties
		{
			get
			{
				if (this.m_data.Properties != null)
				{
					return this.m_data.Properties;
				}
				if (this.m_eventProperties == null)
				{
					this.m_eventProperties = new PropertiesDictionary();
				}
				return this.m_eventProperties;
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06009492 RID: 38034 RVA: 0x00058D5D File Offset: 0x00056F5D
		// (set) Token: 0x06009493 RID: 38035 RVA: 0x00058D65 File Offset: 0x00056F65
		public FixFlags Fix
		{
			get
			{
				return this.m_fixFlags;
			}
			set
			{
				this.FixVolatileData(value);
			}
		}

		// Token: 0x06009494 RID: 38036 RVA: 0x0014399C File Offset: 0x00141B9C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("LoggerName", this.m_data.LoggerName);
			info.AddValue("Level", this.m_data.Level);
			info.AddValue("Message", this.m_data.Message);
			info.AddValue("ThreadName", this.m_data.ThreadName);
			info.AddValue("TimeStamp", this.m_data.TimeStamp);
			info.AddValue("LocationInfo", this.m_data.LocationInfo);
			info.AddValue("UserName", this.m_data.UserName);
			info.AddValue("ExceptionString", this.m_data.ExceptionString);
			info.AddValue("Properties", this.m_data.Properties);
			info.AddValue("Domain", this.m_data.Domain);
			info.AddValue("Identity", this.m_data.Identity);
		}

		// Token: 0x06009495 RID: 38037 RVA: 0x00058D6E File Offset: 0x00056F6E
		internal void EnsureRepository(ILoggerRepository repository)
		{
			if (repository != null)
			{
				this.Repository = repository;
			}
		}

		// Token: 0x06009496 RID: 38038 RVA: 0x00143A9C File Offset: 0x00141C9C
		public void WriteRenderedMessage(TextWriter writer)
		{
			if (this.m_data.Message != null)
			{
				writer.Write(this.m_data.Message);
				return;
			}
			if (this.MessageObject != null)
			{
				if (this.MessageObject is string)
				{
					writer.Write(this.MessageObject as string);
					return;
				}
				if (this.Repository != null)
				{
					this.Repository.RendererMap.FindAndRender(this.MessageObject, writer);
					return;
				}
				writer.Write(this.MessageObject.ToString());
			}
		}

		// Token: 0x06009497 RID: 38039 RVA: 0x00058D7A File Offset: 0x00056F7A
		public LoggingEventData GetLoggingEventData()
		{
			return this.GetLoggingEventData(FixFlags.Partial);
		}

		// Token: 0x06009498 RID: 38040 RVA: 0x00058D87 File Offset: 0x00056F87
		public LoggingEventData GetLoggingEventData(FixFlags fixFlags)
		{
			this.Fix = fixFlags;
			return this.m_data;
		}

		// Token: 0x06009499 RID: 38041 RVA: 0x00058D96 File Offset: 0x00056F96
		[Obsolete("Use GetExceptionString instead")]
		public string GetExceptionStrRep()
		{
			return this.GetExceptionString();
		}

		// Token: 0x0600949A RID: 38042 RVA: 0x00143B20 File Offset: 0x00141D20
		public string GetExceptionString()
		{
			if (this.m_data.ExceptionString == null && this.m_cacheUpdatable)
			{
				if (this.ExceptionObject != null)
				{
					if (this.Repository != null)
					{
						this.m_data.ExceptionString = this.Repository.RendererMap.FindAndRender(this.ExceptionObject);
					}
					else
					{
						this.m_data.ExceptionString = this.ExceptionObject.ToString();
					}
				}
				else
				{
					this.m_data.ExceptionString = string.Empty;
				}
			}
			return this.m_data.ExceptionString;
		}

		// Token: 0x0600949B RID: 38043 RVA: 0x00058D9E File Offset: 0x00056F9E
		[Obsolete("Use Fix property")]
		public void FixVolatileData()
		{
			this.Fix = FixFlags.All;
		}

		// Token: 0x0600949C RID: 38044 RVA: 0x00058DAB File Offset: 0x00056FAB
		[Obsolete("Use Fix property")]
		public void FixVolatileData(bool fastButLoose)
		{
			if (fastButLoose)
			{
				this.Fix = FixFlags.Partial;
				return;
			}
			this.Fix = FixFlags.All;
		}

		// Token: 0x0600949D RID: 38045 RVA: 0x00143BA8 File Offset: 0x00141DA8
		protected void FixVolatileData(FixFlags flags)
		{
			this.m_cacheUpdatable = true;
			FixFlags fixFlags = (flags ^ this.m_fixFlags) & flags;
			if (fixFlags > FixFlags.None)
			{
				if ((fixFlags & FixFlags.Message) != FixFlags.None)
				{
					object obj = this.RenderedMessage;
					this.m_fixFlags |= FixFlags.Message;
				}
				if ((fixFlags & FixFlags.ThreadName) != FixFlags.None)
				{
					object obj = this.ThreadName;
					this.m_fixFlags |= FixFlags.ThreadName;
				}
				if ((fixFlags & FixFlags.LocationInfo) != FixFlags.None)
				{
					object obj = this.LocationInformation;
					this.m_fixFlags |= FixFlags.LocationInfo;
				}
				if ((fixFlags & FixFlags.UserName) != FixFlags.None)
				{
					object obj = this.UserName;
					this.m_fixFlags |= FixFlags.UserName;
				}
				if ((fixFlags & FixFlags.Domain) != FixFlags.None)
				{
					object obj = this.Domain;
					this.m_fixFlags |= FixFlags.Domain;
				}
				if ((fixFlags & FixFlags.Identity) != FixFlags.None)
				{
					object obj = this.Identity;
					this.m_fixFlags |= FixFlags.Identity;
				}
				if ((fixFlags & FixFlags.Exception) != FixFlags.None)
				{
					object obj = this.GetExceptionString();
					this.m_fixFlags |= FixFlags.Exception;
				}
				if ((fixFlags & FixFlags.Properties) != FixFlags.None)
				{
					this.CacheProperties();
					this.m_fixFlags |= FixFlags.Properties;
				}
			}
			this.m_cacheUpdatable = false;
		}

		// Token: 0x0600949E RID: 38046 RVA: 0x00143CC8 File Offset: 0x00141EC8
		private void CreateCompositeProperties()
		{
			this.m_compositeProperties = new CompositeProperties();
			if (this.m_eventProperties != null)
			{
				this.m_compositeProperties.Add(this.m_eventProperties);
			}
			PropertiesDictionary properties = ThreadContext.Properties.GetProperties(false);
			if (properties != null)
			{
				this.m_compositeProperties.Add(properties);
			}
			PropertiesDictionary propertiesDictionary = new PropertiesDictionary();
			propertiesDictionary["log4net:UserName"] = this.UserName;
			propertiesDictionary["log4net:Identity"] = this.Identity;
			PropertiesDictionary propertiesDictionary2 = propertiesDictionary;
			this.m_compositeProperties.Add(propertiesDictionary2);
			this.m_compositeProperties.Add(GlobalContext.Properties.GetReadOnlyProperties());
		}

		// Token: 0x0600949F RID: 38047 RVA: 0x00143D60 File Offset: 0x00141F60
		private void CacheProperties()
		{
			if (this.m_data.Properties != null || !this.m_cacheUpdatable)
			{
				return;
			}
			if (this.m_compositeProperties == null)
			{
				this.CreateCompositeProperties();
			}
			IEnumerable enumerable = this.m_compositeProperties.Flatten();
			PropertiesDictionary propertiesDictionary = new PropertiesDictionary();
			foreach (object obj in enumerable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key as string;
				if (text != null)
				{
					object obj2 = dictionaryEntry.Value;
					IFixingRequired fixingRequired = obj2 as IFixingRequired;
					if (fixingRequired != null)
					{
						obj2 = fixingRequired.GetFixedObject();
					}
					if (obj2 != null)
					{
						propertiesDictionary[text] = obj2;
					}
				}
			}
			this.m_data.Properties = propertiesDictionary;
		}

		// Token: 0x060094A0 RID: 38048 RVA: 0x00058DC7 File Offset: 0x00056FC7
		public object LookupProperty(string key)
		{
			if (this.m_data.Properties != null)
			{
				return this.m_data.Properties[key];
			}
			if (this.m_compositeProperties == null)
			{
				this.CreateCompositeProperties();
			}
			return this.m_compositeProperties[key];
		}

		// Token: 0x060094A1 RID: 38049 RVA: 0x00058E02 File Offset: 0x00057002
		public PropertiesDictionary GetProperties()
		{
			if (this.m_data.Properties != null)
			{
				return this.m_data.Properties;
			}
			if (this.m_compositeProperties == null)
			{
				this.CreateCompositeProperties();
			}
			return this.m_compositeProperties.Flatten();
		}

		// Token: 0x04006233 RID: 25139
		public const string HostNameProperty = "log4net:HostName";

		// Token: 0x04006234 RID: 25140
		public const string IdentityProperty = "log4net:Identity";

		// Token: 0x04006235 RID: 25141
		public const string UserNameProperty = "log4net:UserName";

		// Token: 0x04006236 RID: 25142
		private static readonly Type declaringType = typeof(LoggingEvent);

		// Token: 0x04006237 RID: 25143
		private readonly Type m_callerStackBoundaryDeclaringType;

		// Token: 0x04006238 RID: 25144
		private bool m_cacheUpdatable = true;

		// Token: 0x04006239 RID: 25145
		private CompositeProperties m_compositeProperties;

		// Token: 0x0400623A RID: 25146
		private LoggingEventData m_data;

		// Token: 0x0400623B RID: 25147
		private PropertiesDictionary m_eventProperties;

		// Token: 0x0400623C RID: 25148
		private FixFlags m_fixFlags;
	}
}
