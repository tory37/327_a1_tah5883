using UnityEngine;
using System.Collections;

public class BoidManager : MonoBehaviour {

	public static bool UseInteractions { get { return Instance.useInteractions; } }
	public bool useInteractions;

	public static float SeperationDistance {get {return Instance.seperationDistance;} }
	[Header("Seperation")]
	public float seperationDistance;
	public static float SeperationStrength {get {return Instance.seperationStrength;} }
	public float seperationStrength;

	public static float AlignmentDistance { get { return Instance.alignmentDistance; } }
	[Header("Alignment")]
	public float alignmentDistance;
	public static float AlignmentWeakness {get {return Instance.alignmentWeakness;} }
	public float alignmentWeakness;

	public static float CohesionDistance {get {return Instance.cohesionDistance;} }
	[Header("Cohesion")]
	public float cohesionDistance;
	public static float CohesionStrength {get {return Instance.cohesionStrength;} }
	public float cohesionStrength;

	public static float FlightSpeed {get {return Instance.flightSpeed;} }
	[Header("Base Flight")]
	public float flightSpeed;

	public static float FlockMateAngle {get {return Instance.flockMateAngle;} }
	public float flockMateAngle;

	public static float FlockMateDistance {get {return Instance.flockMateDistance;} }
	public float flockMateDistance;

	public static float RotationSpeed {get {return Instance.rotationSpeed;} }
	public float rotationSpeed;

	public static Transform Player {get {return Instance.player;} }
	public Transform player;

	public float playerSpeed;

	public static LayerMask BoidMask {get {return Instance.boidMask;} }
	public LayerMask boidMask;

	public static float DragForce {get {return Instance.dragForce;} }
	public float dragForce;

	public int numOfBoids;
	public Boid boidPrefab;
	public float spawnRadius;

	public static BoidManager Instance;

	public static Boid GreenBoid { get { return Instance.greenBoid; } }
	[Header( "Green Boid" )]
	public Boid greenBoid;
	public float greenNum;
	public static float GreenWait { get { return Instance.greenWait; } }
	public float greenWait;

	public static Boid RedBoid { get { return Instance.redBoid; } }
	[Header("Red Boid")]
	public Boid redBoid;
	public float redNum;
	public static float RedWait { get { return Instance.redWait; } }
	public float redWait;

	public static Boid BlueBoid { get { return Instance.blueBoid; } }
	[Header( "Blue Boid" )]
	public Boid blueBoid;
	public float blueNum;
	public static float BlueWait { get { return Instance.blueWait; } }
	public float blueWait;

	public static Boid BlackBoid { get { return Instance.blackBoid; } }
	[Header( "Black Boid" )]
	public Boid blackBoid;
	public float blackNum;
	public static float BlackWait { get { return Instance.blackWait; } }
	public float blackWait;

	void Start()
	{
		Instance = this;
		if ( !useInteractions )
		{
			for ( int i = 0; i < numOfBoids; i++ )
			{
				GameObject.Instantiate( boidPrefab, Random.insideUnitSphere * spawnRadius, Quaternion.identity );
			}
		}
		else
		{
			for ( int i = 0; i < blueNum; i++ )
			{
				GameObject.Instantiate( blueBoid, Random.insideUnitSphere * spawnRadius, Quaternion.identity );
			}
			for ( int i = 0; i < blackNum; i++ )
			{
				GameObject.Instantiate( blackBoid, Random.insideUnitSphere * spawnRadius, Quaternion.identity );
			}
			for ( int i = 0; i < greenNum; i++ )
			{
				GameObject.Instantiate( greenBoid, Random.insideUnitSphere * spawnRadius, Quaternion.identity );
			}
			for ( int i = 0; i < redNum; i++ )
			{
				GameObject.Instantiate( redBoid, Random.insideUnitSphere * spawnRadius, Quaternion.identity );
			}
		}
	}

	void Update()
	{
		player.transform.Translate( new Vector3( Input.GetAxis( "Horizontal" ) * playerSpeed * Time.deltaTime, Input.GetAxis("UpDown")* playerSpeed * Time.deltaTime, Input.GetAxis( "Vertical" ) * playerSpeed * Time.deltaTime ) );
	}
	
}
