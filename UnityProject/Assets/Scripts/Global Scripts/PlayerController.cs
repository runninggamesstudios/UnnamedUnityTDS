using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	//PISTOL CAMERA SHAKE ANGLE IS 40
	//SMG IS 110
	//SHOTGUN IS 200
	//PISTOL ACCURACE IS 1.5
	//SHOTGUN IS 2.3

	public GUIText reloadGui;
	private string reloadGuiText; 

	public GameObject cameraGameOb;
	private CameraMoveScript cameraScript;
	private Camera cameraComp;

	public GUIText guiItemPickupNotif;

	public GUIText reloadingInfoGui;
	private string reloadingTextActiveTxt; 

	public string[] weaponList;

	public GameObject guiWeaponTextGameObj;
	private GUIText guiWeaponText;

	public string currentWeapon; //starting is "Pistol"

	//weapon stats variables
	private int pistolMaxClip; //def is 12
	private float pistolFireRate; //def is 0.23
	private float pistolAccuracy; //def is 1.8
	private int pistolCameraShake; //def is 40
	private int pistolBulletsPerShot; //1 if its not a shotgun
	private float pistolReloadTime; //def is 0.5

	private int smgMaxClip; //def is 30
	private float smgFireRate; //def is 0.18
	private float smgAccuracy; //def is 2
	private int smgCameraShake; //def is 110
	private int smgBulletsPerShot; //1 if its not a shotgun
	private float smgReloadTime; //def is 0.5

	private int shotgunMaxClip; //def is 8
	private float shotgunFireRate; //def is 0.5
	private float shotgunAccuracy; //def is 2
	private int shotgunCameraShake; //def is 200
	private int shotgunBulletsPerShot; //def is 9
	private float shotgunReloadTime; //def is 3

	public GameObject projectile;
	public GameObject muzzle;
	private GameObject projectileClone;
	//

	private int Clip;

	public float fireRate; //def is 0.2
	private float nextFire;
	public float accuracy; //def is 1.5

	public float walkSpeed; //def is 450

	private float xInputAxis; //A D
	private float zInputAxis; // W S
	private float fireButtonAxis; //left mouse button

	private CharacterController controller;

	private bool reloading;

	public float reloadTimeDelay; //def is 0.5

	private float cameraShake;



	private float bulletsPerShot; 

	public GameObject shotgunShellEjectSound;

	//functions
	IEnumerator activateGuiItemPickup(string item){
		guiItemPickupNotif.text = "You picked up " + "'" + item + "'";
		yield return new WaitForSeconds(2.0f);
		guiItemPickupNotif.text = "";
	}

	IEnumerator playShotgunShellEjectSound(){
		yield return new WaitForSeconds(0.2f);
		shotgunShellEjectSound.audio.Play();
	}

	IEnumerator reload(){
		reloadGui.text = "";

		reloading = true;
		reloadingInfoGui.text = reloadingTextActiveTxt;

		yield return new WaitForSeconds(reloadTimeDelay);
		if(currentWeapon == "Pistol"){
			Clip = pistolMaxClip;
			guiWeaponText.text = currentWeapon + "" + Clip + "/" + pistolMaxClip;
		} else if (currentWeapon == "SMG"){
			Clip = smgMaxClip;
			guiWeaponText.text = currentWeapon + "" + Clip + "/" + smgMaxClip;
		} else if (currentWeapon == "Shotgun"){
			Clip = shotgunMaxClip;
			guiWeaponText.text = currentWeapon + "" + Clip + "/" + shotgunMaxClip;
		}

		reloadingInfoGui.text = "";
		reloading = false;
	}

 	public void pickUpWeapon(string weapon){
		if(weapon == weaponList[0]){
			currentWeapon = "Pistol";
			Clip = pistolMaxClip;
			fireRate = pistolFireRate;
			accuracy = pistolAccuracy;
			cameraShake = pistolCameraShake;
			guiWeaponText.text = currentWeapon + "" + Clip + "/" + pistolMaxClip;
			bulletsPerShot = pistolBulletsPerShot;
			reloadTimeDelay = pistolReloadTime;		
			StartCoroutine(activateGuiItemPickup("Pistol"));
		} else if (weapon == weaponList[1]){
			currentWeapon = "SMG";
			Clip = smgMaxClip;
			fireRate = smgFireRate;	
			accuracy = smgAccuracy;
			cameraShake = smgCameraShake;
			guiWeaponText.text = currentWeapon + "" + Clip + "/" + smgMaxClip;
			bulletsPerShot = smgBulletsPerShot;
			reloadTimeDelay = smgReloadTime;
			StartCoroutine(activateGuiItemPickup("SMG"));
		} else if (weapon == weaponList[2]){
			currentWeapon = "Shotgun";
			Clip = shotgunMaxClip;
			fireRate = shotgunFireRate;
			accuracy = shotgunAccuracy;
			cameraShake = shotgunCameraShake;
			guiWeaponText.text = currentWeapon + "" + Clip + "/" + shotgunMaxClip;
			bulletsPerShot = shotgunBulletsPerShot;
			reloadTimeDelay = shotgunReloadTime;
			StartCoroutine(activateGuiItemPickup("Shotgun"));
		}
	
	}


	//System Functions (unity called functions)
	void Start(){
		//init variables
		reloadGuiText = "Press 'R' to reload.";
		reloadGui.text = "";
		reloading = false;
		reloadingTextActiveTxt = "Reloading..";
		reloadingInfoGui.text = "";	

		guiWeaponText = guiWeaponTextGameObj.GetComponent<GUIText>();

		cameraComp = cameraGameOb.GetComponent<Camera> ();
		cameraScript = cameraGameOb.GetComponent<CameraMoveScript> ();
		//init currentWeapon, WEAPON SPECS AND WeaponList
		weaponList = new string[3];
		weaponList[0] = "Pistol";
		weaponList[1] = "SMG";
		weaponList[2] = "Shotgun";

		pistolMaxClip = 19;
		pistolFireRate = 0.23f;// def is 0.23
		pistolAccuracy = 2.6f; //def is 2
		pistolCameraShake = 60; // def is 60
		pistolBulletsPerShot = 1;
		pistolReloadTime = 0.8f;

		smgMaxClip = 30;
		smgFireRate = 0.18f;
		smgAccuracy = 3.0f;
		smgCameraShake = 110;// defi s 110
		smgBulletsPerShot = 1;
		smgReloadTime = 0.8f;

		shotgunMaxClip = 8;
		shotgunFireRate = 0.5f;
		shotgunAccuracy = 3.0f;// def is 2.0
		shotgunCameraShake = 200;
		shotgunBulletsPerShot = 6; //def is 6
		shotgunReloadTime = 3.0f;

		//activate the starting weapon (pistol)
		Clip = pistolMaxClip;
		fireRate = pistolFireRate;
		currentWeapon = "Pistol"; //starting weapon
		accuracy = pistolAccuracy;
		cameraShake = pistolCameraShake;
		bulletsPerShot = pistolBulletsPerShot;
		reloadTimeDelay = pistolReloadTime;

		//init guis
		guiWeaponText.text = currentWeapon + "" + Clip + "/" + pistolMaxClip; //starting weapon is pistol		
		guiItemPickupNotif.text = "";

	}

	void Update(){
		// get axis
		CharacterController controller = GetComponent<CharacterController> ();

		xInputAxis = Input.GetAxisRaw ("Horizontal");
		zInputAxis = Input.GetAxisRaw ("Vertical");		
		fireButtonAxis = Input.GetAxisRaw ("Fire1");

		//point player at mouse
		Vector3 mousePos = cameraComp.ScreenToWorldPoint (Input.mousePosition);
		transform.LookAt (new Vector3(mousePos.x, transform.position.y, mousePos.z), Vector3.up);	
		//walking
		//Vector3 forward = transform.TransformDirection (new Vector3(xCtrlAxis, 0.0f, zCtrlAxis));
		controller.SimpleMove(new Vector3(xInputAxis, 0.0f, zInputAxis) * walkSpeed * Time.deltaTime);


		//reloading
		if(Clip != 0){
			if(Input.GetKeyDown(KeyCode.R)){
				StartCoroutine("reload");
			
			}
		
		}		
			//shooting
		if (fireButtonAxis == 1 && Time.time > nextFire) {
			if(Clip != 0 && reloading == false){
				nextFire = Time.time + fireRate;
				//z is up/down and y is forward.
				Clip = Clip - 1;

				if(currentWeapon  == "Pistol"){
					guiWeaponText.text = currentWeapon + "" + Clip + "/" + pistolMaxClip;
				} else if (currentWeapon == "SMG"){
					guiWeaponText.text = currentWeapon + "" + Clip + "/" + smgMaxClip;
				} else if (currentWeapon == "Shotgun"){
					guiWeaponText.text = currentWeapon + "" + Clip + "/" + shotgunMaxClip;
				}

				cameraScript.StartCoroutine(cameraScript.shakeCamera(cameraShake));

				for(int i = 0; i < bulletsPerShot; i++){
					Instantiate(
						projectile,
						muzzle.transform.position, 
						muzzle.transform.rotation* Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
					//
					audio.Play();
				}
				if(currentWeapon == "Shotgun"){
					StartCoroutine("playShotgunShellEjectSound");
				}
				
			} 
		
		}

		if(Clip == 0 && reloading == false){
			reloadGui.text = reloadGuiText;	
			if(Input.GetKeyDown(KeyCode.R)){
				StartCoroutine("reload");
			}
		}

	}

}