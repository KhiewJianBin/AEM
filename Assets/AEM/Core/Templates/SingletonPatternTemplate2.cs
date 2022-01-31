using UnityEngine;

/// <summary>
/// Template Script For Singleton Pattern
/// Auto Creates a new Gameobject when trying to obtain Static Instance when no instance has spawned yet
/// Also Act As a Manager that can be Initlized At Start
/// </summary>
public class SingletonPatternTemplate2 : MonoBehaviour
{
    private static SingletonPatternTemplate2 instance;
    public static SingletonPatternTemplate2 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SingletonPattern2").AddComponent<SingletonPatternTemplate2>();
            }
                
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this); //Optional For Persisting Though Scenes
    }
}
