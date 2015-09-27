using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Boid : MonoBehaviour {

	public enum BoidType
	{
		Green,
		Blue,
		Red,
		Black
	}

	public BoidType type;

	public bool canCollide = false;

	private Vector3 v1 = Vector3.zero;
	private Vector3 v2 = Vector3.zero;
	private Vector3 v3 = Vector3.zero;
	[HideInInspector] public Vector3 Velocity = Vector3.zero;

	void Update () {

		Vector3 vTotal = Cohesion() + Seperation() + Alignment();
		float vMagnitude = vTotal.magnitude;
		transform.forward = Vector3.RotateTowards(transform.forward, vTotal, BoidManager.RotationSpeed * Time.deltaTime, 0f);
		transform.Translate(transform.forward * vMagnitude * Time.deltaTime);
		transform.Translate(-transform.position.normalized * BoidManager.FlightSpeed * Time.deltaTime);
		//transform.Translate( (Cohesion() + Seperation() + Alignment()) * Time.deltaTime);
	}

	private Vector3 Cohesion()
	{
		Vector3 centerOfMass = transform.position;
		int numberOfBoids = 0;
		foreach ( Collider coll in Physics.OverlapSphere( transform.position, BoidManager.CohesionDistance, BoidManager.BoidMask ) )
		{
			Boid boid = coll.GetComponent<Boid>();
			if (boid != null)
			{
				centerOfMass += boid.transform.position;
				numberOfBoids++;
			}
		}
		if ( numberOfBoids > 0 )
			centerOfMass = centerOfMass / numberOfBoids;

		//Debug.Log( "Cohesion: " + centerOfMass.normalized * BoidManager.CohesionStrength );
		return ( centerOfMass.normalized - transform.position) * BoidManager.CohesionStrength;
	}

	private Vector3 Seperation()
	{
		Vector3 sepDirection = Vector3.zero;
		int numberOfBoids = 0;
		foreach ( Collider coll in Physics.OverlapSphere( transform.position, BoidManager.SeperationDistance, BoidManager.BoidMask ) )
		{
			Boid boid = coll.GetComponent<Boid>();
			if (boid != null)
			{
				sepDirection += transform.position - boid.transform.position;
				numberOfBoids++;
			}
		}
		//Debug.Log("Seperation: " + sepDirection.normalized * BoidManager.SeperationStrength);
		return sepDirection.normalized * BoidManager.SeperationStrength;
	}

	private Vector3 Alignment()
	{
		Vector3 newVelocity = Vector3.zero;
		int numberOfBoids = 0;
		foreach ( Collider coll in Physics.OverlapSphere( transform.position, BoidManager.AlignmentDistance, BoidManager.BoidMask ) )
		{
			Boid boid = coll.GetComponent<Boid>();
			if (boid != null)
			{
				newVelocity += boid.Velocity;
				numberOfBoids++;
			}
		}
		//Debug.Log("Alignment: " + newVelocity / BoidManager.AlignmentWeakness);
		return newVelocity / BoidManager.AlignmentWeakness;
	}
}
