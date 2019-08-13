using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Enemy target;
	
	private int time;
	
	public void Awake(){
		//target = null;
		time = 60;
	}
	
	public void Update(){
		if(time <= 0){
			target = null;
			Destroy(gameObject);
			Destroy(this);
		}
		
		time--;
	}
	
	public void FixedUpdate(){
		if(target){
			if(Vector3.Distance(transform.position, target.transform.position) > 50){
				Vector3 targetRotation = Vector3.RotateTowards(transform.position, target.transform.position, 10f, 0f);
				transform.eulerAngles = new Vector3(0f, 0f, targetRotation.z - 150f);
				transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2500f * Time.deltaTime);
			} else{
				target = null;
				Destroy(gameObject);
				Destroy(this);
			}
		} else {
			Destroy(gameObject);
			Destroy(this);
		}
	}
}
