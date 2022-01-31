#define ENABLE_LOG

using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	#region Fields
	/// <summary>
	/// The instance.
	/// </summary>
	private static T instance;
	#endregion

	#region Properties
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get
		{
			if ( instance == null )
			{
				instance = FindObjectOfType<T> ();
			}
			return instance;
		}
	}

    #endregion

    #region Methods
	/// <summary>
	/// Use this for initialization.
	/// </summary>
	protected virtual void Awake ()
	{
		if ( instance == null )
		{
			instance = this as T;
			DontDestroyOnLoad ( gameObject );
		}
		else
		{
            Debug.LogWarning("There is already an instance of this Singleton. Removing instance from:" + name);
            Destroy ( gameObject );
		}
	}
    #endregion
}