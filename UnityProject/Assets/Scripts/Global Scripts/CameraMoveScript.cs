using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {
	public GameObject player;
	public float cameraHeight; //defult is 27
	public float cameraSpeed;

	private Vector3 endPosition;

	void Update () {
		if(player != null){
			endPosition = new Vector3(player.transform.position.x, cameraHeight, player.transform.position.z);
			transform.position = Vector3.Lerp (transform.position, endPosition, cameraSpeed * Time.deltaTime);
		}
	}
}
