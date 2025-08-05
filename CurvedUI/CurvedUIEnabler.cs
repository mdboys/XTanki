using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CurvedUI
{
	// Token: 0x02002CBB RID: 11451
	public class CurvedUIEnabler : MonoBehaviour
	{
		// Token: 0x06009F36 RID: 40758 RVA: 0x00158BE4 File Offset: 0x00156DE4
		public void RefreshCurve()
		{
			foreach (Graphic graphic in base.GetComponentsInChildren<Graphic>(true))
			{
				if (graphic.GetComponent<CurvedUIVertexEffect>() == null)
				{
					graphic.gameObject.AddComponent<CurvedUIVertexEffect>();
					graphic.SetAllDirty();
				}
			}
			foreach (InputField inputField in base.GetComponentsInChildren<InputField>(true))
			{
				if (inputField.GetComponent<CurvedUIInputFieldCaret>() == null)
				{
					inputField.gameObject.AddComponent<CurvedUIInputFieldCaret>();
				}
			}
			foreach (TextMeshProUGUI textMeshProUGUI in base.GetComponentsInChildren<TextMeshProUGUI>(true))
			{
				if (textMeshProUGUI.GetComponent<CurvedUITMP>() == null)
				{
					textMeshProUGUI.gameObject.AddComponent<CurvedUITMP>();
					textMeshProUGUI.SetAllDirty();
				}
			}
		}
	}
}
