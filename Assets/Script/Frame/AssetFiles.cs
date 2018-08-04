//#define FORCE_ASYNC
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Asset = UnityEngine.Object;
/// <summary>
/// Asset async updater.
/// </summary>
public interface AssetAsyncUpdater
{
	void UpdateComplete(string path, bool update, bool cache);
}
/// <summary>
/// Asset async loader.
/// </summary>
public interface AssetAsyncLoader
{
	void LoadComplete(string path, Asset asset);
}
/// <summary>
/// Asset files. (external)
/// </summary>
public sealed partial class AssetFiles : ScriptSingleton<AssetFiles>
{
	public bool UseAssetBundle
	{
		get
		{
			return useAssetBundle;
		}
	}
	public void SetUseAssetBundle(bool useAssetBundle)
	{
		this.useAssetBundle = useAssetBundle;
	}
	public void SetAddress(string address)
	{
		this.address = address;
	}
	public void SetProjectName(string projectName)
	{
		this.projectName = projectName;
	}
	public void SetAssetFilesPath(string assetFilesPath)
	{
		this.assetFilesPath = assetFilesPath;
	}
	public void SetAssetBundlePath(string assetBundlePath)
	{
		this.assetBundlePath = assetBundlePath;
	}
	public Asset LoadAsset(string path)
	{
		return LoadAsset(path, null, defaultVersion);
	}
	public Asset LoadAsset(string path, string tag)
	{
		return LoadAsset(path, tag, defaultVersion);
	}
	public Asset LoadAsset(string path, string tag, int version)
	{
		// asset load

		Asset asset = null;
		if (assetFiles.ContainsKey(path))
		{
			AssetFile assetFile = assetFiles[path];
			if (assetFiles != null)
			{
				if (assetFile.type == AssetFile.Types.Asset)
				{
					asset = assetFile.asset;
				}
			}
		}
		else
		{
			if (useAssetBundle)
			{
				asset = LoadAssetBundle(path, tag, version);

#if UNITY_EDITOR
				if (asset == null)
				{
					asset = LoadAssetAtPath(path, tag, version, false);
				}
				if (asset == null)
				{
					asset = LoadAssetAtPath(path, tag, version, true);
				}
#endif
				if (asset == null)
				{
					asset = LoadResources(path, tag, version);
				}
			}
			else
			{
				asset = LoadResources(path, tag, version);
				if (asset == null)
				{
					asset = LoadAssetAtPath(path, tag, version, false);
				}
				if (asset == null)
				{
					asset = LoadAssetAtPath(path, tag, version, true);
				}
				if (asset == null)
				{
					asset = LoadAssetBundle(path, tag, version);
				}
			}
		}

		if (asset == null)
		{
			LogFile.Write(this, path + " Not found.");
		}
		return asset;
	}
	public void ReleaseAsset(string path, bool unloadAllLoadedObjects)
	{
		if (assetFiles.ContainsKey(path))
		{
			AssetFile assetFile = assetFiles[path];
			if (assetFile != null)
			{
				UnloadAsset(assetFile, unloadAllLoadedObjects);
			}
			assetFiles.Remove(path);
		}
	}
	public bool ClearAsset(string tag)
	{
		return ClearAsset(tag, false);
	}
	public bool ClearAsset(string tag, bool unloadAllLoadedObjects)
	{
		List<string> clearList = new List<string>();
		foreach (KeyValuePair<string, AssetFile> pair in assetFiles)
		{
			if (tag == null)
			{
				if (pair.Value.tag == null)
				{
					UnloadAsset(pair.Value, unloadAllLoadedObjects);
					clearList.Add(pair.Key);
				}
			}
			else if (tag.Equals(pair.Value.tag))
			{
				UnloadAsset(pair.Value, unloadAllLoadedObjects);
				clearList.Add(pair.Key);
			}
		}
		if (clearList.Count > 0)
		{
			foreach (string path in clearList)
			{
				assetFiles.Remove(path);
			}
			LogFile.Write(this, clearList.Count + " AssetFile Clear.");
			clearList.Clear();
			return true;
		}
		return false;
	}
	public void ClearAssetAll(bool unloadAllLoadedObjects)
	{
		foreach (KeyValuePair<string, AssetFile> pair in assetFiles)
		{
			UnloadAsset(pair.Value, unloadAllLoadedObjects);
		}
		assetFiles.Clear();
	}
}

//2015.03.17
//번들 관련 코드는 이 아래에 있다.
//새롭게 수정해야 할 필요가 있을 수 있다.
/// <summary>
/// Asset files. (internal)
/// </summary>
partial class AssetFiles
{
	class AssetFile
	{
		public enum Types
		{
			Asset,
		}
		public Types type;
		public Asset asset;
		public string tag;
		public int version;
		public AssetFile(Types type, Asset asset, string tag, int version)
		{
			this.type = type;
			this.asset = asset;
			this.tag = tag;
			this.version = version;
		}
		public AssetFile(Types type, int version)
		{
			this.type = type;
			this.asset = null;
			this.tag = null;
			this.version = version;
		}
	}
	int defaultVersion = 1;
	string address = null;
	string projectName = null;
	string assetFilesPath = null;
	string assetBundlePath = null;
	bool useAssetBundle = false;

	Dictionary<string, AssetFile> assetFiles = new Dictionary<string, AssetFile>();
	void OnEnable()
	{
		address = "file://";
		LogFile.Register(this);
		LogFile.SetConsoleView(this, false);
	}
	void OnDisable()
	{
		ClearAssetAll(true);
		LogFile.Unregister(this);
	}
	void UnloadAsset(AssetFile assetFile, bool unloadAllLoadedObjects)
	{
		if (assetFile != null)
		{
			if (assetFile.type == AssetFile.Types.Asset)
			{		
			}
		}
	}
	Asset LoadResources(string path, string tag, int version)
	{		
		string assetPath = RemoveFileExtension(path);		
		Asset asset = Resources.Load(assetPath, typeof(Asset));
		if (asset != null)
		{
			//assetFiles.Add(path, new AssetFile(AssetFile.Types.Asset, asset, tag, version));
		}
		return asset;
	}
	Asset LoadAssetAtPath(string path, string tag, int version, bool intact)
	{
#if UNITY_EDITOR
		string assetPath = (intact)? path : (assetFilesPath + "/" + path);
		Asset asset = UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, typeof(Asset));
		if (asset != null)
		{		
			//assetFiles.Add(path, new AssetFile(AssetFile.Types.Asset, asset, tag, version));
			return asset;
		}
#endif
		return null;
	}
	Asset LoadAssetBundle(string path, string tag, int version)
	{
        //2015.03.17
        return null;// AssetBundleCache.Instant.LoadAssetBundle(path);
	}

	string RemoveFileExtension(string filePath)
	{
		int index = filePath.LastIndexOf(".");
		if (index > 0)
		{
			filePath = filePath.Remove(index);
		}
		return filePath;
	}

	//---------------------------------------------------------------------------------
	// public function...

	public void ClearAssetBundle( object scene )
	{
		if (useAssetBundle == false)
			return;

        //2015.03.17
		//AssetBundleManager.Instant.ClearAssetBundle (scene);
	}

	public void AddGlobalPrefab( string filePath )
	{
		if (useAssetBundle == false)
			return;

        //2015.03.17
		//AssetBundleCache.Instant.AddGlobalPrefab (filePath);
	}

	public bool isResourceUnloadUnusedAssets( object scene )
	{
		//if (useAssetBundle == false)
		//	return true;

        //2015.03.17
		/*if (DevelopData.Instant.IsMobile())
		{
			if(ResourceManager.Instant.CheckMemory())
				return AssetBundleManager.Instant.isResourceUnloadUnusedAssets (scene);
			else
				return true;
		}
		else*/
			return true;
	}
}
