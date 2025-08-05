using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002AA1 RID: 10913
	[NullableContext(1)]
	[Nullable(0)]
	public class UdpAppender : AppenderSkeleton
	{
		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06009687 RID: 38535 RVA: 0x0005A127 File Offset: 0x00058327
		// (set) Token: 0x06009688 RID: 38536 RVA: 0x0005A12F File Offset: 0x0005832F
		public IPAddress RemoteAddress { get; set; }

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06009689 RID: 38537 RVA: 0x0005A138 File Offset: 0x00058338
		// (set) Token: 0x0600968A RID: 38538 RVA: 0x00147998 File Offset: 0x00145B98
		public int RemotePort
		{
			get
			{
				return this.m_remotePort;
			}
			set
			{
				bool flag = value < 0 || value > 65535;
				if (flag)
				{
					throw SystemInfo.CreateArgumentOutOfRangeException("value", value, string.Concat(new string[]
					{
						"The value specified is less than ",
						0.ToString(NumberFormatInfo.InvariantInfo),
						" or greater than ",
						65535.ToString(NumberFormatInfo.InvariantInfo),
						"."
					}));
				}
				this.m_remotePort = value;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x0600968B RID: 38539 RVA: 0x0005A140 File Offset: 0x00058340
		// (set) Token: 0x0600968C RID: 38540 RVA: 0x00147A1C File Offset: 0x00145C1C
		public int LocalPort
		{
			get
			{
				return this.m_localPort;
			}
			set
			{
				bool flag = value != 0;
				if (flag)
				{
					bool flag2 = value < 0 || value > 65535;
					flag = flag2;
				}
				if (flag)
				{
					throw SystemInfo.CreateArgumentOutOfRangeException("value", value, string.Concat(new string[]
					{
						"The value specified is less than ",
						0.ToString(NumberFormatInfo.InvariantInfo),
						" or greater than ",
						65535.ToString(NumberFormatInfo.InvariantInfo),
						"."
					}));
				}
				this.m_localPort = value;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x0600968D RID: 38541 RVA: 0x0005A148 File Offset: 0x00058348
		// (set) Token: 0x0600968E RID: 38542 RVA: 0x0005A150 File Offset: 0x00058350
		public Encoding Encoding { get; set; } = Encoding.Default;

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x0600968F RID: 38543 RVA: 0x0005A159 File Offset: 0x00058359
		// (set) Token: 0x06009690 RID: 38544 RVA: 0x0005A161 File Offset: 0x00058361
		protected UdpClient Client { get; set; }

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06009691 RID: 38545 RVA: 0x0005A16A File Offset: 0x0005836A
		// (set) Token: 0x06009692 RID: 38546 RVA: 0x0005A172 File Offset: 0x00058372
		protected IPEndPoint RemoteEndPoint { get; set; }

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06009693 RID: 38547 RVA: 0x00005B7A File Offset: 0x00003D7A
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009694 RID: 38548 RVA: 0x00147AAC File Offset: 0x00145CAC
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.RemoteAddress == null)
			{
				throw new ArgumentNullException("RemoteAddress", "The required property 'Address' was not specified.");
			}
			int num = this.RemotePort;
			bool flag = num < 0 || num > 65535;
			if (flag)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("this.RemotePort", this.RemotePort, string.Concat(new string[]
				{
					"The RemotePort is less than ",
					0.ToString(NumberFormatInfo.InvariantInfo),
					" or greater than ",
					65535.ToString(NumberFormatInfo.InvariantInfo),
					"."
				}));
			}
			flag = this.LocalPort != 0;
			if (flag)
			{
				num = this.LocalPort;
				bool flag2 = num < 0 || num > 65535;
				flag = flag2;
			}
			if (flag)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("this.LocalPort", this.LocalPort, string.Concat(new string[]
				{
					"The LocalPort is less than ",
					0.ToString(NumberFormatInfo.InvariantInfo),
					" or greater than ",
					65535.ToString(NumberFormatInfo.InvariantInfo),
					"."
				}));
			}
			this.RemoteEndPoint = new IPEndPoint(this.RemoteAddress, this.RemotePort);
			this.InitializeClientConnection();
		}

		// Token: 0x06009695 RID: 38549 RVA: 0x00147BFC File Offset: 0x00145DFC
		protected override void Append(LoggingEvent loggingEvent)
		{
			try
			{
				byte[] bytes = this.Encoding.GetBytes(base.RenderLoggingEvent(loggingEvent).ToCharArray());
				this.Client.Send(bytes, bytes.Length, this.RemoteEndPoint);
			}
			catch (Exception ex)
			{
				this.ErrorHandler.Error(string.Format("Unable to send logging event to remote host {0} on port {1}.", this.RemoteAddress, this.RemotePort), ex, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x06009696 RID: 38550 RVA: 0x0005A17B File Offset: 0x0005837B
		protected override void OnClose()
		{
			base.OnClose();
			if (this.Client == null)
			{
				return;
			}
			this.Client.Close();
			this.Client = null;
		}

		// Token: 0x06009697 RID: 38551 RVA: 0x00147C74 File Offset: 0x00145E74
		protected virtual void InitializeClientConnection()
		{
			try
			{
				if (this.LocalPort == 0)
				{
					this.Client = new UdpClient(this.RemoteAddress.AddressFamily);
				}
				else
				{
					this.Client = new UdpClient(this.LocalPort, this.RemoteAddress.AddressFamily);
				}
			}
			catch (Exception ex)
			{
				this.ErrorHandler.Error("Could not initialize the UdpClient connection on port " + this.LocalPort.ToString(NumberFormatInfo.InvariantInfo) + ".", ex, ErrorCode.GenericFailure);
				this.Client = null;
			}
		}

		// Token: 0x04006326 RID: 25382
		private int m_localPort;

		// Token: 0x04006327 RID: 25383
		private int m_remotePort;
	}
}
