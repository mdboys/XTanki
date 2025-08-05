using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000044 RID: 68
[NullableContext(1)]
[Nullable(0)]
public class ME_ParticleTrails : MonoBehaviour
{
    // Token: 0x06000103 RID: 259 RVA: 0x0005FD88 File Offset: 0x0005DF88
    private void Start()
    {
        this.ps = base.GetComponent<ParticleSystem>();
        this.particles = new ParticleSystem.Particle[this.ps.main.maxParticles];
    }

    // Token: 0x06000104 RID: 260 RVA: 0x00005E59 File Offset: 0x00004059
    private void Update()
    {
        this.UpdateTrail();
    }

    // Token: 0x06000105 RID: 261 RVA: 0x00005E61 File Offset: 0x00004061
    private void OnEnable()
    {
        base.InvokeRepeating("ClearEmptyHashes", 1f, 1f);
    }

    // Token: 0x06000106 RID: 262 RVA: 0x00005E78 File Offset: 0x00004078
    private void OnDisable()
    {
        this.Clear();
        base.CancelInvoke("ClearEmptyHashes");
    }

    // Token: 0x06000107 RID: 263 RVA: 0x0005FDC0 File Offset: 0x0005DFC0
    public void Clear()
    {
        foreach (GameObject gameObject in this.currentGO)
        {
            PerformanceManager.Instance.ReturnObject(gameObject);
        }
        this.currentGO.Clear();
    }

    // Token: 0x06000108 RID: 264 RVA: 0x0005FE1C File Offset: 0x0005E01C
    private void UpdateTrail()
    {
        this.newHashTrails.Clear();
        int num = this.ps.GetParticles(this.particles);
        for (int i = 0; i < num; i++)
        {
            if (!this.hashTrails.ContainsKey(this.particles[i].randomSeed))
            {
                GameObject gameObject = PerformanceManager.Instance.GetObject(this.TrailPrefab);
                gameObject.transform.parent = base.transform;
                this.currentGO.Add(gameObject);
                this.newHashTrails.Add(this.particles[i].randomSeed, gameObject);
                gameObject.GetComponent<LineRenderer>().widthMultiplier *= this.particles[i].startSize;
            }
            else
            {
                GameObject gameObject2 = this.hashTrails[this.particles[i].randomSeed];
                if (gameObject2 != null)
                {
                    LineRenderer component = gameObject2.GetComponent<LineRenderer>();
                    component.startColor *= this.particles[i].GetCurrentColor(this.ps);
                    component.endColor *= this.particles[i].GetCurrentColor(this.ps);
                    if (this.ps.main.simulationSpace == ParticleSystemSimulationSpace.World)
                    {
                        gameObject2.transform.position = this.particles[i].position;
                    }
                    if (this.ps.main.simulationSpace == ParticleSystemSimulationSpace.Local)
                    {
                        gameObject2.transform.position = this.ps.transform.TransformPoint(this.particles[i].position);
                    }
                    this.newHashTrails.Add(this.particles[i].randomSeed, gameObject2);
                }
                this.hashTrails.Remove(this.particles[i].randomSeed);
            }
        }
        foreach (KeyValuePair<uint, GameObject> keyValuePair in this.hashTrails)
        {
            if (keyValuePair.Value != null)
            {
                keyValuePair.Value.GetComponent<ME_TrailRendererNoise>().IsActive = false;
            }
        }
        this.AddRange<uint, GameObject>(this.hashTrails, this.newHashTrails);
    }

    // Token: 0x06000109 RID: 265 RVA: 0x000600A8 File Offset: 0x0005E2A8
    public void AddRange<[Nullable(2)] T, [Nullable(2)] S>(Dictionary<T, S> source, Dictionary<T, S> collection)
    {
        if (collection == null)
        {
            return;
        }
        foreach (KeyValuePair<T, S> keyValuePair in collection)
        {
            if (!source.ContainsKey(keyValuePair.Key))
            {
                source.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }

    // Token: 0x0600010A RID: 266 RVA: 0x00060118 File Offset: 0x0005E318
    private void ClearEmptyHashes()
    {
        this.hashTrails = this.hashTrails.Where((KeyValuePair<uint, GameObject> h) => h.Value != null).ToDictionary((KeyValuePair<uint, GameObject> h) => h.Key, (KeyValuePair<uint, GameObject> h) => h.Value);
    }

    // Token: 0x040000A9 RID: 169
    public GameObject TrailPrefab;

    // Token: 0x040000AA RID: 170
    private readonly List<GameObject> currentGO = new List<GameObject>();

    // Token: 0x040000AB RID: 171
    private Dictionary<uint, GameObject> hashTrails = new Dictionary<uint, GameObject>();

    // Token: 0x040000AC RID: 172
    private readonly Dictionary<uint, GameObject> newHashTrails = new Dictionary<uint, GameObject>();

    // Token: 0x040000AD RID: 173
    private ParticleSystem.Particle[] particles;

    // Token: 0x040000AE RID: 174
    private ParticleSystem ps;
}
