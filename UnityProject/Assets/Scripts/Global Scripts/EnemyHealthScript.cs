using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {
	private float enemyHealth;
	public float damage; // amount of damage to inflict should be 0 at start
	public GameObject gameController;
	public LevelCompleteScript levelScript;

	void Start(){
		gameController = GameObject.FindWithTag ("GameController");

		enemyHealth = 100;

		damage = 0.0f;

		levelScript = gameController.GetComponent<LevelCompleteScript>();
	}	

	void Update(){
		if (damage > 0) {
			enemyHealth = enemyHealth - damage;
			damage = 0;
		}

		if (enemyHealth < 0 | enemyHealth == 0) {
			levelScript.enemyDeath();
			

			Destroy(this.gameObject);
		}
	}
}
