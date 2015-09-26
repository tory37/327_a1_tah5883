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

		//CheckCollision();
		Vector3 vTotal = Cohesion() + Seperation() + Alignment();
		float vMagnitude = vTotal.magnitude;
		transform.forward = Vector3.RotateTowards(transform.forward, vTotal, BoidManager.RotationSpeed * Time.deltaTime, 0f);
		transform.Translate( transform.forward * vMagnitude * Time.deltaTime);
		transform.Translate( -transform.position.normalized * BoidManager.FlightSpeed * Time.deltaTime);
		//transform.Translate( (Cohesion() + Seperation() + Alignment()) * Time.deltaTime);

		/*Vector3 baseVelocity = ( BoidManager.Player.transform.position - transform.position ).normalized * BoidManager.FlightSpeed;

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

		transform.LookAt( BoidManager.Player );*/
	}

	private void CheckCollision()
	{
		foreach ( Collider coll in Physics.OverlapSphere( transform.position, .3F, BoidManager.BoidMask ) )
		{
			if ( coll.GetComponent<Boid>() != null )
			{
				Interaction( coll.GetComponent<Boid>() );
				return;
			}
		}
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

	void Interaction(Boid other)
	{
		Debug.Log( "Interaction" );
		if ( canCollide && other.canCollide)
		{
			canCollide = false;
			other.canCollide = false;
			StartCoroutine( WaitToHitAgain() );

			if ( other != null )
			{
				switch ( type )
				{
					case BoidType.Black:
						switch ( other.type )
						{
							case BoidType.Black:
								Destroy( gameObject );
								Destroy( other.gameObject );
								break;
							case BoidType.Blue:
								Destroy( other.gameObject );
								break;
							case BoidType.Green:
								Destroy( other.gameObject );
								break;
							case BoidType.Red:
								Destroy( other.gameObject );
								break;
						}
						break;

					case BoidType.Blue:
						switch ( other.type )
						{
							case BoidType.Black:
								Destroy( this.gameObject );
								break;
							case BoidType.Blue:
								switch ( Random.Range( 1, 4 ) )
								{
									case 1:
										Instantiate( BoidManager.BlackBoid );
										break;
									case 2:
										Instantiate( BoidManager.BlackBoid );
										break;
									case 3:
										Instantiate( BoidManager.GreenBoid );
										break;
									case 4:
										Instantiate( BoidManager.RedBoid );
										break;
									default:
										Instantiate( BoidManager.BlackBoid );
										break;
								}
								break;
							case BoidType.Green:
								Instantiate( BoidManager.GreenBoid );
								break;
							case BoidType.Red:
								Destroy( other.gameObject );
								Instantiate( BoidManager.RedBoid );
								break;
						}
						break;

					case BoidType.Green:
						switch ( other.type )
						{
							case BoidType.Black:
								Destroy( this.gameObject );
								break;
							case BoidType.Blue:
								Instantiate( BoidManager.GreenBoid );
								break;
							case BoidType.Green:
								Instantiate( BoidManager.GreenBoid );
								Instantiate( BoidManager.BlueBoid );
								break;
							case BoidType.Red:
								Destroy( other.gameObject );
								Instantiate( BoidManager.RedBoid );
								break;
						}
						break;

					case BoidType.Red:
						switch ( other.type )
						{
							case BoidType.Black:
								Destroy( this.gameObject );
								break;
							case BoidType.Blue:
								Destroy( other.gameObject );
								Instantiate( BoidManager.RedBoid );
								break;
							case BoidType.Green:
								Destroy( other.gameObject );
								Instantiate( BoidManager.RedBoid );
								break;
							case BoidType.Red:
								Instantiate( BoidManager.BlackBoid );
								Instantiate( BoidManager.BlackBoid );
								break;
						}
						break;

				}
			}
		}
	}

	IEnumerator WaitToHitAgain ()
	{
		switch (type)
		{
			case BoidType.Black:
				yield return new WaitForSeconds( BoidManager.BlackWait );
				break;
			case BoidType.Blue:
				yield return new WaitForSeconds( BoidManager.BlueWait );
				break;
			case BoidType.Green:
				yield return new WaitForSeconds( BoidManager.GreenWait );
				break;
			case BoidType.Red:
				yield return new WaitForSeconds( BoidManager.RedWait );
				break;
			default:
				yield return new WaitForSeconds( 30f );
				break;
		}

		canCollide = true;
	}

}
