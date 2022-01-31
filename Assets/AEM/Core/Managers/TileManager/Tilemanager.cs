using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AEM.Managers.Tilemanager
{
    /// <summary>
    /// struct to represent a tileset status and tiledata
    /// </summary>
    public class Tileset
    {
        public char[][] TileData;
        public Vector2 TileSize; 
    }
    public abstract class Tilemanager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance for other scripts to access
        /// </summary>
        public static Tilemanager Instance; 

        /// <summary>
        /// Key Pair values that holds all the tilesets tiledata.
        /// </summary>
        public Dictionary<string,Tileset> Tilesets = new Dictionary<string, Tileset>();

        /// <summary>
        /// Key Pair value to tell if tileset is spawned in game
        /// </summary>
        public Dictionary<string,GameObject> TilesetSpawned = new Dictionary<string,GameObject>();

        /// <summary>
        /// Key Pair values of the different type of tile pieces represented by a char, used in tilesets data
        /// </summary>
        public Dictionary<char,Tile> TilePieces = new Dictionary<char, Tile>();

        [ReadOnly]public List<GameObject> Sets = new List<GameObject>();
        [ReadOnly]public List<GameObject> Rows = new List<GameObject>();
        [ReadOnly]public List<GameObject> Tiles = new List<GameObject>();

        public virtual void Awake()
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
        public virtual void Start()
        {           
            /*For child override*/
        }

        public virtual void Update()
        {
            /*For child override*/
        }

        /// <summary>
        /// Reset the entire tilemanager , clearing all spawned tiles
        /// </summary>
        public void Reset()
        {
            /*Remove all tiles(destory gameobjects) & clearomg list*/
            foreach (GameObject sets in Sets)
            {
                DestroyImmediate(sets);
            }
            Sets.Clear();
            Rows.Clear();
            Tiles.Clear();

            /*clear all bools*/
            foreach (string key in TilesetSpawned.Keys.ToList())
            {
                TilesetSpawned[key] = null;
            }
        }

        public void RemoveTileset(string tilesetname)
        {
            /*check if key exist*/
            if(!TilesetSpawned.ContainsKey(tilesetname))
            {
                Debug.LogWarning("TilesetSpawned: "+ tilesetname +" is not a valid key. Did you spell it correctly?");
                return;
            }
            /*Check if tileset is spawned */
            if(TilesetSpawned[tilesetname])
            {
                int goIndex = Sets.IndexOf(TilesetSpawned[tilesetname]);
                DestroyImmediate(Sets[goIndex]);
                Sets.RemoveAt(goIndex);
                TilesetSpawned[tilesetname] = null;

                for(int i = Rows.Count - 1; i > -1; i--)
                {
                    if (Rows[i] == null)
                        Rows.RemoveAt(i);
                }
                for(int i = Tiles.Count - 1; i > -1; i--)
                {
                    if (Tiles[i] == null)
                        Tiles.RemoveAt(i);
                }
            }
        }
        public void ReSize(string tilesetname)
        {
            /*Resize just means remove and spawn again*/
            RemoveTileset(tilesetname);
            SpawnTileset(tilesetname,tilesetname);
        }

        /// <summary>
        /// Function to Spawn(instantiate) an entire tileset if it has not spawn
        /// </summary>
        public void SpawnTileset(string name,string tilesetname, Vector3 inSpawnPos = default(Vector3),OriginAnchor indexOrigin = OriginAnchor.TOPLEFT)
        {
            /*First check if tilesetname is valid*/
            if(!TilesetSpawned.ContainsKey(tilesetname))
            {
                Debug.LogWarning("TilesetSpawned: "+ tilesetname +" is not a valid key. Did you spell it correctly?");
                return;
            }
            if(!Tilesets.ContainsKey(tilesetname))
            {
                Debug.LogWarning("Tilesets: "+ tilesetname +" is not a valid key. Did you spell it correctly?");
                return;
            }
            if(!TilesetSpawned[tilesetname])
            {
                Tileset tileset = Tilesets[tilesetname];

                /*Create group*/
                GameObject tilesetgo = new GameObject("Tileset: " + name);
                tilesetgo.transform.SetParent(transform);
                tilesetgo.transform.position = inSpawnPos;

                /*Add tilset to tilesets group*/
                Sets.Add(tilesetgo);

                /*Determine anchor direction, for spawning*/
                OriginAnchor rowAnchor = OriginAnchor.LEFT;
                if(indexOrigin == OriginAnchor.TOPRIGHT || indexOrigin == OriginAnchor.BOTTOMRIGHT)
                {
                    rowAnchor = OriginAnchor.RIGHT;
                }
                // NOTE: Top = 1 ,Bottom = -1;
                int direction = 0;
                if(indexOrigin == OriginAnchor.TOPLEFT || indexOrigin == OriginAnchor.TOP || indexOrigin == OriginAnchor.TOPRIGHT)
                {
                    direction = -1;
                }
                else if(indexOrigin == OriginAnchor.BOTTOMLEFT || indexOrigin == OriginAnchor.BOTTOM || indexOrigin == OriginAnchor.BOTTOMRIGHT)
                {
                    direction = 1;
                }
                else
                {
                    Debug.LogError("Wrong indexorigin");
                }

                /*Spawn Tile Rows*/
                for(int rowCounter = 0;rowCounter < tileset.TileData.Length; rowCounter++)
                {
                    SpawnTilerow("Tileset: " + name +" Row:" + rowCounter,tilesetgo,new Vector3(0, inSpawnPos.y + tileset.TileSize.y * rowCounter * direction, 0),tileset.TileData[rowCounter],tileset.TileSize,rowAnchor);
                }
                TilesetSpawned[tilesetname] = tilesetgo;
            }
        }

        /// <summary>
        /// Function to Spawn(instantiate) an entire tileset any time no restriction
        /// </summary>
        internal void SpawnTileset(string name,Tileset tileset, Vector3 inSpawnPos,OriginAnchor indexOrigin = OriginAnchor.TOPLEFT)
        {
            /*Create group*/
            GameObject tilesetgo = new GameObject("Tileset: " + name);
            tilesetgo.transform.SetParent(transform);
            tilesetgo.transform.position = inSpawnPos;

            /*Add tilset to tilesets group*/
            Sets.Add(tilesetgo);

            /*Determine anchor direction, for spawning*/
            OriginAnchor rowAnchor = OriginAnchor.LEFT;
            if(indexOrigin == OriginAnchor.TOPRIGHT || indexOrigin == OriginAnchor.BOTTOMRIGHT)
            {
                rowAnchor = OriginAnchor.RIGHT;
            }
            // NOTE: Top = 1 ,Bottom = -1;
            int direction = 0;
            if(indexOrigin == OriginAnchor.TOPLEFT || indexOrigin == OriginAnchor.TOP || indexOrigin == OriginAnchor.TOPRIGHT)
            {
                direction = -1;
            }
            else if(indexOrigin == OriginAnchor.BOTTOMLEFT || indexOrigin == OriginAnchor.BOTTOM || indexOrigin == OriginAnchor.BOTTOMRIGHT)
            {
                direction = 1;
            }
            else
            {
                Debug.LogError("Wrong indexorigin");
            }

            /*Spawn Tile Rows*/
            for(int rowCounter = 0;rowCounter < tileset.TileData.Length; rowCounter++)
            {
                SpawnTilerow("Tileset: " + name +" Row:" + rowCounter,tilesetgo,new Vector3(0, inSpawnPos.y + tileset.TileSize.y * rowCounter * direction, 0),tileset.TileData[rowCounter],tileset.TileSize,rowAnchor);
            }
        }

        /// <summary>
        /// Function to Spawn(instantiate) a row of tiles and returns the row gameobject
        /// </summary>
        public GameObject SpawnTilerow(string name,GameObject parentgroup,Vector3 inSpawnLocalPos,char[] rowdata,Vector2 Tilesize,OriginAnchor indexOrigin)
        {
            /*Spawn row and set position*/
            GameObject Rowgo = new GameObject(name);
            Rows.Add(Rowgo);

            /*Set Parent if have parent*/
            if(parentgroup)
            {
                Rowgo.transform.SetParent(parentgroup.transform);
            }

            /*Set local pos*/
            Rowgo.transform.localPosition = inSpawnLocalPos;

            /*Determine anchor direction, for spawning*/
            //NOTE:Left = +1,Right = -1;
            int direction = 0;
            if(indexOrigin == OriginAnchor.LEFT)
            {
                direction = 1;
            }
            else if(indexOrigin == OriginAnchor.RIGHT)
            {
                direction = -1;
            }
            else
            {
                Debug.LogError("Wrong indexorigin");
            }

            /*Spawn the tiles for each col*/
            for (int colCounter = 0 ; colCounter < rowdata.Length;colCounter++)
            {
                /*Use the char rowdata to find tne tilepiece to use*/
                Tile tilepiece;
                TilePieces.TryGetValue(rowdata[colCounter],out tilepiece);

                if (tilepiece)
                {
                    SpawnTile(name +" Col: " + colCounter,tilepiece,Rowgo,new Vector3(Tilesize.x * colCounter * direction,0,0));
                }
            }
            return Rowgo;
        }

        /// <summary>
        /// Function to Spawn(instantiate) a single tile and returns the new gameobject
        /// </summary>
        public GameObject SpawnTile(string name,Tile tile,GameObject parentgroup,Vector3 inSpawnLocalPos)
        {
            /*Check to see if tile is not null*/
            if (tile)
            {
                /*Spawn the gameobject and set position*/
                GameObject tgo = Instantiate(tile.gameObject);
                tgo.name = name;
                Tiles.Add(tgo);

                /*Set Parent if have parent*/
                if(parentgroup)
                {
                    tgo.transform.SetParent(parentgroup.transform);
                }

                /*Set local pos*/
                tgo.transform.localPosition = inSpawnLocalPos;

                return tgo;
            }
            Debug.LogWarning("cannot spawn tile because tile is missing");
            return null;
        }
    }
}