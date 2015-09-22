using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Boid : MonoBehaviour {

	private Vector3 v1 = Vector3.zero;
	private Vector3 v2 = Vector3.zero;
	private Vector3 v3 = Vector3.zero;
	[HideInInspector] public Vector3 Velocity = Vector3.zero;

	void Update () {

		Vector3 baseVelocity = ( BoidManager.Player.transform.position - transform.position ).normalized * BoidManager.FlightSpeed;

		Collider[] nearbyBoids = Physics.OverlapSphere( transform.position, BoidManager.FlockMateDistance, BoidManager.BoidMask );

		//Get all boids in within distance and within the angle in front the boid
		List<Boid> boidsInAngle = new List<Boid>();
 		foreach (Collider coll in nearbyBoids)
		{
			if ((Mathf.Acos(Vector3.Dot(transform.position, coll.transform.position)) * Mathf.Rad2Deg) < BoidManager.FlockMateAngle)
			{
				boidsInAngle.Add(coll.GetComponent<Boid>());
			}
		}

		//Seperation
		Vector3 s = Vector3.zero;
		foreach (Collider coll in Physics.OverlapSphere( transform.position, BoidManager.SeperationDistance, BoidManager.BoidMask ))
		{
			Boid boid = coll.GetComponent<Boid>();

			if (boid != null && (boid.transform.position - transform.position).sqrMagnitude != 0)
			{
				s = s + (transform.position - boid.transform.position).normalized * (1 / (transform.position - boid.transform.position).sqrMagnitude) * BoidManager.SeperationStrength;
			}
		}
		v1 = s;

		//Alignment
		Vector3 a = Vector3.zero;
		int i = 0;
		foreach (Collider coll in Physics.OverlapSphere( transform.position, BoidManager.AlignmentDistance, BoidManager.BoidMask ) )
		{
			Boid boid = coll.GetComponent<Boid>();

			if ( boid != null && (boid.transform.position - transform.position).sqrMagnitude != 0 )
			{
				a += boid.Velocity - Velocity;
				i++;
			}
		}
		if ( i != 0 )
			a = a / i;
		v2 = a * BoidManager.AlignmentStrength;

		//Cohesion
		Vector3 c = Vector3.zero;
		foreach (Collider coll in Physics.OverlapSphere( transform.position, BoidManager.CohesionDistance, BoidManager.BoidMask ))
		{
			Boid boid = coll.GetComponent<Boid>();

			if (boid != null && (boid.transform.position - transform.position).sqrMagnitude != 0)
			{
				c = c + (boid.transform.position - transform.position).normalized * (1 / (transform.position - boid.transform.position).sqrMagnitude) * BoidManager.CohesionStrength;
			}
		}
		v3 = c;

		Velocity += (baseVelocity + v1 + v2 + v3) * Time.deltaTime;
		Velocity = Velocity + (Velocity * -BoidManager.DragForce * Time.deltaTime);
		transform.Translate(Velocity * Time.deltaTime);

		transform.LookAt( BoidManager.Player );
	}

}
