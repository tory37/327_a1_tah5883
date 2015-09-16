using UnityEngine;
using System.Collections;

public class Evade : MonoBehaviour {

	public Player target;

	public float evadeSpeed;
	public float evadeStrength;

	void Update()
	{
		Vector3 direction = transform.position - (target.transform.position + (target.Direction * evadeStrength));
		GetComponent<Rigidbody>().MovePosition( GetComponent<Rigidbody>().position + (direction.normalized * evadeSpeed * Time.deltaTime) );
	}
}
