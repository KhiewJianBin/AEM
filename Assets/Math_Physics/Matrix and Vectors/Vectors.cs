using UnityEngine;

public class Vectors 
{
	/// <summary>
	/// Creates a quaternion containing the rotation from the input matrix.
	/// </summary>
	/// <param name="m">Input matrix to convert to quaternion</param>
	/// <returns></returns>
	public static Quaternion QuaternionFromMatrix(Matrix4x4 m)
	{
		// TODO: test and replace with this simpler, more unity-friendly code
		//       Quaternion q = Quaternion.LookRotation(m.GetColumn(2),m.GetColumn(1));

		Quaternion q = new Quaternion();
		q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
		q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
		q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
		q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
		q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
		q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
		q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
		return q;
	}

	/// <summary>
	/// Extract the translation and rotation components of a Unity matrix
	/// </summary>
	public static void ToTranslationRotation(Matrix4x4 unityMtx, out Vector3 translation, out Quaternion rotation)
	{
		Vector3 upwards = new Vector3(unityMtx.m01, unityMtx.m11, unityMtx.m21);
		Vector3 forward = new Vector3(unityMtx.m02, unityMtx.m12, unityMtx.m22);
		translation = new Vector3(unityMtx.m03, unityMtx.m13, unityMtx.m23);
		rotation = Quaternion.LookRotation(forward, upwards);
	}

	/// <summary>
	/// Returns the distance between a point and an infinite line defined by two points; linePointA and linePointB
	/// </summary>
	/// <param name="point"></param>
	/// <param name="linePointA"></param>
	/// <param name="linePointB"></param>
	/// <returns></returns>
	public static float DistanceOfPointToLine(Vector3 point, Vector3 linePointA, Vector3 linePointB)
	{
		Vector3 closestPoint = ClosestPointOnLine(linePointA, linePointB, point);
		return (point - closestPoint).magnitude;
	}
	public static Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
	{
		Vector3 vVector1 = vPoint - vA;
		Vector3 vVector2 = (vB - vA).normalized;

		float d = Vector3.Distance(vA, vB);
		float t = Vector3.Dot(vVector2, vVector1);

		if (t <= 0)
			return vA;

		if (t >= d)
			return vB;

		Vector3 vVector3 = vVector2 * t;
		Vector3 vClosestPoint = vA + vVector3;
		return vClosestPoint;
	}

	public static float DistanceOfPointToLineSegment(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
	{
		Vector3 closestPoint = ClosestPointOnLineSegmentToPoint(point, lineStart, lineEnd);
		return (point - closestPoint).magnitude;
	}

	public static Vector3 ClosestPointOnLineSegmentToPoint(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
	{
		Vector3 v = lineEnd - lineStart;
		Vector3 w = point - lineStart;

		float c1 = Vector3.Dot(w, v);
		if (c1 <= 0)
		{
			return lineStart;
		}

		float c2 = Vector3.Dot(v, v);
		if (c2 <= c1)
		{
			return lineEnd;
		}

		float b = c1 / c2;

		Vector3 pointB = lineStart + (v * b);

		return pointB;
	}

	/// <summary>
	/// find unsigned distance of 3D point to an infinite line
	/// </summary>
	/// <param name="ray">ray that specifies an infinite line</param>
	/// <param name="point">3D point</param>
	/// <returns>unsigned perpendicular distance from point to line</returns>
	public static float DistanceOfPointToLine(Ray ray, Vector3 point)
	{
		return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
	}

	/// <summary>
	/// Find 3D point that minimizes distance to 2 lines, midpoint of the shortest perpendicular line segment between them
	/// </summary>
	/// <param name="p">ray that specifies a line</param>
	/// <param name="q">ray that specifies a line</param>
	/// <returns>point nearest to the lines</returns>
	public static Vector3 NearestPointToLines(Ray p, Ray q)
	{
		float a = Vector3.Dot(p.direction, p.direction);
		float b = Vector3.Dot(p.direction, q.direction);
		float c = Vector3.Dot(q.direction, q.direction);
		Vector3 w0 = p.origin - q.origin;
		float den = a * c - b * b;
		float epsilon = 0.00001f;
		if (den < epsilon)
		{
			// parallel, so just average origins
			return 0.5f * (p.origin + q.origin);
		}
		float d = Vector3.Dot(p.direction, w0);
		float e = Vector3.Dot(q.direction, w0);
		float sc = (b * e - c * d) / den;
		float tc = (a * e - b * d) / den;
		Vector3 point = 0.5f * (p.origin + sc * p.direction + q.origin + tc * q.direction);
		return point;
	}

	/// <summary>
	/// Rotates a point around a pivot
	/// </summary>
	/// <param name="point">Start position</param>
	/// <param name="pivot">Pivot position</param>
	/// <param name="rotation">Desired rotation around the pivot</param>
	/// <returns>The position after being rotated around the pivot</returns>
	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
	{
		Vector3 dir = point - pivot;
		dir = rotation * dir;
		point = dir + pivot;

		return point;
	}
}
