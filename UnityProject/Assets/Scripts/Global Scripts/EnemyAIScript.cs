using UnityEngine;
using System.Collections;

public class EnemyAIScript : MonoBehaviour {
	public GameObject enemyObj;
	private CharacterController enemyCharCtrl;

	public GameObject enemyLightObj;
	private Light enemyLight;

	private GameObject playerObj;

	private bool playerSeen;

	public GameObject enemyProjectile;

	private float xAxis;
	private float zAxis;
	public float speed; // def is 500

	private float dist; // distance from player
	private float maxDist; //once we are at or under this distance we will start dodging bullets. def is 15

	public float accuracy; //def is 2.0
	public GameObject projectile;
	public float fireRate; //def 0.8
	public float fireRateMin; //def is 0.3
	private float nextFire;
	public GameObject muzzle;

	private Vector3 raycastDir;
	private float raycastDis; //def is 100

	private bool runDodgeBullets;

	public float playerSpottedDelay; //def is 0.5

	private bool shooting;
	
	
	//functions
	IEnumerator shoot(){
		while (shooting == false){
			shooting = true;
			yield return new WaitForSeconds (playerSpottedDelay);
			Instantiate(
				enemyProjectile,
				muzzle.transform.position,
				muzzle.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy))
			);

			audio.Play();
			yield return new WaitForSeconds (Random.Range(fireRateMin, fireRate));
			shooting = false;			
		}
		
	}

	IEnumerator dodgeBullets(){
		yield return new WaitForSeconds (Random.Range (-2.0f, 2.0f));

		while (true) {
			yield return new WaitForSeconds (Random.Range (-0.7f, 0.7f));// MAX AMOUNT OF TIME BEFORE DIRECTION CHANGE
			
			xAxis = Random.Range (-1, 1);
			zAxis = Random.Range (-1, 1);                              
		}
	}
	//


	//unity functions
	void Start(){
		//init variables
		shooting = false;

		playerSeen = false;	
		
		raycastDis = 100.0f;

		xAxis = 0;
		zAxis = 0;

		runDodgeBullets = false;
		dist = 0;
		maxDist = 15;

		//set the player object and enemy and GETCOMPONENT
		playerObj = GameObject.FindWithTag ("Player");
		enemyCharCtrl = enemyObj.GetComponent<CharacterController> ();
		enemyLight = enemyLightObj.GetComponent<Light> ();
	}

	void Update(){
		//change controll axis
		if (playerSeen == true) {
			if(playerObj != null){
				//See if the player is in line of sight and if they are then SHOOT.
				RaycastHit hit;
				raycastDir = transform.TransformDirection(Vector3.forward);

				if(Physics.Raycast(enemyObj.transform.position, raycastDir, out hit, raycastDis)){ 
					if(hit.collider.gameObject.tag == "Player"){
						if(shooting == false){
							StartCoroutine("shoot");					
						}				
					
					} else if(hit.collider.gameObject.tag == "LevelObject") {
						StopCoroutine("shoot");
						shooting = false;
					}
				}

				//look at player
				enemyObj.transform.LookAt(new Vector3(playerObj.transform.position.x, enemyObj.transform.position.y, playerObj.transform.position.z));

							

				//dodge bullets
				dist = Vector3.Distance(enemyObj.transform.position, playerObj.transform.position);
				if(dist < maxDist && runDodgeBullets == false){
					runDodgeBullets = true;
					StartCoroutine("dodgeBullets");
				} else if(dist > maxDist && runDodgeBullets == true){
					runDodgeBullets = false;
					StopCoroutine("dodgeBullets");
					zAxis = 1;
					xAxis = 0; //cant believe i forgot about this... sigh
				}
			}
		}
		//move enemy
		enemyCharCtrl.SimpleMove (transform.TransformDirection(new Vector3 (xAxis, 0.0f, zAxis)) * speed * Time.deltaTime);			
	}	

	void OnTriggerEnter(Collider obj){
		if (obj.gameObject.tag == "Player" && playerSeen == false) {
			playerSeen = true;			
			zAxis = 1;
			enemyLight.intensity = 1.0f;	
		//	nextFire = Time.time + playerSpottedDelay;
		} else if (obj.gameObject.tag == "Projectile" && playerSeen == false) {
			playerSeen = true;			
			zAxis = 1;
			enemyLight.intensity = 1.0f;
		//	nextFire = Time.time + playerSpottedDelay;
		} 	
	}
}
