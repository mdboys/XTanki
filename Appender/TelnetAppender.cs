using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A9D RID: 10909
	[NullableContext(1)]
	[Nullable(0)]
	public class TelnetAppender : AppenderSkeleton
	{
		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x0600965E RID: 38494 RVA: 0x0005A006 File Offset: 0x00058206
		// (set) Token: 0x0600965F RID: 38495 RVA: 0x0014720C File Offset: 0x0014540C
		public int Port
		{
			get
			{
				return this.m_listeningPort;
			}
			set
			{
				bool flag = value < 0 || value > 65535;
				if (flag)
				{
					throw SystemInfo.CreateArgumentOutOfRangeException("value", value, string.Concat(new string[]
					{
						"The value specified for Port is less than ",
						0.ToString(NumberFormatInfo.InvariantInfo),
						" or greater than ",
						65535.ToString(NumberFormatInfo.InvariantInfo),
						"."
					}));
				}
				this.m_listeningPort = value;
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06009660 RID: 38496 RVA: 0x00005B7A File Offset: 0x00003D7A
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009661 RID: 38497 RVA: 0x0005A00E File Offset: 0x0005820E
		protected override void OnClose()
		{
			base.OnClose();
			if (this.m_handler != null)
			{
				this.m_handler.Dispose();
				this.m_handler = null;
			}
		}

		// Token: 0x06009662 RID: 38498 RVA: 0x00147290 File Offset: 0x00145490
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			try
			{
				LogLog.Debug(TelnetAppender.declaringType, string.Format("Creating SocketHandler to listen on port [{0}]", this.m_listeningPort));
				this.m_handler = new TelnetAppender.SocketHandler(this.m_listeningPort);
			}
			catch (Exception ex)
			{
				LogLog.Error(TelnetAppender.declaringType, "Failed to create SocketHandler", ex);
				throw;
			}
		}

		// Token: 0x06009663 RID: 38499 RVA: 0x001472F8 File Offset: 0x001454F8
		protected override void Append(LoggingEvent loggingEvent)
		{
			TelnetAppender.SocketHandler handler = this.m_handler;
			if (handler != null && handler.HasConnections)
			{
				this.m_handler.Send(base.RenderLoggingEvent(loggingEvent));
			}
		}

		// Token: 0x0400631B RID: 25371
		private static readonly Type declaringType = typeof(TelnetAppender);

		// Token: 0x0400631C RID: 25372
		private TelnetAppender.SocketHandler m_handler;

		// Token: 0x0400631D RID: 25373
		private int m_listeningPort = 23;

		// Token: 0x02002A9E RID: 10910
		[Nullable(0)]
		protected class SocketHandler : IDisposable
		{
			// Token: 0x06009666 RID: 38502 RVA: 0x0014732C File Offset: 0x0014552C
			public SocketHandler(int port)
			{
				this.m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				this.m_serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
				this.m_serverSocket.Listen(5);
				this.m_serverSocket.BeginAccept(new AsyncCallback(this.OnConnect), null);
			}

			// Token: 0x1700179B RID: 6043
			// (get) Token: 0x06009667 RID: 38503 RVA: 0x00147394 File Offset: 0x00145594
			public bool HasConnections
			{
				get
				{
					ArrayList clients = this.m_clients;
					return clients != null && clients.Count > 0;
				}
			}

			// Token: 0x06009668 RID: 38504 RVA: 0x001473B8 File Offset: 0x001455B8
			public void Dispose()
			{
				foreach (object obj in this.m_clients)
				{
					((TelnetAppender.SocketHandler.SocketClient)obj).Dispose();
				}
				this.m_clients.Clear();
				Socket serverSocket = this.m_serverSocket;
				this.m_serverSocket = null;
				try
				{
					serverSocket.Shutdown(SocketShutdown.Both);
				}
				catch
				{
				}
				try
				{
					serverSocket.Close();
				}
				catch
				{
				}
			}

			// Token: 0x06009669 RID: 38505 RVA: 0x00147458 File Offset: 0x00145658
			public void Send(string message)
			{
				foreach (object obj in this.m_clients)
				{
					TelnetAppender.SocketHandler.SocketClient socketClient = (TelnetAppender.SocketHandler.SocketClient)obj;
					try
					{
						socketClient.Send(message);
					}
					catch (Exception)
					{
						socketClient.Dispose();
						this.RemoveClient(socketClient);
					}
				}
			}

			// Token: 0x0600966A RID: 38506 RVA: 0x001474D0 File Offset: 0x001456D0
			private void AddClient(TelnetAppender.SocketHandler.SocketClient client)
			{
				lock (this)
				{
					ArrayList arrayList = (ArrayList)this.m_clients.Clone();
					arrayList.Add(client);
					this.m_clients = arrayList;
				}
			}

			// Token: 0x0600966B RID: 38507 RVA: 0x00147520 File Offset: 0x00145720
			private void RemoveClient(TelnetAppender.SocketHandler.SocketClient client)
			{
				lock (this)
				{
					ArrayList arrayList = (ArrayList)this.m_clients.Clone();
					arrayList.Remove(client);
					this.m_clients = arrayList;
				}
			}

			// Token: 0x0600966C RID: 38508 RVA: 0x00147570 File Offset: 0x00145770
			private void OnConnect(IAsyncResult asyncResult)
			{
				try
				{
					Socket socket = this.m_serverSocket.EndAccept(asyncResult);
					LogLog.Debug(TelnetAppender.declaringType, string.Format("Accepting connection from [{0}]", socket.RemoteEndPoint));
					TelnetAppender.SocketHandler.SocketClient socketClient = new TelnetAppender.SocketHandler.SocketClient(socket);
					int count = this.m_clients.Count;
					if (count < 20)
					{
						try
						{
							socketClient.Send(string.Format("TelnetAppender v1.0 ({0} active connections)\r\n\r\n", count + 1));
							this.AddClient(socketClient);
							return;
						}
						catch
						{
							socketClient.Dispose();
							return;
						}
					}
					socketClient.Send("Sorry - Too many connections.\r\n");
					socketClient.Dispose();
				}
				catch
				{
				}
				finally
				{
					Socket serverSocket = this.m_serverSocket;
					if (serverSocket != null)
					{
						serverSocket.BeginAccept(new AsyncCallback(this.OnConnect), null);
					}
				}
			}

			// Token: 0x0400631E RID: 25374
			private const int MAX_CONNECTIONS = 20;

			// Token: 0x0400631F RID: 25375
			private ArrayList m_clients = new ArrayList();

			// Token: 0x04006320 RID: 25376
			private Socket m_serverSocket;

			// Token: 0x02002A9F RID: 10911
			[Nullable(0)]
			protected class SocketClient : IDisposable
			{
				// Token: 0x0600966D RID: 38509 RVA: 0x00147644 File Offset: 0x00145844
				public SocketClient(Socket socket)
				{
					this.m_socket = socket;
					try
					{
						this.m_writer = new StreamWriter(new NetworkStream(socket));
					}
					catch
					{
						this.Dispose();
						throw;
					}
				}

				// Token: 0x0600966E RID: 38510 RVA: 0x0014768C File Offset: 0x0014588C
				public void Dispose()
				{
					try
					{
						if (this.m_writer != null)
						{
							this.m_writer.Close();
							this.m_writer = null;
						}
					}
					catch
					{
					}
					if (this.m_socket != null)
					{
						try
						{
							this.m_socket.Shutdown(SocketShutdown.Both);
						}
						catch
						{
						}
						try
						{
							this.m_socket.Close();
						}
						catch
						{
						}
						this.m_socket = null;
					}
				}

				// Token: 0x0600966F RID: 38511 RVA: 0x0005A051 File Offset: 0x00058251
				public void Send(string message)
				{
					this.m_writer.Write(message);
					this.m_writer.Flush();
				}

				// Token: 0x04006321 RID: 25377
				private Socket m_socket;

				// Token: 0x04006322 RID: 25378
				private StreamWriter m_writer;
			}
		}
	}
}
