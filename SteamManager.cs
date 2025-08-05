using System;
using System.Runtime.CompilerServices;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x02000085 RID: 133
[NullableContext(1)]
[Nullable(0)]
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600028C RID: 652 RVA: 0x00006DB5 File Offset: 0x00004FB5
	private static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600028D RID: 653 RVA: 0x00006DD9 File Offset: 0x00004FD9
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00067550 File Offset: 0x00065750
	private void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid))
			{
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException ex)
		{
			Debug.LogError(string.Format("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n{0}", ex), this);
			Application.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.Log("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			return;
		}
		SteamManager.s_EverInialized = true;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00006DE5 File Offset: 0x00004FE5
	private void Update()
	{
		if (this.m_bInitialized)
		{
			SteamAPI.RunCallbacks();
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00067624 File Offset: 0x00065824
	private void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (this.m_bInitialized && this.m_SteamAPIWarningMessageHook == null)
		{
			SteamAPIWarningMessageHook_t steamAPIWarningMessageHook_t;
			if ((steamAPIWarningMessageHook_t = SteamManager.<>O.<0>__SteamAPIDebugTextHook) == null)
			{
				steamAPIWarningMessageHook_t = (SteamManager.<>O.<0>__SteamAPIDebugTextHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook));
			}
			this.m_SteamAPIWarningMessageHook = steamAPIWarningMessageHook_t;
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00006DF4 File Offset: 0x00004FF4
	private void OnDestroy()
	{
		if (!(SteamManager.s_instance != this))
		{
			SteamManager.s_instance = null;
			if (this.m_bInitialized)
			{
				SteamAPI.Shutdown();
			}
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x00006E16 File Offset: 0x00005016
	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x040001EB RID: 491
	private static SteamManager s_instance;

	// Token: 0x040001EC RID: 492
	private static bool s_EverInialized;

	// Token: 0x040001ED RID: 493
	private bool m_bInitialized;

	// Token: 0x040001EE RID: 494
	private SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	// Token: 0x02000086 RID: 134
	[CompilerGenerated]
	private static class <>O
	{
		// Token: 0x040001EF RID: 495
		[Nullable(0)]
		public static SteamAPIWarningMessageHook_t <0>__SteamAPIDebugTextHook;
	}
}
