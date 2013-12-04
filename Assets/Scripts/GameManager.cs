using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		SpawnPlayer (0,1,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Make a player at all zeros or at a specified piont
	private void SpawnPlayer(){
		Instantiate (player, Vector3.zero, Quaternion.identity);	
	}
	private void SpawnPlayer(float x, float y, float z){
		Instantiate (player, new Vector3(x,y,z), Quaternion.identity);	
	}
}
