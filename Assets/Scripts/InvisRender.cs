using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisRender : MonoBehaviour {

	public GameObject player;
	//Cream variables segons la eix que esta el jugador
	public float playerY;
	public float playerX;
	public float playerZ;

    //Utilitzeu això per a la inicialització
	void Start () {
		
	}
	
	// L'actualització es crida una vegada per fotograma
	void Update () {
		if (player.transform.position.y != playerY || !(player.transform.position.z < playerZ) || !(player.transform.position.x < playerX)) {
			this.gameObject.GetComponent<Renderer> ().enabled = false;
		} else {
			this.gameObject.GetComponent<Renderer>().enabled = true;
		}
	}
}
