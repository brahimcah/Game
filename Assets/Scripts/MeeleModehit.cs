using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleModelhit : MonoBehaviour {

	public GameObject meele;

	public void notDmg(){ //Al final de l'animació danyada, torneu a l'estat normal i l'animació a inactiva
		meele.GetComponent<MeeleControl>().dmg = false;
		//player.GetComponent<PlayerControl>().attacking = false;
		meele.GetComponent<MeeleControl> ().animator.SetInteger ("playermove", 0);
	}

	public void stopAttacking(){ //al final de l'animació de l'atac, torneu a l'estat normal i l'animació a inactiva
		meele.GetComponent<MeeleControl>().attacking = false;
		meele.GetComponent<MeeleControl>().animator.SetInteger ("playermove", 0);
	}
}
