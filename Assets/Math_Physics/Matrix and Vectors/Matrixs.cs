using UnityEngine;
public class Matrixs 
{
	/// <summary>
	/// Element-wise addition of two Matrix4x4s - extension method
	/// </summary>
	/// <param name="a">matrix</param>
	/// <param name="b">matrix</param>
	/// <returns>element-wise (a+b)</returns>
	public static Matrix4x4 Add(Matrix4x4 a, Matrix4x4 b)
	{
		Matrix4x4 result = new Matrix4x4();
		result.SetColumn(0, a.GetColumn(0) + b.GetColumn(0));
		result.SetColumn(1, a.GetColumn(1) + b.GetColumn(1));
		result.SetColumn(2, a.GetColumn(2) + b.GetColumn(2));
		result.SetColumn(3, a.GetColumn(3) + b.GetColumn(3));
		return result;
	}
	/// <summary>
	/// Element-wise subtraction of two Matrix4x4s - extension method
	/// </summary>
	/// <param name="a">matrix</param>
	/// <param name="b">matrix</param>
	/// <returns>element-wise (a-b)</returns>
	public static Matrix4x4 Subtract(Matrix4x4 a, Matrix4x4 b)
	{
		Matrix4x4 result = new Matrix4x4();
		result.SetColumn(0, a.GetColumn(0) - b.GetColumn(0));
		result.SetColumn(1, a.GetColumn(1) - b.GetColumn(1));
		result.SetColumn(2, a.GetColumn(2) - b.GetColumn(2));
		result.SetColumn(3, a.GetColumn(3) - b.GetColumn(3));
		return result;
	}
}
