using UnityEngine;
using System.Collections;

public class BoidManager : MonoBehaviour
{

	public static bool UseInteractions { get { return Instance.useInteractions; } }
	public bool useInteractions;

	public static float SeperationDistance { get { return Instance.seperationDistance; } }
	[Header("Seperation")]
	public float seperationDistance;
	public static float SeperationStrength { get { return Instance.seperationStrength; } }
	public float seperationStrength;

	public static float AlignmentDistance { get { return Instance.alignmentDistance; } }
	[Header("Alignment")]
	public float alignmentDistance;
	public static float AlignmentWeakness { get { return Instance.alignmentWeakness; } }
	public float alignmentWeakness;

	public static float CohesionDistance { get { return Instance.cohesionDistance; } }
	[Header("Cohesion")]
	public float cohesionDistance;
	public static float CohesionStrength { get { return Instance.cohesionStrength; } }
	public float cohesionStrength;

	public static float FlightSpeed { get { return Instance.flightSpeed; } }
	[Header("Base Flight")]
	public float flightSpeed;

	public static float FlockMateAngle { get { return Instance.flockMateAngle; } }
	public float flockMateAngle;

	public static float FlockMateDistance { get { return Instance.flockMateDistance; } }
	public float flockMateDistance;

	public static float RotationSpeed { get { return Instance.rotationSpeed; } }
	public float rotationSpeed;

	public static Transform Player { get { return Instance.player; } }
	public Transform player;

	public float playerSpeed;

	public static LayerMask BoidMask { get { return Instance.boidMask; } }
	public LayerMask boidMask;

	public static float DragForce { get { return Instance.dragForce; } }
	public float dragForce;

	public int numOfBoids;
	public Boid boidPrefab;
	public float spawnRadius;

	public static BoidManager Instance;

	public float interactionInterval;

	public static Boid GreenBoid { get { return Instance.greenBoid; } }
	[Header( "Green Boid" )]
	public Boid greenBoid;
	public float greenNum;

	public static Boid RedBoid { get { return Instance.redBoid; } }
	[Header("Red Boid")]
	public Boid redBoid;
	public float redNum;

	public static Boid BlueBoid { get { return Instance.blueBoid; } }
	[Header( "Blue Boid" )]
	public Boid blueBoid;
	public float blueNum;

	public static Boid BlackBoid { get { return Instance.blackBoid; } }
	[Header( "Black Boid" )]
	public Boid blackBoid;
	public float blackNum;

	void Start()
	{
		Instance = this;
		if ( !useInteractions )
		{
			for ( int i = 0; i < numOfBoids; i++ )
			{
				GameObject.Instantiate(boidPrefab, Random.insideUnitSphere * spawnRadius, Quaternion.identity);
			}
		}
		else
		{
			for ( int i = 0; i < blueNum; i++ )
			{
				GameObject.Instantiate(blueBoid, Random.insideUnitSphere * spawnRadius, Quaternion.identity);
			}
			for ( int i = 0; i < blackNum; i++ )
			{
				GameObject.Instantiate(blackBoid, Random.insideUnitSphere * spawnRadius, Quaternion.identity);
			}
			for ( int i = 0; i < greenNum; i++ )
			{
				GameObject.Instantiate(greenBoid, Random.insideUnitSphere * spawnRadius, Quaternion.identity);
			}
			for ( int i = 0; i < redNum; i++ )
			{
				GameObject.Instantiate(redBoid, Random.insideUnitSphere * spawnRadius, Quaternion.identity);
			}
		}

		foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("Boid"))
		{
			Boid boid = obj.GetComponent<Boid>();
			if (boid != null)
			{
				boid.canCollide = true;
			}
		}

		StartCoroutine(BoidInteraction());
	}

	void Update()
	{
		player.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime, Input.GetAxis("UpDown") * playerSpeed * Time.deltaTime, Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime));
	}

	IEnumerator BoidInteraction()
	{
		yield return new WaitForSeconds(interactionInterval);

		GameObject[] boids = GameObject.FindGameObjectsWithTag("Boid");

		foreach ( GameObject obj in boids )
		{
			Boid boid = obj.GetComponent<Boid>();
			if ( boid != null )
			{
				foreach ( Collider coll in Physics.OverlapSphere(transform.position, 1F, BoidManager.BoidMask) )
				{
					Boid other = coll.GetComponent<Boid>();
					if ( other != null )
					{
						if ( other != null )
						{
							switch ( boid.type )
							{
								case Boid.BoidType.Black:
									switch ( other.type )
									{
										case Boid.BoidType.Black:
											Destroy(boid.gameObject);
											Destroy(other.gameObject);
											break;
										case Boid.BoidType.Blue:
											Destroy(other.gameObject);
											break;
										case Boid.BoidType.Green:
											Destroy(other.gameObject);
											break;
										case Boid.BoidType.Red:
											Destroy(other.gameObject);
											break;
									}
									break;

								case Boid.BoidType.Blue:
									switch ( other.type )
									{
										case Boid.BoidType.Black:
											Destroy(boid.gameObject);
											break;
										case Boid.BoidType.Blue:
											switch ( Random.Range(1, 4) )
											{
												case 1:
													Instantiate(BoidManager.BlackBoid);
													break;
												case 2:
													Instantiate(BoidManager.BlackBoid);
													break;
												case 3:
													Instantiate(BoidManager.GreenBoid);
													break;
												case 4:
													Instantiate(BoidManager.RedBoid);
													break;
												default:
													Instantiate(BoidManager.BlackBoid);
													break;
											}
											break;
										case Boid.BoidType.Green:
											Instantiate(BoidManager.GreenBoid);
											break;
										case Boid.BoidType.Red:
											Destroy(other.gameObject);
											Instantiate(BoidManager.RedBoid);
											break;
									}
									break;

								case Boid.BoidType.Green:
									switch ( other.type )
									{
										case Boid.BoidType.Black:
											Destroy(boid.gameObject);
											break;
										case Boid.BoidType.Blue:
											Instantiate(BoidManager.GreenBoid);
											break;
										case Boid.BoidType.Green:
											Instantiate(BoidManager.GreenBoid);
											Instantiate(BoidManager.BlueBoid);
											break;
										case Boid.BoidType.Red:
											Destroy(other.gameObject);
											Instantiate(BoidManager.RedBoid);
											break;
									}
									break;

								case Boid.BoidType.Red:
									switch ( other.type )
									{
										case Boid.BoidType.Black:
											Destroy(boid.gameObject);
											break;
										case Boid.BoidType.Blue:
											Destroy(other.gameObject);
											Instantiate(BoidManager.RedBoid);
											break;
										case Boid.BoidType.Green:
											Destroy(other.gameObject);
											Instantiate(BoidManager.RedBoid);
											break;
										case Boid.BoidType.Red:
											Instantiate(BoidManager.BlackBoid);
											Instantiate(BoidManager.BlackBoid);
											break;
									}
									break;
							}
							other.canCollide = false;
							boid.canCollide = false;
							break;
						}
					}
				}
			}
		}

		foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("Boid"))
		{
			Boid boid = obj.GetComponent<Boid>();
			if (boid != null)
			{
				boid.canCollide = true;
			}
		}

		StartCoroutine(BoidInteraction());
	}
}
