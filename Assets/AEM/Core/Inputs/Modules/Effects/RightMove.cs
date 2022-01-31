using UnityEngine;

public class RightMove : MonoBehaviour {

    public float Speed = 1f;
    void Update()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime);
    }
}
