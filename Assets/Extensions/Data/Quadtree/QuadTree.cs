// based on https://github.com/CodingTrain/QuadTree

using System.Collections.Generic;
using UnityEngine;

public class QuadTree
{
    #region Fields
    //Settings
    Rect boundary;
    int capacity;

    //Triggers
    bool hasdivided;

    //Data structure
    List<Vector2> points;

    //Recursive Class
    QuadTree northEast;
    QuadTree northWest;
    QuadTree southEast;
    QuadTree southWest;
    #endregion

    public QuadTree(Rect in_boundary, int in_capacity)
    {
        boundary = in_boundary;
        capacity = in_capacity;
        hasdivided = false;
        points = new List<Vector2>();
    }

    public bool Insert(Vector2 point)
    {
        //if the point is not within the boundary, then we cant insert this
        if (boundary.Contains(point) == false) return false;

        if (points.Count < capacity)
        {
            points.Add(point);
            return true;
        }
        else
        {
            // if not yet divided, then divide
            if (hasdivided == false)
            {
                Subdivide();
                hasdivided = true;
            }

            // check which child contains this point
            if (northEast.Insert(point) == true)
            {
                return true;
            }
            else if (northWest.Insert(point) == true)
            {
                return true;
            }
            else if (southEast.Insert(point) == true)
            {
                return true;
            }
            else if (southWest.Insert(point) == true)
            {
                return true;
            }
            //Error,not supose to happen
            Debug.LogError("Weird bug has occured");
            return false;
        }
    }

    void Subdivide()
    {
        float x = boundary.x;
        float y = boundary.y;
        float w = boundary.width;
        float h = boundary.height;

        Rect northEastrect = new Rect(x + w / 2, y + h / 2, w / 2, h / 2);
        northEast = new QuadTree(northEastrect, capacity);
        Rect northWestrect = new Rect(x, y + h / 2, w / 2, h / 2);
        northWest = new QuadTree(northWestrect, capacity);
        Rect southEastrect = new Rect(x + w / 2, y, w / 2, h / 2);
        southEast = new QuadTree(southEastrect, capacity);
        Rect southWestrect = new Rect(x, y, w / 2, h / 2);
        southWest = new QuadTree(southWestrect, capacity);
    }

    List<Vector2> Query(Rect range)
    {
        List<Vector2> foundPoints = new List<Vector2>();
        Query(range, foundPoints);
        return foundPoints;
    }
    void Query(Rect range, List<Vector2> foundPoints)
    {
        if (!boundary.Overlaps(range))
        {
            return;
        }
        else
        {
            //look at all the points in the quad tree
            foreach (Vector2 point in foundPoints)
            {
                if (range.Contains(point))
                {
                    foundPoints.Add(point);
                }
            }
            if (hasdivided)
            {
                //check all quadtree childs
                northEast.Query(range, foundPoints);
                northWest.Query(range, foundPoints);
                southEast.Query(range, foundPoints);
                southWest.Query(range, foundPoints);
            }
        }
    }

    //quadTree.Show(tex);
    //tex.Apply(false);
    public void Show(Texture2D tex)
    {
        // draw boundary edges
        for (float x = 0; x < boundary.width; x++)
        {
            tex.SetPixel((int)(boundary.x + x), (int)(boundary.y), Color.green);
            tex.SetPixel((int)(boundary.x + x), (int)(boundary.y + boundary.height-1), Color.red);
        }
        for (float y = 0; y < boundary.height; y++)
        {
            tex.SetPixel((int)(boundary.x), (int)(boundary.y + y), Color.gray);
            tex.SetPixel((int)(boundary.x + boundary.width - 1), (int)(boundary.y + y), Color.blue);
        }

        // recursively show children
        if (hasdivided == true)
        {
            northEast.Show(tex);
            northWest.Show(tex);
            southEast.Show(tex);
            southWest.Show(tex);
        }

        // draw actual points
        for (int i = 0, length = points.Count; i < length; i++)
        {
            tex.SetPixel((int)points[i].x, (int)points[i].y, Color.white);
        }
    }
}
