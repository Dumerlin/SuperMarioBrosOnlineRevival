using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The base class for Singletons, objects that should have only one instance.
/// Mostly used for managers.
/// </summary>
/// <typeparam name="T">A type derived from MonoBehaviour.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Gets the instance of this Singleton. If it doesn't exist and can't be found in the scene, it will be created.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance != null) return instance;

            //Print an error message and return if the instance is null and this is called after quitting out
            //This prevents this Singletons from leaking into the scene
            if (ApplicationQuit == true)
            {
                Debug.LogError("Attempting to create Singleton instance after the application quit");

                return instance;
            }

            //Try to find the instance in the scene
            instance = FindObjectOfType<T>();

            //Try to load from resources if it's not in the scene
            if (instance == null)
            {
                T prefab = Resources.Load<T>("Prefabs/" + typeof(T).Name);

                if (prefab != null)
                {
                    instance = Instantiate<T>(prefab);
                }
            }

            //The instance can't be found, so create it
            if (instance == null)
            {
                GameObject singletonObj = new GameObject(typeof(T).Name);
                instance = singletonObj.AddComponent<T>();
            }

            return instance;
        }
    }

    /// <summary>
    /// Tells whether the Singleton has an instance or not.
    /// </summary>
    public static bool HasInstance
    {
        get { return instance != null; }
    }

    /// <summary>
    /// Instance reference.
    /// </summary>
    private static T instance = null;

    /// <summary>
    /// Set to true when the Application quits.
    /// Prevents Singletons leaking into scenes by not creating new instances.
    /// </summary>
    private static bool ApplicationQuit = false;

    protected virtual void Awake()
    {
        VerifyInstance();
    }

    protected virtual void OnDestroy()
    {
        if (this == instance)
        {
            instance = null;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        ApplicationQuit = true;
    }

    /// <summary>
    /// Verifies that this instance is the only Singleton instance for this type. If not, this object is destroyed.
    /// </summary>
    /// <returns></returns>
    protected bool VerifyInstance()
    {
        if (this == Instance)
            return true;

        Destroy(this.gameObject);
        return false;
    }
}
