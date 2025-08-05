using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02002A88 RID: 10888
	[NullableContext(1)]
	[Nullable(0)]
	public class FileAppender : TextWriterAppender
	{
		// Token: 0x060095D2 RID: 38354 RVA: 0x00059AB7 File Offset: 0x00057CB7
		public FileAppender()
		{
		}

		// Token: 0x060095D3 RID: 38355 RVA: 0x00059ADC File Offset: 0x00057CDC
		[Obsolete("Instead use the default constructor and set the Layout, File & AppendToFile properties")]
		public FileAppender(ILayout layout, string filename, bool append)
		{
			this.Layout = layout;
			this.File = filename;
			this.AppendToFile = append;
			this.ActivateOptions();
		}

		// Token: 0x060095D4 RID: 38356 RVA: 0x00059B1C File Offset: 0x00057D1C
		[Obsolete("Instead use the default constructor and set the Layout & File properties")]
		public FileAppender(ILayout layout, string filename)
			: this(layout, filename, true)
		{
		}

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x060095D5 RID: 38357 RVA: 0x00059B27 File Offset: 0x00057D27
		// (set) Token: 0x060095D6 RID: 38358 RVA: 0x00059B2F File Offset: 0x00057D2F
		public virtual string File
		{
			get
			{
				return this.m_fileName;
			}
			set
			{
				this.m_fileName = value;
			}
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x060095D7 RID: 38359 RVA: 0x00059B38 File Offset: 0x00057D38
		// (set) Token: 0x060095D8 RID: 38360 RVA: 0x00059B40 File Offset: 0x00057D40
		public bool AppendToFile { get; set; } = true;

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x060095D9 RID: 38361 RVA: 0x00059B49 File Offset: 0x00057D49
		// (set) Token: 0x060095DA RID: 38362 RVA: 0x00059B51 File Offset: 0x00057D51
		public Encoding Encoding { get; set; } = Encoding.Default;

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x060095DB RID: 38363 RVA: 0x00059B5A File Offset: 0x00057D5A
		// (set) Token: 0x060095DC RID: 38364 RVA: 0x00059B62 File Offset: 0x00057D62
		public SecurityContext SecurityContext { get; set; }

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x060095DD RID: 38365 RVA: 0x00059B6B File Offset: 0x00057D6B
		// (set) Token: 0x060095DE RID: 38366 RVA: 0x00059B73 File Offset: 0x00057D73
		public FileAppender.LockingModelBase LockingModel { get; set; } = new FileAppender.ExclusiveLock();

		// Token: 0x060095DF RID: 38367 RVA: 0x00146334 File Offset: 0x00144534
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.SecurityContext == null)
			{
				this.SecurityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
			}
			if (this.LockingModel == null)
			{
				this.LockingModel = new FileAppender.ExclusiveLock();
			}
			this.LockingModel.CurrentAppender = this;
			if (this.m_fileName != null)
			{
				using (this.SecurityContext.Impersonate(this))
				{
					this.m_fileName = FileAppender.ConvertToFullPath(this.m_fileName.Trim());
				}
				this.SafeOpenFile(this.m_fileName, this.AppendToFile);
				return;
			}
			LogLog.Warn(FileAppender.declaringType, "FileAppender: File option not set for appender [" + base.Name + "].");
			LogLog.Warn(FileAppender.declaringType, "FileAppender: Are you using FileAppender instead of ConsoleAppender?");
		}

		// Token: 0x060095E0 RID: 38368 RVA: 0x00059B7C File Offset: 0x00057D7C
		protected override void Reset()
		{
			base.Reset();
			this.m_fileName = null;
		}

		// Token: 0x060095E1 RID: 38369 RVA: 0x00059B8B File Offset: 0x00057D8B
		protected override void PrepareWriter()
		{
			this.SafeOpenFile(this.m_fileName, this.AppendToFile);
		}

		// Token: 0x060095E2 RID: 38370 RVA: 0x00146408 File Offset: 0x00144608
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_stream.AcquireLock())
			{
				try
				{
					base.Append(loggingEvent);
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x060095E3 RID: 38371 RVA: 0x00146448 File Offset: 0x00144648
		protected override void Append(LoggingEvent[] loggingEvents)
		{
			if (this.m_stream.AcquireLock())
			{
				try
				{
					base.Append(loggingEvents);
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x060095E4 RID: 38372 RVA: 0x00146488 File Offset: 0x00144688
		protected override void WriteFooter()
		{
			if (this.m_stream != null)
			{
				this.m_stream.AcquireLock();
				try
				{
					base.WriteFooter();
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x060095E5 RID: 38373 RVA: 0x001464D0 File Offset: 0x001446D0
		protected override void WriteHeader()
		{
			if (this.m_stream != null && this.m_stream.AcquireLock())
			{
				try
				{
					base.WriteHeader();
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x060095E6 RID: 38374 RVA: 0x00146518 File Offset: 0x00144718
		protected override void CloseWriter()
		{
			if (this.m_stream != null)
			{
				this.m_stream.AcquireLock();
				try
				{
					base.CloseWriter();
				}
				finally
				{
					this.m_stream.ReleaseLock();
				}
			}
		}

		// Token: 0x060095E7 RID: 38375 RVA: 0x00059B9F File Offset: 0x00057D9F
		protected void CloseFile()
		{
			this.WriteFooterAndCloseWriter();
		}

		// Token: 0x060095E8 RID: 38376 RVA: 0x00146560 File Offset: 0x00144760
		protected virtual void SafeOpenFile(string fileName, bool append)
		{
			try
			{
				this.OpenFile(fileName, append);
			}
			catch (Exception ex)
			{
				this.ErrorHandler.Error(string.Format("OpenFile({0},{1}) call failed.", fileName, append), ex, ErrorCode.FileOpenFailure);
			}
		}

		// Token: 0x060095E9 RID: 38377 RVA: 0x001465A8 File Offset: 0x001447A8
		protected virtual void OpenFile(string fileName, bool append)
		{
			if (LogLog.IsErrorEnabled)
			{
				bool flag = false;
				using (this.SecurityContext.Impersonate(this))
				{
					flag = Path.IsPathRooted(fileName);
				}
				if (!flag)
				{
					LogLog.Error(FileAppender.declaringType, "INTERNAL ERROR. OpenFile(" + fileName + "): File name is not fully qualified.");
				}
			}
			lock (this)
			{
				this.Reset();
				LogLog.Debug(FileAppender.declaringType, string.Format("Opening file for writing [{0}] append [{1}]", fileName, append));
				this.m_fileName = fileName;
				this.AppendToFile = append;
				this.LockingModel.CurrentAppender = this;
				this.LockingModel.OpenFile(fileName, append, this.Encoding);
				this.m_stream = new FileAppender.LockingStream(this.LockingModel);
				if (this.m_stream != null)
				{
					this.m_stream.AcquireLock();
					try
					{
						this.SetQWForFiles(new StreamWriter(this.m_stream, this.Encoding));
					}
					finally
					{
						this.m_stream.ReleaseLock();
					}
				}
				this.WriteHeader();
			}
		}

		// Token: 0x060095EA RID: 38378 RVA: 0x00059BA7 File Offset: 0x00057DA7
		protected virtual void SetQWForFiles(Stream fileStream)
		{
			this.SetQWForFiles(new StreamWriter(fileStream, this.Encoding));
		}

		// Token: 0x060095EB RID: 38379 RVA: 0x00059BBB File Offset: 0x00057DBB
		protected virtual void SetQWForFiles(TextWriter writer)
		{
			base.QuietWriter = new QuietTextWriter(writer, this.ErrorHandler);
		}

		// Token: 0x060095EC RID: 38380 RVA: 0x00059BCF File Offset: 0x00057DCF
		protected static string ConvertToFullPath(string path)
		{
			return SystemInfo.ConvertToFullPath(path);
		}

		// Token: 0x040062B1 RID: 25265
		private static readonly Type declaringType = typeof(FileAppender);

		// Token: 0x040062B2 RID: 25266
		private string m_fileName;

		// Token: 0x040062B3 RID: 25267
		private FileAppender.LockingStream m_stream;

		// Token: 0x02002A89 RID: 10889
		[Nullable(0)]
		private sealed class LockingStream : Stream, IDisposable
		{
			// Token: 0x060095EE RID: 38382 RVA: 0x00059BE8 File Offset: 0x00057DE8
			public LockingStream(FileAppender.LockingModelBase locking)
			{
				if (locking == null)
				{
					throw new ArgumentException("Locking model may not be null", "locking");
				}
				this.m_lockingModel = locking;
			}

			// Token: 0x17001787 RID: 6023
			// (get) Token: 0x060095EF RID: 38383 RVA: 0x00007F86 File Offset: 0x00006186
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001788 RID: 6024
			// (get) Token: 0x060095F0 RID: 38384 RVA: 0x00059C11 File Offset: 0x00057E11
			public override bool CanSeek
			{
				get
				{
					this.AssertLocked();
					return this.m_realStream.CanSeek;
				}
			}

			// Token: 0x17001789 RID: 6025
			// (get) Token: 0x060095F1 RID: 38385 RVA: 0x00059C24 File Offset: 0x00057E24
			public override bool CanWrite
			{
				get
				{
					this.AssertLocked();
					return this.m_realStream.CanWrite;
				}
			}

			// Token: 0x1700178A RID: 6026
			// (get) Token: 0x060095F2 RID: 38386 RVA: 0x00059C37 File Offset: 0x00057E37
			public override long Length
			{
				get
				{
					this.AssertLocked();
					return this.m_realStream.Length;
				}
			}

			// Token: 0x1700178B RID: 6027
			// (get) Token: 0x060095F3 RID: 38387 RVA: 0x00059C4A File Offset: 0x00057E4A
			// (set) Token: 0x060095F4 RID: 38388 RVA: 0x00059C5D File Offset: 0x00057E5D
			public override long Position
			{
				get
				{
					this.AssertLocked();
					return this.m_realStream.Position;
				}
				set
				{
					this.AssertLocked();
					this.m_realStream.Position = value;
				}
			}

			// Token: 0x060095F5 RID: 38389 RVA: 0x00059C71 File Offset: 0x00057E71
			void IDisposable.Dispose()
			{
				this.Close();
			}

			// Token: 0x060095F6 RID: 38390 RVA: 0x001466D4 File Offset: 0x001448D4
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.AssertLocked();
				IAsyncResult asyncResult = this.m_realStream.BeginRead(buffer, offset, count, callback, state);
				this.m_readTotal = this.EndRead(asyncResult);
				return asyncResult;
			}

			// Token: 0x060095F7 RID: 38391 RVA: 0x00146708 File Offset: 0x00144908
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.AssertLocked();
				IAsyncResult asyncResult = this.m_realStream.BeginWrite(buffer, offset, count, callback, state);
				this.EndWrite(asyncResult);
				return asyncResult;
			}

			// Token: 0x060095F8 RID: 38392 RVA: 0x00059C79 File Offset: 0x00057E79
			public override void Close()
			{
				this.m_lockingModel.CloseFile();
			}

			// Token: 0x060095F9 RID: 38393 RVA: 0x00059C86 File Offset: 0x00057E86
			public override int EndRead(IAsyncResult asyncResult)
			{
				this.AssertLocked();
				return this.m_readTotal;
			}

			// Token: 0x060095FA RID: 38394 RVA: 0x0000568E File Offset: 0x0000388E
			public override void EndWrite(IAsyncResult asyncResult)
			{
			}

			// Token: 0x060095FB RID: 38395 RVA: 0x00059C94 File Offset: 0x00057E94
			public override void Flush()
			{
				this.AssertLocked();
				this.m_realStream.Flush();
			}

			// Token: 0x060095FC RID: 38396 RVA: 0x00059CA7 File Offset: 0x00057EA7
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.m_realStream.Read(buffer, offset, count);
			}

			// Token: 0x060095FD RID: 38397 RVA: 0x00059CB7 File Offset: 0x00057EB7
			public override int ReadByte()
			{
				return this.m_realStream.ReadByte();
			}

			// Token: 0x060095FE RID: 38398 RVA: 0x00059CC4 File Offset: 0x00057EC4
			public override long Seek(long offset, SeekOrigin origin)
			{
				this.AssertLocked();
				return this.m_realStream.Seek(offset, origin);
			}

			// Token: 0x060095FF RID: 38399 RVA: 0x00059CD9 File Offset: 0x00057ED9
			public override void SetLength(long value)
			{
				this.AssertLocked();
				this.m_realStream.SetLength(value);
			}

			// Token: 0x06009600 RID: 38400 RVA: 0x00059CED File Offset: 0x00057EED
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.AssertLocked();
				this.m_realStream.Write(buffer, offset, count);
			}

			// Token: 0x06009601 RID: 38401 RVA: 0x00059D03 File Offset: 0x00057F03
			public override void WriteByte(byte value)
			{
				this.AssertLocked();
				this.m_realStream.WriteByte(value);
			}

			// Token: 0x06009602 RID: 38402 RVA: 0x00059D17 File Offset: 0x00057F17
			private void AssertLocked()
			{
				if (this.m_realStream == null)
				{
					throw new FileAppender.LockingStream.LockStateException("The file is not currently locked");
				}
			}

			// Token: 0x06009603 RID: 38403 RVA: 0x00146738 File Offset: 0x00144938
			public bool AcquireLock()
			{
				bool flag = false;
				lock (this)
				{
					if (this.m_lockLevel == 0)
					{
						this.m_realStream = this.m_lockingModel.AcquireLock();
					}
					if (this.m_realStream != null)
					{
						this.m_lockLevel++;
						flag = true;
					}
				}
				return flag;
			}

			// Token: 0x06009604 RID: 38404 RVA: 0x0014679C File Offset: 0x0014499C
			public void ReleaseLock()
			{
				lock (this)
				{
					this.m_lockLevel--;
					if (this.m_lockLevel == 0)
					{
						this.m_lockingModel.ReleaseLock();
						this.m_realStream = null;
					}
				}
			}

			// Token: 0x040062B8 RID: 25272
			private readonly FileAppender.LockingModelBase m_lockingModel;

			// Token: 0x040062B9 RID: 25273
			private int m_lockLevel;

			// Token: 0x040062BA RID: 25274
			private int m_readTotal = -1;

			// Token: 0x040062BB RID: 25275
			private Stream m_realStream;

			// Token: 0x02002A8A RID: 10890
			[NullableContext(0)]
			public sealed class LockStateException : LogException
			{
				// Token: 0x06009605 RID: 38405 RVA: 0x00059D2C File Offset: 0x00057F2C
				[NullableContext(1)]
				public LockStateException(string message)
					: base(message)
				{
				}
			}
		}

		// Token: 0x02002A8B RID: 10891
		[Nullable(0)]
		public abstract class LockingModelBase
		{
			// Token: 0x1700178C RID: 6028
			// (get) Token: 0x06009606 RID: 38406 RVA: 0x00059D35 File Offset: 0x00057F35
			// (set) Token: 0x06009607 RID: 38407 RVA: 0x00059D3D File Offset: 0x00057F3D
			public FileAppender CurrentAppender { get; set; }

			// Token: 0x06009608 RID: 38408
			public abstract void OpenFile(string filename, bool append, Encoding encoding);

			// Token: 0x06009609 RID: 38409
			public abstract void CloseFile();

			// Token: 0x0600960A RID: 38410
			public abstract Stream AcquireLock();

			// Token: 0x0600960B RID: 38411
			public abstract void ReleaseLock();

			// Token: 0x0600960C RID: 38412 RVA: 0x001467F4 File Offset: 0x001449F4
			protected Stream CreateStream(string filename, bool append, FileShare fileShare)
			{
				Stream stream;
				using (this.CurrentAppender.SecurityContext.Impersonate(this))
				{
					string directoryName = Path.GetDirectoryName(filename);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					FileMode fileMode = ((!append) ? FileMode.Create : FileMode.Append);
					stream = new FileStream(filename, fileMode, FileAccess.Write, fileShare);
				}
				return stream;
			}

			// Token: 0x0600960D RID: 38413 RVA: 0x00146858 File Offset: 0x00144A58
			protected void CloseStream(Stream stream)
			{
				using (this.CurrentAppender.SecurityContext.Impersonate(this))
				{
					stream.Close();
				}
			}
		}

		// Token: 0x02002A8C RID: 10892
		[Nullable(0)]
		public class ExclusiveLock : FileAppender.LockingModelBase
		{
			// Token: 0x0600960F RID: 38415 RVA: 0x0014689C File Offset: 0x00144A9C
			public override void OpenFile(string filename, bool append, Encoding encoding)
			{
				try
				{
					this.m_stream = base.CreateStream(filename, append, FileShare.Read);
				}
				catch (Exception ex)
				{
					base.CurrentAppender.ErrorHandler.Error("Unable to acquire lock on file " + filename + ". " + ex.Message);
				}
			}

			// Token: 0x06009610 RID: 38416 RVA: 0x00059D46 File Offset: 0x00057F46
			public override void CloseFile()
			{
				base.CloseStream(this.m_stream);
				this.m_stream = null;
			}

			// Token: 0x06009611 RID: 38417 RVA: 0x00059D5B File Offset: 0x00057F5B
			public override Stream AcquireLock()
			{
				return this.m_stream;
			}

			// Token: 0x06009612 RID: 38418 RVA: 0x0000568E File Offset: 0x0000388E
			public override void ReleaseLock()
			{
			}

			// Token: 0x040062BD RID: 25277
			private Stream m_stream;
		}

		// Token: 0x02002A8D RID: 10893
		[Nullable(0)]
		public class MinimalLock : FileAppender.LockingModelBase
		{
			// Token: 0x06009614 RID: 38420 RVA: 0x00059D6B File Offset: 0x00057F6B
			public override void OpenFile(string filename, bool append, Encoding encoding)
			{
				this.m_filename = filename;
				this.m_append = append;
			}

			// Token: 0x06009615 RID: 38421 RVA: 0x0000568E File Offset: 0x0000388E
			public override void CloseFile()
			{
			}

			// Token: 0x06009616 RID: 38422 RVA: 0x001468F4 File Offset: 0x00144AF4
			public override Stream AcquireLock()
			{
				if (this.m_stream == null)
				{
					try
					{
						this.m_stream = base.CreateStream(this.m_filename, this.m_append, FileShare.Read);
						this.m_append = true;
					}
					catch (Exception ex)
					{
						base.CurrentAppender.ErrorHandler.Error("Unable to acquire lock on file " + this.m_filename + ". " + ex.Message);
					}
				}
				return this.m_stream;
			}

			// Token: 0x06009617 RID: 38423 RVA: 0x00059D7B File Offset: 0x00057F7B
			public override void ReleaseLock()
			{
				base.CloseStream(this.m_stream);
				this.m_stream = null;
			}

			// Token: 0x040062BE RID: 25278
			private bool m_append;

			// Token: 0x040062BF RID: 25279
			private string m_filename;

			// Token: 0x040062C0 RID: 25280
			private Stream m_stream;
		}

		// Token: 0x02002A8E RID: 10894
		[Nullable(0)]
		public class InterProcessLock : FileAppender.LockingModelBase
		{
			// Token: 0x06009619 RID: 38425 RVA: 0x00146970 File Offset: 0x00144B70
			public override void OpenFile(string filename, bool append, Encoding encoding)
			{
				try
				{
					this.m_stream = base.CreateStream(filename, append, FileShare.ReadWrite);
					string text = filename.Replace("\\", "_").Replace(":", "_").Replace("/", "_");
					this.m_mutex = new Mutex(false, text);
				}
				catch (Exception ex)
				{
					base.CurrentAppender.ErrorHandler.Error("Unable to acquire lock on file " + filename + ". " + ex.Message);
				}
			}

			// Token: 0x0600961A RID: 38426 RVA: 0x00146A04 File Offset: 0x00144C04
			public override void CloseFile()
			{
				try
				{
					base.CloseStream(this.m_stream);
					this.m_stream = null;
				}
				finally
				{
					this.m_mutex.ReleaseMutex();
					this.m_mutex.Close();
					this.m_mutexClosed = true;
				}
			}

			// Token: 0x0600961B RID: 38427 RVA: 0x00059D90 File Offset: 0x00057F90
			public override Stream AcquireLock()
			{
				if (this.m_mutex != null)
				{
					this.m_mutex.WaitOne();
					if (this.m_stream.CanSeek)
					{
						this.m_stream.Seek(0L, SeekOrigin.End);
					}
				}
				return this.m_stream;
			}

			// Token: 0x0600961C RID: 38428 RVA: 0x00059DC8 File Offset: 0x00057FC8
			public override void ReleaseLock()
			{
				if (!this.m_mutexClosed && this.m_mutex != null)
				{
					this.m_mutex.ReleaseMutex();
				}
			}

			// Token: 0x040062C1 RID: 25281
			private Mutex m_mutex;

			// Token: 0x040062C2 RID: 25282
			private bool m_mutexClosed;

			// Token: 0x040062C3 RID: 25283
			private Stream m_stream;
		}
	}
}
