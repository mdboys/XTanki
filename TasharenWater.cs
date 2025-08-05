using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000088 RID: 136
[NullableContext(1)]
[Nullable(0)]
[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
[AddComponentMenu("Tasharen/Water")]
public class TasharenWater : MonoBehaviour
{
	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000295 RID: 661 RVA: 0x00006E1E File Offset: 0x0000501E
	// (set) Token: 0x06000296 RID: 662 RVA: 0x00006E26 File Offset: 0x00005026
	public bool depthTextureSupport { get; private set; }

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000297 RID: 663 RVA: 0x00067680 File Offset: 0x00065880
	public int reflectionTextureSize
	{
		get
		{
			TasharenWater.Quality quality = this.quality;
			if (quality - TasharenWater.Quality.Medium <= 1)
			{
				return 512;
			}
			if (quality == TasharenWater.Quality.Uber)
			{
				return 1024;
			}
			return 0;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000298 RID: 664 RVA: 0x000676AC File Offset: 0x000658AC
	public LayerMask reflectionMask
	{
		get
		{
			TasharenWater.Quality quality = this.quality;
			if (quality == TasharenWater.Quality.Medium)
			{
				return this.mediumReflectionMask;
			}
			if (quality - TasharenWater.Quality.High <= 1)
			{
				return this.highReflectionMask;
			}
			return 0;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000299 RID: 665 RVA: 0x00006E2F File Offset: 0x0000502F
	public bool useRefraction
	{
		get
		{
			return this.quality > TasharenWater.Quality.Fastest;
		}
	}

	// Token: 0x0600029A RID: 666 RVA: 0x000676E0 File Offset: 0x000658E0
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mRen = base.GetComponent<Renderer>();
		this.mSpecular = new Color32(147, 147, 147, byte.MaxValue);
		this.depthTextureSupport = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth);
	}

	// Token: 0x0600029B RID: 667 RVA: 0x00067738 File Offset: 0x00065938
	private void LateUpdate()
	{
		if (!this.keepUnderCamera)
		{
			return;
		}
		Camera main = Camera.main;
		if (!(main == null))
		{
			Vector3 position = main.transform.position;
			position.y = this.mTrans.position.y;
			if (this.mTrans.position != position)
			{
				this.mTrans.position = position;
			}
		}
	}

	// Token: 0x0600029C RID: 668 RVA: 0x00006E3A File Offset: 0x0000503A
	private void OnEnable()
	{
		TasharenWater.instance = this;
		this.mStreamingWater = PlayerPrefs.GetInt("Streaming Water", 0) == 1;
		if (this.automaticQuality)
		{
			this.quality = TasharenWater.GetQuality();
		}
	}

	// Token: 0x0600029D RID: 669 RVA: 0x000677A0 File Offset: 0x000659A0
	private void OnDisable()
	{
		this.Clear();
		foreach (object obj in this.mCameras)
		{
			global::UnityEngine.Object.DestroyImmediate(((Camera)((DictionaryEntry)obj).Value).gameObject);
		}
		this.mCameras.Clear();
		if (TasharenWater.instance == this)
		{
			TasharenWater.instance = null;
		}
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0006782C File Offset: 0x00065A2C
	private void OnWillRenderObject()
	{
		if (TasharenWater.mIsRendering)
		{
			return;
		}
		if (!base.enabled || !this.mRen || !this.mRen.enabled)
		{
			this.Clear();
			return;
		}
		Material sharedMaterial = this.mRen.sharedMaterial;
		if (!sharedMaterial)
		{
			return;
		}
		Camera current = Camera.current;
		if (!current)
		{
			return;
		}
		if (this.mStreamingWater)
		{
			sharedMaterial.SetColor("_Specular", Color.black);
		}
		else
		{
			sharedMaterial.SetColor("_Specular", this.mSpecular);
		}
		if (!this.depthTextureSupport)
		{
			this.quality = TasharenWater.Quality.Fastest;
		}
		if (this.quality == TasharenWater.Quality.Fastest)
		{
			sharedMaterial.shader.maximumLOD = 100;
			int num = 256;
			float num2 = (float)num * 0.5f;
			sharedMaterial.SetFloat("_InvScale", 1f / (float)num);
			Terrain activeTerrain = Terrain.activeTerrain;
			float num3 = ((!(activeTerrain != null)) ? 0f : activeTerrain.transform.position.y);
			if (activeTerrain != null)
			{
				if (this.mDepthTex == null)
				{
					this.mDepthTexIsValid = false;
					this.mDepthTex = new Texture2D(num, num, TextureFormat.Alpha8, false);
				}
				if (!this.mDepthTexIsValid)
				{
					this.mDepthTexIsValid = true;
					Color32[] array = new Color32[num * num];
					float num4 = (float)(num + 1) / (float)num;
					for (int i = 0; i < num; i++)
					{
						float num5 = 0f - num2 + (float)i * num4;
						for (int j = 0; j < num; j++)
						{
							float num6 = 0f - num2 + (float)j * num4;
							float num7 = activeTerrain.SampleHeight(new Vector3(num6, 0f, num5)) + num3;
							if (num7 < 0f)
							{
								array[j + i * num].a = (byte)Mathf.RoundToInt(255f * Mathf.Clamp01((0f - num7) * 0.125f));
							}
							else
							{
								num7 = (float)(array[j + i * num].a = 0);
							}
						}
					}
					this.mDepthTex.SetPixels32(array);
					this.mDepthTex.wrapMode = TextureWrapMode.Clamp;
					this.mDepthTex.Apply();
				}
			}
			sharedMaterial.SetTexture("_DepthTex", this.mDepthTex);
			return;
		}
		if (this.quality == TasharenWater.Quality.Low)
		{
			sharedMaterial.shader.maximumLOD = 200;
			this.Clear();
			return;
		}
		current.depthTextureMode |= DepthTextureMode.Depth;
		LayerMask reflectionMask = this.reflectionMask;
		int reflectionTextureSize = this.reflectionTextureSize;
		if (reflectionMask == 0 || reflectionTextureSize < 512)
		{
			sharedMaterial.shader.maximumLOD = 300;
			this.Clear();
			return;
		}
		sharedMaterial.shader.maximumLOD = 400;
		TasharenWater.mIsRendering = true;
		Camera reflectionCamera = this.GetReflectionCamera(current, sharedMaterial, reflectionTextureSize);
		Vector3 position = this.mTrans.position;
		Vector3 up = this.mTrans.up;
		this.CopyCamera(current, reflectionCamera);
		float num8 = 0f - Vector3.Dot(up, position);
		this.mReflectionPlane.x = up.x;
		this.mReflectionPlane.y = up.y;
		this.mReflectionPlane.z = up.z;
		this.mReflectionPlane.w = num8;
		Matrix4x4 zero = Matrix4x4.zero;
		TasharenWater.CalculateReflectionMatrix(ref zero, this.mReflectionPlane);
		Vector3 position2 = current.transform.position;
		Vector3 vector = zero.MultiplyPoint(position2);
		reflectionCamera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
		Vector4 vector2 = this.CameraSpacePlane(reflectionCamera, position, up, 1f);
		Matrix4x4 projectionMatrix = current.projectionMatrix;
		TasharenWater.CalculateObliqueMatrix(ref projectionMatrix, vector2);
		reflectionCamera.projectionMatrix = projectionMatrix;
		reflectionCamera.cullingMask = -17 & reflectionMask.value;
		reflectionCamera.targetTexture = this.mTex;
		GL.SetRevertBackfacing(true);
		reflectionCamera.transform.position = vector;
		Vector3 eulerAngles = current.transform.eulerAngles;
		eulerAngles.x = 0f;
		reflectionCamera.transform.eulerAngles = eulerAngles;
		reflectionCamera.Render();
		reflectionCamera.transform.position = position2;
		GL.SetRevertBackfacing(false);
		TasharenWater.mIsRendering = false;
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00006E69 File Offset: 0x00005069
	private static float SignExt(float a)
	{
		if (a > 0f)
		{
			return 1f;
		}
		if (a < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x00067C6C File Offset: 0x00065E6C
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		TasharenWater.mTemp.x = TasharenWater.SignExt(clipPlane.x);
		TasharenWater.mTemp.y = TasharenWater.SignExt(clipPlane.y);
		Vector4 vector = projection.inverse * TasharenWater.mTemp;
		Vector4 vector2 = clipPlane * (2f / Vector4.Dot(clipPlane, vector));
		projection[2] = vector2.x - projection[3];
		projection[6] = vector2.y - projection[7];
		projection[10] = vector2.z - projection[11];
		projection[14] = vector2.w - projection[15];
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x00067D28 File Offset: 0x00065F28
	private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
		reflectionMat.m01 = -2f * plane[0] * plane[1];
		reflectionMat.m02 = -2f * plane[0] * plane[2];
		reflectionMat.m03 = -2f * plane[3] * plane[0];
		reflectionMat.m10 = -2f * plane[1] * plane[0];
		reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
		reflectionMat.m12 = -2f * plane[1] * plane[2];
		reflectionMat.m13 = -2f * plane[3] * plane[1];
		reflectionMat.m20 = -2f * plane[2] * plane[0];
		reflectionMat.m21 = -2f * plane[2] * plane[1];
		reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
		reflectionMat.m23 = -2f * plane[3] * plane[2];
		reflectionMat.m30 = 0f;
		reflectionMat.m31 = 0f;
		reflectionMat.m32 = 0f;
		reflectionMat.m33 = 1f;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x00006E8C File Offset: 0x0000508C
	public static TasharenWater.Quality GetQuality()
	{
		return (TasharenWater.Quality)PlayerPrefs.GetInt("Water", 3);
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x00067ED0 File Offset: 0x000660D0
	public static void SetQuality(TasharenWater.Quality q)
	{
		TasharenWater[] array = global::UnityEngine.Object.FindObjectsOfType(typeof(TasharenWater)) as TasharenWater[];
		if (array.Length != 0)
		{
			TasharenWater[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].quality = q;
			}
			return;
		}
		PlayerPrefs.SetInt("Water", (int)q);
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x00006E99 File Offset: 0x00005099
	private void Clear()
	{
		if (this.mTex)
		{
			global::UnityEngine.Object.DestroyImmediate(this.mTex);
			this.mTex = null;
		}
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x00067F1C File Offset: 0x0006611C
	private void CopyCamera(Camera src, Camera dest)
	{
		dest.clearFlags = src.clearFlags;
		dest.backgroundColor = src.backgroundColor;
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
		dest.depthTextureMode = DepthTextureMode.None;
		dest.renderingPath = RenderingPath.Forward;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x00067F98 File Offset: 0x00066198
	private Camera GetReflectionCamera(Camera current, Material mat, int textureSize)
	{
		if (!this.mTex || this.mTexSize != textureSize)
		{
			if (this.mTex)
			{
				global::UnityEngine.Object.DestroyImmediate(this.mTex);
			}
			this.mTex = new RenderTexture(textureSize, textureSize, 16)
			{
				name = string.Format("__MirrorReflection{0}", base.GetInstanceID()),
				isPowerOfTwo = true,
				hideFlags = HideFlags.DontSave
			};
			this.mTexSize = textureSize;
		}
		Camera camera = this.mCameras[current] as Camera;
		if (!camera)
		{
			camera = new GameObject(string.Format("Mirror Refl Camera id{0} for {1}", base.GetInstanceID(), current.GetInstanceID()), new Type[]
			{
				typeof(Camera),
				typeof(Skybox)
			})
			{
				hideFlags = HideFlags.HideAndDontSave
			}.GetComponent<Camera>();
			camera.enabled = false;
			Transform transform = camera.transform;
			transform.position = this.mTrans.position;
			transform.rotation = this.mTrans.rotation;
			camera.gameObject.AddComponent<FlareLayer>();
			this.mCameras[current] = camera;
		}
		if (mat.HasProperty("_ReflectionTex"))
		{
			mat.SetTexture("_ReflectionTex", this.mTex);
		}
		return camera;
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x000680EC File Offset: 0x000662EC
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 vector = worldToCameraMatrix.MultiplyPoint(pos);
		Vector3 vector2 = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(vector2.x, vector2.y, vector2.z, 0f - Vector3.Dot(vector, vector2));
	}

	// Token: 0x040001F2 RID: 498
	public static TasharenWater instance;

	// Token: 0x040001F3 RID: 499
	private static bool mIsRendering;

	// Token: 0x040001F4 RID: 500
	private static Vector3 mTemp = Vector4.one;

	// Token: 0x040001F5 RID: 501
	public TasharenWater.Quality quality = TasharenWater.Quality.High;

	// Token: 0x040001F6 RID: 502
	public LayerMask highReflectionMask = -1;

	// Token: 0x040001F7 RID: 503
	public LayerMask mediumReflectionMask = -1;

	// Token: 0x040001F8 RID: 504
	public bool keepUnderCamera = true;

	// Token: 0x040001F9 RID: 505
	public bool automaticQuality = true;

	// Token: 0x040001FA RID: 506
	private readonly Hashtable mCameras = new Hashtable();

	// Token: 0x040001FB RID: 507
	[NonSerialized]
	private Texture2D mDepthTex;

	// Token: 0x040001FC RID: 508
	[NonSerialized]
	private bool mDepthTexIsValid;

	// Token: 0x040001FD RID: 509
	[NonSerialized]
	private Vector4 mReflectionPlane;

	// Token: 0x040001FE RID: 510
	private Renderer mRen;

	// Token: 0x040001FF RID: 511
	private Color mSpecular;

	// Token: 0x04000200 RID: 512
	private bool mStreamingWater;

	// Token: 0x04000201 RID: 513
	private RenderTexture mTex;

	// Token: 0x04000202 RID: 514
	private int mTexSize;

	// Token: 0x04000203 RID: 515
	private Transform mTrans;

	// Token: 0x02000089 RID: 137
	[NullableContext(0)]
	public enum Quality
	{
		// Token: 0x04000206 RID: 518
		Fastest,
		// Token: 0x04000207 RID: 519
		Low,
		// Token: 0x04000208 RID: 520
		Medium,
		// Token: 0x04000209 RID: 521
		High,
		// Token: 0x0400020A RID: 522
		Uber
	}
}
