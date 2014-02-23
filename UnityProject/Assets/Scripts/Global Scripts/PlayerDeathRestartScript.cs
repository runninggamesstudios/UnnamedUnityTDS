using UnityEngine;
using System.Collections;

public class PlayerDeathRestartScript : MonoBehaviour {
	public GUIText deathGui;
	private string deathGuiText = "You have died. Press 'r' to respawn.";

	public int playerAlive;

	// Use this for initialization
	void Start () {
		playerAlive = 1; //set player to be alive.
		deathGui.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (playerAlive == 0) {
			deathGui.text = deathGuiText;
		}
	
		if (playerAlive == 0 && Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
