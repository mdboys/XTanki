using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerformanceManager : MonoBehaviour
{
    private static PerformanceManager _instance;
    public static PerformanceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PerformanceManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("PerformanceManager");
                    _instance = go.AddComponent<PerformanceManager>();
                }
            }
            return _instance;
        }
    }

    private Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (_objectPool.ContainsKey(prefab.name) && _objectPool[prefab.name].Count > 0)
        {
            GameObject obj = _objectPool[prefab.name].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            obj.name = prefab.name;
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        if (_objectPool.ContainsKey(obj.name))
        {
            _objectPool[obj.name].Enqueue(obj);
        }
        else
        {
            _objectPool.Add(obj.name, new Queue<GameObject>());
            _objectPool[obj.name].Enqueue(obj);
        }
        obj.SetActive(false);
    }

    public void PreloadAssets()
    {
        // In a real project, this would be driven by a configuration file
        // that specifies which assets to preload for each scene.
        // For now, we'll just preload some common assets.

        // Example:
        // StartCoroutine(LoadAssetBundle("asset_bundle_name"));
    }

    private IEnumerator LoadAssetBundle(string assetBundleName)
    {
        string url = "file://" + Application.streamingAssetsPath + "/" + assetBundleName;
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Debug.LogError("Failed to load AssetBundle: " + www.error);
            yield break;
        }

        AssetBundle bundle = www.assetBundle;
        // You can now load assets from the bundle
        // Example:
        // GameObject prefab = bundle.LoadAsset<GameObject>("MyPrefab");
        // PreloadObject(prefab, 10);
    }

    public void PreloadObject(GameObject prefab, int count)
    {
        if (!_objectPool.ContainsKey(prefab.name))
        {
            _objectPool.Add(prefab.name, new Queue<GameObject>());
        }

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.name = prefab.name;
            obj.SetActive(false);
            _objectPool[prefab.name].Enqueue(obj);
        }
    }

    public void UnloadUnusedAssets()
    {
        StartCoroutine(UnloadUnusedAssetsCoroutine());
    }

    private IEnumerator UnloadUnusedAssetsCoroutine()
    {
        // Unload unused assets at the end of the frame to avoid performance spikes.
        yield return new WaitForEndOfFrame();
        Resources.UnloadUnusedAssets();
    }
}
