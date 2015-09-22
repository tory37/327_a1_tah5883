using UnityEngine;
using System.Collections;

public class BoidManager : MonoBehaviour {

	public Transform boidsContainer;

	public static float SeperationStrength {get {return Instance.seperationStrength;} }
	public float seperationStrength;
	public static float SeperationDistance {get {return Instance.seperationDistance;} }
	public float seperationDistance;

	public static float AlignmentDistance { get { return Instance.alignmentDistance; } }
	public float alignmentDistance;
	public static float AlignmentStrength {get {return Instance.alignmentStrength;} }
	public float alignmentStrength;

	public static float CohesionDistance {get {return Instance.cohesionDistance;} }
	public float cohesionDistance;
	public static float CohesionStrength {get {return Instance.cohesionStrength;} }
	public float cohesionStrength;

	public static float FlightSpeed {get {return Instance.flightSpeed;} }
	public float flightSpeed;

	public static float FlockMateAngle {get {return Instance.flockMateAngle;} }
	public float flockMateAngle;

	public static float FlockMateDistance {get {return Instance.flockMateDistance;} }
	public float flockMateDistance;

	public static BoidManager Instance;

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

	void Start()
	{
		Instance = this;
		for (int i = 0; i < numOfBoids; i ++)
		{
			GameObject.Instantiate(boidPrefab, Random.insideUnitSphere * spawnRadius, Quaternion.identity);
		}
	}

	void Update()
	{
		player.transform.Translate( new Vector3( Input.GetAxis( "Horizontal" ) * playerSpeed * Time.deltaTime, Input.GetAxis("UpDown")* playerSpeed * Time.deltaTime, Input.GetAxis( "Vertical" ) * playerSpeed * Time.deltaTime ) );
	}
	
}
