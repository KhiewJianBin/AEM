using System;

using UnityEngine;

/// <summary>
/// Just a Unity Gizmo Wrapper - to include color
/// </summary>
public static class GizmoExtension 
{
    #region Simple Gizmo Wrapper to have colored Gizmo
    public static void DrawCube(Vector3 center, Vector3 size, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawCube(center, size);

        Gizmos.color = currentColor;
    }
    public static void DrawSphere(Vector3 pos, float radius, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawSphere(pos, radius);

        Gizmos.color = currentColor;
    }
    public static void DrawWireCube(Vector3 center, Vector3 size, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawWireCube(center, size);

        Gizmos.color = currentColor;
    }
    public static void DrawWireSphere(Vector3 pos, float radius, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawWireSphere(pos, radius);

        Gizmos.color = currentColor;
    }
    public static void DrawLine(Vector3 from, Vector3 to, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawLine(from, to);

        Gizmos.color = currentColor;
    }
    public static void DrawRay(Ray ray, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawRay(ray);

        Gizmos.color = currentColor;
    }
    public static void DrawRay(Vector3 from, Vector3 direction, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawRay(from, direction);

        Gizmos.color = currentColor;
    }
    public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawFrustum(center, fov, maxRange, minRange, aspect);

        Gizmos.color = currentColor;
    }
    public static void DrawIcon(Vector3 center, string name, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawIcon(center, name);

        Gizmos.color = currentColor;
    }

    public static void DrawIcon(Vector3 center, string name, bool allowScaling, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawIcon(center, name, allowScaling);

        Gizmos.color = currentColor;
    }

    public static void DrawGUITexture(Rect screenRect, Texture texture, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawGUITexture(screenRect, texture);

        Gizmos.color = currentColor;
    }

    public static void DrawGUITexture(Rect screenRect, Texture texture, Material mat, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawGUITexture(screenRect, texture, mat);

        Gizmos.color = currentColor;
    }

    public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder);

        Gizmos.color = currentColor;
    }

    public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);

        Gizmos.color = currentColor;
    }
    #endregion

    #region Advanced Gizmo Wrapper
    public static void DrawPoint(Vector3 position,float scale, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawRay(position + (Vector3.up * 0.5f * scale), -Vector3.up * scale);
        Gizmos.DrawRay(position + (Vector3.right * 0.5f * scale), -Vector3.right * scale);
        Gizmos.DrawRay(position + (Vector3.forward * 0.5f) * scale, -Vector3.forward * scale);

        Gizmos.color = currentColor;
    }
    public static void DrawBounds(Bounds bounds, Color color)
    {
        Vector3 center = bounds.center;

        float x = bounds.extents.x;
        float y = bounds.extents.y;
        float z = bounds.extents.z;

        Vector3 ruf = center + new Vector3(x, y, z);
        Vector3 rub = center + new Vector3(x, y, -z);
        Vector3 luf = center + new Vector3(-x, y, z);
        Vector3 lub = center + new Vector3(-x, y, -z);

        Vector3 rdf = center + new Vector3(x, -y, z);
        Vector3 rdb = center + new Vector3(x, -y, -z);
        Vector3 lfd = center + new Vector3(-x, -y, z);
        Vector3 lbd = center + new Vector3(-x, -y, -z);

        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawLine(ruf, luf);
        Gizmos.DrawLine(ruf, rub);
        Gizmos.DrawLine(luf, lub);
        Gizmos.DrawLine(rub, lub);

        Gizmos.DrawLine(ruf, rdf);
        Gizmos.DrawLine(rub, rdb);
        Gizmos.DrawLine(luf, lfd);
        Gizmos.DrawLine(lub, lbd);

        Gizmos.DrawLine(rdf, lfd);
        Gizmos.DrawLine(rdf, rdb);
        Gizmos.DrawLine(lfd, lbd);
        Gizmos.DrawLine(lbd, rdb);

        Gizmos.color = currentColor;
    }
    public static void DrawLocalCube(Transform transform,Vector3 localPos, Vector3 size, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Vector3 lbb = transform.TransformPoint(localPos + ((-size) * 0.5f));
        Vector3 rbb = transform.TransformPoint(localPos + (new Vector3(size.x, -size.y, -size.z) * 0.5f));

        Vector3 lbf = transform.TransformPoint(localPos + (new Vector3(size.x, -size.y, size.z) * 0.5f));
        Vector3 rbf = transform.TransformPoint(localPos + (new Vector3(-size.x, -size.y, size.z) * 0.5f));

        Vector3 lub = transform.TransformPoint(localPos + (new Vector3(-size.x, size.y, -size.z) * 0.5f));
        Vector3 rub = transform.TransformPoint(localPos + (new Vector3(size.x, size.y, -size.z) * 0.5f));

        Vector3 luf = transform.TransformPoint(localPos + ((size) * 0.5f));
        Vector3 ruf = transform.TransformPoint(localPos + (new Vector3(-size.x, size.y, size.z) * 0.5f));

        Gizmos.DrawLine(lbb, rbb);
        Gizmos.DrawLine(rbb, lbf);
        Gizmos.DrawLine(lbf, rbf);
        Gizmos.DrawLine(rbf, lbb);

        Gizmos.DrawLine(lub, rub);
        Gizmos.DrawLine(rub, luf);
        Gizmos.DrawLine(luf, ruf);
        Gizmos.DrawLine(ruf, lub);

        Gizmos.DrawLine(lbb, lub);
        Gizmos.DrawLine(rbb, rub);
        Gizmos.DrawLine(lbf, luf);
        Gizmos.DrawLine(rbf, ruf);

        Gizmos.color = currentColor;
    }
    public static void DrawLocalCube(Matrix4x4 space, Vector3 localPos, Vector3 size, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Vector3 lbb = space.MultiplyPoint3x4(localPos + ((-size) * 0.5f));
        Vector3 rbb = space.MultiplyPoint3x4(localPos + (new Vector3(size.x, -size.y, -size.z) * 0.5f));

        Vector3 lbf = space.MultiplyPoint3x4(localPos + (new Vector3(size.x, -size.y, size.z) * 0.5f));
        Vector3 rbf = space.MultiplyPoint3x4(localPos + (new Vector3(-size.x, -size.y, size.z) * 0.5f));

        Vector3 lub = space.MultiplyPoint3x4(localPos + (new Vector3(-size.x, size.y, -size.z) * 0.5f));
        Vector3 rub = space.MultiplyPoint3x4(localPos + (new Vector3(size.x, size.y, -size.z) * 0.5f));

        Vector3 luf = space.MultiplyPoint3x4(localPos + ((size) * 0.5f));
        Vector3 ruf = space.MultiplyPoint3x4(localPos + (new Vector3(-size.x, size.y, size.z) * 0.5f));

        Gizmos.DrawLine(lbb, rbb);
        Gizmos.DrawLine(rbb, lbf);
        Gizmos.DrawLine(lbf, rbf);
        Gizmos.DrawLine(rbf, lbb);

        Gizmos.DrawLine(lub, rub);
        Gizmos.DrawLine(rub, luf);
        Gizmos.DrawLine(luf, ruf);
        Gizmos.DrawLine(ruf, lub);

        Gizmos.DrawLine(lbb, lub);
        Gizmos.DrawLine(rbb, rub);
        Gizmos.DrawLine(lbf, luf);
        Gizmos.DrawLine(rbf, ruf);

        Gizmos.color = currentColor;
    }
    public static void DrawCircle(Vector3 position, Vector3 up, float radius, Color color)
    {
        up = ((up == Vector3.zero) ? Vector3.up : up).normalized * radius;
        Vector3 _forward = Vector3.Slerp(up, -up, 0.5f);
        Vector3 _right = Vector3.Cross(up, _forward).normalized * radius;

        Matrix4x4 matrix = new Matrix4x4();

        matrix[0] = _right.x;
        matrix[1] = _right.y;
        matrix[2] = _right.z;

        matrix[4] = up.x;
        matrix[5] = up.y;
        matrix[6] = up.z;

        matrix[8] = _forward.x;
        matrix[9] = _forward.y;
        matrix[10] = _forward.z;

        Vector3 _lastPoint = position + matrix.MultiplyPoint3x4(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)));
        Vector3 _nextPoint = Vector3.zero;

        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        for (var i = 0; i < 91; i++)
        {
            _nextPoint.x = Mathf.Cos((i * 4) * Mathf.Deg2Rad);
            _nextPoint.z = Mathf.Sin((i * 4) * Mathf.Deg2Rad);
            _nextPoint.y = 0;

            _nextPoint = position + matrix.MultiplyPoint3x4(_nextPoint);

            Gizmos.DrawLine(_lastPoint, _nextPoint);
            _lastPoint = _nextPoint;
        }

        Gizmos.color = currentColor;
    }

    public static void DrawCircle(Vector3 position, float radius, Color color)
    {
        DrawCircle(position, Vector3.up, radius, color);
    }

    public static void DrawCylinder(Vector3 start, Vector3 end, Color color, float radius = 1.0f)
    {
        Vector3 up = (end - start).normalized * radius;
        Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
        Vector3 right = Vector3.Cross(up, forward).normalized * radius;

        //Radial circles
        DrawCircle(start, up, radius,color);
        DrawCircle(end, -up, radius,color);
        DrawCircle((start + end) * 0.5f, up, radius, color);

        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        //Side lines
        Gizmos.DrawLine(start + right, end + right);
        Gizmos.DrawLine(start - right, end - right);

        Gizmos.DrawLine(start + forward, end + forward);
        Gizmos.DrawLine(start - forward, end - forward);

        //Start endcap
        Gizmos.DrawLine(start - right, start + right);
        Gizmos.DrawLine(start - forward, start + forward);

        //End endcap
        Gizmos.DrawLine(end - right, end + right);
        Gizmos.DrawLine(end - forward, end + forward);

        Gizmos.color = currentColor;
    }
    public static void DrawCylinder(Vector3 start, Vector3 end, float radius = 1.0f)
    {
        DrawCylinder(start, end, Color.white, radius);
    }
    public static void DrawCone(Vector3 position, Vector3 direction, Color color, float angle = 45)
    {
        float length = direction.magnitude;

        Vector3 _forward = direction;
        Vector3 _up = Vector3.Slerp(_forward, -_forward, 0.5f);
        Vector3 _right = Vector3.Cross(_forward, _up).normalized * length;

        direction = direction.normalized;

        Vector3 slerpedVector = Vector3.Slerp(_forward, _up, angle / 90.0f);

        float dist;
        var farPlane = new Plane(-direction, position + _forward);
        var distRay = new Ray(position, slerpedVector);

        farPlane.Raycast(distRay, out dist);

        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawRay(position, slerpedVector.normalized * dist);
        Gizmos.DrawRay(position, Vector3.Slerp(_forward, -_up, angle / 90.0f).normalized * dist);
        Gizmos.DrawRay(position, Vector3.Slerp(_forward, _right, angle / 90.0f).normalized * dist);
        Gizmos.DrawRay(position, Vector3.Slerp(_forward, -_right, angle / 90.0f).normalized * dist);

        DrawCircle(position + _forward, direction, (_forward - (slerpedVector.normalized * dist)).magnitude, color);
        DrawCircle(position + (_forward * 0.5f), direction, ((_forward * 0.5f) - (slerpedVector.normalized * (dist * 0.5f))).magnitude, color);

        Gizmos.color = currentColor;
    }
    public static void DrawArrow(Vector3 position, Vector3 direction, Color color)
    {
        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        Gizmos.DrawRay(position, direction);
        DrawCone(position + direction, -direction * 0.333f, color, 15);

        Gizmos.color = currentColor;
    }
    public static void DrawCapsule(Vector3 start, Vector3 end, Color color, float radius = 1)
    {
        Vector3 up = (end - start).normalized * radius;
        Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
        Vector3 right = Vector3.Cross(up, forward).normalized * radius;

        Color currentColor = Gizmos.color;
        Gizmos.color = color;

        float height = (start - end).magnitude;
        float sideLength = Mathf.Max(0, (height * 0.5f) - radius);
        Vector3 middle = (end + start) * 0.5f;

        start = middle + ((start - middle).normalized * sideLength);
        end = middle + ((end - middle).normalized * sideLength);

        //Radial circles
        DrawCircle(start, up, radius, color);
        DrawCircle(end, -up, radius, color);

        //Side lines
        Gizmos.DrawLine(start + right, end + right);
        Gizmos.DrawLine(start - right, end - right);

        Gizmos.DrawLine(start + forward, end + forward);
        Gizmos.DrawLine(start - forward, end - forward);

        for (int i = 1; i < 26; i++)
        {

            //Start endcap
            Gizmos.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + start, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + start);
            Gizmos.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + start, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + start);
            Gizmos.DrawLine(Vector3.Slerp(forward, -up, i / 25.0f) + start, Vector3.Slerp(forward, -up, (i - 1) / 25.0f) + start);
            Gizmos.DrawLine(Vector3.Slerp(-forward, -up, i / 25.0f) + start, Vector3.Slerp(-forward, -up, (i - 1) / 25.0f) + start);

            //End endcap
            Gizmos.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + end, Vector3.Slerp(right, up, (i - 1) / 25.0f) + end);
            Gizmos.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + end, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + end);
            Gizmos.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + end, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + end);
            Gizmos.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + end, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + end);
        }

        Gizmos.color = currentColor;
    }
    #endregion
}
