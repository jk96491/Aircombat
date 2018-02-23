using UnityEngine;
/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new object();

    private static bool _UnityQuit = false;

	public static T Instance
	{
		get
		{
			lock (_lock)
			{
                if (_instance == null && _UnityQuit == false)
				{
					_instance = (T)FindObjectOfType(typeof(T));

					if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
						return _instance;
					}

					if (_instance == null)
					{
                        if (EditorResourceLoad())
                            return _instance;
                        GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<T>();
                        singleton.name = string.Format("(singleton){0}", typeof(T).ToString());
						DontDestroyOnLoad(singleton);
					}
				}
				return _instance;
			}
		}
	}

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void OnApplicationQuit()
    {
        _UnityQuit = true;
    }

  
    static bool EditorResourceLoad()
    {
        GameObject singleton = null;
        string path = string.Empty;
        if (typeof(T) == typeof(UIManager))
            path = "Prefabs/UIManager";
      

        if (string.IsNullOrEmpty(path))
        {
            singleton = new GameObject();
            _instance = singleton.AddComponent<T>();
        }
        else
        {
            //T item = Resources.Load(path, typeof(T)) as T;
            GameObject item = Resources.Load(path) as GameObject;
            singleton = GameObject.Instantiate(item.gameObject);
            _instance = singleton.GetComponent<T>();
        }
        singleton.name = string.Format("(singleton){0}", typeof(T).ToString());
        DontDestroyOnLoad(singleton);
        return true;

    }

}