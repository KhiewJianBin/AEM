//https://github.com/Scrawk/Proland-To-Unity/tree/master/Assets

using UnityEngine;
using System;

public class MathUtility_CS_DOTNET
{
	public static double Safe_Acos(double r)
	{
		return Math.Acos(Math.Min(1.0,Math.Max(-1.0,r)));	
	}

	public static double Safe_Asin(double r)
	{
		return Math.Asin(Math.Min(1.0,Math.Max(-1.0,r)));	
	}

	public static float Safe_Acos(float r)
	{
		return Mathf.Acos(Mathf.Min(1.0f,Mathf.Max(-1.0f,r)));	
	}
	
	public static float Safe_Asin(float r)
	{
		return Mathf.Asin(Mathf.Min(1.0f,Mathf.Max(-1.0f,r)));	
	}
	
	public static bool IsFinite(float f)
	{
		return !(float.IsInfinity(f) || float.IsNaN(f));		
	}

	public static bool IsFinite(double f)
	{
		return !(double.IsInfinity(f) || double.IsNaN(f));		
	}

    









    public static double HorizontalFovToVerticalFov(double hfov, double screenWidth, double screenHeight)
	{
		return 2.0 * Math.Atan( Math.Tan(hfov*0.5* Mathf.Deg2Rad) * screenHeight/screenWidth ) * Mathf.Rad2Deg;
	}

	public static double VerticalFovToHorizontalFov(double vfov, double screenWidth, double screenHeight)
	{
		return 2.0 * Math.Atan( Math.Tan(vfov*0.5* Mathf.Deg2Rad) * screenWidth/screenHeight ) * Mathf.Rad2Deg;
	}
}
public static class MathExtension_CS_DOTNET
{
    public static float remap(this float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    public static double remap(this double value, double start1, double stop1, double start2, double stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }
}





















