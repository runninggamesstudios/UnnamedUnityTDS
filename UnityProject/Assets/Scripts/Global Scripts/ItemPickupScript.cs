 using UnityEngine;
using System.Collections;

public class ItemPickupScript : MonoBehaviour {
	public string itemType;
	public string item;

	public GameObject player;
	private PlayerController plrCtrlScript;

	//functions
	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.tag == player.tag){
			if(itemType == "Weapon"){
				plrCtrlScript.pickUpWeapon(item);
				Destroy(gameObject);
			}
		
		}
	
	}                   


	//unity functions
	void Start(){
		plrCtrlScript = player.GetComponent<PlayerController>();
	}

	void Update () {
		transform.Rotate(Vector3.up * 100 * Time.deltaTime);
	}
}
