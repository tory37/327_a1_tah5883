using UnityEngine;
using System.Collections;

public class Evade : MonoBehaviour {

	public Player target;

	public float evadeSpeed;
	public int framesPredicted = 1;

	public float avoidStrength;
	public float avoidDistance;

	void Update()
	{
		Vector3 targetFuturePosition = target.transform.position + (target.Velocity * framesPredicted);
		Vector3 direction = (transform.position - targetFuturePosition).normalized;

		if ( Physics.Raycast( new Ray( transform.position, direction ), avoidDistance ) )
		{
			direction += Vector3.Cross( Vector3.up, direction * avoidStrength );
			Debug.DrawRay( transform.position, direction * 5);
		}

		transform.Translate( direction * evadeSpeed * Time.deltaTime );
	}
}
