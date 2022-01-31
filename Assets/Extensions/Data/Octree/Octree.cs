// based on https://github.com/CodingTrain/QuadTree

using System.Collections.Generic;
using UnityEngine;

//todo add query function

public class Octree
{
    #region Fields
    //Settings
    public Bounds boundary;
    int capacity;

    //Triggers
    bool hasdivided;

    //Data structure
    List<Vector3> points;

    //Recursive Class
    Octree northEastBack;
    Octree northWestBack;
    Octree southEastBack;
    Octree southWestBack;
    Octree northEastFront;
    Octree northWestFront;
    Octree southEastFront;
    Octree southWestFront;
    #endregion

    public Octree(Bounds in_boundary, int in_capacity)
    {
        boundary = in_boundary;
        capacity = in_capacity;
        points = new List<Vector3>();
        hasdivided = false;
    }

    public bool Insert(Vector3 point)
    {
        if (boundary.Contains(point) == false)
        {
            return false;
        }

        if (points.Count < capacity)
        {
            points.Add(point);
            return true;
        }
        else // need to divide
        {
            // if not already split, then divide
            if (hasdivided == false)
            {
                Subdivide();
            }

            // check which child contains this point
            if (northEastBack.Insert(point) == true)
            {
                return true;
            }
            else if (northWestBack.Insert(point) == true)
            {
                return true;
            }
            else if (southEastBack.Insert(point) == true)
            {
                return true;
            }
            else if (southWestBack.Insert(point) == true)
            {
                return true;
            }

            else if (northEastFront.Insert(point) == true)
            {
                return true;
            }
            else if (northWestFront.Insert(point) == true)
            {
                return true;
            }
            else if (southEastFront.Insert(point) == true)
            {
                return true;
            }
            else if (southWestFront.Insert(point) == true)
            {
                return true;
            }
        }
        //Error,not supose to happen
        Debug.LogError("Weird bug has occured");
        return false;
    }

    void Subdivide()
    {
        var x = boundary.center.x;
        var y = boundary.center.y;
        var z = boundary.center.z;
        var w = boundary.extents.x;
        var h = boundary.extents.y;
        var d = boundary.extents.z;

        var neb = new Bounds(new Vector3(x + w / 2, y + h / 2, z + d / 2),new Vector3(w, h, d));
        northEastBack = new Octree(neb, capacity);
        var nwb = new Bounds(new Vector3(x - w / 2, y + h / 2, z + d / 2), new Vector3(w, h, d));
        northWestBack = new Octree(nwb, capacity);
        var seb = new Bounds(new Vector3(x + w / 2, y - h / 2, z + d / 2), new Vector3(w, h, d));
        southEastBack = new Octree(seb, capacity);
        var swb = new Bounds(new Vector3(x - w / 2, y - h / 2, z + d / 2), new Vector3(w, h, d));
        southWestBack = new Octree(swb, capacity);

        var nef = new Bounds(new Vector3(x + w / 2, y + h / 2, z - d / 2), new Vector3(w, h, d));
        northEastFront = new Octree(nef, capacity);
        var nwf = new Bounds(new Vector3(x - w / 2, y + h / 2, z - d / 2), new Vector3(w, h, d));
        northWestFront = new Octree(nwf, capacity);
        var sef = new Bounds(new Vector3(x + w / 2, y - h / 2, z - d / 2), new Vector3(w, h, d));
        southEastFront = new Octree(sef, capacity);
        var swf = new Bounds(new Vector3(x - w / 2, y - h / 2, z - d / 2), new Vector3(w, h, d));
        southWestFront = new Octree(swf, capacity);

        hasdivided = true;
    }









    public void DrawDebug()
    {
        var bottomLeftBack = new Vector3(boundary.center.x - boundary.extents.x, boundary.center.y - boundary.extents.y, boundary.center.z + boundary.extents.z);
        var bottomLeftFront = new Vector3(boundary.center.x - boundary.extents.x, boundary.center.y - boundary.extents.y, boundary.center.z - boundary.extents.z);
        var bottomRigthBack = new Vector3(boundary.center.x + boundary.extents.x, boundary.center.y - boundary.extents.y, boundary.center.z + boundary.extents.z);
        var bottomRigthFront = new Vector3(boundary.center.x + boundary.extents.x, boundary.center.y - boundary.extents.y, boundary.center.z - boundary.extents.z);

        var topLeftBack = new Vector3(boundary.center.x - boundary.extents.x, boundary.center.y + boundary.extents.y, boundary.center.z + boundary.extents.z);
        var topLeftFront = new Vector3(boundary.center.x - boundary.extents.x, boundary.center.y + boundary.extents.y, boundary.center.z - boundary.extents.z);
        var topRigthBack = new Vector3(boundary.center.x + boundary.extents.x, boundary.center.y + boundary.extents.y, boundary.center.z + boundary.extents.z);
        var topRigthFront = new Vector3(boundary.center.x + boundary.extents.x, boundary.center.y + boundary.extents.y, boundary.center.z - boundary.extents.z);

        Debug.DrawLine(bottomLeftBack, bottomLeftFront, Color.red);
        Debug.DrawLine(bottomRigthBack, bottomRigthFront, Color.green);
        Debug.DrawLine(bottomLeftBack, bottomRigthBack, Color.magenta);
        Debug.DrawLine(bottomLeftFront, bottomRigthFront, Color.gray);

        Debug.DrawLine(topLeftBack, topLeftFront, Color.yellow);
        Debug.DrawLine(topRigthBack, topRigthFront, Color.blue);
        Debug.DrawLine(topLeftBack, topRigthBack, Color.cyan);
        Debug.DrawLine(topLeftFront, topRigthFront, Color.white);

        Debug.DrawLine(bottomLeftBack, topLeftBack, Color.red);
        Debug.DrawLine(bottomLeftFront, topLeftFront, Color.green);
        Debug.DrawLine(bottomRigthBack, topRigthBack, Color.magenta);
        Debug.DrawLine(bottomRigthFront, topRigthFront, Color.gray);

        // recursively show children
        if (hasdivided == true)
        {
            northEastBack.DrawDebug();
            northWestBack.DrawDebug();
            southEastBack.DrawDebug();
            southWestBack.DrawDebug();
            northEastFront.DrawDebug();
            northWestFront.DrawDebug();
            southEastFront.DrawDebug();
            southWestFront.DrawDebug();
        }

        // draw actual points
        for (int i = 0, length = points.Count; i < length; i++)
        {
            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), Vector3.up * 0.1f, Color.white);
            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), -Vector3.up * 0.1f, Color.white);

            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), Vector3.forward * 0.1f, Color.white);
            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), -Vector3.forward * 0.1f, Color.white);

            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), Vector3.right * 0.1f, Color.white);
            Debug.DrawRay(new Vector3(points[i].x, points[i].y, points[i].z), -Vector3.right * 0.1f, Color.white);
        }
    }
}
