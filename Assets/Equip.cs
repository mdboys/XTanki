using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets
{
	// Token: 0x02002CCD RID: 11469
	public class Equip : MonoBehaviour
	{
		// Token: 0x06009FE9 RID: 40937 RVA: 0x0015D724 File Offset: 0x0015B924
		public void Do()
		{
			Animator component = base.GetComponent<Animator>();
			if (!component.GetBool("equipped") && Equip.equipped != null && Equip.equipped != this)
			{
				Equip.equipped.GetComponent<Animator>().SetBool("equipped", false);
			}
			component.SetBool("equipped", !component.GetBool("equipped"));
			if (component.GetBool("equipped"))
			{
				Equip.equipped = this;
			}
		}

		// Token: 0x06009FEA RID: 40938 RVA: 0x0005CD85 File Offset: 0x0005AF85
		public void Claim()
		{
			base.GetComponent<Animator>().SetTrigger("claim");
		}

		// Token: 0x06009FEB RID: 40939 RVA: 0x0005CD97 File Offset: 0x0005AF97
		public void Cancel()
		{
			base.GetComponent<Animator>().SetBool("disassembled", false);
		}

		// Token: 0x06009FEC RID: 40940 RVA: 0x0005CDAA File Offset: 0x0005AFAA
		public void Dis()
		{
			base.GetComponent<Animator>().SetBool("disassembled", true);
		}

		// Token: 0x06009FED RID: 40941 RVA: 0x00011086 File Offset: 0x0000F286
		public void OnClaim()
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0400676C RID: 26476
		[Nullable(1)]
		private static Equip equipped;
	}
}
