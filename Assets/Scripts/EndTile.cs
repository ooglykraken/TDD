using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class EndTile : MonoBehaviour {

	public void OnTriggerEnter(Collider c){
		Enemy e  = c.transform.parent.gameObject.GetComponent<Enemy>();
		if(e != null){
			Debug.Log("Enemy made it through");
			e.Death();
			Gameplay.Instance().playerLives--;
		}
	}
}
