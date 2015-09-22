using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour {

	public Transform target;

	public float seekSpeed;

	void Update()
	{
		Vector3 direction = target.position - transform.position;
		transform.Translate(direction.normalized * seekSpeed * Time.deltaTime);
	}
}
