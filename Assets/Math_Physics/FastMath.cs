using System.Runtime.InteropServices;
using UnityEngine;
public class FastMath
{
	//used in fast sqrt , but really need or not?
	// for // Evil floating point bit level hacking.?
	[StructLayout(LayoutKind.Explicit)]
	private struct FloatIntUnion
	{
		[FieldOffset(0)]
		public float f;

		[FieldOffset(0)]
		public int tmp;
	}

	/// <summary>
	/// Low accuracy sqrt method for fast calculation.
	/// </summary>
	public static float FastSqrt(float z)
	{
		if (z == 0) return 0;
		FloatIntUnion u;
		u.tmp = 0;
		u.f = z;
		u.tmp -= 1 << 23; // Subtract 2^m.
		u.tmp >>= 1; // Divide by 2.
		u.tmp += 1 << 29; // Add ((b + 1) / 2) * 2^m.
		return u.f;
	}
	/// <summary>
	/// The Infamous Unusual Fast Inverse Square Root (TM).
	/// </summary>
	public static float InvSqrt(float z)
	{
		if (z == 0) return 0;
		FloatIntUnion u;
		u.tmp = 0;
		float xhalf = 0.5f * z;
		u.f = z;
		u.tmp = 0x5f375a86 - (u.tmp >> 1);
		u.f = u.f * (1.5f - xhalf * u.f * u.f);
		return u.f * z;
	}
	/// <summary>
	/// Returns the distance between a and b (fast but very low accuracy).
	/// </summary>
	public static float FastDistance(Vector3 a, Vector3 b)
	{
		Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		return FastSqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
	}
}