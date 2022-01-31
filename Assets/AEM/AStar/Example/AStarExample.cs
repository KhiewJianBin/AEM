using System.Collections.Generic;
using System.IO;
using AEM.Generation.Maze;
using AEM.Managers;
using UnityEngine;

public class AStarExample : GameManager
{
    /// <summary>
    /// Singleton instance for other scripts to access
    /// </summary>
    public static AStarExample Instance;
    public Transform Startgo;
    public Transform endgo;
    public List<Vector2> Astarpath;

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

        if(AStarTileManager.Instance)
        {
            CalculatePath(ReadGridmap(AStarTileManager.Instance.textfile1));
        }
    }

    void Update()
    {

    }
    public void CalculatePath(bool[][] Mapdata)
    {
        List<Vector2> GridDirections = new List<Vector2>();
        GridDirections.Add(Vector2.up);
        GridDirections.Add(Vector2.down);
        GridDirections.Add(Vector2.left);
        GridDirections.Add(Vector2.right);
        //GridDirections.Add(new Vector2(1,1));
        //GridDirections.Add(new Vector2(-1, -1));
        //GridDirections.Add(new Vector2(-1, 1));
        //GridDirections.Add(new Vector2(1, -1));

        AStar astar = new AStar(GridDirections);
        Astarpath = astar.FindClosestPath(Mapdata,
            new Vector2(Mathf.Round(Startgo.position.x), Mathf.Round(Startgo.position.y)),
            new Vector2(Mathf.Round(endgo.position.x), Mathf.Round(endgo.position.y))
            );
        OptimisePath(Astarpath);
    }
    void OptimisePath(List<Vector2> Astarpath)
    {
        Vector2 lastdiff = Vector2.zero;
        Vector2 currenntdiff = Vector2.zero;

        if (Astarpath == null) return;
        if (Astarpath.Count > 1)
        {
            for (int i = 1; i < Astarpath.Count; i++)
            {
                currenntdiff = Astarpath[i] - Astarpath[i - 1];
                if (currenntdiff == lastdiff)
                {
                    Astarpath.RemoveAt(i - 1); i--;
                }
                else
                {
                    lastdiff = currenntdiff;
                }
            }
        }
    }

    public char[][] CreateMaze(int width,int height)
    {
        Maze maze = new Maze();
        maze.CreateMaze(width, height);
        maze.SaveMaze("Assets/AStarTest/Maze.txt");
        //todo
        //AssetDatabase.ImportAsset("Assets/AStarTest/Maze.txt");
        return maze.getMaze();
    }
    public bool[][] ReadGridmap(char[][] gridmap)
    {
        bool[][] Griddata = new bool[gridmap.Length][];
        for (int row = gridmap.Length - 1; row >= 0; row--)
        {
            Griddata[gridmap.Length - 1 - row] = new bool[gridmap[0].Length];
            for (int col = 0; col < gridmap[0].Length; col++)
            {
                /*If Char = # then its false (inpassable)*/
                if (gridmap[row][col] == '#')
                {
                    Griddata[gridmap.Length - 1 - row][col] = false;
                }
                else
                {
                    Griddata[gridmap.Length - 1 - row][col] = true;
                }
            }
        }
        return Griddata;
    }
    //NOTE WARNING, Textasset will not reversed
    public bool[][] ReadGridmap(TextAsset gridmap)
    {
        if (gridmap)
        {
            int rows = 0;
            int cols = 0;

            /*Get the number of rows , cols in GridMap*/
            string[] TileData = gridmap.text.Split('\n');
            //Array.Reverse(TileData);
            rows = TileData.Length;
            foreach (string s in TileData)
            {
                string cleanS = s.Replace("\r", "");
                if (cleanS.Length > cols)
                    cols = cleanS.Length;
            }

            /*Fill in GridData Base on Map*/
            bool[][] Griddata = new bool[rows][];
            for (int i = 0; i < rows; i++)
            {
                Griddata[i] = new bool[cols];
                for (int j = 0; j < cols; j++)
                {
                    /*If Char = # then its false (inpassable)*/
                    if (TileData[i][j] == '#')
                    {
                        Griddata[i][j] = false;
                    }
                    else
                    {
                        Griddata[i][j] = true;
                    }
                }
            }
            return Griddata;
        }
        else return null;
    }
    //NOTE WARNING, Textasset will not reversed
    public bool[][] ReadGridmap(string path)
    {
        string[] TileData = File.ReadAllLines(path); 
        if (TileData.Length>0)
        {
            int rows = 0;
            int cols = 0;

            //Array.Reverse(TileData);
            rows = TileData.Length;
            foreach (string s in TileData)
            {
                string cleanS = s.Replace("\r", "");
                if (cleanS.Length > cols)
                    cols = cleanS.Length;
            }

            /*Fill in GridData Base on Map*/
            bool[][] Griddata = new bool[rows][];
            for (int i = 0; i < rows; i++)
            {
                Griddata[i] = new bool[cols];
                for (int j = 0; j < cols; j++)
                {
                    /*If Char = # then its false (inpassable)*/
                    if (TileData[i][j] == '#')
                    {
                        Griddata[i][j] = false;
                    }
                    else
                    {
                        Griddata[i][j] = true;
                    }
                }
            }
            return Griddata;
        }
        else return null;
    }
}
