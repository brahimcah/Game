using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public GameObject player;
	public bool hpbar = false;
    //Cream la vairable dels exis de la camara
	public float cameraX = -75.0f;
	public float cameraY = 75.0f;
	public float cameraZ = -75.0f;

    // Utilitzeu això per a la inicialització
    void Start () {
		
	}

	// L'actualització es crida una vegada per fotograma
	void Update () {
		Vector3 pos = player.transform.position;
		//sumem a la variable del camara* el valor de posisicó*.
		pos.x += cameraX;
		pos.y += cameraY;
		pos.z += cameraZ;

		transform.position = pos;
		if (hpbar == true) {
			if (player.activeInHierarchy == false) {
				this.gameObject.SetActive (false);
			}
		}
	}
}
