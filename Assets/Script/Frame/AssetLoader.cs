using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Asset = UnityEngine.Object;
/// <summary>
/// Asset loader. (external)
/// </summary>
public sealed partial class AssetLoader : MonoSingleton<AssetLoader>, AssetAsyncLoader
{
	public bool IsLoading()
	{
		return (asyncList.Count > 0);
	}
	public bool IsLoading(string path)
	{
		foreach (AsyncAsset asyncAsset in asyncList)
		{
			if (asyncAsset.path != null)
			{
				if (asyncAsset.path.Equals(path))
				{
					return true;
				}
			}
		}
		return false;
	}
	public void LoadComplete(string path, Asset asset)
	{
	}
	public void AddLoadList(string path)
	{
		asyncList.Add(new AsyncAsset(path, null));
	}
	public void AddLoadList(string path, string tag)
	{
		asyncList.Add(new AsyncAsset(path, tag));
	}
	public bool StartLoading()
	{
		asyncList.Clear();
		return false;
	}
}
/// <summary>
/// Asset loader. (internal)
/// </summary>
partial class AssetLoader
{
	struct AsyncAsset
	{
		public string path;
		public string tag;
		public AsyncAsset(string path, string tag)
		{
			this.path = path;
			this.tag = tag;
		}
	}
	List<AsyncAsset> asyncList = new List<AsyncAsset>();
	
	void Awake()
	{
		HideInHierarchy();
		this.Ready();
	}
	void OnDestroy()
	{
		this.Destroy();
	}
}
