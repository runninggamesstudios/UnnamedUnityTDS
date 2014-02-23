using UnityEngine;
using System.Collections;
//* Made by Anthony, Graphics work by Ben Kraft
public class ProjectileScript : MonoBehaviour {
	public float speed; //DEF IS 85
	public float damage; //DEF IS 50

	private EnemyHealthScript healthScript;

	//someScript = GetComponent (ExampleScript);

	void OnTriggerEnter(Collider obj){
		if (obj.gameObject.tag == "LevelObject") {
			Destroy (gameObject);
		} else if (obj.gameObject.tag == "Enemy") {
			healthScript = obj.gameObject.GetComponent<EnemyHealthScript>();

			healthScript.damage = damage;
		}
	}

	void Update () {
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
		
}
