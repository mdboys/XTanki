using System;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace log4net
{
	// Token: 0x020029B1 RID: 10673
	[NullableContext(1)]
	public interface ILog : ILoggerWrapper
	{
		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06008FB1 RID: 36785
		bool IsDebugEnabled { get; }

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06008FB2 RID: 36786
		bool IsInfoEnabled { get; }

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06008FB3 RID: 36787
		bool IsWarnEnabled { get; }

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06008FB4 RID: 36788
		bool IsErrorEnabled { get; }

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06008FB5 RID: 36789
		bool IsFatalEnabled { get; }

		// Token: 0x06008FB6 RID: 36790
		void Debug(object message);

		// Token: 0x06008FB7 RID: 36791
		void Debug(object message, Exception exception);

		// Token: 0x06008FB8 RID: 36792
		void DebugFormat(string format, params object[] args);

		// Token: 0x06008FB9 RID: 36793
		void DebugFormat(string format, object arg0);

		// Token: 0x06008FBA RID: 36794
		void DebugFormat(string format, object arg0, object arg1);

		// Token: 0x06008FBB RID: 36795
		void DebugFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x06008FBC RID: 36796
		void DebugFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x06008FBD RID: 36797
		void Info(object message);

		// Token: 0x06008FBE RID: 36798
		void Info(object message, Exception exception);

		// Token: 0x06008FBF RID: 36799
		void InfoFormat(string format, params object[] args);

		// Token: 0x06008FC0 RID: 36800
		void InfoFormat(string format, object arg0);

		// Token: 0x06008FC1 RID: 36801
		void InfoFormat(string format, object arg0, object arg1);

		// Token: 0x06008FC2 RID: 36802
		void InfoFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x06008FC3 RID: 36803
		void InfoFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x06008FC4 RID: 36804
		void Warn(object message);

		// Token: 0x06008FC5 RID: 36805
		void Warn(object message, Exception exception);

		// Token: 0x06008FC6 RID: 36806
		void WarnFormat(string format, params object[] args);

		// Token: 0x06008FC7 RID: 36807
		void WarnFormat(string format, object arg0);

		// Token: 0x06008FC8 RID: 36808
		void WarnFormat(string format, object arg0, object arg1);

		// Token: 0x06008FC9 RID: 36809
		void WarnFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x06008FCA RID: 36810
		void WarnFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x06008FCB RID: 36811
		void Error(object message);

		// Token: 0x06008FCC RID: 36812
		void Error(object message, Exception exception);

		// Token: 0x06008FCD RID: 36813
		void ErrorFormat(string format, params object[] args);

		// Token: 0x06008FCE RID: 36814
		void ErrorFormat(string format, object arg0);

		// Token: 0x06008FCF RID: 36815
		void ErrorFormat(string format, object arg0, object arg1);

		// Token: 0x06008FD0 RID: 36816
		void ErrorFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x06008FD1 RID: 36817
		void ErrorFormat(IFormatProvider provider, string format, params object[] args);

		// Token: 0x06008FD2 RID: 36818
		void Fatal(object message);

		// Token: 0x06008FD3 RID: 36819
		void Fatal(object message, Exception exception);

		// Token: 0x06008FD4 RID: 36820
		void FatalFormat(string format, params object[] args);

		// Token: 0x06008FD5 RID: 36821
		void FatalFormat(string format, object arg0);

		// Token: 0x06008FD6 RID: 36822
		void FatalFormat(string format, object arg0, object arg1);

		// Token: 0x06008FD7 RID: 36823
		void FatalFormat(string format, object arg0, object arg1, object arg2);

		// Token: 0x06008FD8 RID: 36824
		void FatalFormat(IFormatProvider provider, string format, params object[] args);
	}
}
