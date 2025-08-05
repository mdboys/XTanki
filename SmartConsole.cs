using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tanks.Battle.ClientCore.Impl;
using UnityEngine;

// Token: 0x02000079 RID: 121
[NullableContext(1)]
[Nullable(0)]
public class SmartConsole : MonoBehaviour
{
	// Token: 0x06000220 RID: 544 RVA: 0x00006908 File Offset: 0x00004B08
	private void Awake()
	{
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (this.m_font == null)
		{
			Debug.LogError("SmartConsole requires a font to be set in the inspector");
		}
		SmartConsole.Initialise(this);
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00065CB0 File Offset: 0x00063EB0
	private void Update()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (SmartConsole.s_first)
		{
			if (SmartConsole.s_fps == null || SmartConsole.s_textInput == null)
			{
				Debug.LogWarning("Some variables are null that really shouldn't be! Did you make code changes whilst paused? Be aware that such changes are not safe in general!");
				return;
			}
			SmartConsole.SetTopDrawOrderOnGUIText(SmartConsole.s_fps.GetComponent<GUIText>());
			SmartConsole.SetTopDrawOrderOnGUIText(SmartConsole.s_textInput.GetComponent<GUIText>());
			GameObject[] array = SmartConsole.s_historyDisplay;
			for (int i = 0; i < array.Length; i++)
			{
				SmartConsole.SetTopDrawOrderOnGUIText(array[i].GetComponent<GUIText>());
			}
			SmartConsole.s_first = false;
		}
		SmartConsole.HandleInput();
		if (SmartConsole.s_showConsole)
		{
			SmartConsole.s_visiblityLerp += Time.deltaTime / 0.4f;
		}
		else
		{
			SmartConsole.s_visiblityLerp -= Time.deltaTime / 0.4f;
		}
		SmartConsole.s_visiblityLerp = Mathf.Clamp01(SmartConsole.s_visiblityLerp);
		base.transform.position = Vector3.Lerp(SmartConsole.k_hidePosition, (!SmartConsole.s_drawFullConsole) ? SmartConsole.k_position : SmartConsole.k_fullPosition, this.SmootherStep(SmartConsole.s_visiblityLerp));
		base.transform.localScale = SmartConsole.k_scale;
		if (SmartConsole.s_textInput != null && SmartConsole.s_textInput.GetComponent<GUIText>() != null)
		{
			SmartConsole.s_textInput.GetComponent<GUIText>().text = ">" + SmartConsole.s_currentInputLine + ((!SmartConsole.s_blink) ? string.Empty : "_");
		}
		SmartConsole.s_flippy++;
		SmartConsole.s_flippy &= 7;
		if (SmartConsole.s_flippy == 0)
		{
			SmartConsole.s_blink = !SmartConsole.s_blink;
		}
		if (SmartConsole.s_drawFPS)
		{
			SmartConsole.s_fps.GetComponent<GUIText>().text = string.Format("{0}{1} fps ", string.Empty, 1f / Time.deltaTime);
			SmartConsole.s_fps.transform.position = new Vector3(0.8f, 1f, 0f);
			return;
		}
		SmartConsole.s_fps.transform.position = new Vector3(1f, 10f, 0f);
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00006933 File Offset: 0x00004B33
	public static void Clear()
	{
		SmartConsole.s_outputHistory.Clear();
		SmartConsole.SetStringsOnHistoryElements();
	}

	// Token: 0x06000223 RID: 547 RVA: 0x00006944 File Offset: 0x00004B44
	public static void Print(string message)
	{
		SmartConsole.WriteLine(message);
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000694C File Offset: 0x00004B4C
	public static void WriteLine(string message)
	{
		SmartConsole.s_outputHistory.Add(SmartConsole.DeNewLine(message));
		SmartConsole.s_currentCommandHistoryIndex = SmartConsole.s_outputHistory.Count - 1;
		SmartConsole.SetStringsOnHistoryElements();
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00065ECC File Offset: 0x000640CC
	public static void ExecuteLine(string inputLine)
	{
		SmartConsole.WriteLine(">" + inputLine);
		string[] array = SmartConsole.CComParameterSplit(inputLine);
		if (array.Length != 0)
		{
			if (SmartConsole.s_masterDictionary.ContainsKey(array[0]))
			{
				SmartConsole.s_commandHistory.Add(inputLine);
				SmartConsole.s_masterDictionary[array[0]].m_callback(inputLine);
				return;
			}
			SmartConsole.WriteLine("Unrecognised command or variable name: " + array[0]);
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00006974 File Offset: 0x00004B74
	public static void RemoveCommandIfExists(string name)
	{
		SmartConsole.s_commandDictionary.Remove(name);
		SmartConsole.s_masterDictionary.Remove(name);
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00065F3C File Offset: 0x0006413C
	public static void RegisterCommand(string name, string exampleUsage, string helpDescription, SmartConsole.ConsoleCommandFunction callback)
	{
		if (!SmartConsole.s_commandDictionary.ContainsKey(name))
		{
			SmartConsole.Command command = new SmartConsole.Command
			{
				m_name = name,
				m_paramsExample = exampleUsage,
				m_help = helpDescription,
				m_callback = callback
			};
			SmartConsole.s_commandDictionary.Add(name, command);
			SmartConsole.s_masterDictionary.Add(name, command);
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000698E File Offset: 0x00004B8E
	public static void RegisterCommand(string name, string helpDescription, SmartConsole.ConsoleCommandFunction callback)
	{
		SmartConsole.RegisterCommand(name, string.Empty, helpDescription, callback);
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000699D File Offset: 0x00004B9D
	public static void RegisterCommand(string name, SmartConsole.ConsoleCommandFunction callback)
	{
		SmartConsole.RegisterCommand(name, string.Empty, "(no description)", callback);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x00065F90 File Offset: 0x00064190
	public static SmartConsole.Variable<T> CreateVariable<[Nullable(2)] T>(string name, string description, T initialValue) where T : new()
	{
		if (SmartConsole.s_variableDictionary.ContainsKey(name))
		{
			Debug.LogError("Tried to add already existing console variable!");
			return null;
		}
		SmartConsole.Variable<T> variable = new SmartConsole.Variable<T>(name, description, initialValue);
		SmartConsole.s_variableDictionary.Add(name, variable);
		SmartConsole.s_masterDictionary.Add(name, variable);
		return variable;
	}

	// Token: 0x0600022B RID: 555 RVA: 0x000069B0 File Offset: 0x00004BB0
	public static SmartConsole.Variable<T> CreateVariable<[Nullable(2)] T>(string name, string description) where T : new()
	{
		return SmartConsole.CreateVariable<T>(name, description, new T());
	}

	// Token: 0x0600022C RID: 556 RVA: 0x000069BE File Offset: 0x00004BBE
	public static SmartConsole.Variable<T> CreateVariable<[Nullable(2)] T>(string name) where T : new()
	{
		return SmartConsole.CreateVariable<T>(name, string.Empty);
	}

	// Token: 0x0600022D RID: 557 RVA: 0x000069CB File Offset: 0x00004BCB
	public static void DestroyVariable<[Nullable(2)] T>(SmartConsole.Variable<T> variable) where T : new()
	{
		SmartConsole.s_variableDictionary.Remove(variable.m_name);
		SmartConsole.s_masterDictionary.Remove(variable.m_name);
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00065FD8 File Offset: 0x000641D8
	private static void Help(string parameters)
	{
		string text = string.Empty;
		try
		{
			text = parameters.Split(new char[] { ' ' })[1];
		}
		catch (Exception)
		{
		}
		foreach (SmartConsole.Command command in SmartConsole.s_commandDictionary.Values)
		{
			if (string.IsNullOrEmpty(text) || command.m_name.Contains(text))
			{
				string text2 = command.m_name;
				for (int i = command.m_name.Length; i < 25; i++)
				{
					text2 += " ";
				}
				text2 = ((command.m_paramsExample.Length <= 0) ? (text2 + "          ") : (text2 + " example: " + command.m_paramsExample));
				for (int j = command.m_paramsExample.Length; j < 35; j++)
				{
					text2 += " ";
				}
				SmartConsole.WriteLine(text2 + command.m_help);
			}
		}
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00066108 File Offset: 0x00064308
	private static void Echo(string parameters)
	{
		string text = string.Empty;
		string[] array = SmartConsole.CComParameterSplit(parameters);
		for (int i = 1; i < array.Length; i++)
		{
			text = text + array[i] + " ";
		}
		if (text.EndsWith(" "))
		{
			text.Substring(0, text.Length - 1);
		}
		SmartConsole.WriteLine(text);
	}

	// Token: 0x06000230 RID: 560 RVA: 0x000069EF File Offset: 0x00004BEF
	private static void Clear(string parameters)
	{
		SmartConsole.Clear();
	}

	// Token: 0x06000231 RID: 561 RVA: 0x000069F6 File Offset: 0x00004BF6
	private static void LastExceptionCallStack(string parameters)
	{
		SmartConsole.DumpCallStack(SmartConsole.s_lastExceptionCallStack);
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00006A02 File Offset: 0x00004C02
	private static void LastErrorCallStack(string parameters)
	{
		SmartConsole.DumpCallStack(SmartConsole.s_lastErrorCallStack);
	}

	// Token: 0x06000233 RID: 563 RVA: 0x00006A0E File Offset: 0x00004C0E
	private static void LastWarningCallStack(string parameters)
	{
		SmartConsole.DumpCallStack(SmartConsole.s_lastWarningCallStack);
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00006A1A File Offset: 0x00004C1A
	private static void Quit(string parameters)
	{
		Application.Quit();
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00066164 File Offset: 0x00064364
	private static void ListCvars(string parameters)
	{
		foreach (SmartConsole.Command command in SmartConsole.s_variableDictionary.Values)
		{
			string text = command.m_name;
			for (int i = command.m_name.Length; i < 50; i++)
			{
				text += " ";
			}
			SmartConsole.WriteLine(text + command.m_help);
		}
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00006A21 File Offset: 0x00004C21
	private static void Initialise(SmartConsole instance)
	{
		if (!(SmartConsole.s_textInput != null))
		{
			Application.LogCallback logCallback;
			if ((logCallback = SmartConsole.<>O.<0>__LogHandler) == null)
			{
				logCallback = (SmartConsole.<>O.<0>__LogHandler = new Application.LogCallback(SmartConsole.LogHandler));
			}
			Application.RegisterLogCallback(logCallback);
			SmartConsole.InitialiseCommands();
			SmartConsole.InitialiseVariables();
			SmartConsole.InitialiseUI(instance);
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x000661F0 File Offset: 0x000643F0
	private static void HandleInput()
	{
		SmartConsole.s_toggleCooldown += ((Time.deltaTime >= 0.0166f) ? Time.deltaTime : 0.0166f);
		if (SmartConsole.s_toggleCooldown < 0.35f)
		{
			return;
		}
		bool flag = false;
		if (Input.touchCount > 0)
		{
			flag = SmartConsole.IsInputCoordInBounds(Input.touches[0].position);
		}
		else if (Input.GetMouseButton(0))
		{
			flag = SmartConsole.IsInputCoordInBounds(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
		}
		if (flag || Input.GetKeyUp(KeyCode.BackQuote))
		{
			if (!SmartConsole.s_consoleLock)
			{
				InputManagerImpl.SUSPENDED = false;
				SmartConsole.s_showConsole = !SmartConsole.s_showConsole;
				if (SmartConsole.s_showConsole)
				{
					InputManagerImpl.SUSPENDED = true;
					SmartConsole.s_currentInputLine = string.Empty;
				}
			}
			SmartConsole.s_toggleCooldown = 0f;
		}
		if (SmartConsole.s_commandHistory.Count > 0)
		{
			bool flag2 = false;
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				flag2 = true;
				SmartConsole.s_currentCommandHistoryIndex--;
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				flag2 = true;
				SmartConsole.s_currentCommandHistoryIndex++;
			}
			if (flag2)
			{
				SmartConsole.s_currentCommandHistoryIndex = Mathf.Clamp(SmartConsole.s_currentCommandHistoryIndex, 0, SmartConsole.s_commandHistory.Count - 1);
				SmartConsole.s_currentInputLine = SmartConsole.s_commandHistory[SmartConsole.s_currentCommandHistoryIndex];
			}
		}
		SmartConsole.HandleTextInput();
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00066340 File Offset: 0x00064540
	private static void InitialiseCommands()
	{
		string text = "clear";
		string text2 = "clear the console log";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction;
		if ((consoleCommandFunction = SmartConsole.<>O.<1>__Clear) == null)
		{
			consoleCommandFunction = (SmartConsole.<>O.<1>__Clear = new SmartConsole.ConsoleCommandFunction(SmartConsole.Clear));
		}
		SmartConsole.RegisterCommand(text, text2, consoleCommandFunction);
		string text3 = "cls";
		string text4 = "clear the console log (alias for Clear)";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction2;
		if ((consoleCommandFunction2 = SmartConsole.<>O.<1>__Clear) == null)
		{
			consoleCommandFunction2 = (SmartConsole.<>O.<1>__Clear = new SmartConsole.ConsoleCommandFunction(SmartConsole.Clear));
		}
		SmartConsole.RegisterCommand(text3, text4, consoleCommandFunction2);
		string text5 = "echo";
		string text6 = "echo <string>";
		string text7 = "writes <string> to the console log (alias for echo)";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction3;
		if ((consoleCommandFunction3 = SmartConsole.<>O.<2>__Echo) == null)
		{
			consoleCommandFunction3 = (SmartConsole.<>O.<2>__Echo = new SmartConsole.ConsoleCommandFunction(SmartConsole.Echo));
		}
		SmartConsole.RegisterCommand(text5, text6, text7, consoleCommandFunction3);
		string text8 = "help";
		string text9 = "displays help information for console command where available. Add parameter to search by filter";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction4;
		if ((consoleCommandFunction4 = SmartConsole.<>O.<3>__Help) == null)
		{
			consoleCommandFunction4 = (SmartConsole.<>O.<3>__Help = new SmartConsole.ConsoleCommandFunction(SmartConsole.Help));
		}
		SmartConsole.RegisterCommand(text8, text9, consoleCommandFunction4);
		string text10 = "list";
		string text11 = "lists all currently registered console variables";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction5;
		if ((consoleCommandFunction5 = SmartConsole.<>O.<4>__ListCvars) == null)
		{
			consoleCommandFunction5 = (SmartConsole.<>O.<4>__ListCvars = new SmartConsole.ConsoleCommandFunction(SmartConsole.ListCvars));
		}
		SmartConsole.RegisterCommand(text10, text11, consoleCommandFunction5);
		string text12 = "print";
		string text13 = "print <string>";
		string text14 = "writes <string> to the console log";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction6;
		if ((consoleCommandFunction6 = SmartConsole.<>O.<2>__Echo) == null)
		{
			consoleCommandFunction6 = (SmartConsole.<>O.<2>__Echo = new SmartConsole.ConsoleCommandFunction(SmartConsole.Echo));
		}
		SmartConsole.RegisterCommand(text12, text13, text14, consoleCommandFunction6);
		string text15 = "quit";
		string text16 = "quit the game (not sure this works with iOS/Android)";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction7;
		if ((consoleCommandFunction7 = SmartConsole.<>O.<5>__Quit) == null)
		{
			consoleCommandFunction7 = (SmartConsole.<>O.<5>__Quit = new SmartConsole.ConsoleCommandFunction(SmartConsole.Quit));
		}
		SmartConsole.RegisterCommand(text15, text16, consoleCommandFunction7);
		string text17 = "callstack.warning";
		string text18 = "display the call stack for the last warning message";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction8;
		if ((consoleCommandFunction8 = SmartConsole.<>O.<6>__LastWarningCallStack) == null)
		{
			consoleCommandFunction8 = (SmartConsole.<>O.<6>__LastWarningCallStack = new SmartConsole.ConsoleCommandFunction(SmartConsole.LastWarningCallStack));
		}
		SmartConsole.RegisterCommand(text17, text18, consoleCommandFunction8);
		string text19 = "callstack.error";
		string text20 = "display the call stack for the last error message";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction9;
		if ((consoleCommandFunction9 = SmartConsole.<>O.<7>__LastErrorCallStack) == null)
		{
			consoleCommandFunction9 = (SmartConsole.<>O.<7>__LastErrorCallStack = new SmartConsole.ConsoleCommandFunction(SmartConsole.LastErrorCallStack));
		}
		SmartConsole.RegisterCommand(text19, text20, consoleCommandFunction9);
		string text21 = "callstack.exception";
		string text22 = "display the call stack for the last exception message";
		SmartConsole.ConsoleCommandFunction consoleCommandFunction10;
		if ((consoleCommandFunction10 = SmartConsole.<>O.<8>__LastExceptionCallStack) == null)
		{
			consoleCommandFunction10 = (SmartConsole.<>O.<8>__LastExceptionCallStack = new SmartConsole.ConsoleCommandFunction(SmartConsole.LastExceptionCallStack));
		}
		SmartConsole.RegisterCommand(text21, text22, consoleCommandFunction10);
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000664FC File Offset: 0x000646FC
	private static void InitialiseVariables()
	{
		SmartConsole.s_drawFPS = SmartConsole.CreateVariable<bool>("show.fps", "whether to draw framerate counter or not", false);
		SmartConsole.s_drawFullConsole = SmartConsole.CreateVariable<bool>("console.fullscreen", "whether to draw the console over the whole screen or not", false);
		SmartConsole.s_consoleLock = SmartConsole.CreateVariable<bool>("console.lock", "whether to allow showing/hiding the console", false);
		SmartConsole.s_logging = SmartConsole.CreateVariable<bool>("console.log", "whether to redirect log to the console", true);
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00066560 File Offset: 0x00064760
	private static void InitialiseUI(SmartConsole instance)
	{
		SmartConsole.s_font = instance.m_font;
		if (SmartConsole.s_font == null)
		{
			Debug.LogError("SmartConsole needs to have a font set on an instance in the editor!");
			SmartConsole.s_font = new Font("Arial");
		}
		SmartConsole.s_fps = instance.AddChildWithGUIText("FPSCounter");
		SmartConsole.s_textInput = instance.AddChildWithGUIText("SmartConsoleInputField");
		SmartConsole.s_historyDisplay = new GameObject[120];
		for (int i = 0; i < 120; i++)
		{
			SmartConsole.s_historyDisplay[i] = instance.AddChildWithGUIText(string.Format("SmartConsoleHistoryDisplay{0}", i));
		}
		instance.Layout();
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00006A60 File Offset: 0x00004C60
	private GameObject AddChildWithGUIText(string name)
	{
		return this.AddChildWithComponent<GUIText>(name);
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00006A69 File Offset: 0x00004C69
	private GameObject AddChildWithComponent<[Nullable(0)] T>(string name) where T : Component
	{
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<T>();
		gameObject.transform.parent = base.transform;
		gameObject.name = name;
		return gameObject;
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000568E File Offset: 0x0000388E
	private static void SetTopDrawOrderOnGUIText(GUIText text)
	{
	}

	// Token: 0x0600023E RID: 574 RVA: 0x000665FC File Offset: 0x000647FC
	private static void HandleTextInput()
	{
		bool flag = false;
		string inputString = Input.inputString;
		int i = 0;
		while (i < inputString.Length)
		{
			char c = inputString[i];
			switch (c)
			{
			case '\b':
				SmartConsole.s_currentInputLine = ((SmartConsole.s_currentInputLine.Length <= 0) ? string.Empty : SmartConsole.s_currentInputLine.Substring(0, SmartConsole.s_currentInputLine.Length - 1));
				break;
			case '\t':
				SmartConsole.AutoComplete();
				flag = true;
				break;
			case '\n':
			case '\r':
				SmartConsole.ExecuteCurrentLine();
				SmartConsole.s_currentInputLine = string.Empty;
				break;
			case '\v':
			case '\f':
				goto IL_0085;
			default:
				goto IL_0085;
			}
			IL_009B:
			i++;
			continue;
			IL_0085:
			SmartConsole.s_currentInputLine += c.ToString();
			goto IL_009B;
		}
		if (!flag && Input.GetKeyDown(KeyCode.Tab))
		{
			SmartConsole.AutoComplete();
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00006A8F File Offset: 0x00004C8F
	private static void ExecuteCurrentLine()
	{
		SmartConsole.ExecuteLine(SmartConsole.s_currentInputLine);
	}

	// Token: 0x06000240 RID: 576 RVA: 0x000666C8 File Offset: 0x000648C8
	private static void AutoComplete()
	{
		string[] array = SmartConsole.CComParameterSplit(SmartConsole.s_currentInputLine);
		if (array.Length == 0)
		{
			return;
		}
		SmartConsole.Command command = SmartConsole.s_masterDictionary.AutoCompleteLookup(array[0]);
		int num = 0;
		do
		{
			num = command.m_name.IndexOf(".", num + 1);
		}
		while (num > 0 && num < array[0].Length);
		string text = command.m_name;
		if (num >= 0)
		{
			text = command.m_name.Substring(0, num + 1);
		}
		if (text.Length < SmartConsole.s_currentInputLine.Length)
		{
			if (!SmartConsole.AutoCompleteTailString("true") && !SmartConsole.AutoCompleteTailString("false") && !SmartConsole.AutoCompleteTailString("True") && !SmartConsole.AutoCompleteTailString("False") && !SmartConsole.AutoCompleteTailString("TRUE"))
			{
				SmartConsole.AutoCompleteTailString("FALSE");
				return;
			}
		}
		else if (text.Length >= SmartConsole.s_currentInputLine.Length)
		{
			SmartConsole.s_currentInputLine = text;
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x000667A8 File Offset: 0x000649A8
	private static bool AutoCompleteTailString(string tailString)
	{
		for (int i = 1; i < tailString.Length; i++)
		{
			if (SmartConsole.s_currentInputLine.EndsWith(" " + tailString.Substring(0, i)))
			{
				SmartConsole.s_currentInputLine = SmartConsole.s_currentInputLine.Substring(0, SmartConsole.s_currentInputLine.Length - 1) + tailString.Substring(i - 1);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x00066814 File Offset: 0x00064A14
	private void Layout()
	{
		float num = 0f;
		SmartConsole.LayoutTextAtY(SmartConsole.s_textInput, num);
		SmartConsole.LayoutTextAtY(SmartConsole.s_fps, num);
		num += 0.05f;
		for (int i = 0; i < 120; i++)
		{
			SmartConsole.LayoutTextAtY(SmartConsole.s_historyDisplay[i], num);
			num += 0.05f;
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00006A9B File Offset: 0x00004C9B
	private static void LayoutTextAtY(GameObject o, float y)
	{
		o.transform.localPosition = new Vector3(0f, y, 0f);
		o.GetComponent<GUIText>().fontStyle = FontStyle.Normal;
		o.GetComponent<GUIText>().font = SmartConsole.s_font;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00066868 File Offset: 0x00064A68
	private static void SetStringsOnHistoryElements()
	{
		for (int i = 0; i < 120; i++)
		{
			if (SmartConsole.s_outputHistory.Count - 1 - i >= 0)
			{
				SmartConsole.s_historyDisplay[i].GetComponent<GUIText>().text = SmartConsole.s_outputHistory[SmartConsole.s_outputHistory.Count - 1 - i];
			}
			else
			{
				SmartConsole.s_historyDisplay[i].GetComponent<GUIText>().text = string.Empty;
			}
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x00006AD4 File Offset: 0x00004CD4
	private static bool IsInputCoordInBounds(Vector2 inputCoordinate)
	{
		return inputCoordinate.x < 0.05f * (float)Screen.width && inputCoordinate.y > 0.95f * (float)Screen.height;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x000668D4 File Offset: 0x00064AD4
	private static void LogHandler(string message, string stack, LogType type)
	{
		if (SmartConsole.s_logging)
		{
			string text = "[Assert]:             ";
			string text2 = "[Debug.LogError]:     ";
			string text3 = "[Debug.LogException]: ";
			string text4 = "[Debug.LogWarning]:   ";
			string text5 = "[Debug.Log]:          ";
			switch (type)
			{
			case LogType.Error:
				text5 = text2;
				SmartConsole.s_lastErrorCallStack = stack;
				break;
			case LogType.Assert:
				text5 = text;
				break;
			case LogType.Warning:
				text5 = text4;
				SmartConsole.s_lastWarningCallStack = stack;
				break;
			case LogType.Exception:
				text5 = text3;
				SmartConsole.s_lastExceptionCallStack = stack;
				break;
			}
			SmartConsole.WriteLine(text5 + message);
			switch (type)
			{
			}
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00006B00 File Offset: 0x00004D00
	public static string[] CComParameterSplit(string parameters)
	{
		return parameters.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00066978 File Offset: 0x00064B78
	public static string[] CComParameterSplit(string parameters, int requiredParameters)
	{
		string[] array = SmartConsole.CComParameterSplit(parameters);
		if (array.Length < requiredParameters + 1)
		{
			SmartConsole.WriteLine(string.Format("Error: not enough parameters for command. Expected {0} found {1}", requiredParameters, array.Length - 1));
		}
		if (array.Length > requiredParameters + 1)
		{
			int num = array.Length - 1 - requiredParameters;
			SmartConsole.WriteLine(string.Format("Warning: {0}additional parameters will be dropped:", num));
			for (int i = array.Length - num; i < array.Length; i++)
			{
				SmartConsole.WriteLine("\"" + array[i] + "\"");
			}
		}
		return array;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00066A04 File Offset: 0x00064C04
	private static string[] CVarParameterSplit(string parameters)
	{
		string[] array = SmartConsole.CComParameterSplit(parameters);
		if (array.Length == 0)
		{
			SmartConsole.WriteLine("Error: not enough parameters to set or display the value of a console variable.");
		}
		if (array.Length > 2)
		{
			int num = array.Length - 3;
			SmartConsole.WriteLine(string.Format("Warning: {0}additional parameters will be dropped:", num));
			for (int i = array.Length - num; i < array.Length; i++)
			{
				SmartConsole.WriteLine("\"" + array[i] + "\"");
			}
		}
		return array;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00006B14 File Offset: 0x00004D14
	private static string DeNewLine(string message)
	{
		return message.Replace("\n", " | ");
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00066A74 File Offset: 0x00064C74
	private static void DumpCallStack(string stackString)
	{
		string[] array = stackString.Split(new char[] { '\r', '\n' });
		if (array.Length != 0)
		{
			int num = 0;
			while (array[array.Length - 1 - num].Length == 0 && num < array.Length)
			{
				num++;
			}
			int num2 = array.Length - num;
			for (int i = 0; i < num2; i++)
			{
				SmartConsole.WriteLine((i + 1).ToString() + ((i >= 9) ? " " : "  ") + array[i]);
			}
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00006B26 File Offset: 0x00004D26
	private float SmootherStep(float t)
	{
		return ((6f * t - 15f) * t + 10f) * t * t * t;
	}

	// Token: 0x040001A0 RID: 416
	private const float k_animTime = 0.4f;

	// Token: 0x040001A1 RID: 417
	private const float k_lineSpace = 0.05f;

	// Token: 0x040001A2 RID: 418
	private const int k_historyLines = 120;

	// Token: 0x040001A3 RID: 419
	private const float k_toogleCDTime = 0.35f;

	// Token: 0x040001A4 RID: 420
	private static readonly Vector3 k_position = new Vector3(0.01f, 0.65f, 0f);

	// Token: 0x040001A5 RID: 421
	private static readonly Vector3 k_fullPosition = new Vector3(0.01f, 0.05f, 0f);

	// Token: 0x040001A6 RID: 422
	private static readonly Vector3 k_hidePosition = new Vector3(0.01f, 1.1f, 0f);

	// Token: 0x040001A7 RID: 423
	private static readonly Vector3 k_scale = new Vector3(0.5f, 0.5f, 1f);

	// Token: 0x040001A8 RID: 424
	private static int s_flippy;

	// Token: 0x040001A9 RID: 425
	private static bool s_blink;

	// Token: 0x040001AA RID: 426
	private static bool s_first = true;

	// Token: 0x040001AB RID: 427
	private static float s_toggleCooldown;

	// Token: 0x040001AC RID: 428
	private static int s_currentCommandHistoryIndex;

	// Token: 0x040001AD RID: 429
	private static Font s_font;

	// Token: 0x040001AE RID: 430
	private static SmartConsole.Variable<bool> s_drawFPS;

	// Token: 0x040001AF RID: 431
	private static SmartConsole.Variable<bool> s_drawFullConsole;

	// Token: 0x040001B0 RID: 432
	private static SmartConsole.Variable<bool> s_consoleLock;

	// Token: 0x040001B1 RID: 433
	private static SmartConsole.Variable<bool> s_logging;

	// Token: 0x040001B2 RID: 434
	private static GameObject s_fps;

	// Token: 0x040001B3 RID: 435
	private static GameObject s_textInput;

	// Token: 0x040001B4 RID: 436
	private static GameObject[] s_historyDisplay;

	// Token: 0x040001B5 RID: 437
	private static readonly SmartConsole.AutoCompleteDictionary<SmartConsole.Command> s_commandDictionary = new SmartConsole.AutoCompleteDictionary<SmartConsole.Command>();

	// Token: 0x040001B6 RID: 438
	private static readonly SmartConsole.AutoCompleteDictionary<SmartConsole.Command> s_variableDictionary = new SmartConsole.AutoCompleteDictionary<SmartConsole.Command>();

	// Token: 0x040001B7 RID: 439
	private static readonly SmartConsole.AutoCompleteDictionary<SmartConsole.Command> s_masterDictionary = new SmartConsole.AutoCompleteDictionary<SmartConsole.Command>();

	// Token: 0x040001B8 RID: 440
	private static readonly List<string> s_commandHistory = new List<string>();

	// Token: 0x040001B9 RID: 441
	private static readonly List<string> s_outputHistory = new List<string>();

	// Token: 0x040001BA RID: 442
	private static string s_lastExceptionCallStack = "(none yet)";

	// Token: 0x040001BB RID: 443
	private static string s_lastErrorCallStack = "(none yet)";

	// Token: 0x040001BC RID: 444
	private static string s_lastWarningCallStack = "(none yet)";

	// Token: 0x040001BD RID: 445
	private static string s_currentInputLine = string.Empty;

	// Token: 0x040001BE RID: 446
	private static float s_visiblityLerp;

	// Token: 0x040001BF RID: 447
	private static bool s_showConsole;

	// Token: 0x040001C0 RID: 448
	public Font m_font;

	// Token: 0x0200007A RID: 122
	// (Invoke) Token: 0x06000250 RID: 592
	[NullableContext(0)]
	public delegate void ConsoleCommandFunction(string parameters);

	// Token: 0x0200007B RID: 123
	[Nullable(0)]
	public class Command
	{
		// Token: 0x040001C1 RID: 449
		public SmartConsole.ConsoleCommandFunction m_callback;

		// Token: 0x040001C2 RID: 450
		public string m_help = "(no description)";

		// Token: 0x040001C3 RID: 451
		public string m_name;

		// Token: 0x040001C4 RID: 452
		public string m_paramsExample = string.Empty;
	}

	// Token: 0x0200007C RID: 124
	[Nullable(0)]
	public class Variable<[Nullable(2)] T> : SmartConsole.Command where T : new()
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00006B61 File Offset: 0x00004D61
		public Variable(string name)
		{
			this.Initialise(name, string.Empty, new T());
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00006B7A File Offset: 0x00004D7A
		public Variable(string name, string description)
		{
			this.Initialise(name, description, new T());
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00006B8F File Offset: 0x00004D8F
		public Variable(string name, T initialValue)
		{
			this.Initialise(name, string.Empty, initialValue);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public Variable(string name, string description, T initalValue)
		{
			this.Initialise(name, description, initalValue);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00006BB5 File Offset: 0x00004DB5
		public void Set(T val)
		{
			this.m_value = val;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006BBE File Offset: 0x00004DBE
		public static implicit operator T(SmartConsole.Variable<T> var)
		{
			return var.m_value;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00066BCC File Offset: 0x00064DCC
		private void Initialise(string name, string description, T initalValue)
		{
			this.m_name = name;
			this.m_help = description;
			this.m_paramsExample = string.Empty;
			this.m_value = initalValue;
			SmartConsole.ConsoleCommandFunction consoleCommandFunction;
			if ((consoleCommandFunction = SmartConsole.Variable<T>.<>O.<0>__CommandFunction) == null)
			{
				consoleCommandFunction = (SmartConsole.Variable<T>.<>O.<0>__CommandFunction = new SmartConsole.ConsoleCommandFunction(SmartConsole.Variable<T>.CommandFunction));
			}
			this.m_callback = consoleCommandFunction;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00066C1C File Offset: 0x00064E1C
		private static void CommandFunction(string parameters)
		{
			string[] array = SmartConsole.CVarParameterSplit(parameters);
			if (array.Length != 0 && SmartConsole.s_variableDictionary.ContainsKey(array[0]))
			{
				SmartConsole.Variable<T> variable = SmartConsole.s_variableDictionary[array[0]] as SmartConsole.Variable<T>;
				string text = " is set to ";
				if (array.Length == 2)
				{
					variable.SetFromString(array[1]);
					text = " has been set to ";
				}
				string name = variable.m_name;
				string text2 = text;
				T value = variable.m_value;
				SmartConsole.WriteLine(name + text2 + ((value != null) ? value.ToString() : null));
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00006BC6 File Offset: 0x00004DC6
		private void SetFromString(string value)
		{
			this.m_value = (T)((object)Convert.ChangeType(value, typeof(T)));
		}

		// Token: 0x040001C5 RID: 453
		private T m_value;

		// Token: 0x0200007D RID: 125
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001C6 RID: 454
			[Nullable(0)]
			public static SmartConsole.ConsoleCommandFunction <0>__CommandFunction;
		}
	}

	// Token: 0x0200007E RID: 126
	[Nullable(new byte[] { 0, 1, 1 })]
	private class AutoCompleteDictionary<[Nullable(2)] T> : SortedDictionary<string, T>
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00006BE3 File Offset: 0x00004DE3
		public AutoCompleteDictionary()
			: base(new SmartConsole.AutoCompleteDictionary<T>.AutoCompleteComparer())
		{
			this.m_comparer = base.Comparer as SmartConsole.AutoCompleteDictionary<T>.AutoCompleteComparer;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006C01 File Offset: 0x00004E01
		public T LowerBound(string lookupString)
		{
			this.m_comparer.Reset();
			base.ContainsKey(lookupString);
			return base[this.m_comparer.LowerBound];
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006C27 File Offset: 0x00004E27
		public T UpperBound(string lookupString)
		{
			this.m_comparer.Reset();
			base.ContainsKey(lookupString);
			return base[this.m_comparer.UpperBound];
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00066CA4 File Offset: 0x00064EA4
		public T AutoCompleteLookup(string lookupString)
		{
			this.m_comparer.Reset();
			base.ContainsKey(lookupString);
			string text = ((this.m_comparer.UpperBound != null) ? this.m_comparer.UpperBound : this.m_comparer.LowerBound);
			return base[text];
		}

		// Token: 0x040001C7 RID: 455
		[Nullable(new byte[] { 1, 0 })]
		private readonly SmartConsole.AutoCompleteDictionary<T>.AutoCompleteComparer m_comparer;

		// Token: 0x0200007F RID: 127
		[Nullable(0)]
		private class AutoCompleteComparer : IComparer<string>
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000261 RID: 609 RVA: 0x00006C4D File Offset: 0x00004E4D
			// (set) Token: 0x06000262 RID: 610 RVA: 0x00006C55 File Offset: 0x00004E55
			public string LowerBound { get; private set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000263 RID: 611 RVA: 0x00006C5E File Offset: 0x00004E5E
			// (set) Token: 0x06000264 RID: 612 RVA: 0x00006C66 File Offset: 0x00004E66
			public string UpperBound { get; private set; }

			// Token: 0x06000265 RID: 613 RVA: 0x00006C6F File Offset: 0x00004E6F
			public int Compare(string x, string y)
			{
				int num = Comparer<string>.Default.Compare(x, y);
				if (num >= 0)
				{
					this.LowerBound = y;
				}
				if (num <= 0)
				{
					this.UpperBound = y;
				}
				return num;
			}

			// Token: 0x06000266 RID: 614 RVA: 0x00006C93 File Offset: 0x00004E93
			public void Reset()
			{
				this.LowerBound = null;
				this.UpperBound = null;
			}
		}
	}

	// Token: 0x02000080 RID: 128
	[CompilerGenerated]
	private static class <>O
	{
		// Token: 0x040001CA RID: 458
		[Nullable(0)]
		public static Application.LogCallback <0>__LogHandler;

		// Token: 0x040001CB RID: 459
		[Nullable(0)]
		public static SmartConsole.ConsoleCommandFunction <1>__Clear;

		// Token: 0x040001CC RID: 460
		[Nullable(0)]
		public static SmartConsole.ConsoleCommandFunction <2>__Echo;

		// Token: 0x040001CD RID: 461
		[Nullable(0)]
		public static SmartConsole.ConsoleCommandFunction <3>__Help;

		// Token: 0x040001CE RID: 462
		[Nullable(0)]
		public static SmartConsole.ConsoleCommandFunction <4>__ListCvars;

		// Token: 0x040001CF RID: 463
		[Nullable(0)]
		public static SmartConsole.ConsoleCommandFunction <5>__Quit;

		// Token: 0x040001D0 RID: 464
		[Nullable(0)]
		public static SmartConsole.ConsoleCommandFunction <6>__LastWarningCallStack;

		// Token: 0x040001D1 RID: 465
		[Nullable(0)]
		public static SmartConsole.ConsoleCommandFunction <7>__LastErrorCallStack;

		// Token: 0x040001D2 RID: 466
		[Nullable(0)]
		public static SmartConsole.ConsoleCommandFunction <8>__LastExceptionCallStack;
	}
}
