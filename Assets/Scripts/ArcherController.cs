using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherController : MonoBehaviour {

	public GameObject arrow;
	public float alertdistance;
	public Animator animator;
	public GameObject player;
	public GameObject playerModel;
	public int faceDirection;
	public int hp = 3;
	public bool dmgd;
	public bool dead;
	public Image healthBar;
	public AudioSource audio;

	private RaycastHit hit;
	private int newfaceDirection;

	// Cream la inicialitzacio
	void Start () {
		audio = GetComponent<AudioSource> ();
		dead = false;
		dmgd = false;
	}
	
	// Anem actualitzant en cada frame 
	void Update () {
		Vector3 playerpos = player.transform.position;
		Vector3 enemypos = transform.position;
		float distance = Vector3.Distance(playerpos,enemypos);

		RaycastHit hit;
		if (playerpos.y == enemypos.y && distance <= alertdistance && !dmgd) { 
			animator.SetInteger ("alert", 1);
		} else if (!dmgd){
			animator.SetInteger ("alert", 0);
		}

		if (playerpos.y == enemypos.y && distance <= alertdistance && !dead && !dmgd) { 
            //ha de ser del mateix nivell per reconèixer el jugador i la distància entre l'enemic i el jugador ha d'estar a menys de la distancia asignada
                if (playerpos.x == enemypos.x) {
				if (playerpos.z > enemypos.z) { //Arquer a la part superior esquerra
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
					newfaceDirection = 3;
				} else { // Arquer a la part inferior dreta
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
					newfaceDirection = 1;
				}
			}
			else if (playerpos.z == enemypos.z) {
				if (playerpos.x > enemypos.x) { // Arquer a la part superior dreta
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
					newfaceDirection = 2;
				} else { // Arquer a la part superior esquerra
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if (blocked) {
						if (hit.collider.gameObject == playerModel) {
							animator.SetInteger ("alert", 2);
						}
					}
					newfaceDirection = 0;
				}
			}
			if (Mathf.Abs (playerpos.x - enemypos.x) >= Mathf.Abs (playerpos.z - enemypos.z)) { // si la distància entre x és més gran que la distància entre z no ataca el enemic
				if (playerpos.x > enemypos.x) { //si el jugador està darrere de l'enemic el enemic es gira
					newfaceDirection = 2;
				} else {
					newfaceDirection = 0;
				}
			} else { // si la distància entre z és major que la distància entre x 
				if (playerpos.z > enemypos.z) { //si el jugador esta a la esquerra de l'enemic el enemic es gira
					newfaceDirection = 3;
				} else {
					newfaceDirection = 1;
				}
			}
		}

		transform.Rotate (0, 90 * (faceDirection - newfaceDirection), 0);
		faceDirection = newfaceDirection;

		if (hp <= 0) {
			animator.SetInteger("alert", 4);
		}
	}
	
	//Cream una clase Shoot el cual serveix per disparar
	public void Shoot(){
		Vector3 arrowpos = transform.position;
		int dir = 0;

		if (faceDirection == 0) { //bottom left
			arrowpos += 2.0f * Vector3.left + 2.4f * Vector3.up;
			dir = 0;

		} else if (faceDirection == 1) { // bottom right
			arrowpos += 2.0f * Vector3.back + 2.4f * Vector3.up;
			dir = 1;
		} else if (faceDirection == 2) { //top right
			arrowpos += 2.0f * Vector3.right + 2.4f * Vector3.up;
			dir = 2;
		} else if (faceDirection == 3) { // top left
			arrowpos += 2.0f * Vector3.forward + 2.4f * Vector3.up;
			dir = 3;
		}

		GameObject arrowinstance = Instantiate(arrow, arrowpos, Quaternion.identity); 

		if (dir == 1 || dir == 3) {
			arrowinstance.transform.Rotate (0, 90, 0);
		}

		arrowinstance.GetComponent<Arrow> ().archer = this.gameObject;
		arrowinstance.GetComponent<Arrow> ().direction = dir;
	}

	public void getHit() {
		audio.Play ();
		hp -= 1;
		healthBar.fillAmount = hp / 3f;
		dmgd = true;
		animator.SetInteger ("alert", 5);

	}

	public void notDmg(){
		dmgd = false;
	}

	public void Death(){
		//player.GetComponent<PlayerControl>().attacking = false;
		this.gameObject.SetActive(false);
	}
}
