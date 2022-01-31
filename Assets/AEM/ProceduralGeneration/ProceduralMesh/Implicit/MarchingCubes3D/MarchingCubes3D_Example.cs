using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes3D_Example : MonoBehaviour
{
    List<GameObject> meshes = new List<GameObject>();

    public Material MeshMaterial;

    #region define the implicit function here
    double samplingfunction(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        //using >= 0
        return (.5 * .5 - x * x - y * y - z * z);
    }
    #endregion

    void Start()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        MarchingCubes3D marchingCube = new MarchingCubes3D();
        marchingCube.implicitFunction = samplingfunction;

        marchingCube.Reset();

        Vector3 MarchingBoundingBoxSize = new Vector3(1, 1, 1);
        Vector3 MarchingBoundingBoxCenter = new Vector3(0, 0, 0);
        Vector3 BoundingBoxResolution = new Vector3(50, 50, 50);
        marchingCube.MarchChunk(MarchingBoundingBoxCenter, MarchingBoundingBoxSize, BoundingBoxResolution, false);

        IList<Vector3> verts = marchingCube.GetVertices();

        //A mesh in unity can only be made up of 65000 verts.
        //Need to split the verts between multiple meshes.

        int maxVertsPerMesh = 3000; //must be divisible by 3, ie 3 verts == 1 triangle - set this value higher max 65000
        int numMeshes = verts.Count / maxVertsPerMesh + 1;

        for (int i = 0; i < numMeshes; i++)
        {
            List<Vector3> splitVerts = new List<Vector3>();
            List<int> splitIndices = new List<int>();

            for (int j = 0; j < maxVertsPerMesh; j++)
            {
                int idx = i * maxVertsPerMesh + j;

                if (idx < verts.Count)
                {
                    splitVerts.Add(verts[idx]);
                    splitIndices.Add(j);
                }
            }

            if (splitVerts.Count == 0) continue;

            Mesh mesh = new Mesh();
            mesh.SetVertices(splitVerts);
            mesh.SetTriangles(splitIndices, 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            GameObject go = new GameObject("Mesh");
            go.transform.parent = transform;
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            go.GetComponent<Renderer>().material = MeshMaterial;
            go.GetComponent<MeshFilter>().mesh = mesh;

            meshes.Add(go);
        }
        
        sw.Stop();
        Debug.LogFormat("Generation took {0} seconds", sw.Elapsed.TotalSeconds);
    }
}
