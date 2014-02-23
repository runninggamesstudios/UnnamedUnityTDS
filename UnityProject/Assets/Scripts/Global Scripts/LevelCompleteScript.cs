using UnityEngine;
using System.Collections;

public class LevelCompleteScript : MonoBehaviour {
	public int enemys; //level 1 has 9 enenmys

	public string nextLevel; // should be level2 if the current level is level 1

	public void enemyDeath(){
		enemys = enemys - 1;
		if(enemys == 0){
			Debug.Log("Level completed!");

			Application.LoadLevel(nextLevel);		
		}
	}
}
