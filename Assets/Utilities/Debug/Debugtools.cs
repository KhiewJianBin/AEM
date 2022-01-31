using System.Collections.Generic;
using UnityEngine;

public static class AEMDebug
{
    public delegate void Function(params object[] args);
    
    public static float getExcecuteTime(Function f,params object[] args)
    {
        float t = Time.timeSinceLevelLoad;
        f(args);
        return Time.timeSinceLevelLoad - t;
    }
    public static void DrawPath(List<Vector2> path)//TODO ADD drow Vector 3 path
    {
        if (path != null)
        {
            int i = 1;
            for (i = 1; i < path.Count; i++)
            {
                Debug.DrawLine(path[i - 1], path[i], Color.red);
            }
        }
    }
    public static void GLDrawLine()
    {
        //GL.
    }
    //public static void TestTime<T, TR>(Func<T, TR> action, T obj,
    //                               int iterations)
    //{
    //    Stopwatch stopwatch = Stopwatch.StartNew();
    //    for (int i = 0; i < iterations; ++i)
    //        action(obj);
    //    Console.WriteLine(action.Method.Id + " took " + stopwatch.Elapsed);
    //}

    //public static void TestTime<T1, T2, TR>(Func<T1, T2, TR> action, T1 obj1,
    //                                        T2 obj2, int iterations)
    //{
    //    Stopwatch stopwatch = Stopwatch.StartNew();
    //    for (int i = 0; i < iterations; ++i)
    //        action(obj1, obj2);
    //    Console.WriteLine(action.Method.Id + " took " + stopwatch.Elapsed);
    //}
}

