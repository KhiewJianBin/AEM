// this is faster than unity *.bounds.IntersectRay(ray)
// original source https://gamedev.stackexchange.com/a/103714/73429

using UnityEngine;

public static class BoundsExtensions
{
    //if (RayBoxIntersect(ray.Origin, ray.Direction, some.bounds.min, some.bounds.max) > 0) {}
    public static float RayBoxIntersect(this Bounds b,Vector3 rpos, Vector3 rdir, Vector3 vmin, Vector3 vmax)
    {
        float t1 = (vmin.x - rpos.x) / rdir.x;
        float t2 = (vmax.x - rpos.x) / rdir.x;
        float t3 = (vmin.y - rpos.y) / rdir.y;
        float t4 = (vmax.y - rpos.y) / rdir.y;
        float t5 = (vmin.z - rpos.z) / rdir.z;
        float t6 = (vmax.z - rpos.z) / rdir.z;

        float aMin = t1 < t2 ? t1 : t2;
        float bMin = t3 < t4 ? t3 : t4;
        float cMin = t5 < t6 ? t5 : t6;

        float aMax = t1 > t2 ? t1 : t2;
        float bMax = t3 > t4 ? t3 : t4;
        float cMax = t5 > t6 ? t5 : t6;

        float fMax = aMin > bMin ? aMin : bMin;
        float fMin = aMax < bMax ? aMax : bMax;

        float t7 = fMax > cMin ? fMax : cMin;
        float t8 = fMin < cMax ? fMin : cMax;

        float t9 = (t8 < 0 || t7 > t8) ? -1 : t7;

        return t9;
    }
}