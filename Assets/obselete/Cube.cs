﻿using UnityEngine;

public class Cube
{
    public float centerX;
    public float centerY;
    public float centerZ;
    public float width;
    public float height;
    public float depth;

    public Cube(float x, float y, float z, float w, float h, float d)
    {
        centerX = x;
        centerY = y;
        centerZ = z;
        width = w;
        height = h;
        depth = d;
    }

    public bool Contains(Vector3 point)
    {
        bool contains = (point.x > centerX - width && point.x < centerX + width && point.y > centerY - height && point.y < centerY + height && point.z > centerZ - depth && point.z < centerZ + depth);
        return contains;
    }
}