using UnityEngine;

public class Karatsuba_Example : MonoBehaviour
{
    void Start()
    {
        long num1 = 1234567891231232133;
        long num2 = 6789245135113132313;

        float start = Time.realtimeSinceStartup;
        print(Karatsuba.multiply(10, num1, num2));
        float stop = Time.realtimeSinceStartup;
        print(stop - start);

        start = Time.realtimeSinceStartup;
        print(num1 * num2);
        stop = Time.realtimeSinceStartup;
        print(stop - start);
    }
}
