using UnityEngine;

public class ForwardMove : MonoBehaviour
{
    public float Speed = 1f;
	void FixedUpdate ()
	{
	    transform.Translate(transform.forward * Speed * Time.fixedDeltaTime);	
	}
}
