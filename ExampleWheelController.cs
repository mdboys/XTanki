using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class ExampleWheelController : MonoBehaviour
{
	// Token: 0x060000AD RID: 173 RVA: 0x00005AE3 File Offset: 0x00003CE3
	private void Start()
	{
		this.m_Rigidbody = base.GetComponent<Rigidbody>();
		this.m_Rigidbody.maxAngularVelocity = 100f;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0005F00C File Offset: 0x0005D20C
	private void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.m_Rigidbody.AddRelativeTorque(new Vector3(-1f * this.acceleration, 0f, 0f), ForceMode.Acceleration);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.m_Rigidbody.AddRelativeTorque(new Vector3(1f * this.acceleration, 0f, 0f), ForceMode.Acceleration);
		}
		float num = (0f - this.m_Rigidbody.angularVelocity.x) / 100f;
		if (this.motionVectorRenderer)
		{
			this.motionVectorRenderer.material.SetFloat(ExampleWheelController.Uniforms._MotionAmount, Mathf.Clamp(num, -0.25f, 0.25f));
		}
	}

	// Token: 0x0400005D RID: 93
	public float acceleration;

	// Token: 0x0400005E RID: 94
	[Nullable(1)]
	public Renderer motionVectorRenderer;

	// Token: 0x0400005F RID: 95
	[Nullable(1)]
	private Rigidbody m_Rigidbody;

	// Token: 0x0200002A RID: 42
	private static class Uniforms
	{
		// Token: 0x04000060 RID: 96
		internal static readonly int _MotionAmount = Shader.PropertyToID("_MotionAmount");
	}
}
