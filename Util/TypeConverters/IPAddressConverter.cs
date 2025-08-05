using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020029E5 RID: 10725
	[NullableContext(1)]
	[Nullable(0)]
	internal class IPAddressConverter : IConvertFrom
	{
		// Token: 0x06009193 RID: 37267 RVA: 0x000571DB File Offset: 0x000553DB
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06009194 RID: 37268 RVA: 0x0013D9C0 File Offset: 0x0013BBC0
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null && text.Length > 0)
			{
				try
				{
					IPAddress ipaddress;
					if (IPAddress.TryParse(text, out ipaddress))
					{
						return ipaddress;
					}
					IPHostEntry hostEntry = Dns.GetHostEntry(text);
					if (hostEntry != null)
					{
						IPAddress[] addressList = hostEntry.AddressList;
						if (addressList != null && addressList.Length > 0 && hostEntry.AddressList[0] != null)
						{
							return hostEntry.AddressList[0];
						}
					}
				}
				catch (Exception ex)
				{
					throw ConversionNotSupportedException.Create(typeof(IPAddress), source, ex);
				}
			}
			throw ConversionNotSupportedException.Create(typeof(IPAddress), source);
		}

		// Token: 0x04006138 RID: 24888
		private static readonly char[] validIpAddressChars = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D',
			'E', 'F', 'x', 'X', '.', ':', '%'
		};
	}
}
