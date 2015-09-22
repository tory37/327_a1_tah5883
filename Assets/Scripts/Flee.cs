using UnityEngine;
using System.Collections;

public class Flee : MonoBehaviour {

	public Transform target;

	public float fleeSpeed;

	void Update()
	{
		Vector3 direction = transform.position - target.position;
		transform.Translate(direction.normalized * fleeSpeed * Time.deltaTime);
	}
}
