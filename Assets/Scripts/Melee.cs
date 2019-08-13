using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour {

	private int time;
	
	public void Awake(){
		time = 25;
	}
	
	public void Update(){
		if(time <= 0){
			Destroy(gameObject);
			Destroy(this);
		}
		time--;
	}
}
