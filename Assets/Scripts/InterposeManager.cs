using UnityEngine;
using System.Collections;

public class InterposeManager : MonoBehaviour {

	[SerializeField] private Transform leftPlayer;
	[SerializeField] private Transform rightPlayer;
	[SerializeField] private Transform interposer;

	[SerializeField] private float playerSpeed;

	private void Update()
	{
		if ( Input.GetKey(KeyCode.W) )
			leftPlayer.transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
		if ( Input.GetKey(KeyCode.S) )
			leftPlayer.transform.Translate(Vector3.forward * -playerSpeed * Time.deltaTime);
		if ( Input.GetKey(KeyCode.A) )
			leftPlayer.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
		if ( Input.GetKey(KeyCode.D) )
			leftPlayer.transform.Translate(Vector3.right * -playerSpeed * Time.deltaTime);

		if ( Input.GetKey(KeyCode.UpArrow) )
			rightPlayer.transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
		if ( Input.GetKey(KeyCode.DownArrow) )
			rightPlayer.transform.Translate(Vector3.forward * -playerSpeed * Time.deltaTime);
		if ( Input.GetKey(KeyCode.LeftArrow) )
			rightPlayer.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
		if ( Input.GetKey(KeyCode.RightArrow) )
			rightPlayer.transform.Translate(Vector3.right * -playerSpeed * Time.deltaTime);
	}
}
