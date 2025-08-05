using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace log4net.Util
{
	// Token: 0x020029D6 RID: 10710
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SystemInfo
	{
		// Token: 0x0600912D RID: 37165 RVA: 0x00056E45 File Offset: 0x00055045
		static SystemInfo()
		{
			string text = "(null)";
			SystemInfo.NotAvailableText = "NOT AVAILABLE";
			SystemInfo.NullText = text;
		}

		// Token: 0x0600912E RID: 37166 RVA: 0x00005698 File Offset: 0x00003898
		private SystemInfo()
		{
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x0600912F RID: 37167 RVA: 0x00056E7F File Offset: 0x0005507F
		public static string NewLine
		{
			get
			{
				return Environment.NewLine;
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06009130 RID: 37168 RVA: 0x00056E86 File Offset: 0x00055086
		public static string ApplicationBaseDirectory
		{
			get
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06009131 RID: 37169 RVA: 0x00056E92 File Offset: 0x00055092
		public static string ConfigurationFileLocation
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06009132 RID: 37170 RVA: 0x00056EA3 File Offset: 0x000550A3
		public static string EntryAssemblyLocation
		{
			get
			{
				return "-";
			}
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06009133 RID: 37171 RVA: 0x00056EAA File Offset: 0x000550AA
		public static int CurrentThreadId
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06009134 RID: 37172 RVA: 0x0013CD54 File Offset: 0x0013AF54
		public static string HostName
		{
			get
			{
				if (SystemInfo.s_hostName == null)
				{
					try
					{
						SystemInfo.s_hostName = Dns.GetHostName();
					}
					catch (SocketException)
					{
						LogLog.Debug(SystemInfo.declaringType, "Socket exception occurred while getting the dns hostname. Error Ignored.");
					}
					catch (SecurityException)
					{
						LogLog.Debug(SystemInfo.declaringType, "Security exception occurred while getting the dns hostname. Error Ignored.");
					}
					catch (Exception ex)
					{
						LogLog.Debug(SystemInfo.declaringType, "Some other exception occurred while getting the dns hostname. Error Ignored.", ex);
					}
					if (SystemInfo.s_hostName == null || SystemInfo.s_hostName.Length == 0)
					{
						try
						{
							SystemInfo.s_hostName = Environment.MachineName;
						}
						catch (InvalidOperationException)
						{
						}
						catch (SecurityException)
						{
						}
					}
					if (SystemInfo.s_hostName == null || SystemInfo.s_hostName.Length == 0)
					{
						SystemInfo.s_hostName = SystemInfo.NotAvailableText;
						LogLog.Debug(SystemInfo.declaringType, "Could not determine the hostname. Error Ignored. Empty host name will be used");
					}
				}
				return SystemInfo.s_hostName;
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06009135 RID: 37173 RVA: 0x0013CE48 File Offset: 0x0013B048
		public static string ApplicationFriendlyName
		{
			get
			{
				if (SystemInfo.s_appFriendlyName == null)
				{
					try
					{
						SystemInfo.s_appFriendlyName = AppDomain.CurrentDomain.FriendlyName;
					}
					catch (SecurityException)
					{
						LogLog.Debug(SystemInfo.declaringType, "Security exception while trying to get current domain friendly name. Error Ignored.");
					}
					if (SystemInfo.s_appFriendlyName == null || SystemInfo.s_appFriendlyName.Length == 0)
					{
						try
						{
							SystemInfo.s_appFriendlyName = Path.GetFileName(SystemInfo.EntryAssemblyLocation);
						}
						catch (SecurityException)
						{
						}
					}
					if (SystemInfo.s_appFriendlyName == null || SystemInfo.s_appFriendlyName.Length == 0)
					{
						SystemInfo.s_appFriendlyName = SystemInfo.NotAvailableText;
					}
				}
				return SystemInfo.s_appFriendlyName;
			}
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06009136 RID: 37174 RVA: 0x00056EB6 File Offset: 0x000550B6
		public static DateTime ProcessStartTime { get; } = DateTime.Now;

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06009137 RID: 37175 RVA: 0x00056EBD File Offset: 0x000550BD
		// (set) Token: 0x06009138 RID: 37176 RVA: 0x00056EC4 File Offset: 0x000550C4
		public static string NullText { get; set; }

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06009139 RID: 37177 RVA: 0x00056ECC File Offset: 0x000550CC
		// (set) Token: 0x0600913A RID: 37178 RVA: 0x00056ED3 File Offset: 0x000550D3
		public static string NotAvailableText { get; set; }

		// Token: 0x0600913B RID: 37179 RVA: 0x0013CEE8 File Offset: 0x0013B0E8
		public static string AssemblyLocationInfo(Assembly myAssembly)
		{
			if (myAssembly.GlobalAssemblyCache)
			{
				return "Global Assembly Cache";
			}
			string text;
			try
			{
				if (myAssembly is AssemblyBuilder)
				{
					text = "Dynamic Assembly";
				}
				else if (myAssembly.GetType().FullName == "System.Reflection.Emit.InternalAssemblyBuilder")
				{
					text = "Dynamic Assembly";
				}
				else
				{
					text = myAssembly.Location;
				}
			}
			catch (NotSupportedException)
			{
				text = "Dynamic Assembly";
			}
			catch (TargetInvocationException ex)
			{
				text = "Location Detect Failed (" + ex.Message + ")";
			}
			catch (ArgumentException ex2)
			{
				text = "Location Detect Failed (" + ex2.Message + ")";
			}
			catch (SecurityException)
			{
				text = "Location Permission Denied";
			}
			return text;
		}

		// Token: 0x0600913C RID: 37180 RVA: 0x00056EDB File Offset: 0x000550DB
		public static string AssemblyQualifiedName(Type type)
		{
			return type.FullName + ", " + type.Assembly.FullName;
		}

		// Token: 0x0600913D RID: 37181 RVA: 0x0013CFB8 File Offset: 0x0013B1B8
		public static string AssemblyShortName(Assembly myAssembly)
		{
			string text = myAssembly.FullName;
			int num = text.IndexOf(',');
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			return text.Trim();
		}

		// Token: 0x0600913E RID: 37182 RVA: 0x00056EF8 File Offset: 0x000550F8
		public static string AssemblyFileName(Assembly myAssembly)
		{
			return Path.GetFileName(myAssembly.Location);
		}

		// Token: 0x0600913F RID: 37183 RVA: 0x00056F05 File Offset: 0x00055105
		public static Type GetTypeFromString(Type relativeType, string typeName, bool throwOnError, bool ignoreCase)
		{
			return SystemInfo.GetTypeFromString(relativeType.Assembly, typeName, throwOnError, ignoreCase);
		}

		// Token: 0x06009140 RID: 37184 RVA: 0x00056F15 File Offset: 0x00055115
		public static Type GetTypeFromString(string typeName, bool throwOnError, bool ignoreCase)
		{
			return SystemInfo.GetTypeFromString(Assembly.GetCallingAssembly(), typeName, throwOnError, ignoreCase);
		}

		// Token: 0x06009141 RID: 37185 RVA: 0x0013CFE8 File Offset: 0x0013B1E8
		public static Type GetTypeFromString(Assembly relativeAssembly, string typeName, bool throwOnError, bool ignoreCase)
		{
			if (typeName.IndexOf(',') != -1)
			{
				return Type.GetType(typeName, throwOnError, ignoreCase);
			}
			Type type = relativeAssembly.GetType(typeName, false, ignoreCase);
			if (type != null)
			{
				return type;
			}
			Assembly[] array = null;
			try
			{
				array = AppDomain.CurrentDomain.GetAssemblies();
			}
			catch (SecurityException)
			{
			}
			if (array != null)
			{
				foreach (Assembly assembly in array)
				{
					type = assembly.GetType(typeName, false, ignoreCase);
					if (type != null)
					{
						LogLog.Debug(SystemInfo.declaringType, string.Concat(new string[] { "Loaded type [", typeName, "] from assembly [", assembly.FullName, "] by searching loaded assemblies." }));
						return type;
					}
				}
			}
			if (throwOnError)
			{
				throw new TypeLoadException(string.Concat(new string[] { "Could not load type [", typeName, "]. Tried assembly [", relativeAssembly.FullName, "] and all loaded assemblies" }));
			}
			return null;
		}

		// Token: 0x06009142 RID: 37186 RVA: 0x00056F24 File Offset: 0x00055124
		public static Guid NewGuid()
		{
			return Guid.NewGuid();
		}

		// Token: 0x06009143 RID: 37187 RVA: 0x00056F2B File Offset: 0x0005512B
		public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string parameterName, object actualValue, string message)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, message);
		}

		// Token: 0x06009144 RID: 37188 RVA: 0x0013D0DC File Offset: 0x0013B2DC
		public static bool TryParse(string s, out int val)
		{
			val = 0;
			try
			{
				double num;
				if (double.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					val = Convert.ToInt32(num);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06009145 RID: 37189 RVA: 0x0013D120 File Offset: 0x0013B320
		public static bool TryParse(string s, out long val)
		{
			val = 0L;
			try
			{
				double num;
				if (double.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					val = Convert.ToInt64(num);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06009146 RID: 37190 RVA: 0x0013D168 File Offset: 0x0013B368
		public static bool TryParse(string s, out short val)
		{
			val = 0;
			try
			{
				double num;
				if (double.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					val = Convert.ToInt16(num);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06009147 RID: 37191 RVA: 0x0013D1AC File Offset: 0x0013B3AC
		public static string ConvertToFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string text = string.Empty;
			try
			{
				string applicationBaseDirectory = SystemInfo.ApplicationBaseDirectory;
				if (applicationBaseDirectory != null)
				{
					Uri uri = new Uri(applicationBaseDirectory);
					if (uri.IsFile)
					{
						text = uri.LocalPath;
					}
				}
			}
			catch
			{
			}
			if (text != null && text.Length > 0)
			{
				return Path.GetFullPath(Path.Combine(text, path));
			}
			return Path.GetFullPath(path);
		}

		// Token: 0x06009148 RID: 37192 RVA: 0x00056F35 File Offset: 0x00055135
		public static Hashtable CreateCaseInsensitiveHashtable()
		{
			return new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0400611C RID: 24860
		private const string DEFAULT_NULL_TEXT = "(null)";

		// Token: 0x0400611D RID: 24861
		private const string DEFAULT_NOT_AVAILABLE_TEXT = "NOT AVAILABLE";

		// Token: 0x0400611E RID: 24862
		public static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x0400611F RID: 24863
		private static readonly Type declaringType = typeof(SystemInfo);

		// Token: 0x04006120 RID: 24864
		private static string s_hostName;

		// Token: 0x04006121 RID: 24865
		private static string s_appFriendlyName;
	}
}
