using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000043 RID: 67
[NullableContext(1)]
[Nullable(0)]
[ExecuteInEditMode]
public class ME_ParticleGravityPoint : MonoBehaviour
{
	// Token: 0x06000100 RID: 256 RVA: 0x00005E27 File Offset: 0x00004027
	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
		this.mainModule = this.ps.main;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0005FC3C File Offset: 0x0005DE3C
	private void LateUpdate()
	{
		int maxParticles = this.mainModule.maxParticles;
		if (this.particles == null || this.particles.Length < maxParticles)
		{
			this.particles = new ParticleSystem.Particle[maxParticles];
		}
		int num = this.ps.GetParticles(this.particles);
		Vector3 vector = Vector3.zero;
		if (this.mainModule.simulationSpace == ParticleSystemSimulationSpace.Local)
		{
			vector = base.transform.InverseTransformPoint(this.target.position);
		}
		if (this.mainModule.simulationSpace == ParticleSystemSimulationSpace.World)
		{
			vector = this.target.position;
		}
		float num2 = Time.deltaTime * this.Force;
		if (this.DistanceRelative)
		{
			num2 *= Mathf.Abs((this.prevPos - vector).magnitude);
		}
		for (int i = 0; i < num; i++)
		{
			Vector3 vector2 = Vector3.Normalize(vector - this.particles[i].position);
			if (this.DistanceRelative)
			{
				vector2 = Vector3.Normalize(vector - this.prevPos);
			}
			Vector3 vector3 = vector2 * num2;
			ParticleSystem.Particle[] array = this.particles;
			int num3 = i;
			array[num3].velocity = array[num3].velocity + vector3;
		}
		this.ps.SetParticles(this.particles, num);
		this.prevPos = vector;
	}

	// Token: 0x040000A2 RID: 162
	public Transform target;

	// Token: 0x040000A3 RID: 163
	public float Force = 1f;

	// Token: 0x040000A4 RID: 164
	public bool DistanceRelative;

	// Token: 0x040000A5 RID: 165
	private ParticleSystem.MainModule mainModule;

	// Token: 0x040000A6 RID: 166
	private ParticleSystem.Particle[] particles;

	// Token: 0x040000A7 RID: 167
	private Vector3 prevPos;

	// Token: 0x040000A8 RID: 168
	private ParticleSystem ps;
}
