// unity octree https://github.com/unitycoder/Octree

using UnityEngine;

public class OctreeExample : MonoBehaviour
{
    Octree octTree;
    Bounds boundary;

    void Start()
    {
        var areaCenter = new Vector3(0, 0, 0);
        float width = 10;
        float height = 10;
        float depth = 10;
        int capacity = 1;

        boundary = new Bounds(areaCenter, new Vector3(width, height, depth));
        octTree = new Octree(boundary, capacity);
    }

    void Update()
    {
        // show current octree
        octTree.DrawDebug();

        // press mouse to insert random point
        if (Input.GetMouseButtonDown(0))
        {
            Bounds b = octTree.boundary;
            //random point withint the bound
            Vector3 pos = new Vector3(Random.Range(b.center.x - b.extents.x, b.center.x + b.extents.x), Random.Range(b.center.y - b.extents.y, b.center.y + b.extents.y), Random.Range(b.center.z - b.extents.z, b.center.z + b.extents.z));
            octTree.Insert(new Vector3(pos.x, pos.y, pos.z));
        }
    }

}
