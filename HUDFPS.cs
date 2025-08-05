using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000036 RID: 54
[NullableContext(1)]
[Nullable(0)]
public class HUDFPS : MonoBehaviour
{
	// Token: 0x060000CD RID: 205 RVA: 0x00005BD6 File Offset: 0x00003DD6
	private void Start()
	{
		if (this.limitFrameRate)
		{
			Application.targetFrameRate = this.frameRate;
		}
		this.updateTimer = this.frequency;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0005F4C8 File Offset: 0x0005D6C8
	private void Update()
	{
		if (Input.GetKeyUp(this.toggleKey))
		{
			this.show = !this.show;
			this.accum = 0f;
			this.frames = 0;
			this.updateTimer = this.frequency;
		}
		if (this.show)
		{
			this.accum += Time.timeScale / Time.deltaTime;
			this.frames++;
			this.updateTimer -= Time.deltaTime;
			if (this.updateTimer <= 0f)
			{
				this.CalcCurrentFPS();
				this.updateTimer = this.frequency;
			}
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0005F570 File Offset: 0x0005D770
	private void OnGUI()
	{
		if (this.show)
		{
			if (this.style == null)
			{
				this.style = new GUIStyle(GUI.skin.label)
				{
					normal = 
					{
						textColor = Color.white
					},
					alignment = TextAnchor.MiddleCenter
				};
			}
			GUI.color = ((!this.updateColor) ? Color.white : this.color);
			this.startRect = GUI.Window(0, this.startRect, new GUI.WindowFunction(this.DoMyWindow), string.Empty);
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0005F5F8 File Offset: 0x0005D7F8
	private void CalcCurrentFPS()
	{
		float num = this.accum / (float)this.frames;
		this.sFPS = num.ToString(string.Format("f{0}", Mathf.Clamp(this.nbDecimal, 0, 10))) + " FPS";
		this.color = ((num >= 30f) ? Color.green : ((num <= 10f) ? Color.yellow : Color.red));
		this.accum = 0f;
		this.frames = 0;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x0005F684 File Offset: 0x0005D884
	private void DoMyWindow(int windowID)
	{
		GUI.Label(new Rect(0f, 0f, this.startRect.width, this.startRect.height), this.sFPS, this.style);
		if (this.allowDrag)
		{
			GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}
	}

	// Token: 0x04000071 RID: 113
	public KeyCode toggleKey = KeyCode.F8;

	// Token: 0x04000072 RID: 114
	public bool show = true;

	// Token: 0x04000073 RID: 115
	public Rect startRect = new Rect(10f, 10f, 75f, 50f);

	// Token: 0x04000074 RID: 116
	public bool updateColor = true;

	// Token: 0x04000075 RID: 117
	public bool allowDrag = true;

	// Token: 0x04000076 RID: 118
	public float frequency = 0.5f;

	// Token: 0x04000077 RID: 119
	public int nbDecimal = 1;

	// Token: 0x04000078 RID: 120
	public bool limitFrameRate;

	// Token: 0x04000079 RID: 121
	public int frameRate = 60;

	// Token: 0x0400007A RID: 122
	private float accum;

	// Token: 0x0400007B RID: 123
	private Color color = Color.white;

	// Token: 0x0400007C RID: 124
	private int frames;

	// Token: 0x0400007D RID: 125
	private string sFPS = string.Empty;

	// Token: 0x0400007E RID: 126
	private GUIStyle style;

	// Token: 0x0400007F RID: 127
	private float updateTimer;
}
