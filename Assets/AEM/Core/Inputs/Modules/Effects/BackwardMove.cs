using UnityEngine;

public class BackwardMove : MonoBehaviour {

    public float Speed = 1f;
    void Update()
    {
        transform.Translate(-transform.forward * Speed * Time.deltaTime);
    }
}
