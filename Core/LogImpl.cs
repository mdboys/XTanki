using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using log4net.Repository;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02002A69 RID: 10857
	[NullableContext(1)]
	[Nullable(0)]
	public class LogImpl : LoggerWrapperImpl, ILog, ILoggerWrapper
	{
		// Token: 0x060094A3 RID: 38051 RVA: 0x00058E47 File Offset: 0x00057047
		public LogImpl(ILogger logger)
			: base(logger)
		{
			logger.Repository.ConfigurationChanged += this.LoggerRepositoryConfigurationChanged;
			this.ReloadLevels(logger.Repository);
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x060094A4 RID: 38052 RVA: 0x00058E73 File Offset: 0x00057073
		public virtual bool IsDebugEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelDebug);
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x060094A5 RID: 38053 RVA: 0x00058E86 File Offset: 0x00057086
		public virtual bool IsInfoEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelInfo);
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x060094A6 RID: 38054 RVA: 0x00058E99 File Offset: 0x00057099
		public virtual bool IsWarnEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelWarn);
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x060094A7 RID: 38055 RVA: 0x00058EAC File Offset: 0x000570AC
		public virtual bool IsErrorEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelError);
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x060094A8 RID: 38056 RVA: 0x00058EBF File Offset: 0x000570BF
		public virtual bool IsFatalEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelFatal);
			}
		}

		// Token: 0x060094A9 RID: 38057 RVA: 0x00058ED2 File Offset: 0x000570D2
		public virtual void Debug(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, message, null);
		}

		// Token: 0x060094AA RID: 38058 RVA: 0x00058EEC File Offset: 0x000570EC
		public virtual void Debug(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, message, exception);
		}

		// Token: 0x060094AB RID: 38059 RVA: 0x00058F06 File Offset: 0x00057106
		public virtual void DebugFormat(string format, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x060094AC RID: 38060 RVA: 0x00143E2C File Offset: 0x0014202C
		public virtual void DebugFormat(string format, object arg0)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		// Token: 0x060094AD RID: 38061 RVA: 0x00143E70 File Offset: 0x00142070
		public virtual void DebugFormat(string format, object arg0, object arg1)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		// Token: 0x060094AE RID: 38062 RVA: 0x00143EB8 File Offset: 0x001420B8
		public virtual void DebugFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		// Token: 0x060094AF RID: 38063 RVA: 0x00058F33 File Offset: 0x00057133
		public virtual void DebugFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x060094B0 RID: 38064 RVA: 0x00058F5C File Offset: 0x0005715C
		public virtual void Info(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, message, null);
		}

		// Token: 0x060094B1 RID: 38065 RVA: 0x00058F76 File Offset: 0x00057176
		public virtual void Info(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, message, exception);
		}

		// Token: 0x060094B2 RID: 38066 RVA: 0x00058F90 File Offset: 0x00057190
		public virtual void InfoFormat(string format, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x060094B3 RID: 38067 RVA: 0x00143F04 File Offset: 0x00142104
		public virtual void InfoFormat(string format, object arg0)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		// Token: 0x060094B4 RID: 38068 RVA: 0x00143F48 File Offset: 0x00142148
		public virtual void InfoFormat(string format, object arg0, object arg1)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		// Token: 0x060094B5 RID: 38069 RVA: 0x00143F90 File Offset: 0x00142190
		public virtual void InfoFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		// Token: 0x060094B6 RID: 38070 RVA: 0x00058FBD File Offset: 0x000571BD
		public virtual void InfoFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x060094B7 RID: 38071 RVA: 0x00058FE6 File Offset: 0x000571E6
		public virtual void Warn(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, message, null);
		}

		// Token: 0x060094B8 RID: 38072 RVA: 0x00059000 File Offset: 0x00057200
		public virtual void Warn(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, message, exception);
		}

		// Token: 0x060094B9 RID: 38073 RVA: 0x0005901A File Offset: 0x0005721A
		public virtual void WarnFormat(string format, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x060094BA RID: 38074 RVA: 0x00143FDC File Offset: 0x001421DC
		public virtual void WarnFormat(string format, object arg0)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		// Token: 0x060094BB RID: 38075 RVA: 0x00144020 File Offset: 0x00142220
		public virtual void WarnFormat(string format, object arg0, object arg1)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		// Token: 0x060094BC RID: 38076 RVA: 0x00144068 File Offset: 0x00142268
		public virtual void WarnFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		// Token: 0x060094BD RID: 38077 RVA: 0x00059047 File Offset: 0x00057247
		public virtual void WarnFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x060094BE RID: 38078 RVA: 0x00059070 File Offset: 0x00057270
		public virtual void Error(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, message, null);
		}

		// Token: 0x060094BF RID: 38079 RVA: 0x0005908A File Offset: 0x0005728A
		public virtual void Error(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, message, exception);
		}

		// Token: 0x060094C0 RID: 38080 RVA: 0x000590A4 File Offset: 0x000572A4
		public virtual void ErrorFormat(string format, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x060094C1 RID: 38081 RVA: 0x001440B4 File Offset: 0x001422B4
		public virtual void ErrorFormat(string format, object arg0)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		// Token: 0x060094C2 RID: 38082 RVA: 0x001440F8 File Offset: 0x001422F8
		public virtual void ErrorFormat(string format, object arg0, object arg1)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		// Token: 0x060094C3 RID: 38083 RVA: 0x00144140 File Offset: 0x00142340
		public virtual void ErrorFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		// Token: 0x060094C4 RID: 38084 RVA: 0x000590D1 File Offset: 0x000572D1
		public virtual void ErrorFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x060094C5 RID: 38085 RVA: 0x000590FA File Offset: 0x000572FA
		public virtual void Fatal(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, message, null);
		}

		// Token: 0x060094C6 RID: 38086 RVA: 0x00059114 File Offset: 0x00057314
		public virtual void Fatal(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, message, exception);
		}

		// Token: 0x060094C7 RID: 38087 RVA: 0x0005912E File Offset: 0x0005732E
		public virtual void FatalFormat(string format, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x060094C8 RID: 38088 RVA: 0x0014418C File Offset: 0x0014238C
		public virtual void FatalFormat(string format, object arg0)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0 }), null);
			}
		}

		// Token: 0x060094C9 RID: 38089 RVA: 0x001441D0 File Offset: 0x001423D0
		public virtual void FatalFormat(string format, object arg0, object arg1)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1 }), null);
			}
		}

		// Token: 0x060094CA RID: 38090 RVA: 0x00144218 File Offset: 0x00142418
		public virtual void FatalFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2 }), null);
			}
		}

		// Token: 0x060094CB RID: 38091 RVA: 0x0005915B File Offset: 0x0005735B
		public virtual void FatalFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x060094CC RID: 38092 RVA: 0x00144264 File Offset: 0x00142464
		protected virtual void ReloadLevels(ILoggerRepository repository)
		{
			LevelMap levelMap = repository.LevelMap;
			this.m_levelDebug = levelMap.LookupWithDefault(Level.Debug);
			this.m_levelInfo = levelMap.LookupWithDefault(Level.Info);
			this.m_levelWarn = levelMap.LookupWithDefault(Level.Warn);
			this.m_levelError = levelMap.LookupWithDefault(Level.Error);
			this.m_levelFatal = levelMap.LookupWithDefault(Level.Fatal);
		}

		// Token: 0x060094CD RID: 38093 RVA: 0x001442D0 File Offset: 0x001424D0
		private void LoggerRepositoryConfigurationChanged(object sender, EventArgs e)
		{
			ILoggerRepository loggerRepository = sender as ILoggerRepository;
			if (loggerRepository != null)
			{
				this.ReloadLevels(loggerRepository);
			}
		}

		// Token: 0x0400624B RID: 25163
		private static readonly Type ThisDeclaringType = typeof(LogImpl);

		// Token: 0x0400624C RID: 25164
		private Level m_levelDebug;

		// Token: 0x0400624D RID: 25165
		private Level m_levelError;

		// Token: 0x0400624E RID: 25166
		private Level m_levelFatal;

		// Token: 0x0400624F RID: 25167
		private Level m_levelInfo;

		// Token: 0x04006250 RID: 25168
		private Level m_levelWarn;
	}
}
