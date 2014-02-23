using UnityEngine;
using System.Collections;

public class BoundryScript : MonoBehaviour {
	void OnTriggerExit(Collider obj){


		Destroy (obj.gameObject);
	}
}
