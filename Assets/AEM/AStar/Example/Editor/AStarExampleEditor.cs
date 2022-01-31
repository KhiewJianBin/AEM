using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AStarExample))]
[CanEditMultipleObjects]
public class AStarExampleEditor : AEMEditor
{
    /*Decalre SerializedVarible*/
    //SerializedProperty a;

    protected override void OnEnable()
    {
        base.OnEnable();

        //ExcludeProperty.Add("startgo");
        //a = serializedObject.FindProperty("a");
    }

    protected override void BeforeDefaultInspector()
    {

    }

    protected override void AfterDefaultInspector()
    {
        AStarExample t = target as AStarExample;

        if (GUILayout.Button("GenerateNewMaze"))
        {
            //t.CreateMaze((int)t.MazeSize.x, (int)t.MazeSize.y);
            //t.CalculatePath(t.ReadGridmap("Assets/AStarTest/Maze.txt"));
            //t.Tilemanager.Reset();
        }
    }
    void OnSceneGUI()
    {
        AStarExample t = target as AStarExample;

        if (t.Astarpath != null)
        {
            int i = 1;
            for (i = 1; i < t.Astarpath.Count; i++)
            {
                Debug.DrawLine(t.Astarpath[i - 1], t.Astarpath[i], Color.red);
            }
        }
    }
}

