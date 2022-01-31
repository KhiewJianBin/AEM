using System;
using UnityEngine;
using System.Collections.Generic;
using AEM.Managers.Tilemanager;

public class AStarTileManager : Tilemanager
{
    /// <summary>
    /// Singleton instance for other scripts to access
    /// </summary>
    public new static AStarTileManager Instance;

    public Tile SolidTile;
    public TextAsset textfile1;

    public override void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Debug.LogWarning("There is already a TileManager in the Game. Removing TileManager from " + name);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public override void Start()
    {
        //test add tiledata
        Tileset maintileset = new Tileset();
        maintileset.TileData = FileHelper.ReadTilemapChar(textfile1);
        maintileset.TileSize = new Vector2(1,1);

        Tilesets.Add("Main",maintileset);
        TilesetSpawned.Add("Main",null);
        TilePieces.Add('#',SolidTile);
        SpawnTileset("Main","Main",Vector3.zero,OriginAnchor.BOTTOMLEFT);
    }
}