using UnityEngine;

public class Parametric_Example : MonoBehaviour
{
    public MeshFilter meshfilter;

    bool isusingU;
    bool isusingV;
    bool isusingW;

    float uMinDomain;
    float uMaxDomain;

    float vMinDomain;
    float vMaxDomain;

    float wMinDomain;
    float wMaxDomain;

    int sampleresolution_U;
    int sampleresolution_V;
    int sampleresolution_W;

    void samplingfunction(float u, float v, float w, out double x, out double y, out double z)
    {
        x = u;
        y = v;
        z = w;
    }
    // Start is called before the first frame update
    void Start()
    {
        isusingU = true;
        isusingV = true;
        isusingW = true;

        uMinDomain = -1;
        uMaxDomain = 1;

        vMinDomain = -1;
        vMaxDomain = 1;

        wMinDomain = -1;
        wMaxDomain = 1;

        sampleresolution_U = 100;
        sampleresolution_V = 100;
        sampleresolution_W = 100;


        ParametricMesh parametricMesh = new ParametricMesh();
        parametricMesh.parametricFunction = samplingfunction;
        parametricMesh.CreateParametricMesh(GetComponent<MeshFilter>().mesh,
                                isusingU, isusingV, isusingW,
                                uMinDomain, uMaxDomain,
                                vMinDomain, vMaxDomain,
                                wMinDomain, wMaxDomain,
                                sampleresolution_U, sampleresolution_V, sampleresolution_W);
    }
}
