using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/* Depth-first search maze generation algorithm.
Start at a random cell.
Mark the current cell as visited, and get a list of its neighbors. 
For each neighbor, starting with a randomly selected neighbor:
If that neighbor hasn't been visited, 
remove the wall between this cell and that neighbor, 
and then repeat with that neighbor as the current cell.
*/

namespace AEM.Generation.Maze
{
    [System.Flags]
    public enum CellState
    {
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8,
        Visited = 128,
        Initial = Top | Right | Bottom | Left,
    }

    public static class Extensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            T[] s = source.ToArray();
            for (var i = s.Length - 1; i >= 0; i--)
            {
                int swapIndex = Random.Range(0, i + 1);
                yield return s[swapIndex];
                s[swapIndex] = s[i];
            }
        }

        public static CellState OppositeWall(this CellState orig)
        {
            return (CellState) (((int) orig >> 2) | ((int) orig << 2)) & CellState.Initial;
        }

        public static bool HasFlag(this CellState cs, CellState flag)
        {
            return ((int) cs & (int) flag) != 0;
        }
    }

    public class Maze
    {
        CellState[][] cells;

        struct RemoveWallAction
        {
            public Vector2 Neighbour;
            public CellState Wall;
        }

        int Mazeheight;
        int Mazewidth;

        public void CreateMaze(int rows, int cols)
        {
            Mazeheight = rows;
            Mazewidth = cols;
            cells = new CellState[rows][];

            for (int i = 0; i < rows; i++)
            {
                cells[i] = new CellState[cols];
                for (int j = 0; j < cols; j++)
                {
                    cells[i][j] = CellState.Initial;
                }
            }

            VisitCell(new Vector2(Random.Range(0, rows), Random.Range(0, cols)));
        }

        void VisitCell(Vector2 cell)
        {
            cells[(int) cell.x][(int) cell.y] |= CellState.Visited; //Mark Cell as visited
            foreach (
                var p in
                GetNeighbours(new Vector2(cell.x, cell.y))
                    .Shuffle()
                    .Where(c => !(cells[(int) c.Neighbour.x][(int) c.Neighbour.y].HasFlag(CellState.Visited))))
            {
                cells[(int) cell.x][(int) cell.y] -= p.Wall;
                cells[(int) p.Neighbour.x][(int) p.Neighbour.y] -= p.Wall.OppositeWall();
                VisitCell(new Vector2(p.Neighbour.x, p.Neighbour.y));
            }
        }

        /// <summary>
        /// returns list of possible neighbours indicating which side it is
        /// </summary>
        IEnumerable<RemoveWallAction> GetNeighbours(Vector2 cell)
        {
            if (cell.y > 0)
                yield return new RemoveWallAction {Neighbour = new Vector2(cell.x, cell.y - 1), Wall = CellState.Left};
            if (cell.y < Mazewidth - 1)
                yield return new RemoveWallAction {Neighbour = new Vector2(cell.x, cell.y + 1), Wall = CellState.Right};
            if (cell.x > 0)
                yield return new RemoveWallAction {Neighbour = new Vector2(cell.x - 1, cell.y), Wall = CellState.Top};
            if (cell.x < Mazeheight - 1)
                yield return new RemoveWallAction {Neighbour = new Vector2(cell.x + 1, cell.y), Wall = CellState.Bottom}
                    ;
        }

        public void SaveMaze(string pathname)
        {
            string firstLine = string.Empty;
            StreamWriter sw = File.CreateText(pathname);

            for (int i = 0; i < Mazeheight; i++)
            {
                StringBuilder sbTop = new StringBuilder();
                StringBuilder sbMid = new StringBuilder();
                for (int j = 0; j < Mazewidth; j++)
                {
                    sbTop.Append(cells[i][j].HasFlag(CellState.Top) ? "##" : "#-");
                    sbMid.Append(cells[i][j].HasFlag(CellState.Left) ? "#-" : "--");
                }
                if (firstLine == string.Empty)
                    firstLine = sbTop.ToString();

                sw.WriteLine(sbTop.ToString() + "#");
                sw.WriteLine(sbMid.ToString() + "#");
            }
            sw.Write(firstLine.ToString() + "#");
            sw.Close();
        }

        public char[][] getMaze()
        {
            char[][] Mazedata = new char[Mazeheight * 2 + 1][];
            for (int i = 0; i < Mazeheight; i++)
            {
                StringBuilder sbTop = new StringBuilder();
                StringBuilder sbMid = new StringBuilder();
                for (int j = 0; j < Mazewidth; j++)
                {
                    sbTop.Append(cells[i][j].HasFlag(CellState.Top) ? "##" : "#-");
                    sbMid.Append(cells[i][j].HasFlag(CellState.Left) ? "#-" : "--");
                }
                sbTop.Append("#");
                sbMid.Append("#");
                Mazedata[2 * i] = sbTop.ToString().ToCharArray();
                Mazedata[1 + 2 * i] = sbMid.ToString().ToCharArray();
                if (i == 0)
                {
                    Mazedata[2 * Mazeheight] = sbTop.ToString().ToCharArray();
                }
            }
            return Mazedata;
        }
    }
}