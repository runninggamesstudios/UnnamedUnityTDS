using UnityEngine;
using System.Collections;
//* Made by Anthony, Graphics work by Ben Kraft
public class PlayerHealth : MonoBehaviour {
	public float playerHealth;

	public GameObject gameController;

	public float playerDamage; //damage to inflict opon player

	public GameObject plrCamera;

	private PlayerDeathRestartScript restartScript;

	public GUIText healthGui;
	private string healthGuiText;

	public float speed;


	public IEnumerator shakeCamera(float num, float num1, float num2){ //num2 defult is 110
		plrCamera.transform.RotateAround (Vector3.zero, new Vector3(num, 0, num1), num2 * Time.deltaTime);
		yield return new WaitForSeconds(speed);
		plrCamera.transform.RotateAround (Vector3.zero, new Vector3(num * -1, 0, num1 * -1), num2 * Time.deltaTime);
		yield return new WaitForSeconds(speed);
		plrCamera.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
	}

	void Awake () {
		playerHealth = 100; //defult is 100	
		playerDamage = 0;

		speed = 0.15f;

		healthGuiText = "Health:";
	}	

	void Start() {
		restartScript = gameController.GetComponent<PlayerDeathRestartScript> ();

		plrCamera.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
	}

	void Update () {
		if (playerDamage > 0) {
			playerHealth = playerHealth - playerDamage;

			StartCoroutine(shakeCamera(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 210));

			playerDamage = 0;	
		}

		if (playerHealth < 0 || playerHealth == 0) {
			//tell game controller that player has died
			restartScript.playerAlive = 0;
			Destroy(this.gameObject); //remove the player
		}

		healthGui.text = healthGuiText + playerHealth;
	}
}
