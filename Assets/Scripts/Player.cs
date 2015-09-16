using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;

	[HideInInspector]
	public Vector3 Direction;

	void Awake()
	{
		Direction = transform.forward;
	}

	void Update()
	{
		float up = Input.GetAxis( "Vertical" ) * Time.deltaTime * speed;
		float right = Input.GetAxis( "Horizontal" ) * Time.deltaTime * speed;

		Direction = (new Vector3( right, 0, up ));

		GetComponent<Rigidbody>().MovePosition( GetComponent<Rigidbody>().position +
			new Vector3( right, 0f, up ) );

	}

}
