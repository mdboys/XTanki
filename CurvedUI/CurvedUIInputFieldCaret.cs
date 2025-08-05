using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace CurvedUI
{
	// Token: 0x02002CBE RID: 11454
	[NullableContext(1)]
	[Nullable(0)]
	public class CurvedUIInputFieldCaret : MonoBehaviour
	{
		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06009F4F RID: 40783 RVA: 0x0005C7A5 File Offset: 0x0005A9A5
		private bool selected
		{
			get
			{
				return this.myField != null && this.myField.isFocused;
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06009F50 RID: 40784 RVA: 0x0005C7C2 File Offset: 0x0005A9C2
		// (set) Token: 0x06009F51 RID: 40785 RVA: 0x0005C7CA File Offset: 0x0005A9CA
		public Color CaretColor { get; set; }

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06009F52 RID: 40786 RVA: 0x0005C7D3 File Offset: 0x0005A9D3
		// (set) Token: 0x06009F53 RID: 40787 RVA: 0x0005C7DB File Offset: 0x0005A9DB
		public Color SelectionColor { get; set; }

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x06009F54 RID: 40788 RVA: 0x0005C7E4 File Offset: 0x0005A9E4
		// (set) Token: 0x06009F55 RID: 40789 RVA: 0x0005C7F1 File Offset: 0x0005A9F1
		public float CaretBlinkRate
		{
			get
			{
				return this.myField.caretBlinkRate;
			}
			set
			{
				this.myField.caretBlinkRate = value;
			}
		}

		// Token: 0x06009F56 RID: 40790 RVA: 0x0005C7FF File Offset: 0x0005A9FF
		private void Awake()
		{
			this.myField = base.GetComponent<InputField>();
		}

		// Token: 0x06009F57 RID: 40791 RVA: 0x0005C80D File Offset: 0x0005AA0D
		private void Update()
		{
			if (this.selected)
			{
				this.UpdateCaret();
			}
		}

		// Token: 0x06009F58 RID: 40792 RVA: 0x00158CA4 File Offset: 0x00156EA4
		private void CreateCaret()
		{
			GameObject gameObject = new GameObject("CurvedUICaret");
			gameObject.AddComponent<RectTransform>();
			gameObject.AddComponent<Image>();
			gameObject.AddComponent<CurvedUIVertexEffect>();
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.localScale = Vector3.one;
			this.myCaret = gameObject.transform as RectTransform;
			this.myCaret.anchoredPosition3D = Vector3.zero;
			this.myCaret.pivot = new Vector2(0f, 1f);
			gameObject.GetComponent<Image>().color = this.myField.caretColor;
			gameObject.transform.SetAsFirstSibling();
			this.myField.customCaretColor = true;
			this.CaretColor = this.myField.caretColor;
			this.myField.caretColor = new Color(0f, 0f, 0f, 0f);
			this.SelectionColor = this.myField.selectionColor;
			this.myField.selectionColor = new Color(0f, 0f, 0f, 0f);
			gameObject.gameObject.SetActive(false);
		}

		// Token: 0x06009F59 RID: 40793 RVA: 0x00158DD0 File Offset: 0x00156FD0
		private void UpdateCaret()
		{
			if (this.myCaret == null)
			{
				this.CreateCaret();
			}
			Vector2 vector = this.GetLocalPositionInText(this.myField.caretPosition);
			if (this.myField.selectionFocusPosition != this.myField.selectionAnchorPosition)
			{
				this.selectingText = true;
				Vector2 vector2 = new Vector2(this.GetLocalPositionInText(this.myField.selectionAnchorPosition).x - this.GetLocalPositionInText(this.myField.selectionFocusPosition).x, this.GetLocalPositionInText(this.myField.selectionAnchorPosition).y - this.GetLocalPositionInText(this.myField.selectionFocusPosition).y);
				vector = ((vector2.x >= 0f) ? this.GetLocalPositionInText(this.myField.selectionFocusPosition) : this.GetLocalPositionInText(this.myField.selectionAnchorPosition));
				vector2 = new Vector2(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y) + (float)this.myField.textComponent.fontSize);
				this.myCaret.sizeDelta = new Vector2(vector2.x, vector2.y);
				this.myCaret.anchoredPosition = vector;
				this.myCaret.GetComponent<Image>().color = this.SelectionColor;
			}
			else
			{
				this.selectingText = false;
				this.myCaret.sizeDelta = new Vector2((float)this.myField.caretWidth, (float)this.myField.textComponent.fontSize);
				this.myCaret.anchoredPosition = vector;
				this.myCaret.GetComponent<Image>().color = this.CaretColor;
			}
			this.BlinkCaret();
		}

		// Token: 0x06009F5A RID: 40794 RVA: 0x00158F84 File Offset: 0x00157184
		private void BlinkCaret()
		{
			this.blinkTimer += Time.deltaTime;
			if (this.blinkTimer >= this.myField.caretBlinkRate)
			{
				this.blinkTimer = 0f;
				this.myCaret.gameObject.SetActive(this.selectingText || !this.myCaret.gameObject.activeSelf);
			}
		}

		// Token: 0x06009F5B RID: 40795 RVA: 0x00158FF0 File Offset: 0x001571F0
		private Vector2 GetLocalPositionInText(int charNo)
		{
			if (!this.myField.isFocused)
			{
				return Vector2.zero;
			}
			TextGenerator cachedTextGenerator = this.myField.textComponent.cachedTextGenerator;
			if (charNo > cachedTextGenerator.characterCount - 1)
			{
				charNo = cachedTextGenerator.characterCount - 1;
			}
			if (charNo > 0)
			{
				UICharInfo uicharInfo = cachedTextGenerator.characters[charNo - 1];
				float num = (uicharInfo.cursorPos.x + uicharInfo.charWidth) / this.myField.textComponent.pixelsPerUnit + (float)this.lastCharDist;
				float num2 = uicharInfo.cursorPos.y / this.myField.textComponent.pixelsPerUnit;
				return new Vector2(num, num2);
			}
			UICharInfo uicharInfo2 = cachedTextGenerator.characters[charNo];
			float num3 = uicharInfo2.cursorPos.x / this.myField.textComponent.pixelsPerUnit;
			float num4 = uicharInfo2.cursorPos.y / this.myField.textComponent.pixelsPerUnit;
			return new Vector2(num3, num4);
		}

		// Token: 0x0400670C RID: 26380
		private float blinkTimer;

		// Token: 0x0400670D RID: 26381
		private readonly int lastCharDist = 2;

		// Token: 0x0400670E RID: 26382
		private RectTransform myCaret;

		// Token: 0x0400670F RID: 26383
		private InputField myField;

		// Token: 0x04006710 RID: 26384
		private bool selectingText;
	}
}
