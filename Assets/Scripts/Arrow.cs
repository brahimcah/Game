using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public GameObject archer;
	public GameObject player;
	public GameObject hitarea;
	public int direction;
	public int arrowspeed;

	public GameObject hitareainstance;
	private Vector3 hitareapos;
	private Vector3 pos;

	// Usa esto para la inicialización
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		pos = transform.position;
		
	}

    // L'actualització es crida una vegada per fotograma
    void Update () {
		if (direction == 0) { // boto a l 'esquerra
			transform.position = Vector3.MoveTowards (transform.position, pos + 30.0f*Vector3.left, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.left) {
				Destroy (this.gameObject);
			}
		
		} else if (direction == 1) { // boto de la dreta
			transform.position = Vector3.MoveTowards (transform.position, pos + 30.0f*Vector3.back, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.back) {
				Destroy (this.gameObject);
			}
		} else if (direction == 2) { 
			transform.position = Vector3.MoveTowards (transform.position, pos + 30.0f*Vector3.right, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.right) {
				Destroy (this.gameObject);
			}
		} else if (direction == 3) { 
			transform.position = Vector3.MoveTowards (transform.position, pos + 30.0f*Vector3.forward, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.forward) {
				Destroy (this.gameObject);
			}
		}
	
		if(Mathf.Abs(transform.position.x - player.transform.position.x) < 1.0f && Mathf.Abs(transform.position.z - player.transform.position.z) < 1.0f){ 
			player.GetComponent<PlayerControl>().damaged(20, direction, transform.position.x, transform.position.z);
			Destroy (this.gameObject);
		}
	}
}
