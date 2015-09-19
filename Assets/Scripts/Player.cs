using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;

	[HideInInspector]
	public Vector3 Velocity;

	void Awake()
	{
		Velocity = Vector3.zero;
	}

	void Update()
	{
		float forward = Input.GetAxis( "Vertical" ) * Time.deltaTime * speed;
		float right = Input.GetAxis( "Horizontal" ) * Time.deltaTime * speed;

		Velocity = new Vector3(right, 0f, forward);

		transform.Translate( Velocity );

	}

}
