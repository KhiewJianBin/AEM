using UnityEngine;

public class GameSceneTemplate : GameManager
{
    //Static Instance - So that Everyone can Access it
    public static GameSceneTemplate Instance;

    //TODO Decalre Required Components
    public Camera GameCamera;

    public override void Awake()
    {
        base.Awake();

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public override void Start()
    {
        base.Start();

        //TODO Check Required Components if they are assign in inspector
        if (!GameCamera)
        {
            Debug.LogError("GameCamera is missing");
            enabled = false;
            return;
        }
    }

    void Update()
    {

    }
}