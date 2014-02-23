using UnityEngine;
using System.Collections;

public class EnemyProjectileScript : MonoBehaviour {
	public float speed; //DEF IS 85
	public float damage; //DEF IS 25
	
	private PlayerHealth playerHealthScript;
	
	//someScript = GetComponent (ExampleScript);
	
	void OnTriggerEnter(Collider obj){
		if (obj.gameObject.tag == "LevelObject") {
			Destroy (gameObject);
		} else if (obj.gameObject.tag == "Player") {
			playerHealthScript = obj.gameObject.GetComponent<PlayerHealth>();
			playerHealthScript.playerDamage = damage;
			Destroy (gameObject);
		}
		
		
	}
	
	void Update () {
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
	
}
