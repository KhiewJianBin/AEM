using UnityEngine;

/// <summary>
/// Todo:Add Comment to functions
/// TODO: TEST CalculateAngle(), CalculateImpactVelocity() , CalculateVelocity function
/// </summary>
public static class Trajectary
{
    /// <summary>
    /// Calculates Range/Distance of Projectile Using [XVelocity] and [YVelocity]
    /// </summary>
    /// <param name="in_Vx">X Velocity</param>
    /// <param name="in_Vy">Y Velocity</param>
    /// <param name="in_y">Initial Height</param>
    /// <param name="in_g">Gravity</param>
    /// <returns></returns>
    public static float CalculateRange(float in_y, float in_Vx, float in_Vy, float in_g)
    {
        float Tflight = CalculateTflight(in_y, in_Vy, in_g);
        return CalculateRange(in_Vx, Tflight);
    }
    public static float CalculateRange(float in_Vx, float in_Tflight)
    {
        return in_Vx * in_Tflight;
    }

    /// <summary>
    /// Calculate Total FlightTime
    /// </summary>
    /// <param name="in_Vy">X Velocity</param>
    /// <param name="in_y">Initial Height</param>
    /// <param name="in_g">Gravity<param>
    /// <returns></returns>
    public static float CalculateTflight(float in_y, float in_Vy, float in_g)
    {
        float Trise = CalculateTrise(in_Vy, in_g);
        float Tfall = CalculateTfall(in_y, in_Vy, in_g, Trise);

        return CalculateTflight(Trise, Tfall);
    }
    public static float CalculateTflight(float in_Trise, float in_Tfall)
    {
        return in_Trise + in_Tfall;
    }

    /// <summary>
    /// Calculate Time To Reach Max Height
    /// </summary>
    /// <param name="in_Vy">Y Velocity</param>
    /// <param name="in_g">Gravity</param>
    /// <returns></returns>
    public static float CalculateTrise(float in_Vy, float in_g)
    {
        return in_Vy / in_g;
    }

    /// <summary>
    /// Calculate Time To Reach The Ground
    /// </summary>
    /// <param name="in_Vy">Y Velocity</param>
    /// <param name="in_y">Initial Height</param>
    /// <param name="in_g">Gravity</param>
    /// <returns></returns>
    public static float CalculateTfall(float in_y, float in_Vy, float in_g)
    {
        float Trise = CalculateTrise(in_Vy, in_g);
        return CalculateTfall(in_y, in_Vy, Trise, in_g);
    }
    public static float CalculateTfall(float in_y, float in_Vy, float in_g, float in_Trise)
    {
        float h = CalculateHeight(in_y, in_Vy, in_g, in_Trise);
        return CalculateTfall(h, in_g);
    }
    public static float CalculateTfall(float in_h, float in_g)
    {
        return Mathf.Sqrt(2 * in_h / in_g);
    }

    /// <summary>
    /// Express Y in terms of X (Subsitution) To Calculate Height At Any Given Distance X
    /// </summary>
    /// <param name="in_y">Initial Height</param>
    /// <param name="in_x">Distance X</param>
    /// <param name="in_Vx">X Velocity</param>
    /// <param name="in_Vy">Y Velocity</param>
    /// <param name="in_g">Gravity</param>
    /// <returns></returns>
    public static float CalculateHeight(float in_y, float in_x, float in_Vx, float in_Vy, float in_g)
    {
        float t = CalculateXTime(in_x, in_Vx);
        return CalculateHeight(in_y, in_Vy, in_g, t);
    }
    public static float CalculateHeight(float in_y, float in_Vy, float in_g, float in_t)
    {
        return in_y
            + (in_t * in_Vy)
            - (0.5f * in_g * Mathf.Pow(in_t, 2));
    }

    /// <summary>
    /// //Express X in terms of Y (Subsitution) To Calculate Range At Any Given Height Y
    /// </summary>
    /// <param name="in_y"></param>
    /// <param name="in_Vx"></param>
    /// <param name="in_Vy"></param>
    /// <param name="in_g"></param>
    /// <returns></returns>
    public static float CalculateX_WithY1(float in_y, float in_Vx, float in_Vy, float in_g)
    {
        return in_Vx
            * ((in_Vy / in_g)
            - Mathf.Sqrt((Mathf.Pow(in_Vy, 2) / Mathf.Pow(in_g, 2)) - (2 * in_y / in_g)));
    }
    public static float CalculateX_WithY2(float in_y, float in_Vx, float in_Vy, float in_g)
    {
        return in_Vx
            * ((in_Vy / in_g)
            + Mathf.Sqrt((Mathf.Pow(in_Vy, 2) / Mathf.Pow(in_g, 2)) - (2 * in_y / in_g)));
    }

    /// <summary>
    /// Calculate Time For Any Given Distance X
    /// </summary>
    /// <param name="in_x"></param>
    /// <param name="in_Vx"></param>
    /// <returns></returns>
    public static float CalculateXTime(float in_x, float in_Vx)
    {
        return in_x / in_Vx;
    }

    /// <summary>
    /// Calculate Time For Any Given Height Y
    /// </summary>
    /// <param name="in_y">Height</param>
    /// <param name="in_Vy">Y Velocity</param>
    /// <param name="in_g">Gravity</param>
    /// <returns></returns>
    public static float CalculateYTime1(float in_y, float in_Vy, float in_g)
    {
        if (Mathf.Approximately(in_y, Mathf.Pow(in_Vy, 2) / (2 * in_g)))//Max Height Check
        {
            return in_Vy / in_g;
        }
        else
        {
            return (in_Vy / in_g)
            - Mathf.Sqrt((Mathf.Pow(in_Vy, 2) / Mathf.Pow(in_g, 2)) - (2 * in_y / in_g));
        }
    }
    public static float CalculateYTime2(float in_y, float in_Vy, float in_g)
    {
        if (Mathf.Approximately(in_y, Mathf.Pow(in_Vy, 2) / (2 * in_g)))//Max Height Check
        {
            return in_Vy / in_g;
        }
        else
        {
            return (in_Vy / in_g)
            + Mathf.Sqrt((Mathf.Pow(in_Vy, 2) / Mathf.Pow(in_g, 2)) - (2 * in_y / in_g));
        }
    }

    /// <summary>
    /// Calculate Angle Require To Hit The Target Given Distance X And Height Y
    /// </summary>
    /// <param name="in_v">Initial Velocity</param>
    /// <param name="in_y">Height Y Height</param>
    /// <param name="in_Vx">X Velocity</param>
    /// <param name="in_Vy">Y Velocity</param>
    /// <param name="in_x">Distance X</param>
    /// <param name="in_g">Gravity</param>
    /// <returns></returns>
    public static float CalculateAngle(float in_v, float in_y, float in_Vx, float in_Vy, float in_x, float in_g)
    {
        float x = CalculateRange(in_y, in_Vx, in_Vy, in_g);
        return CalculateAngle(in_v, x, in_g);
    }
    public static float CalculateAngle(float in_v, float in_x, float in_g)
    {
        return Mathf.Asin((in_g * in_x) / Mathf.Pow(in_v, 2)) / 2;
    }


    /// <summary>
    /// Calculate The Velocity Upon Impact On Ground
    /// </summary>
    /// <param name="in_y">Initial Height</param>
    /// <param name="in_Vx">X Velocity</param>
    /// <param name="in_Vy">Y Velocity</param>
    /// <param name="in_g">Gravity</param>
    /// <returns></returns>
    public static float CalculateImpactVelocity(float in_y, float in_Vx, float in_Vy, float in_g)
    {
        float Tfall = CalculateTfall(in_y, in_Vy, in_g);
        return CalculateImpactVelocity(in_Vx, Tfall, in_g);
    }
    public static float CalculateImpactVelocity(float in_Vx, float Tfall, float in_g)
    {
        return Mathf.Sqrt(Mathf.Pow(in_Vx, 2) + Mathf.Pow((-in_g * Tfall), 2));
    }

    /// <summary>
    /// Calculate Velocity At Any Given Distance X And Height Y
    /// </summary>
    /// <param name="in_y">Height Y Height</param>
    /// <param name="in_Vx">X Velocity</param>
    /// <param name="in_Vy">Y Velocity</param>
    /// <param name="in_theta">Angle</param>
    /// <param name="in_g">Gravity</param>
    /// <returns></returns>
    public static float CalculateVelocity(float in_y,float in_Vx,float in_Vy,float in_theta,float in_g)
    {
        float x = CalculateRange(in_y, in_Vx, in_Vy, in_g);
        return CalculateVelocity(x, in_theta, in_g);
    }
    public static float CalculateVelocity(float in_x,float in_theta, float in_g)
    {
        return Mathf.Sqrt((in_g * in_x) / (2 * Mathf.Sin(in_theta) * Mathf.Cos(in_theta)));
    }
}