using UnityEngine;
using System.Collections;

public class MousePosition : MonoBehaviour {

	public Vector3 position;

	private void LateUpdate(){
		position = transform.position;
	
		Vector3 direction = Camera.main.transform.forward;//transform.up;
		
		Plane plane = new Plane(direction, transform.position);
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		
		if (!plane.Raycast(ray, out distance))
			return;
			
		Vector3 point = ray.GetPoint(distance);
		position = new Vector3(point.x, point.y, -50f);
	}
		
	private static MousePosition instance;
	public static MousePosition Instance(){
		if(instance == null){
			instance = new GameObject("MousePosition").AddComponent<MousePosition>();

		}
		
		return instance;
	}
}
