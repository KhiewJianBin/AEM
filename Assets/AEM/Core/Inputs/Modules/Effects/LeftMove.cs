using UnityEngine;

public class LeftMove : MonoBehaviour {

    public float Speed = 1f;
    void Update()
    {
        transform.Translate(-transform.right * Speed * Time.deltaTime);
    }
}
