using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {
	public GameObject player;
	public float cameraHeight; //defult is 27
	public float cameraSpeed;
		
	private Vector3 shakeMultiplier;
	private Vector3 endPosition;

	public IEnumerator shakeCamera (float shakeAmount) {
		if(endPosition != new Vector3(0, 0, 0)){
			print("Inside function");

			shakeAmount = shakeAmount / 50;

			shakeMultiplier = new Vector3(
				Random.Range(-shakeAmount, shakeAmount),
				Random.Range(-shakeAmount, shakeAmount),
				Random.Range(-shakeAmount, shakeAmount)
			);
			yield return new WaitForSeconds(0.3f); //def is 0.2
			shakeMultiplier = new Vector3(0,0,0);	
			
		}
	
	}


	void Start () {
		shakeMultiplier = new Vector3(0,0,0);
	}

	void Update () {
		if(player != null){
			endPosition = new Vector3(player.transform.position.x, cameraHeight, player.transform.position.z) + shakeMultiplier;

			transform.position = Vector3.Lerp (transform.position, endPosition, cameraSpeed * Time.deltaTime);
		}
	
	}

}
