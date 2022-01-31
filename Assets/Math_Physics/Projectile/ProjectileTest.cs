using UnityEngine;
using AEM.Managers;

public class ProjectileTest : GameManager
{
    public float totalvelocity;
    public float height;
    public float theta;
    public float gravity;

    //public float x;
    public float xVelocity;
    public float yVelocity;

    public override void Start()
    {
        xVelocity = totalvelocity * Mathf.Cos(Mathf.Deg2Rad * theta);
        yVelocity = totalvelocity * Mathf.Sin(Mathf.Deg2Rad * theta);
    }
    void Update()
    {
        float range = Trajectary.CalculateRange(height, xVelocity, yVelocity, gravity);
        print("RANGE IS :" + range);

        float trise = Trajectary.CalculateTrise(yVelocity, gravity);
        print("TRISE IS :" + trise);

        float tfall = Trajectary.CalculateTfall(height, yVelocity, gravity, trise);
        print("TFALL IS :" + tfall);

        float maxheight = Trajectary.CalculateHeight(height, yVelocity, gravity, trise);
        print("MAX HEIGHT IS :" + maxheight);

        float t = Trajectary.CalculateHeight(height, range / 2, xVelocity, yVelocity, gravity);
        print(maxheight + " VS " + t);

        float YTIME1 = Trajectary.CalculateYTime1(maxheight - height, yVelocity, gravity);
        print("YTIME1 :" + YTIME1);

        float YTIME2 = Trajectary.CalculateYTime2(maxheight - height, yVelocity, gravity);
        print("YTIME2 :" + YTIME2);

        float CalX1 = Trajectary.CalculateX_WithY1(0, xVelocity, yVelocity, gravity);
        print("CalX1 :" + CalX1);

        float CalX2 = Trajectary.CalculateX_WithY2(0, xVelocity, yVelocity, gravity);
        print("CalX2 :" + CalX2);
    }
}