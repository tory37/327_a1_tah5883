using UnityEngine;
using System.Collections;

public class Pursuit : MonoBehaviour {

	public Player target;

	public float pursuitSpeed;
	public int framesPredicted = 1;

	//Walls
	public float avoidStrength;
	public float avoidDistance;

	void Update()
	{
		Vector3 targetFuturePosition = target.transform.position + (target.Velocity * framesPredicted);
		Vector3 direction = (targetFuturePosition - transform.position).normalized;

		//Walls
		if ( Physics.Raycast( new Ray( transform.position, direction ), avoidDistance ) )
		{
			direction += Vector3.Cross( Vector3.up, direction * avoidStrength );
			Debug.DrawRay( transform.position, direction * 5);
		}

		transform.Translate( direction * pursuitSpeed * Time.deltaTime );
	}
}
