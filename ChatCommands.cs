using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Platform.Kernel.ECS.ClientEntitySystem.API;
using Tanks.Lobby.ClientCommunicator.Impl;

// Token: 0x02000013 RID: 19
[NullableContext(1)]
[Nullable(0)]
public class ChatCommands
{
	// Token: 0x06000041 RID: 65 RVA: 0x00005740 File Offset: 0x00003940
	private static Event Leave(string[] parameters)
	{
		return new LeaveFromChatEvent();
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00005747 File Offset: 0x00003947
	private static Event Invite(string[] parameters)
	{
		return new InviteUserToChatEvent
		{
			UserUid = parameters[0]
		};
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00005757 File Offset: 0x00003957
	private static Event CreatePersonal(string[] parameters)
	{
		return new OpenPersonalChannelEvent
		{
			UserUid = parameters[0]
		};
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00005767 File Offset: 0x00003967
	private static Event Mute(string[] parameters)
	{
		return new MuteUserEvent
		{
			UserUid = parameters[0]
		};
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00005777 File Offset: 0x00003977
	private static Event Unmute(string[] parameters)
	{
		return new UnmuteUserEvent
		{
			UserUid = parameters[0]
		};
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00005787 File Offset: 0x00003987
	private static Event Kick(string[] parameters)
	{
		return new KickUserFromChatEvent
		{
			UserUid = parameters[0]
		};
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0005E324 File Offset: 0x0005C524
	public static bool IsCommand(string message, out Event commandEvent)
	{
		commandEvent = null;
		if (message.StartsWith("/"))
		{
			string[] array = message.Split(new char[] { ' ' });
			foreach (ChatCommands.Command command in ChatCommands.CommandList)
			{
				if (array[0] == command.CommandText)
				{
					if (array.Length != command.ParamsCount + 1)
					{
						return false;
					}
					List<string> list = array.ToList<string>();
					list.RemoveAt(0);
					commandEvent = command.CreateEventFunc(list.ToArray());
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x04000021 RID: 33
	private static readonly List<ChatCommands.Command> CommandList = new List<ChatCommands.Command>(8)
	{
		new ChatCommands.Command
		{
			CommandText = "/leave",
			ParamsCount = 0,
			CreateEventFunc = new Func<string[], Event>(ChatCommands.Leave)
		},
		new ChatCommands.Command
		{
			CommandText = "/invite",
			CreateEventFunc = new Func<string[], Event>(ChatCommands.Invite)
		},
		new ChatCommands.Command
		{
			CommandText = "/w",
			CreateEventFunc = new Func<string[], Event>(ChatCommands.CreatePersonal)
		},
		new ChatCommands.Command
		{
			CommandText = "/block",
			CreateEventFunc = new Func<string[], Event>(ChatCommands.Mute)
		},
		new ChatCommands.Command
		{
			CommandText = "/mute",
			CreateEventFunc = new Func<string[], Event>(ChatCommands.Mute)
		},
		new ChatCommands.Command
		{
			CommandText = "/unblock",
			CreateEventFunc = new Func<string[], Event>(ChatCommands.Unmute)
		},
		new ChatCommands.Command
		{
			CommandText = "/unmute",
			CreateEventFunc = new Func<string[], Event>(ChatCommands.Unmute)
		},
		new ChatCommands.Command
		{
			CommandText = "/kick",
			CreateEventFunc = new Func<string[], Event>(ChatCommands.Kick)
		}
	};

	// Token: 0x02000014 RID: 20
	[Nullable(0)]
	private class Command
	{
		// Token: 0x04000022 RID: 34
		public string CommandText;

		// Token: 0x04000023 RID: 35
		public Func<string[], Event> CreateEventFunc;

		// Token: 0x04000024 RID: 36
		public int ParamsCount = 1;
	}
}
