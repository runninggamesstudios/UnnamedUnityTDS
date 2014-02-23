using UnityEngine;
using System.Collections;

public class CameraIntroScript : MonoBehaviour {
	public float speed; //speed of the intro
	public float startSize; //the size that the intro starts with
	public float endSize; //the size that the intro ends with DEFULT IS 20!!!

	//transform.position = player.transform.position + new Vector3(0, cameraHeight, 0);

	void Start(){
		camera.orthographicSize = startSize;
	}

	void Update () {
		camera.orthographicSize = Mathf.Lerp (camera.orthographicSize, endSize, speed * Time.deltaTime);

		if (camera.orthographicSize > endSize - 1) {
			Destroy(this); //this is baiscly saying destroy(script) in roblox
		}
	}
}
