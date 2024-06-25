using UnityEngine;

public static class Karatsuba
{
    public static long multiply(int Base, long x, long y)
    {
        long largernum = x>y ? x : y;

        long digits = countDigits(largernum);
        long m = digits - digits / 2;

        //X =  Xl*B^m/2 + Xr
        //Y =  Yl*B^m/2 + Yr
        long xl = getfirstdigits(x, digits, digits-m);
        long yl = getfirstdigits(y, digits, digits-m);
        long xr = getlastdigits(x, m);
        long yr = getlastdigits(y, m);

        //z2 = xl * yl;
        //z0 = xr * xr;
        //z1 = (xl + xr) * (yl + yr) − z2 − z0;
        //r = z2 * B^2m
        long z2 = xl * yl;
        long z0 = xr * yr;
        long z1 = (xl + xr) * (yl + yr) - z2 - z0;

        long bm = (long)Mathf.Pow(Base, m);
        long result = z2 * (bm * bm) + z1 * bm + z0;

        //Debug.Log(string.Format("xl:{0} xr:{1} yl:{2} yr:{3}", xl, xr, yl, yr));
        //Debug.Log(string.Format("z2:{0} z0:{1} z1:{2}", z2,z0,z1));

        return result;
    }
    static long getlastdigits(long num,long digit)
    {
        return num % (long)Mathf.Pow(10, digit);
    }
    static long getfirstdigits(long num,long max, long digit)
    {
        return num / (long)Mathf.Pow(10, max - digit);
    }
    static long countDigits(long num)
    {
        long count = 0;
        while (num != 0)
        {
            num = num / 10;
            count++;
        }
        return count;
    }
}
